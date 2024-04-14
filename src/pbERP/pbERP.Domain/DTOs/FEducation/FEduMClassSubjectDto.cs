using pbERP.Domain.Models.FEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduMClassSubjectDto
{
   public long ClassSubjectId { get; set; }

   public int? ClassSubjectSl { get; set; }

   public string ClassSubjectCode { get; set; }

   public string ClassSubjectName { get; set; }

   public virtual ICollection<FEduQLinkClassSubject> FEduQLinkClassSubjects { get; set; } = new List<FEduQLinkClassSubject>();
}
