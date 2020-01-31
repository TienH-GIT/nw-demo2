Imports System.Data.Entity
Imports DemoWebApp2.Models

Namespace DAL
    Public Class ProductInitializer
        Inherits DropCreateDatabaseIfModelChanges(Of EmployeeContext)
        Protected Overrides Sub Seed(ByVal context As EmployeeContext)
            'Dim emps = New List(Of Employee)() From {
            '    New Employee() With {
            '        .Code = "SDB-001",
            '        .FirstName = "John",
            '        .LastName = "Phil",
            '        .Gender = 0,
            '        .Age = 25,
            '        .Address = "US",
            '        .Career = "IT",
            '        .Hobby = "Soccer"
            '    },
            '    New Employee() With {
            '        .Code = "SDB-002",
            '        .FirstName = "Peterson",
            '        .LastName = "Evan",
            '        .Gender = 0,
            '        .Age = 27,
            '        .Address = "AUS",
            '        .Career = "IT",
            '        .Hobby = "Tennis"
            '    },
            '    New Employee() With {
            '        .Code = "SDB-003",
            '        .FirstName = "King",
            '        .LastName = "James",
            '        .Gender = 0,
            '        .Age = 33,
            '        .Address = "US",
            '        .Career = "IT",
            '        .Hobby = "Basket ball"
            '    },
            '    New Employee() With {
            '        .Code = "SDB-004",
            '        .FirstName = "Tim",
            '        .LastName = "Howard",
            '        .Gender = 0,
            '        .Age = 41,
            '        .Address = "England",
            '        .Career = "Player",
            '        .Hobby = "Soccer"
            '    },
            '    New Employee() With {
            '        .Code = "SDB-005",
            '        .FirstName = "Lin",
            '        .LastName = "Dan",
            '        .Gender = 0,
            '        .Age = 37,
            '        .Address = "China",
            '        .Career = "Player",
            '        .Hobby = "Badminton"
            '    },
            '    New Employee() With {
            '        .Code = "SDB-006",
            '        .FirstName = "Anne",
            '        .LastName = "Hathaway",
            '        .Gender = 1,
            '        .Age = 32,
            '        .Address = "US",
            '        .Career = "Actress",
            '        .Hobby = "Music"
            '    },
            '    New Employee() With {
            '        .Code = "SDB-007",
            '        .FirstName = "Scarlett",
            '        .LastName = "Johansson",
            '        .Gender = 1,
            '        .Age = 28,
            '        .Address = "US",
            '        .Career = "Actress",
            '        .Hobby = "Casting"
            '    },
            '    New Employee() With {
            '        .Code = "SDB-008",
            '        .FirstName = "Adam",
            '        .LastName = "Lambert",
            '        .Gender = 0,
            '        .Age = 34,
            '        .Address = "US",
            '        .Career = "Singer",
            '        .Hobby = "Casting"
            '    }
            '}
            'emps.ForEach(Function(s) context.Employees.Add(s))
            'context.SaveChanges()
        End Sub
    End Class
End Namespace
