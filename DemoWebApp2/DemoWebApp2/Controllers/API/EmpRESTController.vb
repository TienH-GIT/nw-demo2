Imports System.Net
Imports System.Web.Http
Imports DemoWebApp2.Business
Imports DemoWebApp2.DAL
Imports DemoWebApp2.Models

Namespace Controllers.API
    Public Class EmpRESTController
        Inherits ApiController

        Private db As New EmployeeContext
        Private empLogic As New EmpLogic(db)

        ' GET: api/Emp
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET: api/Emp/5
        Public Function GetValue(ByVal id As Integer) As Results.JsonResult(Of Employee)
            Dim emp As Employee = empLogic.FindEmp(id)

            Return Json(emp)
        End Function

        ' POST: api/Emp
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub

        ' PUT: api/Emp/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE: api/Emp/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class
End Namespace