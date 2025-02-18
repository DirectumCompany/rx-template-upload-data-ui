using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class RolesLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      var reportSessionId = System.Guid.NewGuid().ToString();
      RolesLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.RolesLoaderErrorReport.Role>();
      foreach (var role in RolesLoaderErrorReport.LoaderErrorsStructure.Split(','))
        tableData.Add(Structures.RolesLoaderErrorReport.Role.Create(
          reportSessionId,
          role.Split('|')[0],
          role.Split('|')[1],
          role.Split('|')[2],
          role.Split('|')[3]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.RolesLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.RolesLoaderErrorReport.SourceTableName, RolesLoaderErrorReport.ReportSessionId);
    }

  }
}