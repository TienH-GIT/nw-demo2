Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports DemoWebApp2.DAL
Imports DemoWebApp2.Models

Namespace Controllers
    Public Class ProjectEmployeeController
        Inherits System.Web.Mvc.Controller

        Private db As New EmployeeContext

        ' GET: ProjectEmployee
        Function Index() As ActionResult
            Dim projectEmployees = db.ProjectEmployees.Include(Function(p) p.Employee).Include(Function(p) p.Project)
            Return View(projectEmployees.ToList())
        End Function

        ' GET: ProjectEmployee/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim projectEmployee As ProjectEmployee = db.ProjectEmployees.Find(id)
            If IsNothing(projectEmployee) Then
                Return HttpNotFound()
            End If
            Return View(projectEmployee)
        End Function

        ' GET: ProjectEmployee/Create
        Function Create() As ActionResult
            ViewBag.EmployeeID = New SelectList(db.Employees, "ID", "FullName")
            ViewBag.ProjectID = New SelectList(db.Projects, "ProjectID", "Name")
            Return View()
        End Function

        ' POST: ProjectEmployee/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ProjectID,EmployeeID,Role,JoinDate")> ByVal projectEmployee As ProjectEmployee) As ActionResult
            If ModelState.IsValid Then
                db.ProjectEmployees.Add(projectEmployee)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.EmployeeID = New SelectList(db.Employees, "ID", "FullName", projectEmployee.EmployeeID)
            ViewBag.ProjectID = New SelectList(db.Projects, "ProjectID", "Name", projectEmployee.ProjectID)
            Return View(projectEmployee)
        End Function

        ' GET: ProjectEmployee/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim projectEmployee As ProjectEmployee = db.ProjectEmployees.Find(id)
            If IsNothing(projectEmployee) Then
                Return HttpNotFound()
            End If
            ViewBag.EmployeeID = New SelectList(db.Employees, "ID", "FullName", projectEmployee.EmployeeID)
            ViewBag.ProjectID = New SelectList(db.Projects, "ProjectID", "Name", projectEmployee.ProjectID)
            Return View(projectEmployee)
        End Function

        ' POST: ProjectEmployee/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ProjectID,EmployeeID,Role,JoinDate")> ByVal projectEmployee As ProjectEmployee) As ActionResult
            If ModelState.IsValid Then
                db.Entry(projectEmployee).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.EmployeeID = New SelectList(db.Employees, "ID", "FullName", projectEmployee.EmployeeID)
            ViewBag.ProjectID = New SelectList(db.Projects, "ProjectID", "Name", projectEmployee.ProjectID)
            Return View(projectEmployee)
        End Function

        ' GET: ProjectEmployee/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim projectEmployee As ProjectEmployee = db.ProjectEmployees.Find(id)
            If IsNothing(projectEmployee) Then
                Return HttpNotFound()
            End If
            Return View(projectEmployee)
        End Function

        ' POST: ProjectEmployee/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim projectEmployee As ProjectEmployee = db.ProjectEmployees.Find(id)
            db.ProjectEmployees.Remove(projectEmployee)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
