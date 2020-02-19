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
    Private headers As String() = {"親_名前", "親_性別", "親_メールアドレス", "子1_名前", "子1_性別", "子1_生年月日", "子2_名前", "子2_性別", "子2_生年月日"}

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
    Public EmpList As List(Of Employee) = New List(Of Employee)()


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

        IsValid = ErrorMessageList.Count > 0
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

            If headers(i) IsNot csvHeader(i) Then
                IsValid = False
                ErrorMessageList.Add("ヘッダー[ " & headers(i) & " ]の値が一致しません。")
            End If
        Next
    End Sub


    ''' <summary>
    ''' 1行分 の CSV を検証し、Parent および Child インスタンスを生成して ParentList プロパティに追加します。
    ''' 検証 NG の場合はエラーメッセージをプロパティに追加します。
    ''' 検証 NG の場合でもインスタンスを生成しますが、内容は保証されません。
    ''' </summary>
    ''' <param name="rowNumber">行番号</param>
    ''' <param name="rowContent">1 行分の CSV 要素配列</param>
    Private Sub ValidateLineAndMakeEmp(ByVal rowNumber As Integer, ByVal rowContent As String())
        Dim columnNumber As Integer = 0
        Dim emp = New Employee
        ' 親_名前
        emp.Code = ValidateAndGetRequiredCode(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        ' 親_性別
        emp.FirstName = ValidateAndGetRequiredString(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        ' 親_メールアドレス
        emp.LastName = ValidateAndGetRequiredString(rowNumber, Math.Min(Threading.Interlocked.Increment(columnNumber), columnNumber - 1), rowContent)
        Dim childCount = GetChildCountInRow(rowContent.Length)

        EmpList.Add(emp)
    End Sub


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
    Private Function ValidateAndGetRequiredCode(ByVal rowNumber As Integer, ByVal columnNumber As Integer, ByVal rowContent As String()) As Integer
        Dim value = GetStringOrNull(rowContent, columnNumber)

        If String.IsNullOrEmpty(value) Then
            ErrorMessageList.Add(rowNumber & "行目 : [ " & headers(columnNumber) & " ] は必須です。")
            Return 0
        End If

        If empLogic.IsExistedCode(value) Then
            ErrorMessageList.Add(rowNumber & "行目 : [ " & headers(columnNumber) & " ] の入力値は誤っています。")
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

