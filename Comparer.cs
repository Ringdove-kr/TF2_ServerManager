using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Collections;

namespace TF2_ServerManager
{
    class ListViewItemComparer : IComparer
    {
        private int col;
        public string sort = "asc";
        public ListViewItemComparer()
        {
            col = 0;
        }

        public ListViewItemComparer(int column, string sort)
        {
            col = column;
            this.sort = sort;
        }

        public int Compare(object x, object y)
        {
            if(col == (int)PlayerListItems.Score)
            {
                if (sort == "asc")
                {
                    return int.Parse(((ListViewItem)x).SubItems[col].Text) - int.Parse(((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return int.Parse(((ListViewItem)y).SubItems[col].Text) - int.Parse(((ListViewItem)x).SubItems[col].Text);
                }
            }
            else if (col == (int)PlayerListItems.Time)
            {
                string[] xString = ((ListViewItem)x).SubItems[col].Text.Split(':');
                string[] yString = ((ListViewItem)x).SubItems[col].Text.Split(':');

                int xTime = int.Parse(xString[0]) * 3600 + int.Parse(xString[1]) * 60 + int.Parse(xString[2]);
                int yTime = int.Parse(yString[0]) * 3600 + int.Parse(yString[1]) * 60 + int.Parse(yString[2]);

                if (sort == "asc")
                {
                    return xTime - yTime;
                }
                else
                {
                    return yTime - xTime;
                }
            }
            else
            {
                if (sort == "asc")
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }
    }
}
