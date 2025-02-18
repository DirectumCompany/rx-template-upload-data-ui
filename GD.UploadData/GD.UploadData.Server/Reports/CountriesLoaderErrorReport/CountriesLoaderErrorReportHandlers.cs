using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class CountriesLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      var reportSessionId = System.Guid.NewGuid().ToString();
      CountriesLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.CountriesLoaderErrorReport.Country>();
      foreach (var country in CountriesLoaderErrorReport.LoaderErrorsStructure.Split(','))
        tableData.Add(Structures.CountriesLoaderErrorReport.Country.Create(
          reportSessionId,
          country.Split('|')[0],
          country.Split('|')[1],
          country.Split('|')[2]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.CountriesLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.CountriesLoaderErrorReport.SourceTableName, CountriesLoaderErrorReport.ReportSessionId);
    }

  }
}