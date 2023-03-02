using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmActionGMeter : Form
	{
		private IContainer components;

		private GroupBox groupBox2;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private ImageList imageList1;

		private Label label2;

		private Label lblNameGRU;

		private Label label4;

		private GroupBox groupBox1;

		private C1DateEdit dtDateFabrication;

		private Label label3;

		private C1DateEdit dtDateVerify;

		private Label label1;

		private C1DateEdit dtDateInstall;

		private Label label6;

		private TextBox txtSerialNumber;

		private Label label5;

		private C1Combo cmbTypeGMeter;

		private Label label7;

		private Label lblAccount;

		private Label lblOU;

		private Button cmdTypeGMeter;

		private NumericUpDown numBeginIndication;

		private Label label18;

		private Label label8;

		private C1Combo cmbPU;

		private GroupBox groupBox4;

		private C1DateEdit dtDate;

		private C1Combo cmbMechanic;

		private Label label9;

		private Label label13;

		private Label label14;

		private NumericUpDown numLastIndication;

		private TextBox txtNote;

		private Label label17;

		private Button cmdClose;

		private Button cmdOK;

		private GroupBox grNewPU;

		private TypeGMeters _typegmeters;

		private TypeReasonDisconnects _TypeReasonDisconnects;

		private Agents _agents;

		private Gmeters _gmeters;

		private Button cmdFindRepeat;

		private C1Combo CmbTypeReasonDisconect;

		private Label label15;

		private Gmeter _gmeter;

		public frmActionGMeter(Gmeter oGmeter)
		{
			this.InitializeComponent();
			this._gmeter = oGmeter;
		}

		private void cmbMechanic_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDate.Focus();
			}
		}

		private void cmbPU_TextChanged(object sender, EventArgs e)
		{
			if (this.cmbPU.SelectedIndex != 0)
			{
				if (this.cmbPU.SelectedIndex > 0)
				{
					this._gmeter = this._gmeters[this.cmbPU.SelectedIndex];
				}
				this.grNewPU.Enabled = false;
			}
			else
			{
				this.grNewPU.Enabled = true;
				if (this._typegmeters == null)
				{
					this._typegmeters = new TypeGMeters();
					this._typegmeters.Load();
					this.CreateTypeGMeter((long)0);
					return;
				}
			}
		}

		private void cmbTypeGMeter_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDateFabrication.Focus();
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdFindRepeat_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					if (this.txtSerialNumber.Text.Length != 0)
					{
						System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
						int[] numArray = new int[] { 1, 2 };
						string[] text = new string[] { "@serialnumber", "@idtypegmeter" };
						string[] strArrays = text;
						text = new string[] { this.txtSerialNumber.Text, null };
						long d = this._typegmeters[this.cmbTypeGMeter.SelectedIndex].get_ID();
						text[1] = d.ToString();
						string[] strArrays1 = text;
						string str = string.Concat(Depot.oSettings.ReportPath.Trim(), "repDubleNumberPU.rpt");
						Form frmReport = new frmReports(str, strArrays, strArrays1, numArray)
						{
							Text = "Справка по ПУ с одинаковыми номерами",
							TopMost = true
						};
						frmReport.ShowDialog();
						frmReport = null;
					}
					else
					{
						return;
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.cmbMechanic.SelectedIndex != -1)
					{
						Indication num = null;
						if (this.grNewPU.Enabled)
						{
							if (this.cmbTypeGMeter.SelectedIndex != -1)
							{
								Gmeters gmeter = new Gmeters();
								if (gmeter.Load(this.txtSerialNumber.Text.Trim(), this._typegmeters[this.cmbTypeGMeter.SelectedIndex].get_ID()) != 0 || gmeter.get_Count() <= 0)
								{
									this._gmeter.oTypeGMeter = this._typegmeters[this.cmbTypeGMeter.SelectedIndex];
									this._gmeter.SerialNumber = this.txtSerialNumber.Text;
									this._gmeter.BeginValue = Convert.ToDouble(this.numBeginIndication.Value);
									this._gmeter.DateInstall = (DateTime)this.dtDateInstall.Value;
									this._gmeter.DateVerify = (DateTime)this.dtDateVerify.Value;
									this._gmeter.DateFabrication = (DateTime)this.dtDateFabrication.Value;
									this._gmeter.Memo = this.txtNote.Text;
									this._gmeter.oTypeVerify = Depot.oTypeVerifys.item((long)1);
									if (this._gmeter.Save() == 0)
									{
										num = this._gmeter.oIndications.Add();
										num.Display = Convert.ToDouble(this.numBeginIndication.Value);
										num.Datedisplay = (DateTime)this.dtDateInstall.Value;
										num.oTypeIndication = Depot.oTypeIndications.item((long)5);
										num.oAgent = this._agents[this.cmbMechanic.SelectedIndex];
										if (num.Save(false) != 0)
										{
											MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
											return;
										}
									}
									else
									{
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								else
								{
									MessageBox.Show("ПУ данной марки с таким номером уже существует!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									return;
								}
							}
							else
							{
								MessageBox.Show("Не выбран тип ПУ!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
						}
						if (this.numLastIndication.Visible && this.numLastIndication.Value > new decimal(0))
						{
							num = this._gmeter.oIndications.Add();
							num.Display = Convert.ToDouble(this.numLastIndication.Value);
							num.Datedisplay = (DateTime)this.dtDate.Value;
							num.oTypeIndication = Depot.oTypeIndications.item((long)5);
							num.oAgent = this._agents[this.cmbMechanic.SelectedIndex];
							if (this._gmeter.oStatusGMeter.get_ID() == (long)2)
							{
								if (num.Save(false) != 0)
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							else if (num.Save() != 0)
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
						Document document = new Document()
						{
							oBatch = null,
							oContract = this._gmeter.oGobject.oContract,
							oPeriod = Depot.CurrentPeriod
						};
						if (this._gmeter.oStatusGMeter.get_ID() != (long)2)
						{
							document.oTypeDocument = Depot.oTypeDocuments.item((long)18);
							document.DocumentDate = (DateTime)this.dtDate.Value;
							this._gmeter.DateOnOff = (DateTime)this.dtDate.Value;
						}
						else
						{
							document.oTypeDocument = Depot.oTypeDocuments.item((long)12);
							if (!this.grNewPU.Enabled)
							{
								document.DocumentDate = (DateTime)this.dtDate.Value;
								this._gmeter.DateOnOff = (DateTime)this.dtDate.Value;
							}
							else
							{
								document.DocumentDate = (DateTime)this.dtDateInstall.Value;
								this._gmeter.DateOnOff = (DateTime)this.dtDate.Value;
							}
						}
						document.Note = this.txtNote.Text;
						if (document.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							if (this._gmeter.oStatusGMeter.get_ID() != (long)2)
							{
								this._gmeter.oStatusGMeter = Depot.oStatusGMeters.item((long)2);
							}
							else
							{
								this._gmeter.oStatusGMeter = Depot.oStatusGMeters.item((long)1);
							}
							if (this._gmeter.Save() == 0)
							{
								PD str = document.oPDs.Add();
								str.oTypePD = Depot.oTypePDs.item((long)7);
								str.oDocument = document;
								long d = this._gmeter.get_ID();
								str.Value = d.ToString();
								if (str.Save() == 0)
								{
									str = document.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)16);
									str.oDocument = document;
									d = this._agents[this.cmbMechanic.SelectedIndex].get_ID();
									str.Value = d.ToString();
									if (str.Save() == 0)
									{
										if (this.CmbTypeReasonDisconect.Visible)
										{
											str = document.oPDs.Add();
											str.oTypePD = Depot.oTypePDs.item((long)33);
											str.oDocument = document;
											d = this._TypeReasonDisconnects[this.CmbTypeReasonDisconect.SelectedIndex].get_ID();
											str.Value = d.ToString();
											if (str.Save() != 0)
											{
												document.Delete();
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
										}
										if (num != null)
										{
											str = document.oPDs.Add();
											str.oTypePD = Depot.oTypePDs.item((long)1);
											str.oDocument = document;
											str.Value = num.get_ID().ToString();
											if (str.Save() != 0)
											{
												document.Delete();
												MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												return;
											}
											else if (num.oFactUses.get_Count() > 0)
											{
												num.oFactUses[0].oDocument = document;
												num.oFactUses[0].Save();
											}
										}
										base.Close();
										return;
									}
									else
									{
										document.Delete();
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										return;
									}
								}
								else
								{
									document.Delete();
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
							}
							else
							{
								document.Delete();
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
					}
					else
					{
						MessageBox.Show("Не выбран слесарь!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(string.Concat("Ошибка сохранения данных! ", exception.Message), "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void cmdTypeGMeter_Click(object sender, EventArgs e)
		{
			TypeGMeters typeGMeter = this._typegmeters;
			string[] strArrays = new string[] { "Название", "Класс точности" };
			string[] strArrays1 = strArrays;
			int[] numArray = new int[] { 200, 200 };
			strArrays = new string[] { "Name", "ClassAccuracy" };
			Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort) };
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник приборов учета", typeGMeter, strArrays1, numArray, strArrays, typeArray);
			frmSimpleObj.ShowDialog(this);
			this._typegmeters = new TypeGMeters();
			this._typegmeters.Load();
			if (frmSimpleObj.lvData.SelectedItems.Count <= 0)
			{
				this.CreateTypeGMeter((long)0);
			}
			else
			{
				this.CreateTypeGMeter(Convert.ToInt64(frmSimpleObj.lvData.SelectedItems[0].Tag));
			}
			frmSimpleObj = null;
		}

		private void CreateTypeGMeter(long IdSelected)
		{
			try
			{
				this.cmbTypeGMeter.ClearItems();
				int num = 0;
				for (int i = 0; i < this._typegmeters.get_Count(); i++)
				{
					this.cmbTypeGMeter.AddItem(this._typegmeters[i].Fullname);
					if (this._typegmeters[i].get_ID() == IdSelected)
					{
						num = i;
					}
				}
				if (this.cmbTypeGMeter.ListCount > num)
				{
					this.cmbTypeGMeter.SelectedIndex = num;
				}
				this.cmbTypeGMeter.ColumnWidth = this.cmbTypeGMeter.Width - this.cmbTypeGMeter.Height;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.cmbTypeGMeter.Text = string.Concat("Ошибка загрузки справочника ", exception.Message);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void dtDate_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numLastIndication.Focus();
			}
		}

		private void dtDateFabrication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDateInstall.Focus();
			}
		}

		private void dtDateInstall_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.dtDateVerify.Focus();
			}
		}

		private void dtDateVerify_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numBeginIndication.Focus();
			}
		}

		public void FillC1WhithAll(Gmeters oObjects, C1Combo cmb, long IdSelected, string strAll)
		{
			try
			{
				cmb.ClearItems();
				cmb.AddItem(strAll);
				for (int i = 0; i < oObjects.get_Count(); i++)
				{
					if (oObjects[i].oStatusGMeter.get_ID() != (long)1)
					{
						cmb.AddItem(string.Concat(oObjects[i].oTypeGMeter.get_Name(), ", отключен"));
					}
					else
					{
						cmb.AddItem(string.Concat(oObjects[i].oTypeGMeter.get_Name(), ", подключен"));
					}
					oObjects[i].get_ID();
				}
				cmb.SelectedIndex = 0;
				cmb.ColumnWidth = cmb.Width - cmb.VScrollBar.Width;
			}
			catch (Exception exception)
			{
				cmb.Text = string.Concat("Ошибка загрузки справочника ", exception.Message);
			}
		}

		private void frmActionGMeter_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
			this._agents = null;
			this._gmeter = null;
			this._gmeters = null;
			this._typegmeters = null;
		}

		private void frmActionGMeter_Load(object sender, EventArgs e)
		{
			DateTime today;
			try
			{
				if (this._gmeter == null)
				{
					base.Close();
				}
				else if (this._gmeter.oGobject != null)
				{
					try
					{
						DateEditMonthCalendar calendar = this.dtDateInstall.Calendar;
						today = DateTime.Today;
						string str = Convert.ToString(today.Month);
						today = DateTime.Today;
						((MonthCalendar)calendar).MinDate = Convert.ToDateTime(string.Concat("01.", str, ".", Convert.ToString(today.Year)));
						DateEditMonthCalendar dateTime = this.dtDate.Calendar;
						today = DateTime.Today;
						string str1 = Convert.ToString(today.Month);
						today = DateTime.Today;
						((MonthCalendar)dateTime).MinDate = Convert.ToDateTime(string.Concat("01.", str1, ".", Convert.ToString(today.Year)));
					}
					catch
					{
					}
					C1DateEdit date = this.dtDateFabrication;
					today = DateTime.Today;
					date.Value = today.Date;
					C1DateEdit c1DateEdit = this.dtDateVerify;
					today = DateTime.Today;
					c1DateEdit.Value = today.Date;
					C1DateEdit date1 = this.dtDateInstall;
					today = DateTime.Today;
					date1.Value = today.Date;
					C1DateEdit c1DateEdit1 = this.dtDate;
					today = DateTime.Today;
					c1DateEdit1.Value = today.Date;
					Tools.LoadWindows(this);
					this.lblAccount.Text = this._gmeter.oGobject.oContract.Account;
					this.lblFIO.Text = this._gmeter.oGobject.oContract.oPerson.FullName;
					this.lblAddress.Text = this._gmeter.oGobject.oContract.oPerson.oAddress.get_ShortAddress();
					if (this._gmeter.oGobject.oStatusGObject.get_ID() != (long)1)
					{
						this.lblOU.Text = string.Concat(this._gmeter.oGobject.oTypeGobject.Name, ", отключен, ", this._gmeter.oGobject.oAddress.get_ShortAddress());
					}
					else
					{
						this.lblOU.Text = string.Concat(this._gmeter.oGobject.oTypeGobject.Name, ", подключен, ", this._gmeter.oGobject.oAddress.get_ShortAddress());
					}
					this.lblNameGRU.Text = this._gmeter.oGobject.oGRU.get_Name();
					this._agents = new Agents();
					this._agents.Load(Depot.oTypeAgents.item((long)5));
					Tools.FillC1(this._agents, this.cmbMechanic, (long)0);
					this._TypeReasonDisconnects = new TypeReasonDisconnects();
					this._TypeReasonDisconnects.Load();
					Tools.FillC1(this._TypeReasonDisconnects, this.CmbTypeReasonDisconect, (long)0);
					if (this._gmeter.get_ID() != (long)0)
					{
						if (this._gmeter.oStatusGMeter.get_ID() != (long)1)
						{
							this.cmbPU.Text = string.Concat(this._gmeter.oTypeGMeter.get_Name(), ", отключен");
							this.Text = "Установка прибора учета";
							this.label13.Text = "Дата установки";
							this.label14.Text = "Начальные показания";
							this.CmbTypeReasonDisconect.Visible = false;
							this.label15.Visible = false;
							this.numLastIndication.Value = Convert.ToDecimal(this._gmeter.GetCurrentIndication().Display);
						}
						else
						{
							this.cmbPU.Text = string.Concat(this._gmeter.oTypeGMeter.get_Name(), ", подключен");
						}
						this.cmbPU.Enabled = false;
					}
					else
					{
						this._gmeter.oStatusGMeter = Depot.oStatusGMeters.item((long)2);
						this._gmeters = new Gmeters();
						this._gmeters.Load(this._gmeter.oGobject, Depot.oStatusGMeters.item((long)2));
						this.FillC1WhithAll(this._gmeters, this.cmbPU, (long)0, "Новый ПУ");
						this.Text = "Установка прибора учета";
						this.label13.Visible = false;
						this.label14.Visible = false;
						this.dtDate.Visible = false;
						this.numLastIndication.Visible = false;
						this.CmbTypeReasonDisconect.Visible = false;
						this.label15.Visible = false;
					}
				}
				else
				{
					base.Close();
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmActionGMeter));
			this.groupBox2 = new GroupBox();
			this.lblAccount = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.imageList1 = new ImageList(this.components);
			this.label2 = new Label();
			this.lblNameGRU = new Label();
			this.label4 = new Label();
			this.groupBox1 = new GroupBox();
			this.lblOU = new Label();
			this.grNewPU = new GroupBox();
			this.cmdFindRepeat = new Button();
			this.numBeginIndication = new NumericUpDown();
			this.label18 = new Label();
			this.cmdTypeGMeter = new Button();
			this.dtDateFabrication = new C1DateEdit();
			this.label3 = new Label();
			this.dtDateVerify = new C1DateEdit();
			this.label1 = new Label();
			this.dtDateInstall = new C1DateEdit();
			this.label6 = new Label();
			this.txtSerialNumber = new TextBox();
			this.label5 = new Label();
			this.cmbTypeGMeter = new C1Combo();
			this.label7 = new Label();
			this.cmbPU = new C1Combo();
			this.label8 = new Label();
			this.groupBox4 = new GroupBox();
			this.numLastIndication = new NumericUpDown();
			this.label14 = new Label();
			this.dtDate = new C1DateEdit();
			this.cmbMechanic = new C1Combo();
			this.label9 = new Label();
			this.label13 = new Label();
			this.txtNote = new TextBox();
			this.label17 = new Label();
			this.cmdClose = new Button();
			this.cmdOK = new Button();
			this.CmbTypeReasonDisconect = new C1Combo();
			this.label15 = new Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.grNewPU.SuspendLayout();
			((ISupportInitialize)this.numBeginIndication).BeginInit();
			((ISupportInitialize)this.dtDateFabrication).BeginInit();
			((ISupportInitialize)this.dtDateVerify).BeginInit();
			((ISupportInitialize)this.dtDateInstall).BeginInit();
			((ISupportInitialize)this.cmbTypeGMeter).BeginInit();
			((ISupportInitialize)this.cmbPU).BeginInit();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.numLastIndication).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.cmbMechanic).BeginInit();
			((ISupportInitialize)this.CmbTypeReasonDisconect).BeginInit();
			base.SuspendLayout();
			this.groupBox2.Controls.Add(this.lblAccount);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 96);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.lblAccount.BackColor = SystemColors.Info;
			this.lblAccount.BorderStyle = BorderStyle.FixedSingle;
			this.lblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblAccount.ForeColor = SystemColors.ControlText;
			this.lblAccount.Location = new Point(48, 16);
			this.lblAccount.Name = "lblAccount";
			this.lblAccount.Size = new System.Drawing.Size(112, 20);
			this.lblAccount.TabIndex = 7;
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(48, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(224, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(48, 40);
			this.lblFIO.Name = "lblFIO";
			this.lblFIO.Size = new System.Drawing.Size(224, 20);
			this.lblFIO.TabIndex = 4;
			this.label10.ForeColor = SystemColors.ControlText;
			this.label10.Location = new Point(8, 64);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 2;
			this.label10.Text = "Адрес";
			this.label11.ForeColor = SystemColors.ControlText;
			this.label11.Location = new Point(8, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 1;
			this.label11.Text = "ФИО";
			this.label12.ForeColor = SystemColors.ControlText;
			this.label12.Location = new Point(8, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 16);
			this.label12.TabIndex = 0;
			this.label12.Text = "Л/с";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "ОУ";
			this.lblNameGRU.BackColor = SystemColors.Info;
			this.lblNameGRU.BorderStyle = BorderStyle.FixedSingle;
			this.lblNameGRU.ForeColor = SystemColors.ControlText;
			this.lblNameGRU.Location = new Point(40, 56);
			this.lblNameGRU.Name = "lblNameGRU";
			this.lblNameGRU.Size = new System.Drawing.Size(216, 32);
			this.lblNameGRU.TabIndex = 29;
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 28;
			this.label4.Text = "РУ";
			this.groupBox1.Controls.Add(this.lblNameGRU);
			this.groupBox1.Controls.Add(this.lblOU);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(288, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 96);
			this.groupBox1.TabIndex = 30;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Объект учета";
			this.lblOU.BackColor = SystemColors.Info;
			this.lblOU.BorderStyle = BorderStyle.FixedSingle;
			this.lblOU.ForeColor = SystemColors.ControlText;
			this.lblOU.Location = new Point(40, 16);
			this.lblOU.Name = "lblOU";
			this.lblOU.Size = new System.Drawing.Size(216, 32);
			this.lblOU.TabIndex = 31;
			this.grNewPU.Controls.Add(this.cmdFindRepeat);
			this.grNewPU.Controls.Add(this.numBeginIndication);
			this.grNewPU.Controls.Add(this.label18);
			this.grNewPU.Controls.Add(this.cmdTypeGMeter);
			this.grNewPU.Controls.Add(this.dtDateFabrication);
			this.grNewPU.Controls.Add(this.label3);
			this.grNewPU.Controls.Add(this.dtDateVerify);
			this.grNewPU.Controls.Add(this.label1);
			this.grNewPU.Controls.Add(this.dtDateInstall);
			this.grNewPU.Controls.Add(this.label6);
			this.grNewPU.Controls.Add(this.txtSerialNumber);
			this.grNewPU.Controls.Add(this.label5);
			this.grNewPU.Controls.Add(this.cmbTypeGMeter);
			this.grNewPU.Controls.Add(this.label7);
			this.grNewPU.Enabled = false;
			this.grNewPU.ForeColor = SystemColors.Desktop;
			this.grNewPU.Location = new Point(4, 136);
			this.grNewPU.Name = "grNewPU";
			this.grNewPU.Size = new System.Drawing.Size(548, 96);
			this.grNewPU.TabIndex = 2;
			this.grNewPU.TabStop = false;
			this.grNewPU.Text = "Новый ПУ";
			this.cmdFindRepeat.FlatStyle = FlatStyle.Flat;
			this.cmdFindRepeat.ForeColor = SystemColors.ControlText;
			this.cmdFindRepeat.ImageIndex = 0;
			this.cmdFindRepeat.ImageList = this.imageList1;
			this.cmdFindRepeat.Location = new Point(252, 16);
			this.cmdFindRepeat.Name = "cmdFindRepeat";
			this.cmdFindRepeat.Size = new System.Drawing.Size(20, 20);
			this.cmdFindRepeat.TabIndex = 2;
			this.cmdFindRepeat.Visible = false;
			this.cmdFindRepeat.Click += new EventHandler(this.cmdFindRepeat_Click);
			this.numBeginIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numBeginIndication.DecimalPlaces = 2;
			this.numBeginIndication.Location = new Point(424, 72);
			NumericUpDown num = this.numBeginIndication;
			int[] numArray = new int[] { 9999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numBeginIndication.Name = "numBeginIndication";
			this.numBeginIndication.Size = new System.Drawing.Size(112, 20);
			this.numBeginIndication.TabIndex = 8;
			this.numBeginIndication.KeyPress += new KeyPressEventHandler(this.numBeginIndication_KeyPress);
			this.numBeginIndication.Enter += new EventHandler(this.numBeginIndication_Enter);
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(292, 72);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(128, 16);
			this.label18.TabIndex = 49;
			this.label18.Text = "Начальные показания";
			this.cmdTypeGMeter.FlatStyle = FlatStyle.Flat;
			this.cmdTypeGMeter.ForeColor = SystemColors.ControlText;
			this.cmdTypeGMeter.ImageIndex = 0;
			this.cmdTypeGMeter.ImageList = this.imageList1;
			this.cmdTypeGMeter.Location = new Point(516, 16);
			this.cmdTypeGMeter.Name = "cmdTypeGMeter";
			this.cmdTypeGMeter.Size = new System.Drawing.Size(20, 20);
			this.cmdTypeGMeter.TabIndex = 4;
			this.cmdTypeGMeter.Click += new EventHandler(this.cmdTypeGMeter_Click);
			this.dtDateFabrication.BorderStyle = 1;
			this.dtDateFabrication.FormatType = FormatTypeEnum.LongDate;
			this.dtDateFabrication.Location = new Point(128, 48);
			this.dtDateFabrication.Name = "dtDateFabrication";
			this.dtDateFabrication.Size = new System.Drawing.Size(144, 18);
			this.dtDateFabrication.TabIndex = 5;
			this.dtDateFabrication.Tag = null;
			this.dtDateFabrication.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDateFabrication.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDateFabrication.KeyPress += new KeyPressEventHandler(this.dtDateFabrication_KeyPress);
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 43;
			this.label3.Text = "Дата установки";
			this.dtDateVerify.BorderStyle = 1;
			this.dtDateVerify.FormatType = FormatTypeEnum.LongDate;
			this.dtDateVerify.Location = new Point(392, 48);
			this.dtDateVerify.Name = "dtDateVerify";
			this.dtDateVerify.Size = new System.Drawing.Size(144, 18);
			this.dtDateVerify.TabIndex = 7;
			this.dtDateVerify.Tag = null;
			this.dtDateVerify.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDateVerify.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDateVerify.KeyPress += new KeyPressEventHandler(this.dtDateVerify_KeyPress);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(292, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 41;
			this.label1.Text = "Дата проверки";
			this.dtDateInstall.BorderStyle = 1;
			this.dtDateInstall.FormatType = FormatTypeEnum.LongDate;
			this.dtDateInstall.Location = new Point(128, 72);
			this.dtDateInstall.Name = "dtDateInstall";
			this.dtDateInstall.Size = new System.Drawing.Size(144, 18);
			this.dtDateInstall.TabIndex = 6;
			this.dtDateInstall.Tag = null;
			this.dtDateInstall.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDateInstall.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDateInstall.KeyPress += new KeyPressEventHandler(this.dtDateInstall_KeyPress);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 16);
			this.label6.TabIndex = 39;
			this.label6.Text = "Дата выпуска";
			this.txtSerialNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtSerialNumber.Location = new Point(72, 16);
			this.txtSerialNumber.Name = "txtSerialNumber";
			this.txtSerialNumber.Size = new System.Drawing.Size(176, 20);
			this.txtSerialNumber.TabIndex = 1;
			this.txtSerialNumber.Text = "";
			this.txtSerialNumber.KeyPress += new KeyPressEventHandler(this.txtSerialNumber_KeyPress);
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 38;
			this.label5.Text = "Номер";
			this.cmbTypeGMeter.AddItemSeparator = ';';
			this.cmbTypeGMeter.BorderStyle = 1;
			this.cmbTypeGMeter.Caption = "";
			this.cmbTypeGMeter.CaptionHeight = 17;
			this.cmbTypeGMeter.CharacterCasing = 0;
			this.cmbTypeGMeter.ColumnCaptionHeight = 17;
			this.cmbTypeGMeter.ColumnFooterHeight = 17;
			this.cmbTypeGMeter.ColumnHeaders = false;
			this.cmbTypeGMeter.ColumnWidth = 100;
			this.cmbTypeGMeter.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeGMeter.ContentHeight = 15;
			this.cmbTypeGMeter.DataMode = DataModeEnum.AddItem;
			this.cmbTypeGMeter.DeadAreaBackColor = Color.Empty;
			this.cmbTypeGMeter.EditorBackColor = SystemColors.Window;
			this.cmbTypeGMeter.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeGMeter.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeGMeter.EditorHeight = 15;
			this.cmbTypeGMeter.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeGMeter.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbTypeGMeter.ItemHeight = 15;
			this.cmbTypeGMeter.Location = new Point(328, 16);
			this.cmbTypeGMeter.MatchEntryTimeout = (long)2000;
			this.cmbTypeGMeter.MaxDropDownItems = 5;
			this.cmbTypeGMeter.MaxLength = 32767;
			this.cmbTypeGMeter.MouseCursor = Cursors.Default;
			this.cmbTypeGMeter.Name = "cmbTypeGMeter";
			this.cmbTypeGMeter.RowDivider.Color = Color.DarkGray;
			this.cmbTypeGMeter.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeGMeter.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeGMeter.Size = new System.Drawing.Size(184, 19);
			this.cmbTypeGMeter.TabIndex = 3;
			this.cmbTypeGMeter.KeyPress += new KeyPressEventHandler(this.cmbTypeGMeter_KeyPress);
			this.cmbTypeGMeter.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(292, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 16);
			this.label7.TabIndex = 36;
			this.label7.Text = "Тип";
			this.cmbPU.AddItemSeparator = ';';
			this.cmbPU.BorderStyle = 1;
			this.cmbPU.Caption = "";
			this.cmbPU.CaptionHeight = 17;
			this.cmbPU.CharacterCasing = 0;
			this.cmbPU.ColumnCaptionHeight = 17;
			this.cmbPU.ColumnFooterHeight = 17;
			this.cmbPU.ColumnHeaders = false;
			this.cmbPU.ColumnWidth = 100;
			this.cmbPU.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbPU.ContentHeight = 15;
			this.cmbPU.DataMode = DataModeEnum.AddItem;
			this.cmbPU.DeadAreaBackColor = Color.Empty;
			this.cmbPU.EditorBackColor = SystemColors.Window;
			this.cmbPU.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbPU.EditorForeColor = SystemColors.WindowText;
			this.cmbPU.EditorHeight = 15;
			this.cmbPU.FlatStyle = FlatModeEnum.Flat;
			this.cmbPU.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbPU.ItemHeight = 15;
			this.cmbPU.Location = new Point(52, 112);
			this.cmbPU.MatchEntryTimeout = (long)2000;
			this.cmbPU.MaxDropDownItems = 5;
			this.cmbPU.MaxLength = 32767;
			this.cmbPU.MouseCursor = Cursors.Default;
			this.cmbPU.Name = "cmbPU";
			this.cmbPU.RowDivider.Color = Color.DarkGray;
			this.cmbPU.RowDivider.Style = LineStyleEnum.None;
			this.cmbPU.RowSubDividerColor = Color.DarkGray;
			this.cmbPU.Size = new System.Drawing.Size(500, 19);
			this.cmbPU.TabIndex = 1;
			this.cmbPU.TextChanged += new EventHandler(this.cmbPU_TextChanged);
			this.cmbPU.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(12, 112);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(32, 16);
			this.label8.TabIndex = 64;
			this.label8.Text = "ПУ";
			this.groupBox4.Controls.Add(this.CmbTypeReasonDisconect);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.numLastIndication);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.dtDate);
			this.groupBox4.Controls.Add(this.cmbMechanic);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.ForeColor = SystemColors.Desktop;
			this.groupBox4.Location = new Point(4, 232);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(548, 64);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Выполненная работа";
			this.numLastIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numLastIndication.DecimalPlaces = 2;
			this.numLastIndication.Location = new Point(424, 16);
			NumericUpDown numericUpDown = this.numLastIndication;
			numArray = new int[] { 9999, 0, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.numLastIndication.Name = "numLastIndication";
			this.numLastIndication.Size = new System.Drawing.Size(112, 20);
			this.numLastIndication.TabIndex = 3;
			this.numLastIndication.KeyPress += new KeyPressEventHandler(this.numLastIndication_KeyPress);
			this.numLastIndication.Enter += new EventHandler(this.numLastIndication_Enter);
			this.label14.ForeColor = SystemColors.ControlText;
			this.label14.Location = new Point(292, 16);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(128, 16);
			this.label14.TabIndex = 51;
			this.label14.Text = "Конечные показания";
			this.dtDate.BorderStyle = 1;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(136, 40);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(136, 18);
			this.dtDate.TabIndex = 2;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.dtDate.KeyPress += new KeyPressEventHandler(this.dtDate_KeyPress);
			this.cmbMechanic.AddItemSeparator = ';';
			this.cmbMechanic.BorderStyle = 1;
			this.cmbMechanic.Caption = "";
			this.cmbMechanic.CaptionHeight = 17;
			this.cmbMechanic.CharacterCasing = 0;
			this.cmbMechanic.ColumnCaptionHeight = 17;
			this.cmbMechanic.ColumnFooterHeight = 17;
			this.cmbMechanic.ColumnHeaders = false;
			this.cmbMechanic.ColumnWidth = 100;
			this.cmbMechanic.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbMechanic.ContentHeight = 15;
			this.cmbMechanic.DataMode = DataModeEnum.AddItem;
			this.cmbMechanic.DeadAreaBackColor = Color.Empty;
			this.cmbMechanic.EditorBackColor = SystemColors.Window;
			this.cmbMechanic.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbMechanic.EditorForeColor = SystemColors.WindowText;
			this.cmbMechanic.EditorHeight = 15;
			this.cmbMechanic.FlatStyle = FlatModeEnum.Flat;
			this.cmbMechanic.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmbMechanic.ItemHeight = 15;
			this.cmbMechanic.Location = new Point(80, 16);
			this.cmbMechanic.MatchEntryTimeout = (long)2000;
			this.cmbMechanic.MaxDropDownItems = 5;
			this.cmbMechanic.MaxLength = 32767;
			this.cmbMechanic.MouseCursor = Cursors.Default;
			this.cmbMechanic.Name = "cmbMechanic";
			this.cmbMechanic.RowDivider.Color = Color.DarkGray;
			this.cmbMechanic.RowDivider.Style = LineStyleEnum.None;
			this.cmbMechanic.RowSubDividerColor = Color.DarkGray;
			this.cmbMechanic.Size = new System.Drawing.Size(192, 19);
			this.cmbMechanic.TabIndex = 1;
			this.cmbMechanic.KeyPress += new KeyPressEventHandler(this.cmbMechanic_KeyPress);
			this.cmbMechanic.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label9.ForeColor = SystemColors.ControlText;
			this.label9.Location = new Point(8, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 16);
			this.label9.TabIndex = 1;
			this.label9.Text = "Выполнил";
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 40);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(112, 16);
			this.label13.TabIndex = 0;
			this.label13.Text = "Дата снятия";
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(88, 304);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(464, 32);
			this.txtNote.TabIndex = 4;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 304);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 70;
			this.label17.Text = "Примечание";
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(456, 344);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 6;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(352, 344);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(92, 24);
			this.cmdOK.TabIndex = 5;
			this.cmdOK.Text = "ОК";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.CmbTypeReasonDisconect.AddItemSeparator = ';';
			this.CmbTypeReasonDisconect.BorderStyle = 1;
			this.CmbTypeReasonDisconect.Caption = "";
			this.CmbTypeReasonDisconect.CaptionHeight = 17;
			this.CmbTypeReasonDisconect.CharacterCasing = 0;
			this.CmbTypeReasonDisconect.ColumnCaptionHeight = 17;
			this.CmbTypeReasonDisconect.ColumnFooterHeight = 17;
			this.CmbTypeReasonDisconect.ColumnHeaders = false;
			this.CmbTypeReasonDisconect.ColumnWidth = 100;
			this.CmbTypeReasonDisconect.ComboStyle = ComboStyleEnum.DropdownList;
			this.CmbTypeReasonDisconect.ContentHeight = 15;
			this.CmbTypeReasonDisconect.DataMode = DataModeEnum.AddItem;
			this.CmbTypeReasonDisconect.DeadAreaBackColor = Color.Empty;
			this.CmbTypeReasonDisconect.EditorBackColor = SystemColors.Window;
			this.CmbTypeReasonDisconect.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.CmbTypeReasonDisconect.EditorForeColor = SystemColors.WindowText;
			this.CmbTypeReasonDisconect.EditorHeight = 15;
			this.CmbTypeReasonDisconect.FlatStyle = FlatModeEnum.Flat;
			this.CmbTypeReasonDisconect.Images.Add((Image)resourceManager.GetObject("resource3"));
			this.CmbTypeReasonDisconect.ItemHeight = 15;
			this.CmbTypeReasonDisconect.Location = new Point(352, 40);
			this.CmbTypeReasonDisconect.MatchEntryTimeout = (long)2000;
			this.CmbTypeReasonDisconect.MaxDropDownItems = 5;
			this.CmbTypeReasonDisconect.MaxLength = 32767;
			this.CmbTypeReasonDisconect.MouseCursor = Cursors.Default;
			this.CmbTypeReasonDisconect.Name = "CmbTypeReasonDisconect";
			this.CmbTypeReasonDisconect.RowDivider.Color = Color.DarkGray;
			this.CmbTypeReasonDisconect.RowDivider.Style = LineStyleEnum.None;
			this.CmbTypeReasonDisconect.RowSubDividerColor = Color.DarkGray;
			this.CmbTypeReasonDisconect.Size = new System.Drawing.Size(192, 19);
			this.CmbTypeReasonDisconect.TabIndex = 53;
			this.CmbTypeReasonDisconect.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.label15.ForeColor = SystemColors.ControlText;
			this.label15.Location = new Point(280, 40);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(64, 16);
			this.label15.TabIndex = 52;
			this.label15.Text = "Причина";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(554, 375);
			base.Controls.Add(this.txtNote);
			base.Controls.Add(this.label17);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.cmbPU);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.grNewPU);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "frmActionGMeter";
			this.Text = "Снятие прибора учета";
			base.Closing += new CancelEventHandler(this.frmActionGMeter_Closing);
			base.Load += new EventHandler(this.frmActionGMeter_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.grNewPU.ResumeLayout(false);
			((ISupportInitialize)this.numBeginIndication).EndInit();
			((ISupportInitialize)this.dtDateFabrication).EndInit();
			((ISupportInitialize)this.dtDateVerify).EndInit();
			((ISupportInitialize)this.dtDateInstall).EndInit();
			((ISupportInitialize)this.cmbTypeGMeter).EndInit();
			((ISupportInitialize)this.cmbPU).EndInit();
			this.groupBox4.ResumeLayout(false);
			((ISupportInitialize)this.numLastIndication).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.cmbMechanic).EndInit();
			((ISupportInitialize)this.CmbTypeReasonDisconect).EndInit();
			base.ResumeLayout(false);
		}

		private void numBeginIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numBeginIndication);
		}

		private void numBeginIndication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbMechanic.Focus();
			}
		}

		private void numLastIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numLastIndication);
		}

		private void numLastIndication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote.Focus();
			}
		}

		private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdOK.Focus();
			}
		}

		private void txtSerialNumber_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbTypeGMeter.Focus();
			}
		}
	}
}