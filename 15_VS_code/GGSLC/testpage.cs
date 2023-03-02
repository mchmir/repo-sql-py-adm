using AjaxControlToolkit;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GGSLC
{
	public class testpage : System.Web.UI.Page
	{
		protected HtmlForm form1;

		protected Accordion Accordion1;

		protected AccordionPane AccordionPane6;

		protected AccordionPane AccordionPane7;

		protected AccordionPane AccordionPane8;

		protected Button Button1;

		public testpage()
		{
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 5; i++)
			{
				AccordionPane accordionPane = new AccordionPane()
				{
					ID = string.Concat("pane1", i)
				};
				accordionPane.HeaderContainer.Controls.Add(new LiteralControl(string.Concat("Pane 1", i)));
				accordionPane.ContentContainer.Controls.Add(new LiteralControl("This is Accordion Pane No 1"));
				this.Accordion1.Panes.Add(accordionPane);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}
	}
}