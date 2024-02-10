using System;
using System.IO;
using System.Windows.Forms;

namespace LstCoder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            cbMethod.SelectedIndex = 2; // sha256
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cbLineMasks.Items.Add("---");
            cbLineMasks.Items.Add(@"\d{10}");   // 0123456789
            cbLineMasks.Items.Add(@"(\+\d+\s*){0,1}\({0,1}\d{3}\){0,1}\s*\d+(\-){0,1}\d{4}");   // +7 (999) 999-9999

            cbLineMasks.SelectedIndex = 1;
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            if( openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dp = Path.GetDirectoryName(openFileDialog.FileName) +
                "\\" +
                Path.GetFileNameWithoutExtension(openFileDialog.FileName) +
                "_coded" +
                Path.GetExtension(openFileDialog.FileName);

                txtDestination.Text = dp;
                txtSource.Text = openFileDialog.FileName;
            }
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            try
            {
                Coder coder = new Coder(txtSource.Text, txtDestination.Text, cbLineMasks.Text, cbMethod.Text);
                coder.Go();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
