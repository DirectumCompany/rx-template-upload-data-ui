using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using ClosedXML.Excel;
using Sungero.Docflow;

namespace GD.UploadData.Client
{
  public class ModuleFunctions
  {

    #region Загрузка организационной структуры

    #region Организации
    
    /// <summary>
    /// Загрузить организации из организационной структуры.
    /// </summary>
    public virtual void LoadCompanies()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadBusinessUnitDialogTitle);
      if (file == null)
        return;
      var companies = GetCompaniesFromExcel(file);
      companies = Functions.Module.Remote.CreateOrUpdateCompanies(companies);
      var companiesWithError = companies.Where(x => !string.IsNullOrEmpty(x.Error));
      if (companiesWithError.Any())
        ShowCompaniesLoaderReport(companiesWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(companies.Count, companiesWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника организации из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список организаций.</returns>
    private List<Structures.Module.Company> GetCompaniesFromExcel(byte[] file)
    {
      List<Structures.Module.Company> companies = new List<Structures.Module.Company>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var company = Structures.Module.Company.Create();
          try
          {
            company.Name = range.Cell(1,1).Value.ToString();
            company.LegalName = range.Cell(1,2).Value.ToString();
            company.HeadCompany = range.Cell(1,3).Value.ToString();
            company.Nonresident = range.Cell(1,4).Value.ToString();
            company.TIN = range.Cell(1,5).Value.ToString();
            company.TRRC = range.Cell(1,6).Value.ToString();
            company.PSRN = range.Cell(1,7).Value.ToString();
            company.NCEO = range.Cell(1,8).Value.ToString();
            company.NCEA = range.Cell(1,9).Value.ToString();
            company.City = range.Cell(1,10).Value.ToString();
            company.Region = range.Cell(1,11).Value.ToString();
            company.LegalAddress = range.Cell(1,12).Value.ToString();
            company.PostalAddress = range.Cell(1,13).Value.ToString();
            company.Phones = range.Cell(1,14).Value.ToString();
            company.Email = range.Cell(1,15).Value.ToString();
            company.Homepage = range.Cell(1,16).Value.ToString();
            company.Note = range.Cell(1,17).Value.ToString();
            company.Account = range.Cell(1,18).Value.ToString();
            company.Bank = range.Cell(1,19).Value.ToString();
          }
          catch (Exception ex)
          {
            company.Error = ex.Message;
          }
          
          companies.Add(company);
          currentRow++;
        }
      }
      
      return companies;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке организаций".
    /// </summary>
    /// <param name="companies">Список организаций.</param>
    private void ShowCompaniesLoaderReport(List<Structures.Module.Company> companies)
    {
      var report = Reports.GetCompaniesLoaderErrorReport();
      var errorText = string.Join(";", companies.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}",
                                                                           x.Name,
                                                                           x.LegalName,
                                                                           x.HeadCompany,
                                                                           x.Nonresident,
                                                                           x.TIN,
                                                                           x.TRRC,
                                                                           x.PSRN,
                                                                           x.NCEO,
                                                                           x.NCEA,
                                                                           x.City,
                                                                           x.Region,
                                                                           x.LegalAddress,
                                                                           x.PostalAddress,
                                                                           x.Phones,
                                                                           x.Email,
                                                                           x.Homepage,
                                                                           x.Note,
                                                                           x.Account,
                                                                           x.Bank,
                                                                           x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }

    #endregion
    
    #region Персоны
    
    /// <summary>
    /// Загрузить персоны из организационной структуры.
    /// </summary>
    public virtual void LoadPersons()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadPersonDialogTitle);
      if (file == null)
        return;
      var persons = GetPersonsFromExcel(file);
      persons = Functions.Module.Remote.CreateOrUpdatePersons(persons);
      var personsWithError = persons.Where(x => !string.IsNullOrEmpty(x.Error));
      if (personsWithError.Any())
        ShowPersonsLoaderReport(personsWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(persons.Count, personsWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника персоны из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список персон.</returns>
    private List<Structures.Module.Person> GetPersonsFromExcel(byte[] file)
    {
      List<Structures.Module.Person> persons = new List<Structures.Module.Person>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var person = Structures.Module.Person.Create();
          try
          {
            person.LastName = range.Cell(1,1).Value.ToString();
            person.FirstName = range.Cell(1,2).Value.ToString();
            person.MiddleName = range.Cell(1,3).Value.ToString();
            person.Sex = range.Cell(1,4).Value.ToString();
            person.DateOfBirth = range.Cell(1,5).Value.ToString();
            person.TIN = range.Cell(1,6).Value.ToString();
            person.INILA = range.Cell(1,7).Value.ToString();
            person.City = range.Cell(1,8).Value.ToString();
            person.Region = range.Cell(1,9).Value.ToString();
            person.LegalAddress = range.Cell(1,10).Value.ToString();
            person.PostalAddress = range.Cell(1,11).Value.ToString();
            person.Phones = range.Cell(1,12).Value.ToString();
            person.Email = range.Cell(1,13).Value.ToString();
            person.Homepage = range.Cell(1,14).Value.ToString();
            person.Note = range.Cell(1,15).Value.ToString();
            person.Account = range.Cell(1,16).Value.ToString();
            person.Bank = range.Cell(1,17).Value.ToString();
          }
          catch (Exception ex)
          {
            person.Error = ex.Message;
          }
          
          persons.Add(person);
          currentRow++;
        }
      }
      
      return persons;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке персон".
    /// </summary>
    /// <param name="persons">Список персон.</param>
    private void ShowPersonsLoaderReport(List<Structures.Module.Person> persons)
    {
      var report = Reports.GetPersonsLoaderErrorReport();
      var errorText = string.Join(";", persons.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}",
                                                                         x.LastName,
                                                                         x.FirstName,
                                                                         x.MiddleName,
                                                                         x.Sex,
                                                                         x.DateOfBirth,
                                                                         x.TIN,
                                                                         x.INILA,
                                                                         x.City,
                                                                         x.Region,
                                                                         x.LegalAddress,
                                                                         x.PostalAddress,
                                                                         x.Phones,
                                                                         x.Email,
                                                                         x.Homepage,
                                                                         x.Note,
                                                                         x.Account,
                                                                         x.Bank,
                                                                         x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Должности
    
    /// <summary>
    /// Загрузить должности из организационной структуры.
    /// </summary>
    public virtual void LoadJobTitles()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadJobTitleDialogTitle);
      if (file == null)
        return;
      var jobTitle = GetJobTitlesFromExcel(file);
      jobTitle = Functions.Module.Remote.CreateOrUpdateJobTitles(jobTitle);
      var jobTitleWithError = jobTitle.Where(x => !string.IsNullOrEmpty(x.Error));
      if (jobTitleWithError.Any())
        ShowJobTitlesLoaderReport(jobTitleWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(jobTitle.Count, jobTitleWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника должности из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список должнностей.</returns>
    private List<Structures.Module.JobTitle> GetJobTitlesFromExcel(byte[] file)
    {
      List<Structures.Module.JobTitle> jobTitles = new List<Structures.Module.JobTitle>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var jobTitle = Structures.Module.JobTitle.Create();
          try
          {
            jobTitle.Name = range.Cell(1,1).Value.ToString();
            jobTitle.Department = range.Cell(1,2).Value.ToString();
          }
          catch (Exception ex)
          {
            jobTitle.Error = ex.Message;
          }
          
          jobTitles.Add(jobTitle);
          currentRow++;
        }
      }
      
      return jobTitles;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке должностей".
    /// </summary>
    /// <param name="jobTitles">Список должностей.</param>
    private void ShowJobTitlesLoaderReport(List<Structures.Module.JobTitle> jobTitles)
    {
      var report = Reports.GetJobTitlesLoaderErrorReport();
      var errorText = string.Join(";", jobTitles.Select(x => string.Format("{0}|{1}|{2}",
                                                                           x.Name,
                                                                           x.Department,
                                                                           x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Наши организации
    
    /// <summary>
    /// Загрузить наших организаций из организационной структуры.
    /// </summary>
    public virtual void LoadBusinessUnits()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadBusinessUnitDialogTitle);
      if (file == null)
        return;
      var businessUnits = GetBusinessUnitsFromExcel(file);
      businessUnits = Functions.Module.Remote.CreateOrUpdateBusinessUnits(businessUnits);
      var businessUnitsWithError = businessUnits.Where(x => !string.IsNullOrEmpty(x.Error));
      if (businessUnitsWithError.Any())
        ShowBusinessUnitsLoaderReport(businessUnitsWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(businessUnits.Count, businessUnitsWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника наши организации из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список наших организаций.</returns>
    private List<Structures.Module.BusinessUnit> GetBusinessUnitsFromExcel(byte[] file)
    {
      List<Structures.Module.BusinessUnit> businessUnits = new List<Structures.Module.BusinessUnit>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var businessUnit = Structures.Module.BusinessUnit.Create();
          try
          {
            businessUnit.Name = range.Cell(1,1).Value.ToString();
            businessUnit.LegalName = range.Cell(1,2).Value.ToString();
            businessUnit.HeadCompany = range.Cell(1,3).Value.ToString();
            businessUnit.CEO = range.Cell(1,4).Value.ToString();
            businessUnit.TIN = range.Cell(1,5).Value.ToString();
            businessUnit.TRRC = range.Cell(1,6).Value.ToString();
            businessUnit.PSRN = range.Cell(1,7).Value.ToString();
            businessUnit.NCEO = range.Cell(1,8).Value.ToString();
            businessUnit.NCEA = range.Cell(1,9).Value.ToString();
            businessUnit.City = range.Cell(1,10).Value.ToString();
            businessUnit.Region = range.Cell(1,11).Value.ToString();
            businessUnit.LegalAddress = range.Cell(1,12).Value.ToString();
            businessUnit.PostalAddress = range.Cell(1,13).Value.ToString();
            businessUnit.Phones = range.Cell(1,14).Value.ToString();
            businessUnit.Email = range.Cell(1,15).Value.ToString();
            businessUnit.Homepage = range.Cell(1,16).Value.ToString();
            businessUnit.Note = range.Cell(1,17).Value.ToString();
            businessUnit.Account = range.Cell(1,18).Value.ToString();
            businessUnit.Bank = range.Cell(1,19).Value.ToString();
          }
          catch (Exception ex)
          {
            businessUnit.Error = ex.Message;
          }
          
          businessUnits.Add(businessUnit);
          currentRow++;
        }
      }
      
      return businessUnits;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке наших организаций".
    /// </summary>
    /// <param name="businessUnits">Список наших организаций.</param>
    private void ShowBusinessUnitsLoaderReport(List<Structures.Module.BusinessUnit> businessUnits)
    {
      var report = Reports.GetBusinessUnitsLoaderErrorReport();
      var errorText = string.Join(";", businessUnits.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}",
                                                                               x.Name,
                                                                               x.LegalName,
                                                                               x.HeadCompany,
                                                                               x.CEO,
                                                                               x.TIN,
                                                                               x.TRRC,
                                                                               x.PSRN,
                                                                               x.NCEO,
                                                                               x.NCEA,
                                                                               x.City,
                                                                               x.Region,
                                                                               x.LegalAddress,
                                                                               x.PostalAddress,
                                                                               x.Phones,
                                                                               x.Email,
                                                                               x.Homepage,
                                                                               x.Note,
                                                                               x.Account,
                                                                               x.Bank,
                                                                               x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }

    #endregion
    
    #region Подразделения
    
    /// <summary>
    /// Загрузить подразделения из организационной структуры.
    /// </summary>
    public virtual void LoadDepartments()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadDepartmentDialogTitle);
      if (file == null)
        return;
      var departments = GetDepartmentsFromExcel(file);
      departments = Functions.Module.Remote.CreateOrUpdateDepartments(departments);
      var departmentsWithError = departments.Where(x => !string.IsNullOrEmpty(x.Error));
      if (departmentsWithError.Any())
        ShowDepartmentsLoaderReport(departmentsWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(departments.Count, departmentsWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника подразделений из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список подразделений.</returns>
    private List<Structures.Module.Department> GetDepartmentsFromExcel(byte[] file)
    {
      List<Structures.Module.Department> departments = new List<Structures.Module.Department>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var department = Structures.Module.Department.Create();
          try
          {
            department.Name = range.Cell(1,1).Value.ToString();
            department.ShortName = range.Cell(1,2).Value.ToString();
            department.HeadOffice = range.Cell(1,3).Value.ToString();
            department.BusinessUnit = range.Cell(1,4).Value.ToString();
            department.Code = range.Cell(1,5).Value.ToString();
            department.Manager = range.Cell(1,6).Value.ToString();
            department.Phone = range.Cell(1,7).Value.ToString();
            department.Description = range.Cell(1,8).Value.ToString();
          }
          catch (Exception ex)
          {
            department.Error = ex.Message;
          }
          
          departments.Add(department);
          currentRow++;
        }
      }
      
      return departments;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке подразделений".
    /// </summary>
    /// <param name="departments">Список подразделений.</param>
    private void ShowDepartmentsLoaderReport(List<Structures.Module.Department> departments)
    {
      var report = Reports.GetDepartmentsLoaderErrorReport();
      var errorText = string.Join(";", departments.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                                                                             x.Name,
                                                                             x.ShortName,
                                                                             x.HeadOffice,
                                                                             x.BusinessUnit,
                                                                             x.Code,
                                                                             x.Manager,
                                                                             x.Phone,
                                                                             x.Description,
                                                                             x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }

    #endregion
    
    #region Работники
    
    /// <summary>
    /// Загрузить работников из организационной структуры.
    /// </summary>
    public virtual void LoadEmployees()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadEmployeeDialogTitle);
      if (file == null)
        return;
      var employees = GetEmployeesFromExcel(file);
      employees = Functions.Module.Remote.CreateOrUpdateEmployees(employees);
      var employeesWithError = employees.Where(x => !string.IsNullOrEmpty(x.Error));
      if (employeesWithError.Any())
        ShowEmployeesLoaderReport(employeesWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(employees.Count, employeesWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника работников из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список работников.</returns>
    private List<Structures.Module.Employee> GetEmployeesFromExcel(byte[] file)
    {
      List<Structures.Module.Employee> employees = new List<Structures.Module.Employee>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var employee = Structures.Module.Employee.Create();
          try
          {
            employee.Person = range.Cell(1,1).Value.ToString();
            employee.Login = range.Cell(1,2).Value.ToString();
            employee.BusinessUnit = range.Cell(1,3).Value.ToString();
            employee.Department = range.Cell(1,4).Value.ToString();
            employee.JobTitle = range.Cell(1,5).Value.ToString();
            employee.PersonnelNumber = range.Cell(1,6).Value.ToString();
            employee.Phone = range.Cell(1,7).Value.ToString();
            employee.Email = range.Cell(1,8).Value.ToString();
            employee.Description = range.Cell(1,9).Value.ToString();
          }
          catch (Exception ex)
          {
            employee.Error = ex.Message;
          }
          
          employees.Add(employee);
          currentRow++;
        }
      }
      
      return employees;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке работников".
    /// </summary>
    /// <param name="employees">Список работников.</param>
    private void ShowEmployeesLoaderReport(List<Structures.Module.Employee> employees)
    {
      var report = Reports.GetEmployeesLoaderErrorReport();
      var errorText = string.Join(";", employees.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}",
                                                                           x.Person,
                                                                           x.Login,
                                                                           x.BusinessUnit,
                                                                           x.Department,
                                                                           x.JobTitle,
                                                                           x.PersonnelNumber,
                                                                           x.Phone,
                                                                           x.Email,
                                                                           x.Description,
                                                                           x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Учетные записи
    
    /// <summary>
    /// Загрузить учетные записи из организационной структуры.
    /// </summary>
    public virtual void LoadLogins()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadJobTitleDialogTitle);
      if (file == null)
        return;
      var login = GetLoginsFromExcel(file);
      login = Functions.Module.Remote.CreateOrUpdateLogins(login);
      var loginWithError = login.Where(x => !string.IsNullOrEmpty(x.Error));
      if (loginWithError.Any())
        ShowLoginsLoaderReport(loginWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(login.Count, loginWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника учетные записи из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список учетных записей.</returns>
    private List<Structures.Module.Login> GetLoginsFromExcel(byte[] file)
    {
      List<Structures.Module.Login> logins = new List<Structures.Module.Login>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var login = Structures.Module.Login.Create();
          try
          {
            login.Name = range.Cell(1,1).Value.ToString();
          }
          catch (Exception ex)
          {
            login.Error = ex.Message;
          }
          
          logins.Add(login);
          currentRow++;
        }
      }
      
      return logins;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Учетных записей".
    /// </summary>
    /// <param name="jobTitles">Список должностей.</param>
    private void ShowLoginsLoaderReport(List<Structures.Module.Login> logins)
    {
      var report = Reports.GetLoginsLoaderErrorReport();
      var errorText = string.Join(";", logins.Select(x => string.Format("{0}|{1}",
                                                                        x.Name,
                                                                        x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Контактные лица
    
    /// <summary>
    /// Загрузить контактные лица.
    /// </summary>
    public void LoadContacts()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadContacts);
      if (file == null)
        return;
      var contacts = GetContactsFromExcel(file);
      contacts = Functions.Module.Remote.CreateOrUpdateContacts(contacts);
      var contactsWithError = contacts.Where(c => !string.IsNullOrEmpty(c.Error));
      if (contactsWithError.Any())
        ShowContactsLoaderReport(contactsWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(contacts.Count, contactsWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника контактные лица из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список контактных лиц.</returns>
    public List<Structures.Module.Contact> GetContactsFromExcel(byte[] file)
    {
      var contacts = new List<Structures.Module.Contact>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 5)).IsEmpty())
        {
          var contact = Structures.Module.Contact.Create();
          try
          {
            contact.FullName = string.Join(" ", range.Cell(1,1).Value.ToString()?.Trim(), range.Cell(1,2).Value.ToString()?.Trim(),
                                           range.Cell(1,3).Value.ToString()?.Trim());
            contact.LastName = range.Cell(1,1).Value.ToString()?.Trim();
            contact.Name = range.Cell(1,2).Value.ToString()?.Trim();
            contact.MiddleName = range.Cell(1,3).Value.ToString()?.Trim();
            contact.Company = range.Cell(1,4).Value.ToString()?.Trim();
            contact.JobTitle = range.Cell(1,5).Value.ToString()?.Trim();
            contact.Phone = range.Cell(1,6).Value.ToString()?.Trim();
            contact.Fax = range.Cell(1,7).Value.ToString()?.Trim();
            contact.Email = range.Cell(1,8).Value.ToString()?.Trim();
            contact.Homepage = range.Cell(1,9).Value.ToString()?.Trim();
            contact.Note = range.Cell(1,10).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            contact.Error = ex.Message;
          }
          
          contacts.Add(contact);
          currentRow++;
        }
      }
      return contacts;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Контактных лиц".
    /// </summary>
    /// <param name="contact">Список контактных лиц.</param>
    private void ShowContactsLoaderReport(List<Structures.Module.Contact> contact)
    {
      var report = Reports.GetContactsLoaderErrorReport();
      var errorText = string.Join(";", contact.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
                                                                         x.LastName, x.Name, x.MiddleName, x.Company, x.JobTitle, x.Phone,
                                                                         x.Fax, x.Email, x.Homepage, x.Note, x.Error)));
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Роли
    
    /// <summary>
    /// Загрузить роли.
    /// </summary>
    public void LoadRoles()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadRoles);
      if (file == null)
        return;
      var roles = GetRolesFromExcel(file);
      roles = Functions.Module.Remote.CreateOrUpdateRoles(roles);
      var rolesWithError = roles.Where(c => !string.IsNullOrEmpty(c.Error));
      if (rolesWithError.Any())
        ShowRolesLoaderReport(rolesWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(roles.Count, rolesWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника Роли из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список ролей.</returns>
    public List<Structures.Module.Role> GetRolesFromExcel(byte[] file)
    {
      var roles = new List<Structures.Module.Role>();
      
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 4)).IsEmpty())
        {
          var role = Structures.Module.Role.Create();
          try
          {
            role.Name = range.Cell(1,1).Value.ToString()?.Trim();
            role.Note = range.Cell(1,2).Value.ToString()?.Trim();
            role.Recipients = range.Cell(1,3).Value.ToString()?.Trim();
            role.IsSingleUser = range.Cell(1,4).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            role.Error = ex.Message;
          }
          
          roles.Add(role);
          currentRow++;
        }
      }
      return roles;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Ролей".
    /// </summary>
    /// <param name="roles">Список должностей.</param>
    private void ShowRolesLoaderReport(List<Structures.Module.Role> roles)
    {
      var report = Reports.GetRolesLoaderErrorReport();
      var errorText = string.Join("#", roles.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}", x.Name, x.Note, x.IsSingleUser, x.Recipients, x.Error)));
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Приложения-обработчики
    
    /// <summary>
    /// Загрузить приложения обработчики.
    /// </summary>
    public void LoadAssociatedApplications()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadAssociatedApplications);
      if (file == null)
        return;
      var applications = GetAssociatedApplicationsFromFile(file);
      applications = Functions.Module.Remote.CreateOrUpdateAssociatedApplications(applications);
      var applicationsWithError = applications.Where(a => !string.IsNullOrEmpty(a.Error));
      if (applicationsWithError.Any())
        ShowAssociatedApplicationsLoaderReport(applicationsWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(applications.Count, applicationsWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника обработчики приложений из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список обработчиков приложений.</returns>
    public List<Structures.Module.AssociatedApplication> GetAssociatedApplicationsFromFile(byte[] file)
    {
      var applications = new List<Structures.Module.AssociatedApplication>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 5)).IsEmpty())
        {
          var application = Structures.Module.AssociatedApplication.Create();
          try
          {
            application.Name = range.Cell(1,1).Value.ToString()?.Trim();
            application.Extension = range.Cell(1,2).Value.ToString()?.Trim();
            application.MonitoringType = range.Cell(1,3).Value.ToString()?.Trim();
            application.OpenByDefaultForReading = range.Cell(1,4).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            application.Error = ex.Message;
          }
          
          applications.Add(application);
          currentRow++;
        }
      }
      
      return applications;
    }

    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Обработчиков приложений".
    /// </summary>
    /// <param name="applications">Список обработчиков приложений.</param>
    public void ShowAssociatedApplicationsLoaderReport(List<Structures.Module.AssociatedApplication> applications)
    {
      var report = Reports.GetAssociatedApplicationLoaderErrorReport();
      var errorText = string.Join(";", applications.Select(a => string.Format("{0}|{1}|{2}|{3}|{4}|",
                                                                              a.Name, a.Extension, a.MonitoringType,
                                                                              a.OpenByDefaultForReading, a.Error)));
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #endregion
    
    #region Группы регистрации
    
    /// <summary>
    /// Загрузить группы регистрации.
    /// </summary>
    public void LoadRegistrationGroup()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadRegistrationGroup);
      if (file == null)
        return;
      var registrationGroups = GetRegistrationGroupFromExcel(file);
      registrationGroups = Functions.Module.Remote.CreateOrUpdateRegistrationGroup(registrationGroups);
      var registrationGroupWithError = registrationGroups.Where(c => !string.IsNullOrEmpty(c.Error));
      if (registrationGroupWithError.Any())
        ShowRegistrationGroupsLoaderReport(registrationGroupWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(registrationGroups.Count, registrationGroupWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника группы регистрации из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список групп регистрации.</returns>
    public List<Structures.Module.RegistrationGroup> GetRegistrationGroupFromExcel(byte[] file)
    {
      var registrationGroups = new List<Structures.Module.RegistrationGroup>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 5)).IsEmpty())
        {
          var registrationGroup = Structures.Module.RegistrationGroup.Create();
          
          try
          {
            registrationGroup.Name = range.Cell(1,1).Value.ToString()?.Trim();
            registrationGroup.Index = range.Cell(1,2).Value.ToString()?.Trim();
            registrationGroup.ResponsibleEmployee = range.Cell(1,3).Value.ToString()?.Trim();
            registrationGroup.DocumentFlow = range.Cell(1,4).Value.ToString()?.Trim();
            registrationGroup.RecipientLinks = range.Cell(1,5).Value.ToString()?.Trim();
            registrationGroup.Departments = range.Cell(1,6).Value.ToString()?.Trim();
            registrationGroup.Description = range.Cell(1,7).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            registrationGroup.Error = ex.Message;
          }
          currentRow++;
          registrationGroups.Add(registrationGroup);
        }
      }
      return registrationGroups;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Групп регистрации".
    /// </summary>
    /// <param name="registrationsGroups">Список групп регистрации.</param>
    public void ShowRegistrationGroupsLoaderReport(List<Structures.Module.RegistrationGroup> registrationsGroups)
    {
      var report = Reports.GetRegistrationGroupLoaderErrorReport();
      var errorText = string.Join("#", registrationsGroups.Select(a => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",
                                                                                     a.Name, a.Index, a.ResponsibleEmployee,
                                                                                     a.DocumentFlow, a.RecipientLinks,
                                                                                     a.Departments, a.Description, a.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Журналы регистраций
    
    /// <summary>
    /// Загрузить журнал регистрации.
    /// </summary>
    public void LoadDocumentRegister()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadDocumentRegister);
      if (file == null)
        return;
      var documentRegisters = GetDocumentRegisterFromExcel(file);
      documentRegisters = Functions.Module.Remote.CreateorUpdateDocumentRegister(documentRegisters);
      var documentRegistersWithError = documentRegisters.Where(c => !string.IsNullOrEmpty(c.Error));
      if (documentRegistersWithError.Any())
        ShowDocumentRegisterLoaderReport(documentRegistersWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(documentRegisters.Count, documentRegistersWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника Журнал регистрации из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список журналова регистрации.</returns>
    public List<Structures.Module.DocumentRegister> GetDocumentRegisterFromExcel(byte[] file)
    {
      var documentRegisters = new List<Structures.Module.DocumentRegister>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 5)).IsEmpty())
        {
          var documentRegister = Structures.Module.DocumentRegister.Create();
          
          try
          {
            documentRegister.Name = range.Cell(1,1).Value.ToString()?.Trim();
            documentRegister.RegisterType = range.Cell(1,2).Value.ToString()?.Trim();
            documentRegister.Index = range.Cell(1,3).Value.ToString()?.Trim();
            documentRegister.DocumentFlow = range.Cell(1,4).Value.ToString()?.Trim();
            documentRegister.NumberOfDigitsInItem = range.Cell(1,5).Value.ToString()?.Trim();
            documentRegister.NumberedSection = range.Cell(1,6).Value.ToString()?.Trim();
            documentRegister.NumberingPeriod = range.Cell(1,7).Value.ToString()?.Trim();
            documentRegister.RegistrationGroup = range.Cell(1,8).Value.ToString()?.Trim();
            documentRegister.Error = range.Cell(1,9).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            documentRegister.Error = ex.Message;
          }
          currentRow++;
          documentRegisters.Add(documentRegister);
        }
      }
      return documentRegisters;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке журналова регистрации".
    /// </summary>
    /// <param name="documentRegisters">Список журналов регистрации.</param>
    public void ShowDocumentRegisterLoaderReport(List<Structures.Module.DocumentRegister> documentRegisters)
    {
      var report = Reports.GetDocumentRegisterLoaderErrorReport();
      var errorText = string.Join(";", documentRegisters.Select(a => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                                                                                   a.Name, a.RegisterType, a.Index,
                                                                                   a.DocumentFlow, a.NumberOfDigitsInItem,
                                                                                   a.NumberedSection, a.NumberingPeriod,
                                                                                   a.RegistrationGroup, a.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Виды документов
    
    /// <summary>
    /// Загрузить виды документов.
    /// </summary>
    public void LoadDocumentKinds()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadDocumentKinds);
      if (file == null)
        return;
      var documentKinds = GetDocumentKindFromExcel(file);
      documentKinds = Functions.Module.Remote.CreateOrUpdateDocumentKinds(documentKinds);
      var documentKindsWithError = documentKinds.Where(c => !string.IsNullOrEmpty(c.Error));
      if (documentKindsWithError.Any())
        ShowDocumentKindsLoaderReport(documentKindsWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(documentKinds.Count, documentKindsWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника виды документов из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список видов документов.</returns>
    public List<Structures.Module.DocumentKind> GetDocumentKindFromExcel(byte[] file)
    {
      var documentKinds = new List<Structures.Module.DocumentKind>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 5)).IsEmpty())
        {
          var documentKind = Structures.Module.DocumentKind.Create();
          
          try
          {
            documentKind.Name = range.Cell(1,1).Value.ToString()?.Trim();
            documentKind.ShortName = range.Cell(1,2).Value.ToString()?.Trim();
            documentKind.Code = range.Cell(1,3).Value.ToString()?.Trim();
            documentKind.NumerationType = range.Cell(1,4).Value.ToString()?.Trim();
            documentKind.DocumentFlow = range.Cell(1,5).Value.ToString()?.Trim();
            documentKind.DocumentType = range.Cell(1,6).Value.ToString()?.Trim();
            documentKind.DeadlineDays = range.Cell(1,7).Value.ToString()?.Trim();
            documentKind.DeadlineHours = range.Cell(1,8).Value.ToString()?.Trim();
            documentKind.Note = range.Cell(1,9).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            documentKind.Error = ex.Message;
          }
          currentRow++;
          documentKinds.Add(documentKind);
        }
      }
      return documentKinds;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Видов документов".
    /// </summary>
    /// <param name="documentKinds">Список видов документов.</param>
    public void ShowDocumentKindsLoaderReport(List<Structures.Module.DocumentKind> documentKinds)
    {
      var report = Reports.GetDocumentKindsLoaderErrorReport();
      var errorText = string.Join(";", documentKinds.Select(a => string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}",
                                                                               a.Name, a.ShortName, a.Code,
                                                                               a.NumerationType, a.DocumentFlow,
                                                                               a.DocumentType, a.DeadlineDays,
                                                                               a.DeadlineHours, a.Note, a.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Страны
    
    /// <summary>
    /// Загрузить страны.
    /// </summary>
    public void LoadCountries()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadCountries);
      if (file == null)
        return;
      var countries = GetСountriesFromExcel(file);
      countries = Functions.Module.Remote.CreateOrUpdateCountries(countries);
      var countriesWithError = countries.Where(c => !string.IsNullOrEmpty(c.Error));
      if (countriesWithError.Any())
        ShowCountriesLoaderReport(countriesWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(countries.Count, countriesWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника страны из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список стран.</returns>
    public List<Structures.Module.Country> GetСountriesFromExcel(byte[] file)
    {
      var countries = new List<Structures.Module.Country>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 2)).IsEmpty())
        {
          var country = Structures.Module.Country.Create();
          
          try
          {
            country.Name = range.Cell(1,1).Value.ToString()?.Trim();
            country.Code = range.Cell(1,2).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            country.Error = ex.Message;
          }
          currentRow++;
          countries.Add(country);
        }
      }
      return countries;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Стран".
    /// </summary>
    /// <param name="jobTitles">Список стран.</param>
    public void ShowCountriesLoaderReport(List<Structures.Module.Country> countries)
    {
      var report = Reports.GetCountriesLoaderErrorReport();
      var errorText = string.Join(";", countries.Select(a => string.Format("{0}|{1}|{2}", a.Name, a.Code, a.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Валюты
    
    /// <summary>
    /// Загрузить валюты.
    /// </summary>
    public void LoadCurrencies()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadCurrencies);
      if (file == null)
        return;
      var currencies = GetCurrenciesFromExcel(file);
      currencies = Functions.Module.Remote.CreateOrUpdateCurrencies(currencies);
      var currenciesWithError = currencies.Where(c => !string.IsNullOrEmpty(c.Error));
      if (currenciesWithError.Any())
        ShowCurrenciesLoaderReport(currenciesWithError.ToList());
      Resources.EndOfLoadNotifyMessageTextFormat(currencies.Count, currenciesWithError.Count());
    }
    
    /// <summary>
    /// Получить записи справочника валюты из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список валют.</returns>
    public List<Structures.Module.Currency> GetCurrenciesFromExcel(byte[] file)
    {
      var currencies = new List<Structures.Module.Currency>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 5)).IsEmpty())
        {
          var currency = Structures.Module.Currency.Create();
          
          try
          {
            currency.Name = range.Cell(1,1).Value.ToString()?.Trim();
            currency.ShortName = range.Cell(1,2).Value.ToString()?.Trim();
            currency.FractionName = range.Cell(1,3).Value.ToString()?.Trim();
            currency.AlphaCode = range.Cell(1,4).Value.ToString()?.Trim();
            currency.NumericCode = range.Cell(1,5).Value.ToString()?.Trim();
          }
          catch (Exception ex)
          {
            currency.Error = ex.Message;
          }
          currentRow++;
          currencies.Add(currency);
        }
      }
      return currencies;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке Валют".
    /// </summary>
    /// <param name="jobTitles">Список валют.</param>
    public void ShowCurrenciesLoaderReport(List<Structures.Module.Currency> currencies)
    {
      var report = Reports.GetCurrenciesLoaderErrorReport();
      var errorText = string.Join(";", currencies.Select(a => string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                                                                            a.Name, a.ShortName, a.FractionName,
                                                                            a.AlphaCode, a.NumericCode, a.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }
    
    #endregion
    
    #region Загрузка классификатора обращений
    
    /// <summary>
    /// Загрузить разделы классификатора обращений.
    /// </summary>
    public virtual void LoadSectionsClassifier()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadSectionsDialogTitle);
      if (file == null)
        return;
      var classifier = GetBaseClassifierFromExcel(file);
      classifier = Functions.Module.Remote.CreateOrUpdateSectionClassifier(classifier);
      var classifierWithError = classifier.Where(x => !string.IsNullOrEmpty(x.Error));
      if (classifierWithError.Any())
        ShowClassifierLoaderReport(classifierWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(classifier.Count, classifierWithError.Count()));
    }

    /// <summary>
    /// Загрузить тематики классификатора обращений.
    /// </summary>
    public virtual void LoadTopicsClassifier()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadTopicClassifierDialogTitle);
      if (file == null)
        return;
      var classifier = GetBaseClassifierFromExcel(file);
      classifier = Functions.Module.Remote.CreateOrUpdateTopicClassifier(classifier);
      var classifierWithError = classifier.Where(x => !string.IsNullOrEmpty(x.Error));
      if (classifierWithError.Any())
        ShowClassifierLoaderReport(classifierWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(classifier.Count, classifierWithError.Count()));
    }
    
    /// <summary>
    /// Загрузить темы классификатора обращений.
    /// </summary>
    public virtual void LoadThemesClassifier()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadThemeClassifierDialogTitle);
      if (file == null)
        return;
      var classifier = GetBaseClassifierFromExcel(file);
      classifier = Functions.Module.Remote.CreateOrUpdateThemeClassifier(classifier);
      var classifierWithError = classifier.Where(x => !string.IsNullOrEmpty(x.Error));
      if (classifierWithError.Any())
        ShowClassifierLoaderReport(classifierWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(classifier.Count, classifierWithError.Count()));
    }

    /// <summary>
    /// Загрузить вопросы классификатора обращений.
    /// </summary>
    public virtual void LoadQuestionsClassifier()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadQuestionClassifierDialogTitle);
      if (file == null)
        return;
      var classifier =  GetBaseClassifierFromExcel(file);
      classifier = Functions.Module.Remote.CreateOrUpdateQuestionClassifier(classifier);
      var classifierWithError = classifier.Where(x => !string.IsNullOrEmpty(x.Error));
      if (classifierWithError.Any())
        ShowClassifierLoaderReport(classifierWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(classifier.Count, classifierWithError.Count()));
    }
    
    /// <summary>
    /// Загрузить подвопросы классификатора обращений.
    /// </summary>
    public virtual void LoadSubQuestionsClassifier()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadSubquestionClassifierDialogTitle);
      if (file == null)
        return;
      var classifier = GetBaseClassifierFromExcel(file);
      classifier = Functions.Module.Remote.CreateOrUpdateSubQuestionClassifier(classifier);
      var classifierWithError = classifier.Where(x => !string.IsNullOrEmpty(x.Error));
      if (classifierWithError.Any())
        ShowClassifierLoaderReport(classifierWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(classifier.Count, classifierWithError.Count()));
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке классификатора".
    /// </summary>
    /// <param name="classifiers">Список классификаторов обращений.</param>
    private void ShowClassifierLoaderReport(List<Structures.Module.ClassifierBase> classifiers)
    {
      var report = Reports.GetClassifierLoaderErrorReport();
      report.LoaderErrorsStructure = string.Join(";", classifiers.Select(x => string.Format("{0}|{1}|{2}|{3}", x.Code, x.Name, x.FullCode, x.Error)).ToArray());
      report.Open();
    }
    
    #region Парсинг классификатора обращений из Excel
    
    /// <summary>
    /// Получить записей справочника базового классификатора обращений из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список классификаторов обращений.</returns>
    private List<Structures.Module.ClassifierBase> GetBaseClassifierFromExcel(byte[] file)
    {
      List<Structures.Module.ClassifierBase> classifiers = new List<Structures.Module.ClassifierBase>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var classifier = Structures.Module.ClassifierBase.Create();
          try
          {
            classifier.Code = range.Cell(1,1).Value.ToString();
            classifier.Name = range.Cell(1,2).Value.ToString();
            classifier.FullCode = range.Cell(1,3).Value.ToString();
          }
          catch (Exception ex)
          {
            classifier.Error = ex.Message;
          }
          
          classifiers.Add(classifier);
          currentRow++;
        }
      }
      
      return classifiers;
    }
    
    #endregion
    
    #endregion
    
    /// <summary>
    /// Получить Excel файл выбранный пользователем.
    /// </summary>
    /// <param name="inputDialogTitle">Заголовок диалога.</param>
    /// <returns>Файл.</returns>
    private byte[] GetExcelFromFileSelectDialog(string inputDialogTitle)
    {
      var dialog = Dialogs.CreateInputDialog(inputDialogTitle);
      var file = dialog.AddFileSelect(Resources.FileSelectTitle, true).WithFilter(string.Empty, "xlsx");
      
      if (dialog.Show() == DialogButtons.Ok)
      {
        if (System.IO.Path.GetExtension(file.Value.Name).ToLower() != ".xlsx")
        {
          Dialogs.ShowMessage(UploadData.Resources.FileExtensionMustBeXlsx);
          return GetExcelFromFileSelectDialog(inputDialogTitle);
        }
        return file.Value.Content;
      }
      else
        return null;
    }
    
    #region Загрузка номенклатуры и сроков хранения
    
    #region Сроки хранения дел
    
    /// <summary>
    /// Загрузить сроки хранения дел.
    /// </summary>
    public virtual void LoadFileRetentionPeriod()
    {
      var file = GetExcelFromFileSelectDialog(Resources.LoadFileRetentionPeriodDialogTitle);
      if (file == null)
        return;
      var fileRetentionPeriod = GetFileRetentionPeriodFromExcel(file);
      fileRetentionPeriod = Functions.Module.Remote.CreateOrUpdateFileRetentionPeriods(fileRetentionPeriod);
      var fileRetentionPeriodWithError = fileRetentionPeriod.Where(x => !string.IsNullOrEmpty(x.Error));
      if (fileRetentionPeriodWithError.Any())
        ShowFileRetentionPeriodLoaderReport(fileRetentionPeriodWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(fileRetentionPeriod.Count, fileRetentionPeriodWithError.Count()));
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке сроков хранения дел".
    /// </summary>
    /// <param name="fileRetentionPeriods">Список сроков хранения дел.</param>
    private void ShowFileRetentionPeriodLoaderReport(List<Structures.Module.FileRetentionPeriod> fileRetentionPeriods)
    {
      var report = Reports.GetFileRetentionPeriodLoaderErrorReport();
      report.LoaderErrorsStructure = string.Join(";", fileRetentionPeriods.Select(x => string.Format("{0}|{1}|{2}|{3}", x.Name, x.RetentionPeriod, x.Note, x.Error)).ToArray());
      report.Open();
    }
    
    /// <summary>
    /// Получить записей справочника сроки хранения дел из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список сроков хранения</returns>
    private List<Structures.Module.FileRetentionPeriod> GetFileRetentionPeriodFromExcel(byte[] file)
    {
      List<Structures.Module.FileRetentionPeriod> fileRetentionPeriods = new List<Structures.Module.FileRetentionPeriod>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var fileRetentionPeriod = Structures.Module.FileRetentionPeriod.Create();
          try
          {
            fileRetentionPeriod.Name = range.Cell(1,1).Value.ToString();
            fileRetentionPeriod.RetentionPeriod = range.Cell(1,2).Value.ToString();
            fileRetentionPeriod.Note = range.Cell(1,3).Value.ToString();
          }
          catch (Exception ex)
          {
            fileRetentionPeriod.Error = ex.Message;
          }
          
          fileRetentionPeriods.Add(fileRetentionPeriod);
          currentRow++;
        }
      }
      
      return fileRetentionPeriods;
    }
    #endregion
    
    #region Номенклатуры дел

    /// <summary>
    /// Загрузить номенклатуры дел.
    /// </summary>
    public virtual void LoadCaseFile()
    {
      var caseFileInfo = CreateCaseFileSelectDialog(Resources.LoadCaseFileDialogTitle);
      if (caseFileInfo == null)
        return;
      caseFileInfo = GetCaseFileFromExcel(caseFileInfo);
      var caseFile = Functions.Module.Remote.CreateOrUpdateCaseFiles(caseFileInfo);
      var caseFileWithError = caseFile.Where(x => !string.IsNullOrEmpty(x.Error));
      if (caseFileWithError.Any())
        ShowCaseFileLoaderReport(caseFileWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(caseFile.Count, caseFileWithError.Count()));
    }
    
    /// <summary>
    /// Показать отчет "Ошибки  при загрузке номенклатуры дел".
    /// </summary>
    /// <param name="caseFiles">Список сроков хранения дел.</param>
    private void ShowCaseFileLoaderReport(List<Structures.Module.CaseFile> caseFiles)
    {
      var report = Reports.GetCaseFileLoaderErrorReport();
      report.LoaderErrorsStructure = string.Join(";", caseFiles.Select(x => string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                                                                                          x.Department,
                                                                                          x.RetentionPeriod,
                                                                                          x.Title,
                                                                                          x.Index,
                                                                                          x.Note,
                                                                                          x.Error)).ToArray());
      report.Open();
    }
    
    /// <summary>
    /// Получить Excel файл выбранный пользователем для загрузки номенклатуры дел.
    /// </summary>
    /// <param name="inputDialogTitle">Заголовок диалога.</param>
    /// <returns>Информация по списку номенклатур.</returns>
    private Structures.Module.CaseFilesInfo CreateCaseFileSelectDialog(string inputDialogTitle)
    {
      var dialog = Dialogs.CreateInputDialog(inputDialogTitle);
      var startDate = dialog.AddDate(CaseFiles.Info.Properties.StartDate.LocalizedName, true, Calendar.BeginningOfYear(Calendar.Today));
      var endDate = dialog.AddDate(CaseFiles.Info.Properties.EndDate.LocalizedName, true, Calendar.EndOfYear(Calendar.Today));
      var businessUnit = dialog.AddSelect(CaseFiles.Info.Properties.BusinessUnit.LocalizedName, true, Sungero.Company.BusinessUnits.Null);
      var registrationGroup = dialog.AddSelect(CaseFiles.Info.Properties.RegistrationGroup.LocalizedName, true, RegistrationGroups.Null);
      var file = dialog.AddFileSelect(Resources.FileSelectTitle, true).WithFilter(string.Empty, "xlsx");
      
      dialog.SetOnButtonClick((args) =>
                              {
                                Sungero.Docflow.PublicFunctions.Module.CheckReportDialogPeriod(args, startDate, endDate);
                              });
      
      if (dialog.Show() == DialogButtons.Ok)
      {
        if (System.IO.Path.GetExtension(file.Value.Name).ToLower() != ".xlsx")
        {
          Dialogs.ShowMessage(UploadData.Resources.FileExtensionMustBeXlsx);
          return CreateCaseFileSelectDialog(inputDialogTitle);
        }
        
        return Structures.Module.CaseFilesInfo.Create(file.Value.Content,
                                                      startDate.Value.Value,
                                                      endDate.Value.Value,
                                                      businessUnit.Value,
                                                      registrationGroup.Value,
                                                      new List<GD.UploadData.Structures.Module.CaseFile>());
      }
      else
        return null;
    }

    /// <summary>
    /// Получить записи справочника номенклатуры дел из Excel.
    /// </summary>
    /// <param name="caseFilesInfo">Информация по списку номенклатур.</param>
    /// <returns>Информация по списку номенклатур.</returns>
    private Structures.Module.CaseFilesInfo GetCaseFileFromExcel(Structures.Module.CaseFilesInfo caseFilesInfo)
    {
      using (var memory = new System.IO.MemoryStream(caseFilesInfo.File))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var caseFile = Structures.Module.CaseFile.Create();
          try
          {
            caseFile.Department = range.Cell(1,1).Value.ToString();
            caseFile.RetentionPeriod = range.Cell(1,2).Value.ToString();
            caseFile.Index = range.Cell(1,3).Value.ToString();
            caseFile.Title = range.Cell(1,4).Value.ToString();
            caseFile.Note = range.Cell(1,5).Value.ToString();
          }
          catch (Exception ex)
          {
            caseFile.Error = ex.Message;
          }
          
          caseFilesInfo.CaseFiles.Add(caseFile);
          currentRow++;
        }
      }
      
      return caseFilesInfo;
    }

    #endregion
    
    #endregion
    
    #region ФИАС
    
    #region Населенные пункты с ФИАС
    
    /// <summary>
    /// Загрузить населенные пункты из файла формата ФИАС.
    /// </summary>
    public virtual void LoadCities()
    {
      var dialog = Dialogs.CreateInputDialog(GD.UploadData.Resources.LoadCitiesDialogTitle);
      var file = dialog.AddFileSelect(Resources.FileSelectTitle, true).WithFilter(string.Empty, "xlsx");
      var country = dialog.AddSelect(GD.UploadData.Resources.Country, true, Sungero.Commons.Countries.Null);
      var region = dialog.AddSelect(GD.UploadData.Resources.Region, true, Sungero.Commons.Regions.Null);
      
      if (dialog.Show() != DialogButtons.Ok)
        return;

      if (System.IO.Path.GetExtension(file.Value.Name).ToLower() != ".xlsx")
      {
        Dialogs.ShowMessage(UploadData.Resources.FileExtensionMustBeXlsx);
        return;
      }
      
      var cities = GetCitiesFromExcel(file.Value.Content, country.Value, region.Value);
      cities = Functions.Module.Remote.CreateOrUpdateCities(cities);
      var citiesWithError = cities.Where(x => !string.IsNullOrEmpty(x.Error));
      if (citiesWithError.Any())
        ShowCitiesLoaderReport(citiesWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(cities.Count, citiesWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника Населенные пункты из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список населенных пунктов.</returns>
    private List<Structures.Module.City> GetCitiesFromExcel(byte[] file, Sungero.Commons.ICountry country, Sungero.Commons.IRegion region)
    {
      var cities = new List<Structures.Module.City>();
      using (var memory = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(memory);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var city = Structures.Module.City.Create();
          try
          {
            city.Country = country;
            city.Region = region;
            city.ObjectId = range.Cell(1,2).Value.ToString().ToLower();
            city.ObjectGUID = range.Cell(1,3).Value.ToString().ToLower();
            city.Name = range.Cell(1,5).Value.ToString();
            city.TypeName = range.Cell(1,6).Value.ToString();
            city.Level = range.Cell(1,7).Value.ToString();
            city.IsActual = range.Cell(1,14).Value.ToString();
            city.IsActive = range.Cell(1,15).Value.ToString();
          }
          catch (Exception ex)
          {
            city.Error = ex.Message;
          }
          
          if (city.IsActive == "1" && (city.Level == "5" || city.Level == "6"))
            cities.Add(city);
          currentRow++;
        }
      }
      
      return cities;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки при загрузке населенных пунктов".
    /// </summary>
    /// <param name="companies">Список населенных пунктов.</param>
    private void ShowCitiesLoaderReport(List<Structures.Module.City> cities)
    {
      var report = Reports.GetCitiesLoaderErrorReport();
      var errorText = string.Join(";", cities.Select(x => string.Format("{0}|{1}|{2}|{3}",
                                                                        x.Name,
                                                                        x.ObjectGUID,
                                                                        x.TypeName,
                                                                        x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }

    #endregion

    #region Муниципальные районы с ФИАС
    
    /// <summary>
    /// Загрузить Муниципальные районы из файла.
    /// </summary>
    public virtual void LoadMunicipalAreas()
    {
      var dialog = Dialogs.CreateInputDialog(Resources.LoadMunicipalAreas);
      var file = dialog.AddFileSelect(Resources.FileSelectTitle, true).WithFilter(string.Empty, "xlsx");
      var country = dialog.AddSelect(Resources.Country, true, Sungero.Commons.Countries.Null);
      var region = dialog.AddSelect(Resources.Region, true, Sungero.Commons.Regions.Null);
      
      if (dialog.Show() != DialogButtons.Ok)
        return;

      if (System.IO.Path.GetExtension(file.Value.Name).ToLower() != ".xlsx")
      {
        Dialogs.ShowMessage(Resources.FileExtensionMustBeXlsx);
        return;
      }
      
      var municipalAreas = GetMunicipalAreasFromExcel(file.Value.Content, country.Value, region.Value);
      municipalAreas = Functions.Module.Remote.CreateOrUpdateMunicipalAreas(municipalAreas);
      var municipalAreasWithError = municipalAreas.Where(x => !string.IsNullOrEmpty(x.Error));
      if (municipalAreasWithError.Any())
        ShowMunicipalAreasLoaderReport(municipalAreasWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(municipalAreas.Count, municipalAreasWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника Муниципальные районы из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список населенных пунктов.</returns>
    private List<Structures.Module.MunicipalArea> GetMunicipalAreasFromExcel(byte[] file, Sungero.Commons.ICountry country, Sungero.Commons.IRegion region)
    {
      var municipalAreas = new List<Structures.Module.MunicipalArea>();
      using (var fileStream = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(fileStream);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var area = Structures.Module.MunicipalArea.Create();
          try
          {
            area.Country = country;
            area.Region = region;
            area.ObjectId = range.Cell(1,2).Value.ToString().ToLower();
            area.ObjectGUID = range.Cell(1,3).Value.ToString().ToLower();
            area.Name = range.Cell(1,5).Value.ToString();
            area.TypeName = range.Cell(1,6).Value.ToString();
            area.Level = range.Cell(1,7).Value.ToString();
            area.IsActual = range.Cell(1,14).Value.ToString();
            area.IsActive = range.Cell(1,15).Value.ToString();
          }
          catch (Exception ex)
          {
            area.Error = ex.Message;
          }
          
          if (area.IsActive == "1" && area.Level == "3")
            municipalAreas.Add(area);
          currentRow++;
        }
      }
      
      return municipalAreas;
    }
    

    /// <summary>
    /// Показать отчет "Ошибки при загрузке муниципальных районов".
    /// </summary>
    /// <param name="municipalAreas">Список муниципальных районов.</param>
    private void ShowMunicipalAreasLoaderReport(List<Structures.Module.MunicipalArea> municipalAreas)
    {
      var report = Reports.GetMunicipalAreasLoaderErrorReport();
      var errorText = string.Join(";", municipalAreas.Select(x => string.Format("{0}|{1}|{2}|{3}",
                                                                                x.Name,
                                                                                x.ObjectGUID,
                                                                                x.TypeName,
                                                                                x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }

    #endregion
    
    #region Поселения с ФИАС
    
    /// <summary>
    /// Загрузить Поселения из файла.
    /// </summary>
    public virtual void LoadSettlements()
    {
      var dialog = Dialogs.CreateInputDialog(GD.UploadData.Resources.LoadSettlements);
      var file = dialog.AddFileSelect(Resources.FileSelectTitle, true).WithFilter(string.Empty, "xlsx");
      var country = dialog.AddSelect(Resources.Country, true, Sungero.Commons.Countries.Null);
      var region = dialog.AddSelect(Resources.Region, true, Sungero.Commons.Regions.Null);
      
      if (dialog.Show() != DialogButtons.Ok)
        return;

      if (System.IO.Path.GetExtension(file.Value.Name).ToLower() != ".xlsx")
      {
        Dialogs.ShowMessage(Resources.FileExtensionMustBeXlsx);
        return;
      }
      
      var settlements = GetSettlementsFromExcel(file.Value.Content, country.Value, region.Value);
      settlements = Functions.Module.Remote.CreateOrUpdateSettlements(settlements);
      var settlementsWithError = settlements.Where(x => !string.IsNullOrEmpty(x.Error));
      if (settlementsWithError.Any())
        ShowSettlementsLoaderReport(settlementsWithError.ToList());
      Dialogs.NotifyMessage(Resources.EndOfLoadNotifyMessageTextFormat(settlements.Count, settlementsWithError.Count()));
    }
    
    /// <summary>
    /// Получить записи справочника Поселения из Excel.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <returns>Список поселений.</returns>
    private List<Structures.Module.Settlement> GetSettlementsFromExcel(byte[] file, Sungero.Commons.ICountry country, Sungero.Commons.IRegion region)
    {
      var settlements = new List<Structures.Module.Settlement>();
      using (var fileStream = new System.IO.MemoryStream(file))
      {
        var workbook = new XLWorkbook(fileStream);
        var worksheet = workbook.Worksheet(1);
        
        IXLRange range;
        var currentRow = 2;
        while(!(range = worksheet.Range(currentRow, 1, currentRow, 3)).IsEmpty())
        {
          var settlement = Structures.Module.Settlement.Create();
          try
          {
            settlement.Country = country;
            settlement.Region = region;
            settlement.ObjectId = range.Cell(1,2).Value.ToString().ToLower();
            settlement.ObjectGUID = range.Cell(1,3).Value.ToString().ToLower();
            settlement.Name = range.Cell(1,5).Value.ToString();
            settlement.TypeName = range.Cell(1,6).Value.ToString();
            settlement.Level = range.Cell(1,7).Value.ToString();
            settlement.IsActual = range.Cell(1,14).Value.ToString();
            settlement.IsActive = range.Cell(1,15).Value.ToString();
          }
          catch (Exception ex)
          {
            settlement.Error = ex.Message;
          }
          
          if (settlement.IsActive == "1" && settlement.Level == "4")
            settlements.Add(settlement);
          currentRow++;
        }
      }
      
      return settlements;
    }
    
    /// <summary>
    /// Показать отчет "Ошибки при загрузке поселений".
    /// </summary>
    /// <param name="settlements">Список поселений.</param>
    private void ShowSettlementsLoaderReport(List<Structures.Module.Settlement> settlements)
    {
      var report = Reports.GetSettlementsLoaderErrorReport();
      var errorText = string.Join(";", settlements.Select(x => string.Format("{0}|{1}|{2}|{3}",
                                                                             x.Name,
                                                                             x.ObjectGUID,
                                                                             x.TypeName,
                                                                             x.Error)).ToArray());
      report.LoaderErrorsStructure = errorText;
      report.Open();
    }

    #endregion
    
    #endregion
  }
}