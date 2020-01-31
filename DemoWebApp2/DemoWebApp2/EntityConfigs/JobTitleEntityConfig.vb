Imports System.Data.Entity.ModelConfiguration
Imports DemoWebApp2.Models

Namespace EntityConfigs
    Public Class JobTitleEntityConfig
        Inherits EntityTypeConfiguration(Of JobTitle)

        Public Sub EmployeeEntityConfig()
            Me.Property(Function(p) p.Code).HasMaxLength(20).IsRequired()
            Me.Property(Function(p) p.Name).HasMaxLength(200).IsRequired()
            Me.Property(Function(p) p.Description).HasMaxLength(500)
        End Sub
    End Class
End Namespace
