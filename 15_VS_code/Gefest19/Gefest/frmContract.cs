using C1.Win.C1Command;
using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmContract : Form
	{
		private Contract _contract;

		private TypeTariffs _typetariffs;

		private TextBox txtAccount;

		private Label label1;

		private Label label2;

		private C1Combo cmbStatus;

		private C1Combo cmbType;

		private Label label3;

		private GroupBox groupBox1;

		private Label label4;

		private Label label5;

		private TextBox txtName;

		private TextBox txtAddress;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private Label lblSaldo;

		private Label lblDisplay;

		private Button cmdOk;

		private Button cmdCancel;

		private TypeContracts _typecontracts;

		private TypeInfringementss _TypeInfringementss;

		private C1Combo cmbGObject;

		private Label label6;

		private TextBox txtGRU;

		private Label label7;

		private TextBox txtCountLives;

		private GroupBox groupBox2;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ImageList imageList1;

		private ListView lvDisplay;

		private GroupBox groupBox3;

		private ListView lvFactUse;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ColumnHeader columnHeader10;

		private ColumnHeader columnHeader11;

		private C1Combo cmbGMeter;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader13;

		private ColumnHeader columnHeader14;

		private ColumnHeader columnHeader15;

		private ColumnHeader columnHeader16;

		private ListView lvDoc;

		private C1DateEdit dtFrom;

		private C1DateEdit dtTo;

		private Label label9;

		private CheckBox chkDateFrom;

		private Label label8;

		private Label label10;

		private C1Combo cmbTypeTariff;

		private Label label11;

		private GroupBox groupBox4;

		private ListView lvParam;

		private ColumnHeader columnHeader17;

		private ColumnHeader columnHeader18;

		private ColumnHeader columnHeader19;

		private TextBox txtMemo;

		private Button cmdApply;

		private Button cmdGObject;

		private Button cmdGMeter;

		private Button cmdChangeCL;

		private Button cmdStatusGO;

		private Label lblStatusGO;

		private Button cmdGMeterAdd;

		private Button cmdGObjectAdd;

		private Button cmdGObjectDel;

		private Button cmdGMeterDel;

		private Panel panel1;

		private Panel panel2;

		private ColumnHeader columnHeader20;

		private CheckBox ckshet;

		private C1Combo cmbTypeInfringements;

		private Label label12;

		private ToolBar tbData;

		private ToolBarButton toolBarButton7;

		private ToolBarButton toolBarButton8;

		private ToolBarButton toolBarButton9;

		private ToolBarButton toolBarButton10;

		private ToolBarButton toolBarButton11;

		private ToolBar tbParam;

		private ToolBarButton toolBarButton1;

		private ToolBarButton toolBarButton2;

		private ToolBarButton toolBarButton3;

		private ToolBarButton toolBarButton4;

		private ToolBarButton toolBarButton5;

		private IContainer components;

		private ToolBar toolBar1;

		private ToolBarButton toolBarButton6;

		private ToolBarButton toolBarButton12;

		private ToolBarButton toolBarButton13;

		private ToolBarButton toolBarButton14;

		private ToolBarButton toolBarButton15;

		private C1CommandHolder c1CommandHolder1;

		private CheckBox chkPeny;

		private ToolTip TT;

		private DateTime DateBegin;

		private DateTime DateBeginI;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton17;

		private DateTime DateEnd;

		private DateTime DateEndI;

		private ToolBarButton toolBarButton18;

		private ToolBarButton toolBarButton19;

		private Documents _Docs;

		private Indications _Inds;

		private bool isload;

		public frmContract(Contract oContract)
		{
			this._contract = oContract;
			this.InitializeComponent();
		}

		private void c1Command2_Click(object sender, ClickEventArgs e)
		{
		}

		private void c1Command3_Click(object sender, ClickEventArgs e)
		{
		}

		private void c1Command4_Click(object sender, ClickEventArgs e)
		{
		}

		private void c1CommandMenu1_Click(object sender, ClickEventArgs e)
		{
		}

		private void chkDateFrom_CheckedChanged(object sender, EventArgs e)
		{
			this.dtFrom.Enabled = this.chkDateFrom.Checked;
			this.dtTo.Enabled = this.chkDateFrom.Checked;
		}

		private void ckshet_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isload && this.ckshet.Checked)
			{
				MessageBox.Show("Вы поставили отметку <НЕ ПЕЧАТАТЬ СЧЕТ>", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void cmbGMeter_TextChanged(object sender, EventArgs e)
		{
			this.CreateIndication();
		}

		private void cmbGObject_TextChanged(object sender, EventArgs e)
		{
			if (this.cmbGObject.SelectedIndex >= 0)
			{
				Gobject item = this._contract.oGobjects[this.cmbGObject.SelectedIndex];
				GRU gRU = this._contract.oGobjects[this.cmbGObject.SelectedIndex].oGRU;
				TextBox textBox = this.txtGRU;
				string[] invNumber = new string[] { gRU.InvNumber, ", ", gRU.oAddress.get_oStreet().get_Name(), " ", gRU.oAddress.get_oHouse().get_Name(), ", ", gRU.oAgent.get_Name() };
				textBox.Text = string.Concat(invNumber);
				this.txtCountLives.Text = item.CountLives.ToString();
				this.lblStatusGO.Text = item.oStatusGObject.get_Name();
				if (item.oStatusGObject.get_ID() != (long)1)
				{
					this.cmdStatusGO.Text = "Подключение ОУ";
				}
				else
				{
					this.cmdStatusGO.Text = "Отключение ОУ";
				}
				if (item.oActiveGmeter != null && item.oActiveGmeter.oIndications.get_Count() > 0)
				{
					double display = item.oActiveGmeter.oIndications[0].Display;
					DateTime datedisplay = item.oActiveGmeter.oIndications[0].Datedisplay;
					this.lblDisplay.Text = string.Concat("Показания: ", display.ToString(), " от ", datedisplay.ToShortDateString());
					DateTime today = DateTime.Today;
					int year = today.Year - datedisplay.Year;
					today = DateTime.Today;
					int month = today.Month - datedisplay.Month;
					month = month + year * 12;
					this.lblDisplay.ForeColor = Color.Black;
					if (month > 0)
					{
						this.lblDisplay.ForeColor = Color.Green;
						if (month > 1)
						{
							this.lblDisplay.ForeColor = Color.Blue;
							if (month > 2)
							{
								this.lblDisplay.ForeColor = Color.Yellow;
								if (month > 3)
								{
									this.lblDisplay.ForeColor = Color.Red;
								}
							}
						}
					}
				}
			}
			this.CreateGMeter();
			this.CreateFactUse();
		}

		private void cmdApply_Click(object sender, EventArgs e)
		{
			if (this.Save())
			{
				this.txtAccount.ReadOnly = true;
				this.txtAccount.BackColor = SystemColors.Info;
				this.tabControl1.SelectedIndex = 0;
			}
		}

		private void cmdChangeCL_Click(object sender, EventArgs e)
		{
			try
			{
				frmChangeCountLives frmChangeCountLife = new frmChangeCountLives(this._contract.oGobjects[this.cmbGObject.SelectedIndex]);
				frmChangeCountLife.ShowDialog(this);
				frmChangeCountLife = null;
				TextBox str = this.txtCountLives;
				int countLives = this._contract.oGobjects[this.cmbGObject.SelectedIndex].CountLives;
				str.Text = countLives.ToString();
			}
			catch
			{
			}
		}

		private void cmdGMeter_Click(object sender, EventArgs e)
		{
			try
			{
				Gobject item = this._contract.oGobjects[this.cmbGObject.SelectedIndex];
				if (item != null)
				{
					frmGMeter _frmGMeter = new frmGMeter(item.oGmeters[this.cmbGMeter.SelectedIndex]);
					_frmGMeter.ShowDialog(this);
					if (_frmGMeter.DialogResult == System.Windows.Forms.DialogResult.OK)
					{
						this.CreateGMeter();
					}
					_frmGMeter = null;
					item = null;
				}
			}
			catch
			{
			}
		}

		private void cmdGMeterAdd_Click(object sender, EventArgs e)
		{
			try
			{
				Gobject item = this._contract.oGobjects[this.cmbGObject.SelectedIndex];
				if (item != null)
				{
					if (item.oActiveGmeter == null)
					{
						frmActionGMeter _frmActionGMeter = new frmActionGMeter(item.oGmeters.Add());
						_frmActionGMeter.ShowDialog(this);
						_frmActionGMeter = null;
						this.CreateGMeter();
					}
					else
					{
						MessageBox.Show("Уже есть подключенный прибор учета!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
			}
			catch
			{
			}
		}

		private void cmdGMeterDel_Click(object sender, EventArgs e)
		{
			try
			{
				Gobject item = this._contract.oGobjects[this.cmbGObject.SelectedIndex];
				if (item != null && MessageBox.Show("Вы действительно хотите удалить прибор учета?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					item.oGmeters.Remove(item.oGmeters[this.cmbGMeter.SelectedIndex].get_ID());
					this.CreateGMeter();
				}
			}
			catch
			{
			}
		}

		private void cmdGObject_Click(object sender, EventArgs e)
		{
			try
			{
				frmGObject _frmGObject = new frmGObject(this._contract.oGobjects[this.cmbGObject.SelectedIndex]);
				_frmGObject.ShowDialog(this);
				_frmGObject = null;
				this.CreateGObject();
			}
			catch
			{
			}
		}

		private void cmdGObjectAdd_Click(object sender, EventArgs e)
		{
			try
			{
				frmActionGObject _frmActionGObject = new frmActionGObject(this._contract.oGobjects.Add());
				_frmActionGObject.ShowDialog(this);
				_frmActionGObject = null;
				this.CreateGObject();
			}
			catch
			{
			}
		}

		private void cmdGObjectDel_Click(object sender, EventArgs e)
		{
			try
			{
				if (MessageBox.Show("Вы действительно хотите удалить объект учета?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					this._contract.oGobjects.Remove(this._contract.oGobjects[this.cmbGObject.SelectedIndex].get_ID());
					this.CreateGObject();
				}
			}
			catch
			{
			}
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			if (this.Save())
			{
				base.Close();
			}
		}

		private void cmdStatusGO_Click(object sender, EventArgs e)
		{
			try
			{
				frmActionGObject _frmActionGObject = new frmActionGObject(this._contract.oGobjects[this.cmbGObject.SelectedIndex]);
				_frmActionGObject.ShowDialog(this);
				_frmActionGObject = null;
				this.CreateGObject();
			}
			catch
			{
			}
		}

		private void CreateDocuments()
		{
			ListViewItem d;
			this.lvDoc.Items.Clear();
			foreach (Document oDocument in this._contract.oDocuments)
			{
				ListView.ListViewItemCollection items = this.lvDoc.Items;
				DateTime documentDate = oDocument.DocumentDate;
				d = items.Add(documentDate.ToShortDateString());
				d.Tag = oDocument.get_ID();
				d.SubItems.Add(oDocument.oTypeDocument.get_Name());
				ListViewItem.ListViewSubItemCollection subItems = d.SubItems;
				double documentAmount = oDocument.DocumentAmount;
				subItems.Add(documentAmount.ToString("#.##"));
				try
				{
					long num = oDocument.oTypeDocument.get_ID();
					if (num <= (long)19 && num >= (long)1)
					{
						switch ((int)(num - (long)1))
						{
							case 0:
							{
								ListViewItem.ListViewSubItemCollection listViewSubItemCollections = d.SubItems;
								string[] numberBatch = new string[] { "№", oDocument.oBatch.NumberBatch, " от ", null, null, null };
								documentDate = oDocument.oBatch.BatchDate;
								numberBatch[3] = documentDate.ToShortDateString();
								numberBatch[4] = " на сумму ";
								documentAmount = oDocument.oBatch.BatchAmount;
								numberBatch[5] = documentAmount.ToString();
								listViewSubItemCollections.Add(string.Concat(numberBatch));
								break;
							}
							case 1:
							{
								string str = "";
								str = string.Concat("Стар. знач.: ", oDocument.GetPD(5).get_Name());
								if (oDocument.GetPD(6) != null)
								{
									documentDate = Convert.ToDateTime(oDocument.GetPD(6).get_Name());
									str = string.Concat(str, " по: ", documentDate.ToString("MM.yyyy"));
								}
								d.SubItems.Add(str);
								break;
							}
							case 2:
							{
								Document document = new Document();
								document.Load(Convert.ToInt64(oDocument.GetPD(25)));
								d.SubItems.Add(string.Concat("На л/с ", document.oContract.Account));
								document = null;
								break;
							}
							case 3:
							{
								d.SubItems.Add("");
								break;
							}
							case 4:
							{
								d.SubItems.Add("");
								break;
							}
							case 5:
							{
								d.SubItems.Add("");
								break;
							}
							case 6:
							{
								d.SubItems.Add("");
								break;
							}
							case 7:
							{
								d.SubItems.Add("");
								break;
							}
							case 8:
							{
								d.SubItems.Add("");
								break;
							}
							case 9:
							{
								d.SubItems.Add(this.GetStatusClaimDoc(oDocument));
								break;
							}
							case 10:
							{
								d.SubItems.Add("");
								break;
							}
							case 11:
							{
								d.SubItems.Add("");
								break;
							}
							case 12:
							{
								string str1 = "";
								if (oDocument.GetPD(9) == null)
								{
									str1 = "Начало работы";
								}
								if (oDocument.GetPD(10) == null)
								{
									str1 = "Передача дела в суд";
								}
								else
								{
									documentDate = oDocument.DateModify;
									str1 = string.Concat("Решение суда от:", documentDate.ToShortDateString());
								}
								d.SubItems.Add(str1);
								break;
							}
							case 13:
							{
								d.SubItems.Add("");
								break;
							}
							case 14:
							{
								d.SubItems.Add("");
								break;
							}
							case 15:
							{
								d.SubItems.Add("");
								break;
							}
							case 16:
							{
								d.SubItems.Add("");
								break;
							}
							case 17:
							{
								d.SubItems.Add("");
								break;
							}
							case 18:
							{
								d.SubItems.Add("");
								break;
							}
							default:
							{
								goto Label0;
							}
						}
					}
					else
					{
						goto Label0;
					}
				}
				catch
				{
					d.SubItems.Add("");
				}
			Label1:
				d.SubItems.Add(oDocument.Note);
				if (oDocument.oUserAdd == null)
				{
					ListViewItem.ListViewSubItemCollection subItems1 = d.SubItems;
					documentDate = oDocument.DateAdd;
					subItems1.Add(string.Concat("Неизвестный ", documentDate.ToShortDateString()));
				}
				else
				{
					ListViewItem.ListViewSubItemCollection listViewSubItemCollections1 = d.SubItems;
					string name = oDocument.oUserAdd.get_Name();
					documentDate = oDocument.DateAdd;
					listViewSubItemCollections1.Add(string.Concat(name, " ", documentDate.ToShortDateString()));
				}
			}
			return;
		Label0:
			d.SubItems.Add("");
			goto Label1;
		}

		private void CreateFactUse()
		{
			string[] name;
			DateTime documentDate;
			this.lvFactUse.Items.Clear();
			if (this.cmbGObject.SelectedIndex >= 0)
			{
				Gobject item = this._contract.oGobjects[this.cmbGObject.SelectedIndex];
				FactUses factUse = new FactUses();
				Periods period = new Periods();
				period.Load(this.DateBeginI);
				Period period1 = new Period();
				period1 = period[0];
				period = new Periods();
				period.Load(this.DateEndI);
				Period item1 = new Period();
				item1 = period[0] ?? Depot.CurrentPeriod;
				factUse.Load(item, period1.get_ID(), item1.get_ID());
				foreach (FactUse factUse1 in factUse)
				{
					ListViewItem listViewItem = this.lvFactUse.Items.Add(factUse1.oPeriod.get_Name());
					ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
					double factAmount = factUse1.FactAmount;
					subItems.Add(factAmount.ToString());
					if (factUse1.oPeriod.get_ID() <= (long)62 || factUse1.oTypeFU.get_ID() == (long)1)
					{
						listViewItem.SubItems.Add(factUse1.oTypeFU.Unit);
					}
					else
					{
						listViewItem.SubItems.Add("м3");
					}
					listViewItem.SubItems.Add(factUse1.oTypeFU.get_Name());
					if (factUse1.oDocument == null)
					{
						listViewItem.SubItems.Add("");
					}
					else
					{
						ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
						name = new string[] { factUse1.oDocument.oTypeDocument.get_Name(), " ", factUse1.oDocument.DocumentNumber, " от ", null };
						documentDate = factUse1.oDocument.DocumentDate;
						name[4] = documentDate.ToShortDateString();
						listViewSubItemCollections.Add(string.Concat(name));
					}
					if (factUse1.oOperation == null)
					{
						continue;
					}
					ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
					name = new string[5];
					factAmount = factUse1.oOperation.AmountOperation;
					name[0] = factAmount.ToString("#,##");
					name[1] = " ";
					name[2] = factUse1.oOperation.oDocument.oTypeDocument.get_Name();
					name[3] = " от ";
					documentDate = factUse1.oOperation.oDocument.DocumentDate;
					name[4] = documentDate.ToShortDateString();
					subItems1.Add(string.Concat(name));
				}
			}
		}

		private void CreateFactUse(FactUses _fus)
		{
			string[] name;
			DateTime documentDate;
			foreach (FactUse _fu in _fus)
			{
				ListViewItem listViewItem = this.lvFactUse.Items.Add(_fu.oPeriod.get_Name());
				ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
				double factAmount = _fu.FactAmount;
				subItems.Add(factAmount.ToString());
				if (_fu.oPeriod.get_ID() <= (long)62 || _fu.oTypeFU.get_ID() == (long)1)
				{
					listViewItem.SubItems.Add(_fu.oTypeFU.Unit);
				}
				else
				{
					listViewItem.SubItems.Add("м3");
				}
				listViewItem.SubItems.Add(_fu.oTypeFU.get_Name());
				if (_fu.oDocument == null)
				{
					listViewItem.SubItems.Add("");
				}
				else
				{
					ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
					name = new string[] { _fu.oDocument.oTypeDocument.get_Name(), " ", _fu.oDocument.DocumentNumber, " от ", null };
					documentDate = _fu.oDocument.DocumentDate;
					name[4] = documentDate.ToShortDateString();
					listViewSubItemCollections.Add(string.Concat(name));
				}
				if (_fu.oOperation == null)
				{
					continue;
				}
				ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
				name = new string[5];
				factAmount = _fu.oOperation.AmountOperation;
				name[0] = factAmount.ToString("#,##");
				name[1] = " ";
				name[2] = _fu.oOperation.oDocument.oTypeDocument.get_Name();
				name[3] = " от ";
				documentDate = _fu.oOperation.oDocument.DocumentDate;
				name[4] = documentDate.ToShortDateString();
				subItems1.Add(string.Concat(name));
			}
		}

		private void CreateGMeter()
		{
			Gmeters item = this._contract.oGobjects[this.cmbGObject.SelectedIndex].oGmeters;
			this.cmbGMeter.ClearItems();
			foreach (Gmeter gmeter in item)
			{
				if (gmeter.oTypeGMeter != null)
				{
					C1Combo c1Combo = this.cmbGMeter;
					string[] serialNumber = new string[] { gmeter.SerialNumber, ", ", gmeter.oStatusGMeter.get_Name(), ", ", gmeter.oTypeGMeter.Fullname };
					c1Combo.AddItem(string.Concat(serialNumber));
				}
				else
				{
					this.cmbGMeter.AddItem(string.Concat(gmeter.SerialNumber, ", ", gmeter.oStatusGMeter.get_Name()));
				}
			}
			if (this.cmbGMeter.ListCount > 0)
			{
				this.cmbGMeter.SelectedIndex = 0;
			}
			this.cmbGMeter.ColumnWidth = this.cmbGMeter.Width - this.cmbGMeter.VScrollBar.Width;
		}

		private void CreateGObject()
		{
			this.cmbGObject.ClearItems();
			foreach (Gobject oGobject in this._contract.oGobjects)
			{
				C1Combo c1Combo = this.cmbGObject;
				string[] name = new string[] { oGobject.Name, ", ", oGobject.oAddress.get_ShortAddress(), ", ", oGobject.oStatusGObject.get_Name() };
				c1Combo.AddItem(string.Concat(name));
			}
			if (this.cmbGObject.ListCount > 0)
			{
				this.cmbGObject.SelectedIndex = 0;
			}
			this.cmbGObject.ColumnWidth = this.cmbGObject.Width - this.cmbGObject.Height;
		}

		private void CreateIndication()
		{
			this.lvDisplay.Items.Clear();
			if (this.cmbGMeter.SelectedIndex >= 0)
			{
				Gmeter item = this._contract.oGobjects[this.cmbGObject.SelectedIndex].oGmeters[this.cmbGMeter.SelectedIndex];
				this.lvDisplay.Items.Clear();
				this._Inds = item.oIndicationss(this.DateBeginI, this.DateEndI);
				foreach (Indication oIndication in item.oIndications)
				{
					ListView.ListViewItemCollection items = this.lvDisplay.Items;
					DateTime datedisplay = oIndication.Datedisplay;
					ListViewItem d = items.Add(datedisplay.ToShortDateString());
					d.Tag = oIndication.get_ID();
					d.SubItems.Add(oIndication.Display.ToString());
					d.SubItems.Add(oIndication.oTypeIndication.get_Name());
					if (oIndication.oAgent == null)
					{
						d.SubItems.Add("");
					}
					else
					{
						d.SubItems.Add(oIndication.oAgent.get_Name());
					}
					if (oIndication.oUserAdd == null)
					{
						ListViewItem.ListViewSubItemCollection subItems = d.SubItems;
						datedisplay = oIndication.DateAdd;
						subItems.Add(string.Concat("Неизвестный, ", datedisplay.ToShortDateString()));
					}
					else
					{
						ListViewItem.ListViewSubItemCollection listViewSubItemCollections = d.SubItems;
						string name = oIndication.oUserAdd.get_Name();
						datedisplay = oIndication.DateAdd;
						listViewSubItemCollections.Add(string.Concat(name, " ", datedisplay.ToShortDateString()));
					}
				}
			}
		}

		private void CreateParameters()
		{
			this.lvParam.Items.Clear();
			foreach (PC oPC in this._contract.oPCs)
			{
				ListViewItem listViewItem = this.lvParam.Items.Add(oPC.oTypePC.get_Name());
				listViewItem.SubItems.Add(oPC.Value);
				listViewItem.SubItems.Add(oPC.oPeriod.get_Name());
			}
		}

		private void CreateStatus(int _status)
		{
			this.cmbStatus.ClearItems();
			this.cmbStatus.AddItem("Не определен");
			this.cmbStatus.AddItem("Активен");
			this.cmbStatus.AddItem("Закрыт");
			this.cmbStatus.SelectedIndex = _status;
			this.cmbStatus.ColumnWidth = this.cmbStatus.Width - this.cmbStatus.Height;
		}

		private void CreateTypeContract(long IdType)
		{
			this._typecontracts = new TypeContracts();
			this._typecontracts.Load();
			Tools.FillC1(this._typecontracts, this.cmbType, IdType);
		}

		private void CreateTypeInfringements(long IdType)
		{
			this._TypeInfringementss = new TypeInfringementss();
			this._TypeInfringementss.Load();
			Tools.FillC1(this._TypeInfringementss, this.cmbTypeInfringements, IdType);
		}

		private void CreateTypeTariff()
		{
			if (this._typetariffs == null)
			{
				this._typetariffs = new TypeTariffs();
				this._typetariffs.Load();
			}
			this.cmbTypeTariff.ClearItems();
			int num = 0;
			for (int i = 0; i < this._typetariffs.get_Count(); i++)
			{
				this.cmbTypeTariff.AddItem(this._typetariffs[i].FullName);
				if (this._contract.oTypeTariff != null && this._typetariffs[i].get_ID() == this._contract.oTypeTariff.get_ID())
				{
					num = i;
				}
			}
			if (this.cmbTypeTariff.ListCount > num)
			{
				this.cmbTypeTariff.SelectedIndex = num;
			}
			this.cmbTypeTariff.ColumnWidth = this.cmbTypeTariff.Width - this.cmbTypeTariff.Height;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
			this._contract = null;
			this._typecontracts = null;
			this._typetariffs = null;
		}

		private void frmContract_Closed(object sender, EventArgs e)
		{
			Form owner = base.Owner;
			base.Dispose();
			owner.Activate();
		}

		private void frmContract_Load(object sender, EventArgs e)
		{
			double num;
			this.TT = new ToolTip();
			this.TT.SetToolTip(this.cmdGMeter, "Редактировать ПУ");
			this.TT.SetToolTip(this.cmdGMeterAdd, "Добавить ПУ");
			this.TT.SetToolTip(this.cmdGMeterDel, "Удалить ПУ");
			this.TT.SetToolTip(this.cmdGObject, "Редактировать ОУ");
			this.TT.SetToolTip(this.cmdGObjectAdd, "Добавить ОУ");
			this.TT.SetToolTip(this.cmdGObjectDel, "Удалить ОУ");
			int num1 = -2;
			DateTime today = DateTime.Today;
			this.DateBegin = today.AddYears(num1);
			if (DateTime.Today.Month != 12)
			{
				int year = DateTime.Today.Year;
				today = DateTime.Today;
				today = new DateTime(year, today.Month + 1, 1);
				this.DateEnd = today.AddDays(-1);
			}
			else
			{
				today = DateTime.Today;
				today = new DateTime(today.Year + 1, 1, 1);
				this.DateEnd = today.AddDays(-1);
			}
			today = DateTime.Today;
			this.DateBeginI = today.AddYears(num1);
			if (DateTime.Today.Month != 12)
			{
				int year1 = DateTime.Today.Year;
				today = DateTime.Today;
				today = new DateTime(year1, today.Month + 1, 1);
				this.DateEndI = today.AddDays(-1);
			}
			else
			{
				today = DateTime.Today;
				today = new DateTime(today.Year + 1, 1, 1);
				this.DateEndI = today.AddDays(-1);
			}
			if (this._contract == null)
			{
				base.Close();
			}
			if (!this._contract.get_isNew())
			{
				this.txtAccount.ReadOnly = true;
				this.txtAccount.BackColor = SystemColors.Info;
				this.tabControl1.SelectedIndex = 0;
			}
			else
			{
				this.txtAccount.ReadOnly = false;
				this.txtAccount.BackColor = SystemColors.Window;
				this.tabControl1.SelectedIndex = 2;
			}
			if (this._contract.ChargePeny != 0)
			{
				this.chkPeny.Checked = false;
			}
			else
			{
				this.chkPeny.Checked = true;
			}
			this.txtAccount.Text = this._contract.Account;
			this.txtName.Text = this._contract.oPerson.FullName;
			this.txtAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
			if (!this._contract.get_isNew())
			{
				this.CreateTypeContract(this._contract.oTypeContract.get_ID());
				this.CreateTypeInfringements(this._contract.oTypeInfringements.get_ID());
			}
			else
			{
				this.CreateTypeContract((long)0);
				this.CreateTypeInfringements((long)1);
				this.chkPeny.Checked = false;
			}
			this.isload = false;
			if (this._contract.PrintChetIzvehen != 1)
			{
				this.ckshet.Checked = false;
			}
			else
			{
				this.ckshet.Checked = true;
			}
			this.isload = true;
			this.CreateStatus(this._contract.Status);
			this.CreateGObject();
			this.dtFrom.Value = this._contract.DateBegin;
			this.dtTo.Value = this._contract.DateEnd;
			this.txtMemo.Text = this._contract.Memo;
			this.CreateTypeTariff();
			double num2 = this._contract.CurrentBalance();
			if (num2 >= 0)
			{
				Label label = this.lblSaldo;
				num = Math.Round(num2, 4);
				label.Text = string.Concat("Переплата: ", num.ToString());
				this.lblSaldo.ForeColor = Color.Black;
				return;
			}
			Label label1 = this.lblSaldo;
			num = Math.Round(-num2, 4);
			label1.Text = string.Concat("Долг: ", num.ToString());
			this.lblSaldo.ForeColor = Color.Red;
		}

		private string GetStatusClaimDoc(Document oDocument)
		{
			if (oDocument.GetPD(23) == null)
			{
				return "Сформирована";
			}
			if (oDocument.GetPD(24) != null)
			{
				return "Закрыта";
			}
			return string.Concat("Вручена ", oDocument.GetNamePD(23));
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmContract));
			this.txtAccount = new TextBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.cmbStatus = new C1Combo();
			this.cmbType = new C1Combo();
			this.label3 = new Label();
			this.groupBox1 = new GroupBox();
			this.txtAddress = new TextBox();
			this.txtName = new TextBox();
			this.label5 = new Label();
			this.label4 = new Label();
			this.imageList1 = new ImageList(this.components);
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.cmdGObjectDel = new Button();
			this.lblStatusGO = new Label();
			this.cmdStatusGO = new Button();
			this.cmdChangeCL = new Button();
			this.cmdGObject = new Button();
			this.cmdGObjectAdd = new Button();
			this.groupBox3 = new GroupBox();
			this.lvFactUse = new ListView();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.groupBox2 = new GroupBox();
			this.cmdGMeterDel = new Button();
			this.cmdGMeter = new Button();
			this.cmdGMeterAdd = new Button();
			this.cmbGMeter = new C1Combo();
			this.lvDisplay = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.panel2 = new Panel();
			this.tbData = new ToolBar();
			this.toolBarButton7 = new ToolBarButton();
			this.toolBarButton8 = new ToolBarButton();
			this.toolBarButton9 = new ToolBarButton();
			this.toolBarButton10 = new ToolBarButton();
			this.toolBarButton11 = new ToolBarButton();
			this.toolBarButton18 = new ToolBarButton();
			this.toolBarButton19 = new ToolBarButton();
			this.txtCountLives = new TextBox();
			this.label7 = new Label();
			this.txtGRU = new TextBox();
			this.label6 = new Label();
			this.cmbGObject = new C1Combo();
			this.tabPage2 = new TabPage();
			this.toolBar1 = new ToolBar();
			this.toolBarButton6 = new ToolBarButton();
			this.toolBarButton12 = new ToolBarButton();
			this.toolBarButton13 = new ToolBarButton();
			this.toolBarButton14 = new ToolBarButton();
			this.toolBarButton15 = new ToolBarButton();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton17 = new ToolBarButton();
			this.lvDoc = new ListView();
			this.columnHeader12 = new ColumnHeader();
			this.columnHeader13 = new ColumnHeader();
			this.columnHeader14 = new ColumnHeader();
			this.columnHeader15 = new ColumnHeader();
			this.columnHeader20 = new ColumnHeader();
			this.columnHeader16 = new ColumnHeader();
			this.tabPage3 = new TabPage();
			this.groupBox4 = new GroupBox();
			this.lvParam = new ListView();
			this.columnHeader17 = new ColumnHeader();
			this.columnHeader18 = new ColumnHeader();
			this.columnHeader19 = new ColumnHeader();
			this.panel1 = new Panel();
			this.tbParam = new ToolBar();
			this.toolBarButton1 = new ToolBarButton();
			this.toolBarButton2 = new ToolBarButton();
			this.toolBarButton3 = new ToolBarButton();
			this.toolBarButton4 = new ToolBarButton();
			this.toolBarButton5 = new ToolBarButton();
			this.txtMemo = new TextBox();
			this.label11 = new Label();
			this.label10 = new Label();
			this.cmbTypeTariff = new C1Combo();
			this.dtTo = new C1DateEdit();
			this.label9 = new Label();
			this.dtFrom = new C1DateEdit();
			this.label8 = new Label();
			this.lblSaldo = new Label();
			this.lblDisplay = new Label();
			this.cmdOk = new Button();
			this.cmdCancel = new Button();
			this.chkDateFrom = new CheckBox();
			this.cmdApply = new Button();
			this.ckshet = new CheckBox();
			this.cmbTypeInfringements = new C1Combo();
			this.label12 = new Label();
			this.chkPeny = new CheckBox();
			((ISupportInitialize)this.cmbStatus).BeginInit();
			((ISupportInitialize)this.cmbType).BeginInit();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.cmbGMeter).BeginInit();
			this.panel2.SuspendLayout();
			((ISupportInitialize)this.cmbGObject).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.cmbTypeTariff).BeginInit();
			((ISupportInitialize)this.dtTo).BeginInit();
			((ISupportInitialize)this.dtFrom).BeginInit();
			((ISupportInitialize)this.cmbTypeInfringements).BeginInit();
			base.SuspendLayout();
			this.txtAccount.BorderStyle = BorderStyle.FixedSingle;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(104, 8);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(128, 23);
			this.txtAccount.TabIndex = 0;
			this.txtAccount.Text = "";
			this.txtAccount.TextAlign = HorizontalAlignment.Center;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Лицевой счет:";
			this.label2.Location = new Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Статус договора:";
			this.cmbStatus.AddItemSeparator = ';';
			this.cmbStatus.BorderStyle = 1;
			this.cmbStatus.Caption = "";
			this.cmbStatus.CaptionHeight = 17;
			this.cmbStatus.CharacterCasing = 0;
			this.cmbStatus.ColumnCaptionHeight = 17;
			this.cmbStatus.ColumnFooterHeight = 17;
			this.cmbStatus.ColumnHeaders = false;
			this.cmbStatus.ColumnWidth = 149;
			this.cmbStatus.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbStatus.ContentHeight = 15;
			this.cmbStatus.DataMode = DataModeEnum.AddItem;
			this.cmbStatus.DeadAreaBackColor = Color.Empty;
			this.cmbStatus.EditorBackColor = SystemColors.Window;
			this.cmbStatus.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbStatus.EditorForeColor = SystemColors.WindowText;
			this.cmbStatus.EditorHeight = 15;
			this.cmbStatus.FlatStyle = FlatModeEnum.Flat;
			this.cmbStatus.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbStatus.ItemHeight = 15;
			this.cmbStatus.Location = new Point(104, 32);
			this.cmbStatus.MatchEntryTimeout = (long)2000;
			this.cmbStatus.MaxDropDownItems = 5;
			this.cmbStatus.MaxLength = 32767;
			this.cmbStatus.MouseCursor = Cursors.Default;
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.RowDivider.Color = Color.DarkGray;
			this.cmbStatus.RowDivider.Style = LineStyleEnum.None;
			this.cmbStatus.RowSubDividerColor = Color.DarkGray;
			this.cmbStatus.Size = new System.Drawing.Size(208, 19);
			this.cmbStatus.TabIndex = 3;
			this.cmbStatus.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>149</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.cmbType.AddItemSeparator = ';';
			this.cmbType.BorderStyle = 1;
			this.cmbType.Caption = "";
			this.cmbType.CaptionHeight = 17;
			this.cmbType.CharacterCasing = 0;
			this.cmbType.ColumnCaptionHeight = 17;
			this.cmbType.ColumnFooterHeight = 17;
			this.cmbType.ColumnHeaders = false;
			this.cmbType.ColumnWidth = 149;
			this.cmbType.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbType.ContentHeight = 15;
			this.cmbType.DataMode = DataModeEnum.AddItem;
			this.cmbType.DeadAreaBackColor = Color.Empty;
			this.cmbType.EditorBackColor = SystemColors.Window;
			this.cmbType.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbType.EditorForeColor = SystemColors.WindowText;
			this.cmbType.EditorHeight = 15;
			this.cmbType.FlatStyle = FlatModeEnum.Flat;
			this.cmbType.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbType.ItemHeight = 15;
			this.cmbType.Location = new Point(104, 56);
			this.cmbType.MatchEntryTimeout = (long)2000;
			this.cmbType.MaxDropDownItems = 5;
			this.cmbType.MaxLength = 32767;
			this.cmbType.MouseCursor = Cursors.Default;
			this.cmbType.Name = "cmbType";
			this.cmbType.RowDivider.Color = Color.DarkGray;
			this.cmbType.RowDivider.Style = LineStyleEnum.None;
			this.cmbType.RowSubDividerColor = Color.DarkGray;
			this.cmbType.Size = new System.Drawing.Size(208, 19);
			this.cmbType.TabIndex = 4;
			this.cmbType.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>149</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label3.Location = new Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Тип договора:";
			this.groupBox1.Controls.Add(this.txtAddress);
			this.groupBox1.Controls.Add(this.txtName);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new Point(320, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 72);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Потребитель";
			this.txtAddress.BackColor = SystemColors.Info;
			this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
			this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAddress.Location = new Point(80, 40);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.ReadOnly = true;
			this.txtAddress.Size = new System.Drawing.Size(288, 20);
			this.txtAddress.TabIndex = 3;
			this.txtAddress.Text = "";
			this.txtName.BackColor = SystemColors.Info;
			this.txtName.BorderStyle = BorderStyle.FixedSingle;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtName.Location = new Point(80, 16);
			this.txtName.Name = "txtName";
			this.txtName.ReadOnly = true;
			this.txtName.Size = new System.Drawing.Size(288, 20);
			this.txtName.TabIndex = 2;
			this.txtName.Text = "";
			this.label5.Location = new Point(8, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "Адрес:";
			this.label4.Location = new Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Потребитель:";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.tabControl1.Appearance = TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new Point(0, 112);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(696, 352);
			this.tabControl1.TabIndex = 7;
			this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabPage1.Controls.Add(this.cmdGObjectDel);
			this.tabPage1.Controls.Add(this.lblStatusGO);
			this.tabPage1.Controls.Add(this.cmdStatusGO);
			this.tabPage1.Controls.Add(this.cmdChangeCL);
			this.tabPage1.Controls.Add(this.cmdGObject);
			this.tabPage1.Controls.Add(this.cmdGObjectAdd);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.txtCountLives);
			this.tabPage1.Controls.Add(this.label7);
			this.tabPage1.Controls.Add(this.txtGRU);
			this.tabPage1.Controls.Add(this.label6);
			this.tabPage1.Controls.Add(this.cmbGObject);
			this.tabPage1.Location = new Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(688, 323);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Объект учета";
			this.cmdGObjectDel.FlatStyle = FlatStyle.Flat;
			this.cmdGObjectDel.ForeColor = SystemColors.ControlText;
			this.cmdGObjectDel.ImageIndex = 2;
			this.cmdGObjectDel.ImageList = this.imageList1;
			this.cmdGObjectDel.Location = new Point(408, 0);
			this.cmdGObjectDel.Name = "cmdGObjectDel";
			this.cmdGObjectDel.Size = new System.Drawing.Size(20, 20);
			this.cmdGObjectDel.TabIndex = 15;
			this.cmdGObjectDel.Click += new EventHandler(this.cmdGObjectDel_Click);
			this.lblStatusGO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblStatusGO.Location = new Point(400, 24);
			this.lblStatusGO.Name = "lblStatusGO";
			this.lblStatusGO.Size = new System.Drawing.Size(168, 16);
			this.lblStatusGO.TabIndex = 14;
			this.lblStatusGO.Text = "-";
			this.lblStatusGO.TextAlign = ContentAlignment.TopRight;
			this.cmdStatusGO.FlatStyle = FlatStyle.Flat;
			this.cmdStatusGO.ImageAlign = ContentAlignment.MiddleLeft;
			this.cmdStatusGO.ImageIndex = 7;
			this.cmdStatusGO.ImageList = this.imageList1;
			this.cmdStatusGO.Location = new Point(576, 24);
			this.cmdStatusGO.Name = "cmdStatusGO";
			this.cmdStatusGO.Size = new System.Drawing.Size(112, 24);
			this.cmdStatusGO.TabIndex = 13;
			this.cmdStatusGO.Text = "Отключение ОУ";
			this.cmdStatusGO.TextAlign = ContentAlignment.MiddleRight;
			this.cmdStatusGO.Click += new EventHandler(this.cmdStatusGO_Click);
			this.cmdChangeCL.FlatStyle = FlatStyle.Flat;
			this.cmdChangeCL.ImageAlign = ContentAlignment.MiddleLeft;
			this.cmdChangeCL.ImageIndex = 8;
			this.cmdChangeCL.ImageList = this.imageList1;
			this.cmdChangeCL.Location = new Point(160, 24);
			this.cmdChangeCL.Name = "cmdChangeCL";
			this.cmdChangeCL.Size = new System.Drawing.Size(160, 24);
			this.cmdChangeCL.TabIndex = 12;
			this.cmdChangeCL.Text = "Изменение численности";
			this.cmdChangeCL.TextAlign = ContentAlignment.MiddleRight;
			this.cmdChangeCL.Click += new EventHandler(this.cmdChangeCL_Click);
			this.cmdGObject.FlatStyle = FlatStyle.Flat;
			this.cmdGObject.ForeColor = SystemColors.ControlText;
			this.cmdGObject.ImageIndex = 5;
			this.cmdGObject.ImageList = this.imageList1;
			this.cmdGObject.Location = new Point(360, 0);
			this.cmdGObject.Name = "cmdGObject";
			this.cmdGObject.Size = new System.Drawing.Size(20, 20);
			this.cmdGObject.TabIndex = 11;
			this.cmdGObject.Click += new EventHandler(this.cmdGObject_Click);
			this.cmdGObjectAdd.FlatStyle = FlatStyle.Flat;
			this.cmdGObjectAdd.ForeColor = SystemColors.ControlText;
			this.cmdGObjectAdd.ImageIndex = 0;
			this.cmdGObjectAdd.ImageList = this.imageList1;
			this.cmdGObjectAdd.Location = new Point(384, 0);
			this.cmdGObjectAdd.Name = "cmdGObjectAdd";
			this.cmdGObjectAdd.Size = new System.Drawing.Size(20, 20);
			this.cmdGObjectAdd.TabIndex = 10;
			this.cmdGObjectAdd.Click += new EventHandler(this.cmdGObjectAdd_Click);
			this.groupBox3.Controls.Add(this.lvFactUse);
			this.groupBox3.Location = new Point(0, 200);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(688, 120);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Потребление";
			this.lvFactUse.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lvFactUse.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader9, this.columnHeader10, this.columnHeader11 };
			columns.AddRange(columnHeaderArray);
			this.lvFactUse.FullRowSelect = true;
			this.lvFactUse.GridLines = true;
			this.lvFactUse.Location = new Point(8, 16);
			this.lvFactUse.MultiSelect = false;
			this.lvFactUse.Name = "lvFactUse";
			this.lvFactUse.Size = new System.Drawing.Size(672, 96);
			this.lvFactUse.TabIndex = 1;
			this.lvFactUse.View = View.Details;
			this.columnHeader6.Text = "Период";
			this.columnHeader6.Width = 55;
			this.columnHeader7.Text = "Количество";
			this.columnHeader7.Width = 75;
			this.columnHeader8.Text = "Ед. изм";
			this.columnHeader8.Width = 53;
			this.columnHeader9.Text = "Тип потребления";
			this.columnHeader9.Width = 139;
			this.columnHeader10.Text = "На основании";
			this.columnHeader10.Width = 165;
			this.columnHeader11.Text = "Начислено";
			this.columnHeader11.Width = 162;
			this.groupBox2.BackColor = SystemColors.Menu;
			this.groupBox2.Controls.Add(this.cmdGMeterDel);
			this.groupBox2.Controls.Add(this.cmdGMeter);
			this.groupBox2.Controls.Add(this.cmdGMeterAdd);
			this.groupBox2.Controls.Add(this.cmbGMeter);
			this.groupBox2.Controls.Add(this.lvDisplay);
			this.groupBox2.Controls.Add(this.panel2);
			this.groupBox2.Location = new Point(0, 48);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(688, 152);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Показания";
			this.cmdGMeterDel.FlatStyle = FlatStyle.Flat;
			this.cmdGMeterDel.ForeColor = SystemColors.ControlText;
			this.cmdGMeterDel.ImageIndex = 2;
			this.cmdGMeterDel.ImageList = this.imageList1;
			this.cmdGMeterDel.Location = new Point(656, 16);
			this.cmdGMeterDel.Name = "cmdGMeterDel";
			this.cmdGMeterDel.Size = new System.Drawing.Size(20, 20);
			this.cmdGMeterDel.TabIndex = 7;
			this.cmdGMeterDel.Click += new EventHandler(this.cmdGMeterDel_Click);
			this.cmdGMeter.FlatStyle = FlatStyle.Flat;
			this.cmdGMeter.ForeColor = SystemColors.ControlText;
			this.cmdGMeter.ImageIndex = 5;
			this.cmdGMeter.ImageList = this.imageList1;
			this.cmdGMeter.Location = new Point(608, 16);
			this.cmdGMeter.Name = "cmdGMeter";
			this.cmdGMeter.Size = new System.Drawing.Size(20, 20);
			this.cmdGMeter.TabIndex = 6;
			this.cmdGMeter.Click += new EventHandler(this.cmdGMeter_Click);
			this.cmdGMeterAdd.FlatStyle = FlatStyle.Flat;
			this.cmdGMeterAdd.ForeColor = SystemColors.ControlText;
			this.cmdGMeterAdd.ImageIndex = 0;
			this.cmdGMeterAdd.ImageList = this.imageList1;
			this.cmdGMeterAdd.Location = new Point(632, 16);
			this.cmdGMeterAdd.Name = "cmdGMeterAdd";
			this.cmdGMeterAdd.Size = new System.Drawing.Size(20, 20);
			this.cmdGMeterAdd.TabIndex = 5;
			this.cmdGMeterAdd.Click += new EventHandler(this.cmdGMeterAdd_Click);
			this.cmbGMeter.AddItemSeparator = ';';
			this.cmbGMeter.BorderStyle = 1;
			this.cmbGMeter.Caption = "";
			this.cmbGMeter.CaptionHeight = 17;
			this.cmbGMeter.CharacterCasing = 0;
			this.cmbGMeter.ColumnCaptionHeight = 17;
			this.cmbGMeter.ColumnFooterHeight = 17;
			this.cmbGMeter.ColumnHeaders = false;
			this.cmbGMeter.ColumnWidth = 245;
			this.cmbGMeter.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbGMeter.ContentHeight = 15;
			this.cmbGMeter.DataMode = DataModeEnum.AddItem;
			this.cmbGMeter.DeadAreaBackColor = Color.Empty;
			this.cmbGMeter.EditorBackColor = SystemColors.Window;
			this.cmbGMeter.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbGMeter.EditorForeColor = SystemColors.WindowText;
			this.cmbGMeter.EditorHeight = 15;
			this.cmbGMeter.FlatStyle = FlatModeEnum.Flat;
			this.cmbGMeter.Images.Add((Image)resourceManager.GetObject("resource2"));
			this.cmbGMeter.ItemHeight = 15;
			this.cmbGMeter.Location = new Point(248, 16);
			this.cmbGMeter.MatchEntryTimeout = (long)2000;
			this.cmbGMeter.MaxDropDownItems = 5;
			this.cmbGMeter.MaxLength = 32767;
			this.cmbGMeter.MouseCursor = Cursors.Default;
			this.cmbGMeter.Name = "cmbGMeter";
			this.cmbGMeter.RowDivider.Color = Color.DarkGray;
			this.cmbGMeter.RowDivider.Style = LineStyleEnum.None;
			this.cmbGMeter.RowSubDividerColor = Color.DarkGray;
			this.cmbGMeter.Size = new System.Drawing.Size(360, 19);
			this.cmbGMeter.TabIndex = 3;
			this.cmbGMeter.TextChanged += new EventHandler(this.cmbGMeter_TextChanged);
			this.cmbGMeter.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>245</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.lvDisplay.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columnHeaderCollections = this.lvDisplay.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5 };
			columnHeaderCollections.AddRange(columnHeaderArray);
			this.lvDisplay.FullRowSelect = true;
			this.lvDisplay.GridLines = true;
			this.lvDisplay.Location = new Point(8, 48);
			this.lvDisplay.MultiSelect = false;
			this.lvDisplay.Name = "lvDisplay";
			this.lvDisplay.Size = new System.Drawing.Size(672, 96);
			this.lvDisplay.TabIndex = 0;
			this.lvDisplay.View = View.Details;
			this.columnHeader1.Text = "Дата";
			this.columnHeader1.Width = 84;
			this.columnHeader2.Text = "Показания";
			this.columnHeader2.Width = 89;
			this.columnHeader3.Text = "Тип показания";
			this.columnHeader3.Width = 134;
			this.columnHeader4.Text = "Контролер";
			this.columnHeader4.Width = 170;
			this.columnHeader5.Text = "Автор и дата добавления";
			this.columnHeader5.Width = 180;
			this.panel2.Controls.Add(this.tbData);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 16);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(682, 133);
			this.panel2.TabIndex = 8;
			this.tbData.Appearance = ToolBarAppearance.Flat;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton7, this.toolBarButton8, this.toolBarButton9, this.toolBarButton10, this.toolBarButton11, this.toolBarButton18, this.toolBarButton19 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.Divider = false;
			this.tbData.DropDownArrows = true;
			this.tbData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.tbData.ImageList = this.imageList1;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(682, 26);
			this.tbData.TabIndex = 4;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.toolBarButton7.ImageIndex = 0;
			this.toolBarButton7.Tag = "Add";
			this.toolBarButton7.ToolTipText = "Добавить";
			this.toolBarButton8.ImageIndex = 1;
			this.toolBarButton8.Tag = "Edit";
			this.toolBarButton8.ToolTipText = "Редактировать";
			this.toolBarButton9.ImageIndex = 2;
			this.toolBarButton9.Tag = "Del";
			this.toolBarButton9.ToolTipText = "Удалить";
			this.toolBarButton10.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton11.ImageIndex = 3;
			this.toolBarButton11.Tag = "Excel";
			this.toolBarButton11.ToolTipText = "Конвертировать в Excel";
			this.toolBarButton18.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton19.ImageIndex = 9;
			this.toolBarButton19.Tag = "Depth";
			this.toolBarButton19.ToolTipText = "Интервал";
			this.txtCountLives.BackColor = SystemColors.Info;
			this.txtCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.txtCountLives.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtCountLives.Location = new Point(104, 24);
			this.txtCountLives.Name = "txtCountLives";
			this.txtCountLives.ReadOnly = true;
			this.txtCountLives.Size = new System.Drawing.Size(48, 20);
			this.txtCountLives.TabIndex = 6;
			this.txtCountLives.Text = "";
			this.txtCountLives.TextAlign = HorizontalAlignment.Right;
			this.label7.Location = new Point(8, 24);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 16);
			this.label7.TabIndex = 5;
			this.label7.Text = "Проживает, чел:";
			this.txtGRU.BackColor = SystemColors.Info;
			this.txtGRU.BorderStyle = BorderStyle.FixedSingle;
			this.txtGRU.Location = new Point(432, 0);
			this.txtGRU.Name = "txtGRU";
			this.txtGRU.ReadOnly = true;
			this.txtGRU.Size = new System.Drawing.Size(256, 20);
			this.txtGRU.TabIndex = 4;
			this.txtGRU.Text = "";
			this.label6.Location = new Point(8, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 16);
			this.label6.TabIndex = 3;
			this.label6.Text = "Объект учета:";
			this.cmbGObject.AddItemSeparator = ';';
			this.cmbGObject.BorderStyle = 1;
			this.cmbGObject.Caption = "";
			this.cmbGObject.CaptionHeight = 17;
			this.cmbGObject.CharacterCasing = 0;
			this.cmbGObject.ColumnCaptionHeight = 17;
			this.cmbGObject.ColumnFooterHeight = 17;
			this.cmbGObject.ColumnHeaders = false;
			this.cmbGObject.ColumnWidth = 245;
			this.cmbGObject.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbGObject.ContentHeight = 15;
			this.cmbGObject.DataMode = DataModeEnum.AddItem;
			this.cmbGObject.DeadAreaBackColor = Color.Empty;
			this.cmbGObject.EditorBackColor = SystemColors.Window;
			this.cmbGObject.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbGObject.EditorForeColor = SystemColors.WindowText;
			this.cmbGObject.EditorHeight = 15;
			this.cmbGObject.FlatStyle = FlatModeEnum.Flat;
			this.cmbGObject.Images.Add((Image)resourceManager.GetObject("resource3"));
			this.cmbGObject.ItemHeight = 15;
			this.cmbGObject.Location = new Point(104, 0);
			this.cmbGObject.MatchEntryTimeout = (long)2000;
			this.cmbGObject.MaxDropDownItems = 5;
			this.cmbGObject.MaxLength = 32767;
			this.cmbGObject.MouseCursor = Cursors.Default;
			this.cmbGObject.Name = "cmbGObject";
			this.cmbGObject.RowDivider.Color = Color.DarkGray;
			this.cmbGObject.RowDivider.Style = LineStyleEnum.None;
			this.cmbGObject.RowSubDividerColor = Color.DarkGray;
			this.cmbGObject.Size = new System.Drawing.Size(256, 19);
			this.cmbGObject.TabIndex = 0;
			this.cmbGObject.TextChanged += new EventHandler(this.cmbGObject_TextChanged);
			this.cmbGObject.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>245</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.tabPage2.Controls.Add(this.toolBar1);
			this.tabPage2.Controls.Add(this.lvDoc);
			this.tabPage2.Location = new Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(688, 323);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Документы";
			this.toolBar1.Appearance = ToolBarAppearance.Flat;
			ToolBar.ToolBarButtonCollection toolBarButtonCollections = this.toolBar1.Buttons;
			toolBarButtonArray = new ToolBarButton[] { this.toolBarButton6, this.toolBarButton12, this.toolBarButton13, this.toolBarButton14, this.toolBarButton15, this.toolBarButton16, this.toolBarButton17 };
			toolBarButtonCollections.AddRange(toolBarButtonArray);
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imageList1;
			this.toolBar1.Location = new Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(688, 28);
			this.toolBar1.TabIndex = 1;
			this.toolBar1.ButtonClick += new ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			this.toolBarButton6.ImageIndex = 0;
			this.toolBarButton6.Tag = "Add";
			this.toolBarButton6.ToolTipText = "Добавить";
			this.toolBarButton12.ImageIndex = 1;
			this.toolBarButton12.Tag = "Edit";
			this.toolBarButton12.ToolTipText = "Редактировать";
			this.toolBarButton13.ImageIndex = 2;
			this.toolBarButton13.Tag = "Del";
			this.toolBarButton13.ToolTipText = "Удалить";
			this.toolBarButton14.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton15.ImageIndex = 3;
			this.toolBarButton15.Tag = "Excel";
			this.toolBarButton15.ToolTipText = "Конвертировать в Excel";
			this.toolBarButton16.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton16.Text = "-";
			this.toolBarButton17.ImageIndex = 9;
			this.toolBarButton17.Tag = "Depth";
			this.toolBarButton17.ToolTipText = "Интервал";
			this.lvDoc.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns1 = this.lvDoc.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader12, this.columnHeader13, this.columnHeader14, this.columnHeader15, this.columnHeader20, this.columnHeader16 };
			columns1.AddRange(columnHeaderArray);
			this.lvDoc.FullRowSelect = true;
			this.lvDoc.GridLines = true;
			this.lvDoc.Location = new Point(0, 32);
			this.lvDoc.Name = "lvDoc";
			this.lvDoc.Size = new System.Drawing.Size(688, 288);
			this.lvDoc.TabIndex = 0;
			this.lvDoc.View = View.Details;
			this.columnHeader12.Text = "Дата";
			this.columnHeader13.Text = "Тип документа";
			this.columnHeader13.Width = 114;
			this.columnHeader14.Text = "Сумма";
			this.columnHeader14.TextAlign = HorizontalAlignment.Right;
			this.columnHeader15.Text = "Дополнительно";
			this.columnHeader15.Width = 162;
			this.columnHeader20.Text = "Примечание";
			this.columnHeader20.Width = 92;
			this.columnHeader16.Text = "Автор и дата добавления";
			this.columnHeader16.Width = 197;
			this.tabPage3.Controls.Add(this.groupBox4);
			this.tabPage3.Controls.Add(this.txtMemo);
			this.tabPage3.Controls.Add(this.label11);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.cmbTypeTariff);
			this.tabPage3.Controls.Add(this.dtTo);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.dtFrom);
			this.tabPage3.Controls.Add(this.label8);
			this.tabPage3.Location = new Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(688, 323);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Прочие";
			this.groupBox4.BackColor = SystemColors.Menu;
			this.groupBox4.Controls.Add(this.lvParam);
			this.groupBox4.Controls.Add(this.panel1);
			this.groupBox4.Location = new Point(0, 128);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(688, 192);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Параметры договора";
			this.lvParam.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columnHeaderCollections1 = this.lvParam.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader17, this.columnHeader18, this.columnHeader19 };
			columnHeaderCollections1.AddRange(columnHeaderArray);
			this.lvParam.FullRowSelect = true;
			this.lvParam.GridLines = true;
			this.lvParam.Location = new Point(8, 48);
			this.lvParam.MultiSelect = false;
			this.lvParam.Name = "lvParam";
			this.lvParam.Size = new System.Drawing.Size(672, 136);
			this.lvParam.TabIndex = 0;
			this.lvParam.View = View.Details;
			this.columnHeader17.Text = "Тип параметра";
			this.columnHeader17.Width = 239;
			this.columnHeader18.Text = "Значение";
			this.columnHeader18.Width = 277;
			this.columnHeader19.Text = "Период";
			this.columnHeader19.Width = 132;
			this.panel1.Controls.Add(this.tbParam);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(682, 173);
			this.panel1.TabIndex = 3;
			this.tbParam.Appearance = ToolBarAppearance.Flat;
			ToolBar.ToolBarButtonCollection buttons1 = this.tbParam.Buttons;
			toolBarButtonArray = new ToolBarButton[] { this.toolBarButton1, this.toolBarButton2, this.toolBarButton3, this.toolBarButton4, this.toolBarButton5 };
			buttons1.AddRange(toolBarButtonArray);
			this.tbParam.DropDownArrows = true;
			this.tbParam.ImageList = this.imageList1;
			this.tbParam.Location = new Point(0, 0);
			this.tbParam.Name = "tbParam";
			this.tbParam.ShowToolTips = true;
			this.tbParam.Size = new System.Drawing.Size(682, 28);
			this.tbParam.TabIndex = 0;
			this.tbParam.ButtonClick += new ToolBarButtonClickEventHandler(this.tbParam_ButtonClick);
			this.toolBarButton1.ImageIndex = 0;
			this.toolBarButton1.Tag = "Add";
			this.toolBarButton1.ToolTipText = "Добавить";
			this.toolBarButton2.ImageIndex = 1;
			this.toolBarButton2.Tag = "Edit";
			this.toolBarButton2.ToolTipText = "Редактировать";
			this.toolBarButton3.ImageIndex = 2;
			this.toolBarButton3.Tag = "Del";
			this.toolBarButton3.ToolTipText = "Удалить";
			this.toolBarButton4.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton5.ImageIndex = 3;
			this.toolBarButton5.Tag = "Excel";
			this.toolBarButton5.ToolTipText = "Конвертировать в Excel";
			this.txtMemo.BorderStyle = BorderStyle.FixedSingle;
			this.txtMemo.Location = new Point(104, 48);
			this.txtMemo.Multiline = true;
			this.txtMemo.Name = "txtMemo";
			this.txtMemo.ScrollBars = ScrollBars.Vertical;
			this.txtMemo.Size = new System.Drawing.Size(576, 80);
			this.txtMemo.TabIndex = 12;
			this.txtMemo.Text = "";
			this.label11.Location = new Point(8, 48);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 16);
			this.label11.TabIndex = 11;
			this.label11.Text = "Примечание:";
			this.label10.Location = new Point(8, 24);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(96, 16);
			this.label10.TabIndex = 10;
			this.label10.Text = "Тип тарифа:";
			this.cmbTypeTariff.AddItemSeparator = ';';
			this.cmbTypeTariff.BorderStyle = 1;
			this.cmbTypeTariff.Caption = "";
			this.cmbTypeTariff.CaptionHeight = 17;
			this.cmbTypeTariff.CharacterCasing = 0;
			this.cmbTypeTariff.ColumnCaptionHeight = 17;
			this.cmbTypeTariff.ColumnFooterHeight = 17;
			this.cmbTypeTariff.ColumnHeaders = false;
			this.cmbTypeTariff.ColumnWidth = 245;
			this.cmbTypeTariff.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeTariff.ContentHeight = 15;
			this.cmbTypeTariff.DataMode = DataModeEnum.AddItem;
			this.cmbTypeTariff.DeadAreaBackColor = Color.Empty;
			this.cmbTypeTariff.EditorBackColor = SystemColors.Window;
			this.cmbTypeTariff.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeTariff.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeTariff.EditorHeight = 15;
			this.cmbTypeTariff.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeTariff.Images.Add((Image)resourceManager.GetObject("resource4"));
			this.cmbTypeTariff.ItemHeight = 15;
			this.cmbTypeTariff.Location = new Point(152, 24);
			this.cmbTypeTariff.MatchEntryTimeout = (long)2000;
			this.cmbTypeTariff.MaxDropDownItems = 5;
			this.cmbTypeTariff.MaxLength = 32767;
			this.cmbTypeTariff.MouseCursor = Cursors.Default;
			this.cmbTypeTariff.Name = "cmbTypeTariff";
			this.cmbTypeTariff.RowDivider.Color = Color.DarkGray;
			this.cmbTypeTariff.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeTariff.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeTariff.Size = new System.Drawing.Size(400, 19);
			this.cmbTypeTariff.TabIndex = 9;
			this.cmbTypeTariff.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>245</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.dtTo.BorderStyle = 1;
			this.dtTo.FormatType = FormatTypeEnum.LongDate;
			this.dtTo.Location = new Point(368, 0);
			this.dtTo.Name = "dtTo";
			this.dtTo.Size = new System.Drawing.Size(184, 18);
			this.dtTo.TabIndex = 8;
			this.dtTo.Tag = null;
			this.dtTo.Value = new DateTime(2006, 10, 23, 0, 0, 0, 0);
			this.dtTo.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label9.Location = new Point(344, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(24, 16);
			this.label9.TabIndex = 7;
			this.label9.Text = "по:";
			this.dtFrom.BorderStyle = 1;
			this.dtFrom.FormatType = FormatTypeEnum.LongDate;
			this.dtFrom.Location = new Point(152, 0);
			this.dtFrom.Name = "dtFrom";
			this.dtFrom.Size = new System.Drawing.Size(184, 18);
			this.dtFrom.TabIndex = 6;
			this.dtFrom.Tag = null;
			this.dtFrom.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.label8.Location = new Point(8, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(152, 16);
			this.label8.TabIndex = 5;
			this.label8.Text = "Срок действия договора с:";
			this.lblSaldo.Cursor = Cursors.Hand;
			this.lblSaldo.FlatStyle = FlatStyle.Flat;
			this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 204);
			this.lblSaldo.Location = new Point(504, 112);
			this.lblSaldo.Name = "lblSaldo";
			this.lblSaldo.Size = new System.Drawing.Size(176, 16);
			this.lblSaldo.TabIndex = 8;
			this.lblSaldo.Text = "Долг:";
			this.lblSaldo.Click += new EventHandler(this.lblSaldo_Click);
			this.lblDisplay.FlatStyle = FlatStyle.Flat;
			this.lblDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblDisplay.Location = new Point(248, 112);
			this.lblDisplay.Name = "lblDisplay";
			this.lblDisplay.Size = new System.Drawing.Size(248, 16);
			this.lblDisplay.TabIndex = 9;
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(336, 464);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(116, 24);
			this.cmdOk.TabIndex = 11;
			this.cmdOk.Text = "Ok";
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(576, 464);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(116, 24);
			this.cmdCancel.TabIndex = 10;
			this.cmdCancel.Text = "Закрыть";
			this.chkDateFrom.Location = new Point(8, 0);
			this.chkDateFrom.Name = "chkDateFrom";
			this.chkDateFrom.Size = new System.Drawing.Size(232, 16);
			this.chkDateFrom.TabIndex = 9;
			this.chkDateFrom.Text = "checkBox1";
			this.cmdApply.FlatStyle = FlatStyle.Flat;
			this.cmdApply.Location = new Point(456, 464);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(116, 24);
			this.cmdApply.TabIndex = 12;
			this.cmdApply.Text = "Применить";
			this.cmdApply.Click += new EventHandler(this.cmdApply_Click);
			this.ckshet.Location = new Point(488, 88);
			this.ckshet.Name = "ckshet";
			this.ckshet.Size = new System.Drawing.Size(176, 16);
			this.ckshet.TabIndex = 13;
			this.ckshet.Text = "не печатать cчёт-извещение";
			this.ckshet.CheckedChanged += new EventHandler(this.ckshet_CheckedChanged);
			this.cmbTypeInfringements.AddItemSeparator = ';';
			this.cmbTypeInfringements.BorderStyle = 1;
			this.cmbTypeInfringements.Caption = "";
			this.cmbTypeInfringements.CaptionHeight = 17;
			this.cmbTypeInfringements.CharacterCasing = 0;
			this.cmbTypeInfringements.ColumnCaptionHeight = 17;
			this.cmbTypeInfringements.ColumnFooterHeight = 17;
			this.cmbTypeInfringements.ColumnHeaders = false;
			this.cmbTypeInfringements.ColumnWidth = 149;
			this.cmbTypeInfringements.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypeInfringements.ContentHeight = 15;
			this.cmbTypeInfringements.DataMode = DataModeEnum.AddItem;
			this.cmbTypeInfringements.DeadAreaBackColor = Color.Empty;
			this.cmbTypeInfringements.EditorBackColor = SystemColors.Window;
			this.cmbTypeInfringements.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypeInfringements.EditorForeColor = SystemColors.WindowText;
			this.cmbTypeInfringements.EditorHeight = 15;
			this.cmbTypeInfringements.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypeInfringements.Images.Add((Image)resourceManager.GetObject("resource5"));
			this.cmbTypeInfringements.ItemHeight = 15;
			this.cmbTypeInfringements.Location = new Point(104, 80);
			this.cmbTypeInfringements.MatchEntryTimeout = (long)2000;
			this.cmbTypeInfringements.MaxDropDownItems = 5;
			this.cmbTypeInfringements.MaxLength = 32767;
			this.cmbTypeInfringements.MouseCursor = Cursors.Default;
			this.cmbTypeInfringements.Name = "cmbTypeInfringements";
			this.cmbTypeInfringements.RowDivider.Color = Color.DarkGray;
			this.cmbTypeInfringements.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypeInfringements.RowSubDividerColor = Color.DarkGray;
			this.cmbTypeInfringements.Size = new System.Drawing.Size(208, 19);
			this.cmbTypeInfringements.TabIndex = 4;
			this.cmbTypeInfringements.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Width>149</Width><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.label12.Location = new Point(8, 80);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(96, 16);
			this.label12.TabIndex = 5;
			this.label12.Text = "Тип нарушения:";
			this.chkPeny.Location = new Point(488, 72);
			this.chkPeny.Name = "chkPeny";
			this.chkPeny.Size = new System.Drawing.Size(200, 16);
			this.chkPeny.TabIndex = 14;
			this.chkPeny.Text = "не начислять пеню";
			base.AcceptButton = this.cmdOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new System.Drawing.Size(696, 498);
			base.Controls.Add(this.chkPeny);
			base.Controls.Add(this.ckshet);
			base.Controls.Add(this.cmdApply);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.lblDisplay);
			base.Controls.Add(this.lblSaldo);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.cmbType);
			base.Controls.Add(this.cmbStatus);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.txtAccount);
			base.Controls.Add(this.cmbTypeInfringements);
			base.Controls.Add(this.label12);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "frmContract";
			this.Text = "Договор";
			base.Load += new EventHandler(this.frmContract_Load);
			base.Closed += new EventHandler(this.frmContract_Closed);
			((ISupportInitialize)this.cmbStatus).EndInit();
			((ISupportInitialize)this.cmbType).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.cmbGMeter).EndInit();
			this.panel2.ResumeLayout(false);
			((ISupportInitialize)this.cmbGObject).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.cmbTypeTariff).EndInit();
			((ISupportInitialize)this.dtTo).EndInit();
			((ISupportInitialize)this.dtFrom).EndInit();
			((ISupportInitialize)this.cmbTypeInfringements).EndInit();
			base.ResumeLayout(false);
		}

		private void lblSaldo_Click(object sender, EventArgs e)
		{
			(new frmBalance(this._contract)).ShowDialog(this);
		}

		private bool Save()
		{
			bool flag;
			bool flag1 = false;
			try
			{
				if (this.txtAccount.Enabled)
				{
					if (this.txtAccount.Text.Length < 4 || this.txtAccount.Text.Length > 10)
					{
						MessageBox.Show("Не правильный формат лицевого счета", "Сохранение договора", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						flag = flag1;
						return flag;
					}
					else
					{
						this._contract.Account = this.txtAccount.Text;
					}
				}
				if (this.cmbStatus.SelectedIndex < 0)
				{
					this.cmbStatus.SelectedIndex = 0;
				}
				else
				{
					this._contract.Status = this.cmbStatus.SelectedIndex;
				}
				this._contract.oTypeContract = this._typecontracts[this.cmbType.SelectedIndex];
				this._contract.oTypeInfringements = this._TypeInfringementss[this.cmbTypeInfringements.SelectedIndex];
				this._contract.oTypeTariff = this._typetariffs[this.cmbTypeTariff.SelectedIndex];
				this._contract.DateBegin = (DateTime)this.dtFrom.Value;
				this._contract.DateEnd = (DateTime)this.dtTo.Value;
				this._contract.Memo = this.txtMemo.Text;
				if (!this.ckshet.Checked)
				{
					this._contract.PrintChetIzvehen = 0;
				}
				else
				{
					this._contract.PrintChetIzvehen = 1;
				}
				if (!this.chkPeny.Checked)
				{
					this._contract.ChargePeny = 1;
				}
				else
				{
					this._contract.ChargePeny = 0;
				}
				if (this._contract.oPerson == null)
				{
					MessageBox.Show("Нет потребителя!", "Сохранение договора", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					flag = flag1;
				}
				else if (this._contract.oTypeTariff != null)
				{
					flag1 = this._contract.Save() == 0;
					return flag1;
				}
				else
				{
					MessageBox.Show("Не выбран тип тарифа!", "Сохранение договора", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					flag = flag1;
				}
			}
			catch
			{
				MessageBox.Show("Ошибка сохранения!", "Сохранение договора", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				flag = flag1;
			}
			return flag;
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this._contract.get_isNew())
			{
				if (this.tabControl1.SelectedIndex == 1)
				{
					this._Docs = this._contract.oDocumentss(this.DateBegin, this.DateEnd);
				}
				this.CreateDocuments();
				if (this.tabControl1.SelectedIndex == 2)
				{
					this.CreateParameters();
				}
			}
			else if (this.tabControl1.SelectedIndex != 2)
			{
				this.tabControl1.SelectedIndex = 2;
				return;
			}
		}

		private void tbCorrectNach_Click(object sender, ClickEventArgs e)
		{
			Document document = this._contract.oDocuments.Add();
			document.oTypeDocument = Depot.oTypeDocuments.item((long)7);
			(new frmChangeCharge(document)).ShowDialog(this);
			document = null;
			this.CreateDocuments();
			this.CreateIndication();
			this.CreateFactUse();
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			frmIndication _frmIndication = null;
			try
			{
				Gobject item = this._contract.oGobjects[this.cmbGObject.SelectedIndex];
				"Depth";
				string str = e.Button.Tag.ToString();
				string str1 = str;
				if (str != null)
				{
					str1 = string.IsInterned(str1);
					if ((object)str1 == (object)"Add")
					{
						if (item.oActiveGmeter != null && item.oActiveGmeter.get_ID() == item.oGmeters[this.cmbGMeter.SelectedIndex].get_ID())
						{
							if ((new frmIndication(item.oActiveGmeter.oIndications.Add())).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
							{
								this.CreateIndication();
								this.CreateFactUse();
							}
							_frmIndication = null;
						}
					}
					else if ((object)str1 == (object)"Edit")
					{
						if (item.oActiveGmeter != null && item.oActiveGmeter.get_ID() == item.oGmeters[this.cmbGMeter.SelectedIndex].get_ID())
						{
							if ((new frmIndication(item.oActiveGmeter.oIndications[0])).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
							{
								this.CreateIndication();
								this.CreateFactUse();
							}
							_frmIndication = null;
						}
					}
					else if ((object)str1 == (object)"Del")
					{
						if (item.oActiveGmeter != null && item.oActiveGmeter.get_ID() == item.oGmeters[this.cmbGMeter.SelectedIndex].get_ID())
						{
							Indication indication = item.oActiveGmeter.oIndications[0];
							if (indication.oFactUses.get_Count() != 0 && (indication.oFactUses[0].oDocument == null || indication.oFactUses[0].oDocument.oTypeDocument.get_ID() == (long)1))
							{
								if (MessageBox.Show("Вы действительно хотите удалить последние показания?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
								{
									item.oActiveGmeter.oIndications.Remove(indication.get_ID());
									this.CreateIndication();
									this.CreateFactUse();
								}
								indication = null;
							}
						}
					}
					else if ((object)str1 == (object)"Excel")
					{
						Tools.ConvertToExcel(this.lvDisplay);
					}
					else if ((object)str1 == (object)"Depth")
					{
						frmDepthView _frmDepthView = new frmDepthView();
						_frmDepthView.SetDate(this.DateBeginI, this.DateEndI);
						if (_frmDepthView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							_frmDepthView.GetDate(ref this.DateBeginI, ref this.DateEndI);
							if (this.DateBeginI < new DateTime(2005, 4, 1))
							{
								this.DateBeginI = new DateTime(2005, 4, 1);
							}
							this.CreateIndication();
							this.CreateFactUse();
						}
						_frmIndication = null;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(exception.Message, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void tbDel_Click()
		{
			if (this.lvDoc.SelectedItems.Count > 0 && MessageBox.Show("Удалить текущий документ?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				if (this._contract.oDocuments.item(Convert.ToInt64(this.lvDoc.SelectedItems[0].Tag)).oPeriod.get_ID() != Depot.CurrentPeriod.get_ID())
				{
					MessageBox.Show("Ошибка удаления объекта! Документ за прошлый период!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				long d = this._contract.oDocuments.item(Convert.ToInt64(this.lvDoc.SelectedItems[0].Tag)).oTypeDocument.get_ID();
				if (d != (long)18 && d != (long)12)
				{
					return;
				}
				Document document = this._contract.oDocuments.item(Convert.ToInt64(this.lvDoc.SelectedItems[0].Tag));
				Gmeter gmeter = new Gmeter();
				if (document.GetPD(7) != null && gmeter.Load(Convert.ToInt64(document.GetPD(7).Value)) != 0)
				{
					MessageBox.Show("Ошибка удаления данных!", "Удаление данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (this._contract.oDocuments.Remove(Convert.ToInt64(this.lvDoc.SelectedItems[0].Tag)) == 0)
				{
					if (d == (long)18)
					{
						gmeter.oStatusGMeter = Depot.oStatusGMeters.item((long)1);
					}
					if (d == (long)12)
					{
						gmeter.oStatusGMeter = Depot.oStatusGMeters.item((long)2);
					}
					if (gmeter.Save() != 0)
					{
						MessageBox.Show("Ошибка удаления данных!", "Удаление данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					this.CreateDocuments();
					this.CreateIndication();
					this.CreateFactUse();
					return;
				}
				MessageBox.Show("Ошибка удаления документа!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void tbDocExcel_Click(object sender, ClickEventArgs e)
		{
			Tools.ConvertToExcel(this.lvDoc);
		}

		private void tbParam_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"Excel";
			string str = e.Button.Tag.ToString();
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if ((object)str1 == (object)"Add")
				{
					!this._contract.get_isNew();
				}
				else if ((object)str1 != (object)"Edit" && (object)str1 != (object)"Del" && (object)str1 != (object)"Excel")
				{
					return;
				}
			}
		}

		private void tbRepaymentDept_Click(object sender, ClickEventArgs e)
		{
			frmRepaymentDebt _frmRepaymentDebt = new frmRepaymentDebt(this._contract.oGobjects[0]);
			_frmRepaymentDebt.ShowDialog(this);
			_frmRepaymentDebt = null;
			this.CreateDocuments();
		}

		private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"Depth";
			string str = e.Button.Tag.ToString();
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if ((object)str1 != (object)"Add" && (object)str1 != (object)"Edit")
				{
					if ((object)str1 == (object)"Del")
					{
						this.tbDel_Click();
						return;
					}
					if ((object)str1 == (object)"Excel")
					{
						Tools.ConvertToExcel(this.lvDoc);
						return;
					}
					if ((object)str1 != (object)"Depth")
					{
						return;
					}
					frmDepthView _frmDepthView = new frmDepthView();
					_frmDepthView.SetDate(this.DateBegin, this.DateEnd);
					if (_frmDepthView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						_frmDepthView.GetDate(ref this.DateBegin, ref this.DateEnd);
						this._Docs = this._contract.oDocumentss(this.DateBegin, this.DateEnd);
						this.CreateDocuments();
					}
					_frmDepthView = null;
				}
			}
		}
	}
}