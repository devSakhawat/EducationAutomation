namespace pbERP.Domain.Models.FEducation;

public partial class FEduExamAGradePoint
{
   public long GradePointId { get; set; }

   public int MarkPointStart { get; set; }

   public int MarkPointEnd { get; set; }

   public int GradePonit { get; set; }

   public string LetterGrade { get; set; }

   public string Note { get; set; }
}
