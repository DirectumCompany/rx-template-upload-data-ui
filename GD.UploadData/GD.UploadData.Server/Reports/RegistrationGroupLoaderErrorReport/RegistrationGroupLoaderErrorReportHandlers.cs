using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class RegistrationGroupLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      var reportSessionId = System.Guid.NewGuid().ToString();
      RegistrationGroupLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.RegistrationGroupLoaderErrorReport.RegistrationGroup>();
      foreach (var application in RegistrationGroupLoaderErrorReport.LoaderErrorsStructure.Split('#'))
        tableData.Add(Structures.RegistrationGroupLoaderErrorReport.RegistrationGroup.Create(
          reportSessionId,
          application.Split('|')[0],
          application.Split('|')[1],
          application.Split('|')[2],
          application.Split('|')[3],
          application.Split('|')[4],
          application.Split('|')[5],
          application.Split('|')[6],
          application.Split('|')[7]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.RegistrationGroupLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.RegistrationGroupLoaderErrorReport.SourceTableName,
                                                              RegistrationGroupLoaderErrorReport.ReportSessionId);
    }

  }
}