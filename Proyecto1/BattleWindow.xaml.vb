Class BattleWindow

    'Public Current_character As Troop ' This variable allows us to change the position of the clicked character, which will rotate 90 degrees.
    Public Current_Turn As String ' The player "Blue" is the first one.
    Public Character_list As New List(Of Troop)
    Private Const Character_number As Integer = 32

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)



#Region " -- Base image selection. -- "

        Randomize()
        Dim dice = Int(Rnd() * 6) + 1 ' Random ( 1, 2, 3, 4, 5, 6)

        Dim MyBackgroundImage As ImageBrush = New ImageBrush With {
            .ImageSource = getBackground(dice)
        }

        Main.Background = MyBackgroundImage

#End Region

#Region " -- Characters creation. --"

        Character_list.Add(New Lancer("Blue", 0, 0))
        Character_list.Add(New Lancer("Blue", 0, 1))
        Character_list.Add(New Lancer("Blue", 0, 2))
        Character_list.Add(New Lancer("Blue", 0, 5))
        Character_list.Add(New Lancer("Blue", 0, 6))
        Character_list.Add(New Lancer("Blue", 0, 7))

        Character_list.Add(New Lancer("Red", 11, 0))
        'Character_list.Add(New Lancer("Red", 3, 1)) ' Mod
        Character_list.Add(New Lancer("Red", 11, 1)) ' OK
        Character_list.Add(New Lancer("Red", 11, 2))
        Character_list.Add(New Lancer("Red", 11, 5))
        Character_list.Add(New Lancer("Red", 11, 6))
        'Character_list.Add(New Lancer("Red", 2, 5)) ' Mod
        Character_list.Add(New Lancer("Red", 11, 7)) ' OK

        Character_list.Add(New Knight("Blue", 1, 0))
        Character_list.Add(New Knight("Blue", 1, 1))
        Character_list.Add(New Knight("Blue", 1, 6))
        Character_list.Add(New Knight("Blue", 1, 7))

        Character_list.Add(New Knight("Red", 10, 0))
        Character_list.Add(New Knight("Red", 10, 1))
        Character_list.Add(New Knight("Red", 10, 6))
        Character_list.Add(New Knight("Red", 10, 7))

        Character_list.Add(New Archer("Blue", 1, 2))
        Character_list.Add(New Archer("Blue", 0, 3))
        Character_list.Add(New Archer("Blue", 0, 4))
        Character_list.Add(New Archer("Blue", 1, 5))

        'Character_list.Add(New Archer("Red", 3, 0)) ' Mod
        Character_list.Add(New Archer("Red", 10, 2)) ' OK
        Character_list.Add(New Archer("Red", 11, 3))
        Character_list.Add(New Archer("Red", 11, 4))
        Character_list.Add(New Archer("Red", 10, 5))

        Character_list.Add(New Engineer("Blue", 1, 3))
        Character_list.Add(New Engineer("Blue", 1, 4))

        Character_list.Add(New Engineer("Red", 10, 3))
        Character_list.Add(New Engineer("Red", 10, 4))

#End Region

#Region " -- Blue army start. --"

        Current_Turn = "Blue"

        For Each Troop In Character_list
            If Troop.Colour.Equals("Blue") Then
                Troop.StartTurn()
            Else
                Troop.FinishTurn()
            End If
        Next

#End Region


        'MyData myDataObject = New MyData(DateTime.Now);      
        'Binding myBinding = New Binding("MyDataProperty");
        'myBinding.Source = myDataObject;
        ' Bind the New data source to the myText TextBlock control's Text dependency property.
        'lblUnitClass.SetBinding()
        'myText.SetBinding(TextBlock.TextProperty, myBinding);

        'Binding bind = New Binding(Of String)

#Region " -- Paint all the graphics. --"

        RenderAll()

#End Region

    End Sub

    Private Sub RenderAll()

        ' This procedure shows the graphics of the characters updated. 

        For Each Cell In Cells.Children ' All cells will be updated using this loop.

            Dim this_cell As Image = CType(Cell, Image)

            If this_cell.IsEnabled Then ' The enabled cells are those that are in the foreground. They are the graphics of the characters.

                Try
                    ' Search, with a LINQ query, the character that is in that cell.
                    ' Conditions:
                    ' 1. X = column
                    ' 2. Y = row
                    ' 3. The character must be alive.

                    Dim ThisCharacter = (
                        From Troop In Character_list
                        Where Troop.X = Grid.GetColumn(this_cell) _
                            And Troop.Y = Grid.GetRow(this_cell)
                        ).Single ' Single --> The LINQ query must return a value.

                    ' Now...: repaint!!!

                    this_cell.Source = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + ThisCharacter.Graphic + ".png"))

                Catch ex As Exception
                    this_cell.Source = New BitmapImage(New Uri(Sprites.VOID))
                End Try

            End If

        Next

    End Sub

    ''' <summary>
    '''     Method that is executed when selecting a character.
    ''' </summary>
    ''' <param name="sender">Targeted cell</param>
    ''' <param name="e">Event</param>
    Private Sub Character_Selection(sender As Image, e As MouseButtonEventArgs)

        Ctrl_CharacterSelection.ChrSelection(sender, e, Character_list, Cells, lblUnitName, lblUnitRole, lblUnitDescription, lblUnitMoves, lblUnitTerrain, imgStructure1, imgStructure2)

    End Sub

    ''' <summary>
    '''     Method that is executed when clicking the secondary button of the mouse.
    '''     It allows the character to move to an adyascent cell or shoot.
    ''' </summary>
    ''' <param name="sender">Targeted cell</param>
    ''' <param name="e">Event</param>
    Private Sub MoveOrShoot(sender As Object, e As MouseButtonEventArgs)

        If CType(sender, Image).Source.ToString = Sprites.TARGET Then
            Ctrl_Shoot.Shoot(sender, Character_list)
        Else
            Ctrl_MoveCharacter.Move(sender, e, Character_list, Current_Turn, Cells, lblUnitMoves)
        End If

        imgStructure1.ImageSource = Sprites.VOID_png
        imgStructure2.ImageSource = Sprites.VOID_png

    End Sub

    ''' <summary>
    '''     Method that paints a shooting target on the unit to which we want to shoot.
    ''' </summary>
    ''' <param name="sender">Cell on which we want to shoot.</param>
    ''' <param name="e">It is useless, but it is required.</param>
    Private Sub Aim(sender As Image, e As MouseEventArgs)

        Try
            Ctrl_AimForShoot.Aim(sender, Character_list, Cells)
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    '''     Method that occurs when the button to end the turn is pressed.
    '''     Finish your turn!
    ''' </summary>
    ''' <param name="sender">The button that is pressed to finish your turn.</param>
    ''' <param name="e">It is useless, but it is required.</param>
    Private Sub BtnEndTurn_Click(sender As Object, e As RoutedEventArgs) Handles btnEndTurn.Click

        If Current_Turn.Equals("Blue") Then
            Current_Turn = "Red"
            For Each Troop In Character_list
                If Troop.Colour.Equals("Red") Then
                    Troop.StartTurn()
                Else
                    Troop.FinishTurn()
                End If
            Next
        Else
            Current_Turn = "Blue"
            For Each Troop In Character_list
                If Troop.Colour.Equals("Blue") Then
                    Troop.StartTurn()
                Else
                    Troop.FinishTurn()
                End If
            Next
        End If

        imgStructure1.ImageSource = Sprites.VOID_png
        imgStructure2.ImageSource = Sprites.VOID_png
        Flag.ImageSource = New BitmapImage(New Uri("pack://siteoforigin:,,,/Resources/" + Current_Turn + "Flag.png"))

    End Sub

    ''' <summary>
    '''     Method that occurs when a construction button is pressed.
    '''     This allows to build or destroy structures.
    ''' </summary>
    ''' <param name="sender">The button that is pressed to build the structure.</param>
    ''' <param name="e">It is useless, but it is required.</param>
    Private Sub BuildOrDemolish(sender As Button, e As RoutedEventArgs) Handles btnStructure1.Click, btnStructure2.Click

        If sender.Name = btnStructure1.Name Then
            Ctrl_BuildOrDemolish.BuildOrDemolish(Character_list, imgStructure1, Cells)
        Else
            Ctrl_BuildOrDemolish.BuildOrDemolish(Character_list, imgStructure2, Cells)
        End If

        imgStructure1.ImageSource = New BitmapImage(New Uri(Sprites.VOID))
        imgStructure2.ImageSource = New BitmapImage(New Uri(Sprites.VOID))

    End Sub

    ''' <summary>
    '''     Method whose actions occur when the mouse leaves a cell.
    '''     Paint the cell with the image of the unit that is in it.
    '''     Clear the images that are created when you point with arrows.
    ''' </summary>
    ''' <param name="sender">The cell from which we left.</param>
    ''' <param name="e">It is useless, but it is required.</param>
    Private Sub MouseLeaving(sender As Image, e As MouseEventArgs)

        Ctrl_LeaveFromACell.MouseLeaving(sender, Character_list)

    End Sub

    Private Sub BtnStart_Click(sender As Object, e As RoutedEventArgs) Handles btnStart.Click

        'Dim NewStartMenu As StartMenu = New StartMenu With {
        '    .Character_list = Me.Character_list,
        '    .Current_Turn = Me.Current_Turn,
        '    .Cells = Me.Cells
        '}

        Dim NewStartMenu As StartMenu = New StartMenu

        NewStartMenu.SetFields(Character_list, Current_Turn, Cells)
        NewStartMenu.ShowDialog()

        Character_list = NewStartMenu.Character_list
        Current_Turn = NewStartMenu.Current_Turn
        Cells = NewStartMenu.Cells

        'Dim dia As Dialogo = New Dialogo
        'dia.ShowDialog()

    End Sub
End Class
