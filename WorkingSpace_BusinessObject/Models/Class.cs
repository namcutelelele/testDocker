using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class Class
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual ICollection<TeacherClass> TeacherClasses { get; set; } = new List<TeacherClass>();
}
