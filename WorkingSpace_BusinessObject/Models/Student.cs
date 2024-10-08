using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class Student
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string? ProfileImage { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? Dob { get; set; }

    public int? DepartmentId { get; set; }

    public string? RollNumber { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();

    public virtual ICollection<WorkingSpace> WorkingSpaces { get; set; } = new List<WorkingSpace>();

    public virtual ICollection<WritingPaper> WritingPapers { get; set; } = new List<WritingPaper>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
