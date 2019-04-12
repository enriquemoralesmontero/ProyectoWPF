Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class StartMenu

    Public Character_list As New List(Of Troop)

    Public Sub SetCharacter_list(ByRef Character_list As List(Of Troop))
        Me.Character_list = Character_list
    End Sub

    Private Sub Control_SizeChanged(sender As Object, e As SizeChangedEventArgs)

        Dim tmp As Control = CType(sender, Control)
        tmp.FontSize = e.NewSize.Height / tmp.FontFamily.LineSpacing / 2

    End Sub

    Private Sub Save(sender As Object, e As RoutedEventArgs)

        Dim TestFileStream As Stream = File.Create(GetFilePath())
        Dim serializer As New BinaryFormatter
        serializer.Serialize(TestFileStream, Character_list)
        TestFileStream.Close()

    End Sub

    Private Function GetFilePath() As String

        Dim ReturnString As String
        Dim OpenFileDialog1 As New Microsoft.Win32.OpenFileDialog With {
            .InitialDirectory = "c:\",
            .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            .FilterIndex = 2,
            .RestoreDirectory = True
        }

        If Not OpenFileDialog1.ShowDialog() Then
            Return "AutoSave.txt"
        End If

        ReturnString = OpenFileDialog1.FileName

        Return ReturnString

    End Function

End Class