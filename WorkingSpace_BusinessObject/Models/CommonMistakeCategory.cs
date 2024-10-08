using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class CommonMistakeCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<CommonMistake> CommonMistakes { get; set; } = new List<CommonMistake>();
}
