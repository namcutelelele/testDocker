using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class StudentNoteBook
{
    public int Id { get; set; }

    public int WorkingSpaceId { get; set; }

    public int WorkbookId { get; set; }

    public virtual Workbook Workbook { get; set; } = null!;

    public virtual WorkingSpace WorkingSpace { get; set; } = null!;
}
