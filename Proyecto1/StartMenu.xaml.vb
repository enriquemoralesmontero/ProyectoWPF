Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports Proyecto1

Public Class StartMenu

    Property Character_list As New List(Of Troop)
    Property Current_Turn As String
    Property Cells As Grid

    Private Sub Control_SizeChanged(sender As Object, e As SizeChangedEventArgs)

        Dim tmp As Control = CType(sender, Control)
        tmp.FontSize = e.NewSize.Height / tmp.FontFamily.LineSpacing / 2

    End Sub



    Private Sub Save(sender As Object, e As RoutedEventArgs)

        Dim SavingGame As SavedGame = New SavedGame(Cells, Current_Turn, Character_list)

        Dim TestFileStream As Stream = File.Create(GetSaveFilePath())
        Dim serializer As New BinaryFormatter
        serializer.Serialize(TestFileStream, SavingGame)
        TestFileStream.Close()

    End Sub

    Private Function GetSaveFilePath() As String

        Dim ReturnString As String
        Dim FileDialog As New Microsoft.Win32.SaveFileDialog With {
            .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            .FilterIndex = 2,
            .RestoreDirectory = True
        }

        If Not FileDialog.ShowDialog() Then
            Return "AutoSave.txt"
        End If

        ReturnString = FileDialog.FileName

        Return ReturnString

    End Function

    Private Function GetOpenFilePath() As String

        Dim ReturnString As String
        Dim FileDialog As New Microsoft.Win32.OpenFileDialog With {
            .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            .FilterIndex = 2,
            .RestoreDirectory = True
        }

        If Not FileDialog.ShowDialog() Then
            Return "AutoSave.txt"
        End If

        ReturnString = FileDialog.FileName

        Return ReturnString

    End Function

    Private Sub Open(sender As Object, e As RoutedEventArgs)

        Dim FilePath As String = GetOpenFilePath()

        If File.Exists(FilePath) Then

            Dim TestFileStream As Stream = File.OpenRead(FilePath)
            Dim deserializer As New BinaryFormatter
            Dim OpeningGame = CType(deserializer.Deserialize(TestFileStream), SavedGame)
            TestFileStream.Close()

            Character_list = OpeningGame.Character_list
            Current_Turn = OpeningGame.Current_Turn

            For i As Integer = 0 To (OpeningGame.Structures.Count - 1)

                CType(Cells.Children.Item(i), Image).Source = New BitmapImage(New Uri(OpeningGame.Structures.Item(i)))

            Next

        End If

    End Sub

    Friend Sub SetFields(ByRef Character_list As List(Of Troop), ByRef Current_Turn As String, ByRef Cells As Grid)

        Me.Character_list = Character_list
        Me.Current_Turn = Current_Turn
        Me.Cells = Cells

    End Sub
End Class