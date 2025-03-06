using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class DocumentRegisterLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      var reportSessionId = System.Guid.NewGuid().ToString();
      DocumentRegisterLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.DocumentRegisterLoaderErrorReport.DocumentRegister>();
      foreach (var documentRegister in DocumentRegisterLoaderErrorReport.LoaderErrorsStructure.Split('#'))
        tableData.Add(Structures.DocumentRegisterLoaderErrorReport.DocumentRegister.Create(
          reportSessionId,
          documentRegister.Split('|')[0],
          documentRegister.Split('|')[1],
          documentRegister.Split('|')[2],
          documentRegister.Split('|')[3],
          documentRegister.Split('|')[4],
          documentRegister.Split('|')[5],
          documentRegister.Split('|')[6],
          documentRegister.Split('|')[7],
          documentRegister.Split('|')[8]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.DocumentRegisterLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.DocumentRegisterLoaderErrorReport.SourceTableName,
                                                              DocumentRegisterLoaderErrorReport.ReportSessionId);
    }

  }
}