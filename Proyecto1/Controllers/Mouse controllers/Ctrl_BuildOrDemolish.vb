Imports Proyecto1

Module Ctrl_BuildOrDemolish

    Public Sub BuildOrDemolish(ByRef Character_list As List(Of Troop), ByRef imgStructure As ImageBrush, ByRef Cells As Grid)

        If imgStructure.ImageSource.ToString = Sprites.TOWER Then
            Build("Tower", Character_list, Cells)
        ElseIf imgStructure.ImageSource.ToString = Sprites.PARAPET Then
            Build("Parapet", Character_list, Cells)
        ElseIf imgStructure.ImageSource.ToString = Sprites.FORTRESS Then
            Build("Fortress", Character_list, Cells)
        ElseIf imgStructure.ImageSource.ToString = Sprites.DEMOLISH Then
            Demolish(Character_list, Cells)
        Else
            Exit Sub
        End If

    End Sub

    Private Sub Build(StructureName As String, ByRef Character_list As List(Of Troop), ByRef Cells As Grid)

        Dim ThisCharacter As Troop
        Dim currentCell As Image

        Try
            ThisCharacter = (From Troop In Character_list Where Troop.IsSelected).Single ' Single --> The LINQ query must return a value.

            currentCell = (
                        From i As Image In Cells.Children
                        Where Grid.GetColumn(i) = ThisCharacter.X And Grid.GetRow(i) = ThisCharacter.Y _
                            And Not i.IsEnabled
                            ).Single

        Catch ex As Exception
            Exit Sub ' If no values are collected, the method is not executed.
        End Try

        ThisCharacter.Moves = 0
        ThisCharacter.CanBuild = False
        ThisCharacter.CanDemolish = False

        CType(currentCell, Image).Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + StructureName + ".png"))

    End Sub

    Private Sub Demolish(character_list As List(Of Troop), cells As Grid)

        Dim ThisCharacter As Troop
        Dim currentCell As Image

        Try
            ThisCharacter = (From Troop In character_list Where Troop.IsSelected).Single ' Single --> The LINQ query must return a value.

            currentCell = (
                        From i As Image In cells.Children
                        Where Grid.GetColumn(i) = ThisCharacter.X And Grid.GetRow(i) = ThisCharacter.Y _
                            And Not i.IsEnabled
                            ).Single

        Catch ex As Exception
            Exit Sub ' If no values are collected, the method is not executed.
        End Try

        ThisCharacter.Moves = 0
        ThisCharacter.CanDemolish = False

        CType(currentCell, Image).Source = Sprites.VOID_png

    End Sub

End Module