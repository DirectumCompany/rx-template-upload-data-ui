using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.AssociatedApplicationLoaderErrorReport
{
  /// <summary>
  /// 
  /// </summary>
  partial class AssociatedApplication
  {
    public string ReportSessionId { get; set; }
    
    public string Name { get; set; }
    
    public string Extension { get; set; }
    
    public string MonitoringType { get; set; }
    
    public string OpenByDefaultForReading { get; set; }
    
    public string Error { get; set; }
  }
}