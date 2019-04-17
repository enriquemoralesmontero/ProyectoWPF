<Serializable()> Public Class Engineer
    Inherits Troop ' This is his Superclass.

    ''' <summary> Constructor </summary>
    ''' <param name="colour">Colour</param>
    ''' <param name="X">Location (column)</param>
    ''' <param name="Y">Location (row)</param>
    Public Sub New(ByVal colour As String,
                ByVal X As Integer,
                ByVal Y As Integer)

        MyBase.New("Engineer", colour, X, Y)

        BaseMoves = 2 ' A engineer can move two cells!
        SetMoves(2)
        IsArmored = False
        CanDemolish = True
        CanBuild = True

        Select Case colour

            Case "Blue"
                Graphic = "EngineerBlue"

            Case "Red"
                Graphic = "EngineerRed"

        End Select

    End Sub


    ' #################################################################################################
    ' METHODS
    ' #################################################################################################

    Public Overloads Sub StartTurn() ' This method is overwritten.

        Me.CanBuild = True
        Me.CanDemolish = True

        MyBase.StartTurn()

    End Sub

End Class
