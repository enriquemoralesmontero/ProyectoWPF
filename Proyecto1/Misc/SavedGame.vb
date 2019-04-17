<Serializable()> Public Class SavedGame

    ' Fields.

    Property Structures As New List(Of String)
    Public Property Current_Turn As String
    Public Property Character_list As List(Of Troop)

    ' Constructor.

    Public Sub New(ByVal Cells As Grid,
                ByVal Current_Turn As String,
                ByVal Character_list As List(Of Troop))

        SetStructures(Cells)
        Me.Current_Turn = Current_Turn
        Me.Character_list = Character_list

    End Sub

    ' Getters and setters.

    Public Function GetStructures()
        Return Structures
    End Function

    Public Sub SetStructures(Cells As Grid)

        If Cells Is Nothing Then
            Throw New ArgumentNullException(NameOf(Cells))
        End If

        For Each Cell As Image In Cells.Children
            Structures.Add(Cell.Source.ToString)
        Next

    End Sub

End Class