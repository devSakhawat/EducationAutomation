using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduRExam
{
    public long ExamId { get; set; }

    public int? ExamSl { get; set; }

    public string ExamName { get; set; }

    public string ExamNameLocal { get; set; }

    public string ExamType { get; set; }
}
