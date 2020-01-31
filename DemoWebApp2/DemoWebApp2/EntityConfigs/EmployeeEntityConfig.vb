Imports System.Data.Entity.ModelConfiguration
Imports DemoWebApp2.Models

Namespace EntityConfigs
    Public Class EmployeeEntityConfig
        Inherits EntityTypeConfiguration(Of Employee)

        Public Sub EmployeeEntityConfig()
            Me.Property(Function(p) p.Code).HasMaxLength(20).IsRequired()
            Me.Property(Function(p) p.FirstName).HasMaxLength(100).IsRequired()
            Me.Property(Function(p) p.LastName).HasMaxLength(100)
            Me.Property(Function(p) p.StartDate).IsOptional()
            Me.Property(Function(p) p.RetireDate).IsOptional()
        End Sub
    End Class
End Namespace
