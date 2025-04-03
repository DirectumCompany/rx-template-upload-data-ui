using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class AssociatedApplicationLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      var reportSessionId = System.Guid.NewGuid().ToString();
      AssociatedApplicationLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.AssociatedApplicationLoaderErrorReport.AssociatedApplication>();
      foreach (var application in AssociatedApplicationLoaderErrorReport.LoaderErrorsStructure.Split(Constants.Module.ReportParse))
        tableData.Add(Structures.AssociatedApplicationLoaderErrorReport.AssociatedApplication.Create(
          reportSessionId,
          application.Split('|')[0],
          application.Split('|')[1],
          application.Split('|')[2],
          application.Split('|')[3],
          application.Split('|')[4]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.AssociatedApplicationLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.AssociatedApplicationLoaderErrorReport.SourceTableName,
                                                              AssociatedApplicationLoaderErrorReport.ReportSessionId);
    }

  }
}