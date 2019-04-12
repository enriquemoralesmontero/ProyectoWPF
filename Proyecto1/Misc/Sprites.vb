Module Sprites

    ' Empty image.

    Public Const VOID As String = "pack://siteoforigin:,,,/Resources/Void.png"

    ' Building.

    Public Const TOWER As String = "pack://siteoforigin:,,,/Resources/Tower.png"
    Public Const PARAPET As String = "pack://siteoforigin:,,,/Resources/Parapet.png"
    Public Const FORTRESS As String = "pack://siteoforigin:,,,/Resources/Fortress.png"
    Public Const DEMOLISH As String = "pack://siteoforigin:,,,/Resources/Demolish.png"

    Public VOID_png As BitmapImage = New BitmapImage(New Uri(VOID))
    Public TOWER_png As BitmapImage = New BitmapImage(New Uri(TOWER))
    Public PARAPET_png As BitmapImage = New BitmapImage(New Uri(PARAPET))
    Public FORTRESS_png As BitmapImage = New BitmapImage(New Uri(FORTRESS))
    Public DEMOLISH_png As BitmapImage = New BitmapImage(New Uri(DEMOLISH))

    ' Arrow targets.

    Public Const TARGET As String = "pack://siteoforigin:,,,/Resources/Target.png"
    Public Const KO As String = "pack://siteoforigin:,,,/Resources/KO.jpg"

    Public TARGET_png As BitmapImage = New BitmapImage(New Uri(TARGET))
    Public KO_jpg As BitmapImage = New BitmapImage(New Uri(KO))

    ' Background images.

    Public Const BASE_0 As String = "pack://siteoforigin:,,,/Resources/Base0.png"
    Public Const BASE_1 As String = "pack://siteoforigin:,,,/Resources/Base1.png"
    Public Const BASE_2 As String = "pack://siteoforigin:,,,/Resources/Base2.png"
    Public Const BASE_3 As String = "pack://siteoforigin:,,,/Resources/Base3.png"
    Public Const BASE_4 As String = "pack://siteoforigin:,,,/Resources/Base4.png"
    Public Const BASE_5 As String = "pack://siteoforigin:,,,/Resources/Base5.png"

    Public BASE_0_png As BitmapImage = New BitmapImage(New Uri(BASE_0))
    Public BASE_1_png As BitmapImage = New BitmapImage(New Uri(BASE_1))
    Public BASE_2_png As BitmapImage = New BitmapImage(New Uri(BASE_2))
    Public BASE_3_png As BitmapImage = New BitmapImage(New Uri(BASE_3))
    Public BASE_4_png As BitmapImage = New BitmapImage(New Uri(BASE_4))
    Public BASE_5_png As BitmapImage = New BitmapImage(New Uri(BASE_5))

    Public Function getBackground(ByVal Index As Integer)

        Select Case Index
            Case 1
                Return BASE_0_png
            Case 2
                Return BASE_1_png
            Case 3
                Return BASE_2_png
            Case 4
                Return BASE_3_png
            Case 5
                Return BASE_4_png
            Case 6
                Return BASE_5_png
        End Select

        Return BASE_0_png

    End Function

End Module
