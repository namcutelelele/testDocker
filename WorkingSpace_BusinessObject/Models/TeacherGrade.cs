using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class TeacherGrade
{
    public int Id { get; set; }

    public int WritingPaperId { get; set; }

    public float? TaskAchievementScore { get; set; }

    public float? CoherenceAndCoherensionScore { get; set; }

    public float? LexicalResourceScore { get; set; }

    public float? GrammarRangeScore { get; set; }

    public TimeOnly? GradeTime { get; set; }

    public int GraderAccountId { get; set; }

    public string? Status { get; set; }

    public virtual Teacher GraderAccount { get; set; } = null!;

    public virtual WritingPaper WritingPaper { get; set; } = null!;
}
