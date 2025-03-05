using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.CitizenRequests;
using Sungero.Commons;
using Sungero.Company;
using Sungero.Parties;
using Sungero.Docflow;

namespace GD.UploadData.Server
{
  public class ModuleFunctions
  {
    #region Разделы классификатора обращений
    
    /// <summary>
    /// Создать или обновить записи справочника Разделы классификатора обращений.
    /// </summary>
    /// <param name="classifiers">Список классификаторов обращений.</param>
    [Remote]
    public List<Structures.Module.ClassifierBase> CreateOrUpdateSectionClassifier(List<Structures.Module.ClassifierBase> classifiers)
    {
      foreach (var classifier in classifiers.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var section = GetSectionRecord(classifier.Code);
          if (section == null)
            section = Sections.Create();
          section.Code = classifier.Code;
          section.Name = classifier.Name;
          section.Save();
        }
        catch (Exception ex)
        {
          classifier.Error = ex.Message;
        }
      }
      return classifiers;
    }
    
    /// <summary>
    /// Получить запись справочника Разделы классификатора обращений по коду.
    /// </summary>
    /// <param name="code">Код.</param>
    /// <returns>Запись справочника Разделы классификатора обращений.</returns>
    public ISection GetSectionRecord(string code)
    {
      if (string.IsNullOrEmpty(code))
        return null;
      return Sections.GetAll(x => x.Code == code && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Тематики классификатора обращений
    
    /// <summary>
    /// Создать или обновить записи справочника Тематики классификатора обращений.
    /// </summary>
    /// <param name="classifiers">Список классификаторов обращений.</param>
    [Remote]
    public List<Structures.Module.ClassifierBase> CreateOrUpdateTopicClassifier(List<Structures.Module.ClassifierBase> classifiers)
    {
      foreach (var classifier in classifiers.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var topic = GetTopicRecord(classifier.Code);
          if (topic == null)
            topic = GD.CitizenRequests.Topics.Create();
          topic.Code = classifier.Code;
          topic.Name = classifier.Name;
          topic.Save();
        }
        catch (Exception ex)
        {
          classifier.Error = ex.Message;
        }
      }
      return classifiers;
    }

    /// <summary>
    /// Получить запись справочника Тематики классификатора обращений по коду.
    /// </summary>
    /// <param name="code">Код.</param>
    /// <returns>Запись справочника Тематики классификатора обращений.</returns>
    public GD.CitizenRequests.ITopic GetTopicRecord(string code)
    {
      if (string.IsNullOrEmpty(code))
        return null;
      return GD.CitizenRequests.Topics.GetAll(x => x.Code == code && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Темы классификатора обращений
    
    /// <summary>
    /// Создать или обновить записи справочника Темы классификатора обращений.
    /// </summary>
    /// <param name="classifiers">Список классификаторов обращений.</param>
    [Remote]
    public List<Structures.Module.ClassifierBase> CreateOrUpdateThemeClassifier(List<Structures.Module.ClassifierBase> classifiers)
    {
      foreach (var classifier in classifiers.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var theme = GetThemeRecord(classifier.Code);
          if (theme == null)
            theme = Themes.Create();
          theme.Code = classifier.Code;
          theme.Name = classifier.Name;
          theme.Save();
        }
        catch (Exception ex)
        {
          classifier.Error = ex.Message;
        }
      }
      return classifiers;
    }

    /// <summary>
    /// Получить запись справочника Темы классификатора обращений по коду.
    /// </summary>
    /// <param name="code">Код.</param>
    /// <returns>Запись справочника Темы классификатора обращений.</returns>
    public ITheme GetThemeRecord(string code)
    {
      if (string.IsNullOrEmpty(code))
        return null;
      return Themes.GetAll(x => x.Code == code && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Вопросы классификатора обращений
    
    /// <summary>
    /// Создать или обновить записи справочника Вопросы классификатора обращений.
    /// </summary>
    /// <param name="classifiers">Список классификаторов обращений.</param>
    [Remote]
    public List<Structures.Module.ClassifierBase> CreateOrUpdateQuestionClassifier(List<Structures.Module.ClassifierBase> classifiers)
    {
      foreach (var classifier in classifiers.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var codes = classifier.FullCode.Split('.');
          var question = GetQuestionRecord(classifier.Code);
          if (question == null)
            question = Questions.Create();
          question.Code = classifier.Code;
          question.Name = classifier.Name;
          if (!string.IsNullOrEmpty(codes[0]))
          {
            question.Section = GetSectionRecord(codes[0]);
            if (question.Section == null)
              throw AppliedCodeException.Create(Resources.NotFoundSectionExceptionTextFormat(codes[0]));
          }
          if (!string.IsNullOrEmpty(codes[2]))
          {
            question.Topic = GetTopicRecord(codes[2]);
            if (question.Topic == null)
              throw AppliedCodeException.Create(Resources.NotFoundTopicExceptionTextFormat(codes[2]));
          }
          if (!string.IsNullOrEmpty(codes[1]))
          {
            question.Theme = GetThemeRecord(codes[1]);
            if (question.Theme == null)
              throw AppliedCodeException.Create(Resources.NotFoundThemeExceptionTextFormat(codes[1]));
          }
          question.Save();
        }
        catch (Exception ex)
        {
          classifier.Error = ex.Message;
        }
      }
      return classifiers;
    }

    /// <summary>
    /// Получить запись справочника Вопросы классификатора обращений по коду.
    /// </summary>
    /// <param name="code">Код.</param>
    /// <returns>Запись справочника Вопросы классификатора обращений.</returns>
    public IQuestion GetQuestionRecord(string code)
    {
      if (string.IsNullOrEmpty(code))
        return null;
      
      return Questions.GetAll(x => x.Code == code && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Подвопросы классификатора обращений
    
    /// <summary>
    /// Создать или обновить записи справочника Подвопросы классификатора обращений.
    /// </summary>
    /// <param name="classifiers">Список классификаторов обращений.</param>
    [Remote]
    public List<Structures.Module.ClassifierBase> CreateOrUpdateSubQuestionClassifier(List<Structures.Module.ClassifierBase> classifiers)
    {
      foreach (var classifier in classifiers.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var codes = classifier.FullCode.Split('.');
          var question = GetSubQuestionRecord(classifier);
          question.Code = classifier.Code;
          question.Name = classifier.Name;
          if (!string.IsNullOrEmpty(codes[3]))
          {
            question.Question = GetQuestionRecord(codes[3]);
            if (question.Question == null)
              throw AppliedCodeException.Create(Resources.NotFoundQuestionExceptionTextFormat(codes[3]));
          }
          question.Save();
        }
        catch (Exception ex)
        {
          classifier.Error = ex.Message;
        }
      }
      return classifiers;
    }
    
    /// <summary>
    /// Получить запись справочника Подвопросы классификатора обращений.
    /// </summary>
    /// <param name="classifier">Классификатор обращений.</param>
    /// <returns>Запись справочника Подвопросы классификатора обращений.</returns>
    public ISubQuestion GetSubQuestionRecord(Structures.Module.ClassifierBase classifier)
    {
      var subquestion = SubQuestions.GetAll(x => x.Code == classifier.Code &&
                                            x.Question.Code == classifier.FullCode.Split('.')[3] &&
                                            x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
      if (subquestion == null)
        subquestion = SubQuestions.Create();
      return subquestion;
    }
    
    #endregion

    #region Персоны
    
    /// <summary>
    /// Создать или обновить записи справочника Персоны.
    /// </summary>
    /// <param name="persons">Список персон.</param>
    [Remote]
    public List<Structures.Module.Person> CreateOrUpdatePersons(List<Structures.Module.Person> persons)
    {
      foreach (var person in persons.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = People.Create();
          record.LastName = person.LastName;
          record.FirstName = person.FirstName;
          record.MiddleName = person.MiddleName;
          record.Sex = GetSexValue(person.Sex);
          if (!string.IsNullOrEmpty(person.DateOfBirth))
            record.DateOfBirth = DateTime.Parse(person.DateOfBirth);
          record.TIN = person.TIN;
          record.INILA = person.INILA;
          if (!string.IsNullOrEmpty(person.City))
          {
            record.City = GetCityRecord(person.City);
            if (record.City == null)
              throw AppliedCodeException.Create(Resources.NotFoundCityExceptionTextFormat(person.City));
          }
          if (!string.IsNullOrEmpty(person.Region))
          {
            record.Region = GetRegionRecord(person.Region);
            if (record.Region == null)
              throw AppliedCodeException.Create(Resources.NotFoundRegionExceptionTextFormat(person.Region));
          }
          record.LegalAddress = person.LegalAddress;
          record.PostalAddress = person.PostalAddress;
          record.Phones = person.Phones;
          record.Email = person.Email;
          record.Homepage = person.Homepage;
          record.Note = person.Note;
          record.Account = person.Account;
          if (!string.IsNullOrEmpty(person.Bank))
          {
            record.Bank = GetBankRecord(person.Bank);
            if (record.Bank == null)
              throw AppliedCodeException.Create(Resources.NotFoundBankExceptionTextFormat(person.Bank));
          }
          record.Save();
        }
        catch (Exception ex)
        {
          person.Error = ex.Message;
        }
      }
      return persons;
    }
    
    /// <summary>
    /// Получить запись справочника Персоны.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Персоны.</returns>
    public IPerson GetPersonRecord(string name)
    {
      var person = People.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active);
      if (person.Count() > 1)
        throw AppliedCodeException.Create(Resources.DuplicatePersonsTitleExceptionTextFormat(name));
      return person.FirstOrDefault();
    }
    
    /// <summary>
    /// Получить значение Пол.
    /// </summary>
    /// <param name="nonresident">Строка со значением "Женский" или "Мужской".</param>
    /// <returns>Логическое значение.</returns>
    public Enumeration? GetSexValue(string sex)
    {
      if (string.IsNullOrEmpty(sex))
        return null;
      if (sex.Trim().ToUpper() == "ЖЕНСКИЙ")
        return Sungero.Parties.Person.Sex.Female;
      if (sex.Trim().ToUpper() == "МУЖСКОЙ")
        return Sungero.Parties.Person.Sex.Male;
      
      return null;
    }
    
    #endregion
    
    #region Должности
    
    /// <summary>
    /// Создать или обновить записи справочника Должности.
    /// </summary>
    /// <param name="jobTitles">Список должностей.</param>
    [Remote]
    public List<Structures.Module.JobTitle> CreateOrUpdateJobTitles(List<Structures.Module.JobTitle> jobTitles)
    {
      foreach (var jobTitle in jobTitles.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetJobTitleRecord(jobTitle.Name, jobTitle.Department);
          if (record == null)
            record = JobTitles.Create();
          record.Name = jobTitle.Name;
          record.Department = GetDepartmentRecord(jobTitle.Department);
          record.Save();
        }
        catch (Exception ex)
        {
          jobTitle.Error = ex.Message;
        }
      }
      return jobTitles;
    }
    
    /// <summary>
    /// Получить запись справочника Должности.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <param name="department">Подразделение.</param>
    /// <returns>Запись справочника Должности.</returns>
    public IJobTitle GetJobTitleRecord(string name, string department)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      var jobTitles = JobTitles.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active);
      IJobTitle jobTitle = null;
      if (!string.IsNullOrEmpty(department))
        jobTitle = jobTitles.Where(x => x.Department.Name == department).FirstOrDefault();
      if (jobTitle == null)
        jobTitle = jobTitles.FirstOrDefault();
      
      return jobTitle;
    }
    
    #endregion
    
    #region Организации
    
    /// <summary>
    /// Создать или обновить записи справочника Организации.
    /// </summary>
    /// <param name="companys">Список организаций.</param>
    [Remote]
    public List<Structures.Module.Company> CreateOrUpdateCompanies(List<Structures.Module.Company> companies)
    {
      foreach (var company in companies.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetCompanyRecord(company);
          if (record == null)
            record = Companies.Create();
          record.Name = company.Name;
          record.LegalName = company.LegalName;
          if (!string.IsNullOrEmpty(company.HeadCompany))
          {
            record.HeadCompany = GetCompanyRecord(company.HeadCompany);
            if (record.HeadCompany == null)
              throw AppliedCodeException.Create(Resources.NotFoundCompanyExceptionTextFormat(company.HeadCompany));
          }
          record.Nonresident = GetNonresidentValue(company.Nonresident);
          record.TIN = company.TIN;
          record.TRRC = company.TRRC;
          record.PSRN = company.PSRN;
          record.NCEO = company.NCEO;
          record.NCEA = company.NCEA;
          if (!string.IsNullOrEmpty(company.Region))
          {
            record.Region = GetRegionRecord(company.Region);
            if (record.Region == null)
              throw AppliedCodeException.Create(Resources.NotFoundRegionExceptionTextFormat(company.Region));
          }
          if (!string.IsNullOrEmpty(company.City))
          {
            record.City = GetCityRecord(company.City);
            if (record.City == null)
              throw AppliedCodeException.Create(Resources.NotFoundCityExceptionTextFormat(company.City));
          }
          record.LegalAddress = company.LegalAddress;
          record.PostalAddress = company.PostalAddress;
          record.Phones = company.Phones;
          record.Email = company.Email;
          record.Homepage = company.Homepage;
          record.Note = company.Note;
          record.Account = company.Account;
          if (!string.IsNullOrEmpty(company.Bank))
          {
            record.Bank = GetBankRecord(company.Bank);
            if (record.Bank == null)
              throw AppliedCodeException.Create(Resources.NotFoundBankExceptionTextFormat(company.Bank));
          }
          record.Save();
        }
        catch (Exception ex)
        {
          company.Error = ex.Message;
        }
      }
      return companies;
    }

    /// <summary>
    /// Получить запись справочника Организации.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Организации.</returns>
    public ICompany GetCompanyRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      return Companies.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Организации.
    /// </summary>
    /// <param name="company">Организация.</param>
    /// <returns>Запись справочника Организации.</returns>
    public ICompany GetCompanyRecord(Structures.Module.Company company)
    {
      if (company == null)
        return null;
      return Companies.GetAll(x => x.Name == company.Name &&
                              x.TIN == company.TIN &&
                              x.PSRN == company.PSRN &&
                              x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Регионы.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Регионы.</returns>
    public IRegion GetRegionRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      return Regions.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Населенный пункт.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Населенный пункт.</returns>
    public ICity GetCityRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      return Cities.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Банки.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Банки.</returns>
    public IBank GetBankRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      return Banks.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить значение Не резидент.
    /// </summary>
    /// <param name="nonresident">Строка со значением "Да" или "Нет".</param>
    /// <returns>Логическое значение.</returns>
    public bool? GetNonresidentValue(string nonresident)
    {
      if (string.IsNullOrEmpty(nonresident))
        return null;
      
      if (nonresident.Trim().ToUpper() == "ДА")
        return true;
      if (nonresident.Trim().ToUpper() == "НЕТ")
        return false;
      
      return null;
    }
    
    #endregion
    
    #region Наши организации
    
    /// <summary>
    /// Создать или обновить записи справочника Наши организации.
    /// </summary>
    /// <param name="businessUnits">Список наших организаций.</param>
    [Remote]
    public List<Structures.Module.BusinessUnit> CreateOrUpdateBusinessUnits(List<Structures.Module.BusinessUnit> businessUnits)
    {
      foreach (var businessUnit in businessUnits.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetBusinessUnitRecord(businessUnit);
          if (record == null)
            record = BusinessUnits.Create();
          record.Name = businessUnit.Name;
          record.LegalName = businessUnit.LegalName;
          if (!string.IsNullOrEmpty(businessUnit.HeadCompany))
          {
            record.HeadCompany = GetBusinessUnitRecord(businessUnit.HeadCompany);
            if (record.HeadCompany == null)
              throw AppliedCodeException.Create(Resources.NotFoundBusinessUnitExceptionTextFormat(businessUnit.HeadCompany));
          }
          if (!string.IsNullOrEmpty(businessUnit.CEO))
          {
            record.CEO = GetEmployeeRecord(businessUnit.CEO);
            if (record.CEO == null)
              throw AppliedCodeException.Create(Resources.NotFoundEmployeeExceptionTextFormat(businessUnit.CEO));
          }
          record.TIN = businessUnit.TIN;
          record.TRRC = businessUnit.TRRC;
          record.PSRN = businessUnit.PSRN;
          record.NCEO = businessUnit.NCEO;
          record.NCEA = businessUnit.NCEA;
          if (!string.IsNullOrEmpty(businessUnit.Region))
          {
            record.Region = GetRegionRecord(businessUnit.Region);
            if (record.Region == null)
              throw AppliedCodeException.Create(Resources.NotFoundRegionExceptionTextFormat(businessUnit.Region));
          }
          if (!string.IsNullOrEmpty(businessUnit.City))
          {
            record.City = GetCityRecord(businessUnit.City);
            if (record.City == null)
              throw AppliedCodeException.Create(Resources.NotFoundCityExceptionTextFormat(businessUnit.City));
          }
          record.LegalAddress = businessUnit.LegalAddress;
          record.PostalAddress = businessUnit.PostalAddress;
          record.Phones = businessUnit.Phones;
          record.Email = businessUnit.Email;
          record.Homepage = businessUnit.Homepage;
          record.Note = businessUnit.Note;
          record.Account = businessUnit.Account;
          if (!string.IsNullOrEmpty(businessUnit.Bank))
          {
            record.Bank = GetBankRecord(businessUnit.Bank);
            if (record.Bank == null)
              throw AppliedCodeException.Create(Resources.NotFoundBankExceptionTextFormat(businessUnit.Bank));
          }
          record.Save();
        }
        catch (Exception ex)
        {
          businessUnit.Error = ex.Message;
        }
      }
      return businessUnits;
    }

    /// <summary>
    /// Получить запись справочника Наши организации.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Наши организации.</returns>
    public IBusinessUnit GetBusinessUnitRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      return BusinessUnits.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Наши организации.
    /// </summary>
    /// <param name="businessUnit">Наша организация.</param>
    /// <returns>Запись справочника Наши организации.</returns>
    public IBusinessUnit GetBusinessUnitRecord(Structures.Module.BusinessUnit businessUnit)
    {
      if (businessUnit == null)
        return null;
      return BusinessUnits.GetAll(x => x.Name == businessUnit.Name &&
                                  x.TIN == businessUnit.TIN &&
                                  x.PSRN == businessUnit.PSRN &&
                                  x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }

    #endregion
    
    #region Подразделения
    
    /// <summary>
    /// Создать или обновить записи справочника Подразделения.
    /// </summary>
    /// <param name="departments">Список подразделений.</param>
    [Remote]
    public List<Structures.Module.Department> CreateOrUpdateDepartments(List<Structures.Module.Department> departments)
    {
      foreach (var department in departments.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetDepartmentRecord(department);
          if (record == null)
            record = Departments.Create();
          record.Name = department.Name;
          record.ShortName = department.ShortName;
          if (!string.IsNullOrEmpty(department.BusinessUnit))
          {
            record.BusinessUnit = GetBusinessUnitRecord(department.BusinessUnit);
            if (record.BusinessUnit == null)
              throw AppliedCodeException.Create(Resources.NotFoundBusinessUnitExceptionTextFormat(department.BusinessUnit));
          }
          if (!string.IsNullOrEmpty(department.HeadOffice) && !string.IsNullOrEmpty(department.BusinessUnit))
          {
            record.HeadOffice = GetDepartmentRecord(department.HeadOffice, department.BusinessUnit);
            if (record.HeadOffice == null)
              throw AppliedCodeException.Create(Resources.NotFoundDepartmentExceptionTextFormat(department.HeadOffice, department.BusinessUnit));
          }
          record.Code = department.Code;
          if (!string.IsNullOrEmpty(department.Manager))
          {
            record.Manager = GetEmployeeRecord(department.Manager);
            if (record.Manager == null)
              throw AppliedCodeException.Create(Resources.NotFoundEmployeeExceptionTextFormat(department.Manager));
          }
          record.Phone = department.Phone;
          record.Note = department.Description;
          record.Save();
        }
        catch (Exception ex)
        {
          department.Error = ex.Message;
        }
      }
      return departments;
    }
    
    /// <summary>
    /// Получить запись справочника Подразделения.
    /// </summary>
    /// <param name="department">Подразделение.</param>
    /// <returns>Запись справочника Подразделения.</returns>
    public IDepartment GetDepartmentRecord(Structures.Module.Department department)
    {
      if (department == null)
        return null;
      return Departments.GetAll(x => x.Name == department.Name &&
                                x.BusinessUnit != null &&
                                x.BusinessUnit.Name == department.BusinessUnit &&
                                x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Подразделения.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Подразделения.</returns>
    public IDepartment GetDepartmentRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      return Departments.GetAll(x => x.Name == name &&
                                x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Подразделения.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <param name="businessUnit">Наша организация.</param>
    /// <returns>Запись справочника Подразделения.</returns>
    public IDepartment GetDepartmentRecord(string name, string businessUnit)
    {
      if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(businessUnit))
        return null;
      return Departments.GetAll(x => x.Name == name &&
                                x.BusinessUnit != null &&
                                x.BusinessUnit.Name == businessUnit &&
                                x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    #endregion
    
    #region Сотрудники
    
    /// <summary>
    /// Создать или обновить записи справочника Сотрудники.
    /// </summary>
    /// <param name="employees">Список подразделений.</param>
    [Remote]
    public List<Structures.Module.Employee> CreateOrUpdateEmployees(List<Structures.Module.Employee> employees)
    {
      foreach (var employee in employees.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetEmployeeRecord(employee);
          if (record == null)
            record = Employees.Create();
          record.Person = GetPersonRecord(employee.Person);
          if (!string.IsNullOrEmpty(employee.Login))
          {
            record.Login = GetLoginRecord(employee.Login);
            if (record.Login == null)
              throw AppliedCodeException.Create(Resources.NotFoundLoginExceptionTextFormat(employee.Login));
          }
          if (!string.IsNullOrEmpty(employee.Department) && !string.IsNullOrEmpty(employee.BusinessUnit))
          {
            record.Department = GetDepartmentRecord(employee.Department, employee.BusinessUnit);
            if (record.Department == null)
              throw AppliedCodeException.Create(Resources.NotFoundDepartmentExceptionTextFormat(employee.Department, employee.BusinessUnit));
          }

          if (!string.IsNullOrEmpty(employee.JobTitle))
          {
            record.JobTitle = GetJobTitleRecord(employee.JobTitle, employee.Department);
            if (record.JobTitle == null)
              throw AppliedCodeException.Create(Resources.NotFoundJobTitleExceptionTextFormat(employee.JobTitle, employee.Department));
          }
          record.PersonnelNumber = employee.PersonnelNumber;
          record.Phone = employee.Phone;
          if (!string.IsNullOrEmpty(employee.Email))
          {
            if (IsValidEmail(employee.Email))
              record.Email = employee.Email;
            else
              throw AppliedCodeException.Create(Sungero.Parties.Resources.WrongEmailFormat);
          }
          else
          {
            record.NeedNotifyExpiredAssignments = false;
            record.NeedNotifyNewAssignments = false;
          }
          record.Description = employee.Description;
          record.Save();
        }
        catch (Exception ex)
        {
          employee.Error = ex.Message;
        }
      }
      return employees;
    }
    
    /// <summary>
    /// Получить запись справочника Сотрудники.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    /// <returns>Запись справочника Сотрудники.</returns>
    public IEmployee GetEmployeeRecord(Structures.Module.Employee employee)
    {
      if (employee == null)
        return null;
      return Employees.GetAll(x => x.Person.Name == employee.Person &&
                              x.Department.Name == employee.Department &&
                              x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Сотрудники.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Сотрудники.</returns>
    public IEmployee GetEmployeeRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      return Employees.GetAll(x => x.Person.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Проверить Email-адрес на валидность.
    /// </summary>
    /// <param name="emailAddress">Email-адрес.</param>
    /// <returns>True - если непустой email является валидным.</returns>
    public static bool IsValidEmail(string emailAddress)
    {
      return string.IsNullOrEmpty(emailAddress) || Sungero.Parties.PublicFunctions.Module.EmailIsValid(emailAddress);
    }
    
    #endregion
    
    #region Учетные записи
    
    /// <summary>
    /// Создать или обновить записи справочника Учетные записи.
    /// </summary>
    /// <param name="logins">Список учетных записей.</param>
    /// <returns>Структура с учетными записями.</returns>
    [Remote]
    public List<Structures.Module.Login> CreateOrUpdateLogins(List<Structures.Module.Login> logins)
    {
      foreach (var login in logins.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetLoginRecord(login.Name);
          if (record == null)
          {
            Sungero.Company.PublicFunctions.Module.CreateLogin(login.Name, Constants.Module.PasswordDefault);
            record = GetLoginRecord(login.Name);
          }
          record.NeedChangePassword = true;
          record.Save();
        }
        catch (Exception ex)
        {
          login.Error = ex.Message;
        }
      }
      return logins;
    }
    
    /// <summary>
    /// Получить запись справочника Учетные записи.
    /// </summary>
    /// <param name="name">Имя.</param>
    /// <returns>Запись справочника Учетные записи.</returns>
    public ILogin GetLoginRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      return Logins.GetAll(x => x.LoginName == name &&
                           x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Контакты
    
    /// <summary>
    /// Создать или обновить записи справочника Контактное лицо.
    /// </summary>
    /// <param name="contacts">Список контактных лиц.</param>
    /// <returns>Список контактных лиц.</returns>
    [Remote]
    public List<Structures.Module.Contact> CreateOrUpdateContacts(List<Structures.Module.Contact> contacts)
    {
      foreach (var contact in contacts.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetContact(contact);
          if (record == null)
            record = Contacts.Create();
          record.Name = contact.FullName.Trim();
          if (!string.IsNullOrEmpty(contact.Company))
            record.Company = GetCompanyRecord(contact.Company);
          record.JobTitle = contact.JobTitle;
          record.Phone = contact.Phone;
          record.Fax = contact.Fax;
          record.Email = contact.Email;
          record.Homepage = contact.Homepage;
          record.Note = contact.Note;
          record.Save();
        }
        catch (Exception ex)
        {
          contact.Error = ex.Message;
        }
      }
      return contacts;
    }
    
    
    /// <summary>
    /// Получить контактное лицо.
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    public IContact GetContact(Structures.Module.Contact contact)
    {
      if (string.IsNullOrEmpty(contact.FullName) || string.IsNullOrEmpty(contact.Company))
        return null;
      
      return Contacts.GetAll(c => c.Name == contact.FullName && c.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Роли
    
    /// <summary>
    /// Создать или обновить записи справочника Роль.
    /// </summary>
    /// <param name="roles">Список ролей.</param>
    /// <returns>Список ролей.</returns>
    [Remote]
    public List<Structures.Module.Role> CreateOrUpdateRoles(List<Structures.Module.Role> roles)
    {
      foreach (var role in roles.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetRole(role);
          if (record == null)
            record = Roles.Create();
          record.Name = role.Name;
          record.Description = role.Note;
          
          foreach (var recipient in GetRecipients(role.Recipients))
          {
            if (record.RecipientLinks.Where(r => r.Member.Equals(recipient)).Any())
              continue;
            
            var employee = record.RecipientLinks.AddNew();
            employee.Member = recipient;
          }
          
          record.IsSingleUser = role.IsSingleUser == Resources.Yes ? true : false;
          record.Save();
        }
        catch (Exception ex)
        {
          role.Error = ex.Message;
        }
      }
      return roles;
    }
    
    /// <summary>
    /// Получить субъект прав доступа.
    /// </summary>
    /// <param name="recipient">Название субъекта прав.</param>
    /// <returns>Запись справочника субъекта прав.</returns>
    public IRecipient GetRecipient(string recipient)
    {
      if (string.IsNullOrEmpty(recipient))
        return null;
      var recipientsall = Recipients.GetAll().ToList();
      var existingRecipient = Recipients.GetAll(r => r.Name == recipient && r.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
      if (existingRecipient == null)
        throw AppliedCodeException.Create(Resources.RecipientNotFoundFormat(recipient));
      
      return existingRecipient;
    }
    
    /// <summary>
    /// Получить запись справочника Роль.
    /// </summary>
    /// <param name="role">Название роли.</param>
    /// <returns>Запись справочника Роль.</returns>
    public IRole GetRole(Structures.Module.Role role)
    {
      if (string.IsNullOrEmpty(role.Name))
        return null;
      
      return Roles.GetAll(r => r.Name == role.Name && r.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Приложения обработчики
    
    /// <summary>
    /// Создать или обновить записи справочника Приложение-обработчик.
    /// </summary>
    /// <param name="applications">Список обработчиков приложений.</param>
    /// <returns>Список обработчиков приложений.</returns>
    [Remote]
    public List<Structures.Module.AssociatedApplication> CreateOrUpdateAssociatedApplications(List<Structures.Module.AssociatedApplication> applications)
    {
      foreach (var application in applications.Where(a => string.IsNullOrEmpty(a.Error)))
      {
        try
        {
          var record = GetApplication(application.Name);
          if (record == null)
            record = Sungero.Content.AssociatedApplications.Create();
          record.Name = application.Name;
          record.Extension = application.Extension;
          record.MonitoringType = GetMonitoringType(application.MonitoringType);
          record.OpenByDefaultForReading = application.OpenByDefaultForReading == "Да";
          record.Save();
        }
        catch (Exception ex)
        {
          application.Error = ex.Message;
        }
      }
      return applications;
    }
    
    /// <summary>
    /// Получить запись справочника Приложение-обработчик.
    /// </summary>
    /// <param name="application">Название записи справочника.</param>
    /// <returns>Запись справочника Приложение-обработчик.</returns>
    public Sungero.Content.IAssociatedApplication GetApplication(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;

      return Sungero.Content.AssociatedApplications.GetAll(a => a.Status == Sungero.CoreEntities.DatabookEntry.Status.Active &&
                                                           a.Name == name).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить локализованное значение типа отслеживания закрытия.
    /// </summary>
    /// <param name="monitoringType">Название типа отслеживания закрытия</param>
    /// <returns>Локализованное значение типа отслеживания закрытия.</returns>
    public Sungero.Core.Enumeration? GetMonitoringType(string monitoringType)
    {
      monitoringType = monitoringType.ToLower();
      var byProcessAndWindow = Sungero.Content.AssociatedApplications.Info.Properties.MonitoringType.
        GetLocalizedValue(Sungero.Content.AssociatedApplication.MonitoringType.ByProcessAndWindow).ToLower();
      var manual = Sungero.Content.AssociatedApplications.Info.Properties.MonitoringType.
        GetLocalizedValue(Sungero.Content.AssociatedApplication.MonitoringType.Manual).ToLower();
      var process = Sungero.Content.AssociatedApplications.Info.Properties.MonitoringType.
        GetLocalizedValue(Sungero.Content.AssociatedApplication.MonitoringType.Process).ToLower();
      
      if (monitoringType == byProcessAndWindow)
        return Sungero.Content.AssociatedApplication.MonitoringType.ByProcessAndWindow;
      if (monitoringType == manual)
        return Sungero.Content.AssociatedApplication.MonitoringType.Manual;
      if (monitoringType == process)
        return Sungero.Content.AssociatedApplication.MonitoringType.Process;
      
      return null;
    }
    
    #endregion
    
    #region Группа регистрации
    
    /// <summary>
    /// Создать или обновить записи справочника Группа регистрации.
    /// </summary>
    /// <param name="registrationGroups">Список групп регистрации.</param>
    /// <returns>Список групп регистрации.</returns>
    [Remote]
    public List<Structures.Module.RegistrationGroup> CreateOrUpdateRegistrationGroup(List<Structures.Module.RegistrationGroup> registrationGroups)
    {
      foreach (var registrationGroup in registrationGroups.Where(r => string.IsNullOrEmpty(r.Error)))
      {
        try
        {
          var record = GetRegistrationGroup(registrationGroup.Name, registrationGroup.ResponsibleEmployee, registrationGroup.Index);
          if (record == null)
            record = RegistrationGroups.Create();
          record.Name = registrationGroup.Name;
          record.Index = registrationGroup.Index;
          var responsibleEmployee = GetEmployeeRecord(registrationGroup.ResponsibleEmployee);
          if (responsibleEmployee == null)
            throw AppliedCodeException.Create(Resources.ResponsibleNotFound);
          record.ResponsibleEmployee = responsibleEmployee;
          SetDocumentFlow(record, registrationGroup.DocumentFlow);
          
          foreach (var recipient in GetRecipients(registrationGroup.RecipientLinks))
          {
            if (record.RecipientLinks.Where(r => r.Member.Equals(recipient)).Any())
              continue;
            var employee = record.RecipientLinks.AddNew();
            employee.Member = recipient;
          }
          
          foreach (var department in GetDepartments(registrationGroup.Departments))
          {
            if (record.Departments.Where(r => r.Department.Equals(department)).Any())
              continue;
            var recordDepartment = record.Departments.AddNew();
            recordDepartment.Department = department;
          }
          
          record.Description = registrationGroup.Description;
          record.Save();
        }
        catch (Exception ex)
        {
          registrationGroup.Error = ex.Message;
        }
      }
      return registrationGroups;
    }
    
    /// <summary>
    /// Установить значение регистрируемых докуметопотоков.
    /// </summary>
    /// <param name="record">Группа регистрации.</param>
    /// <param name="documentFlows">Документопотоки.</param>
    public void SetDocumentFlow(IRegistrationGroup record, string documentFlows)
    {
      var inner = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Inner).ToLower();
      var contracts = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Contracts).ToLower();
      var incoming = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Incoming).ToLower();
      var outgoing = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Outgoing).ToLower();
      
      foreach (var flow in documentFlows.ToLower().Split(';'))
      {
        if (string.IsNullOrEmpty(flow))
          continue;
        
        if (flow.Contains(inner))
          record.CanRegisterInternal = true;
        if (flow.Contains(contracts))
          record.CanRegisterContractual = true;
        if (flow.Contains(incoming))
          record.CanRegisterIncoming = true;
        if (flow.Contains(outgoing))
          record.CanRegisterOutgoing = true;
      }
    }
    
    /// <summary>
    /// Получить запись справочника Группа регистрации.
    /// </summary>
    /// <param name="name">Название группы регистрации.</param>
    /// <param name="responsibleEmployee">Ответственный за группу регистрации.</param>
    /// <param name="index">Индекс группы регистрации.</param>
    /// <returns>Запись справочника группы регистрации.</returns>
    public IRegistrationGroup GetRegistrationGroup(string name, string responsibleEmployee, string index)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      return RegistrationGroups.GetAll(x => x.Name == name && x.ResponsibleEmployee.Equals(GetEmployeeRecord(responsibleEmployee)) &&
                                       x.Index == index && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Группа регистрации.
    /// </summary>
    /// <param name="name">Название группы регистрации.</param>
    /// <returns>Запись справочника группы регистрации.</returns>
    public IRegistrationGroup GetRegistrationGroup(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      return RegistrationGroups.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить список участников группы регистрации.
    /// </summary>
    /// <param name="recipientNames">Название группы.</param>
    /// <returns>Список участников группы регистрации.</returns>
    public List<IRecipient> GetRecipients(string recipientNames)
    {
      var recipients = new List<IRecipient>();
      foreach (var recipient in recipientNames.Split(';'))
      {
        if (string.IsNullOrEmpty(recipient))
          continue;
        
        var rec = GetRecipient(recipient.Trim());
        if (rec == null)
          continue;
        
        recipients.Add(rec);
      }
      
      return recipients;
    }
    
    /// <summary>
    /// Получить список Подразделений.
    /// </summary>
    /// <param name="departmentNames">Название подразделения.</param>
    /// <returns>Список подразделений.</returns>
    public List<IDepartment> GetDepartments(string departmentNames)
    {
      var departments = new List<IDepartment>();
      foreach (var department in departmentNames.Split(';'))
      {
        if (string.IsNullOrEmpty(department))
          continue;
        
        departments.Add(GetDepartmentRecord(department.Trim()));
      }
      return departments;
    }
    
    #endregion
    
    #region  Журналы регистраций
    
    /// <summary>
    /// Создать или обновить записи справочника Журнал регистрации.
    /// </summary>
    /// <param name="documentRegisters">Список журналов регистрации.</param>
    /// <returns>Список журналов регистрации.</returns>
    [Remote]
    public List<Structures.Module.DocumentRegister> CreateorUpdateDocumentRegister(List<Structures.Module.DocumentRegister> documentRegisters)
    {
      foreach (var documentRegister in documentRegisters.Where(r => string.IsNullOrEmpty(r.Error)))
      {
        try
        {
          var record = GetDocumentRegisterRecord(documentRegister);
          if (record == null)
            record = DocumentRegisters.Create();
          record.Name = documentRegister.Name;
          record.RegisterType = GetRegisterType(documentRegister.RegisterType);
          record.Index = documentRegister.Index;
          record.DocumentFlow = GetDocumentFlow(documentRegister.DocumentFlow);
          var numberOfDigitsInNumber = 0;
          if (!int.TryParse(documentRegister.NumberOfDigitsInItem, out numberOfDigitsInNumber))
            throw AppliedCodeException.Create(Resources.IncorrectFormatNumberingDigitsInNumber);
          record.NumberOfDigitsInNumber = numberOfDigitsInNumber;
          record.NumberingSection = GetNumberingSection(documentRegister.NumberedSection);
          record.NumberingPeriod = GetNumberingPeriod(documentRegister.NumberingPeriod);
          if (record.RegisterType.Value.Value != DocumentRegisters.Info.Properties.RegisterType.
              GetLocalizedValue(Sungero.Docflow.DocumentRegister.RegisterType.Numbering))
            record.RegistrationGroup = GetRegistrationGroup(documentRegister.RegistrationGroup);
          record.Save();
        }
        catch (Exception ex)
        {
          documentRegister.Error = ex.Message;
        }
      }
      return documentRegisters;
    }
    
    /// <summary>
    /// Получить Разрез нумерации.
    /// </summary>
    /// <param name="section">Название разреза нумерации.</param>
    /// <returns>Локализованное значение разреза нумераци.</returns>
    public Enumeration? GetNumberingSection(string section)
    {
      section = section.ToLower();
      if (section == DocumentRegisters.Info.Properties.NumberingSection.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingSection.BusinessUnit)
          .ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingSection.BusinessUnit;
      if (section == DocumentRegisters.Info.Properties.NumberingSection.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingSection.Department)
          .ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingSection.Department;
      if (section == DocumentRegisters.Info.Properties.NumberingSection.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingSection.LeadingDocument)
          .ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingSection.LeadingDocument;
      if (section == DocumentRegisters.Info.Properties.NumberingSection.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingSection.NoSection)
          .ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingSection.NoSection;
      
      return null;
    }
    
    /// <summary>
    /// Получить Период нумерации.
    /// </summary>
    /// <param name="period">Название периода нумераци.</param>
    /// <returns>Локализованное значение периода нумераци.</returns>
    public Enumeration? GetNumberingPeriod(string period)
    {
      period = period.ToLower();
      if (period == DocumentRegisters.Info.Properties.NumberingPeriod.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingPeriod.Continuous).ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingPeriod.Continuous;
      if (period == DocumentRegisters.Info.Properties.NumberingPeriod.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingPeriod.Day).ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingPeriod.Day;
      if (period == DocumentRegisters.Info.Properties.NumberingPeriod.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingPeriod.Month).ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingPeriod.Month;
      if (period == DocumentRegisters.Info.Properties.NumberingPeriod.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingPeriod.Quarter).ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingPeriod.Quarter;
      if (period == DocumentRegisters.Info.Properties.NumberingPeriod.GetLocalizedValue(Sungero.Docflow.DocumentRegister.NumberingPeriod.Year).ToLower())
        return Sungero.Docflow.DocumentRegister.NumberingPeriod.Year;
      
      return null;
    }
    
    /// <summary>
    /// Получить локализованное значение документопотока.
    /// </summary>
    /// <param name="name">Название документопотока.</param>
    /// <returns>Локализованное значение документопотока.</returns>
    public Enumeration? GetDocumentFlow(string name)
    {
      name = name.ToLower();
      var inner = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Inner).ToLower();
      var contracts = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Contracts).ToLower();
      var incoming = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Incoming).ToLower();
      var outgoing = DocumentRegisters.Info.Properties.DocumentFlow.GetLocalizedValue(Sungero.Docflow.DocumentRegister.DocumentFlow.Outgoing).ToLower();
      
      if (name == inner)
        return Sungero.Docflow.DocumentRegister.DocumentFlow.Inner;
      if (name == contracts)
        return Sungero.Docflow.DocumentRegister.DocumentFlow.Contracts;
      if (name == incoming)
        return Sungero.Docflow.DocumentRegister.DocumentFlow.Incoming;
      if (name == outgoing)
        return Sungero.Docflow.DocumentRegister.DocumentFlow.Outgoing;
      
      return null;
    }
    
    /// <summary>
    /// Получить локализованное значение типа журнала.
    /// </summary>
    /// <param name="type">Название журнала.</param>
    /// <returns>Локализованное значение типа журнала.</returns>
    public Enumeration? GetRegisterType(string type)
    {
      type = type.ToLower();
      var registration = DocumentRegisters.Info.Properties.RegisterType.GetLocalizedValue(Sungero.Docflow.DocumentRegister.RegisterType.Registration).ToLower();
      var numbering = DocumentRegisters.Info.Properties.RegisterType.GetLocalizedValue(Sungero.Docflow.DocumentRegister.RegisterType.Numbering).ToLower();
      
      if (type == numbering)
        return Sungero.Docflow.DocumentRegister.RegisterType.Numbering;
      
      if (type == registration)
        return Sungero.Docflow.DocumentRegister.RegisterType.Registration;
      
      return null;
    }
    
    /// <summary>
    /// Получить запись справочника Журнал регистрации.
    /// </summary>
    /// <param name="documentRegister">Журнал регистрации.</param>
    /// <returns>Запись справочника Журнал регистрации.</returns>
    public IDocumentRegister GetDocumentRegisterRecord(Structures.Module.DocumentRegister documentRegister)
    {
      if (string.IsNullOrEmpty(documentRegister.Name))
        return null;
      
      return DocumentRegisters.GetAll(d => d.Name == documentRegister.Name &&
                                      d.Index == documentRegister.Index &&
                                      d.RegistrationGroup == GetRegistrationGroup(documentRegister.RegistrationGroup) &&
                                      d.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Виды документов
    
    /// <summary>
    /// Создать или обновить записи справочника Вид документа.
    /// </summary>
    /// <param name="documentKinds">Список видов документов.</param>
    /// <returns>Список видов документов.</returns>
    [Remote]
    public List<Structures.Module.DocumentKind> CreateOrUpdateDocumentKinds(List<Structures.Module.DocumentKind> documentKinds)
    {
      foreach (var documentKind in documentKinds.Where(r => string.IsNullOrEmpty(r.Error)))
      {
        try
        {
          var record = GetDocumentKindRecord(documentKind.Name);
          if (record == null)
            record = DocumentKinds.Create();
          record.Name = documentKind.Name;
          record.ShortName = documentKind.ShortName;
          record.Code = documentKind.Code;
          
          var documentFlow = GetDocumentFlow(documentKind.DocumentFlow);
          if (documentFlow == null)
            throw AppliedCodeException.Create(Resources.InvalidDocumentFlow);
          record.DocumentFlow = documentFlow;
          
          var documentType = GetDocumentType(documentKind.DocumentType, documentKind.DocumentFlow);
          if (documentType == null)
            throw AppliedCodeException.Create(Resources.DocumentTypeNotFound);
          record.DocumentType = documentType;
          
          if (record.DocumentType.DocumentFlow != record.DocumentFlow)
            throw AppliedCodeException.Create(Resources.InvalidDocumentType);
          
          if (!string.IsNullOrEmpty(documentKind.NumerationType))
            record.NumberingType = GetNumberingType(documentKind.NumerationType);
          
          var deadline = 0;
          if (!string.IsNullOrEmpty(documentKind.DeadlineDays))
          {
            if (!int.TryParse(documentKind.DeadlineDays, out deadline))
              throw AppliedCodeException.Create(Resources.IncorrectDataFormatDeadlineInDays);
            record.DeadlineInDays = deadline;
          }
          if (!string.IsNullOrEmpty(documentKind.DeadlineHours))
          {
            if (!int.TryParse(documentKind.DeadlineHours, out deadline))
              throw AppliedCodeException.Create(Resources.IncorrectDataFromatDeadlineInHours);
            record.DeadlineInHours = deadline;
          }
          
          record.Note = documentKind.Note;
          record.Save();
        }
        catch (Exception ex)
        {
          documentKind.Error = ex.Message;
        }
      }
      return documentKinds;
    }
    
    /// <summary>
    /// Получить Тип нумерации.
    /// </summary>
    /// <param name="numberingType">Название типа нумерации.</param>
    /// <returns>Локализованное значание типа нумерации.</returns>
    public Enumeration? GetNumberingType(string numberingType)
    {
      numberingType = numberingType.ToLower();
      var numerable = DocumentKinds.Info.Properties.NumberingType.GetLocalizedValue(Sungero.Docflow.DocumentKind.NumberingType.Numerable).ToLower();
      var notNumerable = DocumentKinds.Info.Properties.NumberingType.GetLocalizedValue(Sungero.Docflow.DocumentKind.NumberingType.NotNumerable).ToLower();
      var registrable = DocumentKinds.Info.Properties.NumberingType.GetLocalizedValue(Sungero.Docflow.DocumentKind.NumberingType.Registrable).ToLower();
      
      if (numberingType == numerable)
        return Sungero.Docflow.DocumentKind.NumberingType.Numerable;
      if (numberingType == notNumerable)
        return Sungero.Docflow.DocumentKind.NumberingType.NotNumerable;
      if (numberingType == registrable)
        return Sungero.Docflow.DocumentKind.NumberingType.Registrable;
      
      return null;
    }
    
    /// <summary>
    /// Получить запись справочника Тип документа.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <returns>Запись справочника Тип документа.</returns>
    public IDocumentType GetDocumentType(string name, string documentFlow)
    {
      if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(documentFlow))
        return null;
      
      return DocumentTypes.GetAll(d => d.Name == name && d.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    /// <summary>
    /// Получить запись справочника Вид документа.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <returns>Запись справочника Вид документа.</returns>
    public IDocumentKind GetDocumentKindRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      return DocumentKinds.GetAll(d => d.Name == name && d.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Страны
    
    /// <summary>
    /// Создать или обновить записи справочника Страна.
    /// </summary>
    /// <param name="countries">Список стран.</param>
    /// <returns>Список стран.</returns>
    [Remote]
    public List<Structures.Module.Country> CreateOrUpdateCountries(List<Structures.Module.Country> countries)
    {
      foreach (var country in countries.Where(c => string.IsNullOrEmpty(c.Error)))
      {
        try
        {
          var record = GetCountryRecord(country.Name, country.Code);
          if (record == null)
            record = Countries.Create();
          record.Name = country.Name;
          record.Code = country.Code;
          record.Save();
        }
        catch (Exception ex)
        {
          country.Error = ex.Message;
        }
      }
      return countries;
    }
    
    /// <summary>
    /// Получить запись справочника Страна.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <returns>Запись справочника Страна.</returns>
    public ICountry GetCountryRecord(string name, string code)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      return Countries.GetAll(c => c.Name == name && c.Code == code && c.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Валюты
    
    /// <summary>
    /// Создать или обновить валюты.
    /// </summary>
    /// <param name="currencies">Список валют.</param>
    /// <returns>Список валют.</returns>
    [Remote]
    public List<Structures.Module.Currency> CreateOrUpdateCurrencies(List<Structures.Module.Currency> currencies)
    {
      foreach (var currency in currencies.Where(c => string.IsNullOrEmpty(c.Error)))
      {
        try
        {
          var record = GetCurrency(currency.Name);
          if (record == null)
            record = Currencies.Create();
          record.Name = currency.Name;
          record.ShortName = currency.ShortName;
          record.FractionName = currency.FractionName;
          record.AlphaCode = currency.AlphaCode;
          record.NumericCode = currency.NumericCode;
          record.Status = Sungero.Commons.Currency.Status.Active;
          record.Save();
        }
        catch (Exception ex)
        {
          currency.Error = ex.Message;
        }
      }
      return currencies;
    }
    
    /// <summary>
    /// Получить валюту.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <returns>Запись справочника Валюта.</returns>
    public ICurrency GetCurrency(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      var abc = Currencies.GetAll().ToList();
      var q = abc.Where(c => c.Name == name && c.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
      return abc.Where(c => c.Name == name && c.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Сроки хранения
    
    // <summary>
    /// Создать или обновить записи справочника Сроки хранения.
    /// </summary>
    /// <param name="fileRetentionPeriods">Список сроков хранения.</param>
    [Remote]
    public List<Structures.Module.FileRetentionPeriod> CreateOrUpdateFileRetentionPeriods(List<Structures.Module.FileRetentionPeriod> fileRetentionPeriods)
    {
      foreach (var fileRetentionPeriod in fileRetentionPeriods.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetFileRetentionPeriodRecord(fileRetentionPeriod.Name);
          if (record == null)
            record = FileRetentionPeriods.Create();
          record.Name = fileRetentionPeriod.Name;
          if (!string.IsNullOrEmpty(fileRetentionPeriod.RetentionPeriod))
            record.RetentionPeriod = int.Parse(fileRetentionPeriod.RetentionPeriod);
          record.Note = fileRetentionPeriod.Note;
          record.Save();
        }
        catch (Exception ex)
        {
          fileRetentionPeriod.Error = ex.Message;
        }
      }
      return fileRetentionPeriods;
    }
    
    /// <summary>
    /// Получить запись справочника Сроки хранения.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <returns>Запись справочника Сроки хранения.</returns>
    public IFileRetentionPeriod GetFileRetentionPeriodRecord(string name)
    {
      if (string.IsNullOrEmpty(name))
        return null;
      
      return FileRetentionPeriods.GetAll(x => x.Name == name && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Номенклатура дел
    
    // <summary>
    /// Создать или обновить записи справочника Номенклатура дел.
    /// </summary>
    /// <param name="caseFilesInfo">Список номенклатур.</param>
    [Remote]
    public List<Structures.Module.CaseFile> CreateOrUpdateCaseFiles(Structures.Module.CaseFilesInfo caseFilesInfo)
    {
      foreach (var caseFile in caseFilesInfo.CaseFiles.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetCaseFileRecord(caseFile.Index, caseFilesInfo.BusinessUnit);
          if (record == null)
            record = CaseFiles.Create();
          record.StartDate = caseFilesInfo.StartDate;
          record.EndDate = caseFilesInfo.EndDate;
          record.BusinessUnit = caseFilesInfo.BusinessUnit;
          record.RegistrationGroup = caseFilesInfo.RegistrationGroup;
          record.Index = caseFile.Index;
          record.Title = caseFile.Title;
          if (!string.IsNullOrEmpty(caseFile.RetentionPeriod))
          {
            record.RetentionPeriod = GetFileRetentionPeriodRecord(caseFile.RetentionPeriod);
            if (record.RetentionPeriod == null)
              throw AppliedCodeException.Create(Resources.NotFoundRetentionPeriodExceptionTextFormat(caseFile.RetentionPeriod));
          }
          if (!string.IsNullOrEmpty(caseFile.Department))
          {
            record.Department = GetDepartmentRecord(caseFile.Department, caseFilesInfo.BusinessUnit.Name);
            if (record.Department == null)
              throw AppliedCodeException.Create(Resources.NotFoundDepartmentExceptionTextFormat(caseFile.Department, caseFilesInfo.BusinessUnit.Name));
          }
          record.Note = caseFile.Note;
          record.Save();
        }
        catch (Exception ex)
        {
          caseFile.Error = ex.Message;
        }
      }
      return caseFilesInfo.CaseFiles;
    }
    
    /// <summary>
    /// Получить запись справочника Номенклатура дел.
    /// </summary>
    /// <param name="index">Индекс.</param>
    /// <param name="businessUnit">Наша организация.</param>
    /// <returns>Запись справочника Номенклатура дел.</returns>
    public ICaseFile GetCaseFileRecord(string index, IBusinessUnit businessUnit)
    {
      if (string.IsNullOrEmpty(index))
        return null;
      
      return CaseFiles.GetAll(x => x.Index == index && x.BusinessUnit == businessUnit && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Населенные пункты
    
    /// <summary>
    /// Создать или обновить записи справочника населенные пункты.
    /// </summary>
    /// <param name="cities">Список населенный пунктов.</param>
    [Remote]
    public List<Structures.Module.City> CreateOrUpdateCities(List<Structures.Module.City> cities)
    {
      foreach (var city in cities.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetCityRecordByGuid(city.ObjectGUID);
          if (record == null)
          {
            record = GD.GovernmentSolution.Cities.Create();
            record.FIASIdGD = city.ObjectGUID;
          }
          
          record.Name = string.Format("{0}. {1}", city.TypeName, city.Name).Replace("..", ".");
          record.Country = city.Country;
          record.Region = city.Region;
          GD.GovernmentSolution.Cities.As(record).FiasInnerIdGD = city.ObjectId;
          record.Save();
        }
        catch (Exception ex)
        {
          city.Error = ex.Message;
        }
      }
      return cities;
    }

    /// <summary>
    /// Получить запись справочника Населенные пункты.
    /// </summary>
    /// <param name="guid">Гуид.</param>
    /// <returns>Запись справочника Организации.</returns>
    public GD.GovernmentSolution.ICity GetCityRecordByGuid(string guid)
    {
      if (string.IsNullOrEmpty(guid))
        return null;
      return GD.GovernmentSolution.Cities.GetAll(x => x.FIASIdGD != null && x.FIASIdGD.ToUpper() == guid.ToUpper() && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    #endregion

    #region Муниципальные районы
    
    /// <summary>
    /// Создать или обновить записи справочника муниципальные районы.
    /// </summary>
    /// <param name="municipalAreas">Список муниципальных районов.</param>
    [Remote]
    public List<Structures.Module.MunicipalArea> CreateOrUpdateMunicipalAreas(List<Structures.Module.MunicipalArea> municipalAreas)
    {
      foreach (var municipalArea in municipalAreas.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetMunicipalAreaRecordByGuid(municipalArea.ObjectGUID);
          if (record == null)
          {
            record = GD.GovernmentCommons.MunicipalAreas.Create();
            record.FiasId = municipalArea.ObjectGUID;
          }
          
          record.Name = string.Format("{0}. {1}", municipalArea.TypeName, municipalArea.Name).Replace("..", ".");
          record.Country = municipalArea.Country;
          record.Region = municipalArea.Region;
          record.FiasInnerId = municipalArea.ObjectId;
          record.Save();
        }
        catch (Exception ex)
        {
          municipalArea.Error = ex.Message;
        }
      }
      return municipalAreas;
    }

    /// <summary>
    /// Получить запись справочника Муниципальные районы.
    /// </summary>
    /// <param name="guid">Гуид.</param>
    /// <returns>Запись справочника Муниципальный район.</returns>
    public GD.GovernmentCommons.IMunicipalArea GetMunicipalAreaRecordByGuid(string guid)
    {
      if (string.IsNullOrEmpty(guid))
        return null;
      return GD.GovernmentCommons.MunicipalAreas.GetAll(x => x.FiasId != null && x.FiasId.ToUpper() == guid.ToUpper() && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }
    
    #endregion
    
    #region Поселения
    
    /// <summary>
    /// Создать или обновить записи справочника Поселения.
    /// </summary>
    /// <param name="settlements">Список поселений.</param>
    [Remote]
    public List<Structures.Module.Settlement> CreateOrUpdateSettlements(List<Structures.Module.Settlement> settlements)
    {
      foreach (var settlement in settlements.Where(x => string.IsNullOrEmpty(x.Error)))
      {
        try
        {
          var record = GetSettlementRecordByGuid(settlement.ObjectGUID);
          if (record == null)
          {
            record = GD.GovernmentCommons.Settlements.Create();
            record.FiasId = settlement.ObjectGUID;
          }
          
          record.Name = string.Format("{0}. {1}", settlement.TypeName, settlement.Name).Replace("..", ".");
          record.Country = settlement.Country;
          record.Region = settlement.Region;
          record.FiasInnerId = settlement.ObjectId;
          record.Save();
        }
        catch (Exception ex)
        {
          settlement.Error = ex.Message;
        }
      }
      return settlements;
    }

    /// <summary>
    /// Получить запись справочника Поселения.
    /// </summary>
    /// <param name="guid">Гуид.</param>
    /// <returns>Запись справочника Поселения.</returns>
    public GD.GovernmentCommons.ISettlement GetSettlementRecordByGuid(string guid)
    {
      if (string.IsNullOrEmpty(guid))
        return null;
      return GD.GovernmentCommons.Settlements.GetAll(x => x.FiasId != null && x.FiasId.ToUpper() == guid.ToUpper() && x.Status == Sungero.CoreEntities.DatabookEntry.Status.Active).FirstOrDefault();
    }

    #endregion
    
    #region Функции интеграции

    /// <summary>
    /// Добавить вышестоящий элемент в населенные пункты.
    /// </summary>
    /// <param name="objectId">Глобальный уникальный идентификатор адресного объекта.</param>
    /// <param name="parentObjId">Идентификатор родительского объект.</param>
    /// <returns>Если иерархия создана то True, иначе False</returns>
    [Public(WebApiRequestType = RequestType.Post)]
    public static bool AddMunicipalHierarchyCity(string objectId, string parentObjId)
    {
      var id = 0L;
      var parentId = 0L;
      if (!long.TryParse(objectId, out id) || !long.TryParse(parentObjId, out parentId))
        return false;
      var city = GD.GovernmentSolution.Cities.GetAll(c => c.FiasInnerIdGD == objectId).FirstOrDefault();
      var municipalArea = GD.GovernmentCommons.MunicipalAreas.GetAll(m => m.FiasInnerId == parentObjId).FirstOrDefault();
      var settlement = GD.GovernmentCommons.Settlements.GetAll(m => m.FiasInnerId == parentObjId).FirstOrDefault();
      if (city != null && (municipalArea != null || settlement != null))
      {
        try
        {
          city.MunicipalAreaGD = settlement != null ? settlement.MunicipalArea : municipalArea;
          city.SettlementGD = settlement;
          city.Save();
        }
        catch (Exception ex)
        {
          Logger.ErrorFormat("City id = {0} not update. Reason - {1}.", city.Id, ex.Message);
          return false;
        }
        return true;
      }
      return false;
    }

    /// <summary>
    /// Добавить муниципальный район в поселения.
    /// </summary>
    /// <param name="objectId">Глобальный уникальный идентификатор адресного объекта.</param>
    /// <param name="parentObjId">Идентификатор родительского объект.</param>
    /// <returns>Если иерархия создана то True, иначе False</returns>
    [Public(WebApiRequestType = RequestType.Post)]
    public static bool AddMunicipalHierarchySettlement(string objectId, string parentObjId)
    {
      var id = 0L;
      var parentId = 0L;
      if (!long.TryParse(objectId, out id) || !long.TryParse(parentObjId, out parentId))
        return false;
      var settlement = GD.GovernmentCommons.Settlements.GetAll(m => m.FiasInnerId == objectId).FirstOrDefault();
      var municipalArea = GD.GovernmentCommons.MunicipalAreas.GetAll(m => m.FiasInnerId == parentObjId).FirstOrDefault();
      if (municipalArea != null && settlement != null)
      {
        try
        {
          settlement.MunicipalArea = municipalArea;
          settlement.Save();
        }
        catch (Exception ex)
        {
          Logger.ErrorFormat("Settlement id = {0} not update. Reason - {1}.", settlement.Id, ex.Message);
          return false;
        }
        return true;
      }
      return false;
    }

    /// <summary>
    /// Получить id населенных пунктов.
    /// </summary>
    /// <returns>id населенных пунктов</returns>
    [Public(WebApiRequestType = RequestType.Get)]
    public static List<string> GetCityIds(string regionCode)
    {
      var region = Sungero.Commons.Regions.GetAll(r => r.Code != null && r.Code.ToUpper() == regionCode.ToUpper()).FirstOrDefault();
      if (region != null)
        return GD.GovernmentSolution.Cities.GetAll(c => c.FiasInnerIdGD != null && Equals(c.Region, region)).Select(c => c.FiasInnerIdGD).ToList();
      else
        return GD.GovernmentSolution.Cities.GetAll(c => c.FiasInnerIdGD != null).Select(c => c.FiasInnerIdGD).ToList();
    }

    /// <summary>
    /// Получить id муниципальных районов.
    /// </summary>
    /// <returns>id муниципальных районов</returns>
    [Public(WebApiRequestType = RequestType.Get)]
    public static List<string> GetMunicipalAreaIds(string regionCode)
    {
      var region = Sungero.Commons.Regions.GetAll(r => r.Code != null && r.Code.ToUpper() == regionCode.ToUpper()).FirstOrDefault();
      if (region != null)
        return GD.GovernmentCommons.MunicipalAreas.GetAll(c => c.FiasInnerId != null && Equals(c.Region, region)).Select(c => c.FiasInnerId).ToList();
      else
        return GD.GovernmentCommons.MunicipalAreas.GetAll(c => c.FiasInnerId != null).Select(c => c.FiasInnerId).ToList();
    }

    /// <summary>
    /// Получить id поселений.
    /// </summary>
    /// <returns>id поселений</returns>
    [Public(WebApiRequestType = RequestType.Get)]
    public static List<string> GetSettlementIds(string regionCode)
    {
      var region = Sungero.Commons.Regions.GetAll(r => r.Code != null && r.Code.ToUpper() == regionCode.ToUpper()).FirstOrDefault();
      if (region != null)
        return GD.GovernmentCommons.Settlements.GetAll(c => c.FiasInnerId != null && Equals(c.Region, region)).Select(c => c.FiasInnerId).ToList();
      else
        return GD.GovernmentCommons.Settlements.GetAll(c => c.FiasInnerId != null).Select(c => c.FiasInnerId).ToList();
    }
    
    #endregion

  }
}