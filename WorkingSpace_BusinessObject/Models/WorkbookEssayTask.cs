using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class WorkbookEssayTask
{
    public int Id { get; set; }

    public int WorkbookCategoryId { get; set; }

    public int? WorkbookId { get; set; }

    public int TaskId { get; set; }

    public virtual EssayTask Task { get; set; } = null!;

    public virtual ICollection<WritingPaper> WritingPapers { get; set; } = new List<WritingPaper>();

    public virtual ICollection<Workbook> Workbooks { get; set; } = new List<Workbook>();
}
