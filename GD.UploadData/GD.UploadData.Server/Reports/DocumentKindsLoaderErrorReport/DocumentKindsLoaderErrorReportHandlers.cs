using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class DocumentKindsLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      var reportSessionId = System.Guid.NewGuid().ToString();
      DocumentKindsLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.DocumentKindsLoaderErrorReport.DocumentKind>();
      foreach (var documentKind in DocumentKindsLoaderErrorReport.LoaderErrorsStructure.Split('#'))
        tableData.Add(Structures.DocumentKindsLoaderErrorReport.DocumentKind.Create(
          reportSessionId,
          documentKind.Split('|')[0],
          documentKind.Split('|')[1],
          documentKind.Split('|')[2],
          documentKind.Split('|')[3],
          documentKind.Split('|')[4],
          documentKind.Split('|')[5],
          documentKind.Split('|')[6],
          documentKind.Split('|')[7],
          documentKind.Split('|')[8],
          documentKind.Split('|')[9]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.DocumentKindsLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.DocumentKindsLoaderErrorReport.SourceTableName,
                                                              DocumentKindsLoaderErrorReport.ReportSessionId);
    }

  }
}