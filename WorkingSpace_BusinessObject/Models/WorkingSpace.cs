using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class WorkingSpace
{
    public int Id { get; set; }

    public string? WorkingSpaceDisplayName { get; set; }

    public int StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<StudentNoteBook> StudentNoteBooks { get; set; } = new List<StudentNoteBook>();
}
