Module Ctrl_Shoot

    Public Sub Shoot(ByVal sender As Object, ByRef Character_list As List(Of Troop))

        Dim MyArcher As Troop

        Try
            MyArcher = (From Troop In Character_list Where Troop.IsSelected).Single ' Single --> The LINQ query must return a value.
        Catch ex As Exception
            Exit Sub
        End Try

        Dim Targeted_character As Troop

        Try
            Targeted_character = (
                                    From Troop In Character_list
                                    Where Troop.X = Grid.GetColumn(CType(sender, Image)) _
                                        And Troop.Y = Grid.GetRow(CType(sender, Image))
                                    ).Single ' Single --> The LINQ query must return a value.
        Catch ex As Exception
            Exit Sub
        End Try

        MyArcher.CanShoot = False
        MyArcher.SetMoves(0)
        Character_list.Remove(Targeted_character)
        CType(sender, Image).Source = Sprites.KO_jpg

    End Sub

End Module