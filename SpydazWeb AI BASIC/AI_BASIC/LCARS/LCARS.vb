Imports System.Drawing


Namespace LCARS

    Public Class Colors
        Public Enum LcarsColors
            Orange
            Lilac
            BlueGrey
            RedBrown
            Beige
            LightBlue
            Apricot
            Pink
            HellOrange
        End Enum

#Region "LCARS_NEXTGEN"


        Public Function Orange()
            Return FromArgb(255, 153, 0)
        End Function
        Public Function Lilac()
            Return FromArgb(204, 153, 204)
        End Function

        Public Function BlueGrey()
            Return FromArgb(153, 153, 204)
        End Function
        Public Function RedBrown()
            Return FromArgb(204, 102, 102)
        End Function
        Public Function LcarsBeigeBE()
            Return FromArgb(255, 104, 153)
        End Function
        Public Function LightBlue()
            Return FromArgb(153, 153, 255)
        End Function
        Public Function LcarsApricotAP()
            Return FromArgb(255, 153, 102)
        End Function
        Public Function Pink()
            Return FromArgb(204, 102, 153)
        End Function
        Public Function HellOrange()
            Return FromArgb(247, 198, 74)
        End Function
#End Region

        Private Function FromArgb(red As Integer, green As Integer, blue As Integer) As Color
            Return Color.FromArgb(red, blue, green)
        End Function

    End Class
End Namespace
