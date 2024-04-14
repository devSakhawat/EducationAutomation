using pbERP.Domain.Models.FEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduLClassShiftDto
{
   public long ClassShiftId { get; set; }

   public string ClassShiftName { get; set; }

   //public virtual ICollection<FEduPLinkClassShift> FEduPLinkClassShifts { get; set; } = new List<FEduPLinkClassShift>();
}
