using GGSLC.Eservice;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GGSLC
{
	public class RequestForm : System.Web.UI.Page
	{
		private EService eservice;

		private string lang = "ru";

		protected HtmlForm form1;

		protected Label Label5;

		protected TextBox txt_account;

		protected RequiredFieldValidator RequiredFieldValidator1;

		protected Label Label1;

		protected TextBox txt_fio;

		protected RequiredFieldValidator RequiredFieldValidator4;

		protected Label Label2;

		protected TextBox txt_phone;

		protected RequiredFieldValidator RequiredFieldValidator5;

		protected Label Label3;

		protected TextBox txt_adres;

		protected RequiredFieldValidator RequiredFieldValidator2;

		protected Label Label4;

		protected TextBox txt_email;

		protected RequiredFieldValidator RequiredFieldValidator3;

		protected ImageButton btn_SendForm;

		protected RegularExpressionValidator RegularExpressionValidator1;

		protected RegularExpressionValidator RegularExpressionValidator2;

		protected RegularExpressionValidator RegularExpressionValidator3;

		protected Label l_error;

		public RequestForm()
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
				if (this.txt_phone.Text.Length <= 5 || this.txt_email.Text.Length <= 5 || this.txt_account.Text.Length <= 5 || this.txt_fio.Text.Length <= 5 || this.txt_adres.Text.Length <= 5)
				{
					if (this.lang != "ru")
					{
						this.l_error.Text = "Телефон нөмірін тексеріңіз";
					}
					else
					{
						this.l_error.Text = "Проверьте правильность данных.";
					}
				}
				else if (this.txt_email.Text.IndexOf('@') <= -1 || this.txt_email.Text.IndexOf('.') <= -1)
				{
					if (this.lang != "ru")
					{
						this.l_error.Text = "Телефон нөмірін тексеріңіз";
					}
					else
					{
						this.l_error.Text = "Проверьте правильность данных.";
					}
				}
				else if (this.eservice.PCSaveRequest(this.txt_account.Text, this.txt_fio.Text, this.txt_phone.Text, this.txt_adres.Text, this.txt_email.Text) == "Успешно")
				{
					if (this.lang != "ru")
					{
						this.l_error.Text = "Сіздің өтінішіңіз қабылданды. Жақын арада біз сізбен сіздермен және қолдану нәтижесі туралы хабарлайды.";
					}
					else
					{
						this.l_error.Text = "Ваша заявка принята. В ближайшее время мы с Вами свяжемся и сообщим о результате рассмотрения заявки.";
					}
				}
				else if (this.lang != "ru")
				{
					this.l_error.Text = "Егер сіз ағымдағы өтініш бар ма. алдыңғы қолдану аяқталғаннан күтіңіз";
				}
				else
				{
					this.l_error.Text = "У Вас есть текущая заявка. Пожалуйста ожидайте завершения предыдущей заявки.";
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
				this.Label1.Text = "Укажите Ваши Ф.И.О.";
				this.Label2.Text = "Укажите Ваш телефон";
				this.Label3.Text = "Укажите Ваш адрес";
				this.Label4.Text = "Укажите Ваш email";
				this.btn_SendForm.ImageUrl = "~/images/send3.png";
				this.RequiredFieldValidator1.ErrorMessage = "Не заполнено поле Л/С";
				this.RequiredFieldValidator2.ErrorMessage = "Не заполнено поле адрес";
				this.RequiredFieldValidator3.ErrorMessage = "Не заполнено поле email";
				this.RequiredFieldValidator4.ErrorMessage = "Не заполнено поле Ф.И.О.";
				this.RequiredFieldValidator5.ErrorMessage = "Не заполнено поле телефон";
				this.RegularExpressionValidator1.ErrorMessage = "Проверьте правильность email";
				this.RegularExpressionValidator2.ErrorMessage = "Проверьте правильность Л/С";
				return;
			}
			base.Title = "Жеке кабинет Горгаз-Сервис";
			this.Label5.Text = "Сіздің Д/Ш көрсетіңіз";
			this.Label1.Text = "Сіздің Т.А.Ә. көрсетіңіз";
			this.Label2.Text = "Сіздің телефоныңызды көрсетіңіз";
			this.Label3.Text = "Сіздің мекенжайыңызды көрсетіңіз";
			this.Label4.Text = "Сіздің email-ыңызды көрсетіңіз";
			this.btn_SendForm.ImageUrl = "~/images/s22kz.png";
			this.RequiredFieldValidator1.ErrorMessage = "Өріс бос Д/Ш";
			this.RequiredFieldValidator2.ErrorMessage = "Өріс бос мекенжайың";
			this.RequiredFieldValidator3.ErrorMessage = "Өріс бос email";
			this.RequiredFieldValidator4.ErrorMessage = "Өріс бос Т.А.Ә";
			this.RequiredFieldValidator5.ErrorMessage = "Өріс бос телефон";
			this.RegularExpressionValidator1.ErrorMessage = "email тексеріңіз";
			this.RegularExpressionValidator2.ErrorMessage = "Д/Ш тексеріңіз";
		}
	}
}