using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class Level
{
    public int Id { get; set; }

    public string? LevelName { get; set; }

    public virtual ICollection<Workbook> Workbooks { get; set; } = new List<Workbook>();
}
