using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetPath
{
    public partial class GetPath : Form
    {

        private int mouseX, mouseY;

        public GetPath()
        {
            InitializeComponent();
        }

        private void GetPath_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseY = e.Y;
                this.mouseX = e.X;
            }
        }

        private void GetPath_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            GetPath_MouseDown(sender, e);
        }

        private void li_mv(object sender, MouseEventArgs e)
        {
            GetPath_MouseMove(sender, e);
        }

        private void la_modw(object sender, MouseEventArgs e)
        {
            GetPath_MouseDown(sender, e);
        }

        private void la_momv(object sender, MouseEventArgs e)
        {
            GetPath_MouseMove(sender, e);
        }

        private void GetPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void GetPath_DragDrop(object sender, DragEventArgs e)
        {
            //D&Dされたとき
            string[] path = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (path.Length <= 0)
            {
                return;
            }
            Clipboard.SetDataObject(path[0], true);

            for (int i = 0; i < path.Length; ++i)
            {
                listBox.Items.Add(path[i]);//リストボックスに追加
            }
        }

        private void listBox_DragDrop(object sender, DragEventArgs e)
        {
            GetPath_DragDrop(sender,e);
        }

        private void listBox_DragEnter(object sender, DragEventArgs e)
        {
            GetPath_DragEnter(sender, e);
        }

        private void la_dr(object sender, DragEventArgs e)
        {
            GetPath_DragDrop(sender, e);
        }

        private void la_en(object sender, DragEventArgs e)
        {
            GetPath_DragEnter(sender, e);
        }

        private void check1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.TopMost == true)
            {
                this.TopMost = false;
            }
            else
            {
                this.TopMost = true;
            }
        }

        private void listBox_MouseUp(object sender, MouseEventArgs e)
        {
            //右クリックされたら
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int index = listBox.IndexFromPoint(e.Location);

                if (index >= 0)
                {
                    listBox.ClearSelected();

                    listBox.SelectedIndex = index;

                    Point pos = listBox.PointToScreen(e.Location);
                    contextMenuStrip1.Show(pos);
                }
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(listBox.Text, true);
        }

        private void Kiritori_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(listBox.Text, true);
            int sel = listBox.SelectedIndex;
            listBox.Items.RemoveAt(sel);
        }

        private void Del_Click(object sender, EventArgs e)
        {
            int sel = listBox.SelectedIndex;
            listBox.Items.RemoveAt(sel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
        }
    }
}
