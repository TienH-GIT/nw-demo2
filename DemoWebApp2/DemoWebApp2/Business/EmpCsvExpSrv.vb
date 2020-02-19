Imports DemoWebApp2.Models

Namespace Business
    Public Class EmpCsvExpSrv
        ''' <summary>
        ''' CSV セルの開始終了マーク
        ''' </summary>
        Private Const ENCLOSE_CHARACTER As String = """"

        ''' <summary>
        ''' CSV セル区切り文字
        ''' </summary>
        Private Const DELIMITER As String = ","

        ''' <summary>
        ''' CSV ヘッダー要素の配列
        ''' </summary>
        Private Shared ReadOnly HEADER_ARRAY As String() = New String() {
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
        ''' CSV ファイルデータの文字列を生成して返却します。
        ''' </summary>
        ''' <param name="empList">Employee オブジェクトの List</param>
        ''' <returns></returns>
        Public Shared Function CreateCsv(ByVal empList As List(Of Employee)) As String
            Dim sb = New StringBuilder()
            sb.AppendLine(GetCsvHeader())
            empList.ForEach(Sub(p) sb.AppendLine(CreateCsvLine(p)))
            Return sb.ToString()
        End Function

        ''' <summary>
        ''' CSV ヘッダ定義文字列を返却します。
        ''' 改行文字は含みません。
        ''' </summary>
        ''' <returns>CSV ヘッダ定義文字列</returns>
        Private Shared Function GetCsvHeader() As String
            Dim sb = New StringBuilder()

            For Each v In HEADER_ARRAY
                sb.Append(ENCLOSE_CHARACTER).Append(v).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            Next

            Return sb.Remove(sb.Length - 1, 1).ToString()
        End Function

        ''' <summary>
        ''' CSV の 1 行の文字列を生成して返却します。
        ''' 改行文字は含みません。
        ''' </summary>
        ''' <param name="emp">Employee オブジェクト</param>
        ''' <returns></returns>
        Private Shared Function CreateCsvLine(ByVal emp As Employee) As String
            Dim sb = New StringBuilder()
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Code).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Branch.Name).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.JobTitle.Name).Append(ENCLOSE_CHARACTER).Append(DELIMITER)

            sb.Append(ENCLOSE_CHARACTER).Append(emp.FullName).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Detail.Gender).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Detail.Birthday).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Detail.Age).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Detail.Address).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Detail.Status).Append(ENCLOSE_CHARACTER).Append(DELIMITER)
            sb.Append(ENCLOSE_CHARACTER).Append(emp.Detail.Hobby).Append(ENCLOSE_CHARACTER).Append(DELIMITER)

            Return sb.Remove(sb.Length - 1, 1).ToString()
        End Function
    End Class
End Namespace
