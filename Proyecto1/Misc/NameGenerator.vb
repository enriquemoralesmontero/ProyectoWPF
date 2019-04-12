Module NameGenerator

    Public Function CaptainName() As String

        Dim Name As String = ""

        ' 22

        Dim Prefix As New List(Of String) From {"Adan", "Alad", "Alar", "Ancalim", "Aeg", "Am", "Aran", "Barah", "Beleg", "Celeb", "Curu", "Dag", "Eldar", "Elr", "En", "Fea", "Felag", "Fin", "Findag", "For", "Gald", "Gil", "Hal", "Il", "Imlad", "Ing", "Lin", "Mal", "Nel", "Ond", "Pen", "Sild", "Tar", "Ter", "Thur", "Ul", "Undom"}
        Dim Suffix As New List(Of String) From {"an", "or", "orn", "ir", "ad", "ion", "atar", "andir", "amir", "as", "anrod", "ond", "elion", "uil", "ivorn", "do"}

	' -atar (father) -ion (son)

        Randomize()
        Dim dice = Int(Rnd() * Prefix.Count) ' Random int.

        Name = Prefix(dice)

        Randomize()
        dice = Int(Rnd() * Suffix.Count) ' Random int.

        Name = Name & Suffix(dice)

        Return Name

    End Function

End Module