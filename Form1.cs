using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;
namespace $safeprojectname$
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Getting installation folder from user
            folderBrowserDialog1.Description = "Browse for Installation folder of NPad";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assigning operators
                string installation_folder = folderBrowserDialog1.SelectedPath;
                if (File.Exists(installation_folder + @"\NPad.exe"))
                {
                    // Start the Patching..

                    // Export our Resource file
                    File.WriteAllBytes("NPad.exe", $safeprojectname$.Properties.Resources.NPad);

                    // Creating a Backup..
                    File.Move(installation_folder + @"\NPad.exe", installation_folder + @"\NPad.exe.bak");
                    // Move our Patch..
                    File.Move("NPad.exe", installation_folder + @"\NPad.exe");
                    // Generate of success..
                    MessageBox.Show("Patched Successfully!");
                } else
                {
                    MessageBox.Show("Wrong Installation folder!");
                }
            }
        }
        public bool get_admin_rights()
        {
            bool isElevated=false;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return isElevated;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bool adminrights = get_admin_rights();
            if (adminrights == false)
            {
                button1.Enabled = false;
                button1.Text = "Please Run this with Admin Permission";
            }
        }
    }
}
