using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class WorkbookCategory
{
    public int Id { get; set; }

    public int TeacherId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? EditDate { get; set; }

    public string? Status { get; set; }

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual ICollection<Workbook> Workbooks { get; set; } = new List<Workbook>();
}
