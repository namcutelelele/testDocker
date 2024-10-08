using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class WritingPaper
{
    public int Id { get; set; }

    public int WorkbookEssayTaskId { get; set; }

    public int WriterId { get; set; }

    public DateOnly? WrittenDate { get; set; }

    public string? WrittenContent { get; set; }

    public TimeOnly? WrittenTime { get; set; }

    public TimeOnly? SubmitTime { get; set; }

    public string? Status { get; set; }

    public int? WritingPaperChainId { get; set; }

    public int? StudentNoteBookId { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<TeacherGrade> TeacherGrades { get; set; } = new List<TeacherGrade>();

    public virtual WorkbookEssayTask WorkbookEssayTask { get; set; } = null!;

    public virtual Student Writer { get; set; } = null!;
}
