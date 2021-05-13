using System;

namespace SDK
{
    public partial class EMU_MachineUI
    {
        public EMU_MachineUI()
        {
            InitializeComponent();
            _ButtonRunCode.Name = "ButtonRunCode";
            _ButtonNewScrn.Name = "ButtonNewScrn";
        }

        public void GoUrl(ref string Url)
        {
            BrowserMain.Navigate(Url);
        }

        public string GetUrl(ref string Text)
        {
            switch (Text ?? "")
            {
                case "Amiga Workbench Simulator":
                    {
                        return "https://taws.ch/​";
                    }

                case "Windows 3.1":
                    {
                        return "https://classicreload.com/win3x-windows-31.html";
                    }

                case "Windows 3.1 with CD-ROM":
                    {
                        return "https://www.pcjs.org/blog/2019/05/31/​";
                    }

                case "Macintosh System 7" == "https://archive.org/details/mac_MacOS...":
                    {
                        break;
                    }

                case "Windows 95":
                    {
                        return "https://www.pcjs.org/blog/2015/09/21/";
                    }

                case "OS/ 2":
                    {
                        return "https://www.pcjs.org/blog/2015/12/27/​";
                    }

                case "Windows 93":
                    {
                        return "http://www.windows93.net/";
                    }

                case "Atari ST":
                    {
                        return "https://jamesfriend.com.au/pce-js/ata...​";
                    }

                case "Windows 1.0":
                    {
                        return "https://www.pcjs.org/software/pcx86/s...";
                    }

                case "EmuOS":
                    {
                        return "https://emupedia.net/beta/emuos/​";
                    }

                case "Mac Oxs Lion 10":
                    {
                        return "https://www.alessioatzeni.com/mac-osx-lion-css3/";
                    }

                case "PCDOS 5":
                    {
                        return "http://jamesfriend.com.au/pce-js/ibmpc-games/";
                    }

                case "MAC os7":
                    {
                        return "https://jamesfriend.com.au/pce-js/pce-js-apps/";
                    }

                case var @case when @case == "Windows 95":
                    {
                        return "https://win95.ajf.me/win95.html";
                    }

                case "RegEX 101":
                    {
                        return "https://regex101.com/";
                    }

                case "Mermaid diagrams":
                    {
                        return "https://mermaid-js.github.io/mermaid-live-editor/#/edit/eyJjb2RlIjoiZ3JhcGggVERcbiAgICBBW0NocmlzdG1hc10gLS0-fEdldCBtb25leXwgQihHbyBzaG9wcGluZylcbiAgICBCIC0tPiBDe0xldCBtZSB0aGlua31cbiAgICBDIC0tPnxPbmV8IERbTGFwdG9wXVxuICAgIEMgLS0-fFR3b3wgRVtpUGhvbmVdXG4gICAgQyAtLT58VGhyZWV8IEZbZmE6ZmEtY2FyIENhcl1cbiAgIiwibWVybWFpZCI6eyJ0aGVtZSI6ImRlZmF1bHQifSwidXBkYXRlRWRpdG9yIjpmYWxzZX0";
                    }

                case "AST Explorer":
                    {
                        return "https://astexplorer.net/";
                    }

                case "Visual basic":
                    {
                        return "https://rextester.com/l/visual_basic_online_compiler";
                    }

                case "Nearly Parser":
                    {
                        return "https://omrelli.ug/nearley-playground/";
                    }

                case "Code Script Prettier":
                    {
                        return "https://esprima.org/demo/parse.html";
                    }

                case ".NET":
                    {
                        return "https://try.dot.net/";
                    }

                case "WASM Explorer":
                    {
                        return "https://mbebenita.github.io/WasmExplorer/";
                    }

                case "Web Assembly Studio":
                    {
                        return "https://webassembly.studio/";
                    }

                case "VB Online Compiler":
                    {
                        return "https://rextester.com/l/visual_basic_online_compiler";
                    }
            }

            return null;
        }

        private void ButtonRunCode_Click(object sender, EventArgs e)
        {
            string localGetUrl() { string argText = ComboBoxURL.Text; var ret = GetUrl(ref argText); ComboBoxURL.Text = argText; return ret; }

            string argUrl = localGetUrl();
            GoUrl(ref argUrl);
        }

        private void ButtonNewScrn_Click(object sender, EventArgs e)
        {
            var FrmEmu = new EMU_MachineUI();
            FrmEmu.Show();
            string localGetUrl() { string argText = ComboBoxURL.Text; var ret = GetUrl(ref argText); ComboBoxURL.Text = argText; return ret; }

            string argUrl = localGetUrl();
            FrmEmu.GoUrl(ref argUrl);
        }
    }
}