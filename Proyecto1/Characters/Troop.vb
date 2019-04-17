''' <summary>
'''     Superclass of all game units.
''' </summary>
<Serializable()> Public Class Troop

    ' #################################################################################################
    ' ATTRIBUTES
    ' #################################################################################################

    Public Property X As Integer            ' Coordinate - Column
    Public Property Y As Integer            ' Coordinate - Row
    Public Property BaseMoves As Integer
    Private Property Moves As List(Of Boolean)
    'Private Property Moves As Integer

    Public Property IsSelected As Boolean
    Public Property IsLocked As Boolean     ' The character can not move.
    Public Property IsArmored As Boolean    ' Archers need not move before attacking to kill him.
    Public Property CanRotate As Boolean    ' The unit can be repositioned to lock enemies.
    Public Property CanBuild As Boolean
    Public Property CanDemolish As Boolean
    Public Property CanShoot As Boolean

    Public Property Captain As String       ' His name.
    Public Property Role As String          ' Lancer, knight, archer, engineer...
    Public Property Colour As String
    Public Property Graphic As String       ' The name of your image (.PNG, etc...).
    Public Property Position As String      ' The character can look up, down, right or left to lock enemies.


    ' #################################################################################################
    ' CONSTRUCTOR
    ' #################################################################################################

    ''' <summary> Constructor </summary>
    ''' <param name="role">Lancer, knight, archer, engineer...</param>
    ''' <param name="colour">Colour</param>
    ''' <param name="X">Location (column)</param>
    ''' <param name="Y">Location (row)</param>
    Public Sub New(ByVal role As String,
                ByVal colour As String,
                ByVal X As Integer,
                ByVal Y As Integer)

        Me.Captain = NameGenerator.CaptainName()
        Me.Role = role
        Me.Colour = colour

        Me.X = X
        Me.Y = Y

        Me.IsSelected = False
        Me.IsLocked = False
        Me.CanRotate = True
        Me.CanShoot = False

        ' A engineer can build!

    End Sub

    ' #################################################################################################
    ' SETTERS AND GETTERS
    ' #################################################################################################

    Public Function GetMoves() As Integer
        Return Me.Moves.Count
    End Function

    Public Sub SetMoves(ByVal Moves As Integer)

        Me.Moves = New List(Of Boolean)

        For i As Integer = 1 To Moves
            Me.Moves.Add(True)
        Next

    End Sub

    ' #################################################################################################
    ' METHODS
    ' #################################################################################################

    ''' <summary>
    '''     Procedure executed at the end of the turn. Moves = 0, NO rotation, NO build, NO demolish, NO shoot.
    ''' </summary>
    Public Sub FinishTurn()

        Me.SetMoves(0)

        Me.CanRotate = False
        Me.CanBuild = False
        Me.CanDemolish = False
        Me.CanShoot = False

    End Sub

    ''' <summary>
    '''     The unit starts with all the base attributes prepared for the new turn.
    ''' </summary>
    Public Sub StartTurn() ' This method will be overwritten.

        If Role = "Archer" Then ' This code is duplicated, but I put it for clarity.
            Me.CanShoot = True
        ElseIf Role = "Engineer" Then
            Me.CanBuild = True
            Me.CanDemolish = True
        ElseIf Role = "Lancer" Then
            Me.CanDemolish = True
        End If

        Me.CanRotate = True
        Me.SetMoves(Me.BaseMoves)

    End Sub

    ''' <summary>
    '''     Change the positioning and formation of the unit to be able to lock enemies.
    '''     The rotation is according to the clockwise direction.
    '''     There are units (archers, engineers...) that do not rotate.
    ''' </summary>
    Public Sub Rotate()

        Select Case Me.Position
            Case "Left"
                Me.Position = "Up"
                Me.Graphic = Me.Graphic.Replace("Left", "Up")
            Case "Up"
                Me.Position = "Right"
                Me.Graphic = Me.Graphic.Replace("Up", "Right")
            Case "Right"
                Me.Position = "Down"
                Me.Graphic = Me.Graphic.Replace("Right", "Down")
            Case "Down"
                Me.Position = "Left"
                Me.Graphic = Me.Graphic.Replace("Down", "Left")
        End Select

    End Sub

    ''' <summary>
    '''     Function that controls if the unit has an adjacent enemy.
    '''     If so, the allied unit is locked in combat and can not move!!
    ''' </summary>
    ''' <param name="Character_list">All units in battle.</param>
    Public Sub AdjacentEnemies(ByVal Character_list)

        For Each Enemy As Troop In Character_list

            If Enemy.Colour = Me.Colour Then
                Continue For ' We ignore the allies.
            End If

            ' Unit locked?

            If Me.X = Enemy.X And Me.Y = Enemy.Y - 1 And Enemy.Position = "Up" Then
                Me.IsLocked = True
            ElseIf Me.X = Enemy.X And Me.Y = Enemy.Y + 1 And Enemy.Position = "Down" Then
                Me.IsLocked = True
            ElseIf Me.X = Enemy.X + 1 And Me.Y = Enemy.Y And Enemy.Position = "Right" Then
                Me.IsLocked = True
            ElseIf Me.X = Enemy.X - 1 And Me.Y = Enemy.Y And Enemy.Position = "Left" Then
                Me.IsLocked = True
            End If

        Next
    End Sub

    ''' <summary>
    '''     Function with which the name of the structure on which the character is obtained.
    ''' </summary>
    ''' <param name="Cells">All the cells of the battlefield.</param>
    ''' <returns>The name of the structure on which the character</returns>
    Public Function GetStructure(ByRef Cells As Grid) As String

        Dim currentCell As Image

        Try
            currentCell = (
                        From i As Image In Cells.Children
                        Where Grid.GetColumn(i) = Me.X And Grid.GetRow(i) = Me.Y _
                            And Not i.IsEnabled ' Cells that are not enabled are those that are in the background.
            ).Single
        Catch ex As Exception
            Return ""
        End Try

        Return currentCell.Source.ToString.Replace("pack://siteoforigin:,,,/Resources/", "").Replace(".png", "")

    End Function

End Class