using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.DocumentRegisterLoaderErrorReport
{
  /// <summary>
  /// Журнал регистрации.
  /// </summary>
  partial class DocumentRegister
  {
    public string ReportSessionId { get; set; }
    
    public string Name { get; set; }
    
    public string RegisterType { get; set; }
    
    public string Index { get; set; }
    
    public string DocumentFlow { get; set; }
    
    public string NumberOfDigitsInItem { get; set; }
    
    public string NumberedSection { get; set; }
    
    public string NumberingPeriod { get; set; }
    
    public string RegistrationGroup { get; set; }
    
    public string Error { get; set; }
  }
}