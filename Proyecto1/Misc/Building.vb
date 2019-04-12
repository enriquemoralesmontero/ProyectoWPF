Module Building





    ' No se usa...

    Public Sub GetImage(ByVal Person As Troop, ByRef Cells As Grid)



    End Sub

    ' No se usa... Seguir

    Public Function GetName(ByVal Person As Troop, ByRef Cells As Grid) As String

        Dim currentCell As Image

        Try
            currentCell = (
                        From i As Image In Cells.Children
                        Where Grid.GetColumn(i) = Person.X And Grid.GetRow(i) = Person.Y _
                            And Not i.IsEnabled
                            ).Single
        Catch ex As Exception
            Return ""
        End Try

        Return CType(currentCell, Image).Source.ToString.Replace("pack://siteoforigin:,,,/Resources/", "").Replace(".png", "")

    End Function

End Module