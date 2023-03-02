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
using WebSecurityDLL;

namespace Gefest
{
	public class frmOpenCashChange : Form
	{
		private PictureBox pictureBox1;

		private Panel panel1;

		private Button cmdNext;

		private Button cmdClose;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private TextBox txtNumberBatch;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label lblCash;

		private Label lblAbonent;

		private Label lblAgent;

		private Label lblOther;

		private C1Combo cmbTypePay;

		private C1DateEdit dtDate;

		private C1Combo cmbCashier;

		private Label lblIncassation;

		private Label label10;

		private Label label9;

		private TextBox txtSum;

		private Label lblVidacha;

		private Label label12;

		private int _type;

		private Batch _batch;

		private Batch _cardBatch;

		private Batchs _cardBatchs;

		private Batchs _batchs1;

		private CashBalance _cashbalance;

		private Agents _agents;

		private Batchs _batchsagent;

		private Batchs _batchsother;

		private CheckBox chkKKM;

		private IFprnM45 ECR;

		private System.ComponentModel.Container components = null;

		public frmOpenCashChange(int type)
		{
			this._type = type;
			this.InitializeComponent();
		}

		private void cmbCashier_TextChanged(object sender, EventArgs e)
		{
			try
			{
				double batchAmount = 0;
				double amountBalance = 0;
				this._cardBatch = new Batch();
				this._cardBatchs = new Batchs();
				this._cardBatchs.Load(Depot.oStatusBatchs.item((long)1), Depot.oTypePays.item((long)2), Depot.oTypeBatchs.item((long)1), this._agents[this.cmbCashier.SelectedIndex], 113);
				if (this._cardBatchs.get_Count() <= 0)
				{
					this._cardBatch.BatchCount = 0;
					this._cardBatch.BatchAmount = 0;
				}
				else
				{
					this._cardBatch = this._cardBatchs[0];
				}
				CashBalances cashBalance = new CashBalances();
				cashBalance.Load(DateTime.Today, this._agents[this.cmbCashier.SelectedIndex]);
				if (cashBalance.get_Count() <= 0)
				{
					this.lblCash.Text = "0";
					this.lblAbonent.Text = "0";
					this.lblAgent.Text = "0";
					this.lblOther.Text = "0";
					this.lblIncassation.Text = "0";
					this.lblVidacha.Text = "0";
					this._cashbalance = new CashBalance()
					{
						oCashier = this._agents[this.cmbCashier.SelectedIndex],
						DateCash = DateTime.Today
					};
					this._cashbalance.Save();
				}
				else
				{
					this._cashbalance = cashBalance[0];
					amountBalance = this._cashbalance.AmountBalance;
					this.lblCash.Text = Convert.ToString(Math.Round(this._cashbalance.AmountBalance, 2));
					this._batchsagent = new Batchs();
					this._batchsagent.LoadByAgent(DateTime.Today, this._agents[this.cmbCashier.SelectedIndex], Depot.oTypeBatchs.item((long)1), Depot.oTypePays.item((long)1));
					foreach (Batch batch in this._batchsagent)
					{
						batchAmount += batch.BatchAmount;
					}
					this.lblAgent.Text = Convert.ToString(Math.Round(batchAmount, 2));
					batchAmount = 0;
					double num = 0;
					double batchAmount1 = 0;
					this._batchsother = new Batchs();
					Batchs batch1 = this._batchsother;
					DateTime today = DateTime.Today;
					Agent item = this._agents[this.cmbCashier.SelectedIndex];
					TypeBatch[] typeBatchArray = new TypeBatch[] { Depot.oTypeBatchs.item((long)5), Depot.oTypeBatchs.item((long)3), Depot.oTypeBatchs.item((long)6) };
					batch1.Load(today, item, typeBatchArray);
					foreach (Batch batch2 in this._batchsother)
					{
						if (batch2.oTypeBatch.get_ID() == (long)5)
						{
							batchAmount += batch2.BatchAmount;
						}
						if (batch2.oTypeBatch.get_ID() == (long)3)
						{
							num += batch2.BatchAmount;
						}
						if (batch2.oTypeBatch.get_ID() != (long)6)
						{
							continue;
						}
						batchAmount1 += batch2.BatchAmount;
					}
					this.lblOther.Text = Convert.ToString(Math.Round(batchAmount, 2));
					this.lblIncassation.Text = Convert.ToString(Math.Round(num, 2));
					this.lblVidacha.Text = Convert.ToString(Math.Round(batchAmount1, 2));
				}
				if (this._cashbalance.DateCash != DateTime.Today)
				{
					this._cashbalance = new CashBalance()
					{
						AmountBalance = amountBalance,
						oCashier = this._agents[this.cmbCashier.SelectedIndex],
						DateCash = DateTime.Today
					};
					this._cashbalance.Save();
				}
				Batchs batch3 = new Batchs();
				batch3.Load(Depot.oStatusBatchs.item((long)1), Depot.oTypePays[this.cmbTypePay.SelectedIndex], Depot.oTypeBatchs.item((long)2), this._agents[this.cmbCashier.SelectedIndex]);
				if (batch3.get_Count() <= 0)
				{
					this.InitBatch();
				}
				else
				{
					this._batch = batch3[0];
				}
				this.lblAbonent.Text = Convert.ToString(this._batch.BatchAmount);
			}
			catch
			{
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdNext_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.chkKKM.Checked)
			{
				num = 1;
			}
			switch (this._type)
			{
				case 1:
				{
					try
					{
						if (this._batch.get_ID() != (long)0)
						{
							if (Tools.DateOnly(this._batch.BatchDate) != DateTime.Today)
							{
								if (MessageBox.Show("Хотите закрыть существующую пачку", "Закрытие пачки", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
								{
									break;
								}
								else
								{
									this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)2);
									this._batch.Save();
									this.InitBatch();
									if (this._batch.Save() != 0)
									{
										break;
									}
								}
							}
							frmCash _frmCash = new frmCash(this._batch, this._cardBatch, this._cashbalance, this._batchsagent, this._batchsother, num)
							{
								MdiParent = Depot._main
							};
							_frmCash.Show();
							_frmCash = null;
							base.Close();
						}
						else if (this._batch.Save() == 0)
						{
							frmCash _frmCash1 = new frmCash(this._batch, this._cardBatch, this._cashbalance, this._batchsagent, this._batchsother, num)
							{
								MdiParent = Depot._main
							};
							_frmCash1.Show();
							_frmCash1 = null;
							base.Close();
						}
						return;
					}
					catch
					{
						return;
					}
					break;
				}
				case 2:
				{
					try
					{
						if (this._batch.get_ID() != (long)0)
						{
							this._cardBatchs = new Batchs();
							this._cardBatchs.Load(Depot.oStatusBatchs.item((long)1), Depot.oTypePays.item((long)2), Depot.oTypeBatchs.item((long)1), this._agents[this.cmbCashier.SelectedIndex], 113);
							if (this._cardBatchs.get_Count() > 0)
							{
								this._cardBatch = this._cardBatchs[0];
								this._cardBatch.oStatusBatch = Depot.oStatusBatchs.item((long)2);
								this._cardBatch.Save();
							}
							this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)2);
							if (this._batch.Save() == 0)
							{
								this.PrintPKOGaz(this._batch);
								if (num == 1)
								{
									this.ECR = new FprnM45Class()
									{
										DeviceEnabled = true
									};
									if (this.ECR.ResultCode != 0)
									{
										break;
									}
									else if (this.ECR.GetStatus() == 0)
									{
										this.ECR.Password = "30";
										this.ECR.Mode = 3;
										if (this.ECR.SetMode() == 0)
										{
											this.ECR.ReportType = 1;
											if (this.ECR.Report() != 0)
											{
												break;
											}
										}
										else
										{
											break;
										}
									}
									else
									{
										break;
									}
								}
								base.Close();
								return;
							}
							else
							{
								break;
							}
						}
						else
						{
							MessageBox.Show("Не существует открытой смены - закрывать нечего!", "Закрытие кассовой смены", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							break;
						}
					}
					catch
					{
						return;
					}
					break;
				}
				case 3:
				{
					this.ShowBatchReport();
					base.Close();
					return;
				}
				case 4:
				{
					if (this.txtNumberBatch.Text.Length != 0)
					{
						try
						{
							Batch batch = new Batch()
							{
								oTypeBatch = Depot.oTypeBatchs.item((long)1),
								oDispatcher = this._agents[this.cmbCashier.SelectedIndex],
								NumberBatch = this.txtNumberBatch.Text,
								BatchAmount = Convert.ToDouble(this.txtSum.Text),
								oTypePay = Depot.oTypePays[this.cmbTypePay.SelectedIndex],
								BatchDate = (DateTime)this.dtDate.Value,
								oPeriod = Depot.CurrentPeriod,
								oStatusBatch = Depot.oStatusBatchs.item((long)1)
							};
							if (batch.Save() == 0)
							{
								frmBatch _frmBatch = new frmBatch(batch, false, new FprnM45Class());
								batch = null;
								_frmBatch.ShowDialog(Depot._main);
								_frmBatch = null;
								base.Close();
							}
							return;
						}
						catch
						{
							MessageBox.Show("Ошибка создания пачки!");
							return;
						}
					}
					else
					{
						MessageBox.Show("Укажите номер пачки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
					}
					break;
				}
				default:
				{
					return;
				}
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

		private void frmOpenCashChange_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmOpenCashChange_Load(object sender, EventArgs e)
		{
			try
			{
				this.dtDate.Value = DateTime.Today.Date;
				Tools.LoadWindows(this);
				this._agents = new Agents();
				switch (this._type)
				{
					case 1:
					{
						this.Text = "Открытие кассовой смены";
						Tools.FillC1(Depot.oTypePays, this.cmbTypePay, (long)1);
						this._agents.Load(Depot.oTypeAgents.item((long)2));
						if (Depot.oSettings.oAgent == null)
						{
							MessageBox.Show("Для вашего пользователя не установлено соответствие агенту!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							base.Close();
							return;
						}
						else
						{
							Tools.FillC1(this._agents, this.cmbCashier, Depot.oSettings.oAgent.get_ID());
							break;
						}
					}
					case 2:
					{
						this.Text = "Закрытие кассовой смены";
						this.cmdNext.Text = "Закрыть";
						Tools.FillC1(Depot.oTypePays, this.cmbTypePay, (long)1);
						this._agents.Load(Depot.oTypeAgents.item((long)2));
						if (Depot.oSettings.oAgent == null)
						{
							MessageBox.Show("Для вашего пользователя не установлено соответствие агенту!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							base.Close();
							return;
						}
						else
						{
							Tools.FillC1(this._agents, this.cmbCashier, Depot.oSettings.oAgent.get_ID());
							break;
						}
					}
					case 3:
					{
						this.Text = "Печать кассового отчета";
						Tools.FillC1(Depot.oTypePays, this.cmbTypePay, (long)1);
						this._agents.Load(Depot.oTypeAgents.item((long)2));
						if (Depot.oSettings.oAgent == null)
						{
							MessageBox.Show("Для вашего пользователя не установлено соответствие агенту!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							base.Close();
							return;
						}
						else
						{
							Tools.FillC1(this._agents, this.cmbCashier, Depot.oSettings.oAgent.get_ID());
							this.dtDate.Enabled = true;
							break;
						}
					}
					case 4:
					{
						this.Text = "Начать прием оплаты";
						Tools.FillC1(Depot.oTypePays, this.cmbTypePay, (long)2);
						this._agents.Load(Depot.oTypeAgents.item((long)4));
						if (Depot.oSettings.oAgent == null)
						{
							MessageBox.Show("Для вашего пользователя не установлено соответствие агенту!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							base.Close();
							return;
						}
						else
						{
							Tools.FillC1(this._agents, this.cmbCashier, Depot.oSettings.oAgent.get_ID());
							this.cmbCashier.Enabled = true;
							this.label2.Text = "Агент";
							this.txtNumberBatch.Enabled = true;
							this.txtSum.Enabled = true;
							this.cmbTypePay.Enabled = true;
							this.dtDate.Enabled = true;
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void InitBatch()
		{
			try
			{
				this._batch = new Batch()
				{
					oTypePay = Depot.oTypePays.item((long)1),
					oPeriod = Depot.CurrentPeriod,
					oCashier = this._agents[this.cmbCashier.SelectedIndex]
				};
				this._batch.set_Name("Оплата через кассу");
				this._batch.BatchDate = DateTime.Today;
				this._batch.oTypeBatch = Depot.oTypeBatchs.item((long)2);
				this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)1);
				string[] strArrays = new string[] { "0", Tools.ConvertDateFORSQL(DateTime.Today) };
				object obj = Saver.ExecuteFunction("fShowNumberBatchDispatcher", strArrays);
				if (obj == null)
				{
					this._batch.NumberBatch = "1";
				}
				else
				{
					this._batch.NumberBatch = obj.ToString();
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmOpenCashChange));
			this.pictureBox1 = new PictureBox();
			this.panel1 = new Panel();
			this.lblVidacha = new Label();
			this.label12 = new Label();
			this.txtSum = new TextBox();
			this.label9 = new Label();
			this.lblIncassation = new Label();
			this.label10 = new Label();
			this.cmbCashier = new C1Combo();
			this.dtDate = new C1DateEdit();
			this.cmbTypePay = new C1Combo();
			this.lblOther = new Label();
			this.lblAgent = new Label();
			this.lblAbonent = new Label();
			this.lblCash = new Label();
			this.label8 = new Label();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.txtNumberBatch = new TextBox();
			this.label4 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.cmdNext = new Button();
			this.cmdClose = new Button();
			this.chkKKM = new CheckBox();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.cmbCashier).BeginInit();
			((ISupportInitialize)this.dtDate).BeginInit();
			((ISupportInitialize)this.cmbTypePay).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.BackColor = SystemColors.ActiveCaption;
			this.pictureBox1.Image = (Image)resourceManager.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(144, 336);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.panel1.BorderStyle = BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.chkKKM);
			this.panel1.Controls.Add(this.lblVidacha);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.txtSum);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.lblIncassation);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.cmbCashier);
			this.panel1.Controls.Add(this.dtDate);
			this.panel1.Controls.Add(this.cmbTypePay);
			this.panel1.Controls.Add(this.lblOther);
			this.panel1.Controls.Add(this.lblAgent);
			this.panel1.Controls.Add(this.lblAbonent);
			this.panel1.Controls.Add(this.lblCash);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.txtNumberBatch);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Cursor = Cursors.IBeam;
			this.panel1.Location = new Point(144, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(300, 336);
			this.panel1.TabIndex = 1;
			this.lblVidacha.BackColor = SystemColors.Info;
			this.lblVidacha.BorderStyle = BorderStyle.FixedSingle;
			this.lblVidacha.Location = new Point(136, 312);
			this.lblVidacha.Name = "lblVidacha";
			this.lblVidacha.Size = new System.Drawing.Size(128, 20);
			this.lblVidacha.TabIndex = 21;
			this.label12.Location = new Point(8, 312);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(128, 24);
			this.label12.TabIndex = 20;
			this.label12.Text = "Выдача денег, тенге:";
			this.txtSum.BorderStyle = BorderStyle.FixedSingle;
			this.txtSum.Enabled = false;
			this.txtSum.Location = new Point(104, 72);
			this.txtSum.Name = "txtSum";
			this.txtSum.Size = new System.Drawing.Size(184, 20);
			this.txtSum.TabIndex = 19;
			this.txtSum.Text = "";
			this.label9.Location = new Point(9, 72);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(96, 24);
			this.label9.TabIndex = 18;
			this.label9.Text = "Сумма пачки";
			this.lblIncassation.BackColor = SystemColors.Info;
			this.lblIncassation.BorderStyle = BorderStyle.FixedSingle;
			this.lblIncassation.Location = new Point(136, 288);
			this.lblIncassation.Name = "lblIncassation";
			this.lblIncassation.Size = new System.Drawing.Size(128, 20);
			this.lblIncassation.TabIndex = 17;
			this.label10.Location = new Point(8, 288);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(128, 24);
			this.label10.TabIndex = 16;
			this.label10.Text = "Инкассация, тенге:";
			this.cmbCashier.AddItemSeparator = ';';
			this.cmbCashier.BorderStyle = 1;
			this.cmbCashier.Caption = "";
			this.cmbCashier.CaptionHeight = 17;
			this.cmbCashier.CharacterCasing = 0;
			this.cmbCashier.ColumnCaptionHeight = 17;
			this.cmbCashier.ColumnFooterHeight = 17;
			this.cmbCashier.ColumnHeaders = false;
			this.cmbCashier.ColumnWidth = 100;
			this.cmbCashier.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbCashier.ContentHeight = 15;
			this.cmbCashier.DataMode = DataModeEnum.AddItem;
			this.cmbCashier.DeadAreaBackColor = Color.Empty;
			this.cmbCashier.EditorBackColor = SystemColors.Window;
			this.cmbCashier.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbCashier.EditorForeColor = SystemColors.WindowText;
			this.cmbCashier.EditorHeight = 15;
			this.cmbCashier.Enabled = false;
			this.cmbCashier.FlatStyle = FlatModeEnum.Flat;
			this.cmbCashier.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbCashier.ItemHeight = 15;
			this.cmbCashier.Location = new Point(104, 40);
			this.cmbCashier.MatchEntryTimeout = (long)2000;
			this.cmbCashier.MaxDropDownItems = 5;
			this.cmbCashier.MaxLength = 32767;
			this.cmbCashier.MouseCursor = Cursors.Default;
			this.cmbCashier.Name = "cmbCashier";
			this.cmbCashier.RowDivider.Color = Color.DarkGray;
			this.cmbCashier.RowDivider.Style = LineStyleEnum.None;
			this.cmbCashier.RowSubDividerColor = Color.DarkGray;
			this.cmbCashier.Size = new System.Drawing.Size(184, 19);
			this.cmbCashier.TabIndex = 2;
			this.cmbCashier.TextChanged += new EventHandler(this.cmbCashier_TextChanged);
			this.cmbCashier.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{AlignVert:Center;Border:None,,0, 0, 0, 0;BackColor:ControlDark;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style1{}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;BackColor:Control;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;AlignVert:Center;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style9{AlignHorz:Near;}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.dtDate.BorderStyle = 1;
			this.dtDate.Enabled = false;
			this.dtDate.FormatType = FormatTypeEnum.LongDate;
			this.dtDate.Location = new Point(104, 8);
			this.dtDate.Name = "dtDate";
			this.dtDate.Size = new System.Drawing.Size(184, 18);
			this.dtDate.TabIndex = 1;
			this.dtDate.Tag = null;
			this.dtDate.Value = new DateTime(2006, 11, 8, 0, 0, 0, 0);
			this.dtDate.VisibleButtons = DropDownControlButtonFlags.DropDown;
			this.cmbTypePay.AddItemSeparator = ';';
			this.cmbTypePay.BorderStyle = 1;
			this.cmbTypePay.Caption = "";
			this.cmbTypePay.CaptionHeight = 17;
			this.cmbTypePay.CharacterCasing = 0;
			this.cmbTypePay.ColumnCaptionHeight = 17;
			this.cmbTypePay.ColumnFooterHeight = 17;
			this.cmbTypePay.ColumnHeaders = false;
			this.cmbTypePay.ColumnWidth = 100;
			this.cmbTypePay.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbTypePay.ContentHeight = 15;
			this.cmbTypePay.DataMode = DataModeEnum.AddItem;
			this.cmbTypePay.DeadAreaBackColor = Color.Empty;
			this.cmbTypePay.EditorBackColor = SystemColors.Window;
			this.cmbTypePay.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbTypePay.EditorForeColor = SystemColors.WindowText;
			this.cmbTypePay.EditorHeight = 15;
			this.cmbTypePay.Enabled = false;
			this.cmbTypePay.FlatStyle = FlatModeEnum.Flat;
			this.cmbTypePay.Images.Add((Image)resourceManager.GetObject("resource1"));
			this.cmbTypePay.ItemHeight = 15;
			this.cmbTypePay.Location = new Point(104, 136);
			this.cmbTypePay.MatchEntryTimeout = (long)2000;
			this.cmbTypePay.MaxDropDownItems = 5;
			this.cmbTypePay.MaxLength = 32767;
			this.cmbTypePay.MouseCursor = Cursors.Default;
			this.cmbTypePay.Name = "cmbTypePay";
			this.cmbTypePay.RowDivider.Color = Color.DarkGray;
			this.cmbTypePay.RowDivider.Style = LineStyleEnum.None;
			this.cmbTypePay.RowSubDividerColor = Color.DarkGray;
			this.cmbTypePay.Size = new System.Drawing.Size(184, 19);
			this.cmbTypePay.TabIndex = 4;
			this.cmbTypePay.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.lblOther.BackColor = SystemColors.Info;
			this.lblOther.BorderStyle = BorderStyle.FixedSingle;
			this.lblOther.Location = new Point(136, 264);
			this.lblOther.Name = "lblOther";
			this.lblOther.Size = new System.Drawing.Size(128, 20);
			this.lblOther.TabIndex = 15;
			this.lblAgent.BackColor = SystemColors.Info;
			this.lblAgent.BorderStyle = BorderStyle.FixedSingle;
			this.lblAgent.Location = new Point(136, 240);
			this.lblAgent.Name = "lblAgent";
			this.lblAgent.Size = new System.Drawing.Size(128, 20);
			this.lblAgent.TabIndex = 14;
			this.lblAbonent.BackColor = SystemColors.Info;
			this.lblAbonent.BorderStyle = BorderStyle.FixedSingle;
			this.lblAbonent.Location = new Point(136, 216);
			this.lblAbonent.Name = "lblAbonent";
			this.lblAbonent.Size = new System.Drawing.Size(128, 20);
			this.lblAbonent.TabIndex = 13;
			this.lblCash.BackColor = SystemColors.Info;
			this.lblCash.BorderStyle = BorderStyle.FixedSingle;
			this.lblCash.Location = new Point(136, 192);
			this.lblCash.Name = "lblCash";
			this.lblCash.Size = new System.Drawing.Size(128, 20);
			this.lblCash.TabIndex = 12;
			this.label8.Location = new Point(8, 264);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 24);
			this.label8.TabIndex = 11;
			this.label8.Text = "Прочие, тенге:";
			this.label7.Location = new Point(8, 240);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(128, 24);
			this.label7.TabIndex = 10;
			this.label7.Text = "От агентов, тенге:";
			this.label6.Location = new Point(8, 216);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 24);
			this.label6.TabIndex = 9;
			this.label6.Text = "От абонентов, тенге:";
			this.label5.Location = new Point(8, 192);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 24);
			this.label5.TabIndex = 8;
			this.label5.Text = "Всего в кассе, тенге:";
			this.txtNumberBatch.BorderStyle = BorderStyle.FixedSingle;
			this.txtNumberBatch.Enabled = false;
			this.txtNumberBatch.Location = new Point(104, 104);
			this.txtNumberBatch.Name = "txtNumberBatch";
			this.txtNumberBatch.Size = new System.Drawing.Size(184, 20);
			this.txtNumberBatch.TabIndex = 3;
			this.txtNumberBatch.Text = "";
			this.label4.Location = new Point(8, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 24);
			this.label4.TabIndex = 3;
			this.label4.Text = "Тип оплаты";
			this.label3.Location = new Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 24);
			this.label3.TabIndex = 2;
			this.label3.Text = "Номер пачки";
			this.label2.Location = new Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "Кассир";
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Дата";
			this.cmdNext.FlatStyle = FlatStyle.Flat;
			this.cmdNext.Location = new Point(248, 344);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(92, 24);
			this.cmdNext.TabIndex = 5;
			this.cmdNext.Text = "Далее >";
			this.cmdNext.Click += new EventHandler(this.cmdNext_Click);
			this.cmdClose.FlatStyle = FlatStyle.Flat;
			this.cmdClose.Location = new Point(352, 344);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(92, 24);
			this.cmdClose.TabIndex = 6;
			this.cmdClose.Text = "Отмена";
			this.cmdClose.Click += new EventHandler(this.cmdClose_Click);
			this.chkKKM.Checked = true;
			this.chkKKM.CheckState = CheckState.Checked;
			this.chkKKM.Location = new Point(104, 168);
			this.chkKKM.Name = "chkKKM";
			this.chkKKM.Size = new System.Drawing.Size(184, 16);
			this.chkKKM.TabIndex = 22;
			this.chkKKM.Text = "Прием оплаты с ККМ";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(450, 375);
			base.Controls.Add(this.cmdClose);
			base.Controls.Add(this.cmdNext);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.pictureBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmOpenCashChange";
			this.Text = "Открытие (редактирование) кассовой смены";
			base.Closing += new CancelEventHandler(this.frmOpenCashChange_Closing);
			base.Load += new EventHandler(this.frmOpenCashChange_Load);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.cmbCashier).EndInit();
			((ISupportInitialize)this.dtDate).EndInit();
			((ISupportInitialize)this.cmbTypePay).EndInit();
			base.ResumeLayout(false);
		}

		public bool PrintPKO(Batch oBatch)
		{
			bool flag = false;
			try
			{
				double batchAmount = 0;
				this._batchs1 = new Batchs();
				this._batchs1.Load(oBatch.BatchDate, oBatch.oCashier);
				foreach (Batch batch in this._batchs1)
				{
					if (batch.oDispatcher == null)
					{
						batchAmount += batch.BatchAmount;
					}
					else
					{
						if (batch.oDispatcher.get_ID() == (long)113)
						{
							continue;
						}
						batchAmount += batch.BatchAmount;
					}
				}
				string str = "";
				string str1 = "";
				if (oBatch.oCashier != null)
				{
					str1 = oBatch.oCashier.get_Name().ToString();
				}
				string str2 = "";
				string str3 = "";
				string[] name = new string[] { oBatch.get_ID().ToString(), "1" };
				if (Saver.ExecuteFunction("fGetSumOfAmountDocumentByIDBatch", name) == null)
				{
					str2 = batchAmount.ToString();
					str3 = Tools.ConvertCurencyInString(batchAmount);
				}
				else
				{
					str2 = batchAmount.ToString();
					str3 = Tools.ConvertCurencyInString((double)Convert.ToInt64(batchAmount));
				}
				name = new string[] { "zAccount", "zDay", "zMonth", "zYear", "zMonth2", "zBatchAmount", "zNameCashier", "zBatchAmountStr", "zNameDispetcher", "zNumber" };
				string[] strArrays = name;
				name = new string[] { str, null, null, null, null, null, null, null, null, null };
				int day = DateTime.Today.Day;
				name[1] = day.ToString();
				day = DateTime.Today.Month;
				name[2] = day.ToString();
				day = DateTime.Today.Year;
				name[3] = day.ToString();
				DateTime today = DateTime.Today;
				name[4] = Tools.NameMonth(today.Month);
				name[5] = str2;
				name[6] = this._batch.oCashier.get_Name();
				name[7] = str3;
				name[8] = str1;
				name[9] = oBatch.NumberBatch.ToString();
				string[] strArrays1 = name;
				Tools.ShowWordDocument(string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\ПКО.dot"), strArrays, strArrays1);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		public bool PrintPKOGaz(Batch oBatch)
		{
			DateTime today;
			int day;
			bool flag = false;
			try
			{
				double amountOperation = 0;
				double num = 0;
				this._batchs1 = new Batchs();
				this._batchs1.Load(oBatch.BatchDate, oBatch.oCashier);
				foreach (Batch batch in this._batchs1)
				{
					if (batch.oDispatcher != null)
					{
						if (batch.oDispatcher.get_ID() == (long)113)
						{
							continue;
						}
						foreach (Document oDocument in batch.oDocuments)
						{
							if (oDocument.DocumentAmount <= 0 || oDocument.oContract.oPerson.isJuridical != 0)
							{
								continue;
							}
							foreach (Operation oOperation in oDocument.oOperations)
							{
								if (oOperation.oBalance.oAccounting.get_ID() != (long)6)
								{
									amountOperation += oOperation.AmountOperation;
								}
								else
								{
									num += oOperation.AmountOperation;
								}
							}
						}
					}
					else
					{
						foreach (Document document in batch.oDocuments)
						{
							if (document.DocumentAmount <= 0 || document.oContract.oPerson.isJuridical != 0)
							{
								continue;
							}
							foreach (Operation operation in document.oOperations)
							{
								if (operation.oBalance.oAccounting.get_ID() != (long)6)
								{
									amountOperation += operation.AmountOperation;
								}
								else
								{
									num += operation.AmountOperation;
								}
							}
						}
					}
				}
				num = Math.Round(num, 2);
				amountOperation = Math.Round(amountOperation, 2);
				string str = "";
				if (oBatch.oCashier != null)
				{
					str = oBatch.oCashier.get_Name().ToString();
				}
				string str1 = "";
				string str2 = "";
				string str3 = "";
				string str4 = "";
				string[] name = new string[] { oBatch.get_ID().ToString(), "1" };
				if (Saver.ExecuteFunction("fGetSumOfAmountDocumentByIDBatch", name) == null)
				{
					str1 = amountOperation.ToString();
					str2 = Tools.ConvertCurencyInString(amountOperation);
					str3 = num.ToString();
					str4 = Tools.ConvertCurencyInString(num);
				}
				else
				{
					str1 = amountOperation.ToString();
					str2 = Tools.ConvertCurencyInString((double)Convert.ToInt64(amountOperation));
					str3 = num.ToString();
					str4 = Tools.ConvertCurencyInString((double)Convert.ToInt64(num));
				}
				str = "физических лиц";
				name = new string[] { "zBase", "zAccount", "zDay", "zMonth", "zYear", "zMonth2", "zBatchAmount", "zNameCashier", "zBatchAmountStr", "zNameDispetcher", "zNumber" };
				string[] strArrays = name;
				if (amountOperation > 0)
				{
					name = new string[] { "за газ", "1211", null, null, null, null, null, null, null, null, null };
					day = DateTime.Today.Day;
					name[2] = day.ToString();
					day = DateTime.Today.Month;
					name[3] = day.ToString();
					day = DateTime.Today.Year;
					name[4] = day.ToString();
					today = DateTime.Today;
					name[5] = Tools.NameMonth(today.Month);
					name[6] = str1;
					name[7] = this._batch.oCashier.get_Name();
					name[8] = str2;
					name[9] = str;
					name[10] = oBatch.NumberBatch.ToString();
					string[] strArrays1 = name;
					Tools.ShowWordDocument(string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\ПКО2.dot"), strArrays, strArrays1);
				}
				if (num > 0)
				{
					name = new string[] { "за дополнительные услуги", "1212", null, null, null, null, null, null, null, null, null };
					day = DateTime.Today.Day;
					name[2] = day.ToString();
					day = DateTime.Today.Month;
					name[3] = day.ToString();
					day = DateTime.Today.Year;
					name[4] = day.ToString();
					today = DateTime.Today;
					name[5] = Tools.NameMonth(today.Month);
					name[6] = str3;
					name[7] = this._batch.oCashier.get_Name();
					name[8] = str4;
					name[9] = str;
					name[10] = oBatch.NumberBatch.ToString();
					string[] strArrays2 = name;
					Tools.ShowWordDocument(string.Concat(Depot.oSettings.ReportPath.Trim(), "Templates\\ПКО2.dot"), strArrays, strArrays2);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				EventsLog.SaveLog(string.Concat("Ошибка формирования отчета - РКО! ", exception.Message), 3);
				flag = true;
			}
			return !flag;
		}

		private bool ShowBatchReport()
		{
			bool flag;
			try
			{
				try
				{
					System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
					int[] numArray = new int[] { 2, 3 };
					string[] str = new string[] { "@IDCashier", "@DateB" };
					string[] strArrays = str;
					str = new string[2];
					long d = this._agents[this.cmbCashier.SelectedIndex].get_ID();
					str[0] = d.ToString();
					str[1] = this.dtDate.Value.ToString();
					string[] strArrays1 = str;
					string str1 = string.Concat(Depot.oSettings.ReportPath.Trim(), "repCashRep.rpt");
					frmReports frmReport = new frmReports(str1, strArrays, strArrays1, numArray)
					{
						Text = string.Concat("Кассовый отчет за", this.dtDate.Text),
						MdiParent = base.Owner
					};
					frmReport.Show();
					frmReport = null;
					flag = true;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
					flag = false;
				}
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			return flag;
		}
	}
}