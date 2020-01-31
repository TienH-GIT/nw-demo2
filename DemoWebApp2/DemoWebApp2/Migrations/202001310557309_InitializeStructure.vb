Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitializeStructure
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Branches",
                Function(c) New With
                    {
                        .BranchID = c.Int(nullable := False, identity := True),
                        .Code = c.String(nullable := False, maxLength := 20),
                        .Name = c.String(nullable := False, maxLength := 200),
                        .KataName = c.String(maxLength := 200),
                        .Address = c.String(maxLength := 300),
                        .Description = c.String(maxLength := 500)
                    }) _
                .PrimaryKey(Function(t) t.BranchID)
            
            CreateTable(
                "dbo.Employees",
                Function(c) New With
                    {
                        .ID = c.Int(nullable := False, identity := True),
                        .Code = c.String(nullable := False, maxLength := 20),
                        .FirstName = c.String(nullable := False, maxLength := 100),
                        .LastName = c.String(maxLength := 100),
                        .StartDate = c.DateTime(),
                        .RetireDate = c.DateTime(),
                        .IsActive = c.Boolean(nullable := False),
                        .JobTitleID = c.Int(nullable := False),
                        .BranchID = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.ID) _
                .ForeignKey("dbo.Branches", Function(t) t.BranchID, cascadeDelete := True) _
                .ForeignKey("dbo.JobTitles", Function(t) t.JobTitleID, cascadeDelete := True) _
                .Index(Function(t) t.JobTitleID) _
                .Index(Function(t) t.BranchID)
            
            CreateTable(
                "dbo.PersonalInfoes",
                Function(c) New With
                    {
                        .PersonalInfoID = c.Int(nullable := False),
                        .Gender = c.Int(),
                        .Birthday = c.DateTime(),
                        .Age = c.Int(),
                        .Address = c.String(maxLength := 300),
                        .Status = c.Int(),
                        .Hobby = c.String(maxLength := 200)
                    }) _
                .PrimaryKey(Function(t) t.PersonalInfoID) _
                .ForeignKey("dbo.Employees", Function(t) t.PersonalInfoID) _
                .Index(Function(t) t.PersonalInfoID)
            
            CreateTable(
                "dbo.JobTitles",
                Function(c) New With
                    {
                        .JobTitleID = c.Int(nullable := False, identity := True),
                        .Code = c.String(nullable := False, maxLength := 20),
                        .Name = c.String(nullable := False, maxLength := 200),
                        .Description = c.String(maxLength := 500)
                    }) _
                .PrimaryKey(Function(t) t.JobTitleID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Employees", "JobTitleID", "dbo.JobTitles")
            DropForeignKey("dbo.PersonalInfoes", "PersonalInfoID", "dbo.Employees")
            DropForeignKey("dbo.Employees", "BranchID", "dbo.Branches")
            DropIndex("dbo.PersonalInfoes", New String() { "PersonalInfoID" })
            DropIndex("dbo.Employees", New String() { "BranchID" })
            DropIndex("dbo.Employees", New String() { "JobTitleID" })
            DropTable("dbo.JobTitles")
            DropTable("dbo.PersonalInfoes")
            DropTable("dbo.Employees")
            DropTable("dbo.Branches")
        End Sub
    End Class
End Namespace
