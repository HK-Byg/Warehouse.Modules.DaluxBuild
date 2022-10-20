# Warehouse module: DaluxBuild

This module, fetches data from [Dalux Build](https://www.dalux.com/da/dalux-field) each day, and stores it into your own data warehouse on Azure.

The module is build with [Bygdrift Warehouse](https://github.com/Bygdrift/Warehouse), that enables one to attach multiple modules within the same azure environment.
It can collect and wash data from all kinds of services, in a data lake and database.
By saving data to a MS SQL database, it is:
- easy to fetch data with Power BI, Excel and other systems
- easy to control who has access to what - actually, it can be controlled with AD so you don't have to handle credentials
- It's cheap

## Installation

All modules can be installed and facilitated with ARM templates (Azure Resource Management): [Use ARM templates to setup and maintain this module](https://github.com/HK-Byg/Warehouse.Modules.DaluxBuild/blob/master/Deploy).

## Database content

| TABLE_NAME        | COLUMN_NAME                   | DATA_TYPE      |
| :---------------- | :---------------------------- | :------------- |
| Projects          | ProjectID                     | int            |
| Projects          | Address                       | varchar        |
| Projects          | BoxType                       | varchar        |
| Projects          | Created                       | datetimeoffset |
| Projects          | Name                          | varchar        |
| Projects          | Number                        | varchar        |
| Projects          | * All meta data columns *     | varchar        |
| ProjectsUsers     | ProjectID                     | int            |
| ProjectsUsers     | UserID                        | varchar        |
| ProjectsUsers     | Email                         | varchar        |
| ProjectsUsers     | Firstname                     | varchar        |
| ProjectsUsers     | Lastname                      | varchar        |
| ProjectsUsers     | CompanyID                     | int            |
| ProjectsUsers     | CompanyName                   | varchar        |
| ProjectsUsers     | CompanyVAT                    | varchar        |
| ProjectsContracts | ProjectID                     | int            |
| ProjectsContracts | UserID                        | int            |
| ProjectsContracts | Email                         | int            |
| ProjectsContracts | Firstname                     | varchar        |
| ProjectsContracts | Lastname                      | varchar        |
| ProjectsContracts | CompanyID                     | varchar        |
| ProjectsContracts | CompanyName                   | varchar        |
| ProjectsContracts | CompanyVAT                    | varchar        |
| ProjectsApprovals | ApprovalID                    | int            |
| ProjectsApprovals | Number                        | varchar        |
| ProjectsApprovals | CreatedByUserMail             | varchar        |
| ProjectsApprovals | CreatedByUserID               | varchar        |
| ProjectsApprovals | CreatedByCompanyName          | varchar        |
| ProjectsApprovals | CreatedByCompanyId            | int            |
| ProjectsApprovals | Created                       | datetime       |
| ProjectsApprovals | InspectionType                | varchar        |
| ProjectsApprovals | IsDeleted                     | bit            |
| ProjectsApprovals | ProjectID                     | int            |
| ProjectsApprovals | ProjectName                   | varchar        |
| ProjectsApprovals | ProjectNumber                 | varchar        |
| ProjectsApprovals | RoleName                      | varchar        |
| ProjectsApprovals | RevisionList                  | varchar        |
| ProjectsApprovals | LocationList                  | varchar        |
| ProjectChecklists | BuildingObjectInfo            | varchar        |
| ProjectChecklists | ChecklistId                   | int            |
| ProjectChecklists | Closed                        | bit            |
| ProjectChecklists | CreatedByUserMail             | varchar        |
| ProjectChecklists | CreatedByUserID               | varchar        |
| ProjectChecklists | CreatedByCompanyName          | varchar        |
| ProjectChecklists | CreatedByCompanyID            | int            |
| ProjectChecklists | CreatedByDateTime             | datetime       |
| ProjectChecklists | IsDeleted                     | bit            |
| ProjectChecklists | LastModifiedByUserMail        | varchar        |
| ProjectChecklists | LastModifiedByUserID          | varchar        |
| ProjectChecklists | LastModifiedByCompanyName     | varchar        |
| ProjectChecklists | LastModifiedByCompanyID       | int            |
| ProjectChecklists | LastModifiedDateTime          | datetime       |
| ProjectChecklists | ProjectID                     | int            |
| ProjectChecklists | ProjectName                   | varchar        |
| ProjectChecklists | ProjectNumber                 | varchar        |
| ProjectChecklists | Safety                        | bit            |
| ProjectChecklists | LocationList                  | varchar        |
| ProjectChecklists | ExtensionsDataList            | varchar        |
| ProjectChecklists | ViewPointImageList            | varchar        |
| ProjectsCompanies | ProjectID                     | int            |
| ProjectsCompanies | CompanyID                     | int            |
| ProjectsCompanies | Address                       | varchar        |
| ProjectsCompanies | CountryCode                   | varchar        |
| ProjectsCompanies | Name                          | varchar        |
| ProjectsCompanies | VAT                           | varchar        |
| Log               | TimeStamp                     | datetimeoffset |
| Log               | Errors                        | int            |
| Log               | ErrorDetails                  | varchar        |

## Data lake content

In the data lake container with this modules name, there are two main folders `Raw` and `Refined`.

The folder structure:
+ Raw
    - {yyyy the year}
        - {MM the month}
            - {dd the month}
                - projects.json
                - project `id`_approvals.json
                - project `id`_checklists.json
                - project `id`_companies.json
                - project `id`_contracts.json
                - project `id`_users.json
+ Refined
    - {yyyy the year}
        - {MM the month}
            - {dd the month}
                - Log.csv
                - Projects.csv
                - ProjectsApprovals.csv
                - ProjectsChecklists.csv
                - ProjectsCompanies.csv
                - ProjectsContracts.csv
                - ProjectsUsers.csv

# License

[MIT License](https://github.com/Bygdrift/Warehouse.Modules.Example/blob/master/License.md)