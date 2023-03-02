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
	public class frmDocVerifys : Form
	{
		private ListView lv;

		private ColumnHeader columnHeader1;

		private ToolBar tbData;

		private ToolBarButton tbAdd;

		private ToolBarButton toolBarButton16;

		private ToolBarButton toolBarButton23;

		private ToolBarButton toolBarButton25;

		private ToolBarButton toolBarButton26;

		private ToolBarButton toolBarButton27;

		private ToolBarButton toolBarButton31;

		private ToolBarButton toolBarButton1;

		private ToolBarButton toolBarButton2;

		private ToolBarButton toolBarButton3;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuItem4;

		private IContainer components;

		private ImageList imageList1;

		private MenuItem menuAdd;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuPrint;

		private ColumnHeader columnHeader9;

		private ColumnHeader columnHeader10;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private DateTime DateBegin;

		private DateTime DateEnd;

		private ListViewSortManager m_sortMgr1;

		private Documents _docs;

		private Documents _docs2;

		private string filter;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private int status;

		private ColumnHeader columnHeader11;

		private int idagent;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader13;

		private ToolBarButton toolBarButton4;

		private ToolBarButton toolBarButton5;

		private ToolBarButton toolBarButton6;

		private TypeReasonDisconnects _TypeReasonDisconnects;

		public frmDocVerifys()
		{
			this.InitializeComponent();
		}

		private void AddDoc()
		{
			try
			{
				Document document = null;
				document = this._docs.Add();
				if ((new frmDocVerify(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this.FillOneItem(document, true);
				}
				document = null;
				base.Focus();
			}
			catch
			{
			}
		}

		private string CalcSummCorrec()
		{
			int num = 0;
			double value = 0;
			Tariffs tariff = new Tariffs();
			Period period = new Period();
			Periods period1 = new Periods();
			foreach (ListViewItem item in this.lv.Items)
			{
				try
				{
					this.Cursor = Cursors.WaitCursor;
					if (item.Checked)
					{
						period1.Load(Convert.ToDateTime(item.SubItems[0].Text));
						tariff.Load(period1[0]);
						Tariff item1 = tariff[0];
						double num1 = Convert.ToDouble(item.SubItems[7].Text.Replace(".", ","));
						value = value + num1 * item1.Value;
						num++;
					}
				}
				catch
				{
				}
			}
			string[] str = new string[] { "Будет проведено ", num.ToString(), " документа(-ов) на сумму ", value.ToString(), " тг." };
			return string.Concat(str);
		}

		private void CreateCorrec()
		{
			if (MessageBox.Show(string.Concat("Вы действительно хотите провести корректировки начисления? \n", this.CalcSummCorrec()), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}
			if (MessageBox.Show(string.Concat("Вы подтвердили проведение корректировок. Продолжить?\n", this.CalcSummCorrec()), "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
			{
				return;
			}
			try
			{
				int num = 0;
				this.Cursor = Cursors.WaitCursor;
				Accountings accounting = new Accountings();
				accounting.Load();
				Tariffs tariff = new Tariffs();
				Period period = new Period();
				Periods period1 = new Periods();
				int num1 = 0;
				int num2 = 0;
				foreach (ListViewItem item in this.lv.Items)
				{
					try
					{
						num = 0;
						this.Cursor = Cursors.WaitCursor;
						if (item.Checked)
						{
							Contracts contract = new Contracts();
							contract.Load(item.SubItems[1].Text);
							if (contract[0] != null)
							{
								Contract item1 = contract[0];
								period1.Load(Convert.ToDateTime(item.SubItems[0].Text));
								tariff.Load(period1[0]);
								Tariff tariff1 = tariff[0];
								double num3 = Convert.ToDouble(item.SubItems[7].Text.Replace(".", ","));
								double value = num3 * tariff1.Value;
								Document document = new Document()
								{
									oBatch = null,
									oContract = item1,
									oPeriod = Depot.CurrentPeriod,
									oTypeDocument = Depot.oTypeDocuments.item((long)7),
									DocumentAmount = value,
									DocumentDate = DateTime.Today,
									Note = "за объем, учтенный при поверке"
								};
								if (document.Save() == 0)
								{
									PD str = document.oPDs.Add();
									str.oTypePD = Depot.oTypePDs.item((long)35);
									str.oDocument = document;
									str.Value = "0";
									if (str.Save() == 0)
									{
										str = document.oPDs.Add();
										str.oTypePD = Depot.oTypePDs.item((long)16);
										str.oDocument = document;
										str.Value = "0";
										if (str.Save() == 0)
										{
											str = document.oPDs.Add();
											str.oTypePD = Depot.oTypePDs.item((long)13);
											str.oDocument = document;
											str.Value = tariff1.Value.ToString();
											if (str.Save() == 0)
											{
												PD pD = document.oPDs.Add();
												pD.oTypePD = Depot.oTypePDs.item((long)34);
												pD.oDocument = document;
												pD.Value = "1";
												if (pD.Save() == 0)
												{
													Balance currentPeriod = item1.CurrentBalance(accounting.item((long)1));
													if (currentPeriod == null)
													{
														currentPeriod = item1.oBalances.Add();
														currentPeriod.oAccounting = accounting.item((long)1);
														currentPeriod.oPeriod = Depot.CurrentPeriod;
														currentPeriod.oContract = item1;
														currentPeriod.AmountBalance = 0;
														currentPeriod.AmountCharge = 0;
														currentPeriod.AmountPay = 0;
													}
													Balance amountBalance = currentPeriod;
													amountBalance.AmountBalance = amountBalance.AmountBalance + value;
													if (currentPeriod.Save() == 0)
													{
														Operation operation = new Operation()
														{
															DateOperation = DateTime.Today,
															AmountOperation = value,
															oBalance = currentPeriod,
															oDocument = document,
															oTypeOperation = Depot.oTypeOperations.item((long)3)
														};
														if (operation.Save() != 0)
														{
															document.Delete();
															MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
														}
														else if (num3 != 0)
														{
															Gobject gobject = new Gobject();
															gobject = item1.oGobjects[0];
															FactUse factUse = new FactUse()
															{
																oPeriod = Depot.CurrentPeriod,
																FactAmount = num3 * -1,
																oGobject = gobject,
																oDocument = document,
																oTypeFU = Depot.oTypeFUs.item((long)1),
																oOperation = operation
															};
															if (factUse.Save() == 0)
															{
																goto Label0;
															}
															document.Delete();
															MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
														}
														else
														{
															goto Label0;
														}
													}
													else
													{
														document.Delete();
														MessageBox.Show("Ошибка сохранения данных!", "Запись данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
													}
												}
												else
												{
													document.Delete();
												}
											}
											else
											{
												document.Delete();
											}
										}
										else
										{
											document.Delete();
										}
									}
									else
									{
										document.Delete();
									}
								}
							}
							item.BackColor = Color.Red;
							num1++;
							num = 1;
						Label0:
							if (num == 0)
							{
								item.BackColor = Color.LightGreen;
								num2++;
							}
						}
					}
					catch
					{
						item.BackColor = Color.Red;
						num1++;
					}
				}
				this.Cursor = Cursors.Default;
				string[] strArrays = new string[] { "Успешно сохранено ", num2.ToString(), " документов, ", num1.ToString(), " не сохранено!" };
				MessageBox.Show(string.Concat(strArrays), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch
			{
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка проведения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void DeleteDoc()
		{
			try
			{
				if (this.lv.SelectedItems.Count > 0 && MessageBox.Show("Удалить текущий документ?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					if (this._docs.Remove(Convert.ToInt64(this.lv.SelectedItems[0].Tag)) != 0)
					{
						MessageBox.Show("Ошибка удаления объекта!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						this.lv.Items.Remove(this.lv.SelectedItems[0]);
					}
				}
			}
			catch
			{
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
			try
			{
				if (this.lv.SelectedItems.Count > 0)
				{
					Document document = null;
					document = this._docs.item(Convert.ToInt64(this.lv.SelectedItems[0].Tag));
					if ((new frmDocVerify(document)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
					{
						this.FillOneItem(document, false);
					}
					document = null;
					base.Focus();
				}
			}
			catch
			{
			}
		}

		private void FillList()
		{
			double num;
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.lv.Items.Clear();
				this._docs = new Documents();
				this._docs2 = new Documents();
				this.RefreshCaption();
				this._docs.Load(Depot.oTypeDocuments.item((long)22), this.DateBegin, this.DateEnd, this.status, this.idagent);
				foreach (Document _doc in this._docs)
				{
					DateTime documentDate = _doc.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = _doc.get_ID().ToString()
					};
					listViewItem.SubItems.Add(_doc.oContract.Account);
					listViewItem.SubItems.Add(_doc.oContract.oPerson.oAddress.get_ShortAddress());
					Gmeter gmeter = new Gmeter();
					double documentAmount = _doc.DocumentAmount;
					double num1 = Convert.ToDouble(_doc.GetNamePD(27).Replace(".", ","));
					if (gmeter.Load(Convert.ToInt64(_doc.GetNamePD(7))) != 0)
					{
						listViewItem.SubItems.Add("???");
						listViewItem.SubItems.Add("???");
						listViewItem.SubItems.Add(documentAmount.ToString());
						listViewItem.SubItems.Add(num1.ToString());
						ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
						num = Math.Round(num1 - documentAmount, 3);
						subItems.Add(num.ToString());
						if (Convert.ToInt16(_doc.GetNamePD(30)) != 1)
						{
							listViewItem.SubItems.Add("не прошел");
							listViewItem.Checked = false;
						}
						else
						{
							listViewItem.SubItems.Add("прошел");
							listViewItem.Checked = true;
						}
						listViewItem.SubItems.Add("???");
					}
					else
					{
						listViewItem.SubItems.Add(gmeter.oTypeGMeter.Fullname);
						listViewItem.SubItems.Add(gmeter.SerialNumber);
						listViewItem.SubItems.Add(documentAmount.ToString());
						listViewItem.SubItems.Add(num1.ToString());
						ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
						num = Math.Round(num1 - documentAmount, 3);
						listViewSubItemCollections.Add(num.ToString());
						if (Convert.ToInt16(_doc.GetNamePD(30)) != 1)
						{
							listViewItem.SubItems.Add("не прошел");
							listViewItem.Checked = false;
						}
						else
						{
							listViewItem.SubItems.Add("прошел");
							listViewItem.Checked = true;
						}
						listViewItem.SubItems.Add(gmeter.oStatusGMeter.get_Name());
					}
					Agent agent = new Agent();
					if (agent.Load(Convert.ToInt64(_doc.GetNamePD(16))) != 0)
					{
						listViewItem.SubItems.Add("???");
					}
					else
					{
						listViewItem.SubItems.Add(agent.get_Name());
					}
					listViewItem.SubItems.Add(_doc.Note);
					try
					{
						if (gmeter.oStatusGMeter.get_Name() == "Отключен")
						{
							this._docs2 = new Documents();
							this._docs2.Load(_doc.oContract, Depot.oTypeDocuments.item((long)18));
							IEnumerator enumerator = this._docs2.GetEnumerator();
							try
							{
								if (enumerator.MoveNext())
								{
									Document current = (Document)enumerator.Current;
									try
									{
										string str = this._TypeReasonDisconnects.item((long)Convert.ToInt32(current.GetNamePD(33))).get_Name().ToString();
										listViewItem.SubItems.Add(Convert.ToString(str));
									}
									catch
									{
										listViewItem.SubItems.Add("");
									}
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
						}
					}
					catch
					{
						listViewItem.SubItems.Add("");
					}
					this.lv.Items.Add(listViewItem);
				}
				string[] strArrays = new string[2];
				int count = this.lv.Items.Count;
				strArrays[0] = string.Concat("Загружено: ", count.ToString());
				strArrays[1] = "";
				Depot.status = strArrays;
				this.Cursor = Cursors.Default;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				this.lv.Items.Clear();
				this.Cursor = Cursors.Default;
				MessageBox.Show(string.Concat("Ошибка загрузки данных!", exception.Message), "Загрузка данных", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FillOneItem(Document o, bool isAdd)
		{
			Gmeter gmeter;
			Agent agent;
			double num;
			try
			{
				this.Cursor = Cursors.WaitCursor;
				this.RefreshCaption();
				if (!isAdd)
				{
					this.lv.SelectedItems[0].SubItems[0].Text = o.DocumentDate.ToShortDateString();
					this.lv.SelectedItems[0].SubItems[1].Text = o.oContract.Account;
					this.lv.SelectedItems[0].SubItems[2].Text = o.oContract.oPerson.oAddress.get_ShortAddress();
					gmeter = new Gmeter();
					if (gmeter.Load(Convert.ToInt64(o.GetNamePD(7))) != 0)
					{
						this.lv.SelectedItems[0].SubItems[3].Text = "???";
						this.lv.SelectedItems[0].SubItems[7].Text = "???";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[3].Text = gmeter.SerialNumber;
						this.lv.SelectedItems[0].SubItems[7].Text = gmeter.oStatusGMeter.get_Name();
					}
					this.lv.SelectedItems[0].SubItems[4].Text = o.DocumentAmount.ToString();
					ListViewItem.ListViewSubItem item = this.lv.SelectedItems[0].SubItems[5];
					num = Convert.ToDouble(o.GetNamePD(27).Replace(".", ","));
					item.Text = num.ToString();
					if (Convert.ToInt16(o.GetNamePD(30)) != 1)
					{
						this.lv.SelectedItems[0].SubItems[6].Text = "не прошел";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[6].Text = "прошел";
					}
					agent = new Agent();
					if (agent.Load(Convert.ToInt64(o.GetNamePD(16))) != 0)
					{
						this.lv.SelectedItems[0].SubItems[8].Text = "???";
					}
					else
					{
						this.lv.SelectedItems[0].SubItems[8].Text = agent.get_Name();
					}
					this.lv.SelectedItems[0].SubItems[9].Text = o.Note;
				}
				else
				{
					DateTime documentDate = o.DocumentDate;
					ListViewItem listViewItem = new ListViewItem(documentDate.ToShortDateString())
					{
						Tag = o.get_ID().ToString()
					};
					listViewItem.SubItems.Add(o.oContract.Account);
					listViewItem.SubItems.Add(o.oContract.oPerson.oAddress.get_ShortAddress());
					gmeter = new Gmeter();
					if (gmeter.Load(Convert.ToInt64(o.GetNamePD(7))) != 0)
					{
						listViewItem.SubItems.Add("???");
						ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
						num = o.DocumentAmount;
						subItems.Add(num.ToString());
						ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
						num = Convert.ToDouble(o.GetNamePD(27).Replace(".", ","));
						listViewSubItemCollections.Add(num.ToString());
						if (Convert.ToInt16(o.GetNamePD(30)) != 1)
						{
							listViewItem.SubItems.Add("не прошел");
						}
						else
						{
							listViewItem.SubItems.Add("прошел");
						}
						listViewItem.SubItems.Add("???");
					}
					else
					{
						listViewItem.SubItems.Add(gmeter.SerialNumber);
						ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
						num = o.DocumentAmount;
						subItems1.Add(num.ToString());
						ListViewItem.ListViewSubItemCollection listViewSubItemCollections1 = listViewItem.SubItems;
						num = Convert.ToDouble(o.GetNamePD(27).Replace(".", ","));
						listViewSubItemCollections1.Add(num.ToString());
						if (Convert.ToInt16(o.GetNamePD(30)) != 1)
						{
							listViewItem.SubItems.Add("не прошел");
						}
						else
						{
							listViewItem.SubItems.Add("прошел");
						}
						listViewItem.SubItems.Add(gmeter.oStatusGMeter.get_Name());
					}
					agent = new Agent();
					if (agent.Load(Convert.ToInt64(o.GetNamePD(16))) != 0)
					{
						listViewItem.SubItems.Add("???");
					}
					else
					{
						listViewItem.SubItems.Add(agent.get_Name());
					}
					listViewItem.SubItems.Add(o.Note);
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

		private void frmDocVerifys_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmDocVerifys_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Insert:
				{
					this.AddDoc();
					return;
				}
				case Keys.Delete:
				{
					this.DeleteDoc();
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void frmDocVerifys_Load(object sender, EventArgs e)
		{
			try
			{
				this._TypeReasonDisconnects = new TypeReasonDisconnects();
				this._TypeReasonDisconnects.Load();
				Tools.LoadWindows(this);
				this.filter = "не установлен";
				this.status = 0;
				this.idagent = 0;
				DateTime today = DateTime.Today;
				DateTime dateTime = DateTime.Today;
				this.DateBegin = today.AddDays((double)(-dateTime.Day + 1));
				today = DateTime.Today;
				int day = -DateTime.Today.Day;
				int year = DateTime.Today.Year;
				dateTime = DateTime.Today;
				this.DateEnd = today.AddDays((double)(day + DateTime.DaysInMonth(year, dateTime.Month)));
				ListView listView = this.lv;
				Type[] typeArray = new Type[] { typeof(ListViewDateSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewDoubleSort), typeof(ListViewDoubleSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort), typeof(ListViewTextSort) };
				this.m_sortMgr1 = new ListViewSortManager(listView, typeArray);
				this.FillList();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmDocVerifys));
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader13 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.tbData = new ToolBar();
			this.tbAdd = new ToolBarButton();
			this.toolBarButton16 = new ToolBarButton();
			this.toolBarButton23 = new ToolBarButton();
			this.toolBarButton25 = new ToolBarButton();
			this.toolBarButton26 = new ToolBarButton();
			this.toolBarButton27 = new ToolBarButton();
			this.toolBarButton31 = new ToolBarButton();
			this.toolBarButton1 = new ToolBarButton();
			this.toolBarButton2 = new ToolBarButton();
			this.toolBarButton3 = new ToolBarButton();
			this.toolBarButton4 = new ToolBarButton();
			this.toolBarButton5 = new ToolBarButton();
			this.toolBarButton6 = new ToolBarButton();
			this.imageList1 = new ImageList(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuAdd = new MenuItem();
			this.menuEdit = new MenuItem();
			this.menuDel = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.menuPrint = new MenuItem();
			base.SuspendLayout();
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			this.lv.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader10, this.columnHeader12, this.columnHeader9, this.columnHeader6, this.columnHeader7, this.columnHeader13, this.columnHeader5, this.columnHeader8, this.columnHeader3, this.columnHeader4, this.columnHeader11 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 28);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(928, 417);
			this.lv.TabIndex = 2;
			this.lv.View = View.Details;
			this.lv.KeyPress += new KeyPressEventHandler(this.lv_KeyPress);
			this.lv.DoubleClick += new EventHandler(this.lv_DoubleClick);
			this.columnHeader1.Text = "Дата поверки";
			this.columnHeader1.Width = 85;
			this.columnHeader2.Text = "Л/счет";
			this.columnHeader2.Width = 66;
			this.columnHeader10.Text = "Адрес";
			this.columnHeader10.Width = 137;
			this.columnHeader12.Text = "Марка ПУ";
			this.columnHeader9.Text = "№ ПУ";
			this.columnHeader9.Width = 79;
			this.columnHeader6.Text = "Нач. пок.";
			this.columnHeader7.Text = "Кон. пок.";
			this.columnHeader13.Text = "Объем, м3";
			this.columnHeader5.Text = "Поверка";
			this.columnHeader5.Width = 77;
			this.columnHeader8.Text = "Статус ПУ";
			this.columnHeader8.Width = 71;
			this.columnHeader3.Text = "Исполнитель";
			this.columnHeader3.Width = 90;
			this.columnHeader4.Text = "Примечание";
			this.columnHeader4.Width = 118;
			this.columnHeader11.Text = "Причина";
			this.columnHeader11.Width = 74;
			this.tbData.Appearance = ToolBarAppearance.Flat;
			this.tbData.AutoSize = false;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.tbAdd, this.toolBarButton16, this.toolBarButton23, this.toolBarButton25, this.toolBarButton26, this.toolBarButton27, this.toolBarButton31, this.toolBarButton1, this.toolBarButton2, this.toolBarButton3, this.toolBarButton4, this.toolBarButton5, this.toolBarButton6 };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.DropDownArrows = true;
			this.tbData.ImageList = this.imageList1;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(928, 28);
			this.tbData.TabIndex = 1;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.tbAdd.ImageIndex = 0;
			this.tbAdd.Tag = "Add";
			this.tbAdd.ToolTipText = "Добавить";
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
			this.toolBarButton27.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton31.ImageIndex = 4;
			this.toolBarButton31.Tag = "Depth";
			this.toolBarButton31.ToolTipText = "Интервал журнала";
			this.toolBarButton1.ImageIndex = 8;
			this.toolBarButton1.Tag = "Filter";
			this.toolBarButton1.ToolTipText = "Фильтр";
			this.toolBarButton2.ImageIndex = 10;
			this.toolBarButton2.Tag = "Refresh";
			this.toolBarButton2.ToolTipText = "Обновить";
			this.toolBarButton3.ImageIndex = 11;
			this.toolBarButton3.Tag = "PrintNakl";
			this.toolBarButton3.ToolTipText = "Печать накладной";
			this.toolBarButton3.Visible = false;
			this.toolBarButton4.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton5.Style = ToolBarButtonStyle.Separator;
			this.toolBarButton6.ImageIndex = 14;
			this.toolBarButton6.Tag = "createCorrec";
			this.toolBarButton6.ToolTipText = "Корректировка начисления";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuAdd, this.menuEdit, this.menuDel, this.menuItem4, this.menuPrint };
			menuItems.AddRange(menuItemArray);
			this.menuAdd.Index = 0;
			this.menuAdd.Text = "Добавить";
			this.menuAdd.Click += new EventHandler(this.menuAdd_Click);
			this.menuEdit.Index = 1;
			this.menuEdit.Text = "Изменить";
			this.menuEdit.Click += new EventHandler(this.menuEdit_Click);
			this.menuDel.Index = 2;
			this.menuDel.Text = "Удалить";
			this.menuDel.Click += new EventHandler(this.menuDel_Click);
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			this.menuPrint.Index = 4;
			this.menuPrint.Text = "Печать";
			this.menuPrint.Click += new EventHandler(this.menuPrint_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(928, 446);
			base.Controls.Add(this.lv);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimumSize = new System.Drawing.Size(800, 460);
			base.Name = "frmDocVerifys";
			this.Text = "Журнал поверки приборов учета";
			base.Closing += new CancelEventHandler(this.frmDocVerifys_Closing);
			base.Load += new EventHandler(this.frmDocVerifys_Load);
			base.KeyUp += new KeyEventHandler(this.frmDocVerifys_KeyUp);
			base.ResumeLayout(false);
		}

		private void lv_DoubleClick(object sender, EventArgs e)
		{
			this.EditDoc();
		}

		private void lv_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.EditDoc();
			}
		}

		private void menuAdd_Click(object sender, EventArgs e)
		{
			this.AddDoc();
		}

		private void menuDel_Click(object sender, EventArgs e)
		{
			this.DeleteDoc();
		}

		private void menuEdit_Click(object sender, EventArgs e)
		{
			this.EditDoc();
		}

		private void menuPrint_Click(object sender, EventArgs e)
		{
		}

		private void RefreshCaption()
		{
			string[] shortDateString = new string[] { "Журнал поверки приборов учета (интервал с ", this.DateBegin.ToShortDateString(), " по ", this.DateEnd.ToShortDateString(), ") фильтр: ", this.filter };
			this.Text = string.Concat(shortDateString);
		}

		private void tbData_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			"PrintNakl";
			string str = e.Button.Tag.ToString();
			string str1 = str;
			if (str != null)
			{
				str1 = string.IsInterned(str1);
				if ((object)str1 == (object)"Add")
				{
					this.AddDoc();
					return;
				}
				if ((object)str1 == (object)"Edit")
				{
					this.EditDoc();
					return;
				}
				if ((object)str1 == (object)"Del")
				{
					this.DeleteDoc();
					return;
				}
				if ((object)str1 == (object)"createCorrec")
				{
					this.CreateCorrec();
					return;
				}
				if ((object)str1 == (object)"Excel")
				{
					Tools.ConvertToExcel(this.lv);
					return;
				}
				if ((object)str1 == (object)"Depth")
				{
					frmDepthView _frmDepthView = new frmDepthView();
					_frmDepthView.SetDate(this.DateBegin, this.DateEnd);
					if (_frmDepthView.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						_frmDepthView.GetDate(ref this.DateBegin, ref this.DateEnd);
						this._docs = null;
						this.FillList();
					}
					_frmDepthView = null;
					return;
				}
				if ((object)str1 == (object)"Filter")
				{
					frmFilterDocVerifys frmFilterDocVerify = new frmFilterDocVerifys();
					if (frmFilterDocVerify.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						frmFilterDocVerify.GetData(ref this.status, ref this.idagent, ref this.filter);
						this.FillList();
					}
					frmFilterDocVerify = null;
					return;
				}
				if ((object)str1 == (object)"Refresh")
				{
					this.FillList();
				}
				else if ((object)str1 != (object)"PrintNakl")
				{
					return;
				}
			}
		}
	}
}