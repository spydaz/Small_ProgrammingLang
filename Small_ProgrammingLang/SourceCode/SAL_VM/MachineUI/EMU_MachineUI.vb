Imports System.Web
Imports SDK.SmallProgLang.Compiler

Public Class EMU_MachineUI


    Public Sub GoUrl(ByRef Url As String)

        BrowserMain.Navigate(Url)

    End Sub

    Public Function GetUrl(ByRef Text As String) As String
        Select Case Text
            Case "Amiga Workbench Simulator"
                Return "https://taws.ch/​"
            Case "Windows 3.1"
                Return "https://classicreload.com/win3x-windows-31.html"
            Case "Windows 3.1 with CD-ROM"
                Return "https://www.pcjs.org/blog/2019/05/31/​"
            Case "Macintosh System 7" = "https://archive.org/details/mac_MacOS..."
            Case "Windows 95"
                Return "https://www.pcjs.org/blog/2015/09/21/"
            Case "OS/ 2"
                Return "https://www.pcjs.org/blog/2015/12/27/​"
            Case "Windows 93"
                Return "http://www.windows93.net/"
            Case "Atari ST"
                Return "https://jamesfriend.com.au/pce-js/ata...​"
            Case "Windows 1.0"
                Return "https://www.pcjs.org/software/pcx86/s..."
            Case "EmuOS"
                Return "https://emupedia.net/beta/emuos/​"
            Case "Mac Oxs Lion 10"
                Return "https://www.alessioatzeni.com/mac-osx-lion-css3/"
            Case "PCDOS 5"
                Return "http://jamesfriend.com.au/pce-js/ibmpc-games/"
            Case "MAC os7"
                Return "https://jamesfriend.com.au/pce-js/pce-js-apps/"
            Case "Windows 95"
                Return "https://win95.ajf.me/win95.html"
            Case "RegEX 101"
                Return "https://regex101.com/"
            Case "Mermaid diagrams"
                Return "https://mermaid-js.github.io/mermaid-live-editor/#/edit/eyJjb2RlIjoiZ3JhcGggVERcbiAgICBBW0NocmlzdG1hc10gLS0-fEdldCBtb25leXwgQihHbyBzaG9wcGluZylcbiAgICBCIC0tPiBDe0xldCBtZSB0aGlua31cbiAgICBDIC0tPnxPbmV8IERbTGFwdG9wXVxuICAgIEMgLS0-fFR3b3wgRVtpUGhvbmVdXG4gICAgQyAtLT58VGhyZWV8IEZbZmE6ZmEtY2FyIENhcl1cbiAgIiwibWVybWFpZCI6eyJ0aGVtZSI6ImRlZmF1bHQifSwidXBkYXRlRWRpdG9yIjpmYWxzZX0"
            Case "AST Explorer"
                Return "https://astexplorer.net/"
            Case "Visual basic"
                Return "https://rextester.com/l/visual_basic_online_compiler"
            Case "Nearly Parser"
                Return "https://omrelli.ug/nearley-playground/"
            Case "Code Script Prettier"
                Return "https://esprima.org/demo/parse.html"
            Case ".NET"
                Return "https://try.dot.net/"

        End Select
        Return Nothing
    End Function

    Private Sub ButtonRunCode_Click(sender As Object, e As EventArgs) Handles ButtonRunCode.Click
        Me.GoUrl(GetUrl(Me.ComboBoxURL.Text))
    End Sub

    Private Sub ButtonNewScrn_Click(sender As Object, e As EventArgs) Handles ButtonNewScrn.Click
        Dim FrmEmu As New EMU_MachineUI
        FrmEmu.Show()
        FrmEmu.GoUrl(GetUrl(Me.ComboBoxURL.Text))

    End Sub


End Class