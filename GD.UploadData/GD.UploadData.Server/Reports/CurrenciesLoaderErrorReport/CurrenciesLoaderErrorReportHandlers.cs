using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData
{
  partial class CurrenciesLoaderErrorReportServerHandlers
  {

    public override void BeforeExecute(Sungero.Reporting.Server.BeforeExecuteEventArgs e)
    {
      var reportSessionId = System.Guid.NewGuid().ToString();
      CurrenciesLoaderErrorReport.ReportSessionId = reportSessionId;
      
      var tableData = new List<Structures.CurrenciesLoaderErrorReport.Currency>();
      foreach (var currency in CurrenciesLoaderErrorReport.LoaderErrorsStructure.Split(';'))
        tableData.Add(Structures.CurrenciesLoaderErrorReport.Currency.Create(
          reportSessionId,
          currency.Split('|')[0],
          currency.Split('|')[1],
          currency.Split('|')[2],
          currency.Split('|')[3],
          currency.Split('|')[4],
          currency.Split('|')[5]));
      
      Sungero.Docflow.PublicFunctions.Module.WriteStructuresToTable(Constants.CurrenciesLoaderErrorReport.SourceTableName, tableData);
    }

    public override void AfterExecute(Sungero.Reporting.Server.AfterExecuteEventArgs e)
    {
      Sungero.Docflow.PublicFunctions.Module.DeleteReportData(Constants.CurrenciesLoaderErrorReport.SourceTableName, CurrenciesLoaderErrorReport.ReportSessionId);
    }

  }
}