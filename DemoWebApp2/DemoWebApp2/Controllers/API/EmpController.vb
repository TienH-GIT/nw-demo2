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

        ' IMPORT: api/Emp/Import
        <Route("api/Emp/Import")>
        <HttpPost>
        <ValidateAntiForgeryToken()>
        Public Function Import()
            Dim result As HttpResponseMessage = Nothing
            Dim HttpRequest = HttpContext.Current.Request
            If (HttpRequest.Files.Count <= 0) Then
                Return Request.CreateResponse(HttpStatusCode.BadRequest)
            End If

            For Each file As String In HttpRequest.Files
                Dim postedFile = HttpRequest.Files(file)

                Dim upFile As CsvFile = New CsvFile()
                upFile.UploadFile = New HttpPostedFileWrapper(postedFile)

                ' Manually validate the model
                Me.Validate(upFile)

                ' ファイルチェック
                If Not ModelState.IsValid Then
                    Dim errors = ModelState.Select(Function(x) x.Value.Errors) _
                        .Where(Function(y) y.Count > 0).ToList()
                    Return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState)
                End If

                ' ファイル読み込み
                Dim csvImpSrv As EmpCsvImpSrv = New EmpCsvImpSrv(upFile.UploadFile.InputStream)

                ' バリデーション
                If Not csvImpSrv.IsValid Then
                    Dim errs = New ModelBinding.ModelStateDictionary

                    csvImpSrv.ErrorMessageList.
                        Select(Function(msg, index) New With {Key .Index = index, .Msg = msg}).
                        ToList().
                        ForEach(
                            Sub(item)
                                Dim idx = item.Index + 1
                                errs.AddModelError(String.Format("Error {0}: ", idx), item.Msg)
                            End Sub
                        )

                    Return Request.CreateResponse(HttpStatusCode.BadRequest, errs)
                End If

                ' モデル取得
                Dim empList As List(Of EmpImportModel) = csvImpSrv.EmpList

                ' DB 登録
                'empList.ForEach(Function(p) db.Parents.Add(p))
                'db.SaveChanges()
                Dim SuccessMessage = "インポートに成功しました。"
                Return Ok(SuccessMessage)
            Next

            Return BadRequest()
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