using GGSLC.Eservice;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GGSLC
{
	public class LostPassword : System.Web.UI.Page
	{
		private EService eservice;

		private string lang = "ru";

		protected HtmlForm form1;

		protected Label Label5;

		protected TextBox txt_account;

		protected Label Label4;

		protected TextBox txt_email;

		protected ImageButton btn_SendForm;

		protected RequiredFieldValidator RequiredFieldValidator1;

		protected RequiredFieldValidator RequiredFieldValidator3;

		protected RegularExpressionValidator RegularExpressionValidator2;

		protected RegularExpressionValidator RegularExpressionValidator1;

		protected Label l_error;

		public LostPassword()
		{
		}

		protected void btn_SendForm_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				if (this.eservice == null)
				{
					this.eservice = new EService();
				}
				if (this.eservice.PCLostPWD(this.txt_account.Text, this.txt_email.Text, "0", "111") == "Успешно")
				{
					if (this.lang != "ru")
					{
						this.l_error.Text = "Сіздің құпиясөзіңіз сәтті жаңартылды";
					}
					else
					{
						this.l_error.Text = "Ваш пароль успешно обновлен. Данные для входа в систему у Вас в почте";
					}
				}
				else if (this.lang != "ru")
				{
					this.l_error.Text = "Құпиясөзді жаңарту кезіндегі қате";
				}
				else
				{
					this.l_error.Text = "Ваш пароль не обновлен. Попробуйте позднее";
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

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				HiddenField hiddenField = (HiddenField)this.Page.PreviousPage.FindControl("HiddenField1");
				this.lang = hiddenField.Value;
			}
			catch (Exception exception)
			{
			}
			if (this.lang == "ru")
			{
				base.Title = "Личный кабинет Горгаз-Сервис";
				this.Label5.Text = "Укажите Ваш Л/С";
				this.Label4.Text = "Укажите Ваш email";
				this.btn_SendForm.ImageUrl = "~/images/send3.png";
				this.RequiredFieldValidator1.ErrorMessage = "Не заполнено поле Л/С";
				this.RequiredFieldValidator3.ErrorMessage = "Не заполнено поле email";
				this.RegularExpressionValidator1.ErrorMessage = "Проверьте правильность email";
				this.RegularExpressionValidator2.ErrorMessage = "Проверьте правильность Л/С";
				return;
			}
			base.Title = "Жеке кабинет Горгаз-Сервис";
			this.Label5.Text = "Сіздің Д/Ш көрсетіңіз";
			this.Label4.Text = "Сіздің email-ыңызды көрсетіңіз";
			this.btn_SendForm.ImageUrl = "~/images/s22kz.png";
			this.RequiredFieldValidator1.ErrorMessage = "Өріс бос Д/Ш";
			this.RequiredFieldValidator3.ErrorMessage = "Өріс бос email";
			this.RegularExpressionValidator1.ErrorMessage = "email тексеріңіз";
			this.RegularExpressionValidator2.ErrorMessage = "Д/Ш тексеріңіз";
		}
	}
}