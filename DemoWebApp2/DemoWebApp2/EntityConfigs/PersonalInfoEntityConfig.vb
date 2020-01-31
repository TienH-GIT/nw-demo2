Imports System.Data.Entity.ModelConfiguration
Imports DemoWebApp2.Models

Namespace EntityConfigs
    Public Class PersonalInfoEntityConfig
        Inherits EntityTypeConfiguration(Of PersonalInfo)

        Public Sub EmployeeEntityConfig()
            Me.Property(Function(p) p.Birthday).IsOptional()
            Me.Property(Function(p) p.Age).IsOptional()
            Me.Property(Function(p) p.Address).HasMaxLength(300)
            Me.Property(Function(p) p.Hobby).HasMaxLength(200)
        End Sub
    End Class
End Namespace
