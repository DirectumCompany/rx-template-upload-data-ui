using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.UploadData.Structures.ContactsLoaderErrorReport
{
  /// <summary>
  /// Контакт.
  /// </summary>
  partial class Contact
  {
    public string ReportSessionId { get; set; }
    
    public string LastName { get; set; }

    public string Name { get; set; }

    public string MiddleName { get; set; }
    
    public string Company { get; set; }
    
    public string JobTitle { get; set; }
    
    public string Phone { get; set; }
    
    public string Fax { get; set; }
    
    public string Email { get; set; }
    
    public string Homepage { get; set; }
    
    public string Note { get; set; }
    
    public string Error { get; set; }
  }
}