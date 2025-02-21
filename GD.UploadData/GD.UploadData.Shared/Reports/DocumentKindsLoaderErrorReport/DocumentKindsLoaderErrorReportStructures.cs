using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.DocumentKindsLoaderErrorReport
{
  /// <summary>
  /// Вид документа.
  /// </summary>
  partial class DocumentKind
  {
    public string ReportSessionId { get; set; }
    
    public string Name { get; set; }
    
    public string ShortName { get; set; }
    
    public string Code { get; set; }
    
    public string NumerationType { get; set; }
    
    public string DocumentFlow { get; set; }
    
    public string DocumentType { get; set; }
    
    public string DeadlineDays { get; set; }
    
    public string DeadlineHours { get; set; }
    
    public string Note { get; set; }
    
    public string Error { get; set; }
  }
}