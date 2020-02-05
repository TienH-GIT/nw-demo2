Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Description
Imports DemoWebApp2.DAL
Imports DemoWebApp2.Models

Namespace Controllers.API
    Public Class EmpController
        Inherits ApiController

        Private db As New EmployeeContext

        ' GET: api/Emp
        Function GetEmployees() As IQueryable(Of Employee)
            Return db.Employees
        End Function

        ' GET: api/Emp/5
        <ResponseType(GetType(Employee))>
        Function GetEmployee(ByVal id As Integer) As IHttpActionResult
            Dim employee As Employee = db.Employees.Find(id)
            If IsNothing(employee) Then
                Return NotFound()
            End If

            Return Ok(employee)
        End Function

        ' PUT: api/Emp/5
        <ResponseType(GetType(Void))>
        Function PutEmployee(ByVal id As Integer, ByVal employee As Employee) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            If Not id = employee.ID Then
                Return BadRequest()
            End If

            db.Entry(employee).State = EntityState.Modified

            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                If Not (EmployeeExists(id)) Then
                    Return NotFound()
                Else
                    Throw
                End If
            End Try

            Return StatusCode(HttpStatusCode.NoContent)
        End Function

        ' POST: api/Emp
        <ResponseType(GetType(Employee))>
        Function PostEmployee(ByVal employee As Employee) As IHttpActionResult
            If Not ModelState.IsValid Then
                Return BadRequest(ModelState)
            End If

            db.Employees.Add(employee)
            db.SaveChanges()

            Return CreatedAtRoute("DefaultApi", New With {.id = employee.ID}, employee)
        End Function

        ' DELETE: api/Emp/5
        <ResponseType(GetType(Employee))>
        Function DeleteEmployee(ByVal id As Integer) As IHttpActionResult
            Dim employee As Employee = db.Employees.Find(id)
            If IsNothing(employee) Then
                Return NotFound()
            End If

            db.Employees.Remove(employee)
            db.SaveChanges()

            Return Ok(employee)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Function EmployeeExists(ByVal id As Integer) As Boolean
            Return db.Employees.Count(Function(e) e.ID = id) > 0
        End Function
    End Class
End Namespace