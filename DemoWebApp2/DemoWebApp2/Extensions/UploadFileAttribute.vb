' アップロードされたファイルの種類とサイズを検証する属性
Imports System.ComponentModel.DataAnnotations
Imports System.IO

<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False)>
Public NotInheritable Class UploadFileAttribute
    Inherits ValidationAttribute

    Public Sub New()
        ' 最大のファイルサイズは 2147483647 バイト (= 2 ギガバイト)
        MaxLength = Integer.MaxValue

        ' デフォルトのエラーメッセージを設定する
        ErrorMessage = "ファイル形式が不正です。"
    End Sub

    ' 許可する最大ファイルサイズ
    Public Property MaxLength As Integer

    ' 許可する拡張子
    Public Property Extensions As String

    Public Overrides Function IsValid(ByVal value As Object) As Boolean
        ' 値が null の時には何もしない
        If value Is Nothing Then
            Return True
        End If

        ' 値がアップロードされたファイルか確認
        Dim postedFile = TryCast(value, HttpPostedFileBase)

        If postedFile Is Nothing Then
            Return True
        End If

        ' ファイルサイズを検証
        If postedFile.ContentLength > MaxLength Then
            ' MaxLength より大きいのでエラー
            Return False
        End If

        ' 拡張子を検証
        Dim ext = Path.GetExtension(postedFile.FileName).Replace(".", "")

        If Not String.IsNullOrEmpty(Extensions) AndAlso Not Extensions.Split(";"c).Any(Function(p) p Is ext) Then
            ' 許可されていない拡張子なのでエラー
            Return False
        End If

        ' 検証が成功
        Return True
    End Function
End Class
