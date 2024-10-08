using System;
using System.Collections.Generic;

namespace WorkingSpace_BusinessObject.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int? EmployeeId { get; set; }

    public string? ProfileImage { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? Dob { get; set; }

    public float? TeacherExprience { get; set; }

    public string? Degree { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<TeacherClass> TeacherClasses { get; set; } = new List<TeacherClass>();

    public virtual ICollection<TeacherGrade> TeacherGrades { get; set; } = new List<TeacherGrade>();

    public virtual ICollection<WorkbookCategory> WorkbookCategories { get; set; } = new List<WorkbookCategory>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
