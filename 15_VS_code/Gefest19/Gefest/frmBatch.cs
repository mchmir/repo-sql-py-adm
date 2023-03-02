using C1.Win.C1Input;
using C1.Win.C1List;
using C1.Win.C1List.Util;
using FprnM1C;
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
	public class frmBatch : Form
	{
		private ToolTip toolTip1;

		private StatusBar statusBar1;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private StatusBarPanel statusBarPanel3;

		private GroupBox groupBox4;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private GroupBox groupBox1;

		private Label label3;

		private Label label2;

		private Label label1;

		private IContainer components;

		private ImageList imageList1;

		private GroupBox groupBox3;

		private NumericUpDown numNewIndication;

		private NumericUpDown numAmount;

		private Button cmdApply1;

		private Label lblCurrentIndication;

		private Label label18;

		private Label label19;

		private Label label20;

		private GroupBox groupBox2;

		private Button cmdAccount;

		private Label lblCountLives;

		private Label lblPU;

		private Label label13;

		private Label lblAddress;

		private Label lblFIO;

		private Label label10;

		private Label label11;

		private Label label12;

		private NumericUpDown numBatchCount;

		private ListView lv;

		private NumericUpDown numBatchAmount;

		private Button cmdCloseBatch;

		private Button cmdSaveBatch;

		private C1TextBox txtAccount;

		private ImageList imageList2;

		private Panel panel1;

		private ToolBar tbData;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton23;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private IFprnM45 ECR;

		private Contract _contract;

		private Batch _batch;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Agents _Agents;

		private Address _address;

		private double FactAmount = 0;

		private double docsAmount = 0;

		private int docsCount = 0;

		private bool NeedPrintKKM;

		private Document doc;

		private PD _pd1;

		private Indication _indication;

		private bool dialogresult;

		private Label lblBatchDate;

		private Label label5;

		private Label label4;

		private C1Combo cmbAgents;

		private C1TextBox txtNumberBatch;

		private Button bEnd;

		private CheckBox chkKKM;

		private Button button1;

		private ToolBarButton toolBarButton1;

		private CheckBox chkUslugi;

		private int ind = 0;

		public frmBatch(Batch oBatch, bool NeedPrint, IFprnM45 ECR1)
		{
			this.InitializeComponent();
			this._batch = oBatch;
			this.NeedPrintKKM = NeedPrint;
			if (!NeedPrint)
			{
				this.bEnd.Visible = false;
				this.chkKKM.Visible = false;
				return;
			}
			this.ECR = ECR1;
			this.bEnd.Visible = true;
			this.chkKKM.Visible = true;
			this.txtAccount.Focus();
			this.numBatchAmount.Enabled = false;
			this.numBatchCount.Enabled = false;
			this.cmbAgents.Enabled = false;
		}

		private void bEnd_Click(object sender, EventArgs e)
		{
			if (Math.Abs(this._batch.BatchAmount - this.docsAmount) > 0.01 || this._batch.BatchCount != this.docsCount)
			{
				MessageBox.Show("Сумма и количество (платежей) пачки должно сходиться с суммой и количеством внесенных платежей!", "Закрытие пачки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.dialogresult = false;
				return;
			}
			this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)2);
			if (this._batch.Save() == 0)
			{
				this.dialogresult = true;
			}
			base.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			frmAddress _frmAddress = new frmAddress()
			{
				oAddress = this._address
			};
			this.ResetFields1();
			_frmAddress.ShowDialog(this);
			if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				Address address = _frmAddress.oAddress;
				Gobjects gobject = new Gobjects();
				if (gobject.Load(address) != 0)
				{
					return;
				}
				if (gobject.get_Count() <= 0)
				{
					return;
				}
				this._gobject = gobject[0];
				this._contract = this._gobject.oContract;
				this.txtAccount.Text = this._contract.Account;
				this._address = address;
				address = null;
				this.cmdAccount_Click(null, null);
			}
			_frmAddress = null;
		}

		private void CheckClosed()
		{
			if (this._batch.oStatusBatch.get_ID() == (long)1)
			{
				this.cmdCloseBatch.Text = "Закрыть";
				this.groupBox2.Enabled = true;
				this.groupBox3.Enabled = true;
				this.tbData.Enabled = true;
				this.cmdSaveBatch.Enabled = true;
				return;
			}
			this.cmdCloseBatch.Text = "Открыть";
			this.groupBox2.Enabled = false;
			this.groupBox3.Enabled = false;
			this.toolBarButton16.Enabled = false;
			this.toolBarButton23.Enabled = false;
			this.toolBarButton26.Enabled = true;
			this.cmdSaveBatch.Enabled = false;
		}

		private void cmdAccount_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.txtAccount.Text.Length != 0)
				{
					Contracts contract = new Contracts();
					if (contract.Load(this.txtAccount.Text.Trim()) != 0)
					{
						this.ResetFields1();
					}
					else if (contract.get_Count() <= 0)
					{
						this.ResetFields1();
					}
					else
					{
						this._contract = contract[0];
						this.lblFIO.Text = this._contract.oPerson.FullName;
						this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
						Label str = this.lblCountLives;
						int countLives = this._contract.oGobjects[0].CountLives;
						str.Text = countLives.ToString();
						this._gobject = this._contract.oGobjects[0];
						this._gmeter = this._gobject.GetActiveGMeter();
						if (this._gmeter != null)
						{
							this.lblPU.Text = "Подключен";
							Label label = this.lblCurrentIndication;
							string str1 = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
							DateTime datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
							label.Text = string.Concat(str1, " от ", datedisplay.ToShortDateString());
							this.numNewIndication.Value = new decimal(0);
							this.numNewIndication.Enabled = true;
							this._indication = this._gmeter.oIndications.Add();
						}
						else
						{
							this.lblPU.Text = "Не подключен";
							this.lblCurrentIndication.Text = "";
							this.numNewIndication.Value = new decimal(0);
							this.numNewIndication.Enabled = false;
							this._indication = null;
						}
						this.numAmount.Value = new decimal(0);
						this.numAmount.Focus();
					}
				}
				else
				{
					this.ResetFields1();
				}
			}
			catch
			{
			}
		}

		private void cmdApply1_Click(object sender, EventArgs e)
		{
			Document currentPeriod;
			Document batchDate;
			string[] str;
			long d;
			if (this.NeedPrintKKM)
			{
				try
				{
					try
					{
						this.Cursor = Cursors.WaitCursor;
						if (this._contract == null)
						{
							return;
						}
						else if (this.numAmount.Value != new decimal(0))
						{
							this.numNewIndication_Leave(null, null);
							if (this.FactAmount < 0)
							{
								MessageBox.Show("Не верно указаны показания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								this.numNewIndication.Focus();
								return;
							}
							else if ((this.FactAmount > 10 || this.FactAmount == 0) && this.numNewIndication.Value > new decimal(0) && MessageBox.Show(string.Concat("Потребление составляет ", this.FactAmount.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
							{
								this.numNewIndication.Focus();
								return;
							}
							else
							{
								long num = (long)0;
								if (this._indication != null)
								{
									num = this._indication.get_ID();
								}
								long d1 = (long)0;
								string documentNumber = "";
								string note = "";
								bool flag = false;
								bool flag1 = false;
								if (this.doc != null)
								{
									batchDate = this.doc;
									d1 = this.doc.get_ID();
									documentNumber = this.doc.DocumentNumber;
									note = this.doc.Note;
								}
								else
								{
									batchDate = this._batch.oDocuments.Add();
									batchDate.oContract = this._contract;
									batchDate.oPeriod = Depot.CurrentPeriod;
									batchDate.oTypeDocument = Depot.oTypeDocuments.item((long)1);
									batchDate.DocumentDate = this._batch.BatchDate;
									Batch batchCount = this._batch;
									batchCount.BatchCount = batchCount.BatchCount + 1;
									str = new string[2];
									d = this._batch.get_ID();
									str[0] = d.ToString();
									str[1] = "0";
									Saver.ExecuteFunction("fShowNumberBatch", str);
									documentNumber = (documentNumber == null ? "1" : documentNumber.ToString());
								}
								double num1 = 0;
								int num2 = this._contract.oPerson.isJuridical;
								string rNN = this._contract.oPerson.RNN;
								if (!batchDate.DocumentPay(this._contract.oPerson.isJuridical, Depot.CurrentPeriod.get_ID(), this._batch.get_ID(), this._contract.get_ID(), (long)1, this._batch.BatchDate, Convert.ToDouble(this.numAmount.Value), Convert.ToDouble(this.numNewIndication.Value), flag1, SQLConnect.CurrentUser.get_ID(), ref d1, ref num, ref this.FactAmount, ref documentNumber, ref note, ref flag, ref num1))
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									this.dialogresult = true;
									if (num == (long)0)
									{
										batchDate.oPDs = null;
									}
									batchDate.set_ID(d1);
									if (this.doc != null)
									{
										this.docsAmount -= batchDate.DocumentAmount;
										batchDate.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
										this.FillOneItem(batchDate, false);
									}
									else
									{
										batchDate.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
										this._batch.oDocuments.Add(batchDate);
										this.FillOneItem(batchDate, true);
									}
									this.FillStatusBar();
									if (num2 == 1)
									{
										if (num1 <= 0)
										{
											str = new string[] { "ИИН ", rNN, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1211", string.Concat(str), batchDate, "за газ", batchDate.DocumentAmount);
										}
										else
										{
											str = new string[] { "ИИН ", rNN, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1211", string.Concat(str), batchDate, "за газ", batchDate.DocumentAmount - num1);
											MessageBox.Show("Распечатайте ПКО за газ и нажмите ОК.");
											str = new string[] { "ИИН ", rNN, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1212", string.Concat(str), batchDate, "за услуги", num1);
											MessageBox.Show("Распечатайте ПКО за услуги и нажмите ОК.");
										}
									}
									this.doc = null;
									this._indication = null;
									this.lv.Items[this.ind].BackColor = SystemColors.Window;
									this.txtAccount.Focus();
									if (this.chkKKM.Checked)
									{
										if (batchDate.DocumentAmount <= 0)
										{
											this.PrintKKMVozvrat(batchDate);
										}
										else
										{
											this.PrintKKM(batchDate);
										}
									}
								}
							}
						}
						else
						{
							this.numAmount.Focus();
							return;
						}
					}
					catch
					{
					}
				}
				finally
				{
					this.Cursor = Cursors.Default;
				}
			}
			else
			{
				try
				{
					try
					{
						this.Cursor = Cursors.WaitCursor;
						if (this._contract == null)
						{
							return;
						}
						else if (this.numAmount.Value != new decimal(0))
						{
							this.numNewIndication_Leave(null, null);
							if (this.FactAmount < 0)
							{
								MessageBox.Show("Не верно указаны показания!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								this.numNewIndication.Focus();
								return;
							}
							else if ((this.FactAmount > 10 || this.FactAmount == 0) && this.numNewIndication.Value > new decimal(0) && MessageBox.Show(string.Concat("Потребление составляет ", this.FactAmount.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
							{
								this.numNewIndication.Focus();
								return;
							}
							else
							{
								long d2 = (long)0;
								if (this._indication != null)
								{
									d2 = this._indication.get_ID();
								}
								long d3 = (long)0;
								string documentNumber1 = "";
								string note1 = "";
								bool flag2 = false;
								bool flag3 = false;
								if (this.doc != null)
								{
									currentPeriod = this.doc;
									d3 = this.doc.get_ID();
									documentNumber1 = this.doc.DocumentNumber;
									note1 = this.doc.Note;
								}
								else
								{
									currentPeriod = this._batch.oDocuments.Add();
									currentPeriod.oContract = this._contract;
									currentPeriod.oPeriod = Depot.CurrentPeriod;
									currentPeriod.oTypeDocument = Depot.oTypeDocuments.item((long)1);
									currentPeriod.DocumentDate = this._batch.BatchDate;
									Batch batch = this._batch;
									batch.BatchCount = batch.BatchCount + 1;
									str = new string[2];
									d = this._batch.get_ID();
									str[0] = d.ToString();
									str[1] = "0";
									Saver.ExecuteFunction("fShowNumberBatch", str);
									documentNumber1 = (documentNumber1 == null ? "1" : documentNumber1.ToString());
									currentPeriod.DocumentNumber = documentNumber1;
								}
								if (this._batch.oTypeBatch.get_ID() == (long)2)
								{
									flag3 = true;
								}
								double num3 = 0;
								if (!this.chkUslugi.Checked)
								{
									if (!currentPeriod.DocumentPay(this._contract.oPerson.isJuridical, Depot.CurrentPeriod.get_ID(), this._batch.get_ID(), this._contract.get_ID(), (long)1, this._batch.BatchDate, Convert.ToDouble(this.numAmount.Value), Convert.ToDouble(this.numNewIndication.Value), flag3, SQLConnect.CurrentUser.get_ID(), ref d3, ref d2, ref this.FactAmount, ref documentNumber1, ref note1, ref flag2, ref num3))
									{
										MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									}
									else
									{
										this.dialogresult = true;
										if (d2 == (long)0)
										{
											currentPeriod.oPDs = null;
										}
										currentPeriod.set_ID(d3);
										if (this.doc != null)
										{
											this.docsAmount -= currentPeriod.DocumentAmount;
											currentPeriod.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
											this.FillOneItem(currentPeriod, false);
										}
										else
										{
											currentPeriod.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
											this._batch.oDocuments.Add(currentPeriod);
											this.FillOneItem(currentPeriod, true);
										}
										this.FillStatusBar();
										this.doc = null;
										this._indication = null;
										this.lv.Items[this.ind].BackColor = SystemColors.Window;
										this.txtAccount.Focus();
									}
								}
								else if (!currentPeriod.DocumentPayVDGO(this._contract.oPerson.isJuridical, Depot.CurrentPeriod.get_ID(), this._batch.get_ID(), this._contract.get_ID(), (long)1, this._batch.BatchDate, Convert.ToDouble(this.numAmount.Value), Convert.ToDouble(this.numNewIndication.Value), flag3, SQLConnect.CurrentUser.get_ID(), ref d3, ref d2, ref this.FactAmount, ref documentNumber1, ref note1, ref flag2))
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									this.dialogresult = true;
									if (d2 == (long)0)
									{
										currentPeriod.oPDs = null;
									}
									currentPeriod.set_ID(d3);
									if (this.doc != null)
									{
										this.docsAmount -= currentPeriod.DocumentAmount;
										currentPeriod.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
										this.FillOneItem(currentPeriod, false);
									}
									else
									{
										currentPeriod.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
										this._batch.oDocuments.Add(currentPeriod);
										this.FillOneItem(currentPeriod, true);
									}
									this.FillStatusBar();
									this.doc = null;
									this._indication = null;
									this.lv.Items[this.ind].BackColor = SystemColors.Window;
									this.txtAccount.Focus();
								}
							}
						}
						else
						{
							this.numAmount.Focus();
							return;
						}
					}
					catch
					{
					}
				}
				finally
				{
					this.Cursor = Cursors.Default;
				}
			}
		}

		private void cmdCloseBatch_Click(object sender, EventArgs e)
		{
			if (this._batch.oStatusBatch.get_ID() == (long)1)
			{
				if (Math.Abs(this._batch.BatchAmount - this.docsAmount) > 0.01 || this._batch.BatchCount != this.docsCount)
				{
					MessageBox.Show("Сумма и количество (платежей) пачки должно сходиться с суммой и количеством внесенных платежей!", "Закрытие пачки", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)2);
				if (this._batch.Save() == 0)
				{
					this.dialogresult = true;
				}
			}
			else if (this._batch.oPeriod.get_ID() == Depot.CurrentPeriod.get_ID() && MessageBox.Show("Вы хотите открыть пачку?", "Attention!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)1);
				if (this._batch.Save() == 0)
				{
					this.dialogresult = true;
				}
			}
			this.CheckClosed();
		}

		private void cmdSaveBatch_Click(object sender, EventArgs e)
		{
			this._batch.BatchCount = Convert.ToInt32(this.numBatchCount.Value);
			this._batch.BatchAmount = Convert.ToDouble(this.numBatchAmount.Value);
			this._batch.NumberBatch = this.txtNumberBatch.Text;
			this._batch.oDispatcher = this._Agents[this.cmbAgents.SelectedIndex];
			if (this._batch.Save() == 0)
			{
				this.dialogresult = true;
			}
		}

		private void CreateAgent(long IdSelected)
		{
			try
			{
				this.cmbAgents.ClearItems();
				int num = 0;
				this._Agents = new Agents();
				this._Agents.Load();
				for (int i = 0; i < this._Agents.get_Count(); i++)
				{
					this.cmbAgents.AddItem(this._Agents[i].get_Name());
					if (this._Agents[i].get_ID() == IdSelected)
					{
						num = i;
					}
				}
				if (this.cmbAgents.ListCount > num)
				{
					this.cmbAgents.SelectedIndex = num;
				}
				this.cmbAgents.ColumnWidth = this.cmbAgents.Width - this.cmbAgents.Height;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.cmbAgents.Text = string.Concat("Ошибка загрузки справочника ", exception.Message);
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

		private void EditDoc()
		{
			if (this._batch.oStatusBatch.get_ID() == (long)1 && this.lv.SelectedItems.Count > 0)
			{
				this.txtAccount.Text = this.lv.SelectedItems[0].SubItems[0].Text;
				this.cmdAccount_Click(null, null);
				this.doc = this._batch.oDocuments.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
				this.lv.Items[this.ind].BackColor = SystemColors.Window;
				this.ind = this.lv.SelectedItems[0].Index;
				this.lv.SelectedItems[0].BackColor = SystemColors.Info;
				this.numAmount.Value = Convert.ToDecimal(this.doc.DocumentAmount);
				this.numAmount.Focus();
				if (this.doc.GetPD(1) != null)
				{
					this._pd1 = this.doc.GetPD(1);
					this._indication = new Indication();
					this._indication.Load(Convert.ToInt64(this._pd1.Value));
					this.numNewIndication.Value = Convert.ToDecimal(this._indication.Display);
				}
			}
		}

		private void FillListView()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.docsAmount = 0;
				this.docsCount = 0;
				foreach (Document oDocument in this._batch.oDocuments)
				{
					ListViewItem listViewItem = new ListViewItem(oDocument.oContract.Account)
					{
						Tag = oDocument.get_ID().ToString()
					};
					listViewItem.SubItems.Add(oDocument.oContract.oPerson.FullName);
					if (oDocument.GetPD(1) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Indication indication = new Indication();
						indication.Load(Convert.ToInt64(oDocument.GetPD(1).Value));
						listViewItem.SubItems.Add(Convert.ToString(indication.Display));
					}
					listViewItem.SubItems.Add(Convert.ToString(oDocument.DocumentAmount));
					listViewItem.SubItems.Add(oDocument.oTypeDocument.get_Name());
					this.docsAmount += oDocument.DocumentAmount;
					this.docsCount++;
					this.lv.Items.Add(listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
			catch (Exception exception)
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillOneItem(Document o, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (!isAdd)
				{
					this.docsAmount += o.DocumentAmount;
					this.lv.SelectedItems[0].SubItems[0].Text = this._contract.Account;
					this.lv.SelectedItems[0].SubItems[1].Text = this._contract.oPerson.FullName;
					if (o.GetPD(1) == null)
					{
						this.lv.SelectedItems[0].SubItems[2].Text = "нет";
					}
					else
					{
						Indication indication = new Indication();
						indication.Load(Convert.ToInt64(o.GetPD(1).Value));
						this.lv.SelectedItems[0].SubItems[2].Text = Convert.ToString(indication.Display);
					}
					this.lv.SelectedItems[0].SubItems[3].Text = Convert.ToString(o.DocumentAmount);
					this.lv.SelectedItems[0].SubItems[4].Text = o.oTypeDocument.get_Name();
				}
				else
				{
					this.docsAmount += o.DocumentAmount;
					this.docsCount++;
					ListViewItem listViewItem = new ListViewItem(this._contract.Account)
					{
						Tag = o.get_ID().ToString()
					};
					listViewItem.SubItems.Add(this._contract.oPerson.FullName);
					if (o.GetPD(1) == null)
					{
						listViewItem.SubItems.Add("нет");
					}
					else
					{
						Indication indication1 = new Indication();
						indication1.Load(Convert.ToInt64(o.GetPD(1).Value));
						listViewItem.SubItems.Add(Convert.ToString(indication1.Display));
					}
					listViewItem.SubItems.Add(Convert.ToString(o.DocumentAmount));
					listViewItem.SubItems.Add(o.oTypeDocument.get_Name());
					this.lv.Items.Add(listViewItem);
				}
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillStatusBar()
		{
			try
			{
				this.statusBar1.Panels[0].Text = string.Concat("Документов на сумму: ", Convert.ToString(this.docsAmount));
				this.statusBar1.Panels[1].Text = string.Concat("Внесено документов: ", Convert.ToString(this.docsCount));
				if (this._batch.BatchAmount - this.docsAmount <= 0.01)
				{
					this.statusBar1.Panels[2].Text = string.Concat("Баланс: разница по сумме 0 ,разница в количестве ", Convert.ToString(this._batch.BatchCount - this.docsCount));
				}
				else
				{
					this.statusBar1.Panels[2].Text = string.Concat("Баланс: разница по сумме ", Convert.ToString(this._batch.BatchAmount - this.docsAmount), " ,разница в количестве ", Convert.ToString(this._batch.BatchCount - this.docsCount));
				}
			}
			catch
			{
			}
		}

		private void frmBatch_Closing(object sender, CancelEventArgs e)
		{
			if (this.dialogresult)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			Tools.SaveWindows(this);
			this._batch = null;
			this._contract = null;
			this._gmeter = null;
			this._gobject = null;
			this._indication = null;
			this._pd1 = null;
		}

		private void frmBatch_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				if (this._batch.oTypeBatch.get_ID() != (long)1 && this._batch.oTypeBatch.get_ID() != (long)2)
				{
					base.Close();
				}
				else if (this._batch.oTypeBatch.get_ID() != (long)2 || this._batch.oStatusBatch.get_ID() != (long)1)
				{
					this.numBatchCount.Value = Convert.ToDecimal(this._batch.BatchCount);
					this.numBatchAmount.Value = Convert.ToDecimal(this._batch.BatchAmount);
					this.txtNumberBatch.Text = this._batch.NumberBatch;
					this.txtNumberBatch.Enabled = false;
					if (this._batch.oDispatcher != null)
					{
						this.CreateAgent(this._batch.oDispatcher.get_ID());
					}
					this.lblBatchDate.Text = this._batch.BatchDate.ToShortDateString();
					this.FillListView();
					this.FillStatusBar();
					this.CheckClosed();
					if (this._batch.oTypeBatch.get_ID() == (long)2)
					{
						this.groupBox1.Enabled = false;
						this.groupBox2.Enabled = false;
						this.groupBox3.Enabled = false;
						this.toolBarButton16.Enabled = false;
						this.toolBarButton23.Enabled = false;
					}
					if (this._batch.oTypeBatch.get_ID() == (long)1)
					{
						this.txtNumberBatch.BackColor = Color.FromName("HighlightText");
						this.txtNumberBatch.Enabled = true;
					}
				}
				else
				{
					base.Close();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmBatch));
			this.toolTip1 = new ToolTip(this.components);
			this.lblPU = new Label();
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.statusBarPanel3 = new StatusBarPanel();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.groupBox4 = new GroupBox();
			this.panel1 = new Panel();
			this.tbData = new ToolBar();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton23 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.toolBarButton1 = new ToolBarButton();
			this.imageList2 = new ImageList(this.components);
			this.groupBox1 = new GroupBox();
			this.cmbAgents = new C1Combo();
			this.lblBatchDate = new Label();
			this.label5 = new Label();
			this.cmdCloseBatch = new Button();
			this.cmdSaveBatch = new Button();
			this.numBatchCount = new NumericUpDown();
			this.numBatchAmount = new NumericUpDown();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.label4 = new Label();
			this.txtNumberBatch = new C1TextBox();
			this.imageList1 = new ImageList(this.components);
			this.groupBox3 = new GroupBox();
			this.chkKKM = new CheckBox();
			this.bEnd = new Button();
			this.numNewIndication = new NumericUpDown();
			this.numAmount = new NumericUpDown();
			this.cmdApply1 = new Button();
			this.lblCurrentIndication = new Label();
			this.label18 = new Label();
			this.label19 = new Label();
			this.label20 = new Label();
			this.groupBox2 = new GroupBox();
			this.button1 = new Button();
			this.txtAccount = new C1TextBox();
			this.cmdAccount = new Button();
			this.lblCountLives = new Label();
			this.label13 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.chkUslugi = new CheckBox();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			((ISupportInitialize)this.statusBarPanel3).BeginInit();
			this.groupBox4.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.cmbAgents).BeginInit();
			((ISupportInitialize)this.numBatchCount).BeginInit();
			((ISupportInitialize)this.numBatchAmount).BeginInit();
			((ISupportInitialize)this.txtNumberBatch).BeginInit();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.numNewIndication).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.txtAccount).BeginInit();
			base.SuspendLayout();
			this.lblPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(160, 88);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(120, 20);
			this.lblPU.TabIndex = 3;
			this.toolTip1.SetToolTip(this.lblPU, "Статус ПУ");
			this.statusBar1.Location = new Point(0, 464);
			this.statusBar1.Name = "statusBar1";
			StatusBar.StatusBarPanelCollection panels = this.statusBar1.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.statusBarPanel1, this.statusBarPanel2, this.statusBarPanel3 };
			panels.AddRange(statusBarPanelArray);
			this.statusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(728, 24);
			this.statusBar1.TabIndex = 3;
			this.statusBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Документов на сумму:";
			this.statusBarPanel1.Width = 134;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Text = "Внесено документов:";
			this.statusBarPanel2.Width = 127;
			this.statusBarPanel3.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel3.Text = "Баланс:";
			this.statusBarPanel3.Width = 451;
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader4, this.columnHeader5, this.columnHeader6 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(304, 48);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(416, 403);
			this.lv.TabIndex = 2;
			this.lv.View = View.Details;
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.lv.KeyUp += new KeyEventHandler(this.lv_KeyUp);
			this.columnHeader1.Text = "Л/с";
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 100;
			this.columnHeader4.Text = "Нов. пок.";
			this.columnHeader5.Text = "Сумма";
			this.columnHeader6.Text = "Тип";
			this.columnHeader6.Width = 70;
			this.groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.groupBox4.Controls.Add(this.panel1);
			this.groupBox4.ForeColor = SystemColors.Desktop;
			this.groupBox4.Location = new Point(300, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(424, 451);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Принятые платежи";
			this.panel1.Controls.Add(this.tbData);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(418, 432);
			this.panel1.TabIndex = 9;
			this.tbData.Appearance = ToolBarAppearance.Flat;
			this.tbData.AutoSize = false;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton16, this.toolBarButton23, this.toolBarButton25, this.toolBarButton26, this.toolBarButton1 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.Divider = false;
			this.tbData.DropDownArrows = true;
			this.tbData.ImageList = this.imageList2;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(418, 24);
			this.tbData.TabIndex = 1;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.toolBarButton16.ImageIndex = 1;
			this.toolBarButton16.Tag = "Edit";
			this.toolBarButton16.ToolTipText = "Редактировать";
			this.toolBarButton23.ImageIndex = 2;
			this.toolBarButton23.Tag = "Del";
			this.toolBarButton23.ToolTipText = "Удалить";
			this.toolBarButton25.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton26.ImageIndex = 3;
			this.toolBarButton26.Tag = "Excel";
			this.toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.toolBarButton1.Enabled = false;
			this.toolBarButton1.ImageIndex = 2;
			this.toolBarButton1.Tag = "DelAll";
			this.toolBarButton1.ToolTipText = "Удалить все документы из пачки";
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.cmbAgents);
			this.groupBox1.Controls.Add(this.lblBatchDate);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cmdCloseBatch);
			this.groupBox1.Controls.Add(this.cmdSaveBatch);
			this.groupBox1.Controls.Add(this.numBatchCount);
			this.groupBox1.Controls.Add(this.numBatchAmount);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtNumberBatch);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(4, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 176);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Информация по ведомости";
			this.cmbAgents.AddItemSeparator = ';';
			this.cmbAgents.BorderStyle = 1;
			this.cmbAgents.Caption = "";
			this.cmbAgents.CaptionHeight = 17;
			this.cmbAgents.CharacterCasing = 0;
			this.cmbAgents.ColumnCaptionHeight = 17;
			this.cmbAgents.ColumnFooterHeight = 17;
			this.cmbAgents.ColumnHeaders = false;
			this.cmbAgents.ColumnWidth = 100;
			this.cmbAgents.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbAgents.ContentHeight = 15;
			this.cmbAgents.DataMode = DataModeEnum.AddItem;
			this.cmbAgents.DeadAreaBackColor = Color.Empty;
			this.cmbAgents.EditorBackColor = SystemColors.Window;
			this.cmbAgents.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAgents.EditorForeColor = SystemColors.WindowText;
			this.cmbAgents.EditorHeight = 15;
			this.cmbAgents.FlatStyle = FlatModeEnum.Flat;
			this.cmbAgents.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbAgents.ItemHeight = 15;
			this.cmbAgents.Location = new Point(60, 64);
			this.cmbAgents.MatchEntryTimeout = (long)2000;
			this.cmbAgents.MaxDropDownItems = 5;
			this.cmbAgents.MaxLength = 32767;
			this.cmbAgents.MouseCursor = Cursors.Default;
			this.cmbAgents.Name = "cmbAgents";
			this.cmbAgents.RowDivider.Color = Color.DarkGray;
			this.cmbAgents.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgents.RowSubDividerColor = Color.DarkGray;
			this.cmbAgents.Size = new System.Drawing.Size(220, 19);
			this.cmbAgents.TabIndex = 9;
			this.cmbAgents.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>17</DefaultRecSelWidth></Blob>";
			this.lblBatchDate.BackColor = SystemColors.Info;
			this.lblBatchDate.BorderStyle = BorderStyle.FixedSingle;
			this.lblBatchDate.ForeColor = SystemColors.ControlText;
			this.lblBatchDate.Location = new Point(160, 88);
			this.lblBatchDate.Name = "lblBatchDate";
			this.lblBatchDate.Size = new System.Drawing.Size(120, 20);
			this.lblBatchDate.TabIndex = 8;
			this.label5.ForeColor = SystemColors.ControlText;
			this.label5.Location = new Point(8, 88);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Дата пачки";
			this.cmdCloseBatch.FlatStyle = FlatStyle.Flat;
			this.cmdCloseBatch.ForeColor = SystemColors.ControlText;
			this.cmdCloseBatch.Location = new Point(32, 144);
			this.cmdCloseBatch.Name = "cmdCloseBatch";
			this.cmdCloseBatch.Size = new System.Drawing.Size(120, 24);
			this.cmdCloseBatch.TabIndex = 2;
			this.cmdCloseBatch.Text = "Закрыть";
			this.cmdCloseBatch.Click += new EventHandler(this.cmdCloseBatch_Click);
			this.cmdSaveBatch.FlatStyle = FlatStyle.Flat;
			this.cmdSaveBatch.ForeColor = SystemColors.ControlText;
			this.cmdSaveBatch.Location = new Point(160, 144);
			this.cmdSaveBatch.Name = "cmdSaveBatch";
			this.cmdSaveBatch.Size = new System.Drawing.Size(120, 24);
			this.cmdSaveBatch.TabIndex = 3;
			this.cmdSaveBatch.Text = "Сохранить";
			this.cmdSaveBatch.Click += new EventHandler(this.cmdSaveBatch_Click);
			this.numBatchCount.BorderStyle = BorderStyle.FixedSingle;
			this.numBatchCount.Location = new Point(160, 40);
			NumericUpDown num = this.numBatchCount;
			int[] numArray = new int[] { 1410065407, 2, 0, 0 };
			num.Maximum = new decimal(numArray);
			this.numBatchCount.Name = "numBatchCount";
			this.numBatchCount.TabIndex = 1;
			this.numBatchCount.Enter += new EventHandler(this.numBatchCount_Enter);
			this.numBatchCount.ValueChanged += new EventHandler(this.numBatchCount_ValueChanged);
			this.numBatchAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numBatchAmount.DecimalPlaces = 2;
			this.numBatchAmount.Location = new Point(160, 16);
			NumericUpDown numericUpDown = this.numBatchAmount;
			numArray = new int[] { 1410065407, 2, 0, 0 };
			numericUpDown.Maximum = new decimal(numArray);
			this.numBatchAmount.Name = "numBatchAmount";
			this.numBatchAmount.TabIndex = 6;
			this.numBatchAmount.TabStop = false;
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(8, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Агент";
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Количество платежей";
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Сумма платежей";
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 116);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Номер пачки";
			this.txtNumberBatch.BackColor = SystemColors.Info;
			this.txtNumberBatch.BorderStyle = 1;
			this.txtNumberBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtNumberBatch.Location = new Point(160, 116);
			this.txtNumberBatch.Name = "txtNumberBatch";
			this.txtNumberBatch.Size = new System.Drawing.Size(120, 21);
			this.txtNumberBatch.TabIndex = 1;
			this.txtNumberBatch.Tag = null;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.chkUslugi);
			this.groupBox3.Controls.Add(this.chkKKM);
			this.groupBox3.Controls.Add(this.bEnd);
			this.groupBox3.Controls.Add(this.numNewIndication);
			this.groupBox3.Controls.Add(this.numAmount);
			this.groupBox3.Controls.Add(this.cmdApply1);
			this.groupBox3.Controls.Add(this.lblCurrentIndication);
			this.groupBox3.Controls.Add(this.label18);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.Controls.Add(this.label20);
			this.groupBox3.ForeColor = SystemColors.Desktop;
			this.groupBox3.Location = new Point(4, 304);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(288, 152);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Платеж";
			this.chkKKM.Checked = true;
			this.chkKKM.CheckState = CheckState.Checked;
			this.chkKKM.ForeColor = SystemColors.ControlText;
			this.chkKKM.Location = new Point(40, 120);
			this.chkKKM.Name = "chkKKM";
			this.chkKKM.TabIndex = 5;
			this.chkKKM.Text = "Печать ККМ";
			this.bEnd.FlatStyle = FlatStyle.Flat;
			this.bEnd.ForeColor = SystemColors.ControlText;
			this.bEnd.Location = new Point(36, 92);
			this.bEnd.Name = "bEnd";
			this.bEnd.Size = new System.Drawing.Size(120, 24);
			this.bEnd.TabIndex = 4;
			this.bEnd.Text = "Закончить";
			this.bEnd.Click += new EventHandler(this.bEnd_Click);
			this.numNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numNewIndication.DecimalPlaces = 2;
			this.numNewIndication.Enabled = false;
			this.numNewIndication.Location = new Point(160, 68);
			NumericUpDown num1 = this.numNewIndication;
			numArray = new int[] { 9999, 0, 0, 0 };
			num1.Maximum = new decimal(numArray);
			this.numNewIndication.Name = "numNewIndication";
			this.numNewIndication.TabIndex = 2;
			this.numNewIndication.KeyPress += new KeyPressEventHandler(this.numNewIndication_KeyPress);
			this.numNewIndication.Enter += new EventHandler(this.numNewIndication_Enter);
			this.numNewIndication.Leave += new EventHandler(this.numNewIndication_Leave);
			this.numAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numAmount.DecimalPlaces = 2;
			this.numAmount.Location = new Point(160, 20);
			NumericUpDown numericUpDown1 = this.numAmount;
			numArray = new int[] { 999999, 0, 0, 0 };
			numericUpDown1.Maximum = new decimal(numArray);
			NumericUpDown num2 = this.numAmount;
			numArray = new int[] { 999999, 0, 0, -2147483648 };
			num2.Minimum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.TabIndex = 1;
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.cmdApply1.FlatStyle = FlatStyle.Flat;
			this.cmdApply1.ForeColor = SystemColors.ControlText;
			this.cmdApply1.Location = new Point(160, 92);
			this.cmdApply1.Name = "cmdApply1";
			this.cmdApply1.Size = new System.Drawing.Size(120, 24);
			this.cmdApply1.TabIndex = 3;
			this.cmdApply1.Text = "Принять";
			this.cmdApply1.Click += new EventHandler(this.cmdApply1_Click);
			this.lblCurrentIndication.BackColor = SystemColors.Info;
			this.lblCurrentIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblCurrentIndication.ForeColor = SystemColors.ControlText;
			this.lblCurrentIndication.Location = new Point(128, 44);
			this.lblCurrentIndication.Name = "lblCurrentIndication";
			this.lblCurrentIndication.Size = new System.Drawing.Size(152, 20);
			this.lblCurrentIndication.TabIndex = 2;
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(8, 68);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(120, 16);
			this.label18.TabIndex = 2;
			this.label18.Text = "Новые показания";
			this.label19.ForeColor = SystemColors.ControlText;
			this.label19.Location = new Point(8, 44);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(128, 16);
			this.label19.TabIndex = 1;
			this.label19.Text = "Текущие показания";
			this.label20.ForeColor = SystemColors.ControlText;
			this.label20.Location = new Point(8, 20);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(96, 16);
			this.label20.TabIndex = 0;
			this.label20.Text = "Сумма, тенге";
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.txtAccount);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblCountLives);
			this.groupBox2.Controls.Add(this.lblPU);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 184);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(288, 120);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.ForeColor = SystemColors.ControlText;
			this.button1.ImageIndex = 0;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new Point(260, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(20, 20);
			this.button1.TabIndex = 11;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(56, 16);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(96, 21);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.Tag = null;
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
			this.txtAccount.Enter += new EventHandler(this.txtAccount_Enter);
			this.txtAccount.Leave += new EventHandler(this.txtAccount_Leave);
			this.cmdAccount.FlatStyle = FlatStyle.Flat;
			this.cmdAccount.ForeColor = SystemColors.ControlText;
			this.cmdAccount.ImageIndex = 0;
			this.cmdAccount.ImageList = this.imageList1;
			this.cmdAccount.Location = new Point(160, 16);
			this.cmdAccount.Name = "cmdAccount";
			this.cmdAccount.Size = new System.Drawing.Size(20, 20);
			this.cmdAccount.TabIndex = 2;
			this.cmdAccount.Click += new EventHandler(this.cmdAccount_Click);
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(80, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(64, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Проживает";
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(56, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(200, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.ForeColor = SystemColors.ControlText;
			this.lblFIO.Location = new Point(56, 40);
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
			this.chkUslugi.Location = new Point(160, 120);
			this.chkUslugi.Name = "chkUslugi";
			this.chkUslugi.Size = new System.Drawing.Size(120, 24);
			this.chkUslugi.TabIndex = 6;
			this.chkUslugi.Text = "за услуги";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(728, 488);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.groupBox4);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MaximumSize = new System.Drawing.Size(734, 520);
			base.MinimizeBox = false;
			base.MinimumSize = new System.Drawing.Size(734, 520);
			base.Name = "frmBatch";
			this.Text = "Ведомость по сбору оплаты";
			base.Closing += new CancelEventHandler(this.frmBatch_Closing);
			base.Load += new EventHandler(this.frmBatch_Load);
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			((ISupportInitialize)this.statusBarPanel3).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((ISupportInitialize)this.cmbAgents).EndInit();
			((ISupportInitialize)this.numBatchCount).EndInit();
			((ISupportInitialize)this.numBatchAmount).EndInit();
			((ISupportInitialize)this.txtNumberBatch).EndInit();
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.numNewIndication).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			this.groupBox2.ResumeLayout(false);
			((ISupportInitialize)this.txtAccount).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_DoubleClick(object sender, EventArgs e)
		{
			this.EditDoc();
		}

		private void lv_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.EditDoc();
			}
		}

		private void numAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmount);
		}

		private void numAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (this.numNewIndication.Enabled)
				{
					this.numNewIndication.Focus();
					return;
				}
				this.cmdApply1.Focus();
			}
		}

		private void numBatchCount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numBatchCount);
		}

		private void numBatchCount_ValueChanged(object sender, EventArgs e)
		{
			this._batch.BatchCount = Convert.ToInt32(this.numBatchCount.Value);
		}

		private void numNewIndication_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numNewIndication);
		}

		private void numNewIndication_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numNewIndication_Leave(null, null);
				this.cmdApply1.Focus();
			}
		}

		private void numNewIndication_Leave(object sender, EventArgs e)
		{
			this.FactAmount = -1;
			if (this.numNewIndication.Value > new decimal(0) && this._indication != null)
			{
				string str = "";
				try
				{
					this._indication.oGmeter = this._gmeter;
					this._indication.Datedisplay = this._batch.BatchDate;
					this._indication.Display = Convert.ToDouble(this.numNewIndication.Value);
					str = this._indication.CalcFactUse();
					this.FactAmount = Convert.ToDouble(str);
				}
				catch
				{
					MessageBox.Show(str, "Показания прибора учета", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.numNewIndication.Value = new decimal(0);
					this.numNewIndication.Focus();
				}
			}
			if (this.numNewIndication.Value == new decimal(0))
			{
				this.FactAmount = 0;
			}
		}

		public bool PrintKKM(Document oDocument)
		{
			bool flag = false;
			try
			{
				if (this.ECR.CheckState != 0)
				{
					this.ECR.CloseCheck();
				}
				this.ECR.OpenCheck();
				this.ECR.Mode = 1;
				this.ECR.SetMode();
				this.ECR.TextWrap = 1;
				this.ECR.Alignment = 1;
				this.ECR.PrintPicture();
				this.ECR.AdvancedRegistration = true;
				this.ECR.SlipDocCharLineLength = 40;
				this.ECR.SlipDocTopMargin = 3;
				this.ECR.Alignment = 1;
				this.ECR.SlipDocLeftMargin = 1;
				this.ECR.SlipDocOrientation = 0;
				this.ECR.BeginDocument();
				this.ECR.Caption = "контрольный чек";
				this.ECR.PrintString();
				this.ECR.Caption = "ТОО Горгаз-сервис";
				this.ECR.PrintString();
				this.ECR.Alignment = 0;
				this.ECR.Caption = "г.Петропавловск,   ул.Букетова,  34";
				this.ECR.PrintString();
				this.ECR.Caption = "Св-во НДС 48001 № 0002050 от 27.10.2009";
				this.ECR.PrintString();
				this.ECR.Caption = "РНН 480100212262";
				this.ECR.PrintString();
				this.ECR.Caption = "БИН 020540001653";
				this.ECR.PrintString();
				this.ECR.Caption = "***************************************";
				this.ECR.PrintString();
				this.ECR.Caption = string.Concat("Лицевой счет:", oDocument.oContract.Account.ToString());
				this.ECR.PrintString();
				this.ECR.Caption = string.Concat("ФИО:", oDocument.oContract.oPerson.FullName.ToString());
				this.ECR.PrintString();
				this.ECR.EndDocument();
				this.ECR.Department = 1;
				this.ECR.Price = oDocument.DocumentAmount;
				this.ECR.Summ = Convert.ToDouble(this.numAmount.Value);
				this.ECR.Registration();
				this.ECR.TypeClose = 0;
				this.ECR.TaxTypeNumber = 1;
				if (this.ECR.Delivery() != 0)
				{
					MessageBox.Show("Ошибка закрытия чека! ", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.ECR.CloseCheck();
				}
				this.ECR.CloseCheck();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка печати ККМ! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintKKMVozvrat(Document oDocument)
		{
			bool flag = false;
			try
			{
				if (this.ECR.CheckState != 0)
				{
					this.ECR.CloseCheck();
				}
				this.ECR.EnableCheckSumm = false;
				this.ECR.Price = -oDocument.DocumentAmount;
				this.ECR.Quantity = 1;
				this.ECR.TypeClose = 0;
				if (this.ECR.Return() != 0)
				{
					MessageBox.Show("Ошибка закрытия чека!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.ECR.CloseCheck();
				}
				this.ECR.CloseCheck();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка печати ККМ! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintPKO(string zAccount, string zNameDispetcher, Document oDoc, string sBase, double fAmount)
		{
			bool flag = false;
			try
			{
				double num = Math.Round(fAmount, 2);
				string str = oDoc.DocumentNumber.ToString();
				if (str.Length == 0)
				{
					str = "00001";
				}
				string str1 = Tools.ConvertCurencyInString(num);
				int day = DateTime.Today.Day;
				day.ToString();
				day = DateTime.Today.Month;
				day.ToString();
				day = DateTime.Today.Year;
				day.ToString();
				DateTime today = DateTime.Today;
				Tools.NameMonth(today.Month);
				Convert.ToString(num);
				this._batch.oCashier.get_Name();
				day = DateTime.Today.Day;
				string str2 = day.ToString();
				day = DateTime.Today.Month;
				string str3 = day.ToString();
				day = DateTime.Today.Year;
				string str4 = day.ToString();
				today = DateTime.Today;
				string str5 = Tools.NameMonth(today.Month);
				string str6 = "";
				string str7 = "";
				string str8 = "";
				string str9 = "";
				string[] strArrays = new string[] { string.Concat("1|", str, "|200|719|0"), string.Concat("3|", str, "|445|745|0"), string.Concat("1|", zAccount.ToString(), "|127|612|0"), string.Concat("1|", num.ToString(), "|210|612|0"), string.Concat("3|", str1, "|75|560|0"), string.Concat("3|", str1, "|403|675|0"), string.Concat("3|", this._batch.oCashier.get_Name(), "|210|491|0"), string.Concat("3|", this._batch.oCashier.get_Name(), "|400|506|0"), null, null, null, null, null, null, null, null };
				string[] strArrays1 = new string[] { "1|", str2, ".", str3, ".", str4, "|295|719|0" };
				strArrays[8] = string.Concat(strArrays1);
				strArrays1 = new string[] { "1|", str2, " «", str5, "» ", str4, "г.|460|608|0" };
				strArrays[9] = string.Concat(strArrays1);
				strArrays[10] = string.Concat("3|", sBase, "|430|699|0");
				strArrays[11] = string.Concat("3|", sBase, "|100|572|0");
				strArrays[12] = "3||95|586|0";
				strArrays[13] = string.Concat("3|", zNameDispetcher, "|95|586|0");
				strArrays[14] = "3||428|723|0";
				strArrays[15] = string.Concat("3|", zNameDispetcher, "|428|723|0");
				string[] strArrays2 = strArrays;
				int length = zNameDispetcher.Length;
				if (length > 47)
				{
					str6 = zNameDispetcher.Substring(0, 48);
					str7 = zNameDispetcher.Substring(48);
					strArrays2.SetValue(string.Concat("3|", str6, "|95|593|0"), 12);
					strArrays2.SetValue(string.Concat("3|", str7, "|95|584|0"), 13);
				}
				if (length > 29)
				{
					str8 = zNameDispetcher.Substring(0, 29);
					str9 = zNameDispetcher.Substring(29);
					strArrays2.SetValue(string.Concat("3|", str8, "|428|732|0"), 14);
					strArrays2.SetValue(string.Concat("3|", str9, "|428|723|0"), 15);
				}
				Tools.ShowPdfDocument(string.Concat("Report_", zAccount.ToString(), ".pdf"), string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\"), "ПКО.pdf", strArrays2);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		private void ResetFields1()
		{
			this.txtAccount.Text = "";
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblCountLives.Text = "";
			this.lblPU.Text = "";
			this.lblCurrentIndication.Text = "";
			this.numAmount.Value = new decimal(0);
			this.numNewIndication.Value = new decimal(0);
			this.numNewIndication.Enabled = false;
			this.chkUslugi.Checked = false;
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
			this.doc = null;
			this._indication = null;
			this.lv.Items[this.ind].BackColor = SystemColors.Window;
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			try
			{
				"Excel";
				string str = e.Button.Tag.ToString();
				string str1 = str;
				if (str != null)
				{
					str1 = string.IsInterned(str1);
					if ((object)str1 == (object)"Edit")
					{
						this.EditDoc();
					}
					else if ((object)str1 == (object)"Del")
					{
						if (this.lv.SelectedItems.Count > 0 && MessageBox.Show(string.Concat("Вы действительно хотите удалить документ по договору ", this.lv.SelectedItems[0].Text, "?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						{
							double documentAmount = this._batch.oDocuments.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).DocumentAmount;
							if (this._batch.oDocuments.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) != 0)
							{
								MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this.docsAmount -= documentAmount;
								this.docsCount--;
								this.lv.Items.Remove(this.lv.SelectedItems[0]);
								this.FillStatusBar();
							}
						}
					}
					else if ((object)str1 != (object)"DelAll")
					{
						if ((object)str1 == (object)"Excel")
						{
							Tools.ConvertToExcel(this.lv);
						}
					}
					else if (this.lv.Items.Count > 0 && MessageBox.Show("Вы действительно хотите удалить ВСЕ документы из пачки?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						for (int i = 0; i < this.lv.Items.Count; i++)
						{
							double num = this._batch.oDocuments.item(Convert.ToInt64(this.lv.Items[i].Tag)).DocumentAmount;
							if (this._batch.oDocuments.Remove(Convert.ToInt64(this.lv.Items[i].Tag)) != 0)
							{
								MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this.docsAmount -= num;
								this.docsCount--;
							}
						}
						this.lv.Items.Clear();
						this.FillStatusBar();
						MessageBox.Show("Все удалено!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
			catch
			{
			}
		}

		private void txtAccount_Enter(object sender, EventArgs e)
		{
			this.txtAccount.SelectAll();
		}

		private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdAccount_Click(null, null);
			}
		}

		private void txtAccount_Leave(object sender, EventArgs e)
		{
			this.cmdAccount_Click(null, null);
		}
	}
}