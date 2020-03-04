Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Web API configuration and services

        ' Web API routes
        config.MapHttpAttributeRoutes()

        ' GET|PUT|DELETE /api/{resource}/{id}
        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        ' POST /api/{resource}/{action}
        'config.Routes.MapHttpRoute(
        '    name:="Web API RPC Post",
        '    routeTemplate:="api/{controller}/{action}",
        '    defaults:=Nothing,
        '    constraints:=New With {.action = "[A-Za-z]+", .httpMethod = New HttpMethodConstraint("POST")}
        ')
    End Sub
End Module