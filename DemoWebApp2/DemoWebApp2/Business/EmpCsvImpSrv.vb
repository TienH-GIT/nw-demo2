Imports System.IO
Imports DemoWebApp2.Business
Imports DemoWebApp2.DAL
Imports DemoWebApp2.Models
Imports Microsoft.VisualBasic.FileIO

Public Class EmpCsvImpSrv
    Private db As EmployeeContext = Nothing
    Private empLogic As EmpLogic = Nothing

    ''' <summary>
    ''' CSV ファイルの最低行数
    ''' </summary>
    Private Const MIN_CSV_ROW_COUNT As Integer = 2

    ''' <summary>
    ''' CSV 1 行に含める Child の最大数
    ''' </summary>
    Private Const MAX_CHILD_COUNT As Integer = 2

    ''' <summary>
    ''' ヘッダー定義
    ''' </summary>
    Private headers As String() = {
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Code),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Branch.Name),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.JobTitle.Name),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.FullName),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Detail.Gender),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Detail.Birthday),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Detail.Age),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Detail.Address),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Detail.Status),
        ExtensionMethods.GetDisplayName(Of Employee)(Function(t) t.Detail.Hobby)
    }

    ''' <summary>
    ''' CSV 1行における Parent 1 つを構成するカラム数
    ''' </summary>
    Private Const ONE_PARENT_COLUMN_COUNT As Integer = 3

    ''' <summary>
    ''' CSV 1行における Child 1 つを構成するカラム数
    ''' </summary>
    Private Const ONE_CHILD_COLUMN_COUNT As Integer = 3

    ''' <summary>
    ''' 読みこんだ CSV のバリデーションが OK であることを示すフラグ
    ''' </summary>
    Public IsValid As Boolean = False

    ''' <summary>
    ''' エラーメッセージリスト
    ''' </summary>
    Public ErrorMessageList As List(Of String) = New List(Of String)()

    ''' <summary>
    ''' Parent モデルのリスト。IsValid プロパティが false の場合は内容の有効性が保証されない。
    ''' </summary>
    Public EmpList As List(Of EmpImportModel) = New List(Of EmpImportModel)()


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="inputStream">CSV ファイルの Stream</param>
    Public Sub New(ByVal inputStream As Stream)
        db = New EmployeeContext
        empLogic = New EmpLogic(db)

        Dim csvLines As IEnumerable(Of String()) = ReadCsv(inputStream)
        ' TODO 2回目の LINQ 取得がうまくいかないので for する。
        'var header = csvLines.FirstOrDefault();
        'var lineList = csvLines.Skip(0).ToList();
        Dim i As Integer = 0

        For Each csvLine In csvLines
            ' 1回目のみヘッダーチェック
            i += 1

            If i = 1 Then
                ValidateHeader(csvLine)
                Continue For
            End If


            ' 2回目以降はチェックおよびオブジェクト生成
            ValidateLineAndMakeEmp(i, csvLine)
        Next


        ' 2回目以降はチェックおよびオブジェクト生成
        If i < MIN_CSV_ROW_COUNT Then
            ErrorMessageList.Add("読み込むデータがありません。")
        End If

        IsValid = ErrorMessageList.Count <= 0
    End Sub


    ''' <summary>
    ''' CSV の Stream を読み込み、string 配列の列挙子を返却します。
    ''' </summary>
    ''' <param name="stream">CSV ファイルの Stream</param>
    ''' <returns>各セルを string 配列の要素とし、1 行を 1 つの要素とした列挙子</returns>
    Private Iterator Function ReadCsv(ByVal stream As Stream) As IEnumerable(Of String())
        Using parser As TextFieldParser = New TextFieldParser(stream, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters({","})
            parser.HasFieldsEnclosedInQuotes = True

            While Not parser.EndOfData
                Dim fields As String() = parser.ReadFields()
                Yield fields
            End While
        End Using
    End Function


    ''' <summary>
    ''' CSV のヘッダーを検証し、NG の項目はエラーメッセージに追加します。
    ''' </summary>
    ''' <param name="csvHeader">CSV ヘッダー項目の配列</param>
    Private Sub ValidateHeader(ByVal csvHeader As String())
        ' 要素数チェック
        If headers.Length <> csvHeader.Length Then
            IsValid = False
            ErrorMessageList.Add("ヘッダーの要素数が一致しません。")
        End If

        ' 値チェック
        Dim max As Integer = csvHeader.Length

        For i As Integer = 0 To max - 1

            If Not headers(i).Equals(csvHeader(i), StringComparison.OrdinalIgnoreCase) Then
                IsValid = False
                ErrorMessageList.Add("ヘッダー[ " & headers(i) & " ]の値が一致しません。")
            End If
        Next
    End Sub


    ''' <summary>
    ''' 1行分 の CSV を検証し、データインスタンスを生成して 従業員 プロパティに追加します。
    ''' 検証 NG の場合はエラーメッセージをプロパティに追加します。
    ''' 検証 NG の場合でもインスタンスを生成しますが、内容は保証されません。
    ''' </summary>
    ''' <param name="rowNumber">行番号</param>
    ''' <param name="rowContent">1 行分の CSV 要素配列</param>
    Private Sub ValidateLineAndMakeEmp(ByVal rowNumber As Integer, ByVal rowContent As String())
        Dim columnNumber As Integer = 0

        '*** 従業員の情報 ***'
        Dim emp = BindEmpInfo(rowNumber, rowContent, columnNumber)

        '*** 詳細の情報 ***'
        Dim empDtl = BindEmpDetailInfo(rowNumber, rowContent, columnNumber)

        '*** データを登録 ***'
        Dim empImp = New EmpImportModel With {
            .Employee = emp,
            .EmpDetail = empDtl
        }
        EmpList.Add(empImp)
    End Sub


    ''' <summary>
    ''' Bind Employee infos
    ''' </summary>
    ''' <param name="rowNumber"></param>
    ''' <param name="rowContent"></param>
    ''' <param name="columnNumber"></param>
    ''' <returns></returns>
    Private Function BindEmpInfo(ByVal rowNumber As Integer, ByVal rowContent As String(), ByRef columnNumber As Integer)
        Dim emp = New Employee

        ' 従業員コード
        emp.Code = ValidateAndGetRequiredCode(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        ' 支店ID
        Dim branchId = ValidateAndGetRequiredString(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        If IsNumeric(branchId) Then
            emp.BranchID = branchId
        Else
            emp.BranchID = empLogic.FindBranchByName(branchId).BranchID
        End If
        ' 役職ID
        Dim jobId = ValidateAndGetRequiredString(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        If IsNumeric(jobId) Then
            emp.JobTitleID = jobId
        Else
            emp.JobTitleID = empLogic.FindJobTitleByName(jobId).JobTitleID
        End If
        ' 姓名
        Dim fullName = ValidateAndGetRequiredString(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        Dim firstName As String = fullName.Substring(0, fullName.IndexOf(" "))
        Dim lastName As String = fullName.Substring(fullName.IndexOf(" ") + 1)
        ' 従業員の名
        emp.FirstName = firstName
        ' 従業員の姓
        emp.LastName = lastName

        Return emp
    End Function


    ''' <summary>
    ''' Bind Personal infos
    ''' </summary>
    ''' <param name="rowNumber"></param>
    ''' <param name="rowContent"></param>
    ''' <param name="columnNumber"></param>
    ''' <returns></returns>
    Private Function BindEmpDetailInfo(ByVal rowNumber As Integer, ByVal rowContent As String(), ByRef columnNumber As Integer)
        Dim empDtl = New PersonalInfo

        ' 性別
        Dim gender = GetStringOrNull(rowContent, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1))
        empDtl.Gender = DirectCast([Enum].Parse(GetType(GenderEnum), gender), GenderEnum)
        ' 誕生日
        empDtl.Birthday = ValidateAndGetRequiredDateTime(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        ' 年齢
        empDtl.Age = GetStringOrNull(rowContent, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1))
        ' 住所
        empDtl.Address = GetStringOrNull(rowContent, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1))
        ' 個人状態
        Dim status = GetStringOrNull(rowContent, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1))
        empDtl.Status = DirectCast([Enum].Parse(GetType(StatusEnum), status), StatusEnum)
        ' 趣味
        empDtl.Hobby = GetStringOrNull(rowContent, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1))

        Return empDtl
    End Function


    ''' <summary>
    ''' 列番号に対応した要素の値を検証し、OK の場合は値をそのまま返却します。
    ''' 検証 NG の場合は行数を含めてエラーメッセージをプロパティに追加し、null を返却します。
    ''' </summary>
    ''' <param name="rowNumber">行数</param>
    ''' <param name="columnNumber">列番号</param>
    ''' <param name="rowContent">1 行分の CSV 要素配列</param>
    ''' <returns>列番号に対応した要素の値。検証 NG の場合は null</returns>
    Private Function ValidateAndGetRequiredString(ByVal rowNumber As Integer, ByVal columnNumber As Integer, ByVal rowContent As String()) As String
        Dim value = GetStringOrNull(rowContent, columnNumber)

        If String.IsNullOrEmpty(value) Then
            ErrorMessageList.Add(rowNumber & "行目 : [ " & headers(columnNumber) & " ] は必須です。")
            Return ""
        End If

        Return value
    End Function


    ''' <summary>
    ''' インデックスに対応した配列の要素を返却します。
    ''' インデックスが配列の要素数の範囲外の場合は null を返却します。
    ''' </summary>
    ''' <param name="array">string の配列</param>
    ''' <param name="index">配列のインデックス</param>
    ''' <returns>インデックスに対応した配列の要素。無い場合は null</returns>
    Private Function GetStringOrNull(ByVal array As String(), ByVal index As Integer) As String
        If array.Length <= index Then
            Return Nothing
        End If

        Return array(index)
    End Function


    ''' <summary>
    ''' 列番号に対応した要素の値を検証し、OK の場合は要素の値に対応する SexId を返却します。
    ''' </summary>
    ''' <param name="rowNumber">行数</param>
    ''' <param name="columnNumber">列番号</param>
    ''' <param name="rowContent">1 行分の CSV 要素配列</param>
    ''' <returns>SexId。検証 NG の場合は 0</returns>
    Private Function ValidateAndGetRequiredCode(ByVal rowNumber As Integer, ByVal columnNumber As Integer, ByVal rowContent As String()) As String
        Dim value = GetStringOrNull(rowContent, columnNumber)

        If String.IsNullOrEmpty(value) Then
            ErrorMessageList.Add(rowNumber & "行目 : [ " & headers(columnNumber) & " ] は必須です。")
            Return 0
        End If

        If empLogic.IsExistedCode(value) Then
            ErrorMessageList.Add(rowNumber & "行目 : [ " & headers(columnNumber) & " ] の入力値は誤っています（重複）。")
            Return 0
        End If

        Return value
    End Function


    ''' <summary>
    ''' CSV 1 行に含まれる子の数を算出します。
    ''' 1 行の要素数が少ない場合、この数はマイナスや 0 となる場合があります。
    ''' </summary>
    ''' <param name="rowColumnCount">CSV  1 行の要素数</param>
    ''' <returns>CSV 1 行に含まれる子の数。</returns>
    Private Function GetChildCountInRow(ByVal rowColumnCount As Integer) As Integer
        Return (rowColumnCount - ONE_PARENT_COLUMN_COUNT) / ONE_CHILD_COLUMN_COUNT
    End Function


    ''' <summary>
    ''' 列番号に対応した要素の値を検証し、OK の場合は要素の値に対応する DateTime を返却します。
    ''' </summary>
    ''' <param name="rowNumber">行数</param>
    ''' <param name="columnNumber">列番号</param>
    ''' <param name="rowContent">1 行分の CSV 要素配列</param>
    ''' <returns>DateTime に変換した年月日。検証 NG の場合は DateTime.MinValue</returns>
    Private Function ValidateAndGetRequiredDateTime(ByVal rowNumber As Integer, ByVal columnNumber As Integer, ByVal rowContent As String()) As Date
        Dim value = GetStringOrNull(rowContent, columnNumber)

        If String.IsNullOrEmpty(value) Then
            ErrorMessageList.Add(rowNumber & "行目 : [ " & headers(columnNumber) & " ] は必須です。")
            Return Date.MinValue
        End If

        Dim result As Date

        If Not Date.TryParse(value, result) Then
            ErrorMessageList.Add(rowNumber & "行目 : [ " & headers(columnNumber) & " ] の入力値は誤っています。")
        End If

        Return result
    End Function
End Class

