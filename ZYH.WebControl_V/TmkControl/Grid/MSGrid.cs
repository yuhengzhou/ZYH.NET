using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    public class MSGrid : Grid_Base
    {
        protected override void OnBuildFooter(int DataSource_Count)
        {
            _FooterRow = new TableRow(); _Grid.Rows.Add(_FooterRow);
            TableCell td = new TableCell(); _FooterRow.Cells.Add(td); td.ColumnSpan = Columns.Count; td.CssClass = this.CssClass_FooterRow;
            Panel P = new Panel(); td.Controls.Add(P);P.CssClass = "paging";
            HtmlGenericControl ul = new HtmlGenericControl("ul"); P.Controls.Add(ul);
            HtmlGenericControl li;

            li = new HtmlGenericControl("li"); ul.Controls.Add(li);
            HyperLink hk = new HyperLink(); hk.ID = "Previous"; li.Controls.Add(hk); hk.Text = "Previous"; hk.CssClass = "";
            if (PageIndex == 0) { hk.Enabled = false; } else { hk.Enabled = true; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PreviousClick();"; }

            int pageCount = (DataSource_Count / PageSize);
            if ((DataSource_Count % PageSize) != 0) pageCount++;

            int start; int end;
            if (PageIndex < 9) { start = 0; } else { start = (PageIndex / 10) * 10; }
            end = start + 10;
            if (end > pageCount)
            {
                end = pageCount; start = end - 10;
                if (start < 0) start = 0;
            }
            if (start > 0)
            {
                li = new HtmlGenericControl("li"); ul.Controls.Add(li);
                hk = new HyperLink(); hk.ID = "hkf"; li.Controls.Add(hk); hk.Text = "1"; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(0);";
                li = new HtmlGenericControl("li"); ul.Controls.Add(li);
                hk = new HyperLink(); hk.ID = "hks"; li.Controls.Add(hk); hk.Text = "..."; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + (start - 1).ToString() + ");";
            }
            for (int i = start; i < end; i++)
            {
                Label lb;
                if (i == PageIndex)
                {
                    li = new HtmlGenericControl("li"); ul.Controls.Add(li);
                    lb = new Label(); lb.ID = "l" + i.ToString(); li.Controls.Add(lb); lb.Text = (i + 1).ToString(); lb.CssClass = "this";
                }
                else
                {
                    li = new HtmlGenericControl("li"); ul.Controls.Add(li);
                    hk = new HyperLink(); hk.ID = "p" + i.ToString(); li.Controls.Add(hk); hk.Text = (i + 1).ToString(); hk.CssClass = "";
                    hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + i.ToString() + ");";
                }
            }
            if (end < pageCount - 1)
            {
                li = new HtmlGenericControl("li"); ul.Controls.Add(li);
                hk = new HyperLink(); hk.ID = "hke"; li.Controls.Add(hk); hk.Text = "..."; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + (end).ToString() + ");";
                li = new HtmlGenericControl("li"); ul.Controls.Add(li);
                hk = new HyperLink(); hk.ID = "hkl"; li.Controls.Add(hk); hk.Text = pageCount.ToString(); hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + (pageCount - 1).ToString() + ");"; hk.CssClass = "";
            }

            li = new HtmlGenericControl("li"); ul.Controls.Add(li);
            hk = new HyperLink(); hk.ID = "Next"; li.Controls.Add(hk); hk.Text = "Next";
            if (PageIndex == pageCount - 1) { hk.Enabled = false; } else { hk.Enabled = true; hk.NavigateUrl = "javascript:" + ClientID + "Instance.NextClick();"; }
        }
    }
}
