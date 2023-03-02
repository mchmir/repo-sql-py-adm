using C1.Win.C1Command;
using C1.Win.C1Input;
using FprnM1C;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmCash : Form
	{
		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private GroupBox groupBox2;

		private Label label10;

		private Label label11;

		private Label label12;

		private Label label13;

		private ImageList imageList1;

		private GroupBox groupBox3;

		private Label label18;

		private Label label19;

		private Label label20;

		private ToolTip toolTip1;

		private StatusBar statusBar1;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private StatusBarPanel statusBarPanel3;

		private StatusBarPanel statusBarPanel4;

		private StatusBarPanel statusBarPanel5;

		private StatusBarPanel statusBarPanel7;

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

		private GroupBox groupBox5;

		private Label label24;

		private Label label25;

		private Label label26;

		private Label label15;

		private GroupBox groupBox6;

		private Label label17;

		private Label label21;

		private Label label22;

		private Label label23;

		private Label label27;

		private Button cmdAccount;

		private Label lblCountLives;

		private Label lblAddress;

		private Label lblFIO;

		private Label lblPU;

		private IContainer components;

		private Label lblCurrentIndication;

		private Button cmdApply1;

		private Button cmdPrint;

		private NumericUpDown numAmount;

		private NumericUpDown numNewIndication;

		private CheckBox checkPrint1;

		private Label lblFactUse;

		private Label label8;

		private Label label9;

		private Label lblBalance;

		private ListView lv;

		private Label lblBatchCount;

		private Label lblBatchAmount;

		private Label lblCashier;

		private Button cmdApply2;

		private NumericUpDown numBatchCount;

		private ComboBox cmbAgent;

		private TextBox txtNote;

		private NumericUpDown numBatchAmount;

		private CheckBox checkPrint2;

		private Button cmdApply3;

		private NumericUpDown numBatchAmount2;

		private ComboBox cmbKorr;

		private TextBox txtNote2;

		private TextBox txtFromWhom;

		private CheckBox checkPrint3;

		private ComboBox cmbOperation;

		private C1TextBox txtAccount;

		private Panel panel1;

		private C1CommandHolder CommandHolder;

		private C1Command cmd_toolBarButton23;

		private C1Command cmd_toolBarButton25;

		private C1Command cmd_toolBarButton26;

		private ImageList imageList2;

		private StatusBarPanel statusBarPanel6;

		private Button cmdAddress;

		private C1Command cmd_toolBarButton1;

		private ListViewSortManager m_sortMgr1;

		private List lvContent;

		private Address _address;

		private Contract _contract;

		private Batch _batch;

		private CashBalance _cashbalance;

		private Gobject _gobject;

		private Gmeter _gmeter;

		private Agents _agents;

		private Indication _indication;

		private Batch _cardBatch;

		private Batchs _cardBatchs;

		private double FactAmount = 0;

		private int PrKKM = 0;

		private double sumFromAgent = 0;

		private double sumOther = 0;

		private double sumIncas = 0;

		private double sumVidacha = 0;

		private int countFromAgent = 0;

		private int countOther = 0;

		private int countIncas = 0;

		private int countVidacha = 0;

		private int IDUser = Convert.ToInt32(SQLConnect.CurrentUser.get_ID());

		private Batchs _batchsagent;

		private ToolBar tbData;

		private ToolBarButton toolBarButton23;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private ToolBarButton toolBarButton1;

		private TabPage tabPage5;

		private TabPage tabPage6;

		private TabPage tabPage7;

		private TabPage tabPage8;

		private ImageList imageList3;

		private TabPage tabPage9;

		private GroupBox groupBox8;

		private NumericUpDown numericUpDown2;

		private Button button4;

		private CheckBox checkBox1;

		private Label label34;

		private GroupBox groupBox7;

		private Button button1;

		private C1TextBox c1TextBox1;

		private Button button2;

		private Label label7;

		private Label label14;

		private Label label16;

		private Label label28;

		private Label label29;

		private TabPage tabPage4;

		private Batchs _batchsother;

		private NumericUpDown nmcPoluch;

		private Label label4;

		private Label lblZdacha;

		private Label label5;

		private TextBox txtBaze;

		private Label label6;

		private TextBox txtRNN;

		private Button bFindRNN;

		private TextBox txtAbRNN;

		private Button bRNN;

		private Label label30;

		private IFprnM45 ECR;

		private GroupBox groupBox9;

		private Label label31;

		private TextBox txtAccountUL;

		private Button bAccountUL;

		private Label label32;

		private Label lblInfoUL;

		private Label label33;

		private Label lblAddressUL;

		private Button bAddressUL;

		private Label label35;

		private TextBox txtIINul;

		private Button bIINul;

		private GroupBox groupBox10;

		private Label label36;

		private Label label37;

		private Label lblBalanceUL;

		private Label label38;

		private Label lblZdachaUL;

		private NumericUpDown numAmountUL;

		private NumericUpDown numPoluchUL;

		private Button bApplyUL;

		private Label label39;

		private TextBox txtBaseUL;

		private ComboBox cmbAccount;

		private ComboBox cmbAccountUL;

		private CheckBox chkPayCard;

		private StatusBarPanel statusBarPanel8;

		private Panel panel2;

		private CheckBox chkPayCardUL;

		private ToolTip TT;

		public frmCash(Batch oBatch, Batch oCardBatch, CashBalance oCashBalance, Batchs oBatchAgent, Batchs oBatchOther, int OpenKKM)
		{
			this.InitializeComponent();
			this._batch = oBatch;
			this._cashbalance = oCashBalance;
			this._batchsagent = oBatchAgent;
			this._batchsother = oBatchOther;
			this._cardBatch = oCardBatch;
			foreach (Batch batch in this._batchsagent)
			{
				this.sumFromAgent += batch.BatchAmount;
			}
			this.countFromAgent = this._batchsagent.get_Count();
			foreach (Batch batch1 in this._batchsother)
			{
				if (batch1.oTypeBatch.get_ID() == (long)5)
				{
					this.sumOther += batch1.BatchAmount;
					this.countOther++;
				}
				if (batch1.oTypeBatch.get_ID() == (long)3)
				{
					this.sumIncas += batch1.BatchAmount;
					this.countIncas++;
				}
				if (batch1.oTypeBatch.get_ID() != (long)6)
				{
					continue;
				}
				this.sumVidacha += batch1.BatchAmount;
				this.countVidacha++;
			}
			if (OpenKKM == 1)
			{
				this.ECR = new FprnM45Class()
				{
					DeviceEnabled = true
				};
				if (this.ECR.ResultCode != 0)
				{
					MessageBox.Show("Не\tвозможно подключить порт!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (this.ECR.CheckState != 0 && this.ECR.CancelCheck() != 0)
				{
					return;
				}
				this.ECR.Password = "1";
				this.PrKKM = 1;
				this.ECR.Mode = 1;
				this.ECR.SetMode() == 0;
			}
		}

		private void bAccountUL_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.cmbAccountUL.Text.Length != 0)
				{
					this.FindContractUL(this.cmbAccountUL.Text.Trim());
				}
				else
				{
					this.ResetFields2();
				}
			}
			catch
			{
			}
		}

		private void bAddressUL_Click(object sender, EventArgs e)
		{
			frmAddress _frmAddress = new frmAddress()
			{
				oAddress = this._address
			};
			this.ResetFields2();
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
				this.cmbAccountUL.Items.Clear();
				this.cmbAccountUL.Items.Add(this._contract.Account);
				this.cmbAccountUL.SelectedIndex = 0;
				this._address = address;
				address = null;
				this.bAccountUL_Click(null, null);
			}
			_frmAddress = null;
		}

		private void bApplyUL_Click(object sender, EventArgs e)
		{
			string[] strArrays;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract == null)
					{
						return;
					}
					else if (this.numAmountUL.Value == new decimal(0))
					{
						this.numAmountUL.Focus();
						return;
					}
					else if (this.numAmountUL.Value > new decimal(30000) && MessageBox.Show(string.Concat("Сумма оплаты составляет ", this.numAmountUL.Value.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
					{
						this.numAmountUL.Focus();
						return;
					}
					else if (this.txtBaseUL.Text.Length == 0)
					{
						MessageBox.Show("Не указано основание!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					else if (this.txtIINul.Text.Length >= 12)
					{
						long num = (long)0;
						long num1 = (long)0;
						string str = "";
						string str1 = "";
						bool flag = false;
						if (!this.chkPayCardUL.Checked)
						{
							Document document = new Document()
							{
								oBatch = this._batch,
								oContract = this._contract,
								oPeriod = Depot.CurrentPeriod
							};
							if (this.numAmountUL.Value >= new decimal(0))
							{
								document.oTypeDocument = Depot.oTypeDocuments.item((long)1);
							}
							else
							{
								document.oTypeDocument = Depot.oTypeDocuments.item((long)23);
							}
							document.DocumentAmount = Convert.ToDouble(this.numAmountUL.Value);
							document.DocumentDate = this._batch.BatchDate;
							str = Convert.ToString(this._batch.oDocuments.get_Count() + 1);
							document.DocumentNumber = str;
							if (!document.DocumentPayVDGO(this._contract.oPerson.isJuridical, Depot.CurrentPeriod.get_ID(), this._batch.get_ID(), this._contract.get_ID(), (long)1, this._batch.BatchDate, Convert.ToDouble(this.numAmountUL.Value), 0, false, SQLConnect.CurrentUser.get_ID(), ref num1, ref num, ref this.FactAmount, ref str, ref str1, ref flag))
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								CashBalance amountBalance = this._cashbalance;
								amountBalance.AmountBalance = amountBalance.AmountBalance + document.DocumentAmount;
								this._cashbalance.Save();
								Batch batchCount = this._batch;
								batchCount.BatchCount = batchCount.BatchCount + 1;
								Batch batchAmount = this._batch;
								batchAmount.BatchAmount = batchAmount.BatchAmount + document.DocumentAmount;
								document.set_ID(num1);
								this._batch.oDocuments.Add(document);
								this.FillOneItem(document, true);
								this.FillCommonInfo();
								this.FillStatusBar();
								this.txtAccountUL.Focus();
								if (this._contract.oPerson.isJuridical == 1 && document.DocumentAmount > 0)
								{
									strArrays = new string[] { "ИИН ", this.txtIINul.Text.Trim(), ", ", this.lblInfoUL.Text, ", ", this.lblAddressUL.Text };
									this.PrintPKO("1212", string.Concat(strArrays), document, this.txtBaseUL.Text, document.DocumentAmount);
								}
								if (document.DocumentAmount < 0)
								{
									this.PrintPKODoc(document);
								}
								if (this.PrKKM == 1)
								{
									if (document.DocumentAmount <= 0)
									{
										this.PrintKKMVozvrat(document);
									}
									else
									{
										this.PrintKKM(document, 2);
									}
									this.lblZdachaUL.Text = "";
									this.numPoluchUL.Value = new decimal(0);
								}
								this.ResetFields2();
								this.cmbAccountUL.Focus();
							}
						}
						else
						{
							this._cardBatchs = new Batchs();
							this._cardBatchs.Load(Depot.oStatusBatchs.item((long)1), Depot.oTypePays.item((long)2), Depot.oTypeBatchs.item((long)1), this._batch.oCashier, 113);
							if (this._cardBatchs.get_Count() <= 0)
							{
								this._cardBatch.oTypePay = Depot.oTypePays.item((long)2);
								this._cardBatch.oPeriod = Depot.CurrentPeriod;
								this._cardBatch.oDispatcher = this._agents.item((long)113);
								this._cardBatch.oCashier = this._batch.oCashier;
								this._cardBatch.oStatusBatch = Depot.oStatusBatchs.item((long)1);
								this._cardBatch.set_Name("Оплата картой");
								this._cardBatch.BatchCount = 0;
								this._cardBatch.BatchAmount = 0;
								this._cardBatch.oTypeBatch = Depot.oTypeBatchs.item((long)1);
								this._cardBatch.BatchDate = DateTime.Today;
								this._cardBatch.NumberBatch = string.Concat(this._batch.NumberBatch, "/1");
								if (this._cardBatch.Save() != 0)
								{
									return;
								}
							}
							else
							{
								this._cardBatch = this._cardBatchs[0];
							}
							Document batchDate = new Document()
							{
								oBatch = this._cardBatch,
								oContract = this._contract,
								oPeriod = Depot.CurrentPeriod
							};
							if (this.numAmount.Value >= new decimal(0))
							{
								batchDate.oTypeDocument = Depot.oTypeDocuments.item((long)1);
							}
							else
							{
								batchDate.oTypeDocument = Depot.oTypeDocuments.item((long)23);
							}
							batchDate.DocumentAmount = Convert.ToDouble(this.numAmountUL.Value);
							batchDate.DocumentDate = this._cardBatch.BatchDate;
							str = Convert.ToString(this._cardBatch.oDocuments.get_Count() + 1);
							batchDate.DocumentNumber = str;
							int num2 = this._contract.oPerson.isJuridical;
							if (!batchDate.DocumentPayVDGO(this._contract.oPerson.isJuridical, Depot.CurrentPeriod.get_ID(), this._cardBatch.get_ID(), this._contract.get_ID(), (long)1, this._cardBatch.BatchDate, Convert.ToDouble(this.numAmountUL.Value), 0, false, SQLConnect.CurrentUser.get_ID(), ref num1, ref num, ref this.FactAmount, ref str, ref str1, ref flag))
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								Batch batch = this._cardBatch;
								batch.BatchCount = batch.BatchCount + 1;
								Batch batchAmount1 = this._cardBatch;
								batchAmount1.BatchAmount = batchAmount1.BatchAmount + batchDate.DocumentAmount;
								this._cardBatch.Save();
								batchDate.set_ID(num1);
								this._cardBatch.oDocuments.Add(batchDate);
								this.FillStatusBar();
								if (this._contract.oPerson.isJuridical == 1 && batchDate.DocumentAmount > 0)
								{
									strArrays = new string[] { "ИИН ", this.txtIINul.Text.Trim(), ", ", this.lblInfoUL.Text, ", ", this.lblAddressUL.Text };
									this.PrintPKO("1212", string.Concat(strArrays), batchDate, this.txtBaseUL.Text, batchDate.DocumentAmount);
								}
								if (batchDate.DocumentAmount < 0)
								{
									this.PrintPKODoc(batchDate);
								}
								if (this.PrKKM == 1)
								{
									if (batchDate.DocumentAmount <= 0)
									{
										this.PrintKKMVozvrat(batchDate);
									}
									else
									{
										this.PrintKKMCard(batchDate, 3);
									}
									this.lblZdachaUL.Text = "";
									this.numPoluchUL.Value = new decimal(0);
								}
								this.ResetFields2();
								this.cmbAccountUL.Focus();
							}
						}
					}
					else
					{
						MessageBox.Show("Не указан ИИН!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
				this.lblZdacha.Text = "";
			}
		}

		private void bFindRNN_Click(object sender, EventArgs e)
		{
			this.SearchConsumer(3);
		}

		private void bIINul_Click(object sender, EventArgs e)
		{
			this.ResetFields4();
			if (this.lvContent == null)
			{
				this.lvContent = new List();
			}
			string str = "";
			if (this.txtIINul.Text.Length <= 5)
			{
				return;
			}
			str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(str(tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract,  case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 20), 10) else ' ' end date , p.RNN  from person p  inner join address a on a.idaddress=p.idaddress  and p.RNN like '", this.txtIINul.Text, "%' inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  left join contract c on c.idperson=p.idperson  left join gobject g on g.idcontract=c.idcontract  left join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  left join gmeter gm on g.idgobject=gm.idgobject  and gm.idstatusgmeter=1  left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter order by c.Account");
			this.lvContent.set_nametable_pr("contract");
			this.lvContent.set_select_pr(str);
			this.lvContent.Load();
			this.cmbAccountUL.Items.Clear();
			foreach (string[] mylistPr in this.lvContent.get_mylist_pr())
			{
				this.cmbAccountUL.Items.Add(mylistPr[2]);
			}
			if (this.cmbAccountUL.Items.Count <= 0)
			{
				this.ResetFields4();
			}
			else
			{
				this.cmbAccountUL.SelectedIndex = 0;
			}
			if (this.cmbAccountUL.Items.Count == 1)
			{
				this.FindContract(this.cmbAccountUL.Text);
			}
		}

		private void bRNN_Click(object sender, EventArgs e)
		{
			this.ResetFields3();
			if (this.lvContent == null)
			{
				this.lvContent = new List();
			}
			string str = "";
			if (this.txtAbRNN.Text.Length <= 5)
			{
				return;
			}
			str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.Surname else isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(str(tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract,  case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 20), 10) else ' ' end date , p.RNN  from person p  inner join address a on a.idaddress=p.idaddress  and p.RNN like '", this.txtAbRNN.Text, "%' inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  left join contract c on c.idperson=p.idperson  left join gobject g on g.idcontract=c.idcontract  left join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  left join gmeter gm on g.idgobject=gm.idgobject  and gm.idstatusgmeter=1  left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter order by c.Account");
			this.lvContent.set_nametable_pr("contract");
			this.lvContent.set_select_pr(str);
			this.lvContent.Load();
			this.cmbAccount.Items.Clear();
			foreach (string[] mylistPr in this.lvContent.get_mylist_pr())
			{
				this.cmbAccount.Items.Add(mylistPr[2]);
			}
			if (this.cmbAccount.Items.Count <= 0)
			{
				this.ResetFields3();
			}
			else
			{
				this.cmbAccount.SelectedIndex = 0;
			}
			if (this.cmbAccount.Items.Count == 1)
			{
				this.FindContract(this.cmbAccount.SelectedText);
			}
		}

		private void checkPrint1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdApply1.Focus();
			}
		}

		private void checkPrint2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdApply2.Focus();
			}
		}

		private void checkPrint3_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdApply3.Focus();
			}
		}

		private void chkPayCard_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkPayCard.Checked)
			{
				MessageBox.Show("Будет проведена оплата картой!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void chkPayCardUL_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkPayCardUL.Checked)
			{
				MessageBox.Show("Будет проведена оплата картой!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void cmbAccount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmdAccount_Click(null, null);
			}
		}

		private void cmbAccount_Leave(object sender, EventArgs e)
		{
			this.cmdAccount_Click(null, null);
		}

		private void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cmdAccount_Click(null, null);
		}

		private void cmbAccountUL_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.bAccountUL_Click(null, null);
			}
		}

		private void cmbAccountUL_Leave(object sender, EventArgs e)
		{
			this.bAccountUL_Click(null, null);
		}

		private void cmbAccountUL_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.bAccountUL_Click(null, null);
		}

		private void cmbAgent_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote.Focus();
			}
		}

		private void cmbKorr_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtNote2.Focus();
			}
		}

		private void cmbOperation_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numBatchAmount2.Focus();
			}
		}

		private void cmdAccount_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.cmbAccount.Text.Length != 0)
				{
					this.FindContract(this.cmbAccount.Text);
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

		private void cmdAddress_Click(object sender, EventArgs e)
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
				this.cmbAccount.Items.Clear();
				this.cmbAccount.Items.Add(this._contract.Account);
				this.cmbAccount.SelectedIndex = 0;
				this._address = address;
				address = null;
				this.cmdAccount_Click(null, null);
			}
			_frmAddress = null;
		}

		private void cmdApply1_Click(object sender, EventArgs e)
		{
			string[] text;
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this._contract == null)
					{
						return;
					}
					else if (this.numAmount.Value == new decimal(0))
					{
						this.numAmount.Focus();
						return;
					}
					else if (this.numAmount.Value > new decimal(30000) && MessageBox.Show(string.Concat("Сумма оплаты составляет ", this.numAmount.Value.ToString(), ". Продолжить?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
					{
						this.numAmount.Focus();
						return;
					}
					else if (this._contract.oPerson.isJuridical != 1 || this.txtAbRNN.Text.Length >= 12)
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
							long d = (long)0;
							if (this._indication != null)
							{
								d = this._indication.get_ID();
							}
							long num = (long)0;
							string str = "";
							string str1 = "";
							bool flag = false;
							if (!this.chkPayCard.Checked)
							{
								Document document = new Document()
								{
									oBatch = this._batch,
									oContract = this._contract,
									oPeriod = Depot.CurrentPeriod
								};
								if (this.numAmount.Value >= new decimal(0))
								{
									document.oTypeDocument = Depot.oTypeDocuments.item((long)1);
								}
								else
								{
									document.oTypeDocument = Depot.oTypeDocuments.item((long)23);
								}
								document.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
								document.DocumentDate = this._batch.BatchDate;
								str = Convert.ToString(this._batch.oDocuments.get_Count() + 1);
								document.DocumentNumber = str;
								int num1 = this._contract.oPerson.isJuridical;
								double num2 = 0;
								if (!document.DocumentPay(num1, Depot.CurrentPeriod.get_ID(), this._batch.get_ID(), this._contract.get_ID(), (long)1, this._batch.BatchDate, Convert.ToDouble(this.numAmount.Value), Convert.ToDouble(this.numNewIndication.Value), false, SQLConnect.CurrentUser.get_ID(), ref num, ref d, ref this.FactAmount, ref str, ref str1, ref flag, ref num2))
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									CashBalance amountBalance = this._cashbalance;
									amountBalance.AmountBalance = amountBalance.AmountBalance + document.DocumentAmount;
									this._cashbalance.Save();
									Batch batchCount = this._batch;
									batchCount.BatchCount = batchCount.BatchCount + 1;
									Batch batchAmount = this._batch;
									batchAmount.BatchAmount = batchAmount.BatchAmount + document.DocumentAmount;
									document.set_ID(num);
									this._batch.oDocuments.Add(document);
									this.FillOneItem(document, true);
									this.FillCommonInfo();
									this.FillStatusBar();
									this.txtAccount.Focus();
									if (num1 == 1)
									{
										if (num2 <= 0)
										{
											text = new string[] { "ИИН ", this.txtAbRNN.Text, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1211", string.Concat(text), document, "за газ", document.DocumentAmount);
										}
										else
										{
											text = new string[] { "ИИН ", this.txtAbRNN.Text, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1211", string.Concat(text), document, "за газ", document.DocumentAmount - num2);
											MessageBox.Show("Распечатайте ПКО за газ и нажмите ОК.");
											text = new string[] { "ИИН ", this.txtAbRNN.Text, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1212", string.Concat(text), document, "за услуги", num2);
											MessageBox.Show("Распечатайте ПКО за услуги и нажмите ОК.");
										}
									}
									else if (this.checkPrint1.Checked)
									{
										this.Print1(document.get_ID());
									}
									if (document.DocumentAmount < 0)
									{
										this.PrintPKOVozvratDoc(document);
									}
									this.checkPrint1.Checked = false;
									if (this.PrKKM == 1)
									{
										if (document.DocumentAmount <= 0)
										{
											this.PrintKKMVozvrat(document);
										}
										else
										{
											this.PrintKKM(document, 1);
										}
										this.lblZdacha.Text = "";
										this.nmcPoluch.Value = new decimal(0);
									}
									this.ResetFields1();
									this.cmbAccount.Focus();
								}
							}
							else
							{
								this._cardBatchs = new Batchs();
								this._cardBatchs.Load(Depot.oStatusBatchs.item((long)1), Depot.oTypePays.item((long)2), Depot.oTypeBatchs.item((long)1), this._batch.oCashier, 113);
								if (this._cardBatchs.get_Count() <= 0)
								{
									this._cardBatch.oTypePay = Depot.oTypePays.item((long)2);
									this._cardBatch.oPeriod = Depot.CurrentPeriod;
									this._cardBatch.oDispatcher = this._agents.item((long)113);
									this._cardBatch.oCashier = this._batch.oCashier;
									this._cardBatch.oStatusBatch = Depot.oStatusBatchs.item((long)1);
									this._cardBatch.set_Name("Оплата картой");
									this._cardBatch.BatchCount = 0;
									this._cardBatch.BatchAmount = 0;
									this._cardBatch.oTypeBatch = Depot.oTypeBatchs.item((long)1);
									this._cardBatch.BatchDate = DateTime.Today;
									this._cardBatch.NumberBatch = string.Concat(this._batch.NumberBatch, "/1");
									if (this._cardBatch.Save() != 0)
									{
										return;
									}
								}
								else
								{
									this._cardBatch = this._cardBatchs[0];
								}
								Document batchDate = new Document()
								{
									oBatch = this._cardBatch,
									oContract = this._contract,
									oPeriod = Depot.CurrentPeriod
								};
								if (this.numAmount.Value >= new decimal(0))
								{
									batchDate.oTypeDocument = Depot.oTypeDocuments.item((long)1);
								}
								else
								{
									batchDate.oTypeDocument = Depot.oTypeDocuments.item((long)23);
								}
								batchDate.DocumentAmount = Convert.ToDouble(this.numAmount.Value);
								batchDate.DocumentDate = this._cardBatch.BatchDate;
								str = Convert.ToString(this._cardBatch.oDocuments.get_Count() + 1);
								batchDate.DocumentNumber = str;
								int num3 = this._contract.oPerson.isJuridical;
								double num4 = 0;
								if (!batchDate.DocumentPay(num3, Depot.CurrentPeriod.get_ID(), this._cardBatch.get_ID(), this._contract.get_ID(), (long)1, this._cardBatch.BatchDate, Convert.ToDouble(this.numAmount.Value), Convert.ToDouble(this.numNewIndication.Value), false, SQLConnect.CurrentUser.get_ID(), ref num, ref d, ref this.FactAmount, ref str, ref str1, ref flag, ref num4))
								{
									MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									Batch batch = this._cardBatch;
									batch.BatchCount = batch.BatchCount + 1;
									Batch batchAmount1 = this._cardBatch;
									batchAmount1.BatchAmount = batchAmount1.BatchAmount + batchDate.DocumentAmount;
									this._cardBatch.Save();
									batchDate.set_ID(num);
									this._cardBatch.oDocuments.Add(batchDate);
									this.FillStatusBar();
									this.txtAccount.Focus();
									if (num3 == 1)
									{
										if (num4 <= 0)
										{
											text = new string[] { "ИИН ", this.txtAbRNN.Text, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1211", string.Concat(text), batchDate, "за газ", batchDate.DocumentAmount);
										}
										else
										{
											text = new string[] { "ИИН ", this.txtAbRNN.Text, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1211", string.Concat(text), batchDate, "за газ", batchDate.DocumentAmount - num4);
											MessageBox.Show("Распечатайте ПКО за газ и нажмите ОК.");
											text = new string[] { "ИИН ", this.txtAbRNN.Text, ", ", this.lblFIO.Text, ", ", this.lblAddress.Text };
											this.PrintPKO("1212", string.Concat(text), batchDate, "за услуги", num4);
											MessageBox.Show("Распечатайте ПКО за услуги и нажмите ОК.");
										}
									}
									else if (this.checkPrint1.Checked)
									{
										this.Print1(batchDate.get_ID());
									}
									if (batchDate.DocumentAmount < 0)
									{
										this.PrintPKODoc(batchDate);
									}
									this.checkPrint1.Checked = false;
									if (this.PrKKM == 1)
									{
										if (batchDate.DocumentAmount <= 0)
										{
											this.PrintKKMVozvrat(batchDate);
										}
										else
										{
											this.PrintKKMCard(batchDate, 3);
										}
										this.lblZdacha.Text = "";
										this.nmcPoluch.Value = new decimal(0);
									}
									this.ResetFields1();
									this.cmbAccount.Focus();
								}
							}
						}
					}
					else
					{
						MessageBox.Show("Уажите РНН(ИИН)!");
						return;
					}
				}
				catch (Exception exception)
				{
					this.WriteLog(exception.Message.ToString());
				}
			}
			finally
			{
				this.WriteLog("зашли в finnally");
				this.Cursor = Cursors.Default;
				this.lblZdacha.Text = "";
				this.cmbAccount.Focus();
			}
		}

		private void cmdApply2_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (this.cmbAgent.SelectedIndex == -1 || this.numBatchAmount.Value == new decimal(0) || this.numBatchCount.Value == new decimal(0))
					{
						MessageBox.Show("Необходимо указать агента, количество платежей и сумму пачки!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					else
					{
						Batch currentPeriod = this._batchsagent.Add();
						currentPeriod.oTypePay = Depot.oTypePays.item((long)1);
						currentPeriod.oPeriod = Depot.CurrentPeriod;
						currentPeriod.oCashier = this._batch.oCashier;
						currentPeriod.oDispatcher = this._agents[this.cmbAgent.SelectedIndex];
						currentPeriod.set_Name("Оплата через кассу");
						currentPeriod.BatchCount = Convert.ToInt32(this.numBatchCount.Value);
						currentPeriod.BatchAmount = Convert.ToDouble(this.numBatchAmount.Value);
						currentPeriod.BatchDate = DateTime.Today;
						currentPeriod.oTypeBatch = Depot.oTypeBatchs.item((long)1);
						currentPeriod.oStatusBatch = Depot.oStatusBatchs.item((long)1);
						string[] strArrays = new string[] { "0", Tools.ConvertDateFORSQL(DateTime.Today) };
						object obj = Saver.ExecuteFunction("fShowNumberBatchDispatcher", strArrays);
						if (obj == null)
						{
							currentPeriod.NumberBatch = "1";
						}
						else
						{
							currentPeriod.NumberBatch = obj.ToString();
						}
						currentPeriod.Note = this.txtNote.Text;
						if (currentPeriod.Save() != 0)
						{
							MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else if ((new frmBatch(currentPeriod, true, this.ECR)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
						{
							CashBalance amountBalance = this._cashbalance;
							amountBalance.AmountBalance = amountBalance.AmountBalance + Convert.ToDouble(this.numBatchAmount.Value);
							this._cashbalance.Save();
							this.sumFromAgent += (double)Convert.ToInt32(this.numBatchAmount.Value);
							this.countFromAgent++;
							this.FillOneItemAgent(currentPeriod);
							this.FillCommonInfo();
							this.FillStatusBar();
							this.numBatchAmount.Value = new decimal(0);
							this.numBatchCount.Value = new decimal(0);
							this.txtNote.Text = "";
							this.numBatchAmount.Focus();
							if (this.checkPrint2.Checked)
							{
								this.PrintPKO(currentPeriod, "за газ");
							}
						}
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

		private void cmdApply3_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					string str = "";
					if (this.cmbOperation.SelectedIndex == 0)
					{
						str = "Будет произведен прием денег. Продолжить?";
					}
					if (this.cmbOperation.SelectedIndex == 1)
					{
						str = "Будет произведена инскассация. Продолжить?";
					}
					if (this.cmbOperation.SelectedIndex == 2)
					{
						str = "Будет произведена выдача денег. Продолжить?";
					}
					if (MessageBox.Show(str, "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
					{
						this.Cursor = Cursors.WaitCursor;
						if (this.cmbOperation.SelectedIndex == -1 || this.cmbKorr.SelectedIndex == -1 || this.numBatchAmount2.Value == new decimal(0))
						{
							MessageBox.Show("Необходимо указать тип операции, корреспондирующий счет и сумму пачки!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						else if (this.cmbOperation.SelectedIndex == 1 && Convert.ToDouble(this.numBatchAmount2.Value) > this._cashbalance.AmountBalance)
						{
							MessageBox.Show("Нельзя инкассировать денег больше, чем есть в кассе!", "Инкассация", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							this.numBatchAmount2.Focus();
							return;
						}
						else if (this.cmbOperation.SelectedIndex != 2 || Convert.ToDouble(this.numBatchAmount2.Value) <= this._cashbalance.AmountBalance)
						{
							Batch currentPeriod = this._batchsother.Add();
							currentPeriod.oTypePay = Depot.oTypePays.item((long)1);
							currentPeriod.oPeriod = Depot.CurrentPeriod;
							currentPeriod.oCashier = this._batch.oCashier;
							currentPeriod.set_Name(string.Concat(this.cmbKorr.Text, "@", this.txtFromWhom.Text));
							currentPeriod.BatchCount = 1;
							currentPeriod.BatchAmount = Convert.ToDouble(this.numBatchAmount2.Value);
							currentPeriod.BatchDate = DateTime.Today;
							if (this.cmbOperation.SelectedIndex == 0)
							{
								currentPeriod.oTypeBatch = Depot.oTypeBatchs.item((long)5);
							}
							if (this.cmbOperation.SelectedIndex == 1)
							{
								currentPeriod.oTypeBatch = Depot.oTypeBatchs.item((long)3);
							}
							if (this.cmbOperation.SelectedIndex == 2)
							{
								currentPeriod.oTypeBatch = Depot.oTypeBatchs.item((long)6);
							}
							currentPeriod.oStatusBatch = Depot.oStatusBatchs.item((long)2);
							if (this.cmbOperation.SelectedIndex == 0)
							{
								string[] strArrays = new string[] { "0", Tools.ConvertDateFORSQL(DateTime.Today) };
								object obj = Saver.ExecuteFunction("fShowNumberBatchDispatcher", strArrays);
								if (obj == null)
								{
									currentPeriod.NumberBatch = "1";
								}
								else
								{
									currentPeriod.NumberBatch = obj.ToString();
								}
							}
							currentPeriod.Note = this.txtNote2.Text;
							if (currentPeriod.Save() != 0)
							{
								MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								double amountBalance = this._cashbalance.AmountBalance;
								this.WriteLog(amountBalance.ToString());
								if (this.cmbOperation.SelectedIndex == 0)
								{
									CashBalance cashBalance = this._cashbalance;
									cashBalance.AmountBalance = cashBalance.AmountBalance + Convert.ToDouble(this.numBatchAmount2.Value);
									this.sumOther += (double)Convert.ToInt32(this.numBatchAmount2.Value);
									this.countOther++;
								}
								if (this.cmbOperation.SelectedIndex == 1)
								{
									CashBalance amountBalance1 = this._cashbalance;
									amountBalance1.AmountBalance = amountBalance1.AmountBalance - Convert.ToDouble(this.numBatchAmount2.Value);
									this.sumIncas += (double)Convert.ToInt32(this.numBatchAmount2.Value);
									this.countIncas++;
								}
								if (this.cmbOperation.SelectedIndex == 2)
								{
									CashBalance cashBalance1 = this._cashbalance;
									cashBalance1.AmountBalance = cashBalance1.AmountBalance - Convert.ToDouble(this.numBatchAmount2.Value);
									this.sumVidacha += (double)Convert.ToInt32(this.numBatchAmount2.Value);
									this.countVidacha++;
								}
								this._cashbalance.Save();
								amountBalance = this._cashbalance.AmountBalance;
								this.WriteLog(amountBalance.ToString());
								if (this.checkPrint3.Checked)
								{
									this.Print3(currentPeriod);
								}
								if (this.PrKKM == 1 && this.cmbOperation.SelectedIndex == 0)
								{
									this.PrintKKMDrugoe(currentPeriod);
								}
								this.FillOneItemOther(currentPeriod);
								this.FillCommonInfo();
								this.FillStatusBar();
								this.numBatchAmount2.Value = new decimal(0);
								this.txtFromWhom.Text = "";
								this.txtNote2.Text = "";
								this.cmbOperation.Focus();
							}
						}
						else
						{
							MessageBox.Show("Нельзя выдать денег больше, чем есть в кассе!", "Выдача денег", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							this.numBatchAmount2.Focus();
							return;
						}
					}
					else
					{
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void FillCommonInfo()
		{
			try
			{
				this.lblBatchAmount.Text = Convert.ToString(this._batch.BatchAmount + this.sumFromAgent + this.sumOther);
				this.lblBatchCount.Text = Convert.ToString(this._batch.BatchCount + this.countFromAgent + this.countOther);
				this.lblCashier.Text = this._batch.oCashier.get_Name();
			}
			catch
			{
			}
		}

		private void FillListView()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
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

		private void FillListViewAgent()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				foreach (Batch batch in this._batchsagent)
				{
					this.Cursor = Cursors.WaitCursor;
					ListViewItem listViewItem = new ListViewItem("нет")
					{
						Tag = batch.get_ID().ToString()
					};
					listViewItem.SubItems.Add(batch.oDispatcher.get_Name());
					listViewItem.SubItems.Add("нет");
					listViewItem.SubItems.Add(Convert.ToString(batch.BatchAmount));
					listViewItem.SubItems.Add(batch.oTypeBatch.get_Name());
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

		private void FillListViewOther()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				foreach (Batch batch in this._batchsother)
				{
					string name = batch.get_Name();
					char[] chr = new char[] { Convert.ToChar("@") };
					string[] strArrays = name.Split(chr);
					ListViewItem listViewItem = new ListViewItem(strArrays[0])
					{
						Tag = batch.get_ID().ToString()
					};
					listViewItem.SubItems.Add(strArrays[1]);
					listViewItem.SubItems.Add("нет");
					listViewItem.SubItems.Add(Convert.ToString(batch.BatchAmount));
					listViewItem.SubItems.Add(batch.oTypeBatch.get_Name());
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

		private void FillOneItem(Document o, bool isAdd)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (!isAdd)
				{
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
					this.lv.Items.Insert(0, listViewItem);
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

		private void FillOneItemAgent(Batch o)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				ListViewItem listViewItem = new ListViewItem("нет")
				{
					Tag = o.get_ID().ToString()
				};
				listViewItem.SubItems.Add(o.oDispatcher.get_Name());
				listViewItem.SubItems.Add("нет");
				listViewItem.SubItems.Add(Convert.ToString(o.BatchAmount));
				listViewItem.SubItems.Add(o.oTypeBatch.get_Name());
				this.lv.Items.Insert(0, listViewItem);
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка загрузки данных!", "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillOneItemOther(Batch o)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				string name = o.get_Name();
				char[] chr = new char[] { Convert.ToChar("@") };
				string[] strArrays = name.Split(chr);
				ListViewItem listViewItem = new ListViewItem(strArrays[0])
				{
					Tag = o.get_ID().ToString()
				};
				listViewItem.SubItems.Add(strArrays[1]);
				listViewItem.SubItems.Add("нет");
				listViewItem.SubItems.Add(Convert.ToString(o.BatchAmount));
				listViewItem.SubItems.Add(o.oTypeBatch.get_Name());
				this.lv.Items.Insert(0, listViewItem);
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
				this.statusBar1.Panels[0].Text = string.Concat("Итого в кассе: ", Convert.ToString(this._cashbalance.AmountBalance));
				this.statusBar1.Panels[1].Text = string.Concat("По абонентам: ", Convert.ToString(this._batch.BatchAmount), "/", Convert.ToString(this._batch.BatchCount));
				this.statusBar1.Panels[2].Text = string.Concat("По агентам: ", Convert.ToString(this.sumFromAgent), "/", Convert.ToString(this.countFromAgent));
				this.statusBar1.Panels[3].Text = string.Concat("Прочие: ", Convert.ToString(this.sumOther), "/", Convert.ToString(this.countOther));
				this.statusBar1.Panels[4].Text = string.Concat("Инкассация: ", Convert.ToString(this.sumIncas), "/", Convert.ToString(this.countIncas));
				this.statusBar1.Panels[5].Text = string.Concat("Выдача денег: ", Convert.ToString(this.sumVidacha), "/", Convert.ToString(this.countVidacha));
				this.statusBar1.Panels[6].Text = string.Concat("Всего платежей: ", Convert.ToString(this._batch.BatchCount + this.countFromAgent + this.countOther));
				this.statusBar1.Panels[7].Text = string.Concat("Картой: ", Convert.ToString(this._cardBatch.BatchCount), "/", Convert.ToString(this._cardBatch.BatchAmount));
			}
			catch
			{
			}
		}

		private void FindContract(string Account)
		{
			DateTime datedisplay;
			try
			{
				Contracts contract = new Contracts();
				if (contract.Load(Account) != 0)
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
					this.txtAbRNN.Text = this._contract.oPerson.RNN;
					if (this._contract.oPerson.isJuridical != 0)
					{
						this.lblFIO.Text = string.Concat(this._contract.oPerson.Name, ",", this._contract.oPerson.FullName);
					}
					else
					{
						this.lblFIO.Text = this._contract.oPerson.FullName;
					}
					this.lblAddress.Text = this._contract.oPerson.oAddress.get_ShortAddress();
					Label str = this.lblCountLives;
					int countLives = this._contract.oGobjects[0].CountLives;
					str.Text = countLives.ToString();
					this.lblBalance.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(), 2));
					this._gobject = this._contract.oGobjects[0];
					this._gmeter = this._gobject.GetActiveGMeter();
					if (this._gmeter == null)
					{
						this.lblPU.Text = "Не подключен";
						this.lblCurrentIndication.Text = "";
						this.numNewIndication.Value = new decimal(0);
						this.numNewIndication.Enabled = false;
						this._indication = null;
					}
					else if (this._gmeter.oTypeVerify.get_ID() != (long)2)
					{
						this.lblPU.Text = "Подключен";
						if (this._gmeter.GetCurrentIndication().oTypeIndication == null)
						{
							Label label = this.lblCurrentIndication;
							string str1 = Convert.ToString(this._gmeter.GetCurrentIndication().Display);
							datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
							label.Text = string.Concat(str1, " от ", datedisplay.ToShortDateString());
						}
						else
						{
							Label label1 = this.lblCurrentIndication;
							string[] shortDateString = new string[] { Convert.ToString(this._gmeter.GetCurrentIndication().Display), " от ", null, null, null };
							datedisplay = this._gmeter.GetCurrentIndication().Datedisplay;
							shortDateString[2] = datedisplay.ToShortDateString();
							shortDateString[3] = ", ";
							shortDateString[4] = this._gmeter.GetCurrentIndication().oTypeIndication.get_Name();
							label1.Text = string.Concat(shortDateString);
						}
						this.numNewIndication.Value = new decimal(0);
						this.numNewIndication.Enabled = true;
						this._indication = this._gmeter.oIndications.Add();
					}
					else
					{
						this.lblPU.Text = "Забракован";
						this.lblCurrentIndication.Text = "";
						this.numNewIndication.Value = new decimal(0);
						this.numNewIndication.Enabled = false;
						this._indication = null;
					}
					this.numAmount.Value = new decimal(0);
					this.numAmount.Focus();
				}
			}
			catch
			{
			}
		}

		private void FindContractUL(string Account)
		{
			try
			{
				Contracts contract = new Contracts();
				if (contract.Load(Account) != 0)
				{
					this.ResetFields2();
				}
				else if (contract.get_Count() <= 0)
				{
					this.ResetFields2();
				}
				else
				{
					this._contract = contract[0];
					this.txtIINul.Text = this._contract.oPerson.RNN;
					if (this._contract.oPerson.isJuridical != 0)
					{
						this.lblInfoUL.Text = string.Concat(this._contract.oPerson.Name, ",", this._contract.oPerson.FullName);
					}
					else
					{
						this.lblInfoUL.Text = this._contract.oPerson.FullName;
					}
					this.lblAddressUL.Text = this._contract.oPerson.oAddress.get_ShortAddress();
					this.lblBalanceUL.Text = Convert.ToString(Math.Round(this._contract.CurrentBalance(), 2));
					this._gobject = this._contract.oGobjects[0];
					this._gmeter = this._gobject.GetActiveGMeter();
					if (this._gmeter == null)
					{
						this._indication = null;
					}
					else if (this._gmeter.oTypeVerify.get_ID() != (long)2)
					{
						this._indication = this._gmeter.oIndications.Add();
					}
					else
					{
						this._indication = null;
					}
					this.numAmountUL.Value = new decimal(0);
					this.numAmountUL.Focus();
				}
			}
			catch
			{
			}
		}

		private void frmCash_Closing(object sender, CancelEventArgs e)
		{
			if (this.ECR != null)
			{
				this.ECR.DeviceEnabled = false;
				this.ECR.ResetMode();
			}
			this._batch.Save();
			Tools.SaveWindows(this);
		}

		private void frmCash_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F1:
				{
					this.tabControl1.SelectedIndex = 0;
					this.txtAccount.Focus();
					return;
				}
				case Keys.F2:
				{
					this.tabControl1.SelectedIndex = 1;
					this.numBatchAmount.Focus();
					return;
				}
				case Keys.F3:
				{
					this.tabControl1.SelectedIndex = 2;
					this.cmbOperation.Focus();
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void frmCash_Load(object sender, EventArgs e)
		{
			try
			{
				ListView listView = this.lv;
				Type[] typeArray = new Type[] { typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this._agents = new Agents();
				this._agents.Load();
				Tools.FillCMB(this._agents, this.cmbAgent, (long)0);
				this.FillListView();
				this.FillListViewAgent();
				this.FillListViewOther();
				this.FillCommonInfo();
				this.FillStatusBar();
				if (Tools.IsFirstWorkDay(DateTime.Today, false, Depot.CurrentPeriod.DateBegin))
				{
					this.numNewIndication.Enabled = false;
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmCash));
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.groupBox3 = new GroupBox();
			this.chkPayCard = new CheckBox();
			this.lblZdacha = new Label();
			this.nmcPoluch = new NumericUpDown();
			this.label4 = new Label();
			this.lblBalance = new Label();
			this.label9 = new Label();
			this.lblFactUse = new Label();
			this.label8 = new Label();
			this.numNewIndication = new NumericUpDown();
			this.numAmount = new NumericUpDown();
			this.cmdApply1 = new Button();
			this.checkPrint1 = new CheckBox();
			this.lblCurrentIndication = new Label();
			this.label18 = new Label();
			this.label19 = new Label();
			this.label20 = new Label();
			this.txtAccount = new C1TextBox();
			this.groupBox2 = new GroupBox();
			this.label30 = new Label();
			this.bRNN = new Button();
			this.imageList1 = new ImageList(this.components);
			this.txtAbRNN = new TextBox();
			this.cmdAddress = new Button();
			this.cmdAccount = new Button();
			this.lblCountLives = new Label();
			this.cmdPrint = new Button();
			this.lblPU = new Label();
			this.label13 = new Label();
			this.lblAddress = new Label();
			this.lblFIO = new Label();
			this.label10 = new Label();
			this.label11 = new Label();
			this.label12 = new Label();
			this.cmbAccount = new ComboBox();
			this.tabPage9 = new TabPage();
			this.groupBox10 = new GroupBox();
			this.txtBaseUL = new TextBox();
			this.label39 = new Label();
			this.lblZdachaUL = new Label();
			this.bApplyUL = new Button();
			this.numPoluchUL = new NumericUpDown();
			this.label38 = new Label();
			this.lblBalanceUL = new Label();
			this.label37 = new Label();
			this.numAmountUL = new NumericUpDown();
			this.label36 = new Label();
			this.txtAccountUL = new TextBox();
			this.groupBox9 = new GroupBox();
			this.cmbAccountUL = new ComboBox();
			this.bIINul = new Button();
			this.txtIINul = new TextBox();
			this.label35 = new Label();
			this.bAddressUL = new Button();
			this.lblAddressUL = new Label();
			this.label33 = new Label();
			this.lblInfoUL = new Label();
			this.label32 = new Label();
			this.bAccountUL = new Button();
			this.label31 = new Label();
			this.tabPage4 = new TabPage();
			this.groupBox8 = new GroupBox();
			this.numericUpDown2 = new NumericUpDown();
			this.button4 = new Button();
			this.checkBox1 = new CheckBox();
			this.label34 = new Label();
			this.groupBox7 = new GroupBox();
			this.button1 = new Button();
			this.c1TextBox1 = new C1TextBox();
			this.button2 = new Button();
			this.label7 = new Label();
			this.label14 = new Label();
			this.label16 = new Label();
			this.label28 = new Label();
			this.label29 = new Label();
			this.tabPage8 = new TabPage();
			this.tabPage7 = new TabPage();
			this.tabPage2 = new TabPage();
			this.groupBox5 = new GroupBox();
			this.numBatchCount = new NumericUpDown();
			this.numBatchAmount = new NumericUpDown();
			this.txtNote = new TextBox();
			this.label15 = new Label();
			this.cmbAgent = new ComboBox();
			this.cmdApply2 = new Button();
			this.checkPrint2 = new CheckBox();
			this.label24 = new Label();
			this.label25 = new Label();
			this.label26 = new Label();
			this.tabPage5 = new TabPage();
			this.tabPage6 = new TabPage();
			this.tabPage3 = new TabPage();
			this.groupBox6 = new GroupBox();
			this.bFindRNN = new Button();
			this.txtRNN = new TextBox();
			this.label6 = new Label();
			this.txtBaze = new TextBox();
			this.label5 = new Label();
			this.numBatchAmount2 = new NumericUpDown();
			this.cmbKorr = new ComboBox();
			this.label27 = new Label();
			this.txtNote2 = new TextBox();
			this.label17 = new Label();
			this.cmbOperation = new ComboBox();
			this.txtFromWhom = new TextBox();
			this.cmdApply3 = new Button();
			this.checkPrint3 = new CheckBox();
			this.label21 = new Label();
			this.label22 = new Label();
			this.label23 = new Label();
			this.imageList3 = new ImageList(this.components);
			this.toolTip1 = new ToolTip(this.components);
			this.statusBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.statusBarPanel3 = new StatusBarPanel();
			this.statusBarPanel4 = new StatusBarPanel();
			this.statusBarPanel6 = new StatusBarPanel();
			this.statusBarPanel7 = new StatusBarPanel();
			this.statusBarPanel5 = new StatusBarPanel();
			this.statusBarPanel8 = new StatusBarPanel();
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.groupBox4 = new GroupBox();
			this.panel1 = new Panel();
			this.tbData = new ToolBar();
			this.toolBarButton23 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.toolBarButton1 = new ToolBarButton();
			this.imageList2 = new ImageList(this.components);
			this.CommandHolder = new C1CommandHolder();
			this.cmd_toolBarButton26 = new C1Command();
			this.cmd_toolBarButton1 = new C1Command();
			this.cmd_toolBarButton23 = new C1Command();
			this.cmd_toolBarButton25 = new C1Command();
			this.groupBox1 = new GroupBox();
			this.lblCashier = new Label();
			this.lblBatchAmount = new Label();
			this.label3 = new Label();
			this.label1 = new Label();
			this.label2 = new Label();
			this.lblBatchCount = new Label();
			this.panel2 = new Panel();
			this.chkPayCardUL = new CheckBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((ISupportInitialize)this.nmcPoluch).BeginInit();
			((ISupportInitialize)this.numNewIndication).BeginInit();
			((ISupportInitialize)this.numAmount).BeginInit();
			((ISupportInitialize)this.txtAccount).BeginInit();
			this.groupBox2.SuspendLayout();
			this.tabPage9.SuspendLayout();
			this.groupBox10.SuspendLayout();
			((ISupportInitialize)this.numPoluchUL).BeginInit();
			((ISupportInitialize)this.numAmountUL).BeginInit();
			this.groupBox9.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.groupBox8.SuspendLayout();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			this.groupBox7.SuspendLayout();
			((ISupportInitialize)this.c1TextBox1).BeginInit();
			this.tabPage2.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((ISupportInitialize)this.numBatchCount).BeginInit();
			((ISupportInitialize)this.numBatchAmount).BeginInit();
			this.tabPage3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((ISupportInitialize)this.numBatchAmount2).BeginInit();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			((ISupportInitialize)this.statusBarPanel3).BeginInit();
			((ISupportInitialize)this.statusBarPanel4).BeginInit();
			((ISupportInitialize)this.statusBarPanel6).BeginInit();
			((ISupportInitialize)this.statusBarPanel7).BeginInit();
			((ISupportInitialize)this.statusBarPanel5).BeginInit();
			((ISupportInitialize)this.statusBarPanel8).BeginInit();
			this.groupBox4.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.CommandHolder).BeginInit();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Appearance = TabAppearance.Buttons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage9);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage8);
			this.tabControl1.Controls.Add(this.tabPage7);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Cursor = Cursors.Default;
			this.tabControl1.ImageList = this.imageList3;
			this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.tabControl1.Location = new Point(4, 4);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new Point(10, 6);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(300, 488);
			this.tabControl1.SizeMode = TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 1;
			this.tabPage1.BackColor = SystemColors.Control;
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.ForeColor = SystemColors.ActiveCaption;
			this.tabPage1.Location = new Point(4, 94);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tabPage1.Size = new System.Drawing.Size(292, 390);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "От абонентов F1";
			this.groupBox3.Controls.Add(this.chkPayCard);
			this.groupBox3.Controls.Add(this.lblZdacha);
			this.groupBox3.Controls.Add(this.nmcPoluch);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.lblBalance);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.lblFactUse);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.numNewIndication);
			this.groupBox3.Controls.Add(this.numAmount);
			this.groupBox3.Controls.Add(this.cmdApply1);
			this.groupBox3.Controls.Add(this.checkPrint1);
			this.groupBox3.Controls.Add(this.lblCurrentIndication);
			this.groupBox3.Controls.Add(this.label18);
			this.groupBox3.Controls.Add(this.label19);
			this.groupBox3.Controls.Add(this.label20);
			this.groupBox3.Controls.Add(this.txtAccount);
			this.groupBox3.ForeColor = SystemColors.Desktop;
			this.groupBox3.Location = new Point(4, 136);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(280, 248);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Платеж";
			this.chkPayCard.FlatStyle = FlatStyle.Flat;
			this.chkPayCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.chkPayCard.Location = new Point(8, 164);
			this.chkPayCard.Name = "chkPayCard";
			this.chkPayCard.TabIndex = 12;
			this.chkPayCard.Text = "Оплата картой";
			this.chkPayCard.CheckedChanged += new EventHandler(this.chkPayCard_CheckedChanged);
			this.lblZdacha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblZdacha.ForeColor = Color.Red;
			this.lblZdacha.Location = new Point(8, 216);
			this.lblZdacha.Name = "lblZdacha";
			this.lblZdacha.Size = new System.Drawing.Size(264, 36);
			this.lblZdacha.TabIndex = 11;
			this.nmcPoluch.BorderStyle = BorderStyle.FixedSingle;
			this.nmcPoluch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.nmcPoluch.Location = new Point(152, 144);
			NumericUpDown num = this.nmcPoluch;
			int[] numArray = new int[] { 9999999, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown = this.nmcPoluch;
			numArray = new int[] { 9999999, 0, 0, -2147483648 };
			numericUpDown.Minimum = new decimal(numArray);
			this.nmcPoluch.Name = "nmcPoluch";
			this.nmcPoluch.TabIndex = 3;
			this.nmcPoluch.KeyPress += new KeyPressEventHandler(this.nmcPoluch_KeyPress);
			this.nmcPoluch.Enter += new EventHandler(this.nmcPoluch_Enter);
			this.nmcPoluch.Leave += new EventHandler(this.nmcPoluch_Leave);
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label4.ForeColor = SystemColors.ControlText;
			this.label4.Location = new Point(8, 144);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Получено, тенге";
			this.lblBalance.BackColor = SystemColors.Info;
			this.lblBalance.BorderStyle = BorderStyle.FixedSingle;
			this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblBalance.ForeColor = SystemColors.ControlText;
			this.lblBalance.Location = new Point(152, 44);
			this.lblBalance.Name = "lblBalance";
			this.lblBalance.Size = new System.Drawing.Size(120, 20);
			this.lblBalance.TabIndex = 8;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label9.ForeColor = SystemColors.ControlText;
			this.label9.Location = new Point(8, 48);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(128, 16);
			this.label9.TabIndex = 7;
			this.label9.Text = "Текущий баланс";
			this.lblFactUse.BackColor = SystemColors.Info;
			this.lblFactUse.BorderStyle = BorderStyle.FixedSingle;
			this.lblFactUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblFactUse.ForeColor = SystemColors.ControlText;
			this.lblFactUse.Location = new Point(152, 120);
			this.lblFactUse.Name = "lblFactUse";
			this.lblFactUse.Size = new System.Drawing.Size(120, 20);
			this.lblFactUse.TabIndex = 6;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label8.ForeColor = SystemColors.ControlText;
			this.label8.Location = new Point(8, 120);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 16);
			this.label8.TabIndex = 5;
			this.label8.Text = "Потребление";
			this.numNewIndication.BorderStyle = BorderStyle.FixedSingle;
			this.numNewIndication.DecimalPlaces = 2;
			this.numNewIndication.Enabled = false;
			this.numNewIndication.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numNewIndication.Location = new Point(152, 92);
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
			this.numAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numAmount.Location = new Point(152, 16);
			NumericUpDown numericUpDown1 = this.numAmount;
			numArray = new int[] { 99999999, 0, 0, 0 };
			numericUpDown1.Maximum = new decimal(numArray);
			NumericUpDown num2 = this.numAmount;
			numArray = new int[] { 99999999, 0, 0, -2147483648 };
			num2.Minimum = new decimal(numArray);
			this.numAmount.Name = "numAmount";
			this.numAmount.TabIndex = 1;
			NumericUpDown numericUpDown2 = this.numAmount;
			numArray = new int[] { 99999999, 0, 0, 0 };
			numericUpDown2.Value = new decimal(numArray);
			this.numAmount.KeyPress += new KeyPressEventHandler(this.numAmount_KeyPress);
			this.numAmount.Enter += new EventHandler(this.numAmount_Enter);
			this.cmdApply1.FlatStyle = FlatStyle.Flat;
			this.cmdApply1.ForeColor = SystemColors.ControlText;
			this.cmdApply1.Location = new Point(152, 188);
			this.cmdApply1.Name = "cmdApply1";
			this.cmdApply1.Size = new System.Drawing.Size(120, 24);
			this.cmdApply1.TabIndex = 5;
			this.cmdApply1.Text = "Принять";
			this.cmdApply1.Click += new EventHandler(this.cmdApply1_Click);
			this.checkPrint1.FlatStyle = FlatStyle.Flat;
			this.checkPrint1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.checkPrint1.ForeColor = SystemColors.ControlText;
			this.checkPrint1.Location = new Point(8, 192);
			this.checkPrint1.Name = "checkPrint1";
			this.checkPrint1.Size = new System.Drawing.Size(104, 16);
			this.checkPrint1.TabIndex = 4;
			this.checkPrint1.Text = "Печать ПКО";
			this.checkPrint1.KeyPress += new KeyPressEventHandler(this.checkPrint1_KeyPress);
			this.lblCurrentIndication.BackColor = SystemColors.Info;
			this.lblCurrentIndication.BorderStyle = BorderStyle.FixedSingle;
			this.lblCurrentIndication.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblCurrentIndication.ForeColor = SystemColors.ControlText;
			this.lblCurrentIndication.Location = new Point(120, 68);
			this.lblCurrentIndication.Name = "lblCurrentIndication";
			this.lblCurrentIndication.Size = new System.Drawing.Size(152, 20);
			this.lblCurrentIndication.TabIndex = 2;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label18.ForeColor = SystemColors.ControlText;
			this.label18.Location = new Point(8, 96);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(120, 16);
			this.label18.TabIndex = 2;
			this.label18.Text = "Новые показания";
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label19.ForeColor = SystemColors.ControlText;
			this.label19.Location = new Point(8, 72);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(104, 16);
			this.label19.TabIndex = 1;
			this.label19.Text = "Тек. показания";
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label20.ForeColor = SystemColors.ControlText;
			this.label20.Location = new Point(8, 16);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(96, 16);
			this.label20.TabIndex = 0;
			this.label20.Text = "Сумма, тенге";
			this.txtAccount.BorderStyle = 1;
			this.txtAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAccount.Location = new Point(116, 16);
			this.txtAccount.Name = "txtAccount";
			this.txtAccount.Size = new System.Drawing.Size(32, 21);
			this.txtAccount.TabIndex = 1;
			this.txtAccount.Tag = null;
			this.txtAccount.Visible = false;
			this.txtAccount.TextChanged += new EventHandler(this.txtAccount_TextChanged);
			this.txtAccount.KeyPress += new KeyPressEventHandler(this.txtAccount_KeyPress);
			this.txtAccount.Enter += new EventHandler(this.txtAccount_Enter);
			this.txtAccount.Leave += new EventHandler(this.txtAccount_Leave);
			this.groupBox2.Controls.Add(this.label30);
			this.groupBox2.Controls.Add(this.bRNN);
			this.groupBox2.Controls.Add(this.txtAbRNN);
			this.groupBox2.Controls.Add(this.cmdAddress);
			this.groupBox2.Controls.Add(this.cmdAccount);
			this.groupBox2.Controls.Add(this.lblCountLives);
			this.groupBox2.Controls.Add(this.cmdPrint);
			this.groupBox2.Controls.Add(this.lblPU);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblAddress);
			this.groupBox2.Controls.Add(this.lblFIO);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.cmbAccount);
			this.groupBox2.ForeColor = SystemColors.Desktop;
			this.groupBox2.Location = new Point(4, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(280, 136);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Абонент";
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label30.ForeColor = SystemColors.ControlText;
			this.label30.Location = new Point(8, 112);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(40, 16);
			this.label30.TabIndex = 13;
			this.label30.Text = "РНН";
			this.bRNN.FlatStyle = FlatStyle.Flat;
			this.bRNN.ForeColor = SystemColors.ControlText;
			this.bRNN.ImageIndex = 0;
			this.bRNN.ImageList = this.imageList1;
			this.bRNN.Location = new Point(251, 112);
			this.bRNN.Name = "bRNN";
			this.bRNN.Size = new System.Drawing.Size(20, 20);
			this.bRNN.TabIndex = 12;
			this.bRNN.Click += new EventHandler(this.bRNN_Click);
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.txtAbRNN.BorderStyle = BorderStyle.FixedSingle;
			this.txtAbRNN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtAbRNN.Location = new Point(48, 111);
			this.txtAbRNN.Name = "txtAbRNN";
			this.txtAbRNN.Size = new System.Drawing.Size(200, 22);
			this.txtAbRNN.TabIndex = 11;
			this.txtAbRNN.Text = "";
			this.txtAbRNN.KeyPress += new KeyPressEventHandler(this.txtAbRNN_KeyPress);
			this.cmdAddress.FlatStyle = FlatStyle.Flat;
			this.cmdAddress.ForeColor = SystemColors.ControlText;
			this.cmdAddress.ImageIndex = 0;
			this.cmdAddress.ImageList = this.imageList1;
			this.cmdAddress.Location = new Point(251, 64);
			this.cmdAddress.Name = "cmdAddress";
			this.cmdAddress.Size = new System.Drawing.Size(20, 20);
			this.cmdAddress.TabIndex = 4;
			this.cmdAddress.Click += new EventHandler(this.cmdAddress_Click);
			this.cmdAccount.FlatStyle = FlatStyle.Flat;
			this.cmdAccount.ForeColor = SystemColors.ControlText;
			this.cmdAccount.ImageIndex = 0;
			this.cmdAccount.ImageList = this.imageList1;
			this.cmdAccount.Location = new Point(158, 16);
			this.cmdAccount.Name = "cmdAccount";
			this.cmdAccount.Size = new System.Drawing.Size(22, 22);
			this.cmdAccount.TabIndex = 2;
			this.cmdAccount.Click += new EventHandler(this.cmdAccount_Click);
			this.lblCountLives.BackColor = SystemColors.Info;
			this.lblCountLives.BorderStyle = BorderStyle.FixedSingle;
			this.lblCountLives.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblCountLives.ForeColor = SystemColors.ControlText;
			this.lblCountLives.Location = new Point(96, 88);
			this.lblCountLives.Name = "lblCountLives";
			this.lblCountLives.Size = new System.Drawing.Size(40, 20);
			this.lblCountLives.TabIndex = 10;
			this.cmdPrint.FlatStyle = FlatStyle.Flat;
			this.cmdPrint.ForeColor = SystemColors.ControlText;
			this.cmdPrint.Location = new Point(180, 16);
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.Size = new System.Drawing.Size(92, 22);
			this.cmdPrint.TabIndex = 3;
			this.cmdPrint.Text = "Печать";
			this.lblPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblPU.ForeColor = SystemColors.ControlText;
			this.lblPU.Location = new Point(152, 88);
			this.lblPU.Name = "lblPU";
			this.lblPU.Size = new System.Drawing.Size(120, 20);
			this.lblPU.TabIndex = 7;
			this.toolTip1.SetToolTip(this.lblPU, "Статус ПУ");
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label13.ForeColor = SystemColors.ControlText;
			this.label13.Location = new Point(8, 88);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(80, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Проживает";
			this.lblAddress.BackColor = SystemColors.Info;
			this.lblAddress.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblAddress.ForeColor = SystemColors.ControlText;
			this.lblAddress.Location = new Point(48, 64);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(200, 20);
			this.lblAddress.TabIndex = 5;
			this.lblFIO.BackColor = SystemColors.Info;
			this.lblFIO.BorderStyle = BorderStyle.FixedSingle;
			this.lblFIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
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
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label11.ForeColor = SystemColors.ControlText;
			this.label11.Location = new Point(8, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(40, 16);
			this.label11.TabIndex = 1;
			this.label11.Text = "ФИО";
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label12.ForeColor = SystemColors.ControlText;
			this.label12.Location = new Point(8, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 16);
			this.label12.TabIndex = 0;
			this.label12.Text = "Л/с";
			this.cmbAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.cmbAccount.Location = new Point(48, 16);
			this.cmbAccount.Name = "cmbAccount";
			this.cmbAccount.Size = new System.Drawing.Size(112, 24);
			this.cmbAccount.TabIndex = 12;
			this.cmbAccount.KeyPress += new KeyPressEventHandler(this.cmbAccount_KeyPress);
			this.cmbAccount.SelectedIndexChanged += new EventHandler(this.cmbAccount_SelectedIndexChanged);
			this.cmbAccount.Leave += new EventHandler(this.cmbAccount_Leave);
			this.tabPage9.Controls.Add(this.groupBox10);
			this.tabPage9.Controls.Add(this.groupBox9);
			this.tabPage9.ForeColor = SystemColors.Desktop;
			this.tabPage9.Location = new Point(4, 94);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Size = new System.Drawing.Size(292, 390);
			this.tabPage9.TabIndex = 8;
			this.tabPage9.Text = "За доп. услуги";
			this.groupBox10.Controls.Add(this.chkPayCardUL);
			this.groupBox10.Controls.Add(this.txtBaseUL);
			this.groupBox10.Controls.Add(this.label39);
			this.groupBox10.Controls.Add(this.lblZdachaUL);
			this.groupBox10.Controls.Add(this.bApplyUL);
			this.groupBox10.Controls.Add(this.numPoluchUL);
			this.groupBox10.Controls.Add(this.label38);
			this.groupBox10.Controls.Add(this.lblBalanceUL);
			this.groupBox10.Controls.Add(this.label37);
			this.groupBox10.Controls.Add(this.numAmountUL);
			this.groupBox10.Controls.Add(this.label36);
			this.groupBox10.Controls.Add(this.txtAccountUL);
			this.groupBox10.Location = new Point(8, 136);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(280, 248);
			this.groupBox10.TabIndex = 1;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Платеж";
			this.txtBaseUL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.txtBaseUL.Location = new Point(112, 90);
			this.txtBaseUL.Multiline = true;
			this.txtBaseUL.Name = "txtBaseUL";
			this.txtBaseUL.ScrollBars = ScrollBars.Vertical;
			this.txtBaseUL.Size = new System.Drawing.Size(160, 78);
			this.txtBaseUL.TabIndex = 9;
			this.txtBaseUL.Text = "";
			this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label39.ForeColor = SystemColors.ControlText;
			this.label39.Location = new Point(8, 90);
			this.label39.Name = "label39";
			this.label39.TabIndex = 8;
			this.label39.Text = "Основание";
			this.lblZdachaUL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.lblZdachaUL.ForeColor = Color.Red;
			this.lblZdachaUL.Location = new Point(8, 208);
			this.lblZdachaUL.Name = "lblZdachaUL";
			this.lblZdachaUL.Size = new System.Drawing.Size(264, 32);
			this.lblZdachaUL.TabIndex = 7;
			this.bApplyUL.FlatStyle = FlatStyle.Flat;
			this.bApplyUL.ForeColor = SystemColors.ControlText;
			this.bApplyUL.Location = new Point(152, 176);
			this.bApplyUL.Name = "bApplyUL";
			this.bApplyUL.Size = new System.Drawing.Size(120, 23);
			this.bApplyUL.TabIndex = 6;
			this.bApplyUL.Text = "Принять";
			this.bApplyUL.Click += new EventHandler(this.bApplyUL_Click);
			this.numPoluchUL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numPoluchUL.Location = new Point(144, 65);
			NumericUpDown num3 = this.numPoluchUL;
			numArray = new int[] { 99999999, 0, 0, 0 };
			num3.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown3 = this.numPoluchUL;
			numArray = new int[] { 99999999, 0, 0, -2147483648 };
			numericUpDown3.Minimum = new decimal(numArray);
			this.numPoluchUL.Name = "numPoluchUL";
			this.numPoluchUL.Size = new System.Drawing.Size(128, 22);
			this.numPoluchUL.TabIndex = 5;
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label38.ForeColor = SystemColors.ControlText;
			this.label38.Location = new Point(8, 65);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(120, 23);
			this.label38.TabIndex = 4;
			this.label38.Text = "Получено, тенге";
			this.lblBalanceUL.BorderStyle = BorderStyle.FixedSingle;
			this.lblBalanceUL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lblBalanceUL.ForeColor = SystemColors.ControlText;
			this.lblBalanceUL.Location = new Point(144, 40);
			this.lblBalanceUL.Name = "lblBalanceUL";
			this.lblBalanceUL.Size = new System.Drawing.Size(128, 23);
			this.lblBalanceUL.TabIndex = 3;
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label37.ForeColor = SystemColors.ControlText;
			this.label37.Location = new Point(8, 40);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(112, 23);
			this.label37.TabIndex = 2;
			this.label37.Text = "Текущий баланс";
			this.numAmountUL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numAmountUL.Location = new Point(144, 16);
			NumericUpDown num4 = this.numAmountUL;
			numArray = new int[] { 99999999, 0, 0, 0 };
			num4.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown4 = this.numAmountUL;
			numArray = new int[] { 99999999, 0, 0, -2147483648 };
			numericUpDown4.Minimum = new decimal(numArray);
			this.numAmountUL.Name = "numAmountUL";
			this.numAmountUL.Size = new System.Drawing.Size(128, 22);
			this.numAmountUL.TabIndex = 1;
			NumericUpDown num5 = this.numAmountUL;
			numArray = new int[] { 99999999, 0, 0, 0 };
			num5.Value = new decimal(numArray);
			this.numAmountUL.KeyPress += new KeyPressEventHandler(this.numAmountUL_KeyPress);
			this.numAmountUL.Enter += new EventHandler(this.numAmountUL_Enter);
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label36.ForeColor = SystemColors.ControlText;
			this.label36.Location = new Point(8, 16);
			this.label36.Name = "label36";
			this.label36.TabIndex = 0;
			this.label36.Text = "Сумма, тенге";
			this.txtAccountUL.BorderStyle = BorderStyle.FixedSingle;
			this.txtAccountUL.Location = new Point(24, 136);
			this.txtAccountUL.Name = "txtAccountUL";
			this.txtAccountUL.Size = new System.Drawing.Size(56, 20);
			this.txtAccountUL.TabIndex = 1;
			this.txtAccountUL.Text = "";
			this.txtAccountUL.Visible = false;
			this.txtAccountUL.KeyPress += new KeyPressEventHandler(this.txtAccountUL_KeyPress);
			this.txtAccountUL.Leave += new EventHandler(this.txtAccountUL_Leave);
			this.txtAccountUL.Enter += new EventHandler(this.txtAccountUL_Enter);
			this.groupBox9.Controls.Add(this.cmbAccountUL);
			this.groupBox9.Controls.Add(this.bIINul);
			this.groupBox9.Controls.Add(this.txtIINul);
			this.groupBox9.Controls.Add(this.label35);
			this.groupBox9.Controls.Add(this.bAddressUL);
			this.groupBox9.Controls.Add(this.lblAddressUL);
			this.groupBox9.Controls.Add(this.label33);
			this.groupBox9.Controls.Add(this.lblInfoUL);
			this.groupBox9.Controls.Add(this.label32);
			this.groupBox9.Controls.Add(this.bAccountUL);
			this.groupBox9.Controls.Add(this.label31);
			this.groupBox9.Location = new Point(8, 8);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(280, 128);
			this.groupBox9.TabIndex = 0;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Абонент (юр. лицо)";
			this.cmbAccountUL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.cmbAccountUL.Location = new Point(56, 16);
			this.cmbAccountUL.Name = "cmbAccountUL";
			this.cmbAccountUL.Size = new System.Drawing.Size(121, 24);
			this.cmbAccountUL.TabIndex = 11;
			this.cmbAccountUL.KeyPress += new KeyPressEventHandler(this.cmbAccountUL_KeyPress);
			this.cmbAccountUL.SelectedIndexChanged += new EventHandler(this.cmbAccountUL_SelectedIndexChanged);
			this.cmbAccountUL.Leave += new EventHandler(this.cmbAccountUL_Leave);
			this.bIINul.FlatStyle = FlatStyle.Flat;
			this.bIINul.ForeColor = SystemColors.ControlText;
			this.bIINul.ImageIndex = 0;
			this.bIINul.ImageList = this.imageList1;
			this.bIINul.Location = new Point(248, 94);
			this.bIINul.Name = "bIINul";
			this.bIINul.Size = new System.Drawing.Size(24, 22);
			this.bIINul.TabIndex = 10;
			this.bIINul.Click += new EventHandler(this.bIINul_Click);
			this.txtIINul.BorderStyle = BorderStyle.FixedSingle;
			this.txtIINul.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.txtIINul.Location = new Point(56, 94);
			this.txtIINul.Name = "txtIINul";
			this.txtIINul.Size = new System.Drawing.Size(192, 22);
			this.txtIINul.TabIndex = 9;
			this.txtIINul.Text = "";
			this.txtIINul.KeyPress += new KeyPressEventHandler(this.txtIINul_KeyPress);
			this.txtIINul.KeyUp += new KeyEventHandler(this.txtIINul_KeyUp);
			this.txtIINul.Enter += new EventHandler(this.txtIINul_Enter);
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label35.ForeColor = SystemColors.ControlText;
			this.label35.Location = new Point(8, 96);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(40, 23);
			this.label35.TabIndex = 8;
			this.label35.Text = "ИИН";
			this.bAddressUL.FlatStyle = FlatStyle.Flat;
			this.bAddressUL.ForeColor = SystemColors.ControlText;
			this.bAddressUL.ImageIndex = 0;
			this.bAddressUL.ImageList = this.imageList1;
			this.bAddressUL.Location = new Point(248, 67);
			this.bAddressUL.Name = "bAddressUL";
			this.bAddressUL.Size = new System.Drawing.Size(24, 23);
			this.bAddressUL.TabIndex = 7;
			this.bAddressUL.Click += new EventHandler(this.bAddressUL_Click);
			this.lblAddressUL.BackColor = SystemColors.Info;
			this.lblAddressUL.BorderStyle = BorderStyle.FixedSingle;
			this.lblAddressUL.Location = new Point(56, 67);
			this.lblAddressUL.Name = "lblAddressUL";
			this.lblAddressUL.Size = new System.Drawing.Size(192, 23);
			this.lblAddressUL.TabIndex = 6;
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label33.ForeColor = SystemColors.ControlText;
			this.label33.Location = new Point(8, 67);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(48, 23);
			this.label33.TabIndex = 5;
			this.label33.Text = "Адрес";
			this.lblInfoUL.BackColor = SystemColors.Info;
			this.lblInfoUL.BorderStyle = BorderStyle.FixedSingle;
			this.lblInfoUL.ForeColor = SystemColors.ControlText;
			this.lblInfoUL.Location = new Point(56, 40);
			this.lblInfoUL.Name = "lblInfoUL";
			this.lblInfoUL.Size = new System.Drawing.Size(216, 23);
			this.lblInfoUL.TabIndex = 4;
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label32.ForeColor = SystemColors.ControlText;
			this.label32.Location = new Point(8, 40);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(48, 23);
			this.label32.TabIndex = 3;
			this.label32.Text = "Наим.";
			this.bAccountUL.FlatStyle = FlatStyle.Flat;
			this.bAccountUL.ForeColor = SystemColors.ControlText;
			this.bAccountUL.ImageIndex = 0;
			this.bAccountUL.ImageList = this.imageList1;
			this.bAccountUL.Location = new Point(184, 16);
			this.bAccountUL.Name = "bAccountUL";
			this.bAccountUL.Size = new System.Drawing.Size(22, 22);
			this.bAccountUL.TabIndex = 2;
			this.bAccountUL.Click += new EventHandler(this.bAccountUL_Click);
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label31.ForeColor = SystemColors.ControlText;
			this.label31.Location = new Point(8, 16);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(32, 23);
			this.label31.TabIndex = 0;
			this.label31.Text = "Л/с";
			this.tabPage4.BackColor = SystemColors.ActiveBorder;
			this.tabPage4.Controls.Add(this.groupBox8);
			this.tabPage4.Controls.Add(this.groupBox7);
			this.tabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.tabPage4.ForeColor = SystemColors.ControlText;
			this.tabPage4.ImageIndex = 14;
			this.tabPage4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.tabPage4.Location = new Point(4, 94);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tabPage4.Size = new System.Drawing.Size(292, 390);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Мусор1";
			this.groupBox8.BackColor = SystemColors.ActiveBorder;
			this.groupBox8.Controls.Add(this.numericUpDown2);
			this.groupBox8.Controls.Add(this.button4);
			this.groupBox8.Controls.Add(this.checkBox1);
			this.groupBox8.Controls.Add(this.label34);
			this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.groupBox8.ForeColor = SystemColors.Desktop;
			this.groupBox8.Location = new Point(0, 96);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(288, 80);
			this.groupBox8.TabIndex = 3;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Платеж";
			this.numericUpDown2.BorderStyle = BorderStyle.FixedSingle;
			this.numericUpDown2.DecimalPlaces = 2;
			this.numericUpDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numericUpDown2.Location = new Point(148, 16);
			NumericUpDown numericUpDown5 = this.numericUpDown2;
			numArray = new int[] { 999999999, 0, 0, 0 };
			numericUpDown5.Maximum = new decimal(numArray);
			NumericUpDown num6 = this.numericUpDown2;
			numArray = new int[] { 999999999, 0, 0, -2147483648 };
			num6.Minimum = new decimal(numArray);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.TabIndex = 1;
			NumericUpDown numericUpDown6 = this.numericUpDown2;
			numArray = new int[] { 999999999, 0, 0, 0 };
			numericUpDown6.Value = new decimal(numArray);
			this.button4.FlatStyle = FlatStyle.Flat;
			this.button4.ForeColor = SystemColors.ControlText;
			this.button4.Location = new Point(148, 44);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(120, 24);
			this.button4.TabIndex = 4;
			this.button4.Text = "Принять";
			this.checkBox1.FlatStyle = FlatStyle.Flat;
			this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.checkBox1.ForeColor = SystemColors.ControlText;
			this.checkBox1.Location = new Point(12, 52);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(104, 16);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "Печать ПКО";
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label34.ForeColor = SystemColors.ControlText;
			this.label34.Location = new Point(8, 16);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(96, 16);
			this.label34.TabIndex = 0;
			this.label34.Text = "Сумма, тенге";
			this.groupBox7.BackColor = SystemColors.Control;
			this.groupBox7.Controls.Add(this.button1);
			this.groupBox7.Controls.Add(this.c1TextBox1);
			this.groupBox7.Controls.Add(this.button2);
			this.groupBox7.Controls.Add(this.label7);
			this.groupBox7.Controls.Add(this.label14);
			this.groupBox7.Controls.Add(this.label16);
			this.groupBox7.Controls.Add(this.label28);
			this.groupBox7.Controls.Add(this.label29);
			this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.groupBox7.ForeColor = SystemColors.Desktop;
			this.groupBox7.Location = new Point(0, 0);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(288, 96);
			this.groupBox7.TabIndex = 2;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Абонент";
			this.groupBox7.Visible = false;
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.ForeColor = SystemColors.ControlText;
			this.button1.ImageIndex = 0;
			this.button1.ImageList = this.imageList1;
			this.button1.Location = new Point(260, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(20, 20);
			this.button1.TabIndex = 4;
			this.c1TextBox1.BorderStyle = 1;
			this.c1TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.c1TextBox1.Location = new Point(48, 16);
			this.c1TextBox1.Name = "c1TextBox1";
			this.c1TextBox1.Size = new System.Drawing.Size(208, 21);
			this.c1TextBox1.TabIndex = 1;
			this.c1TextBox1.Tag = null;
			this.button2.FlatStyle = FlatStyle.Flat;
			this.button2.ForeColor = SystemColors.ControlText;
			this.button2.ImageIndex = 0;
			this.button2.ImageList = this.imageList1;
			this.button2.Location = new Point(260, 16);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(20, 20);
			this.button2.TabIndex = 2;
			this.label7.BackColor = SystemColors.Info;
			this.label7.BorderStyle = BorderStyle.FixedSingle;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label7.ForeColor = SystemColors.ControlText;
			this.label7.Location = new Point(48, 64);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(208, 20);
			this.label7.TabIndex = 5;
			this.label14.BackColor = SystemColors.Info;
			this.label14.BorderStyle = BorderStyle.FixedSingle;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label14.ForeColor = SystemColors.ControlText;
			this.label14.Location = new Point(48, 40);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(232, 20);
			this.label14.TabIndex = 4;
			this.label16.ForeColor = SystemColors.ControlText;
			this.label16.Location = new Point(8, 64);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(40, 16);
			this.label16.TabIndex = 2;
			this.label16.Text = "Адрес";
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label28.ForeColor = SystemColors.ControlText;
			this.label28.Location = new Point(8, 40);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(40, 16);
			this.label28.TabIndex = 1;
			this.label28.Text = "ФИО";
			this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.label29.ForeColor = Color.Black;
			this.label29.Location = new Point(8, 16);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(40, 16);
			this.label29.TabIndex = 0;
			this.label29.Text = "Л/с";
			this.tabPage8.ImageIndex = 8;
			this.tabPage8.Location = new Point(4, 94);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Size = new System.Drawing.Size(292, 390);
			this.tabPage8.TabIndex = 7;
			this.tabPage8.Text = "Мусор3";
			this.tabPage7.ImageIndex = 15;
			this.tabPage7.Location = new Point(4, 94);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Size = new System.Drawing.Size(292, 390);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "Мусор2";
			this.tabPage2.Controls.Add(this.groupBox5);
			this.tabPage2.ForeColor = SystemColors.Desktop;
			this.tabPage2.Location = new Point(4, 94);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(292, 390);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "От агентов F2";
			this.groupBox5.Controls.Add(this.numBatchCount);
			this.groupBox5.Controls.Add(this.numBatchAmount);
			this.groupBox5.Controls.Add(this.txtNote);
			this.groupBox5.Controls.Add(this.label15);
			this.groupBox5.Controls.Add(this.cmbAgent);
			this.groupBox5.Controls.Add(this.cmdApply2);
			this.groupBox5.Controls.Add(this.checkPrint2);
			this.groupBox5.Controls.Add(this.label24);
			this.groupBox5.Controls.Add(this.label25);
			this.groupBox5.Controls.Add(this.label26);
			this.groupBox5.ForeColor = SystemColors.Desktop;
			this.groupBox5.Location = new Point(4, 8);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(280, 288);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Платеж";
			this.numBatchCount.BorderStyle = BorderStyle.FixedSingle;
			this.numBatchCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numBatchCount.Location = new Point(136, 40);
			NumericUpDown num7 = this.numBatchCount;
			numArray = new int[] { 999999, 0, 0, 0 };
			num7.Maximum = new decimal(numArray);
			this.numBatchCount.Name = "numBatchCount";
			this.numBatchCount.Size = new System.Drawing.Size(136, 23);
			this.numBatchCount.TabIndex = 2;
			this.numBatchCount.KeyPress += new KeyPressEventHandler(this.numBatchCount_KeyPress);
			this.numBatchCount.Enter += new EventHandler(this.numBatchCount_Enter);
			this.numBatchAmount.BorderStyle = BorderStyle.FixedSingle;
			this.numBatchAmount.DecimalPlaces = 2;
			this.numBatchAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numBatchAmount.Location = new Point(136, 16);
			NumericUpDown numericUpDown7 = this.numBatchAmount;
			numArray = new int[] { 99999999, 0, 0, 0 };
			numericUpDown7.Maximum = new decimal(numArray);
			this.numBatchAmount.Name = "numBatchAmount";
			this.numBatchAmount.Size = new System.Drawing.Size(136, 23);
			this.numBatchAmount.TabIndex = 1;
			this.numBatchAmount.KeyPress += new KeyPressEventHandler(this.numBatchAmount_KeyPress);
			this.numBatchAmount.Enter += new EventHandler(this.numBatchAmount_Enter);
			this.txtNote.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote.Location = new Point(8, 104);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.Size = new System.Drawing.Size(264, 144);
			this.txtNote.TabIndex = 4;
			this.txtNote.Text = "";
			this.txtNote.KeyPress += new KeyPressEventHandler(this.txtNote_KeyPress);
			this.label15.ForeColor = SystemColors.ControlText;
			this.label15.Location = new Point(8, 88);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(96, 16);
			this.label15.TabIndex = 14;
			this.label15.Text = "Примечание";
			this.cmbAgent.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbAgent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAgent.Location = new Point(56, 64);
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.Size = new System.Drawing.Size(216, 24);
			this.cmbAgent.TabIndex = 3;
			this.cmbAgent.KeyPress += new KeyPressEventHandler(this.cmbAgent_KeyPress);
			this.cmdApply2.FlatStyle = FlatStyle.Flat;
			this.cmdApply2.ForeColor = SystemColors.ControlText;
			this.cmdApply2.Location = new Point(152, 256);
			this.cmdApply2.Name = "cmdApply2";
			this.cmdApply2.Size = new System.Drawing.Size(120, 24);
			this.cmdApply2.TabIndex = 6;
			this.cmdApply2.Text = "Принять";
			this.cmdApply2.Click += new EventHandler(this.cmdApply2_Click);
			this.checkPrint2.Checked = true;
			this.checkPrint2.CheckState = CheckState.Checked;
			this.checkPrint2.FlatStyle = FlatStyle.Flat;
			this.checkPrint2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.checkPrint2.ForeColor = SystemColors.ControlText;
			this.checkPrint2.Location = new Point(8, 256);
			this.checkPrint2.Name = "checkPrint2";
			this.checkPrint2.Size = new System.Drawing.Size(104, 16);
			this.checkPrint2.TabIndex = 5;
			this.checkPrint2.Text = "Печать ПКО";
			this.checkPrint2.KeyPress += new KeyPressEventHandler(this.checkPrint2_KeyPress);
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label24.ForeColor = SystemColors.ControlText;
			this.label24.Location = new Point(8, 64);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(56, 16);
			this.label24.TabIndex = 2;
			this.label24.Text = "Агент";
			this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label25.ForeColor = SystemColors.ControlText;
			this.label25.Location = new Point(8, 40);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(128, 16);
			this.label25.TabIndex = 1;
			this.label25.Text = "Кол-во позиций";
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label26.ForeColor = SystemColors.ControlText;
			this.label26.Location = new Point(8, 16);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(96, 16);
			this.label26.TabIndex = 0;
			this.label26.Text = "Сумма, тенге";
			this.tabPage5.ImageIndex = 4;
			this.tabPage5.Location = new Point(4, 94);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(292, 390);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Тепло";
			this.tabPage6.ImageIndex = 6;
			this.tabPage6.Location = new Point(4, 94);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(292, 390);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Вода";
			this.tabPage3.Controls.Add(this.groupBox6);
			this.tabPage3.ForeColor = SystemColors.Desktop;
			this.tabPage3.Location = new Point(4, 94);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(292, 390);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Прочие F3";
			this.groupBox6.Controls.Add(this.bFindRNN);
			this.groupBox6.Controls.Add(this.txtRNN);
			this.groupBox6.Controls.Add(this.label6);
			this.groupBox6.Controls.Add(this.txtBaze);
			this.groupBox6.Controls.Add(this.label5);
			this.groupBox6.Controls.Add(this.numBatchAmount2);
			this.groupBox6.Controls.Add(this.cmbKorr);
			this.groupBox6.Controls.Add(this.label27);
			this.groupBox6.Controls.Add(this.txtNote2);
			this.groupBox6.Controls.Add(this.label17);
			this.groupBox6.Controls.Add(this.cmbOperation);
			this.groupBox6.Controls.Add(this.txtFromWhom);
			this.groupBox6.Controls.Add(this.cmdApply3);
			this.groupBox6.Controls.Add(this.checkPrint3);
			this.groupBox6.Controls.Add(this.label21);
			this.groupBox6.Controls.Add(this.label22);
			this.groupBox6.Controls.Add(this.label23);
			this.groupBox6.ForeColor = SystemColors.Desktop;
			this.groupBox6.Location = new Point(4, 8);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(280, 376);
			this.groupBox6.TabIndex = 2;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Платеж";
			this.bFindRNN.FlatStyle = FlatStyle.Flat;
			this.bFindRNN.ForeColor = SystemColors.ControlText;
			this.bFindRNN.ImageIndex = 0;
			this.bFindRNN.ImageList = this.imageList1;
			this.bFindRNN.Location = new Point(251, 65);
			this.bFindRNN.Name = "bFindRNN";
			this.bFindRNN.Size = new System.Drawing.Size(20, 20);
			this.bFindRNN.TabIndex = 21;
			this.bFindRNN.Click += new EventHandler(this.bFindRNN_Click);
			this.txtRNN.BorderStyle = BorderStyle.FixedSingle;
			this.txtRNN.Cursor = Cursors.IBeam;
			this.txtRNN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.txtRNN.Location = new Point(88, 64);
			this.txtRNN.MaxLength = 12;
			this.txtRNN.Name = "txtRNN";
			this.txtRNN.Size = new System.Drawing.Size(160, 23);
			this.txtRNN.TabIndex = 20;
			this.txtRNN.Text = "";
			this.txtRNN.Leave += new EventHandler(this.txtRNN_Leave);
			this.txtRNN.KeyUp += new KeyEventHandler(this.txtRNN_KeyUp);
			this.txtRNN.Enter += new EventHandler(this.txtRNN_Enter);
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label6.ForeColor = SystemColors.ControlText;
			this.label6.Location = new Point(8, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 19;
			this.label6.Text = "РНН";
			this.txtBaze.BorderStyle = BorderStyle.FixedSingle;
			this.txtBaze.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.txtBaze.Location = new Point(88, 223);
			this.txtBaze.Multiline = true;
			this.txtBaze.Name = "txtBaze";
			this.txtBaze.ScrollBars = ScrollBars.Vertical;
			this.txtBaze.Size = new System.Drawing.Size(184, 49);
			this.txtBaze.TabIndex = 18;
			this.txtBaze.Text = "";
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label5.ForeColor = SystemColors.WindowText;
			this.label5.Location = new Point(8, 223);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 23);
			this.label5.TabIndex = 17;
			this.label5.Text = "Основание";
			this.numBatchAmount2.BorderStyle = BorderStyle.FixedSingle;
			this.numBatchAmount2.DecimalPlaces = 2;
			this.numBatchAmount2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.numBatchAmount2.Location = new Point(88, 40);
			NumericUpDown num8 = this.numBatchAmount2;
			numArray = new int[] { 99999999, 0, 0, 0 };
			num8.Maximum = new decimal(numArray);
			this.numBatchAmount2.Name = "numBatchAmount2";
			this.numBatchAmount2.Size = new System.Drawing.Size(184, 22);
			this.numBatchAmount2.TabIndex = 2;
			this.numBatchAmount2.KeyPress += new KeyPressEventHandler(this.numBatchAmount2_KeyPress);
			this.numBatchAmount2.Enter += new EventHandler(this.numBatchAmount2_Enter);
			this.cmbKorr.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbKorr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			ComboBox.ObjectCollection items = this.cmbKorr.Items;
			object[] objArray = new object[] { "1211", "1212", "1210.1", "1210.2", "1214", "1010", "1210.5", "1210.4", "1210.3" };
			items.AddRange(objArray);
			this.cmbKorr.Location = new Point(88, 195);
			this.cmbKorr.Name = "cmbKorr";
			this.cmbKorr.Size = new System.Drawing.Size(184, 24);
			this.cmbKorr.TabIndex = 4;
			this.cmbKorr.KeyPress += new KeyPressEventHandler(this.cmbKorr_KeyPress);
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label27.ForeColor = SystemColors.ControlText;
			this.label27.Location = new Point(8, 196);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(80, 16);
			this.label27.TabIndex = 16;
			this.label27.Text = "Корр. счет";
			this.txtNote2.BorderStyle = BorderStyle.FixedSingle;
			this.txtNote2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.txtNote2.Location = new Point(8, 296);
			this.txtNote2.Multiline = true;
			this.txtNote2.Name = "txtNote2";
			this.txtNote2.Size = new System.Drawing.Size(264, 40);
			this.txtNote2.TabIndex = 5;
			this.txtNote2.Text = "";
			this.txtNote2.KeyPress += new KeyPressEventHandler(this.txtNote2_KeyPress);
			this.label17.ForeColor = SystemColors.ControlText;
			this.label17.Location = new Point(8, 280);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(96, 16);
			this.label17.TabIndex = 14;
			this.label17.Text = "Примечание";
			this.cmbOperation.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			ComboBox.ObjectCollection objectCollections = this.cmbOperation.Items;
			objArray = new object[] { "Прием денег", "Инкассация", "Выдача денег" };
			objectCollections.AddRange(objArray);
			this.cmbOperation.Location = new Point(88, 16);
			this.cmbOperation.Name = "cmbOperation";
			this.cmbOperation.Size = new System.Drawing.Size(184, 24);
			this.cmbOperation.TabIndex = 1;
			this.cmbOperation.KeyPress += new KeyPressEventHandler(this.cmbOperation_KeyPress);
			this.txtFromWhom.BorderStyle = BorderStyle.FixedSingle;
			this.txtFromWhom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.txtFromWhom.Location = new Point(88, 88);
			this.txtFromWhom.Multiline = true;
			this.txtFromWhom.Name = "txtFromWhom";
			this.txtFromWhom.ScrollBars = ScrollBars.Vertical;
			this.txtFromWhom.Size = new System.Drawing.Size(184, 104);
			this.txtFromWhom.TabIndex = 3;
			this.txtFromWhom.Text = "";
			this.txtFromWhom.KeyPress += new KeyPressEventHandler(this.txtFromWhom_KeyPress);
			this.cmdApply3.FlatStyle = FlatStyle.Flat;
			this.cmdApply3.ForeColor = SystemColors.ControlText;
			this.cmdApply3.Location = new Point(152, 344);
			this.cmdApply3.Name = "cmdApply3";
			this.cmdApply3.Size = new System.Drawing.Size(120, 24);
			this.cmdApply3.TabIndex = 7;
			this.cmdApply3.Text = "Принять";
			this.cmdApply3.Click += new EventHandler(this.cmdApply3_Click);
			this.checkPrint3.Checked = true;
			this.checkPrint3.CheckState = CheckState.Checked;
			this.checkPrint3.FlatStyle = FlatStyle.Flat;
			this.checkPrint3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.checkPrint3.ForeColor = SystemColors.ControlText;
			this.checkPrint3.Location = new Point(8, 344);
			this.checkPrint3.Name = "checkPrint3";
			this.checkPrint3.Size = new System.Drawing.Size(136, 16);
			this.checkPrint3.TabIndex = 6;
			this.checkPrint3.Text = "Печать ПКО/РКО";
			this.checkPrint3.KeyPress += new KeyPressEventHandler(this.checkPrint3_KeyPress);
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label21.ForeColor = SystemColors.ControlText;
			this.label21.Location = new Point(8, 88);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(64, 40);
			this.label21.TabIndex = 2;
			this.label21.Text = "От кого/ Кому";
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label22.ForeColor = SystemColors.ControlText;
			this.label22.Location = new Point(8, 16);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(48, 16);
			this.label22.TabIndex = 1;
			this.label22.Text = "Тип";
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label23.ForeColor = SystemColors.ControlText;
			this.label23.Location = new Point(8, 40);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(96, 16);
			this.label23.TabIndex = 0;
			this.label23.Text = "Сумма, тг";
			this.imageList3.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList3.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList3.ImageStream");
			this.imageList3.TransparentColor = Color.Transparent;
			this.statusBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.statusBar1.Location = new Point(0, 550);
			this.statusBar1.Name = "statusBar1";
			StatusBar.StatusBarPanelCollection panels = this.statusBar1.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.statusBarPanel1, this.statusBarPanel2, this.statusBarPanel3, this.statusBarPanel4, this.statusBarPanel6, this.statusBarPanel7, this.statusBarPanel5, this.statusBarPanel8 };
			panels.AddRange(statusBarPanelArray);
			this.statusBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(1052, 24);
			this.statusBar1.TabIndex = 3;
			this.statusBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Итого в кассе:";
			this.statusBarPanel1.Width = 104;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Text = "По абонентам:";
			this.statusBarPanel2.Width = 107;
			this.statusBarPanel3.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel3.Text = "По агентам:";
			this.statusBarPanel3.Width = 90;
			this.statusBarPanel4.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel4.Text = "Прочие:";
			this.statusBarPanel4.Width = 65;
			this.statusBarPanel6.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel6.Text = "Инкассация:";
			this.statusBarPanel6.Width = 93;
			this.statusBarPanel7.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel7.Text = "Выдача денег:";
			this.statusBarPanel7.Width = 106;
			this.statusBarPanel5.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel5.Text = "Всего платежей:";
			this.statusBarPanel5.Width = 371;
			this.statusBarPanel8.Text = "Картой:";
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader4, this.columnHeader5, this.columnHeader6 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(316, 48);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(728, 424);
			this.lv.TabIndex = 2;
			this.lv.View = View.Details;
			this.lv.Leave += new EventHandler(this.lv_Leave);
			this.columnHeader1.Text = "Л/с";
			this.columnHeader2.Text = "ФИО";
			this.columnHeader2.Width = 100;
			this.columnHeader4.Text = "Нов. пок.";
			this.columnHeader5.Text = "Сумма";
			this.columnHeader6.Text = "Тип";
			this.columnHeader6.Width = 112;
			this.groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.groupBox4.Controls.Add(this.panel1);
			this.groupBox4.ForeColor = SystemColors.Desktop;
			this.groupBox4.Location = new Point(304, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(748, 480);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Принятые платежи";
			this.panel1.Controls.Add(this.tbData);
			this.panel1.Location = new Point(8, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(750, 456);
			this.panel1.TabIndex = 0;
			this.tbData.Appearance = ToolBarAppearance.Flat;
			this.tbData.AutoSize = false;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.toolBarButton23, this.toolBarButton25, this.toolBarButton26, this.toolBarButton1 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.Divider = false;
			this.tbData.DropDownArrows = true;
			this.tbData.ImageList = this.imageList2;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(750, 24);
			this.tbData.TabIndex = 3;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.toolBarButton23.ImageIndex = 2;
			this.toolBarButton23.Tag = "Del";
			this.toolBarButton23.ToolTipText = "Удалить";
			this.toolBarButton25.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton26.ImageIndex = 3;
			this.toolBarButton26.Tag = "Excel";
			this.toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.toolBarButton1.ImageIndex = 11;
			this.toolBarButton1.Tag = "PrintPKO";
			this.toolBarButton1.ToolTipText = "Печать ПКО";
			this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList2.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList2.ImageStream");
			this.imageList2.TransparentColor = Color.Transparent;
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton26);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton1);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton23);
			this.CommandHolder.Commands.Add(this.cmd_toolBarButton25);
			this.CommandHolder.Owner = this;
			this.cmd_toolBarButton26.ImageIndex = 3;
			this.cmd_toolBarButton26.Name = "cmd_toolBarButton26";
			this.cmd_toolBarButton26.ToolTipText = "Конвертировать в Excel";
			this.cmd_toolBarButton1.ImageIndex = 11;
			this.cmd_toolBarButton1.Name = "cmd_toolBarButton1";
			this.cmd_toolBarButton1.ToolTipText = "Печать ПКО";
			this.cmd_toolBarButton23.ImageIndex = 2;
			this.cmd_toolBarButton23.Name = "cmd_toolBarButton23";
			this.cmd_toolBarButton23.ToolTipText = "Удалить";
			this.cmd_toolBarButton25.Name = "cmd_toolBarButton25";
			this.groupBox1.Controls.Add(this.lblCashier);
			this.groupBox1.Controls.Add(this.lblBatchAmount);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.lblBatchCount);
			this.groupBox1.ForeColor = SystemColors.Desktop;
			this.groupBox1.Location = new Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1048, 40);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Информация по кассе";
			this.lblCashier.BackColor = SystemColors.Info;
			this.lblCashier.BorderStyle = BorderStyle.FixedSingle;
			this.lblCashier.ForeColor = SystemColors.ControlText;
			this.lblCashier.Location = new Point(480, 16);
			this.lblCashier.Name = "lblCashier";
			this.lblCashier.Size = new System.Drawing.Size(184, 20);
			this.lblCashier.TabIndex = 5;
			this.lblBatchAmount.BackColor = SystemColors.Info;
			this.lblBatchAmount.BorderStyle = BorderStyle.FixedSingle;
			this.lblBatchAmount.ForeColor = SystemColors.ControlText;
			this.lblBatchAmount.Location = new Point(288, 16);
			this.lblBatchAmount.Name = "lblBatchAmount";
			this.lblBatchAmount.Size = new System.Drawing.Size(120, 20);
			this.lblBatchAmount.TabIndex = 3;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label3.ForeColor = SystemColors.ControlText;
			this.label3.Location = new Point(408, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Кассир";
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label1.ForeColor = SystemColors.ControlText;
			this.label1.Location = new Point(160, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Сумма платежей";
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label2.ForeColor = SystemColors.ControlText;
			this.label2.Location = new Point(672, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Количество платежей";
			this.lblBatchCount.BackColor = SystemColors.Info;
			this.lblBatchCount.BorderStyle = BorderStyle.FixedSingle;
			this.lblBatchCount.ForeColor = SystemColors.ControlText;
			this.lblBatchCount.Location = new Point(824, 16);
			this.lblBatchCount.Name = "lblBatchCount";
			this.lblBatchCount.Size = new System.Drawing.Size(64, 20);
			this.lblBatchCount.TabIndex = 4;
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Dock = DockStyle.Bottom;
			this.panel2.Location = new Point(0, 494);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1052, 56);
			this.panel2.TabIndex = 4;
			this.chkPayCardUL.FlatStyle = FlatStyle.Flat;
			this.chkPayCardUL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 204);
			this.chkPayCardUL.Location = new Point(8, 176);
			this.chkPayCardUL.Name = "chkPayCardUL";
			this.chkPayCardUL.TabIndex = 13;
			this.chkPayCardUL.Text = "Оплата картой";
			this.chkPayCardUL.CheckedChanged += new EventHandler(this.chkPayCardUL_CheckedChanged);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(1052, 574);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.statusBar1);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.groupBox4);
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.MinimumSize = new System.Drawing.Size(1060, 608);
			base.Name = "frmCash";
			this.Text = "Касса";
			base.Closing += new CancelEventHandler(this.frmCash_Closing);
			base.Load += new EventHandler(this.frmCash_Load);
			base.KeyUp += new KeyEventHandler(this.frmCash_KeyUp);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((ISupportInitialize)this.nmcPoluch).EndInit();
			((ISupportInitialize)this.numNewIndication).EndInit();
			((ISupportInitialize)this.numAmount).EndInit();
			((ISupportInitialize)this.txtAccount).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.tabPage9.ResumeLayout(false);
			this.groupBox10.ResumeLayout(false);
			((ISupportInitialize)this.numPoluchUL).EndInit();
			((ISupportInitialize)this.numAmountUL).EndInit();
			this.groupBox9.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			((ISupportInitialize)this.numericUpDown2).EndInit();
			this.groupBox7.ResumeLayout(false);
			((ISupportInitialize)this.c1TextBox1).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			((ISupportInitialize)this.numBatchCount).EndInit();
			((ISupportInitialize)this.numBatchAmount).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			((ISupportInitialize)this.numBatchAmount2).EndInit();
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			((ISupportInitialize)this.statusBarPanel3).EndInit();
			((ISupportInitialize)this.statusBarPanel4).EndInit();
			((ISupportInitialize)this.statusBarPanel6).EndInit();
			((ISupportInitialize)this.statusBarPanel7).EndInit();
			((ISupportInitialize)this.statusBarPanel5).EndInit();
			((ISupportInitialize)this.statusBarPanel8).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.CommandHolder).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void LogPrint(int Type, string Text)
		{
			try
			{
				PrintLog printLog = new PrintLog()
				{
					IDUser = this.IDUser,
					DatePrintLog = DateTime.Now,
					Note = Text,
					TypePrintLog = Type
				};
				printLog.Save();
			}
			catch
			{
			}
		}

		private void lv_Leave(object sender, EventArgs e)
		{
		}

		private void nmcPoluch_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.nmcPoluch);
		}

		private void nmcPoluch_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numNewIndication_Leave(null, null);
				this.cmdApply1.Focus();
			}
		}

		private void nmcPoluch_Leave(object sender, EventArgs e)
		{
			if (this.numAmount.Value > new decimal(0))
			{
				decimal num = new decimal(0);
				try
				{
					this.lblZdacha.Text = string.Concat("СДАЧА=", Convert.ToString(this.nmcPoluch.Value - this.numAmount.Value), "тенге");
					num = this.nmcPoluch.Value - this.numAmount.Value;
					this.checkPrint1.Focus();
				}
				catch
				{
					MessageBox.Show(num.ToString(), "Сдача", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (this.nmcPoluch.Value == new decimal(0))
			{
				this.lblZdacha.Text = "СДАЧА=0";
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
				if (this.numAmount.Value > new decimal(5000))
				{
					if (MessageBox.Show(string.Concat("Вы действительно хотите внести эту сумму ", this.numAmount.Value.ToString(), "?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
					{
						this.numAmount.Focus();
						return;
					}
					if (this.numNewIndication.Enabled)
					{
						this.numNewIndication.Focus();
						return;
					}
					this.checkPrint1.Focus();
					return;
				}
				if (this.numNewIndication.Enabled)
				{
					this.numNewIndication.Focus();
					return;
				}
				this.checkPrint1.Focus();
			}
		}

		private void numAmountUL_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numAmountUL);
		}

		private void numAmountUL_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				if (this.numAmountUL.Value > new decimal(5000))
				{
					if (MessageBox.Show(string.Concat("Вы действительно хотите внести эту сумму ", this.numAmountUL.Value.ToString(), "?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
					{
						this.numAmountUL.Focus();
						return;
					}
					this.bApplyUL.Focus();
					return;
				}
				this.bApplyUL.Focus();
			}
		}

		private void numBatchAmount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numBatchAmount);
		}

		private void numBatchAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.numBatchCount.Focus();
			}
		}

		private void numBatchAmount2_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numBatchAmount2);
		}

		private void numBatchAmount2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.txtFromWhom.Focus();
			}
		}

		private void numBatchCount_Enter(object sender, EventArgs e)
		{
			Tools.SelectAllNumericUpDown(this.numBatchCount);
		}

		private void numBatchCount_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbAgent.Focus();
			}
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
				this.nmcPoluch.Focus();
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
					this.lblFactUse.Text = str;
					this.FactAmount = Convert.ToDouble(str);
					this.nmcPoluch.Focus();
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

		private void Print1(long IDDocument)
		{
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2 };
					string[] str = new string[] { "@IdDocument" };
					string[] strArrays = str;
					str = new string[] { IDDocument.ToString() };
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repCountNoticeOne.rpt");
					frmReports frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = "Приходный кассовый ордер.",
						MdiParent = base.MdiParent
					};
					frmReport.Show();
					frmReport = null;
					this.LogPrint(2, string.Concat("IDDocument=", Convert.ToString(IDDocument)));
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

		private void Print3(Batch oBatch)
		{
			try
			{
				if (this.cmbOperation.SelectedIndex == 0)
				{
					this.PrintPKO(oBatch, this.txtBaze.Text);
				}
				if (this.cmbOperation.SelectedIndex == 1)
				{
					this.PrintRKO(oBatch, 1);
				}
				if (this.cmbOperation.SelectedIndex == 2)
				{
					this.PrintRKO(oBatch, 2);
				}
			}
			catch
			{
			}
		}

		public bool PrintKKM(Document oDocument, int Section)
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
				this.ECR.Department = Section;
				this.ECR.Price = oDocument.DocumentAmount;
				this.ECR.Summ = Convert.ToDouble(this.nmcPoluch.Value);
				this.ECR.Registration();
				this.ECR.TypeClose = 0;
				this.ECR.TaxTypeNumber = 1;
				if (this.ECR.Delivery() != 0)
				{
					MessageBox.Show("Ошибка закрытия чека! ", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.ECR.CloseCheck();
				}
				this.ECR.CloseCheck();
				string[] str = new string[] { "Account=", oDocument.oContract.Account.ToString(), ";FIO=", oDocument.oContract.oPerson.FullName.ToString(), ";Amount=", null };
				str[5] = oDocument.DocumentAmount.ToString();
				this.LogPrint(1, string.Concat(str));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка печати ККМ! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintKKMCard(Document oDocument, int Section)
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
				this.ECR.Department = Section;
				this.ECR.Price = oDocument.DocumentAmount;
				this.ECR.Summ = Convert.ToDouble(this.nmcPoluch.Value);
				this.ECR.Registration();
				this.ECR.TypeClose = 3;
				this.ECR.TaxTypeNumber = 1;
				if (this.ECR.Delivery() != 0)
				{
					MessageBox.Show("Ошибка закрытия чека! ", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.ECR.CloseCheck();
				}
				this.ECR.CloseCheck();
				string[] str = new string[] { "Account=", oDocument.oContract.Account.ToString(), ";FIO=", oDocument.oContract.oPerson.FullName.ToString(), ";Amount=", null };
				str[5] = oDocument.DocumentAmount.ToString();
				this.LogPrint(1, string.Concat(str));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка печати ККМ! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintKKMDrugoe(Batch oBatch)
		{
			bool flag = false;
			string str = "";
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
				if (oBatch.oDispatcher == null)
				{
					char[] chr = new char[] { Convert.ToChar("@") };
					string[] strArrays = oBatch.get_Name().Split(chr);
					if ((int)strArrays.Length == 2)
					{
						this.ECR.Caption = string.Concat("Принято от:", strArrays[1]);
						str = strArrays[1];
						this.ECR.Department = 2;
					}
				}
				else
				{
					this.ECR.Caption = string.Concat("Принято от:", oBatch.oDispatcher.get_Name().ToString());
					str = oBatch.oDispatcher.get_Name().ToString();
					this.ECR.Department = 3;
				}
				this.ECR.PrintString();
				this.ECR.EndDocument();
				this.ECR.Price = oBatch.BatchAmount;
				this.ECR.Summ = 0;
				this.ECR.Registration();
				this.ECR.TypeClose = 0;
				this.ECR.TaxTypeNumber = 1;
				if (this.ECR.Delivery() != 0)
				{
					MessageBox.Show("Ошибка закрытия чека! ", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.ECR.CloseCheck();
				}
				this.ECR.CloseCheck();
				string str1 = str.ToString();
				double batchAmount = oBatch.BatchAmount;
				this.LogPrint(1, string.Concat("FIO=", str1, ";Amount=", batchAmount.ToString()));
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
				this.LogPrint(1, string.Concat("VozvratAmount=", Convert.ToString(-oDocument.DocumentAmount)));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка печати ККМ! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintPKO(Batch oBatch, string sBase)
		{
			bool flag = false;
			try
			{
				string str = "1211";
				string str1 = "";
				if (oBatch.oDispatcher != null)
				{
					str1 = oBatch.oDispatcher.get_Name().ToString();
				}
				char[] chr = new char[] { Convert.ToChar("@") };
				string[] strArrays = oBatch.get_Name().Split(chr);
				if ((int)strArrays.Length == 2)
				{
					str = strArrays[0];
					if (str1 == "")
					{
						str1 = strArrays[1];
					}
				}
				string str2 = Tools.ConvertCurencyInString(oBatch.BatchAmount);
				int day = DateTime.Today.Day;
				day.ToString();
				day = DateTime.Today.Month;
				day.ToString();
				day = DateTime.Today.Year;
				day.ToString();
				DateTime today = DateTime.Today;
				Tools.NameMonth(today.Month);
				oBatch.BatchAmount.ToString();
				this._batch.oCashier.get_Name();
				oBatch.NumberBatch.ToString();
				day = DateTime.Today.Day;
				string str3 = day.ToString();
				day = DateTime.Today.Month;
				string str4 = day.ToString();
				day = DateTime.Today.Year;
				string str5 = day.ToString();
				today = DateTime.Today;
				string str6 = Tools.NameMonth(today.Month);
				string str7 = "";
				string str8 = "";
				string str9 = "";
				string str10 = "";
				string[] strArrays1 = new string[16];
				strArrays1[0] = string.Concat("1|", oBatch.NumberBatch.ToString(), "|200|719|0");
				strArrays1[1] = string.Concat("3|", oBatch.NumberBatch.ToString(), "|445|745|0");
				strArrays1[2] = string.Concat("1|", str.ToString(), "|127|612|0");
				double batchAmount = oBatch.BatchAmount;
				strArrays1[3] = string.Concat("1|", batchAmount.ToString(), "|210|612|0");
				strArrays1[4] = string.Concat("3|", str2, "|75|560|0");
				strArrays1[5] = string.Concat("3|", str2, "|403|675|0");
				strArrays1[6] = string.Concat("3|", this._batch.oCashier.get_Name(), "|210|491|0");
				strArrays1[7] = string.Concat("3|", this._batch.oCashier.get_Name(), "|400|506|0");
				string[] strArrays2 = new string[] { "1|", str3, ".", str4, ".", str5, "|295|719|0" };
				strArrays1[8] = string.Concat(strArrays2);
				strArrays2 = new string[] { "1|", str3, " «", str6, "» ", str5, "г.|460|608|0" };
				strArrays1[9] = string.Concat(strArrays2);
				strArrays1[10] = string.Concat("3|", sBase, "|430|699|0");
				strArrays1[11] = string.Concat("3|", sBase, "|100|572|0");
				strArrays1[12] = "3||95|586|0";
				strArrays1[13] = string.Concat("3|", str1, "|95|586|0");
				strArrays1[14] = "3||428|723|0";
				strArrays1[15] = string.Concat("3|", str1, "|428|723|0");
				string[] strArrays3 = strArrays1;
				int length = str1.Length;
				if (length > 47)
				{
					str7 = str1.Substring(0, 48);
					str8 = str1.Substring(48);
					strArrays3.SetValue(string.Concat("3|", str7, "|95|593|0"), 12);
					strArrays3.SetValue(string.Concat("3|", str8, "|95|584|0"), 13);
				}
				if (length > 29)
				{
					str9 = str1.Substring(0, 29);
					str10 = str1.Substring(29);
					strArrays3.SetValue(string.Concat("3|", str9, "|428|732|0"), 14);
					strArrays3.SetValue(string.Concat("3|", str10, "|428|723|0"), 15);
				}
				Tools.ShowPdfDocument(string.Concat("Report_", str.ToString(), ".pdf"), string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\"), "ПКО.pdf", strArrays3);
				string str11 = oBatch.NumberBatch.ToString();
				batchAmount = oBatch.BatchAmount;
				this.LogPrint(2, string.Concat("NumBatch=", str11, ";BatchAmount=", batchAmount.ToString()));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
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
				int num1 = 0;
				num1 = (zAccount != "1211" ? 3 : 2);
				this.LogPrint(num1, string.Concat("Account=", oDoc.oContract.Account.ToString(), ";Amount=", num.ToString()));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintPKODoc(Document oDocument)
		{
			bool flag = false;
			try
			{
				string str = "1010";
				string str1 = "Центральная касса";
				string str2 = "Возврат денежных средств";
				string str3 = Tools.ConvertCurencyInString(-oDocument.DocumentAmount);
				int day = DateTime.Today.Day;
				day.ToString();
				day = DateTime.Today.Month;
				day.ToString();
				day = DateTime.Today.Year;
				day.ToString();
				DateTime today = DateTime.Today;
				Tools.NameMonth(today.Month);
				Convert.ToString(-oDocument.DocumentAmount);
				oDocument.oBatch.oCashier.get_Name().ToString();
				day = DateTime.Today.Day;
				string str4 = day.ToString();
				day = DateTime.Today.Month;
				string str5 = day.ToString();
				day = DateTime.Today.Year;
				string str6 = day.ToString();
				today = DateTime.Today;
				Tools.NameMonth(today.Month);
				string[] strArrays = new string[] { string.Concat("1|", str, "|85|670|0"), string.Concat("3|", str1, "|103|646|0"), string.Concat("3|", str2, "|103|624|0"), string.Concat("1|", Convert.ToString(-oDocument.DocumentAmount), "|235|670|0"), string.Concat("3|", str3, "|100|598|0"), string.Concat("3|", str3, "|92|531|0"), null, null };
				string[] strArrays1 = new string[] { "1|", str4, ".", str5, ".", str6, "г.|510|733|0" };
				strArrays[6] = string.Concat(strArrays1);
				strArrays[7] = string.Concat("1|", oDocument.oBatch.oCashier.get_Name().ToString(), "|358|453|0");
				string[] strArrays2 = strArrays;
				Tools.ShowPdfDocument(string.Concat("Report_", str1.ToString(), ".pdf"), string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\"), "РКО.pdf", strArrays2);
				this.LogPrint(4, string.Concat("KassVozvrat=", Convert.ToString(-oDocument.DocumentAmount), ";Cashier=", oDocument.oBatch.oCashier.get_Name().ToString()));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintPKOVozvratDoc(Document oDocument)
		{
			bool flag = false;
			try
			{
				string str = "1211";
				string text = this.lblFIO.Text;
				string str1 = "Заявление абонента";
				string str2 = Tools.ConvertCurencyInString(-oDocument.DocumentAmount);
				int day = DateTime.Today.Day;
				day.ToString();
				day = DateTime.Today.Month;
				day.ToString();
				day = DateTime.Today.Year;
				day.ToString();
				DateTime today = DateTime.Today;
				Tools.NameMonth(today.Month);
				Convert.ToString(-oDocument.DocumentAmount);
				oDocument.oBatch.oCashier.get_Name().ToString();
				day = DateTime.Today.Day;
				string str3 = day.ToString();
				day = DateTime.Today.Month;
				string str4 = day.ToString();
				day = DateTime.Today.Year;
				string str5 = day.ToString();
				today = DateTime.Today;
				Tools.NameMonth(today.Month);
				string[] strArrays = new string[] { string.Concat("1|", str, "|85|670|0"), string.Concat("3|", text, "|103|646|0"), string.Concat("3|", str1, "|103|624|0"), string.Concat("1|", Convert.ToString(-oDocument.DocumentAmount), "|235|670|0"), string.Concat("3|", str2, "|100|598|0"), string.Concat("3|", str2, "|92|531|0"), null, null };
				string[] strArrays1 = new string[] { "1|", str3, ".", str4, ".", str5, "г.|510|733|0" };
				strArrays[6] = string.Concat(strArrays1);
				strArrays[7] = string.Concat("1|", oDocument.oBatch.oCashier.get_Name().ToString(), "|358|453|0");
				string[] strArrays2 = strArrays;
				Tools.ShowPdfDocument(string.Concat("Report_", text.ToString(), ".pdf"), string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\"), "РКО1211.pdf", strArrays2);
				this.LogPrint(4, string.Concat("KassVozvrat=", Convert.ToString(-oDocument.DocumentAmount), ";Cashier=", oDocument.oBatch.oCashier.get_Name().ToString()));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintRKO(Batch oBatch, int type)
		{
			bool flag = false;
			try
			{
				string str = "";
				if (type == 1)
				{
					str = string.Concat("Сдано в центральную кассу через ", oBatch.oCashier.get_Name().ToString());
				}
				string str1 = "";
				string str2 = Tools.ConvertCurencyInString(oBatch.BatchAmount);
				char[] chr = new char[] { Convert.ToChar("@") };
				string[] strArrays = oBatch.get_Name().Split(chr);
				if ((int)strArrays.Length == 2)
				{
					str1 = strArrays[0];
				}
				string str3 = "";
				if (type == 1)
				{
					str3 = "Внутреннее перемещение";
				}
				int day = oBatch.BatchDate.Day;
				day.ToString();
				day = oBatch.BatchDate.Month;
				day.ToString();
				day = oBatch.BatchDate.Year;
				day.ToString();
				oBatch.BatchAmount.ToString();
				oBatch.oCashier.get_Name();
				day = oBatch.BatchDate.Day;
				string str4 = day.ToString();
				day = oBatch.BatchDate.Month;
				string str5 = day.ToString();
				day = oBatch.BatchDate.Year;
				string str6 = day.ToString();
				string[] strArrays1 = new string[] { string.Concat("1|", str1, "|85|670|0"), string.Concat("3|", str, "|103|646|0"), string.Concat("3|", str3, "|103|624|0"), null, null, null, null, null };
				double batchAmount = oBatch.BatchAmount;
				strArrays1[3] = string.Concat("1|", batchAmount.ToString(), "|235|670|0");
				strArrays1[4] = string.Concat("3|", str2, "|100|598|0");
				strArrays1[5] = string.Concat("3|", str2, "|92|531|0");
				string[] strArrays2 = new string[] { "1|", str4, ".", str5, ".", str6, "г.|510|733|0" };
				strArrays1[6] = string.Concat(strArrays2);
				strArrays1[7] = string.Concat("1|", oBatch.oCashier.get_Name(), "|358|453|0");
				string[] strArrays3 = strArrays1;
				Tools.ShowPdfDocument(string.Concat("Report_", str.ToString(), ".pdf"), string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\"), "РКО.pdf", strArrays3);
				this.LogPrint(4, string.Concat("Inkass=", str2, ";Cashier=", oBatch.oCashier.get_Name()));
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - PКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		private void ResetFields1()
		{
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblCountLives.Text = "";
			this.lblPU.Text = "";
			this.lblBalance.Text = "";
			this.lblCurrentIndication.Text = "";
			this.lblFactUse.Text = "";
			this.txtAbRNN.Text = "";
			this.numAmount.Value = new decimal(0);
			this.cmbAccount.Items.Clear();
			this.cmbAccount.Text = "";
			this.numNewIndication.Value = new decimal(0);
			this.numNewIndication.Enabled = false;
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
			this._indication = null;
			this.chkPayCard.Checked = false;
		}

		private void ResetFields2()
		{
			this.lblInfoUL.Text = "";
			this.lblAddressUL.Text = "";
			this.lblBalanceUL.Text = "";
			this.numAmountUL.Value = new decimal(0);
			this.txtBaseUL.Text = "";
			this.txtIINul.Text = "";
			this.cmbAccount.Items.Clear();
			this.cmbAccount.Text = "";
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
			this._indication = null;
			this.chkPayCardUL.Checked = false;
		}

		private void ResetFields3()
		{
			this.lblFIO.Text = "";
			this.lblAddress.Text = "";
			this.lblCountLives.Text = "";
			this.lblPU.Text = "";
			this.lblBalance.Text = "";
			this.lblCurrentIndication.Text = "";
			this.lblFactUse.Text = "";
			this.numAmount.Value = new decimal(0);
			this.numNewIndication.Value = new decimal(0);
			this.numNewIndication.Enabled = false;
			this.cmbAccount.Items.Clear();
			this.cmbAccount.Text = "";
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
			this._indication = null;
		}

		private void ResetFields4()
		{
			this.lblInfoUL.Text = "";
			this.lblAddressUL.Text = "";
			this.lblBalanceUL.Text = "";
			this.numAmountUL.Value = new decimal(0);
			this.txtBaseUL.Text = "";
			this.cmbAccountUL.Items.Clear();
			this.cmbAccountUL.Text = "";
			this._contract = null;
			this._gobject = null;
			this._gmeter = null;
			this._indication = null;
		}

		private void SearchConsumer(int TypeSearch)
		{
			if (this.lvContent == null)
			{
				this.lvContent = new List();
			}
			string str = "";
			if (TypeSearch == 3 && this.txtRNN.Text.Length > 5)
			{
				str = string.Concat("select c.idcontract, p.idperson, c.account,  case when p.isJuridical=1 then p.RNN+', '+isnull(p.name,'')+' '+isnull(os.Name,'')+', '+isnull(p.Surname,'')+', '+  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') else p.RNN+', '+isnull(p.Surname,'')+' '+isnull(p.Name,'')+' '+isnull(p.Patronic,'')+', '+ isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') end FIO,  isnull(s.Name,'')+' '+isnull(ltrim(str(h.housenumber)),'')+isnull(h.housenumberchar, '')+'-'+isnull(a.flat,'') address, g.Countlives, round(dbo.fGetLastBalance(dbo.fGetNowPeriod(), c.IdContract, 0), 2) balance, case when gm.idstatusgmeter=1 then tg.name+', '+ltrim(str(tg.ClassAccuracy)) else 'Отключен' end PU, so.Name OU, case when isnull(Status,0)=1 then 'Активен' else case when isnull(Status,0)=0 then 'Не определен' else 'Закрыт' end end Contract,  case when gm.idstatusgmeter=1 then left(convert(varchar,gm.DateFabrication, 20), 10) else ' ' end date , p.RNN  from person p  inner join address a on a.idaddress=p.idaddress  and p.RNN like '", this.txtRNN.Text, "%' inner join house h on h.idhouse=a.idhouse  inner join street s on s.idstreet=h.idstreet  left join contract c on c.idperson=p.idperson  left join gobject g on g.idcontract=c.idcontract  left join StatusGObject so on so.IDStatusGObject=g.IDStatusGObject  left join gmeter gm on g.idgobject=gm.idgobject  and gm.idstatusgmeter=1  left join TypeGmeter tg on gm.idtypegmeter=tg.idtypegmeter order by c.Account");
			}
			this.lvContent.set_nametable_pr("contract");
			this.lvContent.set_select_pr(str);
			this.lvContent.Load();
			int num = 0;
			IEnumerator enumerator = this.lvContent.get_mylist_pr().GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					string[] current = (string[])enumerator.Current;
					this.txtFromWhom.Text = string.Concat("РНН:", current[3]);
					num = 1;
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			if (num == 0)
			{
				this.txtFromWhom.Text = string.Concat("РНН:", Convert.ToString(this.txtRNN.Text));
			}
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			try
			{
				"PrintPKO";
				string str = e.Button.Tag.ToString();
				string str1 = str;
				if (str != null)
				{
					str1 = string.IsInterned(str1);
					if ((object)str1 == (object)"Del")
					{
						if (this.lv.SelectedItems.Count > 0 && MessageBox.Show(string.Concat("Вы действительно хотите удалить документ по договору ", this.lv.SelectedItems[0].Text, "?"), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						{
							if (this.lv.SelectedItems[0].SubItems[0].Text != "1210.1" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.2" && this.lv.SelectedItems[0].SubItems[0].Text != "1010" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.3" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.4" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.5" && this.lv.SelectedItems[0].SubItems[0].Text != "нет")
							{
								double documentAmount = this._batch.oDocuments.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).DocumentAmount;
								if (this._batch.oDocuments.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) != 0)
								{
									MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									CashBalance amountBalance = this._cashbalance;
									amountBalance.AmountBalance = amountBalance.AmountBalance - documentAmount;
									this._cashbalance.Save();
									Batch batchCount = this._batch;
									batchCount.BatchCount = batchCount.BatchCount - 1;
									Batch batchAmount = this._batch;
									batchAmount.BatchAmount = batchAmount.BatchAmount - documentAmount;
									this.lv.Items.Remove(this.lv.SelectedItems[0]);
									this.FillCommonInfo();
									this.FillStatusBar();
								}
							}
							else if (this.lv.SelectedItems[0].SubItems[0].Text != "нет")
							{
								long d = this._batchsother.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).oTypeBatch.get_ID();
								double num = this._batchsother.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).BatchAmount;
								if (this._batchsother.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) != 0)
								{
									MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									CashBalance cashBalance = this._cashbalance;
									cashBalance.AmountBalance = cashBalance.AmountBalance + num;
									this._cashbalance.Save();
									if (d != (long)3)
									{
										this.sumOther -= num;
										this.countOther--;
									}
									else
									{
										this.sumIncas -= num;
										this.countIncas--;
									}
									this.lv.Items.Remove(this.lv.SelectedItems[0]);
									this.FillCommonInfo();
									this.FillStatusBar();
								}
							}
							else
							{
								double batchAmount1 = this._batchsagent.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).BatchAmount;
								if (this._batchsagent.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) != 0)
								{
									MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									CashBalance amountBalance1 = this._cashbalance;
									amountBalance1.AmountBalance = amountBalance1.AmountBalance - batchAmount1;
									this._cashbalance.Save();
									this.sumFromAgent -= batchAmount1;
									this.countFromAgent--;
									this.lv.Items.Remove(this.lv.SelectedItems[0]);
									this.FillCommonInfo();
									this.FillStatusBar();
								}
							}
						}
					}
					else if ((object)str1 == (object)"Excel")
					{
						Tools.ConvertToExcel(this.lv);
					}
					else if ((object)str1 == (object)"PrintPKO")
					{
						if (this.lv.SelectedItems.Count > 0)
						{
							if (this._batch.oDocuments.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)).DocumentAmount >= 0)
							{
								if (this.lv.SelectedItems[0].SubItems[0].Text != "1210.1" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.2" && this.lv.SelectedItems[0].SubItems[0].Text != "1010" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.3" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.4" && this.lv.SelectedItems[0].SubItems[0].Text != "1210.5" && this.lv.SelectedItems[0].SubItems[0].Text != "нет")
								{
									this.Print1(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
								}
								else if (this.lv.SelectedItems[0].SubItems[0].Text != "нет")
								{
									if (!this.lv.SelectedItems[0].SubItems[4].Text.StartsWith("Инкассация") && !this.lv.SelectedItems[0].SubItems[4].Text.StartsWith("Выдача денег"))
									{
										this.PrintPKO(this._batchsother.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)), "за газ");
									}
									else if (!this.lv.SelectedItems[0].SubItems[4].Text.StartsWith("Инкассация"))
									{
										this.PrintRKO(this._batchsother.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)), 2);
									}
									else
									{
										this.PrintRKO(this._batchsother.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)), 1);
									}
								}
								if (this.lv.SelectedItems[0].SubItems[0].Text == "нет")
								{
									this.PrintPKO(this._batchsagent.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)), "за газ");
								}
							}
							else
							{
								this.PrintPKODoc(this._batch.oDocuments.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag)));
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void txtAbRNN_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.bRNN_Click(null, null);
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

		private void txtAccount_TextChanged(object sender, EventArgs e)
		{
		}

		private void txtAccountUL_Enter(object sender, EventArgs e)
		{
			this.txtAccountUL.SelectAll();
		}

		private void txtAccountUL_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.bAccountUL_Click(null, null);
			}
		}

		private void txtAccountUL_Leave(object sender, EventArgs e)
		{
			this.bAccountUL_Click(null, null);
		}

		private void txtFromWhom_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.cmbKorr.Focus();
			}
		}

		private void txtIINul_Enter(object sender, EventArgs e)
		{
			this.txtIINul.SelectAll();
		}

		private void txtIINul_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.bIINul_Click(null, null);
			}
		}

		private void txtIINul_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.bIINul_Click(null, null);
			}
		}

		private void txtNote_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.checkPrint2.Focus();
			}
		}

		private void txtNote2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.checkPrint3.Focus();
			}
		}

		private void txtRNN_Enter(object sender, EventArgs e)
		{
			this.txtRNN.SelectAll();
		}

		private void txtRNN_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.SearchConsumer(3);
			}
		}

		private void txtRNN_Leave(object sender, EventArgs e)
		{
		}

		private void WriteLog(string str)
		{
			using (StreamWriter streamWriter = new StreamWriter("C:\\Gefest.txt", true))
			{
				streamWriter.WriteLine(str);
			}
		}
	}
}