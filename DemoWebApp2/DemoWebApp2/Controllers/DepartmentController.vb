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
    Public Class DepartmentController
        Inherits System.Web.Mvc.Controller

        Private db As New EmployeeContext

        ' GET: Department
        Function Index() As ActionResult
            Return View(db.Departments.ToList())
        End Function

        ' GET: Department/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim department As Department = db.Departments.Find(id)
            If IsNothing(department) Then
                Return HttpNotFound()
            End If
            Return View(department)
        End Function

        ' GET: Department/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Department/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="DepartmentID,Code,Name,BusinessAreas,Description")> ByVal department As Department) As ActionResult
            If ModelState.IsValid Then
                db.Departments.Add(department)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(department)
        End Function

        ' GET: Department/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim department As Department = db.Departments.Find(id)
            If IsNothing(department) Then
                Return HttpNotFound()
            End If
            Return View(department)
        End Function

        ' POST: Department/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="DepartmentID,Code,Name,BusinessAreas,Description")> ByVal department As Department) As ActionResult
            If ModelState.IsValid Then
                db.Entry(department).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(department)
        End Function

        ' GET: Department/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim department As Department = db.Departments.Find(id)
            If IsNothing(department) Then
                Return HttpNotFound()
            End If
            Return View(department)
        End Function

        ' POST: Department/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim department As Department = db.Departments.Find(id)
            db.Departments.Remove(department)
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
