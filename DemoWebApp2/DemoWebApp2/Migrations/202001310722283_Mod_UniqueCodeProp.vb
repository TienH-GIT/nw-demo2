Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Mod_UniqueCodeProp
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateIndex("dbo.Branches", "Code", unique := True, name := "Ix_Code")
            CreateIndex("dbo.Employees", "Code", unique := True, name := "Ix_Code")
            CreateIndex("dbo.JobTitles", "Code", unique := True, name := "Ix_Code")
        End Sub
        
        Public Overrides Sub Down()
            DropIndex("dbo.JobTitles", "Ix_Code")
            DropIndex("dbo.Employees", "Ix_Code")
            DropIndex("dbo.Branches", "Ix_Code")
        End Sub
    End Class
End Namespace
