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
    Public Class BranchController
        Inherits System.Web.Mvc.Controller

        Private db As New EmployeeContext

        ' GET: Branch
        Function Index() As ActionResult
            Return View(db.Branches.ToList())
        End Function

        ' GET: Branch/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim branch As Branch = db.Branches.Find(id)
            If IsNothing(branch) Then
                Return HttpNotFound()
            End If
            Return View(branch)
        End Function

        ' GET: Branch/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Branch/Create
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="BranchID,Code,Name,KataName,Address,Description")> ByVal branch As Branch) As ActionResult
            If ModelState.IsValid Then
                db.Branches.Add(branch)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(branch)
        End Function

        ' GET: Branch/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim branch As Branch = db.Branches.Find(id)
            If IsNothing(branch) Then
                Return HttpNotFound()
            End If
            Return View(branch)
        End Function

        ' POST: Branch/Edit/5
        '過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        '詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="BranchID,Code,Name,KataName,Address,Description")> ByVal branch As Branch) As ActionResult
            If ModelState.IsValid Then
                db.Entry(branch).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(branch)
        End Function

        ' GET: Branch/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim branch As Branch = db.Branches.Find(id)
            If IsNothing(branch) Then
                Return HttpNotFound()
            End If
            Return View(branch)
        End Function

        ' POST: Branch/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim branch As Branch = db.Branches.Find(id)
            db.Branches.Remove(branch)
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
