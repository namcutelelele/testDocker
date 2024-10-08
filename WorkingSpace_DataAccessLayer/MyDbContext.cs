using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace WorkingSpace_BusinessObject.Models;

public partial class MyDbContext : DbContext
{

    private readonly IConfiguration _configuration;
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration)
    : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<CommonMistake> CommonMistakes { get; set; }

    public virtual DbSet<CommonMistakeCategory> CommonMistakeCategories { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EssayTask> EssayTasks { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<MistakesGrading> MistakesGradings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentClass> StudentClasses { get; set; }

    public virtual DbSet<StudentNoteBook> StudentNoteBooks { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherClass> TeacherClasses { get; set; }

    public virtual DbSet<TeacherGrade> TeacherGrades { get; set; }

    public virtual DbSet<Workbook> Workbooks { get; set; }

    public virtual DbSet<WorkbookCategory> WorkbookCategories { get; set; }

    public virtual DbSet<WorkbookEssayTask> WorkbookEssayTasks { get; set; }

    public virtual DbSet<WorkingSpace> WorkingSpaces { get; set; }

    public virtual DbSet<WritingPaper> WritingPapers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnectionString");
        optionsBuilder.UseMySql(connectionString,
            new MySqlServerVersion(new Version(8, 0, 33)));
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=178.128.91.153;port=3307;database=AES_db;user=root;password=tuanparkyang", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.0.1-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Account");

            entity.HasIndex(e => e.RoleId, "FKAccount757266");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status).HasMaxLength(1);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAccount757266");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Class");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(1);
        });

        modelBuilder.Entity<CommonMistake>(entity =>
        {
            entity.HasKey(e => e.MistakeId).HasName("PRIMARY");

            entity.HasIndex(e => e.CategoryId, "FKCommonMist11954");

            entity.Property(e => e.MistakeId).HasColumnName("MistakeID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Example).HasColumnType("text");

            entity.HasOne(d => d.Category).WithMany(p => p.CommonMistakes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCommonMist11954");
        });

        modelBuilder.Entity<CommonMistakeCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CommonMistakeCategory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsFixedLength();
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Department");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(1);

            entity.HasMany(d => d.Students).WithMany(p => p.Departments)
                .UsingEntity<Dictionary<string, object>>(
                    "DepartmentStudent",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKDepartment75175"),
                    l => l.HasOne<Department>().WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKDepartment252519"),
                    j =>
                    {
                        j.HasKey("DepartmentId", "StudentId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Department_Student");
                        j.HasIndex(new[] { "StudentId" }, "FKDepartment75175");
                        j.IndexerProperty<int>("DepartmentId").HasColumnName("DepartmentID");
                        j.IndexerProperty<int>("StudentId").HasColumnName("StudentID");
                    });

            entity.HasMany(d => d.Teachers).WithMany(p => p.Departments)
                .UsingEntity<Dictionary<string, object>>(
                    "DepartmentTeacher",
                    r => r.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKDepartment927884"),
                    l => l.HasOne<Department>().WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKDepartment683272"),
                    j =>
                    {
                        j.HasKey("DepartmentId", "TeacherId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Department_Teacher");
                        j.HasIndex(new[] { "TeacherId" }, "FKDepartment927884");
                        j.IndexerProperty<int>("DepartmentId").HasColumnName("DepartmentID");
                        j.IndexerProperty<int>("TeacherId").HasColumnName("TeacherID");
                    });
        });

        modelBuilder.Entity<EssayTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("EssayTask");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.TaskHtmlcontent)
                .HasColumnType("text")
                .HasColumnName("TaskHTMLContent");
            entity.Property(e => e.TaskName).HasMaxLength(255);
            entity.Property(e => e.TaskOwnerId).HasColumnName("TaskOwnerID");
            entity.Property(e => e.TaskPlainContent).HasColumnType("text");
            entity.Property(e => e.TaskUpdateChain).HasMaxLength(255);
            entity.Property(e => e.WorkbookEssayTaskId).HasColumnName("WorkbookEssayTaskID");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.WritingPaperId, "FKFeedback235458");

            entity.HasIndex(e => e.FeedbackProvideId, "FKFeedback910383");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FeedbackProvideId).HasColumnName("FeedbackProvideID");
            entity.Property(e => e.WritingPaperId).HasColumnName("WritingPaperID");

            entity.HasOne(d => d.FeedbackProvide).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.FeedbackProvideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFeedback910383");

            entity.HasOne(d => d.WritingPaper).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.WritingPaperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKFeedback235458");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Level");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LevelName).HasMaxLength(255);
        });

        modelBuilder.Entity<MistakesGrading>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("MistakesGrading");

            entity.HasIndex(e => e.FeedbackId, "FKMistakesGr130223");

            entity.HasIndex(e => e.MistakesId, "FKMistakesGr983740");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.IsGood).HasMaxLength(1);
            entity.Property(e => e.MistakesId).HasColumnName("MistakesID");

            entity.HasOne(d => d.Feedback).WithMany(p => p.MistakesGradings)
                .HasForeignKey(d => d.FeedbackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMistakesGr130223");

            entity.HasOne(d => d.Mistakes).WithMany(p => p.MistakesGradings)
                .HasForeignKey(d => d.MistakesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMistakesGr983740");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(255);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Student");

            entity.HasIndex(e => e.AccountId, "FKStudent782508");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.RollNumber).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.Students)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKStudent782508");
        });

        modelBuilder.Entity<StudentClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("StudentClass");

            entity.HasIndex(e => e.StudentId, "FKStudentCla35959");

            entity.HasIndex(e => e.ClassId, "FKStudentCla49461");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Class).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKStudentCla49461");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKStudentCla35959");
        });

        modelBuilder.Entity<StudentNoteBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("StudentNoteBook");

            entity.HasIndex(e => e.WorkbookId, "FKStudentNot000010_idx");

            entity.HasIndex(e => e.WorkingSpaceId, "FKStudentNot279000");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.WorkbookId).HasColumnName("WorkbookID");
            entity.Property(e => e.WorkingSpaceId).HasColumnName("WorkingSpaceID");

            entity.HasOne(d => d.Workbook).WithMany(p => p.StudentNoteBooks)
                .HasForeignKey(d => d.WorkbookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKStudentNot000010");

            entity.HasOne(d => d.WorkingSpace).WithMany(p => p.StudentNoteBooks)
                .HasForeignKey(d => d.WorkingSpaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKStudentNot279000");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Teacher");

            entity.HasIndex(e => e.AccountId, "FKTeacher351755");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Degree).HasMaxLength(1000);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);

            entity.HasOne(d => d.Account).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTeacher351755");
        });

        modelBuilder.Entity<TeacherClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("TeacherClass");

            entity.HasIndex(e => e.ClassId, "FKTeacherCla103352");

            entity.HasIndex(e => e.TeacherId, "FKTeacherCla309434");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Class).WithMany(p => p.TeacherClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTeacherCla103352");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherClasses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTeacherCla309434");
        });

        modelBuilder.Entity<TeacherGrade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("TeacherGrade");

            entity.HasIndex(e => e.WritingPaperId, "FKTeacherGra105716");

            entity.HasIndex(e => e.GraderAccountId, "FKTeacherGra940251");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.GradeTime).HasMaxLength(6);
            entity.Property(e => e.GraderAccountId).HasColumnName("GraderAccountID");
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.WritingPaperId).HasColumnName("WritingPaperID");

            entity.HasOne(d => d.GraderAccount).WithMany(p => p.TeacherGrades)
                .HasForeignKey(d => d.GraderAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTeacherGra940251");

            entity.HasOne(d => d.WritingPaper).WithMany(p => p.TeacherGrades)
                .HasForeignKey(d => d.WritingPaperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTeacherGra105716");
        });

        modelBuilder.Entity<Workbook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Workbook");

            entity.HasIndex(e => e.WorkbookCategoryId, "FKWorkbook550451");

            entity.HasIndex(e => e.LevelId, "FKWorkbook662091");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.LevelId).HasColumnName("LevelID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.WorkbookCategoryId).HasColumnName("WorkbookCategoryID");

            entity.HasOne(d => d.Level).WithMany(p => p.Workbooks)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkbook662091");

            entity.HasOne(d => d.WorkbookCategory).WithMany(p => p.Workbooks)
                .HasForeignKey(d => d.WorkbookCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkbook550451");

            entity.HasMany(d => d.WorkbookEssayTasks).WithMany(p => p.Workbooks)
                .UsingEntity<Dictionary<string, object>>(
                    "WorkbookWorkbookEssayTask",
                    r => r.HasOne<WorkbookEssayTask>().WithMany()
                        .HasForeignKey("WorkbookEssayTaskId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKWorkbook_W356021"),
                    l => l.HasOne<Workbook>().WithMany()
                        .HasForeignKey("WorkbookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKWorkbook_W532387"),
                    j =>
                    {
                        j.HasKey("WorkbookId", "WorkbookEssayTaskId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Workbook_WorkbookEssayTask");
                        j.HasIndex(new[] { "WorkbookEssayTaskId" }, "FKWorkbook_W356021");
                        j.IndexerProperty<int>("WorkbookId").HasColumnName("WorkbookID");
                        j.IndexerProperty<int>("WorkbookEssayTaskId").HasColumnName("WorkbookEssayTaskID");
                    });
        });

        modelBuilder.Entity<WorkbookCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("WorkbookCategory");

            entity.HasIndex(e => e.TeacherId, "FKWorkbookCa866651");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Teacher).WithMany(p => p.WorkbookCategories)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkbookCa866651");
        });

        modelBuilder.Entity<WorkbookEssayTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("WorkbookEssayTask");

            entity.HasIndex(e => e.TaskId, "FKWorkbookEs504719");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.WorkbookCategoryId).HasColumnName("WorkbookCategoryID");
            entity.Property(e => e.WorkbookId).HasColumnName("WorkbookID");

            entity.HasOne(d => d.Task).WithMany(p => p.WorkbookEssayTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkbookEs504719");
        });

        modelBuilder.Entity<WorkingSpace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("WorkingSpace");

            entity.HasIndex(e => e.StudentId, "FKWorkingSpa599185");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.WorkingSpaceDisplayName).HasMaxLength(255);

            entity.HasOne(d => d.Student).WithMany(p => p.WorkingSpaces)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWorkingSpa599185");
        });

        modelBuilder.Entity<WritingPaper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("WritingPaper");

            entity.HasIndex(e => e.WorkbookEssayTaskId, "FKWritingPap350772");

            entity.HasIndex(e => e.WriterId, "FKWritingPap98181");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.StudentNoteBookId).HasColumnName("StudentNoteBookID");
            entity.Property(e => e.SubmitTime).HasMaxLength(6);
            entity.Property(e => e.WorkbookEssayTaskId).HasColumnName("WorkbookEssayTaskID");
            entity.Property(e => e.WriterId).HasColumnName("WriterID");
            entity.Property(e => e.WritingPaperChainId).HasColumnName("WritingPaperChainID");
            entity.Property(e => e.WrittenTime).HasMaxLength(6);

            entity.HasOne(d => d.WorkbookEssayTask).WithMany(p => p.WritingPapers)
                .HasForeignKey(d => d.WorkbookEssayTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWritingPap350772");

            entity.HasOne(d => d.Writer).WithMany(p => p.WritingPapers)
                .HasForeignKey(d => d.WriterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKWritingPap98181");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
