Module Ctrl_CharacterSelection

    Public Sub ChrSelection(ByVal Selected_cell As Image, ByRef e As MouseButtonEventArgs, ByRef Character_list As List(Of Troop), ByRef Cells As Grid, ByRef lblUnitName As TextBlock, ByRef lblUnitRole As TextBlock, ByRef lblUnitDescription As TextBlock, ByRef lblUnitMoves As TextBlock, ByRef lblUnitStructure As TextBlock, ByRef imgStructure1 As ImageBrush, ByRef imgStructure2 As ImageBrush)

#Region " -- Searching the clicked character. --"

        ' Search, with a LINQ query, the character that is in that cell.
        ' Conditions:
        ' 1. X = column
        ' 2. Y = row

        Dim ThisCharacter As Troop
        Dim OldCharacter As Troop

        Try
            ThisCharacter = (
                        From Troop In Character_list
                        Where Troop.X = Grid.GetColumn(Selected_cell) _
                            And Troop.Y = Grid.GetRow(Selected_cell)
                        ).Single ' Single --> The LINQ query must return a value.
        Catch ex As Exception
            Exit Sub ' If no values are collected, the method is not executed.
        End Try
#End Region

#Region " -- The others are deselected. --"

        Try
            OldCharacter = (From Troop In Character_list Where Troop.IsSelected And Not Troop Is ThisCharacter).Single ' Single --> The LINQ query must return a value.
            OldCharacter.IsSelected = False
            Dim currentCell = (
                        From i As Image In Cells.Children
                        Where Grid.GetColumn(i) = OldCharacter.X And Grid.GetRow(i) = OldCharacter.Y _
                            And i.IsEnabled
                            ).Single
            CType(currentCell, Image).Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + OldCharacter.Graphic + ".png"))
        Catch ex As Exception
            ' Nothing happens.
        End Try

#End Region

#Region " -- Conditional rotation of the selected character. -- "

        ' Logic rotation (if he is selected and he can rotate).

        If ThisCharacter.IsSelected And ThisCharacter.CanRotate Then
            ThisCharacter.Rotate()
        End If

        ' Visual rotation.

        CType(Selected_cell, Image).Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + ThisCharacter.Graphic + "Selec.png"))

#End Region

#Region " -- Logic selection of the character. -- "

        ThisCharacter.IsSelected = True ' Logic selection.

#End Region

#Region " -- Visual selection of the character. -- "

        lblUnitName.Text = ThisCharacter.Captain
        lblUnitRole.Text = "Tipo: " + ThisCharacter.Role
        lblUnitDescription.Text = ThisCharacter.Role
        lblUnitMoves.Text = "Movimientos: " + ThisCharacter.Moves.ToString

        Dim Struct = ThisCharacter.GetStructure(Cells)

        If Struct = "Void" Then
            lblUnitStructure.Text = "Descubierto"
        Else
            lblUnitStructure.Text = Struct
        End If

        ThisCharacter.IsSelected = True ' Logic selection.

#End Region



        If ThisCharacter.CanBuild Then

            Dim CurrentStructureName As String = ThisCharacter.GetStructure(Cells)

            If CurrentStructureName = "Tower" Or CurrentStructureName = "Parapet" Then
                imgStructure1.ImageSource = Sprites.FORTRESS_png
                imgStructure2.ImageSource = Sprites.DEMOLISH_png
            ElseIf CurrentStructureName = "Fortress" Then
                imgStructure1.ImageSource = Sprites.DEMOLISH_png
                imgStructure2.ImageSource = Sprites.VOID_png
            Else
                imgStructure1.ImageSource = Sprites.TOWER_png
                imgStructure2.ImageSource = Sprites.PARAPET_png
            End If

        ElseIf ThisCharacter.CanDemolish Then

            Dim CurrentStructureName As String = ThisCharacter.GetStructure(Cells)

            If Not CurrentStructureName = "Void" Then
                imgStructure1.ImageSource = Sprites.DEMOLISH_png
                imgStructure2.ImageSource = Sprites.VOID_png
            End If

        Else
            imgStructure1.ImageSource = Sprites.VOID_png
            imgStructure2.ImageSource = Sprites.VOID_png
        End If



    End Sub

End Module