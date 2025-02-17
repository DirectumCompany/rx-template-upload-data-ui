using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.RolesLoaderErrorReport
{
  /// <summary>
  /// Роль.
  /// </summary>
  partial class Role
  {
    public string ReportSessionId { get; set; }
    
    public string Name { get; set; }
    
    public string Note { get; set; }
    
    public string IsSingleUser { get; set;}
    
    public string Recipients { get; set; }
    
    public string Error { get; set; }
  }
}