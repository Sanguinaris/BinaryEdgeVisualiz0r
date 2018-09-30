using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleJsonParser
{
    class ListViewItemComparer : IComparer
    {
        private int col;
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
            ((ListViewItem)y).SubItems[col].Text);
            return returnVal;
        }
    }

    class ListViewIntComparer : IComparer
    {
        private int col;
        public ListViewIntComparer()
        {
            col = 0;
        }
        public ListViewIntComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return int.Parse(((ListViewItem)x).SubItems[col].Text) -
           int.Parse(((ListViewItem)y).SubItems[col].Text);
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                uint curUpdatePhase = 1;
                bool hasBeenRun = false;
                listView1.Items.Clear();
                using (var fs = new StreamReader(openFileDialog1.FileName))
                {
                    try
                    {
                        while (fs.Peek() >= 0)
                        {
                            var str = fs.ReadLine();
                            using (var strm = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(str)))
                            {
                                var deserializer = new DataContractJsonSerializer(typeof(Json));
                                Json json = (Json)deserializer.ReadObject(strm);

                                if (!hasBeenRun)
                                {
                                    if (curUpdatePhase++ %2 == 0)
                                        hasBeenRun = true;
                                    OhHaiMark.Text = $"Hai {json.origin.client_id}";
                                    label1.Text = $"Your job {json.origin.job_id} gave us this shit:";
                                }
                                else {
                                    if (curUpdatePhase++ % 4 == 0)
                                        Application.DoEvents();

                                    if (json.origin.type != "ssh")
                                        continue;
                                     var item = new ListViewItem(json.origin.ip);
                                     item.SubItems.Add(json.origin.type);
                                     item.SubItems.Add(json.target.ip);
                                     item.SubItems.Add(json.target.port.ToString());

                                     listView1.Items.Add(item);
                                }
                            }
                        }
                    }catch(Exception ex)
                    {
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            for (var idx = 0; idx < listView1.SelectedItems.Count; ++idx)
            {
                Clipboard.SetText(Clipboard.GetText() + System.Environment.NewLine + listView1.SelectedItems[idx].SubItems[2].Text + ":" + listView1.SelectedItems[idx].SubItems[3].Text);
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if(listView1.Columns[e.Column].Text == "in the")
                this.listView1.ListViewItemSorter = new ListViewIntComparer(e.Column);
            else
                this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
            listView1.Sort();
        }
    }
}
