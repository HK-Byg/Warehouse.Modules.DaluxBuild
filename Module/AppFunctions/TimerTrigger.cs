using Bygdrift.Warehouse;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Module.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Module.AppFunctions
{
    public class TimerTrigger
    {
        public TimerTrigger(ILogger<TimerTrigger> logger, bool saveToDataLake = true, bool saveToDatabase = true)
        {
            App = new AppBase<Settings>(logger);
            SaveToDataLake = saveToDataLake;
            SaveToDatabase = saveToDatabase;
        }

        public AppBase<Settings> App { get; private set; }
        public bool SaveToDataLake { get; }
        public bool SaveToDatabase { get; }

        [FunctionName(nameof(TimerTriggerAsync))]
        public async Task TimerTriggerAsync([TimerTrigger("%ScheduleExpression%")] TimerInfo myTimer)
        {
            App.Log.LogInformation($"The module '{App.ModuleName}' is started");
            var service = new WebService(App, SaveToDataLake);

            var projects = await service.GetProjectsAsync();
            await new Refines.ProjectsRefine(App, SaveToDataLake, SaveToDatabase).Refine(projects);
            
            var projectUsers = await service.GetProjectsUsersAsync(projects);
            await new Refines.ProjectsUsersRefine(App, SaveToDataLake, SaveToDatabase).Refine(projectUsers);

            var projectContracts = await service.GetProjectsContractsAsync(projects);
            await new Refines.ProjectsContractsRefine(App, SaveToDataLake, SaveToDatabase).Refine(projectContracts);

            var projectsApprovals = await service.GetProjectsApprovalsAsync(projects);
            await new Refines.ProjectsApprovalsRefine(App, SaveToDataLake, SaveToDatabase).Refine(projectsApprovals.ToList());

            var projectsChecklists = await service.GetProjectsCheckListsAsync(projects);
            await new Refines.ProjectsChecklistsRefine(App, SaveToDataLake, SaveToDatabase).Refine(projectsChecklists.ToList());

            var projectsCompanies = await service.GetProjectsCompaniesAsync(projects);
            await new Refines.ProjectsCompaniesRefine(App, SaveToDataLake, SaveToDatabase).Refine(projectsCompanies);

            await new Refines.Log(App, SaveToDataLake, SaveToDatabase).WriteLog();
        }
    }
}