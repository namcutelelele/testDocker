using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class Workbook
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int WorkbookCategoryId { get; set; }

    public string? Description { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? EditDate { get; set; }

    public int LevelId { get; set; }

    public string? Status { get; set; }

    public virtual Level Level { get; set; } = null!;

    public virtual ICollection<StudentNoteBook> StudentNoteBooks { get; set; } = new List<StudentNoteBook>();

    public virtual WorkbookCategory WorkbookCategory { get; set; } = null!;

    public virtual ICollection<WorkbookEssayTask> WorkbookEssayTasks { get; set; } = new List<WorkbookEssayTask>();
}
