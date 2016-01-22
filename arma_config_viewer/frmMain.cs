using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arma_config_viewer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void loadNewConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            dlgFile.Filter = "ArmaConfigFile|config.cpp|CPP Files (*.cpp)|*.cpp|All files (*.*)|*.*";
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlgFile.FileNames)
                {
                    Parser config_parser = new Parser(file);
                    BuildTreeView(config_parser.ConfigFile);
                    
                }
            }
        }

        private void BuildTreeView(ConfigFile data)
        {
            trvObj.Nodes.Clear();

            for (int i = 0; i < data.Entries.Count; i++)
            {
                TreeNode node = new TreeNode(data.Entries[i].ClassWithInheritName, TreeviewChilds(data.Entries[i]));
                node.Tag = data.Entries[i];
                trvObj.Nodes.Add(node);

            }
        }

        private TreeNode[] TreeviewChilds(ConfigEntry configObj)
        {

            if (configObj.Childs.Count > 0)
            {
                TreeNode[] nodes = new TreeNode[configObj.Childs.Count];


                for (int i =0;i < configObj.Childs.Count;i++)
                {
                    nodes[i] = new TreeNode(configObj.Childs[i].ClassWithInheritName, TreeviewChilds(configObj.Childs[i]));
                    nodes[i].Tag = configObj.Childs[i];
                }

                return nodes;
            } else
            {
                return new TreeNode[0];
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trvObj_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ConfigEntry config = (ConfigEntry)e.Node.Tag;
            List<ConfigEntry> baseClasses = config.Inherits();
            List<string> values;
            ListViewItem lItem;

            string value = "";

            lstDetail.Items.Clear();
            lstDetail.Columns.Clear();

            lstDetail.Columns.Add("Property");



            //string props
            List<string> all_props = new List<string>();

            all_props.AddRange(config.Data_prop.Keys.ToList());

            foreach (ConfigEntry obj in baseClasses)
            {
                lstDetail.Columns.Add(obj.ClassName); //add column for object
                all_props.AddRange(obj.Data_prop.Keys.ToList());
            }

            //reduce to unique entries
            all_props = all_props.Distinct().ToList();

            foreach (string keyval in all_props)
            {
                lItem = new ListViewItem(keyval);
                values = new List<string>();

                foreach (ConfigEntry obj in baseClasses)
                {
                    if (obj.Data_prop.TryGetValue(keyval, out value))
                    {
                        values.Add(value);
                    }
                    else
                    {
                        values.Add("");
                    }
                }

                lItem.SubItems.AddRange(values.ToArray());

                if (!String.IsNullOrEmpty(values[0]))
                {
                    for (int i = 1; i < values.Count; i++)
                    {
                        if (!values[0].Equals(values[i]) && !String.IsNullOrEmpty(values[i]))
                        {
                            lItem.SubItems[0].ForeColor = Color.Red;
                        }
                    }
                }


                lstDetail.Items.Add(lItem);
            }
        }
    }
}
