using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int FeedbackProvideId { get; set; }

    public int? StartPosition { get; set; }

    public int? EndPosition { get; set; }

    public DateOnly? FeedbackDate { get; set; }

    public int WritingPaperId { get; set; }

    public virtual Teacher FeedbackProvide { get; set; } = null!;

    public virtual ICollection<MistakesGrading> MistakesGradings { get; set; } = new List<MistakesGrading>();

    public virtual WritingPaper WritingPaper { get; set; } = null!;
}
