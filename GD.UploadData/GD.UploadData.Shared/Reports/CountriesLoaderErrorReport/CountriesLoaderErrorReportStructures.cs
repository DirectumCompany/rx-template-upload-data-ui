using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.CountriesLoaderErrorReport
{
  /// <summary>
  /// Страна.
  /// </summary>
  partial class Country
  {
    public string ReportSessionId { get; set; }
    
    public string Name { get; set; }
    
    public string Code { get; set; }
    
    public string Error { get; set; }
  }
}