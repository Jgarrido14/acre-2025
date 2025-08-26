Imports System.Security.Principal
Imports System.Runtime.InteropServices
Imports System.IO

Public Class ImpersonationHelper

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Shared Function LogonUser(
        lpszUsername As String,
        lpszDomain As String,
        lpszPassword As String,
        dwLogonType As Integer,
        dwLogonProvider As Integer,
        ByRef phToken As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function CloseHandle(handle As IntPtr) As Boolean
    End Function

    Public Shared Function ImpersonateAndExecute(username As String, domain As String, password As String, action As Action) As Boolean
        Dim tokenHandle As IntPtr = IntPtr.Zero
        Const LOGON32_LOGON_NEW_CREDENTIALS As Integer = 9
        Const LOGON32_PROVIDER_WINNT50 As Integer = 3

        Dim returnValue = LogonUser(username, domain, password, LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_WINNT50, tokenHandle)
        If Not returnValue Then
            Return False
        End If

        Using impersonatedUser = WindowsIdentity.Impersonate(tokenHandle)
            action()
        End Using

        CloseHandle(tokenHandle)
        Return True
    End Function
End Class
