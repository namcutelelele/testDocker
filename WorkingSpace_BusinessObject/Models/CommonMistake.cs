using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class CommonMistake
{
    public int MistakeId { get; set; }

    public string? Description { get; set; }

    public string? Example { get; set; }

    public int CategoryId { get; set; }

    public virtual CommonMistakeCategory Category { get; set; } = null!;

    public virtual ICollection<MistakesGrading> MistakesGradings { get; set; } = new List<MistakesGrading>();
}
