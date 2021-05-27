'---------------------------------------------------------------------------------------------------
' file:		AI_BASIC\LCARS\LCARS.vb
'
' summary:	Lcars class
'---------------------------------------------------------------------------------------------------

Imports System.Drawing


Namespace LCARS

    '''////////////////////////////////////////////////////////////////////////////////////////////////////
    ''' <summary>   A colors. </summary>
    '''
    ''' <remarks>   Leroy, 27/05/2021. </remarks>
    '''////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Class Colors

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Values that represent lcars colors. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Enum LcarsColors
            ''' <summary>   An enum constant representing the orange option. </summary>
            Orange
            ''' <summary>   An enum constant representing the lilac option. </summary>
            Lilac
            ''' <summary>   An enum constant representing the blue grey option. </summary>
            BlueGrey
            ''' <summary>   An enum constant representing the red brown option. </summary>
            RedBrown
            ''' <summary>   An enum constant representing the beige option. </summary>
            Beige
            ''' <summary>   An enum constant representing the light blue option. </summary>
            LightBlue
            ''' <summary>   An enum constant representing the apricot option. </summary>
            Apricot
            ''' <summary>   An enum constant representing the pink option. </summary>
            Pink
            ''' <summary>   An enum constant representing the hell orange option. </summary>
            HellOrange
        End Enum

#Region "LCARS_NEXTGEN"

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Oranges this.  </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function Orange()
            Return FromArgb(255, 153, 0)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Lilacs this.  </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function Lilac()
            Return FromArgb(204, 153, 204)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Blue grey. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function BlueGrey()
            Return FromArgb(153, 153, 204)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Red brown. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function RedBrown()
            Return FromArgb(204, 102, 102)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Lcars beige be. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function LcarsBeigeBE()
            Return FromArgb(255, 104, 153)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Light blue. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function LightBlue()
            Return FromArgb(153, 153, 255)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Lcars apricot a p. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function LcarsApricotAP()
            Return FromArgb(255, 153, 102)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Pinks this.  </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function Pink()
            Return FromArgb(204, 102, 153)
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Hell orange. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   . </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function HellOrange()
            Return FromArgb(247, 198, 74)
        End Function
#End Region

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   From ARGB. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="red">      The red. </param>
        ''' <param name="green">    The green. </param>
        ''' <param name="blue">     The blue. </param>
        '''
        ''' <returns>   A Color. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function FromArgb(red As Integer, green As Integer, blue As Integer) As Color
            Return Color.FromArgb(red, blue, green)
        End Function

    End Class
End Namespace
