using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Inv
{
    public partial class BinaryTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Array a;
            List<int> b=new List<int>();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var tree = new Cls_BinaryTree(values);
        }
    }
    public class Cls_BinaryTree
    {
        int value;
        Cls_BinaryTree left;
        Cls_BinaryTree right;

        public Cls_BinaryTree(int[] values) : this(values, 0) { }

        Cls_BinaryTree(int[] values, int index)
        {
            Load(this, values, index);
        }

        void Load(Cls_BinaryTree tree, int[] values, int index)
        {
            this.value = values[index];
            if (index * 2 + 1 < values.Length)
            {
                this.left = new Cls_BinaryTree(values, index * 2 + 1);
            }
            if (index * 2 + 2 < values.Length)
            {
                this.right = new Cls_BinaryTree(values, index * 2 + 2);
            }
        }
    }

}
