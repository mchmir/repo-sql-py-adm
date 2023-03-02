using GGSLC.Eservice;
using GGSLC.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GGSLC
{
	public class mobile : System.Web.UI.Page
	{
		private string prevpageinfo = "";

		private string ls = "";

		private string fio = "";

		private string adres = "";

		private string curBalance = "";

		private string statusObject = "";

		private string ConsamerPhone = "";

		private string meterName = "";

		private string meterType = "";

		private string meterNomer = "";

		private string meterDateCreate = "";

		private string meterDateCheck = "";

		private string LastIndication = "";

		private string DateIndication = "";

		private string idgmeter = "";

		private static bool needchgPWD;

		private static bool needverif;

		private string lang = "ru";

		private EService eservice;

		protected System.Web.UI.WebControls.Image Image1;

		protected LinkButton LinkButton1;

		protected LinkButton LinkButton4;

		protected LinkButton LinkButton6;

		protected LinkButton LinkButton2;

		protected LinkButton LinkButton3;

		protected LinkButton LinkButton8;

		protected LinkButton LinkButton5;

		protected Label l_fio;

		protected Label l_adress;

		protected Label l_account;

		protected MultiView MultiView1;

		protected View View1;

		protected Label l_balanceInfo;

		protected Label l_info;

		protected Label l_inf;

		protected Label l_inf0;

		protected Label l_prUcheta;

		protected System.Web.UI.WebControls.Image img_vz;

		protected System.Web.UI.WebControls.Image img_kvz;

		protected Label l_devname;

		protected Label l_meterName;

		protected Label l_devnomer;

		protected Label l_meternomer;

		protected Label l_DateCreate;

		protected Label l_meterDateCreate;

		protected Label l_DateVer;

		protected Label l_meterDateCheck;

		protected View View2;

		protected Label l_pokaz;

		protected Label l_curPokaz;

		protected Label l_prevPokaz;

		protected Label l_DatePokaz;

		protected Label l_newPokaz;

		protected TextBox txt_NewInd;

		protected ImageButton btn_SendPokaz;

		protected Label l_sendInd;

		protected CheckBox chb_confirm10;

		protected View View3;

		protected Label l_operc;

		protected Panel Panel3;

		protected GridView GridView1;

		protected View View4;

		protected Label l_smenPar;

		protected Label l_newpwd;

		protected Label l_confNewpwd;

		protected ImageButton btn_ChangePWD;

		protected Label l_needchgPWD;

		protected Label l_changePWD;

		protected View View5;

		protected Label l_otzuv;

		protected Panel p_theme;

		protected TextBox txt_messTitle;

		protected Panel p_mess;

		protected TextBox txt_MessBody;

		protected Label Label9;

		protected ImageButton btn_SaveComment;

		protected View View6;

		protected Label l_balanceInfo1;

		protected Label l_shetDate;

		protected ImageButton btn_MakeShet;

		protected Label l_sendInd0;

		protected Panel Panel4;

		protected System.Web.UI.WebControls.Image Image2;

		protected System.Web.UI.WebControls.Image Image3;

		static mobile()
		{
		}

		public mobile()
		{
		}

		protected void btn_ChangePWD_Click(object sender, ImageClickEventArgs e)
		{
			string item = base.Request.Form["txt_newPWD"];
			string str = base.Request.Form["txt_confNewPWD"];
			if (!string.IsNullOrEmpty(item))
			{
				if (item.Length <= 7)
				{
					if (this.lang != "ru")
					{
						this.l_changePWD.Text = "Құпиясөз өте қысқа. Ең кемінде 8 символ";
					}
					else
					{
						this.l_changePWD.Text = "Слишком короткий пароль. Минимум 8 символов";
					}
				}
				else if (str == item)
				{
					if (this.eservice == null)
					{
						this.eservice = new EService();
					}
					if (this.eservice.PCChabgePWD(this.ls, this.GetHashString(item), "1", "2") == "Успешно")
					{
						if (this.lang != "ru")
						{
							this.l_changePWD.Text = "Сіздің құпиясөзіңіз сәтті жаңартылды";
						}
						else
						{
							this.l_changePWD.Text = "Ваш пароль успешно обновлен";
						}
						string str1 = "";
						item = str1;
						str = str1;
					}
					else if (this.lang != "ru")
					{
						this.l_changePWD.Text = "Құпиясөзді жаңарту кезіндегі қате";
					}
					else
					{
						this.l_changePWD.Text = "Ошибка при обновлении пароля";
					}
				}
				else if (this.lang != "ru")
				{
					this.l_changePWD.Text = "Құпиясөздер сәйкес келмейді";
				}
				else
				{
					this.l_changePWD.Text = "Пароли не совпадают";
				}
			}
			this.MultiView1.ActiveViewIndex = 4;
			this.l_changePWD.Visible = true;
		}

		protected void btn_CreateCard_Click(object sender, EventArgs e)
		{
		}

		protected void btn_CreateShet_Click(object sender, EventArgs e)
		{
			if (this.lang != "ru")
			{
				DateTimeFormatInfo dateTimeFormat = (new CultureInfo("kk-KZ")).DateTimeFormat;
				Label lShetDate = this.l_shetDate;
				object[] year = new object[] { DateTime.Now.Year, " жылдың  ", null, null, null };
				DateTime now = DateTime.Now;
				year[2] = dateTimeFormat.GetMonthName(now.Month - 1).Substring(0, 1).ToUpper();
				DateTime dateTime = DateTime.Now;
				year[3] = dateTimeFormat.GetMonthName(dateTime.Month - 1).Substring(1);
				year[4] = " айына ";
				lShetDate.Text = string.Concat(year);
			}
			else
			{
				DateTimeFormatInfo dateTimeFormatInfo = (new CultureInfo("ru-RU")).DateTimeFormat;
				Label label = this.l_shetDate;
				object[] monthName = new object[] { "За ", null, null, null, null };
				DateTime now1 = DateTime.Now;
				monthName[1] = dateTimeFormatInfo.GetMonthName(now1.Month - 1);
				monthName[2] = " месяц ";
				monthName[3] = DateTime.Now.Year;
				monthName[4] = " года";
				label.Text = string.Concat(monthName);
			}
			this.MultiView1.ActiveViewIndex = 6;
		}

		protected void btn_Main_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 0;
		}

		protected void btn_MakeCard_Click(object sender, ImageClickEventArgs e)
		{
		}

		protected void btn_MakeShet_Click(object sender, ImageClickEventArgs e)
		{
			Paragraph paragraph;
			Paragraph paragraph1;
			Paragraph paragraph2;
			Paragraph paragraph3;
			object obj;
			if (this.eservice == null)
			{
				this.eservice = new EService();
			}
			EService eService = this.eservice;
			string str = this.ls;
			string str1 = DateTime.Now.Year.ToString();
			int month = DateTime.Now.Month - 1;
			string str2 = eService.PCGetData(str, str1, month.ToString(), "1");
			if (!string.IsNullOrEmpty(str2))
			{
				string[] strArrays = str2.Split(new char[] { ';' });
				if ((int)strArrays.Length > 1)
				{
					BaseFont baseFont = BaseFont.CreateFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF"), "Identity-H", false);
					iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12f, 0);
					BaseColor gRAY = BaseColor.GRAY;
					BaseColor bLACK = BaseColor.BLACK;
					iTextSharp.text.Font font1 = new iTextSharp.text.Font(baseFont, 9f, 0, gRAY);
					iTextSharp.text.Font font2 = new iTextSharp.text.Font(baseFont, 10f, 1, bLACK);
					iTextSharp.text.Font font3 = new iTextSharp.text.Font(baseFont, 9f, 1, bLACK);
					iTextSharp.text.Font font4 = new iTextSharp.text.Font(baseFont, 9f, 0, bLACK);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (Document document = new Document(PageSize.A4, 5f, 5f, 30f, 30f))
						{
							using (PdfWriter instance = PdfWriter.GetInstance(document, memoryStream))
							{
								document.Open();
								Bitmap bitmap = new Bitmap(Resources.logos);
								iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(mobile.ImageToByte(bitmap));
								image.SetAbsolutePosition(470f, 790f);
								instance.DirectContent.AddImage(image);
								iTextSharp.text.Image instance1 = iTextSharp.text.Image.GetInstance(mobile.ImageToByte(bitmap));
								instance1.SetAbsolutePosition(470f, 640f);
								instance.DirectContent.AddImage(instance1);
								PdfContentByte directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(80f, 800f);
								directContent.LineTo(80f, 400f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(10f, 680f);
								directContent.LineTo(document.PageSize.Width - 10f, 680f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(190f, 590f);
								directContent.LineTo(document.PageSize.Width - 10f, 590f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(190f, 565f);
								directContent.LineTo(document.PageSize.Width - 10f, 565f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(190f, 590f);
								directContent.LineTo(190f, 565f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(document.PageSize.Width - 10f, 590f);
								directContent.LineTo(document.PageSize.Width - 10f, 565f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(240f, 590f);
								directContent.LineTo(240f, 565f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(290f, 590f);
								directContent.LineTo(290f, 565f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(350f, 590f);
								directContent.LineTo(350f, 565f);
								directContent.Stroke();
								directContent = instance.DirectContent;
								directContent.SetLineWidth(1f);
								directContent.MoveTo(460f, 590f);
								directContent.LineTo(460f, 565f);
								directContent.Stroke();
								Paragraph paragraph4 = new Paragraph(" ", font1);
								Paragraph paragraph5 = new Paragraph("Шот-хабарлама      Айы/ Месяц:        ", font1);
								DateTimeFormatInfo dateTimeFormat = (new CultureInfo("ru-RU")).DateTimeFormat;
								DateTimeFormatInfo dateTimeFormatInfo = (new CultureInfo("kk-KZ")).DateTimeFormat;
								object[] upper = new object[6];
								DateTime now = DateTime.Now;
								upper[0] = dateTimeFormatInfo.GetMonthName(now.Month - 1).Substring(0, 1).ToUpper();
								DateTime dateTime = DateTime.Now;
								upper[1] = dateTimeFormatInfo.GetMonthName(dateTime.Month - 1).Substring(1);
								upper[2] = "/";
								DateTime now1 = DateTime.Now;
								upper[3] = dateTimeFormat.GetMonthName(now1.Month - 1);
								upper[4] = " ";
								now1 = DateTime.Now;
								upper[5] = now1.Year;
								Phrase phrase = new Phrase(string.Concat(upper), font2);
								paragraph5.Add(phrase);
								Paragraph paragraph6 = new Paragraph("Счет-извещение     Е/шоты/ Л/счет:  ", font1);
								phrase = new Phrase(strArrays[14], font2);
								paragraph6.Add(phrase);
								Paragraph paragraph7 = new Paragraph("                                 Абонент:             ", font1);
								phrase = new Phrase(string.Concat(strArrays[32], "; ", strArrays[33]), font2);
								paragraph7.Add(phrase);
								Paragraph paragraph8 = new Paragraph("Газ үшін барлық төленетін ақы /Итого за газоснабжение ", font1);
								double num = Math.Round(double.Parse(strArrays[20]), 0);
								paragraph8.Add(new Phrase((num > 0 ? "0" : Math.Abs(num).ToString()), font2));
								paragraph8.Add(new Phrase(" теңге", font2));
								paragraph8.Alignment = 2;
								Paragraph paragraph9 = new Paragraph("Төлемнің  сомасы/ Сумма платежа ___________________________ теңге", font1)
								{
									Alignment = 2
								};
								upper = new object[] { "Төлеушінің қолы/ Подпись плательщика ____________________  ", '\"', "_____", '\"', "_______________20____ж." };
								Paragraph paragraph10 = new Paragraph(string.Concat(upper), font1)
								{
									Alignment = 2
								};
								Paragraph paragraph11 = new Paragraph("Төлем  алушы/Получатель платежа:", font1)
								{
									Alignment = 2
								};
								Paragraph paragraph12 = new Paragraph("«Горгаз-сервис» ЖШC, Букетов көшесі, 34, БСН/БИН 020540001653, е/ш KZ726010251000024640  ", font1)
								{
									Alignment = 2
								};
								Paragraph paragraph13 = new Paragraph("Солт. Қаз. Филиалы АҚ «Қазақстан Халық банкі» БКС/БИК HSBKKZKX", font1)
								{
									Alignment = 2
								};
								Paragraph paragraph14 = new Paragraph("Шот-түбіртек           Айы/ Месяц:        ", font1);
								upper = new object[6];
								now1 = DateTime.Now;
								upper[0] = dateTimeFormatInfo.GetMonthName(now1.Month - 1).Substring(0, 1).ToUpper();
								now1 = DateTime.Now;
								upper[1] = dateTimeFormatInfo.GetMonthName(now1.Month - 1).Substring(1);
								upper[2] = "/";
								now1 = DateTime.Now;
								upper[3] = dateTimeFormat.GetMonthName(now1.Month - 1);
								upper[4] = " ";
								now1 = DateTime.Now;
								upper[5] = now1.Year;
								phrase = new Phrase(string.Concat(upper), font2);
								paragraph14.Add(phrase);
								Paragraph paragraph15 = new Paragraph("Счет-квитанция      Е/шоты/ Л/счет:  ", font1);
								phrase = new Phrase(strArrays[14], font2);
								paragraph15.Add(phrase);
								string str3 = "";
								str3 = (this.curBalance.IndexOf('-') >= 0 ? string.Concat(" задолженость ", Math.Abs(Math.Round(double.Parse(strArrays[19]), 2)), " тенге") : string.Concat(" переплата ", Math.Abs(Math.Round(double.Parse(strArrays[19]), 2)), " тенге"));
								string str4 = "";
								str4 = (this.curBalance.IndexOf('-') >= 0 ? string.Concat(" қарызы ", Math.Abs(Math.Round(double.Parse(strArrays[19]), 2)), " теңге") : string.Concat(" артық төленген ақы ", Math.Abs(Math.Round(double.Parse(strArrays[19]), 2)), " теңге"));
								string str5 = "";
								str5 = (strArrays[20].IndexOf('-') >= 0 ? string.Concat(" қарызы/долг ", Math.Abs(Math.Round(double.Parse(strArrays[20]), 2)), " теңге") : string.Concat(" артық төленген ақы/переплата ", Math.Round(double.Parse(strArrays[20]), 2), " теңге"));
								Paragraph paragraph16 = new Paragraph("                                 Басында:     ", font1);
								paragraph16.Add(new Phrase(str4, font2));
								string[] strArrays1 = new string[] { "                                 ", strArrays[18], "   адам тұрып жатыр / Проживает  ", strArrays[18], " человек(а)" };
								phrase = new Phrase(string.Concat(strArrays1), font1);
								paragraph16.Add(phrase);
								Paragraph paragraph17 = new Paragraph("                                 На начало:  ", font1);
								paragraph17.Add(new Phrase(str3, font2));
								string str6 = this.meterNomer;
								now1 = DateTime.Parse(this.meterDateCheck);
								phrase = new Phrase(string.Concat("                                   ЕҚ/ПУ: № ", str6, ",  дата поверки ", now1.ToString("dd.MM.yyyy")), font2);
								paragraph17.Add(phrase);
								Paragraph paragraph18 = new Paragraph("                                 Есептелген:                        Саны            Бағасы          Сомасы        Басындағы көрсеткіші           Соңғы көрсеткіші", font1);
								Paragraph paragraph19 = new Paragraph("                                 Начислено:                        Кол-во            Цена            Сумма           Начальные показания         Конечные показания", font1);
								string str7 = (strArrays[40] == "1" ? " м3" : " чел");
								upper = new object[15];
								upper[0] = "                                 ";
								upper[1] = (strArrays[40] == "1" ? string.Concat("ЕҚ/ПУ: № ", this.meterNomer) : "Норма б-ша/по норме:");
								upper[2] = "           ";
								object[] objArray = upper;
								if (strArrays[40] == "1")
								{
									obj = (strArrays[23].Length < 3 ? string.Concat(strArrays[23], ",000") : strArrays[23]);
								}
								else
								{
									obj = strArrays[18];
								}
								objArray[3] = obj;
								upper[4] = str7;
								upper[5] = "        ";
								upper[6] = (strArrays[40] == "1" ? Math.Round(double.Parse(strArrays[30]), 2) : Math.Round(double.Parse(strArrays[31]), 2));
								upper[7] = "          ";
								upper[8] = Math.Round(double.Parse(strArrays[22]), 2);
								upper[9] = "            ";
								upper[10] = strArrays[36];
								upper[11] = "  ";
								upper[12] = strArrays[37].Substring(0, 10);
								upper[13] = "                   ";
								upper[14] = (strArrays[34] != "0" ? string.Concat(strArrays[34], "  ", strArrays[35].Substring(0, 10)) : "Жоқ/Отсутствуют");
								Paragraph paragraph20 = new Paragraph(string.Concat(upper), font3);
								if (mobile.needverif)
								{
									paragraph = new Paragraph("Құрметті абонент! ЕҚ шұғыл түрде тексеруге тапсырыңыздар.", font3);
									paragraph1 = new Paragraph(" Тексеру мерзімі өтіп кеткен ЕҚ бойынша көрсетулер сертификат ұсынылғанға дейін қабылданбайды.", font3);
									paragraph2 = new Paragraph("", font3);
									paragraph3 = new Paragraph("", font3);
								}
								else
								{
									paragraph = new Paragraph("Көрсетулерді қабылдау 630400 телефоны бойынша.", font4);
									paragraph1 = new Paragraph("жұмыс күндері 8-00-ден 19-00 дейін", font4);
									paragraph2 = new Paragraph("19-00 дейін, сенбі 8-ден 14-00 дейін. Көрсеткіштер болмаған.", font4);
									paragraph3 = new Paragraph("жағдайда төлемақы тұтыну мөлшері бойынша есептелінеді.", font4);
								}
								paragraph.Alignment = 2;
								paragraph1.Alignment = 2;
								paragraph2.Alignment = 2;
								paragraph3.Alignment = 2;
								Paragraph paragraph21 = new Paragraph("                                 Барлық төленетін ақы/Итого начислено:     ", font1);
								phrase = new Phrase(string.Concat("  ", Math.Round(double.Parse(strArrays[22]), 2), "  "), font2);
								paragraph21.Add(phrase);
								paragraph21.Add(new Phrase("                       Прием показаний по тел.: 630-400 по будням с 8-00", font4));
								Paragraph paragraph22 = new Paragraph("                                                                  до 19-00, суббота с 8-00 до 14-00. При отсутствии показаний", font4)
								{
									Alignment = 2
								};
								Paragraph paragraph23 = new Paragraph("                                 Ақы төлеу/ Оплата:     ", font1);
								phrase = new Phrase(string.Concat("                                         ", Math.Round(double.Parse(strArrays[27]), 2), ",00 "), font3);
								paragraph23.Add(phrase);
								paragraph23.Add(new Phrase("          начисление оплаты производится по норме потребления.", font4));
								Paragraph paragraph24 = new Paragraph("                                 Барлығы төленді/ Итого оплачено:                     ", font1);
								phrase = new Phrase(string.Concat(Math.Round(double.Parse(strArrays[21]), 2), ",00 "), font3);
								paragraph24.Add(phrase);
								Paragraph paragraph25 = new Paragraph("                                 Соңында/На конец:      ", font1);
								phrase = new Phrase(string.Concat(str5, "  "), font3);
								paragraph25.Add(phrase);
								Paragraph paragraph26 = new Paragraph("Кассир                                                                                                     Төлемнің  сомасы/ Сумма платежа _________________________ теңге", font1);
								document.Add(paragraph5);
								document.Add(paragraph6);
								document.Add(paragraph7);
								document.Add(paragraph8);
								document.Add(paragraph9);
								document.Add(paragraph10);
								document.Add(paragraph11);
								document.Add(paragraph12);
								document.Add(paragraph13);
								document.Add(paragraph4);
								document.Add(paragraph4);
								document.Add(paragraph14);
								document.Add(paragraph15);
								document.Add(paragraph7);
								document.Add(paragraph16);
								document.Add(paragraph17);
								document.Add(paragraph18);
								document.Add(paragraph19);
								document.Add(paragraph20);
								document.Add(paragraph);
								document.Add(paragraph1);
								document.Add(paragraph2);
								document.Add(paragraph3);
								document.Add(paragraph21);
								document.Add(paragraph22);
								document.Add(paragraph23);
								document.Add(paragraph24);
								document.Add(paragraph25);
								document.Add(paragraph26);
								document.Add(paragraph10);
								document.Close();
								instance.Close();
								memoryStream.Close();
								base.Response.ContentType = "pdf/application";
								base.Response.AddHeader("content-disposition", "attachment;filename=Account_notification.pdf");
								base.Response.OutputStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.GetBuffer().Length);
							}
						}
					}
				}
			}
		}

		protected void btn_SaveComment_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.txt_messTitle.Text) && !string.IsNullOrEmpty(this.txt_MessBody.Text))
				{
					if (this.txt_MessBody.Text.Length < 240)
					{
						if (this.eservice == null)
						{
							this.eservice = new EService();
						}
						if (this.eservice.PCSaveComment(this.ls, this.txt_messTitle.Text, this.txt_MessBody.Text, "111") == "Успешно")
						{
							if (this.lang != "ru")
							{
								this.Label9.Text = "Пікір жіберілді";
							}
							else
							{
								this.Label9.Text = "Отзыв отправлен";
							}
						}
						else if (this.lang != "ru")
						{
							this.Label9.Text = "Пікір жіберілмеді. Кейінірек байқап көріңіз";
						}
						else
						{
							this.Label9.Text = "Отзыв не отправлен. Попробуйте позднее";
						}
						TextBox txtMessTitle = this.txt_messTitle;
						string str = "";
						string str1 = str;
						this.txt_MessBody.Text = str;
						txtMessTitle.Text = str1;
					}
					else if (this.lang != "ru")
					{
						this.Label9.Text = string.Concat(" Хабарлама ұзақтығына шектеу. Хабарлама ұзақтығын 200 символға дейін қысқартыңыз. ", this.txt_MessBody.Text.Length, " белгі енгізілді");
					}
					else
					{
						this.Label9.Text = string.Concat("Ограничение на длину сообщения. Сократите длину сообщения до 200 символов. Введено ", this.txt_MessBody.Text.Length, " знаков");
					}
				}
			}
			catch (Exception exception)
			{
				if (this.lang != "ru")
				{
					this.Label9.Text = "Қателік орын алды. Кейінірек байқап көріңіз";
				}
				else
				{
					this.Label9.Text = "Произошла ошибка. Попробуйте позднее";
				}
			}
		}

		protected void btn_SendPokaz_Click(object sender, ImageClickEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.txt_NewInd.Text))
			{
				double num = double.Parse(this.l_prevPokaz.Text);
				double num1 = double.Parse(this.txt_NewInd.Text.Replace(".", ","));
				if (num >= num1)
				{
					if (this.lang != "ru")
					{
						this.l_sendInd.Text = "Енгізілген көрсетулер бұрынғысынан кем";
					}
					else
					{
						this.l_sendInd.Text = "Введенные показания меньше чем предыдушие";
					}
				}
				else if (num1 - num <= 10)
				{
					this.sendInd();
				}
				else if (this.chb_confirm10.Checked)
				{
					this.sendInd();
				}
				else
				{
					if (this.lang != "ru")
					{
						this.l_sendInd.Text = "Көрсетулер 10 бірліктен асып кетті";
					}
					else
					{
						this.l_sendInd.Text = "Показания превышают 10 единиц";
					}
					this.chb_confirm10.Visible = true;
					this.chb_confirm10.Checked = false;
				}
			}
			this.l_sendInd.Visible = true;
			this.MultiView1.ActiveViewIndex = 1;
		}

		protected void btn_SendUserData_Click(object sender, EventArgs e)
		{
			this.l_sendInd.Text = "";
			this.CheckCanSendInd();
		}

		protected void btn_Setting_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 4;
			this.l_changePWD.Visible = false;
		}

		protected void btn_ShowInfo_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 0;
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			this.ls = "";
			this.fio = "";
			this.adres = "";
			this.curBalance = "";
			this.statusObject = "";
			this.ConsamerPhone = "";
			this.meterName = "";
			this.meterType = "";
			this.meterNomer = "";
			this.meterDateCreate = "";
			this.meterDateCheck = "";
			this.LastIndication = "";
			this.DateIndication = "";
			this.idgmeter = "";
			this.Dispose();
			base.Response.Redirect("~/Default.aspx");
		}

		protected void Button1_Click1(object sender, EventArgs e)
		{
		}

		private void CheckCanSendInd()
		{
			if (this.statusObject != "Подключен")
			{
				this.l_sendInd.Text = "Прибор учета отключен. Прием показаний невозможен";
				this.btn_SendPokaz.Enabled = false;
			}
			else
			{
				this.btn_SendPokaz.Enabled = true;
			}
			this.MultiView1.ActiveViewIndex = 1;
		}

		private void CheckNeedChangePWD()
		{
			if (!mobile.needchgPWD)
			{
				this.MultiView1.ActiveViewIndex = 0;
				return;
			}
			this.MultiView1.ActiveViewIndex = 4;
			this.l_needchgPWD.Visible = true;
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

		protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 0;
		}

		protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
		{
		}

		private static byte[] ImageToByte(Bitmap img)
		{
			return (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
		}

		protected void img_kz_Click(object sender, ImageClickEventArgs e)
		{
			this.lang = "kz";
			this.Session["lang"] = "kz";
			this.Page_Load(null, null);
			if (this.MultiView1.ActiveViewIndex == 2)
			{
				this.lbtn_LoadPays_Click(null, null);
			}
			if (this.MultiView1.ActiveViewIndex == 6)
			{
				this.btn_CreateShet_Click(null, null);
			}
		}

		protected void img_ru_Click(object sender, ImageClickEventArgs e)
		{
			this.lang = "ru";
			this.Session["lang"] = "ru";
			this.Page_Load(null, null);
			if (this.MultiView1.ActiveViewIndex == 2)
			{
				this.lbtn_LoadPays_Click(null, null);
			}
			if (this.MultiView1.ActiveViewIndex == 6)
			{
				this.btn_CreateShet_Click(null, null);
			}
		}

		protected void lbtn_Comment_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 4;
		}

		protected void lbtn_LoadPays_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 2;
			if (this.eservice == null)
			{
				this.eservice = new EService();
			}
			object[] objArray = null;
			object[] objArray1 = null;
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Тип");
			dataTable.Columns.Add("Дата операции");
			dataTable.Columns.Add("Сумма");
			objArray = this.eservice.PCGetDataPayinArray(this.ls, "1", "2", "3");
			if (objArray != null && (int)objArray.Length > 0)
			{
				for (int i = 0; i < (int)objArray.Length - 1; i++)
				{
					objArray1 = (object[])objArray[i];
					DataRow str = dataTable.NewRow();
					if (this.lang != "ru")
					{
						str["Тип"] = (objArray1[0].ToString() == "1" ? "Төлем" : "Есептеу");
					}
					else
					{
						str["Тип"] = (objArray1[0].ToString() == "1" ? "Оплата" : "Начисление");
					}
					str["Дата операции"] = objArray1[1].ToString().Substring(0, 10);
					str["Сумма"] = objArray1[2].ToString();
					dataTable.Rows.Add(str);
				}
			}
			this.GridView1.DataSource = dataTable;
			this.GridView1.DataBind();
			if (this.lang == "ru")
			{
				this.GridView1.Columns[0].HeaderText = "Тип";
				this.GridView1.Columns[1].HeaderText = "Дата операции";
				this.GridView1.Columns[2].HeaderText = "Сумма";
				return;
			}
			this.GridView1.Columns[0].HeaderText = "Типі";
			this.GridView1.Columns[1].HeaderText = "Төлем күні";
			this.GridView1.Columns[2].HeaderText = "Сомасы";
		}

		protected void LinkButton1_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 0;
		}

		protected void LinkButton2_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 1;
		}

		protected void LinkButton3_Click(object sender, EventArgs e)
		{
			this.MultiView1.ActiveViewIndex = 3;
		}

		private void LoadData(string isFull)
		{
			if (this.eservice == null)
			{
				this.eservice = new EService();
			}
			EService eService = this.eservice;
			string str = this.ls;
			string str1 = DateTime.Now.Year.ToString();
			int month = DateTime.Now.Month;
			string str2 = eService.PCGetData(str, str1, month.ToString(), isFull);
			if (!string.IsNullOrEmpty(str2))
			{
				string[] strArrays = str2.Split(new char[] { ';' });
				if ((int)strArrays.Length > 1)
				{
					this.Session.Add("ConsamerPhone", strArrays[0]);
					this.Session.Add("statusObject", strArrays[1]);
					this.Session.Add("meterName", strArrays[2]);
					this.Session.Add("meterType", strArrays[3]);
					this.Session.Add("meterNomer", strArrays[4]);
					this.Session.Add("meterDateCreate", strArrays[5]);
					this.Session.Add("meterDateCheck", strArrays[6]);
					this.Session.Add("LastIndication", strArrays[7]);
					this.Session.Add("DateIndication", strArrays[8]);
					this.ConsamerPhone = this.Session["ConsamerPhone"].ToString();
					this.statusObject = this.Session["statusObject"].ToString();
					this.meterName = this.Session["meterName"].ToString();
					this.meterType = this.Session["meterType"].ToString();
					this.meterNomer = this.Session["meterNomer"].ToString();
					this.meterDateCreate = this.Session["meterDateCreate"].ToString();
					this.meterDateCheck = this.Session["meterDateCheck"].ToString();
					this.LastIndication = this.Session["LastIndication"].ToString();
					this.DateIndication = this.Session["DateIndication"].ToString();
					this.l_meterName.Text = string.Concat(this.meterName, " (тип ", this.meterType, ")");
					this.l_meternomer.Text = this.meterNomer;
					Label lMeterDateCreate = this.l_meterDateCreate;
					DateTime dateTime = DateTime.Parse(this.meterDateCreate);
					lMeterDateCreate.Text = dateTime.ToString("dd.MM.yyyy");
					Label lMeterDateCheck = this.l_meterDateCheck;
					DateTime dateTime1 = DateTime.Parse(this.meterDateCheck);
					lMeterDateCheck.Text = dateTime1.ToString("dd.MM.yyyy");
					this.l_prevPokaz.Text = this.LastIndication;
					this.l_DatePokaz.Text = this.DateIndication.Substring(0, 10);
					DateTime dateTime2 = DateTime.Parse(this.meterDateCheck);
					if (string.IsNullOrEmpty(strArrays[13]) || strArrays[13] == "0")
					{
						mobile.needchgPWD = true;
					}
					else
					{
						mobile.needchgPWD = false;
					}
					this.idgmeter = strArrays[10];
					int days = (dateTime2 - DateTime.Now).Days;
					if (days < 0)
					{
						mobile.needverif = true;
						this.img_vz.Visible = true;
						this.img_kvz.Visible = false;
						return;
					}
					mobile.needverif = false;
					if (days < 7)
					{
						this.img_vz.Visible = false;
						this.img_kvz.Visible = true;
						System.Web.UI.WebControls.Image imgKvz = this.img_kvz;
						TimeSpan now = dateTime2 - DateTime.Now;
						imgKvz.ToolTip = string.Concat("Уважаемый абонент! Срок поверки Вашего прибора учета истекает через ", now.Days, " дней");
					}
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Session.Timeout = 2;
				if (string.IsNullOrEmpty(this.prevpageinfo))
				{
					try
					{
						this.Session.Add("prevpageinfo", "123");
						HiddenField hiddenField = (HiddenField)this.Page.PreviousPage.FindControl("HiddenField1");
						this.prevpageinfo = hiddenField.Value;
						string[] strArrays = this.prevpageinfo.Split(new char[] { ';' });
						if ((int)strArrays.Length > 1)
						{
							this.Session.Add("ls", strArrays[3]);
							this.Session.Add("fio", strArrays[0]);
							this.Session.Add("adres", strArrays[1]);
							this.Session.Add("curBalance", strArrays[2]);
							this.Session.Add("lang", strArrays[4]);
							this.ls = this.Session["ls"].ToString();
						}
					}
					catch (Exception exception)
					{
					}
					this.LoadData("0");
					this.CheckNeedChangePWD();
				}
			}
			this.ls = this.Session["ls"].ToString();
			this.fio = this.Session["fio"].ToString();
			this.adres = this.Session["adres"].ToString();
			this.curBalance = this.Session["curBalance"].ToString();
			this.l_fio.Text = this.fio;
			this.l_adress.Text = this.adres;
			this.l_account.Text = this.ls;
			this.lang = this.Session["lang"].ToString();
			this.ConsamerPhone = this.Session["ConsamerPhone"].ToString();
			this.statusObject = this.Session["statusObject"].ToString();
			this.meterName = this.Session["meterName"].ToString();
			this.meterType = this.Session["meterType"].ToString();
			this.meterNomer = this.Session["meterNomer"].ToString();
			this.meterDateCreate = this.Session["meterDateCreate"].ToString();
			this.meterDateCheck = this.Session["meterDateCheck"].ToString();
			this.LastIndication = this.Session["LastIndication"].ToString();
			this.DateIndication = this.Session["DateIndication"].ToString();
			if (this.Session["prevpageinfo"] == null || string.IsNullOrEmpty(this.Session["prevpageinfo"].ToString()))
			{
				this.Button1_Click(null, null);
			}
			if (this.lang != "ru")
			{
				base.Title = "Жеке кабинет Горгаз-Сервис";
				this.l_balanceInfo.Text = string.Concat("Сіздің теңгеріміңіз ", this.curBalance, " теңге");
				this.l_prUcheta.Text = "Есепке алу құралы";
				this.l_devname.Text = "Аты";
				this.l_devnomer.Text = "Нөмірі";
				this.l_DateCreate.Text = "Шыққан күні";
				this.l_DateVer.Text = "Тексеру күні";
				this.l_pokaz.Text = "Көрсетулер";
				this.l_curPokaz.Text = "Сіздің ағымдағы көрсетуіңіз";
				this.l_newPokaz.Text = "Жаңа көрсетуді енгізіңіз";
				this.l_smenPar.Text = "Құпиясөзді ауыстыру";
				this.l_newpwd.Text = "Жаңа құпиясөз";
				this.l_confNewpwd.Text = "Құпиясөзді растаңыз";
				this.l_otzuv.Text = "Пікір қалдыру";
				this.p_theme.GroupingText = "Тақырып";
				this.p_mess.GroupingText = "Хабарлама";
				this.l_operc.Text = "Шот бойынша операциялар";
				this.LinkButton1.Text = "Ақпарат";
				this.LinkButton4.Text = "Төлем шоты";
				this.LinkButton6.Text = "Шот бойынша операциялар";
				this.LinkButton2.Text = "Көрсетулер";
				this.LinkButton8.Text = "Пікір";
				this.LinkButton5.Text = "Шығу";
				this.btn_SendPokaz.ImageUrl = "~/images/s2kz.png";
				this.btn_ChangePWD.ImageUrl = "~/images/c2kz.png";
				this.btn_SaveComment.ImageUrl = "~/images/s22kz.png";
				this.btn_MakeShet.ImageUrl = "~/images/s22kz.png";
				this.l_balanceInfo1.Text = "Төлем шоты";
				this.chb_confirm10.Text = "Растау";
				string[] strArrays1 = this.adres.Split(new char[] { ',' });
				Label lAdress = this.l_adress;
				string[] strArrays2 = new string[] { strArrays1[0].Substring(4), " к-ci, ", strArrays1[1].Substring(3), " үй, ", strArrays1[2].Substring(4), " пәтер" };
				lAdress.Text = string.Concat(strArrays2);
			}
			else
			{
				base.Title = "Личный кабинет Горгаз-Сервис";
				this.l_balanceInfo.Text = string.Concat("Ваш баланс ", this.curBalance, " тенге");
				this.l_prUcheta.Text = "Прибор учета";
				this.l_devname.Text = "Название";
				this.l_devnomer.Text = "Номер";
				this.l_DateCreate.Text = "Дата выпуска";
				this.l_DateVer.Text = "Дата поверки";
				this.l_pokaz.Text = "Показания";
				this.l_curPokaz.Text = "Ваши текущие показания";
				this.l_newPokaz.Text = "Введите новые показания";
				this.l_smenPar.Text = "Сменить пароль";
				this.l_newpwd.Text = "Новый пароль";
				this.l_confNewpwd.Text = "Подтвердите пароль";
				this.l_otzuv.Text = "Оставить отзыв";
				this.p_theme.GroupingText = "Тема";
				this.p_mess.GroupingText = "Сообщение";
				this.l_operc.Text = "Операции по счету";
				this.LinkButton1.Text = "Информация";
				this.LinkButton4.Text = "Счет на оплату";
				this.LinkButton6.Text = "Операции по счету";
				this.LinkButton2.Text = "Показания";
				this.LinkButton8.Text = "Отзыв";
				this.LinkButton5.Text = "Выход";
				this.btn_SendPokaz.ImageUrl = "~/images/s2.png";
				this.btn_ChangePWD.ImageUrl = "~/images/c2.png";
				this.btn_SaveComment.ImageUrl = "~/images/s22.png";
				this.btn_MakeShet.ImageUrl = "~/images/make.png";
				this.l_balanceInfo1.Text = "Счет на оплату";
				this.chb_confirm10.Text = "Подтверждаю";
				this.l_adress.Text = this.adres;
			}
			if (double.Parse(this.curBalance) < 0)
			{
				if (this.lang != "ru")
				{
					this.l_info.Text = "Құрметті абонент.";
					this.l_inf.Text = "Сізден газбен жабдықтау қызметтері ";
					this.l_inf0.Text = "үшін борышыңызды өтеуіңізді сұраймыз";
				}
				else
				{
					this.l_info.Text = "Уважаемый абонент.";
					this.l_inf.Text = "Просим Вас погасить задолженность";
					this.l_inf0.Text = " за услуги газоснабжения";
				}
			}
			else if (this.lang != "ru")
			{
				this.l_info.Text = "Құрметті абонент.";
				this.l_inf.Text = " Газбен жабдықтау қызметтеріне уақтылы ";
				this.l_inf0.Text = "төлем  жасағаныңыз үшін Сізге алғыс білдіреміз";
			}
			else
			{
				this.l_info.Text = "Уважаемый абонент.";
				this.l_inf.Text = " Благодарим Вас за своевременную ";
				this.l_inf0.Text = "оплату услуг газоснабжения";
			}
			if (this.lang != "ru")
			{
				this.GridView1.Columns[0].HeaderText = "Типі";
				this.GridView1.Columns[1].HeaderText = "Төлем күні";
				this.GridView1.Columns[2].HeaderText = "Сомасы";
			}
			else
			{
				this.GridView1.Columns[0].HeaderText = "Тип";
				this.GridView1.Columns[1].HeaderText = "Дата операции";
				this.GridView1.Columns[2].HeaderText = "Сумма";
			}
			this.txt_MessBody.Attributes.Add("onchange", "GetCharCount(this, 200);");
			this.txt_MessBody.Attributes.Add("onkeypress", "GetCharCount(this, 200);");
		}

		private void refreshbalance()
		{
			if (this.eservice == null)
			{
				this.eservice = new EService();
			}
			if (string.IsNullOrEmpty(this.l_account.Text))
			{
				HiddenField hiddenField = (HiddenField)this.Page.PreviousPage.FindControl("HiddenField1");
				this.l_account.Text = hiddenField.Value;
			}
			string str = this.eservice.PCLogin(this.l_account.Text, "123");
			if (!string.IsNullOrEmpty(str))
			{
				string[] strArrays = str.Split(new char[] { ';' });
				if ((int)strArrays.Length > 1)
				{
					this.l_fio.Text = string.Concat(strArrays[0], " Адрес ", strArrays[1], " Номер л/с");
					this.l_balanceInfo.Text = string.Concat("Ваш текущий баланс равен ", strArrays[2], " тенге");
				}
			}
		}

		private void sendInd()
		{
			if (this.eservice == null)
			{
				this.eservice = new EService();
			}
			if (this.eservice.PCSaveInd(this.txt_NewInd.Text.Replace(",", "."), this.idgmeter, "1", "2") != "Успешно")
			{
				this.l_sendInd.Text = "Невозможно передать показания";
			}
			else
			{
				this.l_sendInd.Text = "Ваши показания приняты";
				this.LoadData("0");
			}
			this.txt_NewInd.Text = "";
			this.l_sendInd.Visible = true;
			this.chb_confirm10.Checked = false;
			this.chb_confirm10.Visible = false;
		}

		protected void txt_MessBody_TextChanged(object sender, EventArgs e)
		{
			if (this.lang == "ru")
			{
				this.Label9.Text = string.Concat("Ограничение на длину сообщения 200 знаков. Введено ", this.txt_MessBody.Text.Length, " знаков");
				return;
			}
			this.Label9.Text = string.Concat("Хабарлама ұзақтығына шектеу. Хабарлама ұзақтығын 200 символға дейін қысқартыңыз. ", this.txt_MessBody.Text.Length, " белгі енгізілді.");
		}
	}
}