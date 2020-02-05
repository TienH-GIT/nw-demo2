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
    Public Class ProjectController
        Inherits System.Web.Mvc.Controller

        Private db As New EmployeeContext

        ' GET: Project
        Function Index() As ActionResult
            Dim projects = db.Projects.Include(Function(p) p.Department)
            Return View(projects.ToList())
        End Function

        ' GET: Project/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim project As Project = db.Projects.Find(id)
            If IsNothing(project) Then
                Return HttpNotFound()
            End If
            Return View(project)
        End Function

        ' GET: Project/Create
        Function Create() As ActionResult
            ViewBag.DepartmentID = New SelectList(db.Departments, "DepartmentID", "Name")
            Return View()
        End Function

        ' POST: Project/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ProjectID,Code,Name,Description,ScopeType,Scale,Duration,PlanStartDate,PlanEndDate,StartDate,EndDate,DepartmentID")> ByVal project As Project) As ActionResult
            If ModelState.IsValid Then
                db.Projects.Add(project)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.DepartmentID = New SelectList(db.Departments, "DepartmentID", "Name", project.DepartmentID)
            Return View(project)
        End Function

        ' GET: Project/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim project As Project = db.Projects.Find(id)
            If IsNothing(project) Then
                Return HttpNotFound()
            End If
            ViewBag.DepartmentID = New SelectList(db.Departments, "DepartmentID", "Name", project.DepartmentID)
            Return View(project)
        End Function

        ' POST: Project/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ProjectID,Code,Name,Description,ScopeType,Scale,Duration,PlanStartDate,PlanEndDate,StartDate,EndDate,DepartmentID")> ByVal project As Project) As ActionResult
            If ModelState.IsValid Then
                db.Entry(project).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.DepartmentID = New SelectList(db.Departments, "DepartmentID", "Name", project.DepartmentID)
            Return View(project)
        End Function

        ' GET: Project/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim project As Project = db.Projects.Find(id)
            If IsNothing(project) Then
                Return HttpNotFound()
            End If
            Return View(project)
        End Function

        ' POST: Project/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim project As Project = db.Projects.Find(id)
            db.Projects.Remove(project)
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
