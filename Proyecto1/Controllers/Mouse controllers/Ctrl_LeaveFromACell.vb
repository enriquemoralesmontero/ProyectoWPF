Module Ctrl_LeaveFromACell

    ''' <summary>
    '''     Method whose actions occur when the mouse leaves a cell.
    '''     Paint the cell with the image of the unit that is in it.
    '''     Clear the images that are created when you point with arrows.
    ''' </summary>
    ''' <param name="sender">The cell from which we left.</param>
    ''' <param name="Character_list">All units in battle.</param>
    Public Sub MouseLeaving(sender As Image, ByRef Character_list As List(Of Troop))

        ' We clean up the image that remains after having shot down a unit with arrows.

        If sender.Source.ToString = Sprites.KO Then
            sender.Source = Sprites.VOID_png
            Exit Sub
        End If

        Dim This_troop As Troop

        Try

            This_troop = (
                From Troop In Character_list
                Where Troop.X = Grid.GetColumn(sender) _
                And Troop.Y = Grid.GetRow(sender)
            ).Single

            If This_troop.IsSelected Then
                sender.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + This_troop.Graphic + "Selec.png"))
            Else
                sender.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + This_troop.Graphic + ".png"))
            End If



        Catch ex As Exception
            'this_cell.Source = New BitmapImage(New Uri(Sprites.VOID))
        End Try

    End Sub

End Module
