using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class ContactsLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      //TODO Отчет не открывается. Падает внутренняя ошибка сервера.
      var reportSessionId = System.Guid.NewGuid().ToString();
      ContactsLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.ContactsLoaderErrorReport.Contact>();
      foreach (var contact in ContactsLoaderErrorReport.LoaderErrorsStructure.Split(';'))
        tableData.Add(Structures.ContactsLoaderErrorReport.Contact.Create(
          reportSessionId,
          contact.Split('|')[0],
          contact.Split('|')[1],
          contact.Split('|')[2],
          contact.Split('|')[3],
          contact.Split('|')[4],
          contact.Split('|')[5],
          contact.Split('|')[6],
          contact.Split('|')[7],
          contact.Split('|')[8],
          contact.Split('|')[9],
          contact.Split('|')[10]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.ContactsLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.ContactsLoaderErrorReport.SourceTableName, ContactsLoaderErrorReport.ReportSessionId);
    }

  }
}