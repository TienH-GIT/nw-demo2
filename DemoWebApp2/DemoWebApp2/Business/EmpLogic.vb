Imports System.Data.Entity
Imports DemoWebApp2.DAL
Imports DemoWebApp2.Models

Namespace Business
    Public Class EmpLogic
        Private _db As EmployeeContext

        Public Overridable Property Employee As Employee

#Region "Public method"
        Public Sub New(ByRef db As EmployeeContext)
            Me._db = db
        End Sub

        Public Sub New(ByRef db As EmployeeContext, ByVal empId As Integer)
            Me._db = db

            ' Load employee info
            LoadEmployee(empId)
        End Sub

        ''' <summary>
        ''' Check input code is exists in DB or not
        ''' </summary>
        ''' <param name="Code"></param>
        ''' <returns></returns>
        Public Function IsExistedCode(ByVal Code As String) As Boolean
            Dim validateCode = _db.Employees.FirstOrDefault(Function(x) x.Code.Equals(Code))
            If validateCode IsNot Nothing Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' Get structure for Employee ViewModdel
        ''' <para>+ List all data of Employee</para>
        ''' <para>+ Init CsvFile</para>
        ''' </summary>
        ''' <returns></returns>
        Public Function GetEmpViewModel() As EmpViewModel
            Dim empVMD As EmpViewModel = New EmpViewModel()
            empVMD.Employees = _db.Employees.ToList()
            empVMD.CsvFile = New CsvFile
            Return empVMD
        End Function

        ''' <summary>
        ''' Get all data of Employee
        ''' </summary>
        ''' <returns></returns>
        Public Function GetListEmp() As List(Of Employee)
            Return _db.Employees.ToList()
        End Function

        ''' <summary>
        ''' Get all data of Branch for binding Dropdownlist
        ''' </summary>
        ''' <returns>List of object</returns>
        Public Function GetListBranchBindCbx() As Object
            Dim ListBranch As Object

            If Employee IsNot Nothing Then
                ListBranch = New SelectList(LoadBranches(), "BranchID", "Name", Employee.BranchID)
            End If
            ListBranch = _db.Branches.[Select](Function(m) New SelectListItem() With {
                            .Text = m.Name,
                            .Value = m.BranchID})

            Return ListBranch
        End Function

        ''' <summary>
        ''' Get all data of JobTitle for binding Dropdownlist
        ''' </summary>
        ''' <returns>List object</returns>
        Public Function GetListJobTitleBindCbx() As Object
            Dim ListJobTitle As Object

            If Employee IsNot Nothing Then
                ListJobTitle = New SelectList(LoadJobTitles(), "JobTitleID", "Name", Employee.JobTitleID)
            End If
            ListJobTitle = _db.JobTitles.[Select](Function(m) New SelectListItem() With {
                            .Text = m.Name,
                            .Value = m.JobTitleID})

            Return ListJobTitle
        End Function

        ''' <summary>
        ''' Get Employee info by Id
        ''' </summary>
        ''' <param name="empId"></param>
        ''' <returns></returns>
        Public Function FindEmp(empId As Integer) As Employee
            LoadEmployee(empId)
            Return Me.Employee
        End Function

        ''' <summary>
        ''' Get Branch info by Name
        ''' </summary>
        ''' <param name="branchName"></param>
        ''' <returns></returns>
        Public Function FindBranchByName(branchName As String) As Branch
            Return _db.Branches.Where(Function(p) p.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase)).First()
        End Function

        ''' <summary>
        ''' Get JobTitle info by Name
        ''' </summary>
        ''' <param name="jobName"></param>
        ''' <returns></returns>
        Public Function FindJobTitleByName(jobName As String) As JobTitle
            Return _db.JobTitles.Where(Function(p) p.Name.Equals(jobName, StringComparison.OrdinalIgnoreCase)).First()
        End Function

        ''' <summary>
        ''' Add new Employee and PersonalInfo as related info
        ''' </summary>
        ''' <param name="emp"></param>
        ''' <param name="empDetail"></param>
        Public Sub AddEmp(emp As Employee, empDetail As PersonalInfo)
            Try
                ' Add Employee
                _db.Employees.Add(emp)
                _db.SaveChanges()

                ' Add PersonalInfo
                empDetail.PersonalInfoID = emp.ID
                _db.PersonalInfoes.Add(empDetail)
                _db.SaveChanges()
            Catch dex As DataException
                Throw New DataException(dex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Update Employee and PersonalInfo
        ''' </summary>
        ''' <param name="emp"></param>
        ''' <param name="empDetail"></param>
        Public Sub UpdateEmp(emp As Employee, empDetail As PersonalInfo)
            Try
                _db.Entry(emp).State = EntityState.Modified
                _db.Entry(empDetail).State = EntityState.Modified
                _db.SaveChanges()
            Catch dex As DataException
                Throw New DataException(dex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Delete Employee by Id
        ''' </summary>
        ''' <param name="empId"></param>
        Public Sub DeleteEmp(empId As Integer)
            Try
                Me.Employee = Nothing

                ' Delete sub content first
                Dim empDetail As PersonalInfo = FindEmpDetail(empId)
                _db.PersonalInfoes.Remove(empDetail)

                ' Delete parent info
                Dim emp As Employee = FindEmp(empId)
                _db.Employees.Remove(emp)

                _db.SaveChanges()
            Catch dex As DataException
                Throw New DataException(dex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Do action to import Employee data
        ''' </summary>
        ''' <param name="empList"></param>
        Public Sub DoImportEmp(empList As List(Of EmpImportModel))
            empList.ForEach(
                Function(p)
                    AddEmp(p.Employee, p.EmpDetail)
                    Return True
                End Function
            )
        End Sub
#End Region

#Region "Private method"
        ''' <summary>
        ''' Find Employee info then bind to internal class object
        ''' </summary>
        ''' <param name="empId"></param>
        Private Sub LoadEmployee(empId As Integer)
            Me.Employee = _db.Employees.Find(empId)
        End Sub

        Public Function FindEmpDetail(empId As Integer) As PersonalInfo
            Return _db.PersonalInfoes.Find(empId)
        End Function

        Private Function LoadBranches() As ICollection(Of Branch)
            Return _db.Branches.ToList()
        End Function

        Private Function LoadJobTitles() As ICollection(Of JobTitle)
            Return _db.JobTitles.ToList()
        End Function
    End Class
#End Region

End Namespace
