using pbERP.Domain.Models.FEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduKClassGroupDto
{
   public long ClassGroupId { get; set; }

   public string ClassGroupName { get; set; }

   //public virtual ICollection<FEduNLinkClassGroup> FEduNLinkClassGroups { get; set; } = new List<FEduNLinkClassGroup>();

   //public virtual ICollection<FEduQLinkClassSubject> FEduQLinkClassSubjects { get; set; } = new List<FEduQLinkClassSubject>();
}
