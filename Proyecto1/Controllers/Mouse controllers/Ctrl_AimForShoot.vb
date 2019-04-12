Module Ctrl_AimForShoot

    ''' <summary>
    '''     Method that paints a shooting target on the unit to which we want to shoot.
    ''' </summary>
    ''' <param name="Targeted_cell">Cell on which we want to shoot.</param>
    ''' <param name="Character_list">All units in battle.</param>
    ''' <param name="Cells">All the cells of the battlefield.</param>
    Public Sub Aim(ByVal Targeted_cell As Image, ByRef Character_list As List(Of Troop), ByRef Cells As Grid)

        Dim CurrentCharacter As Troop ' Archer who shoot.


        ' - Control 1:
        ' The archer must be selected.

        Try
            CurrentCharacter = (From Troop In Character_list Where Troop.IsSelected).Single ' Single --> The LINQ query must return a value.
        Catch ex As Exception
            Exit Sub
        End Try


        ' - Control 2:
        ' If the archer can´t shoot: exit.

        If Not CurrentCharacter.CanShoot Then
            Exit Sub
        End If

        ' Range of the arrow.

        Dim range As Integer = 3

        ' If the archer is in a high place, his range increases.

        Try

            Dim CurrentCell As Image = (
                        From i As Image In Cells.Children
                        Where Grid.GetColumn(i) = CurrentCharacter.X And Grid.GetRow(i) = CurrentCharacter.Y _
                            And Not i.IsEnabled
                ).Single

            If CurrentCell.Source.ToString = Sprites.TOWER Then ' The tower is a high place.
                range = range + 1
            ElseIf CurrentCell.Source.ToString = Sprites.FORTRESS Then ' The fortress is a high place.
                range = range + 1
            End If

        Catch ex As Exception
            ' If no values are collected, nothing happens.
        End Try


        ' - Control 3:
        ' The targeted unit must be an enemy (different colors).

        Dim MyColour As String = CurrentCharacter.Colour ' Your colour.
        Dim TargetedCharacter As Troop ' The targeted unit.

        Try
            TargetedCharacter = (
                                    From Troop In Character_list
                                    Where Troop.X = Grid.GetColumn(Targeted_cell) _
                                        And Troop.Y = Grid.GetRow(Targeted_cell) _
                                        And Troop.Colour <> MyColour
                                    ).Single ' Single --> The LINQ query must return a value.
        Catch ex As Exception
            Exit Sub
        End Try


        ' - Control 4:
        ' The enemy must be at the correct distance.

        If Not ((CurrentCharacter.Y = TargetedCharacter.Y - range And CurrentCharacter.X = TargetedCharacter.X) _
                Or (CurrentCharacter.Y = TargetedCharacter.Y + range And CurrentCharacter.X = TargetedCharacter.X) _
                Or (CurrentCharacter.X = TargetedCharacter.X - range And CurrentCharacter.Y = TargetedCharacter.Y) _
                Or (CurrentCharacter.X = TargetedCharacter.X + range And CurrentCharacter.Y = TargetedCharacter.Y)) Then

            Exit Sub

        End If


        ' - Control 5:
        ' If the enemy is protected with structures, we can not shoot him.

        If TargetedCharacter.GetStructure(Cells) = "Parapet" Then
            Targeted_cell.Source = Sprites.PARAPET_png
            Exit Sub
        ElseIf TargetedCharacter.GetStructure(Cells) = "Fortress" Then
            Targeted_cell.Source = Sprites.FORTRESS_png
            Exit Sub
        End If


        ' - Control 6:
        ' If the enemy has any protection against the arrows, we are forced to shoot him without having moved before.

        If TargetedCharacter.IsArmored And CurrentCharacter.Moves <> CurrentCharacter.BaseMoves Then
            Targeted_cell.Source = Sprites.PARAPET_png
            Exit Sub
        End If

        ' All right!
        ' If we can shoot him, after all the previous controls, we paint the shooting target.

        Targeted_cell.Source = Sprites.TARGET_png

        ' Ready to shoot!!!

    End Sub

End Module