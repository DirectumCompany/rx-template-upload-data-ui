using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.RegistrationGroupLoaderErrorReport
{
  /// <summary>
  /// 
  /// </summary>
  partial class RegistrationGroup
  {
    public string ReportSessionId { get; set; }
    
    public string Name { get; set; }
    
    public string Index { get; set; }
    
    public string ResponsibleEmployee { get; set; }
    
    public string DocumentFlow { get; set; }
    
    public string RecipientLinks { get; set; }
    
    public string Departments { get; set; }
    
    public string Description { get; set; }
    
    public string Error { get; set; }
  }
}