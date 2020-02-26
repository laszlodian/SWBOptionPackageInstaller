using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWB_OptionPackageInstaller
{
    public partial class MainForm : Form
    {
        #region Properties
        private string pathOfSWB;

        public string PathOfSWB
        {
            get { return pathOfSWB; }
            set { pathOfSWB = value; }
        }

        private string pathOfOptionPackages;

        public string PathOfOptionPackages
        {
            get { return pathOfOptionPackages; }
            set { pathOfOptionPackages = value; }
        }
       
        public DataGridView DataGridViewOfArtifacts { get { return dgv; } set{dgv=value; } }

        #endregion


        #region Variables
        public DirectoryInfo tempDir;
        public static MainForm Instance = null;
        public DataGridView dgv = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        #endregion

        public MainForm()
        {
            Instance = this;
            InitializeComponent();

            dgv.Dock = DockStyle.Top;
            dgv.BackgroundColor = Color.Fuchsia;      
            this.tabControl1.TabPages[1].Controls.Add(dgv);    
        
            tbPathOfOptionpackages.Text = @"C:\_SWB\optionpackages";
            tbPathOfSWB.Text = @"C:\_SWB\SWB";
        }

        public void ConfigureDataGridView()
        {
            foreach (string pkg in CommandControler.Instance.OptionPackageList.Split(' '))
            {
                bindingSource1.Add(new PackageGridModel(pkg, "version",new DataGridViewCheckBoxCell()));
                
            }

            dgv.Font = new Font(dgv.Font, FontStyle.Bold);

            this.Load += new System.EventHandler(EnumsAndComboBox_Load_For_All);
        }

        public void EnumsAndComboBox_Load_For_All(object sender, EventArgs e)
        {
            // Initialize the DataGridView.
               dgv.AutoGenerateColumns = false;
               dgv.AutoSize = true;
               dgv.DataSource = bindingSource1;

            DataGridViewColumn column;


            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "OptionPackageName";
            column.Name = "Option Package Elnevezése";
            dgv.Columns.Add(column);


            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Version";
            column.Name = "Verzió";
            dgv.Columns.Add(column);

            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "InstallCell";
            column.Name = "Install";
            column.Width = 140;
            dgv.Columns.Add(column);

            // Initialize the form.
            this.Controls.Add(dgv);
            this.AutoSize = true;
            this.Text = "Founded option packages - Choose which of them to install to SunriseWorkBench";
        }

        private void tbPathOfSWB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pathOfSWB = tbPathOfSWB.Text.Trim().Replace('\\','/');
                if (!CheckStatesHandler.Instance.CheckPath(PathOfSWB))
                    return;

                btStart_Click(null, EventArgs.Empty);
            }

        }

        private void tbPathOfOptionpackages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pathOfOptionPackages = tbPathOfOptionpackages.Text.Trim().Replace('\\', '/'); 
                if (!CheckStatesHandler.Instance.CheckPath(pathOfOptionPackages))
                    return;

                CommandControler.Instance.OptionPackageList= ArtifactHandler.Instance.CollectOptionPackages(PathOfOptionPackages);
                ConfigureDataGridView();
                btStart_Click(null, EventArgs.Empty);
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (!CheckStatesHandler.Instance.CheckPath(tbPathOfOptionpackages.Text.Trim()) || !CheckStatesHandler.Instance.CheckPath(tbPathOfSWB.Text.Trim()))
            {
                return;
            }
            else
            {
                PathOfOptionPackages = tbPathOfOptionpackages.Text.Trim().Replace('\\', '/'); ;
                PathOfSWB = tbPathOfSWB.Text.Trim().Replace('\\', '/'); ;
            }


            CommandControler.Instance.InstallOptionPackages();

        }

      
    }
}
