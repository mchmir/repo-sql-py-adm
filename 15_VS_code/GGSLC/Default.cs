using GGSLC.Eservice;
using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GGSLC
{
	public class Default : System.Web.UI.Page
	{
		private string lang = "ru";

		private EService eservice;

		protected HtmlForm form1;

		protected Button btn_Enter0;

		protected HiddenField HiddenField1;

		protected MultiView MultiView1;

		protected View View1;

		protected TextBox txt_login;

		protected ImageButton btn_Enter;

		protected LinkButton hl_req;

		protected LinkButton hl_pwd;

		protected LinkButton lbtn_kaz;

		protected LinkButton lbtn_ru;

		protected Label l_error;

		protected View View2;

		protected TextBox txt_login0;

		protected LinkButton LinkButton1;

		protected LinkButton LinkButton2;

		protected ImageButton btn_Enter1;

		public Default()
		{
		}

		protected void btn_Enter_Click(object sender, EventArgs e)
		{
		}

		protected void btn_Enter_Click(object sender, ImageClickEventArgs e)
		{
			this.enter(this.txt_login, "pwdin");
		}

		protected void btn_Enter1_Click(object sender, ImageClickEventArgs e)
		{
			this.enter(this.txt_login0, "pwdin0");
		}

		private void enter(TextBox login, string pwdtxtname)
		{
			try
			{
				if (this.eservice == null)
				{
					this.eservice = new EService();
				}
				string str = this.eservice.PCLogin(login.Text, this.GetHashString(base.Request.Form[pwdtxtname].Trim()));
				if (!string.IsNullOrEmpty(str))
				{
					this.HiddenField1.Value = string.Concat(str, login.Text, ";", this.lang);
					if (base.Request.ServerVariables["HTTP_USER_AGENT"].IndexOf("Windows") <= 0)
					{
						base.Server.Transfer("mpage.aspx");
					}
					else
					{
						base.Server.Transfer("MainPage.aspx");
					}
				}
				else if (this.lang != "ru")
				{
					this.l_error.Text = "Қателік орын алды. Кейінірек байқап көріңіз";
				}
				else
				{
					this.l_error.Text = "Проверьте правильность введенных данных";
				}
			}
			catch (Exception exception)
			{
				if (this.lang != "ru")
				{
					this.l_error.Text = "Қателік орын алды. Кейінірек байқап көріңіз";
				}
				else
				{
					this.l_error.Text = "Произошла ошибка. Попробуйте позднее";
				}
			}
		}

		private string GetHashString(string s)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(s);
			byte[] numArray = (new MD5CryptoServiceProvider()).ComputeHash(bytes);
			string empty = string.Empty;
			byte[] numArray1 = numArray;
			for (int i = 0; i < (int)numArray1.Length; i++)
			{
				byte num = numArray1[i];
				empty = string.Concat(empty, string.Format("{0:x2}", num));
			}
			return empty;
		}

		protected void hl_pwd_Click(object sender, EventArgs e)
		{
			try
			{
				this.HiddenField1.Value = this.lang;
				base.Server.Transfer("LostPassword.aspx");
			}
			catch (Exception exception)
			{
			}
		}

		protected void hl_req_Click(object sender, EventArgs e)
		{
			try
			{
				this.HiddenField1.Value = this.lang;
				base.Server.Transfer("RequestForm.aspx");
			}
			catch (Exception exception)
			{
			}
		}

		protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
		{
		}

		protected void lbtn_kaz_Click(object sender, EventArgs e)
		{
			this.lang = "kz";
			this.Page_Load(null, null);
		}

		protected void lbtn_ru_Click(object sender, EventArgs e)
		{
			this.lang = "ru";
			this.Page_Load(null, null);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.ServerVariables["HTTP_USER_AGENT"].IndexOf("Windows") <= 0)
				{
					this.MultiView1.ActiveViewIndex = 1;
				}
				else
				{
					this.MultiView1.ActiveViewIndex = 0;
				}
			}
			if (this.lang == "ru")
			{
				base.Title = "Личный кабинет Горгаз-Сервис";
				this.hl_req.Text = "Подать заявку на регистрацию";
				this.hl_pwd.Text = "Восстановить пароль";
				this.btn_Enter.ImageUrl = "~/images/login.png";
				return;
			}
			base.Title = "Жеке кабинет Горгаз-Сервис";
			this.hl_req.Text = "Тіркелуге өтінім беру";
			this.hl_pwd.Text = "Құпиясөзді қалпына келтіру";
			this.btn_Enter.ImageUrl = "~/images/loginkz2.png";
		}
	}
}