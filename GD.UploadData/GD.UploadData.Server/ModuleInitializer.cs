using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;

namespace GD.UploadData.Server
{
  public partial class ModuleInitializer
  {

    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      // Отчеты.
      InitializationLogger.Debug("Init: Grant right on reports to all users.");
      Reports.AccessRights.Grant(Reports.GetClassifierLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetCompaniesLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetPersonsLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetBusinessUnitsLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetEmployeesLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetLoginsLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetFileRetentionPeriodLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetCaseFileLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetCitiesLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetMunicipalAreasLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetSettlementsLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      
      Reports.AccessRights.Grant(Reports.GetContactsLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetRolesLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetAssociatedApplicationLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetRegistrationGroupLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      Reports.AccessRights.Grant(Reports.GetDocumentRegisterLoaderErrorReport().Info, Roles.AllUsers, DefaultReportAccessRightsTypes.Execute);
      CreateReportsTables();
    }
    
    /// <summary>
    /// Создать таблицы для отчетов.
    /// </summary>
    public static void CreateReportsTables()
    {
      var classifierLoaderErrorReportTableName = Constants.ClassifierLoaderErrorReport.SourceTableName;
      var companiesLoaderErrorReportTableName = Constants.CompaniesLoaderErrorReport.SourceTableName;
      var personsLoaderErrorReportTableName = Constants.PersonsLoaderErrorReport.SourceTableName;
      var jobTitlesLoaderErrorReportTableName = Constants.JobTitlesLoaderErrorReport.SourceTableName;
      var businessUnitsLoaderErrorReportTableName = Constants.BusinessUnitsLoaderErrorReport.SourceTableName;
      var employeesLoaderErrorReportTableName = Constants.EmployeesLoaderErrorReport.SourceTableName;
      var contactsLoaderErrorReportTableName = Constants.ContactsLoaderErrorReport.SourceTableName;
      var departmentsLoaderErrorReportTableName = Constants.DepartmentsLoaderErrorReport.SourceTableName;
      var loginsLoaderErrorReportTableName = Constants.LoginsLoaderErrorReport.SourceTableName;
      var fileRetentionPeriodErrorReportTableName = Constants.FileRetentionPeriodLoaderErrorReport.SourceTableName;
      var caseFileErrorReportTableName = Constants.CaseFileLoaderErrorReport.SourceTableName;
      var citiesLoaderErrorReportTableName = Constants.CitiesLoaderErrorReport.SourceTableName;
      var municipalAreasLoaderErrorReportTableName = Constants.MunicipalAreasLoaderErrorReport.SourceTableName;
      var settlementsLoaderErrorReportTableName = Constants.SettlementsLoaderErrorReport.SourceTableName;
      var registrationLoaderErrorReportTableName = Constants.RegistrationGroupLoaderErrorReport.SourceTableName;
      var documentRegisterLoaderErrorReportTableName = Constants.DocumentRegisterLoaderErrorReport.SourceTableName;
      
      Sungero.Docflow.PublicFunctions.Module.DropReportTempTables(new[] {
                                                                    classifierLoaderErrorReportTableName,
                                                                    companiesLoaderErrorReportTableName,
                                                                    personsLoaderErrorReportTableName,
                                                                    jobTitlesLoaderErrorReportTableName,
                                                                    businessUnitsLoaderErrorReportTableName,
                                                                    departmentsLoaderErrorReportTableName,
                                                                    loginsLoaderErrorReportTableName,
                                                                    employeesLoaderErrorReportTableName,
                                                                    contactsLoaderErrorReportTableName,
                                                                    caseFileErrorReportTableName,
                                                                    fileRetentionPeriodErrorReportTableName,
                                                                    citiesLoaderErrorReportTableName,
                                                                    municipalAreasLoaderErrorReportTableName,
                                                                    settlementsLoaderErrorReportTableName,
                                                                    registrationLoaderErrorReportTableName,
                                                                    documentRegisterLoaderErrorReportTableName});

      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.ClassifierLoaderErrorReport.CreateSourceTable, new[] {classifierLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.CompaniesLoaderErrorReport.CreateSourceTable, new[] {companiesLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.PersonsLoaderErrorReport.CreateSourceTable, new[] {personsLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.JobTitlesLoaderErrorReport.CreateSourceTable, new[] {jobTitlesLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.BusinessUnitsLoaderErrorReport.CreateSourceTable, new[] {businessUnitsLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.EmployeesLoaderErrorReport.CreateSourceTable, new[] {employeesLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.ContactsLoaderErrorReport.CreateSourceTable, new[] {contactsLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.DepartmentsLoaderErrorReport.CreateSourceTable, new[] {departmentsLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.LoginsLoaderErrorReport.CreateSourceTable, new[] {loginsLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.CaseFileLoaderErrorReport.CreateSourceTable, new[] {caseFileErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.FileRetentionPeriodLoaderErrorReport.CreateSourceTable, new[] {fileRetentionPeriodErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.CitiesLoaderErrorReport.CreateSourceTable, new[] {citiesLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.MunicipalAreasLoaderErrorReport.CreateSourceTable, new[] {municipalAreasLoaderErrorReportTableName});
      
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.SettlementsLoaderErrorReport.CreateSourceTable, new[] {settlementsLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.RegistrationGroupLoaderErrorReport.CreateSourceTable, new[]
                                                                     {registrationLoaderErrorReportTableName});
      Sungero.Docflow.PublicFunctions.Module.ExecuteSQLCommandFormat(Queries.DocumentRegisterLoaderErrorReport.CreateSourceTable, new[]
                                                                     {documentRegisterLoaderErrorReportTableName});
    }
  }
}
