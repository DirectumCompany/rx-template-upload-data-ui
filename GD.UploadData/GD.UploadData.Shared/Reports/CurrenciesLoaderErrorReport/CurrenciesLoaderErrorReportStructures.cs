using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.CurrenciesLoaderErrorReport
{
  /// <summary>
  /// Валюта.
  /// </summary>
  partial class Currency
  {
    public string ReportSessionId { get; set; }
    
    public string Name { get; set; }
    
    public string ShortName { get; set; }
    
    public string FractionName { get; set;}
    
    public string AlphaCode { get; set; }
    
    public string NumericCode { get; set; }
    
    public string Error { get; set; }
  }
}