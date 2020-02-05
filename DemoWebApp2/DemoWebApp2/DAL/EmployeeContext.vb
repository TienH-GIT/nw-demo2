Imports DemoWebApp2.EntityConfigs
Imports DemoWebApp2.Models
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions

Namespace DAL
    Public Class EmployeeContext
        Inherits ApplicationDbContext

        Public Property Employees As DbSet(Of Employee)
        Public Property PersonalInfoes As DbSet(Of PersonalInfo)
        Public Property Branches As DbSet(Of Branch)
        Public Property JobTitles As DbSet(Of JobTitle)
        Public Property Departments As DbSet(Of Department)
        Public Property Projects As DbSet(Of Project)
        Public Property ProjectEmployees As DbSet(Of ProjectEmployee)

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            ' Distributed EntityConfiguration class
            modelBuilder.Configurations _
                .Add(New EmployeeEntityConfig()) _
                .Add(New PersonalInfoEntityConfig()) _
                .Add(New BranchEntityConfig()) _
                .Add(New JobTitleEntityConfig())

            ' Relation of Employee & PersonalInfo (One-to-One)
            modelBuilder.Entity(Of Employee) _
                .HasRequired(Function(p) p.Detail) _
                .WithRequiredPrincipal(Function(s) s.Employee)

            ' Relation of Project & Employee (Many-to-Many)
            modelBuilder.Entity(Of ProjectEmployee) _
                .HasKey(Function(t) New With {t.ProjectID, t.EmployeeID})

            modelBuilder.Entity(Of ProjectEmployee) _
                .HasRequired(Function(pt) pt.Project) _
                .WithMany(Function(p) p.ProjectEmployees) _
                .HasForeignKey(Function(pt) pt.ProjectID)

            modelBuilder.Entity(Of ProjectEmployee) _
                .HasRequired(Function(pt) pt.Employee) _
                .WithMany(Function(p) p.ProjectEmployees) _
                .HasForeignKey(Function(pt) pt.EmployeeID)
        End Sub
    End Class
End Namespace
