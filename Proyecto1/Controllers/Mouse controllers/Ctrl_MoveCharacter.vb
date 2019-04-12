Module Ctrl_MoveCharacter

    Public Sub Move(ByVal sender As Object, ByRef e As MouseButtonEventArgs, ByRef Character_list As List(Of Troop), ByRef Current_Turn As String, ByRef Cells As Grid, ByRef lblUnitMoves As TextBlock)

        For Each Troop In Character_list
            If Troop.Colour = Current_Turn Then
                Troop.IsLocked = False
                Troop.AdjacentEnemies(Character_list)
            End If
        Next

        Dim This_Character As Troop ' The unit who moves.
        Dim Invaded_cell As Image = CType(sender, Image)    ' Cell invaded.
        Dim Attacked_character = Nothing                    ' Enemy invaded.
        Dim Invaded_structure As Image                     ' Structure invaded.

        ' Search, with a LINQ query, the character that Is in that cell.
        ' Conditions:
        '   1. The character must be selected.

        Try
            This_Character = (
                        From Troop In Character_list
                        Where Troop.IsSelected
                        ).Single ' Single --> The LINQ query must return a value.
        Catch ex As Exception

        End Try



        ' Continuar por aqui... 

        Try
            Invaded_structure = (
                From im As Image In Cells.Children
                Where Grid.GetColumn(Invaded_cell) = Grid.GetColumn(im) _
                    And Grid.GetRow(Invaded_cell) = Grid.GetRow(im) _
                    And Not im.IsEnabled
                ).Single
        Catch ex As Exception

        End Try

        If This_Character.Role = "Knight" And Invaded_structure.Source.ToString = Sprites.TOWER Then
            Exit Sub
        ElseIf This_Character.Role = "Knight" And Invaded_structure.Source.ToString = Sprites.FORTRESS Then
            Exit Sub
        End If






        Try
            Attacked_character = (
                        From Troop In Character_list
                        Where Troop.X = Grid.GetColumn(Invaded_cell) _
                            And Troop.Y = Grid.GetRow(Invaded_cell)
                        ).Single ' Single --> The LINQ query must return a value.

            If CType(Attacked_character, Troop).Colour = CType(This_Character, Troop).Colour Then
                Exit Sub
            ElseIf CType(This_Character, Troop).Position = "" Then
                Exit Sub
            ElseIf CType(This_Character, Troop).IsLocked Then
                Exit Sub
            End If

            Character_list.Remove(Attacked_character) ' DEAD!!!

        Catch ex As Exception

        End Try



#Region " -- Searching the selected character. --"



#End Region

        Try
            Dim Old_Cell = (
                From im As Image In Cells.Children
                Where This_Character.X = Grid.GetColumn(im) _
                    And This_Character.Y = Grid.GetRow(im) _
                    And im.IsEnabled
                ).Single

            ' While the character
            '   - is Not locked,
            '   - has moves (> 0)...,
            ' the character moves!

            If Not This_Character.IsLocked And This_Character.Moves > 0 Then

                If IsNothing(Attacked_character) Then

                End If

                If This_Character.X = Grid.GetColumn(Invaded_cell) And This_Character.Y = Grid.GetRow(Invaded_cell) - 1 Then
                    This_Character.Y += 1 ' Move down.
                    This_Character.Moves -= 1 ' Moves are reduced.
                    lblUnitMoves.Text = "Movimientos: " + This_Character.Moves.ToString
                    Old_Cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/Void.png"))
                    Invaded_cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + This_Character.Graphic + "Selec.png"))
                ElseIf This_Character.X = Grid.GetColumn(Invaded_cell) And This_Character.Y = Grid.GetRow(Invaded_cell) + 1 Then
                    This_Character.Y -= 1 ' Move up.
                    This_Character.Moves -= 1 ' Moves are reduced.
                    lblUnitMoves.Text = "Movimientos: " + This_Character.Moves.ToString
                    Old_Cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/Void.png"))
                    Invaded_cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + This_Character.Graphic + "Selec.png"))
                ElseIf This_Character.X + 1 = Grid.GetColumn(Invaded_cell) And This_Character.Y = Grid.GetRow(Invaded_cell) Then
                    This_Character.X += 1 ' Move right.
                    This_Character.Moves -= 1 ' Moves are reduced.
                    lblUnitMoves.Text = "Movimientos: " + This_Character.Moves.ToString
                    Old_Cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/Void.png"))
                    Invaded_cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + This_Character.Graphic + "Selec.png"))
                ElseIf This_Character.X - 1 = Grid.GetColumn(Invaded_cell) And This_Character.Y = Grid.GetRow(Invaded_cell) Then
                    This_Character.X -= 1 ' Move left.
                    This_Character.Moves -= 1 ' Moves are reduced.
                    lblUnitMoves.Text = "Movimientos: " + This_Character.Moves.ToString
                    Old_Cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/Void.png"))
                    Invaded_cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + This_Character.Graphic + "Selec.png"))
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

End Module