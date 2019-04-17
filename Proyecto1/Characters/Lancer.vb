''' <summary>
'''     The spearman is a... somewhat pathetic troop.
'''     Attack at close range. He has a poor movement. It does not stand out in anything!
'''     On the other hand, there are many of them.
'''     When the adversary begins to build an imposing system of structures (where knights and archers become useless), this infantry can assault him and destroy his defenses.
'''     The closed formations of lancers become impenetrable to enemy cavalry, but they are weak to the arrows!
''' </summary>
<Serializable()> Public Class Lancer
    Inherits Troop ' This is his Superclass.

    ''' <summary> Constructor </summary>
    ''' <param name="colour">Colour</param>
    ''' <param name="X">Location (column)</param>
    ''' <param name="Y">Location (row)</param>
    Public Sub New(ByVal colour As String,
                ByVal X As Integer,
                ByVal Y As Integer)

        MyBase.New("Lancer", colour, X, Y)

        BaseMoves = 1
        SetMoves(1)
        IsArmored = False
        CanDemolish = True ' He can destroy structures!!!
        CanBuild = False

        Select Case colour

            Case "Blue"
                Position = "Right"
                Graphic = "LancerBlueRight"

            Case "Red"
                Position = "Left"
                Graphic = "LancerRedLeft"

        End Select

    End Sub


    ' #################################################################################################
    ' METHODS
    ' #################################################################################################

    Public Overloads Sub StartTurn() ' This method is overwritten.

        Me.CanDemolish = True

        MyBase.StartTurn()

    End Sub

End Class
