Imports System.Data.Entity
Imports System.Net
Imports DemoWebApp2.DAL
Imports DemoWebApp2.Business
Imports DemoWebApp2.Models

Namespace Controllers
    Public Class EmployeeController
        Inherits System.Web.Mvc.Controller

        Private db As New EmployeeContext
        Private empLogic As New EmpLogic(db)

        ' GET: /Employee/
        Function Index() As ActionResult
            ViewData("Message") = "List All members"

            Return View(empLogic.GetEmpViewModel)
        End Function

        ' GET: /Employee/Details/1
        Function Detail(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim emp As Employee = empLogic.FindEmp(id)

            If IsNothing(emp) Then
                Return HttpNotFound()
            End If

            Return View(emp)
        End Function

        ' GET: /Employee/Info/1
        Function EmpInfo(ByVal id As Integer?) As ActionResult
            Dim emp As Employee = empLogic.FindEmp(id)

            If IsNothing(emp) Then
                Return HttpNotFound()
            End If

            Return PartialView("EmpInfo", emp)
        End Function

        ' GET: /Employee/Create
        Function Create() As ActionResult
            'Dim vmEmp As ViewModelEmp = New ViewModelEmp(Me.db)

            empLogic.Employee = New Employee
            LoadDataForDropdown()

            Return View()
        End Function

        ' POST: /Employee/Create
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Code,FirstName,LastName,BranchID,JobTitleID")> ByVal emp As Employee,
                        <Bind(Include:="Gender,Birthday,Age,Address,Status,Hobby")> ByVal detail As PersonalInfo) As ActionResult
            Try
                If ModelState.IsValid Then
                    empLogic.AddEmp(emp, detail)

                    Return RedirectToAction("Index")
                End If
            Catch dex As DataException
                'Log the error (add a line here to write a log)
                ModelState.AddModelError("", "Unable to save changes. Please confirm and try again later. ")
            End Try

            LoadDataForDropdown()
            Return View(emp)
        End Function

        ' GET: /Employee/Edit/1
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim emp As Employee = empLogic.FindEmp(id)
            LoadDataForDropdown()

            If IsNothing(emp) Then
                Return HttpNotFound()
            End If

            Return View(emp)
        End Function

        ' POST: /Employee/Edit/1
        <HttpPost(), ActionName("Edit")>
        <ValidateAntiForgeryToken()>
        Function EditPost(ByVal id? As Integer) As ActionResult
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim empForUpd = empLogic.FindEmp(id)
            Dim detailForUpd = empLogic.FindEmpDetail(id)
            If TryUpdateModel(empForUpd, "", New String() {
                              "Code", "FirstName", "LastName", "BranchID", "JobTitleID"}) And
                TryUpdateModel(detailForUpd, "Detail", New String() {
                              "Gender", "Birthday", "Age", "Address", "Status", "Hobby"}) Then
                Try
                    empLogic.UpdateEmp(empForUpd, detailForUpd)

                    Return RedirectToAction("Index")
                Catch Dex As DataException
                    'Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Please confirm and try again later.")
                End Try
            End If

            LoadDataForDropdown()
            Return View(empForUpd)
        End Function

        ' GET: /Employee/Delete/1
        Function Delete(ByVal id As Integer?, Optional ByVal saveChangesError As Boolean? = False) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            If saveChangesError.GetValueOrDefault() Then
                ViewBag.Message = "Delete failed. Try again, and if the problem persists see your system administrator."
            End If
            Dim emp As Employee = empLogic.FindEmp(id)
            If IsNothing(emp) Then
                Return HttpNotFound()
            End If
            Return View(emp)
        End Function

        ' POST: /Employee/Delete/1
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function Delete(ByVal id As Integer) As ActionResult
            Try
                empLogic.DeleteEmp(id)
            Catch dex As DataException
                'Log the error (uncomment dex variable name and add a line here to write a log.
                Return RedirectToAction("Delete", New With {.id = id, .saveChangesError = True})
            End Try
            Return RedirectToAction("Index")
        End Function

        ' POST: /Employee/Export
        <HttpPost(), ActionName("Export")>
        <ValidateAntiForgeryToken()>
        Function ExportDownloaded() As ActionResult
            ' DB からデータ取得
            Dim empList = empLogic.GetListEmp()

            ' CSV 内容生成
            Dim csvString = EmpCsvExpSrv.CreateCsv(empList)

            ' クライアントにダウンロードさせる形で CSV 出力
            Dim fileName = String.Format("従業員情報_{0}.csv", Date.Now.ToString("yyyyMMddHHmmss"))
            ' IE で全角が文字化けするため、ファイル名を UTF-8 でエンコーディング
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", HttpUtility.UrlEncode(fileName, Encoding.UTF8)))

            Return Content(csvString, "text/csv", Encoding.GetEncoding("Shift_JIS"))
        End Function

        ' GET: /Employee/Import
        Public Function Import() As ActionResult
            Return PartialView("EmpImport")
        End Function

        ' POST: /Employee/Import
        <HttpPost(), ActionName("Import")>
        <ValidateAntiForgeryToken()>
        Public Function Import(ByVal file As CsvFile) As ActionResult
            ' ファイルチェック
            If Not ModelState.IsValid Then
                Return View()
            End If

            ' ファイル読み込み
            Dim csvImpSrv As EmpCsvImpSrv = New EmpCsvImpSrv(file.UploadFile.InputStream)

            ' バリデーション
            If Not csvImpSrv.IsValid Then
                ViewBag.ErrorMessageList = csvImpSrv.ErrorMessageList
                Return View()
            End If

            ' モデル取得
            Dim empList As List(Of Employee) = csvImpSrv.EmpList

            ' DB 登録
            'empList.ForEach(Function(p) db.Parents.Add(p))
            'db.SaveChanges()
            ViewBag.SuccessMessage = "インポートに成功しました。"
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        ''' <summary>
        ''' Get data for binding dropdownlist
        ''' </summary>
        Private Sub LoadDataForDropdown()
            ' Get list Branch
            ViewBag.model1 = empLogic.GetListBranchBindCbx
            ' Get list JobTitle
            ViewBag.model2 = empLogic.GetListJobTitleBindCbx
        End Sub
    End Class
End Namespace