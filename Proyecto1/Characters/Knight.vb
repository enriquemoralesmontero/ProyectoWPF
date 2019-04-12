''' <summary>
'''     Some think it's the best unit in the game... They are not very wrong.
'''     Knights are the most mobile unit.
'''     They have a special defense against arrows.
'''     The weak points are the towers and the fortresses. A knight can not enter them.
''' </summary>
<Serializable()> Public Class Knight
    Inherits Troop ' This is his Superclass.

    ''' <summary> Constructor </summary>
    ''' <param name="colour">Colour</param>
    ''' <param name="X">Location (column)</param>
    ''' <param name="Y">Location (row)</param>
    Public Sub New(ByVal colour As String,
                ByVal X As Integer,
                ByVal Y As Integer)

        MyBase.New("Knight", colour, X, Y)

        BaseMoves = 3 ' A knight can move many cells!
        Moves = 3
        IsArmored = True
        CanDemolish = False
        CanBuild = False

        Select Case colour

            Case "Blue"
                Position = "Right"
                Graphic = "KnightBlueRight"

            Case "Red"
                Position = "Left"
                Graphic = "KnightRedLeft"

        End Select

    End Sub


    ' #################################################################################################
    ' METHODS
    ' #################################################################################################

    Public Overloads Sub StartTurn() ' This method is overwritten.

        MyBase.StartTurn()

    End Sub

End Class
