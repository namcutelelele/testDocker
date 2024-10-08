using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class MistakesGrading
{
    public int Id { get; set; }

    public int FeedbackId { get; set; }

    public int MistakesId { get; set; }

    public string? IsGood { get; set; }

    public string? Comment { get; set; }

    public virtual Feedback Feedback { get; set; } = null!;

    public virtual CommonMistake Mistakes { get; set; } = null!;
}
