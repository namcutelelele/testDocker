using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class EssayTask
{
    public int Id { get; set; }

    public string? TaskName { get; set; }

    public int? WordCountLimit { get; set; }

    public int? DurationLimit { get; set; }

    public string? TaskPlainContent { get; set; }

    public string? TaskHtmlcontent { get; set; }

    public int? TaskOwnerId { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public string? TaskUpdateChain { get; set; }

    public int WorkbookEssayTaskId { get; set; }

    public string? Status { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public virtual ICollection<WorkbookEssayTask> WorkbookEssayTasks { get; set; } = new List<WorkbookEssayTask>();
}
