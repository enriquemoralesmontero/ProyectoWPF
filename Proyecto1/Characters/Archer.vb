''' <summary>
'''     The archer is a unit that attacks from a distance. He does not lock the enemy.
'''     He is not able to defend himself at close range, so he is advised to be in the rearguard or protected by structures.
'''     He has two types of shot:
'''         - A stronger one that will prevent him from moving. This one is effective against armored units (knights).
'''         - A weaker one that does not reduce his mobility.
''' </summary>
<Serializable()> Public Class Archer
    Inherits Troop ' This is his Superclass.

    ''' <summary> Constructor </summary>
    ''' <param name="colour">Colour</param>
    ''' <param name="X">Location (column)</param>
    ''' <param name="Y">Location (row)</param>
    Public Sub New(ByVal colour As String,
                ByVal X As Integer,
                ByVal Y As Integer)

        MyBase.New("Archer", colour, X, Y)

        BaseMoves = 1
        SetMoves(1)
        IsArmored = False
        CanDemolish = False
        CanBuild = False

        Select Case colour

            Case "Blue"
                Graphic = "ArcherBlue"

            Case "Red"
                Graphic = "ArcherRed"

        End Select

    End Sub


    ' #################################################################################################
    ' METHODS
    ' #################################################################################################

    Public Overloads Sub StartTurn() ' This method is overwritten.

        Me.CanShoot = True

        MyBase.StartTurn()

    End Sub

End Class
