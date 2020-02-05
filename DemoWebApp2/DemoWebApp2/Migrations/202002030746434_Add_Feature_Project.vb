Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Add_Feature_Project
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Departments",
                Function(c) New With
                    {
                        .DepartmentID = c.Int(nullable := False, identity := True),
                        .Code = c.String(nullable := False, maxLength := 20),
                        .Name = c.String(nullable := False, maxLength := 200),
                        .BusinessAreas = c.Int(),
                        .Description = c.String(maxLength := 500)
                    }) _
                .PrimaryKey(Function(t) t.DepartmentID) _
                .Index(Function(t) t.Code, unique := True, name := "Ix_Code")
            
            CreateTable(
                "dbo.Projects",
                Function(c) New With
                    {
                        .ProjectID = c.Int(nullable := False, identity := True),
                        .Code = c.String(nullable := False, maxLength := 20),
                        .Name = c.String(nullable := False, maxLength := 200),
                        .Description = c.String(maxLength := 500),
                        .ScopeType = c.Int(),
                        .Scale = c.Int(),
                        .Duration = c.String(),
                        .PlanStartDate = c.DateTime(),
                        .PlanEndDate = c.DateTime(),
                        .StartDate = c.DateTime(),
                        .EndDate = c.DateTime(),
                        .DepartmentID = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ProjectID) _
                .ForeignKey("dbo.Departments", Function(t) t.DepartmentID, cascadeDelete := True) _
                .Index(Function(t) t.Code, unique := True, name := "Ix_Code") _
                .Index(Function(t) t.DepartmentID)
            
            CreateTable(
                "dbo.ProjectEmployees",
                Function(c) New With
                    {
                        .ProjectID = c.Int(nullable := False),
                        .EmployeeID = c.Int(nullable := False),
                        .Role = c.Int(),
                        .JoinDate = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) New With { t.ProjectID, t.EmployeeID }) _
                .ForeignKey("dbo.Employees", Function(t) t.EmployeeID, cascadeDelete := True) _
                .ForeignKey("dbo.Projects", Function(t) t.ProjectID, cascadeDelete := True) _
                .Index(Function(t) t.ProjectID) _
                .Index(Function(t) t.EmployeeID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.ProjectEmployees", "ProjectID", "dbo.Projects")
            DropForeignKey("dbo.ProjectEmployees", "EmployeeID", "dbo.Employees")
            DropForeignKey("dbo.Projects", "DepartmentID", "dbo.Departments")
            DropIndex("dbo.ProjectEmployees", New String() { "EmployeeID" })
            DropIndex("dbo.ProjectEmployees", New String() { "ProjectID" })
            DropIndex("dbo.Projects", New String() { "DepartmentID" })
            DropIndex("dbo.Projects", "Ix_Code")
            DropIndex("dbo.Departments", "Ix_Code")
            DropTable("dbo.ProjectEmployees")
            DropTable("dbo.Projects")
            DropTable("dbo.Departments")
        End Sub
    End Class
End Namespace
