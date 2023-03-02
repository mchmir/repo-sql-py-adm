using PayNavigator.SynchServ;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace PayNavigator
{
	public class f_main : Form
	{
		private string strConnectDB = string.Concat("Data Source=", Application.StartupPath, "\\DataBase.db;New=True;Version=3");

		private string SQLconnString = "server=GG-DOC\\SQLEXPRESS;uid=epayuser;pwd=asdfgh;database=EPay";

		private string queryD = "";

		private string response = "";

		private ArrayList ServicesInfoCollection = null;

		private ArrayList ResultList = null;

		private bool UseLocalStore = false;

		private bool DBIsClose = false;

		private bool UseDebug = false;

		private PayNavigator.SynchServ.SynchServ SvSynch = null;

		private int time = 0;

		private int interval = 10;

		private DateTime tmpDate = DateTime.Now;

		private object[] tmp = null;

		private object[] olist = null;

		private object[] resultListD = null;

		private object[] listD = null;

		private Thread UpdateThread = null;

		private DateTime DateUpdateLocal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 27);

		private string certName = "CN=GG_SIGN_CERT";

		private string PFXFilePath = "C:\\EPAY\\GG_SIGN_CERT.pfx";

		private string PFXFilePWD = "123456789";

		private IContainer components = null;

		private TabControl tc_Main;

		private TabPage tabPage1;

		private Button btn_ChangeTime;

		private Label l_Status;

		private Label label3;

		private Label label2;

		private Label label1;

		private NumericUpDown nud_TimerInterval;

		private Button btn_Stop;

		private Button btn_Start;

		private TabPage tabPage2;

		private System.Windows.Forms.Timer t_mainTimer;

		private ComboBox cmb_Portal;

		private GroupBox groupBox1;

		private Panel p_Services;

		private Label label4;

		private GroupBox gb_Services;

		private SplitContainer splitContainer1;

		private NumericUpDown nud_ServiceTimeInterval;

		private Label label6;

		private ListView lv_services;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private Button btn_RefreshServList;

		private Button btn_DeleteFromList;

		private Button btn_SaveServ;

		private Button btn_FormatXML;

		private TabPage tabPage3;

		private TextBox txt_XMLformatData;

		private ColumnHeader columnHeader5;

		private Button btn_LoadData;

		private Button btn_Manual;

		private GroupBox groupBox2;

		private Panel panel2;

		private Panel panel1;

		private Button btn_SaveCodeWords;

		private Button btn_LoadListZam;

		private Button btn_AttrInList;

		private Button btn_AttrDelete;

		private TextBox txt_Zam;

		private TextBox txt_Attribut;

		private Label label5;

		private Label label7;

		private GroupBox groupBox3;

		private ListView lv_AttrList;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private Label l_CurStat;

		private Panel p_Debug;

		private Label label9;

		private Label label8;

		private Label label11;

		private Label label10;

		private CheckBox chb_UseDebugview;

		private Label l_SILcount;

		private Label l_SICcount;

		private Label l_interval;

		private Label l_time;

		private CheckBox chb_UseClearList;

		private Label l_del;

		private Label label12;

		private Label l_ReslitsCount;

		private Label label13;

		private GroupBox groupBox4;

		private Label l_CnLiveStatus;

		private Label label14;

		private Button btn_CnLiveStop;

		private Button btn_CnLiveStart;

		private System.Windows.Forms.Timer timerForCnLive;

		private Label l_CnLiveRes;

		private Label label15;

		private Label l_CnLiveDateUpdate;

		private Label l_DBisClose;

		private Label label16;

		private TabPage tabPage4;

		private GroupBox groupBox5;

		private Label label17;

		private Button btn_NOuseDBGefest;

		private Button btn_UseDBGefest;

		private Label l_UseDBGefest;

		private GroupBox groupBox6;

		private Label l_lastUpLocalDBDateStatus;

		private Label label21;

		private Label l_lastUpLocalDBDate;

		private Label label19;

		private ComboBox cmb_UpdateLocalDBInterval;

		private Label label18;

		private Button btn_UpdateLocalDBTimerStop;

		private Button btn_UpdateLocalDBTimerStart;

		private Button btn_UseGefestDBCheck;

		private Button btn_UseDEBUG;

		private Label l_UpdateDBStatus;

		private Label label20;

		public f_main()
		{
			this.InitializeComponent();
		}

		private void btn_AttrDelete_Click(object sender, EventArgs e)
		{
			if ((this.lv_AttrList.SelectedItems == null ? false : this.lv_AttrList.SelectedItems.Count > 0))
			{
			}
		}

		private void btn_AttrInList_Click(object sender, EventArgs e)
		{
			if ((string.IsNullOrEmpty(this.txt_Attribut.Text) ? true : string.IsNullOrEmpty(this.txt_Zam.Text)))
			{
				MessageBox.Show("Не указан атрибут или его заменитель!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				bool flag = true;
				foreach (ListViewItem item in this.lv_AttrList.Items)
				{
					if (this.txt_Attribut.Text == item.Text)
					{
						MessageBox.Show("Атрибут с таким именем уже сушествует!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						flag = false;
						break;
					}
					else if (this.txt_Zam.Text == item.SubItems[1].Text)
					{
						MessageBox.Show("Атрибут с таким заменителем уже сушествует!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						flag = false;
						break;
					}
				}
				if (flag)
				{
					ListViewItem listViewItem = new ListViewItem(this.txt_Attribut.Text);
					listViewItem.SubItems.Add(this.txt_Zam.Text);
					this.lv_AttrList.Items.Add(listViewItem);
				}
			}
			this.lv_AttrList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		private void btn_ChangeTime_Click(object sender, EventArgs e)
		{
			if (!this.t_mainTimer.Enabled)
			{
				this.t_mainTimer.Interval = (int)this.nud_TimerInterval.Value * 1000;
			}
			else
			{
				this.btn_Stop_Click(null, null);
				this.t_mainTimer.Interval = (int)this.nud_TimerInterval.Value * 1000;
				this.btn_Start_Click(null, null);
			}
			decimal value = this.nud_TimerInterval.Value;
			this.SaveParam("MainTimerInterval", value.ToString());
		}

		private void btn_CnLiveStart_Click(object sender, EventArgs e)
		{
			this.l_CnLiveDateUpdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
			this.GetLiveConnect();
			this.btn_CnLiveStart.Enabled = false;
			this.btn_CnLiveStop.Enabled = true;
			this.l_CnLiveStatus.Text = "В работе";
			this.timerForCnLive.Start();
			this.SaveParam("CnLive", "True");
		}

		private void btn_CnLiveStop_Click(object sender, EventArgs e)
		{
			this.btn_CnLiveStart.Enabled = true;
			this.btn_CnLiveStop.Enabled = false;
			this.l_CnLiveStatus.Text = "Остановленно";
			this.timerForCnLive.Stop();
			this.SaveParam("CnLive", "False");
		}

		private void btn_DeleteFromList_Click(object sender, EventArgs e)
		{
			if ((this.lv_services.SelectedItems == null ? false : this.lv_services.SelectedItems.Count > 0))
			{
				this.DeleteServiceSett(this.lv_services.SelectedItems[0].SubItems[4].Text);
				this.RefreshData();
			}
		}

		private void btn_FormatXML_Click(object sender, EventArgs e)
		{
			this.tc_Main.SelectedIndex = 2;
		}

		private void btn_LoadData_Click(object sender, EventArgs e)
		{
			this.ServicesInfoCollection = this.GetList("SELECT [IDService],[Portal],[Interval],[DataFormat]  FROM [ServiceSettings]");
		}

		private void btn_Manual_Click(object sender, EventArgs e)
		{
			if (this.time >= 3800)
			{
				this.time = 10;
			}
			else
			{
				f_main interval = this;
				interval.time = interval.time + this.t_mainTimer.Interval / 1000;
			}
			this.GetData();
		}

		private void btn_NOuseDBGefest_Click(object sender, EventArgs e)
		{
			this.l_UseDBGefest.Text = "ВЫКЛ";
			this.SaveData("UPDATE [EPay].[dbo].[Config]  SET [ParamValue] = 'true' where [ParamName]='DBisClose'");
		}

		private void btn_RefreshServList_Click(object sender, EventArgs e)
		{
			this.RefreshData();
		}

		private void btn_SaveServ_Click(object sender, EventArgs e)
		{
			if (this.p_Services.Controls.Count <= 0)
			{
				MessageBox.Show("Незаполнен список сервисов", "Внимание");
			}
			else
			{
				int num = 0;
				foreach (CheckBox control in this.p_Services.Controls)
				{
					if (control.Checked)
					{
						num++;
						if (string.IsNullOrEmpty(this.cmb_Portal.Text))
						{
							MessageBox.Show("Невыбран шлюз", "Внимание");
						}
						else if (string.IsNullOrEmpty(this.txt_XMLformatData.Text))
						{
							MessageBox.Show("Не заполнен формат данных", "Внимание");
						}
						else
						{
							this.SaveServiceSettings(control.Tag.ToString());
							this.RefreshData();
						}
					}
				}
				if (num == 0)
				{
					MessageBox.Show("Не выбран не один из сервисов", "Внимание");
				}
			}
		}

		private void btn_Start_Click(object sender, EventArgs e)
		{
			this.btn_Start.Enabled = false;
			this.btn_Stop.Enabled = true;
			this.l_Status.Text = "В работе";
			this.btn_ChangeTime_Click(null, null);
			this.t_mainTimer.Start();
			this.SaveParam("InWork", "True");
		}

		private void btn_Stop_Click(object sender, EventArgs e)
		{
			this.btn_Start.Enabled = true;
			this.btn_Stop.Enabled = false;
			this.l_Status.Text = "Остановленно";
			this.t_mainTimer.Stop();
			this.SaveParam("InWork", "False");
		}

		private void btn_UpdateLocalDBTimerStart_Click(object sender, EventArgs e)
		{
			this.btn_UpdateLocalDBTimerStart.Enabled = false;
			this.btn_UpdateLocalDBTimerStop.Enabled = true;
			this.l_UpdateDBStatus.Text = "В работе";
			this.UpdateThread = new Thread(new ThreadStart(this.UpdateLocalDB));
			this.UpdateThread.Start();
		}

		private void btn_UpdateLocalDBTimerStop_Click(object sender, EventArgs e)
		{
			if (this.UpdateThread != null)
			{
				this.UpdateThread.Abort();
			}
		}

		private void btn_UseDBGefest_Click(object sender, EventArgs e)
		{
			this.l_UseDBGefest.Text = "ВКЛ";
			this.SaveData("UPDATE [EPay].[dbo].[Config]  SET [ParamValue] = 'false' where [ParamName]='DBisClose'");
		}

		private void btn_UseDEBUG_Click(object sender, EventArgs e)
		{
			this.UseDebug = !this.UseDebug;
		}

		private void btn_UseGefestDBCheck_Click(object sender, EventArgs e)
		{
			this.l_UseDBGefest.Text = this.Get0Result("select ParamValue from [EPay].[dbo].[Config]  where [ParamName]='DBisClose'");
		}

		private void chb_UseDebugview_CheckedChanged(object sender, EventArgs e)
		{
			this.p_Debug.Visible = this.chb_UseDebugview.Checked;
		}

		private void cmb_UpdateLocalDBInterval_SelectedIndexChanged(object sender, EventArgs e)
		{
			DateTime now;
			int year;
			if (!string.IsNullOrEmpty(this.cmb_UpdateLocalDBInterval.Text))
			{
				string text = this.cmb_UpdateLocalDBInterval.Text;
				if (text != null)
				{
					switch (text)
					{
						case "1":
						{
							int num = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(num, now.Month, 1);
							break;
						}
						case "5":
						{
							int year1 = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(year1, now.Month, 5);
							break;
						}
						case "10":
						{
							int num1 = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(num1, now.Month, 10);
							break;
						}
						case "15":
						{
							int year2 = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(year2, now.Month, 15);
							break;
						}
						case "20":
						{
							int num2 = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(num2, now.Month, 20);
							break;
						}
						case "25":
						{
							int year3 = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(year3, now.Month, 25);
							break;
						}
						case "27":
						{
							int num3 = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(num3, now.Month, 27);
							break;
						}
						default:
						{
							year = DateTime.Now.Year;
							now = DateTime.Now;
							this.DateUpdateLocal = new DateTime(year, now.Month, 27);
							return;
						}
					}
				}
				else
				{
					year = DateTime.Now.Year;
					now = DateTime.Now;
					this.DateUpdateLocal = new DateTime(year, now.Month, 27);
					return;
				}
			}
		}

		private byte[] ConvertStringToByteArray(string s)
		{
			byte[] bytes;
			try
			{
				bytes = (new ASCIIEncoding()).GetBytes(s);
			}
			catch (Exception exception)
			{
				this.WriteLog(string.Concat("Ошибка перевода из строки в байты строка ", s), exception, "ConvertStringToByteArray");
				bytes = null;
			}
			return bytes;
		}

		private void DeleteServiceSett(string IDRow)
		{
			this.SaveData(string.Concat("Delete from [ServiceSettings] where [IDRow]=", IDRow));
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void DoUpdateLocalDB()
		{
			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer()
			{
				Interval = 86000000,
				Enabled = true
			};
			timer.Tick += new EventHandler(this.t_UpdateLocalDBTimer_Tick);
		}

		private void f_main_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Завершить работу программы", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
			{
				e.Cancel = false;
			}
			else
			{
				if ((this.l_Status.Text == "Остановленно" ? false : !(this.l_Status.Text == "")))
				{
					this.SaveParam("InWork", "True");
				}
				else
				{
					this.SaveParam("InWork", "False");
				}
				decimal value = this.nud_TimerInterval.Value;
				this.SaveParam("MainTimerInterval", value.ToString());
			}
		}

		private void f_main_Load(object sender, EventArgs e)
		{
			try
			{
				this.SvSynch = new PayNavigator.SynchServ.SynchServ();
				this.ServicesInfoCollection = new ArrayList();
				this.btn_LoadData_Click(null, null);
				string str = this.ReadParam("MainTimerInterval");
				int num = 0;
				if (!string.IsNullOrEmpty(str))
				{
					int.TryParse(str, out num);
					if (num > 0)
					{
						this.t_mainTimer.Interval = num;
					}
				}
				if (this.ReadParam("InWork") == "True")
				{
					this.btn_Start_Click(null, null);
				}
				if (this.ReadParam("CnLive") == "True")
				{
					this.btn_CnLiveStart_Click(null, null);
				}
				this.nud_TimerInterval.Value = num;
			}
			catch (Exception exception)
			{
				this.WriteLog("Ошибка загрузки приложения", exception, "f_main_Load");
			}
		}

		private string Get0Result(string Query)
		{
			string str = "";
			try
			{
				SqlConnection sqlConnection = new SqlConnection(this.SQLconnString);
				SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
				sqlConnection.Open();
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				while (sqlDataReader.Read())
				{
					str = sqlDataReader.GetValue(0).ToString();
				}
				sqlDataReader.Close();
				sqlConnection.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (this.UseDebug)
				{
					MessageBox.Show(string.Concat("GetList :", exception.Message, (exception.InnerException != null ? string.Concat(Environment.NewLine, exception.InnerException.Message) : "")), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			return str;
		}

		private void GetData()
		{
			int count;
			this.queryD = "";
			this.resultListD = null;
			this.listD = null;
			try
			{
				if ((this.ServicesInfoCollection == null ? true : this.ServicesInfoCollection.Count == 0))
				{
					this.btn_LoadData_Click(null, null);
				}
				if (this.chb_UseDebugview.Checked)
				{
					this.l_time.Text = this.time.ToString();
					Label lSICcount = this.l_SICcount;
					count = this.ServicesInfoCollection.Count;
					lSICcount.Text = count.ToString();
				}
				foreach (ArrayList servicesInfoCollection in this.ServicesInfoCollection)
				{
					int.TryParse(servicesInfoCollection[2].ToString(), out this.interval);
					if (this.chb_UseDebugview.Checked)
					{
						this.l_interval.Text = this.interval.ToString();
						this.l_SILcount.Text = servicesInfoCollection.Count.ToString();
						Label lDel = this.l_del;
						count = this.time % this.interval;
						lDel.Text = count.ToString();
					}
					if (this.time % this.interval == 0)
					{
						this.l_CurStat.Text = "Опрос";
						this.queryD = string.Concat("SELECT p.[UserInputData],p.[PayAmount],p.[DatePay],t.[IDRow],p.[IDPay],p.[IDAgent] FROM [Payments] p inner join dbo.Terminal t on t.IDTerminal=p.IDTerminal where [IDService]=", servicesInfoCollection[0].ToString(), " and IsPayComplete=1");
						this.resultListD = this.GetListOB(this.queryD);
						if (this.chb_UseDebugview.Checked)
						{
							this.l_ReslitsCount.Text = (this.resultListD != null ? ((int)this.resultListD.Length).ToString() : "0");
						}
						if (!(servicesInfoCollection[3].ToString() == "list"))
						{
							object[] objArray = this.resultListD;
							for (int i = 0; i < (int)objArray.Length; i++)
							{
								ArrayList arrayLists = (ArrayList)objArray[i];
								this.SendPayments(arrayLists, servicesInfoCollection[3].ToString());
							}
						}
						else
						{
							this.listD = new object[3];
							this.response = "ListPayments";
							this.listD[0] = this.response;
							this.listD[1] = this.Sign(this.response, this.PFXFilePath, this.PFXFilePWD);
							if ((this.resultListD == null ? false : (int)this.resultListD.Length > 0))
							{
								this.GetLiveConnect();
								this.listD[2] = this.resultListD;
								this.resultListD = null;
								this.SvSynch._ResiverPayments(this.listD);
								if (this.chb_UseClearList.Checked)
								{
									this.listD = null;
								}
							}
						}
					}
				}
				this.l_CurStat.Text = "";
			}
			catch (Exception exception)
			{
				this.WriteLog("Ошибка опроса", exception, "GetData");
			}
		}

		private ArrayList GetList(string Query)
		{
			ArrayList arrayLists;
			ArrayList arrayLists1 = new ArrayList();
			try
			{
				ArrayList arrayLists2 = null;
				SqlConnection sqlConnection = new SqlConnection(this.SQLconnString);
				SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
				sqlConnection.Open();
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				while (sqlDataReader.Read())
				{
					arrayLists2 = new ArrayList();
					for (int i = 0; i < sqlDataReader.FieldCount; i++)
					{
						if (!string.IsNullOrEmpty(sqlDataReader.GetValue(i).ToString()))
						{
							arrayLists2.Add(sqlDataReader.GetValue(i));
						}
					}
					arrayLists1.Add(arrayLists2);
				}
				sqlDataReader.Close();
				sqlConnection.Close();
				arrayLists = arrayLists1;
			}
			catch (Exception exception)
			{
				this.WriteLog(string.Concat("Ошибка выполнения запроса ", Query), exception, "GetList");
				arrayLists = arrayLists1;
			}
			return arrayLists;
		}

		private ArrayList GetListLVItems(string Query)
		{
			ArrayList arrayLists;
			try
			{
				ArrayList arrayLists1 = new ArrayList();
				ListViewItem listViewItem = null;
				string str = "";
				SqlConnection sqlConnection = new SqlConnection(this.SQLconnString);
				SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
				sqlConnection.Open();
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				while (sqlDataReader.Read())
				{
					listViewItem = new ListViewItem(sqlDataReader.GetValue(0).ToString());
					for (int i = 1; i < sqlDataReader.FieldCount; i++)
					{
						str = sqlDataReader.GetValue(i).ToString();
						if (!string.IsNullOrEmpty(str))
						{
							listViewItem.SubItems.Add(str);
						}
					}
					arrayLists1.Add(listViewItem);
				}
				sqlDataReader.Close();
				sqlConnection.Close();
				arrayLists = arrayLists1;
			}
			catch (Exception exception)
			{
				this.WriteLog(string.Concat("Ошибка выполнения запроса ", Query), exception, "GetListLVItems");
				arrayLists = null;
			}
			return arrayLists;
		}

		private object[] GetListOB(string Query)
		{
			int i;
			object[] objArray;
			this.ResultList = new ArrayList();
			try
			{
				this.tmpDate = DateTime.Now;
				SqlConnection sqlConnection = new SqlConnection(this.SQLconnString);
				try
				{
					SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
					try
					{
						sqlConnection.Open();
						SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
						try
						{
							while (sqlDataReader.Read())
							{
								this.tmp = new object[sqlDataReader.FieldCount];
								for (i = 0; i < sqlDataReader.FieldCount; i++)
								{
									if (!string.IsNullOrEmpty(sqlDataReader.GetValue(i).ToString()))
									{
										if (!(sqlDataReader.GetDataTypeName(i) == "datetime"))
										{
											this.tmp[i] = sqlDataReader.GetValue(i);
										}
										else
										{
											this.tmpDate = sqlDataReader.GetDateTime(i);
											this.tmp[i] = this.tmpDate.ToString("yyyy-MM-dd HH:mm:ss");
										}
									}
								}
								this.ResultList.Add(this.tmp);
							}
						}
						finally
						{
							if (sqlDataReader != null)
							{
								((IDisposable)sqlDataReader).Dispose();
							}
						}
					}
					finally
					{
						if (sqlCommand != null)
						{
							((IDisposable)sqlCommand).Dispose();
						}
					}
				}
				finally
				{
					if (sqlConnection != null)
					{
						((IDisposable)sqlConnection).Dispose();
					}
				}
				if ((this.ResultList == null ? true : this.ResultList.Count <= 0))
				{
					objArray = null;
				}
				else
				{
					this.olist = new object[this.ResultList.Count];
					for (i = 0; i < this.ResultList.Count; i++)
					{
						this.olist[i] = this.ResultList[i];
					}
					objArray = this.olist;
				}
			}
			catch (Exception exception)
			{
				this.WriteLog(string.Concat("Ошибка выполнения запроса ", Query), exception, "GetList");
				objArray = null;
			}
			return objArray;
		}

		private void GetLiveConnect()
		{
			try
			{
				this.l_CnLiveRes.Text = "NA";
				string str = "exec [gg-app].[master].dbo.spCheckCloseDB";
				SqlConnection sqlConnection = new SqlConnection(this.SQLconnString);
				SqlCommand sqlCommand = new SqlCommand(str, sqlConnection);
				sqlConnection.Open();
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				while (sqlDataReader.Read())
				{
					if (!(sqlDataReader.GetValue(0).ToString() == "0"))
					{
						this.DBIsClose = true;
					}
					else
					{
						this.DBIsClose = false;
					}
					this.l_CnLiveRes.Text = string.Concat("OK DBIsC ", this.DBIsClose.ToString());
				}
				sqlDataReader.Close();
				sqlConnection.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.l_CnLiveRes.Text = "BAD";
				this.WriteLog("Ошибка метода ", exception, "GetLiveConnect");
			}
			if (!this.DBIsClose)
			{
				this.l_DBisClose.Text = "false";
			}
			else
			{
				this.l_DBisClose.Text = "true";
			}
			if (this.DBIsClose)
			{
				if (this.btn_Stop.Enabled)
				{
					this.WriteLog("БД НЕ доступна. Обмен информацией прекрашен ", null, "GetLiveConnect");
					this.btn_Stop_Click(null, null);
				}
			}
			else if (this.btn_Start.Enabled)
			{
				this.WriteLog("БД ДОСТУПНА. Обмен информацией восстановлен ", null, "GetLiveConnect");
				this.btn_Start_Click(null, null);
			}
			string str1 = string.Concat("UPDATE [EPay].[dbo].[Config]  SET [ParamValue] = '", this.l_DBisClose.Text, "' where [ParamName]='DBisClose'");
			this.SaveData(str1);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(f_main));
			this.tc_Main = new TabControl();
			this.tabPage1 = new TabPage();
			this.groupBox4 = new GroupBox();
			this.l_CnLiveDateUpdate = new Label();
			this.l_CnLiveRes = new Label();
			this.label15 = new Label();
			this.l_CnLiveStatus = new Label();
			this.label14 = new Label();
			this.btn_CnLiveStop = new Button();
			this.btn_CnLiveStart = new Button();
			this.chb_UseDebugview = new CheckBox();
			this.p_Debug = new Panel();
			this.l_DBisClose = new Label();
			this.label16 = new Label();
			this.chb_UseClearList = new CheckBox();
			this.l_ReslitsCount = new Label();
			this.l_del = new Label();
			this.l_SILcount = new Label();
			this.l_SICcount = new Label();
			this.l_interval = new Label();
			this.l_time = new Label();
			this.label13 = new Label();
			this.label12 = new Label();
			this.label11 = new Label();
			this.label10 = new Label();
			this.label9 = new Label();
			this.label8 = new Label();
			this.groupBox2 = new GroupBox();
			this.btn_ChangeTime = new Button();
			this.nud_TimerInterval = new NumericUpDown();
			this.label1 = new Label();
			this.label2 = new Label();
			this.btn_Manual = new Button();
			this.btn_LoadData = new Button();
			this.l_Status = new Label();
			this.l_CurStat = new Label();
			this.label3 = new Label();
			this.btn_Stop = new Button();
			this.btn_Start = new Button();
			this.tabPage2 = new TabPage();
			this.splitContainer1 = new SplitContainer();
			this.btn_FormatXML = new Button();
			this.btn_RefreshServList = new Button();
			this.btn_DeleteFromList = new Button();
			this.btn_SaveServ = new Button();
			this.groupBox1 = new GroupBox();
			this.p_Services = new Panel();
			this.nud_ServiceTimeInterval = new NumericUpDown();
			this.label4 = new Label();
			this.label6 = new Label();
			this.cmb_Portal = new ComboBox();
			this.gb_Services = new GroupBox();
			this.lv_services = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.tabPage3 = new TabPage();
			this.panel2 = new Panel();
			this.txt_XMLformatData = new TextBox();
			this.panel1 = new Panel();
			this.btn_LoadListZam = new Button();
			this.btn_AttrInList = new Button();
			this.btn_AttrDelete = new Button();
			this.txt_Zam = new TextBox();
			this.txt_Attribut = new TextBox();
			this.label5 = new Label();
			this.label7 = new Label();
			this.groupBox3 = new GroupBox();
			this.lv_AttrList = new ListView();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.btn_SaveCodeWords = new Button();
			this.t_mainTimer = new System.Windows.Forms.Timer(this.components);
			this.timerForCnLive = new System.Windows.Forms.Timer(this.components);
			this.tabPage4 = new TabPage();
			this.groupBox5 = new GroupBox();
			this.btn_UseDBGefest = new Button();
			this.btn_NOuseDBGefest = new Button();
			this.label17 = new Label();
			this.l_UseDBGefest = new Label();
			this.groupBox6 = new GroupBox();
			this.btn_UpdateLocalDBTimerStart = new Button();
			this.btn_UpdateLocalDBTimerStop = new Button();
			this.label18 = new Label();
			this.cmb_UpdateLocalDBInterval = new ComboBox();
			this.label19 = new Label();
			this.l_lastUpLocalDBDate = new Label();
			this.label21 = new Label();
			this.l_lastUpLocalDBDateStatus = new Label();
			this.btn_UseGefestDBCheck = new Button();
			this.btn_UseDEBUG = new Button();
			this.label20 = new Label();
			this.l_UpdateDBStatus = new Label();
			this.tc_Main.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.p_Debug.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.nud_TimerInterval).BeginInit();
			this.tabPage2.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.nud_ServiceTimeInterval).BeginInit();
			this.gb_Services.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox6.SuspendLayout();
			base.SuspendLayout();
			this.tc_Main.Controls.Add(this.tabPage1);
			this.tc_Main.Controls.Add(this.tabPage2);
			this.tc_Main.Controls.Add(this.tabPage3);
			this.tc_Main.Controls.Add(this.tabPage4);
			this.tc_Main.Dock = DockStyle.Fill;
			this.tc_Main.Location = new Point(0, 0);
			this.tc_Main.Name = "tc_Main";
			this.tc_Main.SelectedIndex = 0;
			this.tc_Main.Size = new System.Drawing.Size(560, 373);
			this.tc_Main.TabIndex = 0;
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Controls.Add(this.chb_UseDebugview);
			this.tabPage1.Controls.Add(this.p_Debug);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.btn_Manual);
			this.tabPage1.Controls.Add(this.btn_LoadData);
			this.tabPage1.Controls.Add(this.l_Status);
			this.tabPage1.Controls.Add(this.l_CurStat);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.btn_Stop);
			this.tabPage1.Controls.Add(this.btn_Start);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(552, 347);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Настройки приложения";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.groupBox4.Controls.Add(this.l_CnLiveDateUpdate);
			this.groupBox4.Controls.Add(this.l_CnLiveRes);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.l_CnLiveStatus);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.btn_CnLiveStop);
			this.groupBox4.Controls.Add(this.btn_CnLiveStart);
			this.groupBox4.Location = new Point(14, 124);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(195, 111);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Проверка доступности сервера";
			this.l_CnLiveDateUpdate.AutoSize = true;
			this.l_CnLiveDateUpdate.Location = new Point(19, 88);
			this.l_CnLiveDateUpdate.Name = "l_CnLiveDateUpdate";
			this.l_CnLiveDateUpdate.Size = new System.Drawing.Size(22, 13);
			this.l_CnLiveDateUpdate.TabIndex = 6;
			this.l_CnLiveDateUpdate.Text = "NA";
			this.l_CnLiveRes.AutoSize = true;
			this.l_CnLiveRes.Location = new Point(88, 65);
			this.l_CnLiveRes.Name = "l_CnLiveRes";
			this.l_CnLiveRes.Size = new System.Drawing.Size(19, 13);
			this.l_CnLiveRes.TabIndex = 5;
			this.l_CnLiveRes.Text = "na";
			this.label15.AutoSize = true;
			this.label15.Location = new Point(19, 65);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(59, 13);
			this.label15.TabIndex = 4;
			this.label15.Text = "Результат";
			this.l_CnLiveStatus.AutoSize = true;
			this.l_CnLiveStatus.Location = new Point(88, 45);
			this.l_CnLiveStatus.Name = "l_CnLiveStatus";
			this.l_CnLiveStatus.Size = new System.Drawing.Size(19, 13);
			this.l_CnLiveStatus.TabIndex = 3;
			this.l_CnLiveStatus.Text = "na";
			this.label14.AutoSize = true;
			this.label14.Location = new Point(19, 45);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(41, 13);
			this.label14.TabIndex = 2;
			this.label14.Text = "Статус";
			this.btn_CnLiveStop.Enabled = false;
			this.btn_CnLiveStop.Location = new Point(87, 19);
			this.btn_CnLiveStop.Name = "btn_CnLiveStop";
			this.btn_CnLiveStop.Size = new System.Drawing.Size(75, 23);
			this.btn_CnLiveStop.TabIndex = 1;
			this.btn_CnLiveStop.Text = "Стоп";
			this.btn_CnLiveStop.UseVisualStyleBackColor = true;
			this.btn_CnLiveStop.Click += new EventHandler(this.btn_CnLiveStop_Click);
			this.btn_CnLiveStart.Location = new Point(6, 19);
			this.btn_CnLiveStart.Name = "btn_CnLiveStart";
			this.btn_CnLiveStart.Size = new System.Drawing.Size(75, 23);
			this.btn_CnLiveStart.TabIndex = 0;
			this.btn_CnLiveStart.Text = "Старт";
			this.btn_CnLiveStart.UseVisualStyleBackColor = true;
			this.btn_CnLiveStart.Click += new EventHandler(this.btn_CnLiveStart_Click);
			this.chb_UseDebugview.AutoSize = true;
			this.chb_UseDebugview.Location = new Point(14, 254);
			this.chb_UseDebugview.Name = "chb_UseDebugview";
			this.chb_UseDebugview.Size = new System.Drawing.Size(129, 30);
			this.chb_UseDebugview.TabIndex = 12;
			this.chb_UseDebugview.Text = "Отображать данные\r\n для отладки";
			this.chb_UseDebugview.UseVisualStyleBackColor = true;
			this.chb_UseDebugview.CheckedChanged += new EventHandler(this.chb_UseDebugview_CheckedChanged);
			this.p_Debug.Controls.Add(this.l_DBisClose);
			this.p_Debug.Controls.Add(this.label16);
			this.p_Debug.Controls.Add(this.chb_UseClearList);
			this.p_Debug.Controls.Add(this.l_ReslitsCount);
			this.p_Debug.Controls.Add(this.l_del);
			this.p_Debug.Controls.Add(this.l_SILcount);
			this.p_Debug.Controls.Add(this.l_SICcount);
			this.p_Debug.Controls.Add(this.l_interval);
			this.p_Debug.Controls.Add(this.l_time);
			this.p_Debug.Controls.Add(this.label13);
			this.p_Debug.Controls.Add(this.label12);
			this.p_Debug.Controls.Add(this.label11);
			this.p_Debug.Controls.Add(this.label10);
			this.p_Debug.Controls.Add(this.label9);
			this.p_Debug.Controls.Add(this.label8);
			this.p_Debug.Location = new Point(341, 104);
			this.p_Debug.Name = "p_Debug";
			this.p_Debug.Size = new System.Drawing.Size(200, 219);
			this.p_Debug.TabIndex = 11;
			this.p_Debug.Visible = false;
			this.l_DBisClose.AutoSize = true;
			this.l_DBisClose.Location = new Point(58, 196);
			this.l_DBisClose.Name = "l_DBisClose";
			this.l_DBisClose.Size = new System.Drawing.Size(0, 13);
			this.l_DBisClose.TabIndex = 19;
			this.label16.AutoSize = true;
			this.label16.Location = new Point(3, 196);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(55, 13);
			this.label16.TabIndex = 18;
			this.label16.Text = "DBisClose";
			this.chb_UseClearList.AutoSize = true;
			this.chb_UseClearList.Checked = true;
			this.chb_UseClearList.CheckState = CheckState.Checked;
			this.chb_UseClearList.Location = new Point(6, 163);
			this.chb_UseClearList.Name = "chb_UseClearList";
			this.chb_UseClearList.Size = new System.Drawing.Size(180, 17);
			this.chb_UseClearList.TabIndex = 17;
			this.chb_UseClearList.Text = "Использовать очистку списка";
			this.chb_UseClearList.UseVisualStyleBackColor = true;
			this.l_ReslitsCount.AutoSize = true;
			this.l_ReslitsCount.Location = new Point(60, 132);
			this.l_ReslitsCount.Name = "l_ReslitsCount";
			this.l_ReslitsCount.Size = new System.Drawing.Size(0, 13);
			this.l_ReslitsCount.TabIndex = 16;
			this.l_del.AutoSize = true;
			this.l_del.Location = new Point(60, 108);
			this.l_del.Name = "l_del";
			this.l_del.Size = new System.Drawing.Size(0, 13);
			this.l_del.TabIndex = 16;
			this.l_SILcount.AutoSize = true;
			this.l_SILcount.Location = new Point(58, 85);
			this.l_SILcount.Name = "l_SILcount";
			this.l_SILcount.Size = new System.Drawing.Size(0, 13);
			this.l_SILcount.TabIndex = 16;
			this.l_SICcount.AutoSize = true;
			this.l_SICcount.Location = new Point(58, 59);
			this.l_SICcount.Name = "l_SICcount";
			this.l_SICcount.Size = new System.Drawing.Size(0, 13);
			this.l_SICcount.TabIndex = 15;
			this.l_interval.AutoSize = true;
			this.l_interval.Location = new Point(58, 33);
			this.l_interval.Name = "l_interval";
			this.l_interval.Size = new System.Drawing.Size(0, 13);
			this.l_interval.TabIndex = 14;
			this.l_time.AutoSize = true;
			this.l_time.Location = new Point(58, 10);
			this.l_time.Name = "l_time";
			this.l_time.Size = new System.Drawing.Size(0, 13);
			this.l_time.TabIndex = 13;
			this.label13.AutoSize = true;
			this.label13.Location = new Point(5, 132);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(49, 13);
			this.label13.TabIndex = 6;
			this.label13.Text = "RLCount";
			this.label12.AutoSize = true;
			this.label12.Location = new Point(5, 108);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(15, 13);
			this.label12.TabIndex = 6;
			this.label12.Text = "%";
			this.label11.AutoSize = true;
			this.label11.Location = new Point(3, 85);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(53, 13);
			this.label11.TabIndex = 6;
			this.label11.Text = "SIL count";
			this.label10.AutoSize = true;
			this.label10.Location = new Point(3, 59);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(54, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "SIC count";
			this.label9.AutoSize = true;
			this.label9.Location = new Point(3, 33);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(42, 13);
			this.label9.TabIndex = 4;
			this.label9.Text = "Interval";
			this.label8.AutoSize = true;
			this.label8.Location = new Point(3, 10);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(30, 13);
			this.label8.TabIndex = 0;
			this.label8.Text = "Time";
			this.groupBox2.Controls.Add(this.btn_ChangeTime);
			this.groupBox2.Controls.Add(this.nud_TimerInterval);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new Point(189, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(210, 95);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Настройки таймера";
			this.btn_ChangeTime.Location = new Point(105, 61);
			this.btn_ChangeTime.Name = "btn_ChangeTime";
			this.btn_ChangeTime.Size = new System.Drawing.Size(85, 25);
			this.btn_ChangeTime.TabIndex = 7;
			this.btn_ChangeTime.Text = "Применить";
			this.btn_ChangeTime.UseVisualStyleBackColor = true;
			this.btn_ChangeTime.Click += new EventHandler(this.btn_ChangeTime_Click);
			this.nud_TimerInterval.Location = new Point(105, 17);
			NumericUpDown nudTimerInterval = this.nud_TimerInterval;
			int[] numArray = new int[] { 3600, 0, 0, 0 };
			nudTimerInterval.Maximum = new decimal(numArray);
			NumericUpDown num = this.nud_TimerInterval;
			numArray = new int[] { 10, 0, 0, 0 };
			num.Minimum = new decimal(numArray);
			this.nud_TimerInterval.Name = "nud_TimerInterval";
			this.nud_TimerInterval.Size = new System.Drawing.Size(71, 20);
			this.nud_TimerInterval.TabIndex = 2;
			NumericUpDown numericUpDown = this.nud_TimerInterval;
			numArray = new int[] { 10, 0, 0, 0 };
			numericUpDown.Value = new decimal(numArray);
			this.nud_TimerInterval.ValueChanged += new EventHandler(this.nud_TimerInterval_ValueChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Интервал опроса";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(182, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(25, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "сек";
			this.btn_Manual.Location = new Point(402, 54);
			this.btn_Manual.Name = "btn_Manual";
			this.btn_Manual.Size = new System.Drawing.Size(139, 44);
			this.btn_Manual.TabIndex = 9;
			this.btn_Manual.Text = "Выполнить ручной опрос";
			this.btn_Manual.UseVisualStyleBackColor = true;
			this.btn_Manual.Click += new EventHandler(this.btn_Manual_Click);
			this.btn_LoadData.Location = new Point(402, 6);
			this.btn_LoadData.Name = "btn_LoadData";
			this.btn_LoadData.Size = new System.Drawing.Size(139, 44);
			this.btn_LoadData.TabIndex = 8;
			this.btn_LoadData.Text = "Загрузить данные о сервисах";
			this.btn_LoadData.UseVisualStyleBackColor = true;
			this.btn_LoadData.Click += new EventHandler(this.btn_LoadData_Click);
			this.l_Status.AutoSize = true;
			this.l_Status.Location = new Point(186, 101);
			this.l_Status.Name = "l_Status";
			this.l_Status.Size = new System.Drawing.Size(0, 13);
			this.l_Status.TabIndex = 6;
			this.l_CurStat.AutoSize = true;
			this.l_CurStat.Location = new Point(8, 124);
			this.l_CurStat.Name = "l_CurStat";
			this.l_CurStat.Size = new System.Drawing.Size(0, 13);
			this.l_CurStat.TabIndex = 5;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(8, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(153, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Текущий статус приложения";
			this.btn_Stop.Enabled = false;
			this.btn_Stop.Location = new Point(11, 54);
			this.btn_Stop.Name = "btn_Stop";
			this.btn_Stop.Size = new System.Drawing.Size(172, 44);
			this.btn_Stop.TabIndex = 1;
			this.btn_Stop.Text = "Завершить опрос";
			this.btn_Stop.UseVisualStyleBackColor = true;
			this.btn_Stop.Click += new EventHandler(this.btn_Stop_Click);
			this.btn_Start.Location = new Point(11, 6);
			this.btn_Start.Name = "btn_Start";
			this.btn_Start.Size = new System.Drawing.Size(172, 44);
			this.btn_Start.TabIndex = 0;
			this.btn_Start.Text = "Начать опрос";
			this.btn_Start.UseVisualStyleBackColor = true;
			this.btn_Start.Click += new EventHandler(this.btn_Start_Click);
			this.tabPage2.Controls.Add(this.splitContainer1);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(552, 347);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Настройки сервисов";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = Orientation.Horizontal;
			this.splitContainer1.Panel1.Controls.Add(this.btn_FormatXML);
			this.splitContainer1.Panel1.Controls.Add(this.btn_RefreshServList);
			this.splitContainer1.Panel1.Controls.Add(this.btn_DeleteFromList);
			this.splitContainer1.Panel1.Controls.Add(this.btn_SaveServ);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1.Controls.Add(this.nud_ServiceTimeInterval);
			this.splitContainer1.Panel1.Controls.Add(this.label4);
			this.splitContainer1.Panel1.Controls.Add(this.label6);
			this.splitContainer1.Panel1.Controls.Add(this.cmb_Portal);
			this.splitContainer1.Panel2.Controls.Add(this.gb_Services);
			this.splitContainer1.Size = new System.Drawing.Size(546, 341);
			this.splitContainer1.SplitterDistance = 170;
			this.splitContainer1.TabIndex = 1;
			this.btn_FormatXML.Location = new Point(229, 60);
			this.btn_FormatXML.Name = "btn_FormatXML";
			this.btn_FormatXML.Size = new System.Drawing.Size(119, 23);
			this.btn_FormatXML.TabIndex = 11;
			this.btn_FormatXML.Text = "Формат XML";
			this.btn_FormatXML.UseVisualStyleBackColor = true;
			this.btn_FormatXML.Click += new EventHandler(this.btn_FormatXML_Click);
			this.btn_RefreshServList.Location = new Point(466, 9);
			this.btn_RefreshServList.Name = "btn_RefreshServList";
			this.btn_RefreshServList.Size = new System.Drawing.Size(75, 23);
			this.btn_RefreshServList.TabIndex = 10;
			this.btn_RefreshServList.Text = "Обновить";
			this.btn_RefreshServList.UseVisualStyleBackColor = true;
			this.btn_RefreshServList.Click += new EventHandler(this.btn_RefreshServList_Click);
			this.btn_DeleteFromList.Enabled = false;
			this.btn_DeleteFromList.Location = new Point(229, 143);
			this.btn_DeleteFromList.Name = "btn_DeleteFromList";
			this.btn_DeleteFromList.Size = new System.Drawing.Size(119, 23);
			this.btn_DeleteFromList.TabIndex = 9;
			this.btn_DeleteFromList.Text = "Удалить из списка";
			this.btn_DeleteFromList.UseVisualStyleBackColor = true;
			this.btn_DeleteFromList.Click += new EventHandler(this.btn_DeleteFromList_Click);
			this.btn_SaveServ.Location = new Point(466, 143);
			this.btn_SaveServ.Name = "btn_SaveServ";
			this.btn_SaveServ.Size = new System.Drawing.Size(75, 23);
			this.btn_SaveServ.TabIndex = 8;
			this.btn_SaveServ.Text = "Сохранить";
			this.btn_SaveServ.UseVisualStyleBackColor = true;
			this.btn_SaveServ.Click += new EventHandler(this.btn_SaveServ_Click);
			this.groupBox1.Controls.Add(this.p_Services);
			this.groupBox1.Dock = DockStyle.Left;
			this.groupBox1.Location = new Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(220, 170);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Доступные сервисы";
			this.p_Services.AutoScroll = true;
			this.p_Services.Dock = DockStyle.Fill;
			this.p_Services.Location = new Point(3, 16);
			this.p_Services.Name = "p_Services";
			this.p_Services.Size = new System.Drawing.Size(214, 151);
			this.p_Services.TabIndex = 0;
			this.nud_ServiceTimeInterval.Location = new Point(375, 33);
			NumericUpDown nudServiceTimeInterval = this.nud_ServiceTimeInterval;
			numArray = new int[] { 3600, 0, 0, 0 };
			nudServiceTimeInterval.Maximum = new decimal(numArray);
			NumericUpDown nudServiceTimeInterval1 = this.nud_ServiceTimeInterval;
			numArray = new int[] { 10, 0, 0, 0 };
			nudServiceTimeInterval1.Minimum = new decimal(numArray);
			this.nud_ServiceTimeInterval.Name = "nud_ServiceTimeInterval";
			this.nud_ServiceTimeInterval.Size = new System.Drawing.Size(51, 20);
			this.nud_ServiceTimeInterval.TabIndex = 7;
			NumericUpDown num1 = this.nud_ServiceTimeInterval;
			numArray = new int[] { 10, 0, 0, 0 };
			num1.Value = new decimal(numArray);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(226, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(36, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Шлюз";
			this.label6.AutoSize = true;
			this.label6.Location = new Point(226, 35);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(122, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Интервал опроса, сек.";
			this.cmb_Portal.FormattingEnabled = true;
			ComboBox.ObjectCollection items = this.cmb_Portal.Items;
			object[] objArray = new object[] { "ГорГаз" };
			items.AddRange(objArray);
			this.cmb_Portal.Location = new Point(281, 6);
			this.cmb_Portal.Name = "cmb_Portal";
			this.cmb_Portal.Size = new System.Drawing.Size(145, 21);
			this.cmb_Portal.TabIndex = 3;
			this.gb_Services.Controls.Add(this.lv_services);
			this.gb_Services.Dock = DockStyle.Fill;
			this.gb_Services.Location = new Point(0, 0);
			this.gb_Services.Name = "gb_Services";
			this.gb_Services.Size = new System.Drawing.Size(546, 167);
			this.gb_Services.TabIndex = 0;
			this.gb_Services.TabStop = false;
			this.gb_Services.Text = "Сервисы";
			ListView.ColumnHeaderCollection columns = this.lv_services.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5 };
			columns.AddRange(columnHeaderArray);
			this.lv_services.Dock = DockStyle.Fill;
			this.lv_services.FullRowSelect = true;
			this.lv_services.GridLines = true;
			this.lv_services.Location = new Point(3, 16);
			this.lv_services.MultiSelect = false;
			this.lv_services.Name = "lv_services";
			this.lv_services.Size = new System.Drawing.Size(540, 148);
			this.lv_services.TabIndex = 0;
			this.lv_services.UseCompatibleStateImageBehavior = false;
			this.lv_services.View = View.Details;
			this.lv_services.SelectedIndexChanged += new EventHandler(this.lv_services_SelectedIndexChanged);
			this.columnHeader1.Text = "Название";
			this.columnHeader1.Width = 109;
			this.columnHeader2.Text = "Шлюз";
			this.columnHeader2.Width = 177;
			this.columnHeader3.Text = "Формат";
			this.columnHeader3.Width = 175;
			this.columnHeader4.Text = "Интревал";
			this.columnHeader4.Width = 74;
			this.columnHeader5.Text = "id";
			this.columnHeader5.Width = 0;
			this.tabPage3.Controls.Add(this.panel2);
			this.tabPage3.Controls.Add(this.panel1);
			this.tabPage3.Location = new Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(552, 347);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "XML формат";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.panel2.Controls.Add(this.txt_XMLformatData);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(552, 230);
			this.panel2.TabIndex = 0;
			this.txt_XMLformatData.Dock = DockStyle.Fill;
			this.txt_XMLformatData.Location = new Point(0, 0);
			this.txt_XMLformatData.Multiline = true;
			this.txt_XMLformatData.Name = "txt_XMLformatData";
			this.txt_XMLformatData.Size = new System.Drawing.Size(552, 230);
			this.txt_XMLformatData.TabIndex = 0;
			this.panel1.Controls.Add(this.btn_LoadListZam);
			this.panel1.Controls.Add(this.btn_AttrInList);
			this.panel1.Controls.Add(this.btn_AttrDelete);
			this.panel1.Controls.Add(this.txt_Zam);
			this.panel1.Controls.Add(this.txt_Attribut);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.btn_SaveCodeWords);
			this.panel1.Dock = DockStyle.Bottom;
			this.panel1.Location = new Point(0, 230);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(552, 117);
			this.panel1.TabIndex = 1;
			this.btn_LoadListZam.Enabled = false;
			this.btn_LoadListZam.Location = new Point(115, 6);
			this.btn_LoadListZam.Name = "btn_LoadListZam";
			this.btn_LoadListZam.Size = new System.Drawing.Size(75, 23);
			this.btn_LoadListZam.TabIndex = 10;
			this.btn_LoadListZam.Text = "Загрузить";
			this.btn_LoadListZam.UseVisualStyleBackColor = true;
			this.btn_AttrInList.Location = new Point(115, 83);
			this.btn_AttrInList.Name = "btn_AttrInList";
			this.btn_AttrInList.Size = new System.Drawing.Size(75, 23);
			this.btn_AttrInList.TabIndex = 9;
			this.btn_AttrInList.Text = "Добавить";
			this.btn_AttrInList.UseVisualStyleBackColor = true;
			this.btn_AttrInList.Click += new EventHandler(this.btn_AttrInList_Click);
			this.btn_AttrDelete.Enabled = false;
			this.btn_AttrDelete.Location = new Point(8, 84);
			this.btn_AttrDelete.Name = "btn_AttrDelete";
			this.btn_AttrDelete.Size = new System.Drawing.Size(75, 23);
			this.btn_AttrDelete.TabIndex = 8;
			this.btn_AttrDelete.Text = "Удалить";
			this.btn_AttrDelete.UseVisualStyleBackColor = true;
			this.btn_AttrDelete.Click += new EventHandler(this.btn_AttrDelete_Click);
			this.txt_Zam.Location = new Point(83, 58);
			this.txt_Zam.Name = "txt_Zam";
			this.txt_Zam.Size = new System.Drawing.Size(107, 20);
			this.txt_Zam.TabIndex = 7;
			this.txt_Attribut.Location = new Point(83, 32);
			this.txt_Attribut.Name = "txt_Attribut";
			this.txt_Attribut.Size = new System.Drawing.Size(107, 20);
			this.txt_Attribut.TabIndex = 6;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(8, 61);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(69, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Заменитель";
			this.label7.AutoSize = true;
			this.label7.Location = new Point(8, 35);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(47, 13);
			this.label7.TabIndex = 2;
			this.label7.Text = "Атрибут";
			this.groupBox3.Controls.Add(this.lv_AttrList);
			this.groupBox3.Location = new Point(196, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(337, 100);
			this.groupBox3.TabIndex = 5;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Список заменителей";
			ListView.ColumnHeaderCollection columnHeaderCollections = this.lv_AttrList.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader6, this.columnHeader7 };
			columnHeaderCollections.AddRange(columnHeaderArray);
			this.lv_AttrList.Dock = DockStyle.Fill;
			this.lv_AttrList.FullRowSelect = true;
			this.lv_AttrList.GridLines = true;
			this.lv_AttrList.Location = new Point(3, 16);
			this.lv_AttrList.MultiSelect = false;
			this.lv_AttrList.Name = "lv_AttrList";
			this.lv_AttrList.Size = new System.Drawing.Size(331, 81);
			this.lv_AttrList.TabIndex = 0;
			this.lv_AttrList.UseCompatibleStateImageBehavior = false;
			this.lv_AttrList.View = View.Details;
			this.columnHeader6.Text = "Атрибут";
			this.columnHeader6.Width = 106;
			this.columnHeader7.Text = "Заменитель";
			this.columnHeader7.Width = 116;
			this.btn_SaveCodeWords.Enabled = false;
			this.btn_SaveCodeWords.Location = new Point(8, 6);
			this.btn_SaveCodeWords.Name = "btn_SaveCodeWords";
			this.btn_SaveCodeWords.Size = new System.Drawing.Size(75, 23);
			this.btn_SaveCodeWords.TabIndex = 4;
			this.btn_SaveCodeWords.Text = "Сохранить заменители";
			this.btn_SaveCodeWords.UseVisualStyleBackColor = true;
			this.t_mainTimer.Tick += new EventHandler(this.t_mainTimer_Tick);
			this.timerForCnLive.Interval = 250000;
			this.timerForCnLive.Tick += new EventHandler(this.timerForCnLive_Tick);
			this.tabPage4.Controls.Add(this.btn_UseDEBUG);
			this.tabPage4.Controls.Add(this.groupBox6);
			this.tabPage4.Controls.Add(this.groupBox5);
			this.tabPage4.Location = new Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(552, 347);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Дополнительно";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.groupBox5.Controls.Add(this.btn_UseGefestDBCheck);
			this.groupBox5.Controls.Add(this.l_UseDBGefest);
			this.groupBox5.Controls.Add(this.label17);
			this.groupBox5.Controls.Add(this.btn_NOuseDBGefest);
			this.groupBox5.Controls.Add(this.btn_UseDBGefest);
			this.groupBox5.Location = new Point(3, 6);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(200, 86);
			this.groupBox5.TabIndex = 0;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Обращение к БД Gefest";
			this.btn_UseDBGefest.Location = new Point(6, 19);
			this.btn_UseDBGefest.Name = "btn_UseDBGefest";
			this.btn_UseDBGefest.Size = new System.Drawing.Size(75, 23);
			this.btn_UseDBGefest.TabIndex = 0;
			this.btn_UseDBGefest.Text = "ВКЛ";
			this.btn_UseDBGefest.UseVisualStyleBackColor = true;
			this.btn_UseDBGefest.Click += new EventHandler(this.btn_UseDBGefest_Click);
			this.btn_NOuseDBGefest.Location = new Point(119, 19);
			this.btn_NOuseDBGefest.Name = "btn_NOuseDBGefest";
			this.btn_NOuseDBGefest.Size = new System.Drawing.Size(75, 23);
			this.btn_NOuseDBGefest.TabIndex = 1;
			this.btn_NOuseDBGefest.Text = "ВЫКЛ";
			this.btn_NOuseDBGefest.UseVisualStyleBackColor = true;
			this.btn_NOuseDBGefest.Click += new EventHandler(this.btn_NOuseDBGefest_Click);
			this.label17.AutoSize = true;
			this.label17.Location = new Point(6, 57);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(61, 13);
			this.label17.TabIndex = 2;
			this.label17.Text = "Состояние";
			this.l_UseDBGefest.AutoSize = true;
			this.l_UseDBGefest.Location = new Point(116, 57);
			this.l_UseDBGefest.Name = "l_UseDBGefest";
			this.l_UseDBGefest.Size = new System.Drawing.Size(27, 13);
			this.l_UseDBGefest.TabIndex = 3;
			this.l_UseDBGefest.Text = "N/A";
			this.groupBox6.Controls.Add(this.l_UpdateDBStatus);
			this.groupBox6.Controls.Add(this.label20);
			this.groupBox6.Controls.Add(this.l_lastUpLocalDBDateStatus);
			this.groupBox6.Controls.Add(this.label21);
			this.groupBox6.Controls.Add(this.l_lastUpLocalDBDate);
			this.groupBox6.Controls.Add(this.label19);
			this.groupBox6.Controls.Add(this.cmb_UpdateLocalDBInterval);
			this.groupBox6.Controls.Add(this.label18);
			this.groupBox6.Controls.Add(this.btn_UpdateLocalDBTimerStop);
			this.groupBox6.Controls.Add(this.btn_UpdateLocalDBTimerStart);
			this.groupBox6.Location = new Point(3, 98);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(200, 154);
			this.groupBox6.TabIndex = 1;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Обновление базы потребителей";
			this.btn_UpdateLocalDBTimerStart.Location = new Point(6, 19);
			this.btn_UpdateLocalDBTimerStart.Name = "btn_UpdateLocalDBTimerStart";
			this.btn_UpdateLocalDBTimerStart.Size = new System.Drawing.Size(75, 23);
			this.btn_UpdateLocalDBTimerStart.TabIndex = 0;
			this.btn_UpdateLocalDBTimerStart.Text = "ВКЛ";
			this.btn_UpdateLocalDBTimerStart.UseVisualStyleBackColor = true;
			this.btn_UpdateLocalDBTimerStart.Click += new EventHandler(this.btn_UpdateLocalDBTimerStart_Click);
			this.btn_UpdateLocalDBTimerStop.Enabled = false;
			this.btn_UpdateLocalDBTimerStop.Location = new Point(119, 19);
			this.btn_UpdateLocalDBTimerStop.Name = "btn_UpdateLocalDBTimerStop";
			this.btn_UpdateLocalDBTimerStop.Size = new System.Drawing.Size(75, 23);
			this.btn_UpdateLocalDBTimerStop.TabIndex = 1;
			this.btn_UpdateLocalDBTimerStop.Text = "ВЫКЛ";
			this.btn_UpdateLocalDBTimerStop.UseVisualStyleBackColor = true;
			this.btn_UpdateLocalDBTimerStop.Click += new EventHandler(this.btn_UpdateLocalDBTimerStop_Click);
			this.label18.AutoSize = true;
			this.label18.Location = new Point(6, 57);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(56, 13);
			this.label18.TabIndex = 2;
			this.label18.Text = "Интервал";
			this.cmb_UpdateLocalDBInterval.FormattingEnabled = true;
			ComboBox.ObjectCollection objectCollections = this.cmb_UpdateLocalDBInterval.Items;
			objArray = new object[] { "1", "5", "10", "15", "20", "25", "27" };
			objectCollections.AddRange(objArray);
			this.cmb_UpdateLocalDBInterval.Location = new Point(119, 48);
			this.cmb_UpdateLocalDBInterval.Name = "cmb_UpdateLocalDBInterval";
			this.cmb_UpdateLocalDBInterval.Size = new System.Drawing.Size(75, 21);
			this.cmb_UpdateLocalDBInterval.TabIndex = 3;
			this.cmb_UpdateLocalDBInterval.SelectedIndexChanged += new EventHandler(this.cmb_UpdateLocalDBInterval_SelectedIndexChanged);
			this.label19.AutoSize = true;
			this.label19.Location = new Point(6, 84);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(87, 13);
			this.label19.TabIndex = 4;
			this.label19.Text = "Дата посл. обн.";
			this.l_lastUpLocalDBDate.AutoSize = true;
			this.l_lastUpLocalDBDate.Location = new Point(116, 84);
			this.l_lastUpLocalDBDate.Name = "l_lastUpLocalDBDate";
			this.l_lastUpLocalDBDate.Size = new System.Drawing.Size(27, 13);
			this.l_lastUpLocalDBDate.TabIndex = 5;
			this.l_lastUpLocalDBDate.Text = "N/A";
			this.label21.AutoSize = true;
			this.label21.Location = new Point(6, 106);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(87, 13);
			this.label21.TabIndex = 6;
			this.label21.Text = "Последнее обн.";
			this.l_lastUpLocalDBDateStatus.AutoSize = true;
			this.l_lastUpLocalDBDateStatus.Location = new Point(116, 106);
			this.l_lastUpLocalDBDateStatus.Name = "l_lastUpLocalDBDateStatus";
			this.l_lastUpLocalDBDateStatus.Size = new System.Drawing.Size(27, 13);
			this.l_lastUpLocalDBDateStatus.TabIndex = 7;
			this.l_lastUpLocalDBDateStatus.Text = "N/A";
			this.btn_UseGefestDBCheck.Location = new Point(171, 52);
			this.btn_UseGefestDBCheck.Name = "btn_UseGefestDBCheck";
			this.btn_UseGefestDBCheck.Size = new System.Drawing.Size(23, 23);
			this.btn_UseGefestDBCheck.TabIndex = 4;
			this.btn_UseGefestDBCheck.Text = "|";
			this.btn_UseGefestDBCheck.UseVisualStyleBackColor = true;
			this.btn_UseGefestDBCheck.Click += new EventHandler(this.btn_UseGefestDBCheck_Click);
			this.btn_UseDEBUG.Location = new Point(209, 25);
			this.btn_UseDEBUG.Name = "btn_UseDEBUG";
			this.btn_UseDEBUG.Size = new System.Drawing.Size(75, 23);
			this.btn_UseDEBUG.TabIndex = 2;
			this.btn_UseDEBUG.Text = "DEBUG";
			this.btn_UseDEBUG.UseVisualStyleBackColor = true;
			this.btn_UseDEBUG.Click += new EventHandler(this.btn_UseDEBUG_Click);
			this.label20.AutoSize = true;
			this.label20.Location = new Point(6, 128);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(61, 13);
			this.label20.TabIndex = 8;
			this.label20.Text = "Состояние";
			this.l_UpdateDBStatus.AutoSize = true;
			this.l_UpdateDBStatus.Location = new Point(116, 128);
			this.l_UpdateDBStatus.Name = "l_UpdateDBStatus";
			this.l_UpdateDBStatus.Size = new System.Drawing.Size(27, 13);
			this.l_UpdateDBStatus.TabIndex = 9;
			this.l_UpdateDBStatus.Text = "N/A";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(560, 373);
			base.Controls.Add(this.tc_Main);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "f_main";
			this.Text = "PayNavigator";
			base.FormClosing += new FormClosingEventHandler(this.f_main_FormClosing);
			base.Load += new EventHandler(this.f_main_Load);
			this.tc_Main.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.p_Debug.ResumeLayout(false);
			this.p_Debug.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.nud_TimerInterval).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.nud_ServiceTimeInterval).EndInit();
			this.gb_Services.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			base.ResumeLayout(false);
		}

		private string LoadServicesInfo()
		{
			string str = "";
			try
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection(this.strConnectDB);
				try
				{
					SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
					try
					{
						sQLiteConnection.Open();
						sQLiteCommand.CommandText = "select ProjectName,ProjectPlace,IsChecked from Project";
						SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
						try
						{
							while (sQLiteDataReader.Read())
							{
							}
						}
						finally
						{
							if (sQLiteDataReader != null)
							{
								sQLiteDataReader.Dispose();
							}
						}
					}
					finally
					{
						if (sQLiteCommand != null)
						{
							sQLiteCommand.Dispose();
						}
					}
				}
				finally
				{
					if (sQLiteConnection != null)
					{
						sQLiteConnection.Dispose();
					}
				}
				this.lv_services.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка чтения значения ", (exception.InnerException != null ? string.Concat(exception.Message, Environment.NewLine, exception.InnerException.Message) : exception.Message)), "LoadProjectsInfo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return str;
		}

		private void lv_services_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.btn_DeleteFromList.Enabled = true;
		}

		private void nud_TimerInterval_ValueChanged(object sender, EventArgs e)
		{
			this.btn_ChangeTime.Enabled = true;
		}

		private ArrayList ReadFromLocalDB(string query)
		{
			ArrayList arrayLists = new ArrayList();
			try
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection(this.strConnectDB);
				try
				{
					SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
					try
					{
						sQLiteConnection.Open();
						sQLiteCommand.CommandText = query;
						SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
						try
						{
							while (sQLiteDataReader.Read())
							{
								ListViewItem listViewItem = new ListViewItem(sQLiteDataReader.GetValue(0).ToString());
								for (int i = 1; i < sQLiteDataReader.FieldCount; i++)
								{
									listViewItem.SubItems.Add(sQLiteDataReader.GetValue(i).ToString());
								}
								arrayLists.Add(listViewItem);
							}
						}
						finally
						{
							if (sQLiteDataReader != null)
							{
								sQLiteDataReader.Dispose();
							}
						}
					}
					finally
					{
						if (sQLiteCommand != null)
						{
							sQLiteCommand.Dispose();
						}
					}
				}
				finally
				{
					if (sQLiteConnection != null)
					{
						sQLiteConnection.Dispose();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка чтения значения ", (exception.InnerException != null ? string.Concat(exception.Message, Environment.NewLine, exception.InnerException.Message) : exception.Message)), "ReadFromLocalDB", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return arrayLists;
		}

		private string ReadParam(string paramName)
		{
			string str = "";
			try
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection(this.strConnectDB);
				try
				{
					SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
					try
					{
						sQLiteConnection.Open();
						sQLiteCommand.CommandText = string.Concat("select paramName,paramvalue from Config where paramName='", paramName, "' order by IDParam limit 1");
						SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
						try
						{
							while (sQLiteDataReader.Read())
							{
								str = sQLiteDataReader.GetValue(1).ToString();
							}
						}
						finally
						{
							if (sQLiteDataReader != null)
							{
								sQLiteDataReader.Dispose();
							}
						}
					}
					finally
					{
						if (sQLiteCommand != null)
						{
							sQLiteCommand.Dispose();
						}
					}
				}
				finally
				{
					if (sQLiteConnection != null)
					{
						sQLiteConnection.Dispose();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка чтения значения ", paramName, " ", (exception.InnerException != null ? string.Concat(exception.Message, Environment.NewLine, exception.InnerException.Message) : exception.Message)), "SaveInLocalDB", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return str;
		}

		private void RefreshData()
		{
			this.cmb_Portal.SelectedIndex = -1;
			if (!string.IsNullOrEmpty(this.txt_XMLformatData.Text))
			{
				if (MessageBox.Show("Очистить поле ввода формата XML?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					this.txt_XMLformatData.Text = "";
				}
			}
			this.p_Services.Controls.Clear();
			CheckBox checkBox = null;
			int num = 3;
			foreach (ArrayList list in this.GetList("SELECT [IDService],[ServiceName]  FROM [Service]"))
			{
				checkBox = new CheckBox()
				{
					Name = string.Concat("chb_", list[1].ToString().Substring(0, 3)),
					Text = list[1].ToString(),
					Tag = list[0],
					Dock = DockStyle.Top,
					Location = new Point(3, num)
				};
				this.p_Services.Controls.Add(checkBox);
				num += 20;
			}
			this.lv_services.Items.Clear();
			string str = "select s.ServiceName,ss.[Portal],ss.[DataFormat],ss.[Interval],ss.[IDRow] from [ServiceSettings] ss inner join [Service] s on s.IDService=ss.IDService";
			this.lv_services.Items.AddRange((ListViewItem[])this.GetListLVItems(str).ToArray(typeof(ListViewItem)));
		}

		private void SaveData(string Query)
		{
			try
			{
				SqlConnection sqlConnection = new SqlConnection(this.SQLconnString);
				SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);
				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (this.UseDebug)
				{
					MessageBox.Show(string.Concat("SaveData :", exception.Message, (exception.InnerException != null ? string.Concat(Environment.NewLine, exception.InnerException.Message) : "")), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.WriteLog(string.Concat("Ошибка выполнения запроса ", Query), exception, "SaveData");
				}
			}
		}

		private bool SaveInLocalDB(string query)
		{
			bool flag = false;
			try
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection(this.strConnectDB);
				try
				{
					SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
					try
					{
						sQLiteConnection.Open();
						sQLiteCommand.CommandText = query;
						sQLiteCommand.ExecuteNonQuery();
						flag = true;
					}
					finally
					{
						if (sQLiteCommand != null)
						{
							sQLiteCommand.Dispose();
						}
					}
				}
				finally
				{
					if (sQLiteConnection != null)
					{
						sQLiteConnection.Dispose();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка сохранения значения ", (exception.InnerException != null ? string.Concat(exception.Message, Environment.NewLine, exception.InnerException.Message) : exception.Message)), "SaveInLocalDB", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return flag;
		}

		private bool SaveParam(string paramName, string paramValue)
		{
			string[] strArrays;
			bool flag = false;
			try
			{
				string str = "";
				if (string.IsNullOrEmpty(this.ReadParam(paramName)))
				{
					strArrays = new string[] { "insert into Config (paramName,paramvalue) values ('", paramName, "','", paramValue, "')" };
					str = string.Concat(strArrays);
				}
				else
				{
					strArrays = new string[] { "update Config set paramvalue='", paramValue, "' where paramName='", paramName, "'" };
					str = string.Concat(strArrays);
				}
				SQLiteConnection sQLiteConnection = new SQLiteConnection(this.strConnectDB);
				try
				{
					SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
					try
					{
						sQLiteConnection.Open();
						sQLiteCommand.CommandText = str;
						sQLiteCommand.ExecuteNonQuery();
						flag = true;
					}
					finally
					{
						if (sQLiteCommand != null)
						{
							sQLiteCommand.Dispose();
						}
					}
				}
				finally
				{
					if (sQLiteConnection != null)
					{
						sQLiteConnection.Dispose();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				strArrays = new string[] { "Ошибка сохранения значения ", paramName, " ", paramValue, " ", null };
				strArrays[5] = (exception.InnerException != null ? string.Concat(exception.Message, Environment.NewLine, exception.InnerException.Message) : exception.Message);
				MessageBox.Show(string.Concat(strArrays), "SaveInLocalDB", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.WriteLog("Ошибка сохранения параметров приложения", exception, "SaveParam");
			}
			return flag;
		}

		private bool SaveProjectInfo(string ProjectName, string ProjectPlace, bool isChecked)
		{
			return false;
		}

		private void SaveServiceSettings(string ServID)
		{
			object[] servID;
			try
			{
				string str = "";
				ArrayList list = this.GetList(string.Concat("select [IDRow] from [ServiceSettings] where [IDService]=", ServID));
				if ((list == null ? false : list.Count > 0))
				{
					str = ((ArrayList)list[0] == null || ((ArrayList)list[0]).Count <= 0 ? "" : ((ArrayList)list[0])[0].ToString());
				}
				if (string.IsNullOrEmpty(str))
				{
					servID = new object[] { "INSERT INTO [EPay].[dbo].[ServiceSettings] ([IDService],[Portal],[DataFormat],[DataFormatChar],[Interval])     VALUES(", ServID, ",'", this.cmb_Portal.Text, "','", this.txt_XMLformatData.Text, "','", this.txt_XMLformatData.Text, "',", this.nud_TimerInterval.Value, ")" };
					this.SaveData(string.Concat(servID));
				}
				else
				{
					servID = new object[] { "UPDATE [EPay].[dbo].[ServiceSettings]  SET [Portal] = '", this.cmb_Portal.Text, "' ,[DataFormat] = '", this.txt_XMLformatData.Text, "' ,[DataFormatChar] = '", this.txt_XMLformatData.Text, "' ,[Interval] = ", this.nud_ServiceTimeInterval.Value, "WHERE [IDRow]=", str };
					this.SaveData(string.Concat(servID));
				}
			}
			catch (Exception exception)
			{
				this.WriteLog("Ошибка загрузки приложения", exception, "f_main_Load");
			}
		}

		private void SendPayments(ArrayList Payment, string pattAnswer)
		{
			try
			{
				if ((Payment == null ? false : Payment.Count > 0))
				{
					pattAnswer = pattAnswer.Replace("phonenumber", Payment[0].ToString());
					pattAnswer = pattAnswer.Replace("amountPay", Payment[1].ToString());
				}
			}
			catch (Exception exception)
			{
				this.WriteLog("Ошибка отправки платежа", exception, "SendPayments");
			}
		}

		private string Sign(string StrForSign, string FileName, string pwd)
		{
			string base64String;
			try
			{
				X509Certificate2 x509Certificate2 = null;
				RSACryptoServiceProvider privateKey = null;
				if (this.UseLocalStore)
				{
					X509Store x509Store = new X509Store(StoreLocation.CurrentUser);
					x509Store.Open(OpenFlags.ReadOnly);
					X509Certificate2Enumerator enumerator = x509Store.Certificates.GetEnumerator();
					while (enumerator.MoveNext())
					{
						X509Certificate2 current = enumerator.Current;
						if (current.Subject == this.certName)
						{
							x509Certificate2 = current;
							break;
						}
					}
					if (x509Certificate2 != null)
					{
						privateKey = (RSACryptoServiceProvider)x509Certificate2.PrivateKey;
						x509Store.Close();
					}
					else
					{
						this.WriteLog("В локальном хранилище не найдет требуемый сертификат", null, "Sign");
						base64String = "";
						return base64String;
					}
				}
				else if (!File.Exists(FileName))
				{
					this.WriteLog("по указнному пути не найдет требуемый сертификат", null, "Sign");
					base64String = "";
					return base64String;
				}
				else
				{
					privateKey = (RSACryptoServiceProvider)(new X509Certificate2(FileName, pwd)).PrivateKey;
				}
				if (privateKey == null)
				{
					this.WriteLog("Пустой криптосервиспровайдер", null, "Sign");
					base64String = "";
				}
				else
				{
					byte[] numArray = privateKey.SignData(this.ConvertStringToByteArray(StrForSign), "SHA1");
					Array.Reverse(numArray);
					base64String = Convert.ToBase64String(numArray, Base64FormattingOptions.None);
				}
			}
			catch (Exception exception)
			{
				this.WriteLog("Ошибка создания подписи", exception, "Sign");
				base64String = "";
			}
			return base64String;
		}

		private void t_mainTimer_Tick(object sender, EventArgs e)
		{
			if (this.time >= 3800)
			{
				this.time = 10;
			}
			else
			{
				f_main interval = this;
				interval.time = interval.time + this.t_mainTimer.Interval / 1000;
			}
			this.GetData();
		}

		private void t_UpdateLocalDBTimer_Tick(object sender, EventArgs e)
		{
			if (this.DateUpdateLocal == DateTime.Now)
			{
				this.SaveData("exec spUpdateConsumers");
				this.SaveData("exec spUpdateConsumers");
			}
		}

		private void timerForCnLive_Tick(object sender, EventArgs e)
		{
			this.l_CnLiveDateUpdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
			this.GetLiveConnect();
		}

		private void UpdateLocalDB()
		{
			try
			{
				this.DoUpdateLocalDB();
			}
			catch (Exception exception)
			{
				this.WriteLog("Ошибка в потоке для ежедневных отчетов", exception, "EDReportWorkMetod");
			}
			if (this.UpdateThread != null)
			{
				this.UpdateThread.Abort();
			}
		}

		private bool Verify(string StrForVerify, string Sign, string FileName, string pwd)
		{
			bool flag;
			try
			{
				X509Certificate2 x509Certificate2 = null;
				RSACryptoServiceProvider key = null;
				if (this.UseLocalStore)
				{
					X509Store x509Store = new X509Store(StoreLocation.CurrentUser);
					x509Store.Open(OpenFlags.ReadOnly);
					X509Certificate2Enumerator enumerator = x509Store.Certificates.GetEnumerator();
					while (enumerator.MoveNext())
					{
						X509Certificate2 current = enumerator.Current;
						if (current.Subject == this.certName)
						{
							x509Certificate2 = current;
							break;
						}
					}
					if (x509Certificate2 != null)
					{
						key = (RSACryptoServiceProvider)x509Certificate2.PublicKey.Key;
						x509Store.Close();
					}
					else
					{
						this.WriteLog("В локальном хранилище не найдет требуемый сертификат", null, "Verify");
						flag = false;
						return flag;
					}
				}
				else if (!File.Exists(FileName))
				{
					this.WriteLog("По указанному пути не найдет требуемый сертификат", null, "Verify");
					flag = false;
					return flag;
				}
				else
				{
					key = (RSACryptoServiceProvider)(new X509Certificate2(FileName, pwd)).PublicKey.Key;
				}
				if (key == null)
				{
					this.WriteLog("Пустой криптосервиспровайдер", null, "Verify");
					flag = false;
				}
				else
				{
					byte[] byteArray = this.ConvertStringToByteArray(StrForVerify);
					byte[] numArray = Convert.FromBase64String(Sign);
					Array.Reverse(numArray);
					flag = key.VerifyData(byteArray, "SHA1", numArray);
				}
			}
			catch (Exception exception)
			{
				this.WriteLog("Ошибка проверки подписи", exception, "Verify");
				flag = false;
			}
			return flag;
		}

		private void WriteLog(string TextLog, Exception ExMessage, string MetodName)
		{
			object obj;
			try
			{
				if (!Directory.Exists("Logs"))
				{
					Directory.CreateDirectory("Logs");
					this.WriteLog(TextLog, ExMessage, MetodName);
				}
				else
				{
					DateTime now = DateTime.Now;
					StreamWriter streamWriter = new StreamWriter(string.Concat("Logs\\NE_", now.ToString("ddMMyyyy"), ".txt"), true);
					try
					{
						StreamWriter streamWriter1 = streamWriter;
						object[] objArray = new object[] { DateTime.Now, "  ", TextLog, "  ", MetodName, "  ", null };
						object[] objArray1 = objArray;
						if (ExMessage != null)
						{
							obj = (ExMessage.InnerException != null ? string.Concat(ExMessage.Message, "  ", ExMessage.InnerException.Message) : ExMessage.Message);
						}
						else
						{
							obj = "";
						}
						objArray1[6] = obj;
						streamWriter1.WriteLine(string.Concat(objArray));
					}
					finally
					{
						if (streamWriter != null)
						{
							((IDisposable)streamWriter).Dispose();
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(exception.Message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}