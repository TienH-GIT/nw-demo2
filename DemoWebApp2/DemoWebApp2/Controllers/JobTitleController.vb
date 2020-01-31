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
    Public Class JobTitleController
        Inherits System.Web.Mvc.Controller

        Private db As New EmployeeContext

        ' GET: JobTitle
        Function Index() As ActionResult
            Return View(db.JobTitles.ToList())
        End Function

        ' GET: JobTitle/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim jobTitle As JobTitle = db.JobTitles.Find(id)
            If IsNothing(jobTitle) Then
                Return HttpNotFound()
            End If
            Return View(jobTitle)
        End Function

        ' GET: JobTitle/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: JobTitle/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="JobTitleID,Code,Name,Description")> ByVal jobTitle As JobTitle) As ActionResult
            If ModelState.IsValid Then
                db.JobTitles.Add(jobTitle)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(jobTitle)
        End Function

        ' GET: JobTitle/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim jobTitle As JobTitle = db.JobTitles.Find(id)
            If IsNothing(jobTitle) Then
                Return HttpNotFound()
            End If
            Return View(jobTitle)
        End Function

        ' POST: JobTitle/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="JobTitleID,Code,Name,Description")> ByVal jobTitle As JobTitle) As ActionResult
            If ModelState.IsValid Then
                db.Entry(jobTitle).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(jobTitle)
        End Function

        ' GET: JobTitle/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim jobTitle As JobTitle = db.JobTitles.Find(id)
            If IsNothing(jobTitle) Then
                Return HttpNotFound()
            End If
            Return View(jobTitle)
        End Function

        ' POST: JobTitle/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim jobTitle As JobTitle = db.JobTitles.Find(id)
            db.JobTitles.Remove(jobTitle)
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
