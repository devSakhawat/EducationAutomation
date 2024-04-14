using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbERP.Domain.DTOs.FEducation;

public class FEduRExamDto
{
   public long ExamId { get; set; }

   public int? ExamSl { get; set; }

   public string ExamName { get; set; }

   public string ExamType { get; set; }
}
