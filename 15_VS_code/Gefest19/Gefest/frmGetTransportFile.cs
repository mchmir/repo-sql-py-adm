using C1.Win.C1List;
using C1.Win.C1List.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using WebSecurityDLL;

namespace Gefest
{
	public class frmGetTransportFile : Form
	{
		private Label label2;

		private TextBox txtNumber;

		private Label label1;

		private Button cmdApply;

		private Button cmdOpen;

		private StatusBar stBar1;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private StatusBarPanel statusBarPanel3;

		private ListView lv;

		private ColumnHeader columnHeader0;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private C1Combo cmbAgent;

		private System.ComponentModel.Container components = null;

		private string filepath;

		private string filename;

		private AutoBatch _autobatch;

		private Batch _batch;

		private AutoDocuments _autodocs;

		private Agents _agents;

		private Agent _agent;

		private double summaFF;

		private int countFF;

		private bool DoCheck = true;

		public frmGetTransportFile(Agent oAgent, string number)
		{
			this.InitializeComponent();
			if (number != null)
			{
				this.txtNumber.Text = number;
			}
			this._agent = oAgent;
		}

		private void cmdApply_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (this.cmbAgent.SelectedIndex == -1 || this.txtNumber.Text.Length == 0)
				{
					this.Cursor = Cursors.Default;
					MessageBox.Show("Выберите агента, укажите номер пачки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					this._batch = new Batch();
					if (this._autobatch.oBatch != null)
					{
						this._batch.Load(this._autobatch.oBatch.get_ID());
					}
					if (this._batch.oStatusBatch != null)
					{
						if (this._batch.oStatusBatch.get_ID() == (long)2)
						{
							MessageBox.Show("Пачка уже закрыта!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							return;
						}
						else if (this._batch.BatchDate.Year != DateTime.Now.Year)
						{
							MessageBox.Show("Пачка за прошлый год!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							return;
						}
					}
					this._batch.oTypePay = Depot.oTypePays.item((long)2);
					this._batch.oPeriod = Depot.CurrentPeriod;
					this._batch.oDispatcher = this._agents[this.cmbAgent.SelectedIndex];
					this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)1);
					this._batch.set_Name(this.filename);
					this._batch.BatchCount = this.countFF;
					this._batch.BatchAmount = this.summaFF;
					this._batch.oTypeBatch = Depot.oTypeBatchs.item((long)1);
					if (this.lv.Items.Count > 0)
					{
						this._batch.BatchDate = Convert.ToDateTime(this.lv.Items[0].SubItems[4].Text);
					}
					this._batch.NumberBatch = this.txtNumber.Text;
					this._batch.Save();
					this._autobatch.oBatch = this._batch;
					this._autobatch.Save();
					int num = 0;
					int num1 = 0;
					double documentAmount = 0;
					foreach (ListViewItem item in this.lv.Items)
					{
						if (item.Checked)
						{
							continue;
						}
						Contracts contract = new Contracts();
						contract.Load(item.SubItems[6].Text);
						Document document = new Document();
						if (this._autodocs[item.Index].oDocument != null)
						{
							document.Load(this._autodocs[item.Index].oDocument.get_ID());
						}
						document.oContract = contract[0];
						document.oPeriod = Depot.CurrentPeriod;
						document.oBatch = this._batch;
						document.oTypeDocument = Depot.oTypeDocuments.item((long)1);
						document.DocumentNumber = item.SubItems[1].Text;
						document.DocumentDate = Convert.ToDateTime(item.SubItems[4].Text);
						document.DocumentAmount = Convert.ToDouble(item.SubItems[5].Text);
						if (document.Save() != 0)
						{
							item.BackColor = Color.Red;
							num++;
						}
						else
						{
							double num2 = 0;
							long num3 = (long)0;
							long d = document.get_ID();
							string documentNumber = document.DocumentNumber;
							string str = "";
							bool flag = false;
							double num4 = 0;
							if (!document.DocumentPay(document.oContract.oPerson.isJuridical, Depot.CurrentPeriod.get_ID(), document.oBatch.get_ID(), document.oContract.get_ID(), (long)1, document.DocumentDate, document.DocumentAmount, 0, false, SQLConnect.CurrentUser.get_ID(), ref d, ref num3, ref num2, ref documentNumber, ref str, ref flag, ref num4))
							{
								item.BackColor = Color.Red;
								num++;
							}
							else
							{
								document.set_ID(d);
								this.DoCheck = false;
								item.Checked = true;
								this.DoCheck = true;
								item.EnsureVisible();
								this.Refresh();
								this._autodocs[item.Index].IDStatusAutoDocument = 1;
								item.BackColor = SystemColors.Window;
								num1++;
								documentAmount += document.DocumentAmount;
							}
							this._autodocs[item.Index].oDocument = document;
							this._autodocs[item.Index].Save();
						}
					}
					this.cmdApply.Enabled = false;
					this.Cursor = Cursors.Default;
					string[] strArrays = new string[] { "Успешно проведено ", num1.ToString(), " документов на сумму ", documentAmount.ToString(), ", ", num.ToString(), " платежей не проведено!" };
					MessageBox.Show(string.Concat(strArrays), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					if (num != 0)
					{
						MessageBox.Show("Пачка не закрыта, проверьте ошибки!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						this._batch.oStatusBatch = Depot.oStatusBatchs.item((long)2);
						this._batch.Save();
					}
				}
			}
			catch
			{
				this.Cursor = Cursors.Default;
				MessageBox.Show("Ошибка проведения!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void cmdOpen_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "АТФ Банк (*.txt)|*.txt|Текстовый файл(*.txt)|*.txt|БанкТуранАлем (bta*.gaz)|bta*.gaz|Казпочта (ip2*.*)|ip2*.*|Народный Банкомат (*.HBC)|*.HBC|Народный РКО (ip2o006*.*)|ip2o006*.*|Север телеком (*.txt)|*.txt|Регион север (*.txt)|*.txt|Темирбанк (tr3o0002.*)|tr3o0002.*|Цеснабанк (gaz*.*)|gaz*.*|КазКом (*.xml)|*.xml|QIWI-Казахстан (*.csv)|*.csv|Все типы файлов (*.*)|*.*"
			};
			if (this._agent != null)
			{
				openFileDialog.FilterIndex = this.cmbAgent.SelectedIndex + 1;
			}
			else
			{
				openFileDialog.FilterIndex = 3;
			}
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.filepath = openFileDialog.FileName;
				if (openFileDialog.FilterIndex > 0 && openFileDialog.FilterIndex < 8)
				{
					this.cmbAgent.SelectedIndex = openFileDialog.FilterIndex - 1;
				}
				string str = this.filepath;
				char[] chrArray = new char[] { char.Parse("\\") };
				string[] strArrays = str.Split(chrArray);
				this.filename = strArrays[(int)strArrays.Length - 1];
				this.Text = string.Concat("Прием оплаты из транспортного файла: ", this.filepath);
				base.Invalidate();
				this.FillList(this.filepath, openFileDialog.FilterIndex);
				if (this.lv.Items.Count <= 0)
				{
					this.cmdApply.Enabled = false;
					this.Cursor = Cursors.Default;
					MessageBox.Show("Неверный формат файла!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					this.cmdApply.Enabled = true;
				}
			}
			this.Cursor = Cursors.Default;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void FillList(string path, int _file)
		{
			double num;
			ListViewItem listViewItem;
			Encoding encoding;
			StreamReader streamReader;
			string str;
			string[] strArrays;
			char[] chrArray;
			DateTime dateTime;
			byte[] numArray;
			try
			{
				num = 0;
				this.lv.Items.Clear();
				listViewItem = new ListViewItem();
				string lower = this.filepath.Substring(this.filepath.Length - 4).ToLower();
				if (this.filepath.IndexOf("Kaspi") > -1)
				{
					lower = ".ksp";
				}
				".csv";
				string str1 = lower;
				string str2 = str1;
				if (str1 != null)
				{
					str2 = string.IsInterned(str2);
					if ((object)str2 == (object)".hbc")
					{
						encoding = Encoding.GetEncoding(866);
						streamReader = new StreamReader(path, encoding);
						this.countFF = 0;
						while (streamReader.Peek() != -1)
						{
							str = streamReader.ReadLine();
							chrArray = new char[] { ' ' };
							strArrays = str.Split(chrArray);
							listViewItem = new ListViewItem("")
							{
								Checked = false
							};
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add(strArrays[0]);
							listViewItem.SubItems.Add("");
							ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
							dateTime = Convert.ToDateTime(strArrays[2]);
							subItems.Add(dateTime.ToShortDateString());
							listViewItem.SubItems.Add(strArrays[5].Replace(".", ","));
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add("");
							this.lv.Items.Add(listViewItem);
							listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
							num += Convert.ToDouble(strArrays[5].Replace(".", ","));
							this.countFF++;
						}
						streamReader.Close();
						this.summaFF = num;
					}
					else if ((object)str2 == (object)".xml")
					{
						string str3 = path;
						string value = "";
						string value1 = "";
						string value2 = "";
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(str3);
						foreach (XmlNode elementsByTagName in xmlDocument.GetElementsByTagName("unikom-report"))
						{
							value2 = elementsByTagName.Attributes.Item(1).Value;
						}
						XmlNodeList childNodes = xmlDocument.DocumentElement.ChildNodes;
						this.countFF = 0;
						foreach (XmlNode childNode in childNodes)
						{
							foreach (XmlNode xmlNodes in childNode.ChildNodes)
							{
								string name = xmlNodes.Name;
								if (xmlNodes.Name != "document")
								{
									continue;
								}
								foreach (XmlNode childNode1 in xmlNodes.ChildNodes)
								{
									if (childNode1.Attributes.Item(0).Value == "clientId")
									{
										value = childNode1.Attributes.Item(2).Value;
									}
									if (childNode1.Attributes.Item(0).Value != "amount")
									{
										continue;
									}
									value1 = childNode1.Attributes.Item(2).Value;
								}
								listViewItem = new ListViewItem("")
								{
									Checked = false
								};
								listViewItem.SubItems.Add("");
								listViewItem.SubItems.Add(value);
								listViewItem.SubItems.Add("");
								ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem.SubItems;
								dateTime = Convert.ToDateTime(value2);
								listViewSubItemCollections.Add(dateTime.ToShortDateString());
								listViewItem.SubItems.Add(value1.Replace(".", ","));
								listViewItem.SubItems.Add("");
								listViewItem.SubItems.Add("");
								this.lv.Items.Add(listViewItem);
								listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
								num += Convert.ToDouble(value1.Replace(".", ","));
								this.countFF++;
							}
						}
						this.summaFF = num;
					}
					else if ((object)str2 == (object)".txt")
					{
						encoding = Encoding.GetEncoding(866);
						streamReader = new StreamReader(path, encoding);
						this.countFF = 0;
						while (streamReader.Peek() != -1)
						{
							str = streamReader.ReadLine();
							numArray = new byte[] { 254 };
							strArrays = str.Split(encoding.GetChars(numArray));
							if ((int)strArrays.Length == 1)
							{
								numArray = new byte[] { 238 };
								strArrays = str.Split(encoding.GetChars(numArray));
							}
							if ((int)strArrays.Length == 1)
							{
								numArray = new byte[] { 63 };
								strArrays = str.Split(encoding.GetChars(numArray));
							}
							if ((int)strArrays.Length > 1 && strArrays[0].Trim() == "3")
							{
								this.summaFF = Convert.ToDouble(strArrays[1].Replace(".", ","));
							}
							if ((int)strArrays.Length > 1 && strArrays[0].Trim() == "1")
							{
								frmGetTransportFile _frmGetTransportFile = this;
								_frmGetTransportFile.Text = string.Concat(_frmGetTransportFile.Text, " ", strArrays[1].ToString());
							}
							if ((int)strArrays.Length < 5 || !(strArrays[0].Trim() == "2"))
							{
								continue;
							}
							listViewItem = new ListViewItem("")
							{
								Checked = false
							};
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add(strArrays[1].Trim());
							listViewItem.SubItems.Add("");
							ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
							dateTime = Convert.ToDateTime(strArrays[3]);
							subItems1.Add(dateTime.ToShortDateString());
							listViewItem.SubItems.Add(strArrays[2].Trim().Replace(".", ","));
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add("");
							this.lv.Items.Add(listViewItem);
							listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
							num += Convert.ToDouble(strArrays[2].Replace(".", ","));
							this.countFF++;
						}
						streamReader.Close();
					}
					else if ((object)str2 == (object)".ksp")
					{
						encoding = Encoding.GetEncoding("UTF-8");
						streamReader = new StreamReader(path, encoding);
						this.countFF = 0;
						while (streamReader.Peek() != -1)
						{
							str = streamReader.ReadLine();
							chrArray = new char[] { '\u25A0' };
							strArrays = str.Split(chrArray);
							if ((int)strArrays.Length > 1 && strArrays[0].Trim() == "3")
							{
								this.summaFF = Convert.ToDouble(strArrays[1].Replace(".", ","));
							}
							if ((int)strArrays.Length > 1 && strArrays[0].Trim() == "1")
							{
								frmGetTransportFile _frmGetTransportFile1 = this;
								_frmGetTransportFile1.Text = string.Concat(_frmGetTransportFile1.Text, " ", strArrays[1].ToString());
							}
							if ((int)strArrays.Length < 5 || !(strArrays[0].Trim() == "2"))
							{
								continue;
							}
							listViewItem = new ListViewItem("")
							{
								Checked = false
							};
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add(strArrays[1].Trim());
							listViewItem.SubItems.Add("");
							ListViewItem.ListViewSubItemCollection listViewSubItemCollections1 = listViewItem.SubItems;
							dateTime = Convert.ToDateTime(strArrays[3]);
							listViewSubItemCollections1.Add(dateTime.ToShortDateString());
							listViewItem.SubItems.Add(strArrays[2].Trim().Replace(".", ","));
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add("");
							this.lv.Items.Add(listViewItem);
							listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
							num += Convert.ToDouble(strArrays[2].Replace(".", ","));
							this.countFF++;
						}
						streamReader.Close();
					}
					else
					{
						if ((object)str2 != (object)".csv")
						{
							goto Label0;
						}
						encoding = Encoding.GetEncoding(866);
						streamReader = new StreamReader(path, encoding);
						this.countFF = 0;
						while (streamReader.Peek() != -1)
						{
							str = streamReader.ReadLine();
							chrArray = new char[] { ';' };
							strArrays = str.Split(chrArray);
							listViewItem = new ListViewItem("")
							{
								Checked = false
							};
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add(strArrays[2].Trim());
							listViewItem.SubItems.Add("");
							ListViewItem.ListViewSubItemCollection subItems2 = listViewItem.SubItems;
							dateTime = Convert.ToDateTime(strArrays[1]);
							subItems2.Add(dateTime.ToShortDateString());
							listViewItem.SubItems.Add(strArrays[3].Trim().Replace(".", ","));
							listViewItem.SubItems.Add("");
							listViewItem.SubItems.Add("");
							this.lv.Items.Add(listViewItem);
							listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
							num += Convert.ToDouble(strArrays[3].Replace(".", ","));
							this.countFF++;
						}
						streamReader.Close();
					}
				}
				else
				{
					goto Label0;
				}
			Label2:
				streamReader = null;
				listViewItem = null;
				StatusBarPanel item = this.stBar1.Panels[0];
				int count = this.lv.Items.Count;
				item.Text = string.Concat("Всего платежей ", count.ToString());
				this.stBar1.Panels[1].Text = string.Concat("Платежей на сумму ", num.ToString(), ", сумма из ТФ ", this.summaFF.ToString());
				this.stBar1.Panels[2].Text = "Ошибок 0";
				this.SverkaLS();
			}
			catch
			{
				MessageBox.Show("Не верный формат файла!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			return;
		Label0:
			encoding = Encoding.GetEncoding(866);
			streamReader = new StreamReader(path, encoding);
			this.countFF = 0;
			while (streamReader.Peek() != -1)
			{
				str = streamReader.ReadLine();
				numArray = new byte[] { 254 };
				strArrays = str.Split(encoding.GetChars(numArray));
				if ((int)strArrays.Length > 1 && strArrays[0].Trim() == "3")
				{
					this.summaFF = Convert.ToDouble(strArrays[1].Replace(".", ","));
				}
				if ((int)strArrays.Length > 1 && strArrays[0].Trim() == "1")
				{
					frmGetTransportFile _frmGetTransportFile2 = this;
					_frmGetTransportFile2.Text = string.Concat(_frmGetTransportFile2.Text, " ", strArrays[1].ToString());
				}
				if ((int)strArrays.Length < 11 || !(strArrays[0].Trim() == "2"))
				{
					continue;
				}
				listViewItem = new ListViewItem("")
				{
					Checked = false
				};
				listViewItem.SubItems.Add("");
				listViewItem.SubItems.Add(strArrays[1].Trim());
				listViewItem.SubItems.Add(strArrays[8].Trim());
				ListViewItem.ListViewSubItemCollection listViewSubItemCollections2 = listViewItem.SubItems;
				dateTime = Convert.ToDateTime(strArrays[3]);
				listViewSubItemCollections2.Add(dateTime.ToShortDateString());
				listViewItem.SubItems.Add(strArrays[2].Trim().Replace(".", ","));
				listViewItem.SubItems.Add("");
				listViewItem.SubItems.Add("");
				this.lv.Items.Add(listViewItem);
				listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
				num += Convert.ToDouble(strArrays[2].Replace(".", ","));
				this.countFF++;
			}
			streamReader.Close();
			goto Label2;
		}

		private void frmGetTransportFile_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmGetTransportFile_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
				this._agents = new Agents();
				this._agents.Load(Depot.oTypeAgents.item((long)4));
				if (this._agent != null)
				{
					Tools.FillC1(this._agents, this.cmbAgent, this._agent.get_ID());
				}
				else
				{
					Tools.FillC1(this._agents, this.cmbAgent, (long)0);
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			ResourceManager resourceManager = new ResourceManager(typeof(frmGetTransportFile));
			this.label2 = new Label();
			this.txtNumber = new TextBox();
			this.label1 = new Label();
			this.cmdApply = new Button();
			this.cmdOpen = new Button();
			this.stBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.statusBarPanel3 = new StatusBarPanel();
			this.lv = new ListView();
			this.columnHeader0 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.cmbAgent = new C1Combo();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			((ISupportInitialize)this.statusBarPanel3).BeginInit();
			((ISupportInitialize)this.cmbAgent).BeginInit();
			base.SuspendLayout();
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label2.Location = new Point(552, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 16;
			this.label2.Text = "из пачки номер:";
			this.txtNumber.BorderStyle = BorderStyle.FixedSingle;
			this.txtNumber.Location = new Point(648, 8);
			this.txtNumber.MaxLength = 20;
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(96, 20);
			this.txtNumber.TabIndex = 3;
			this.txtNumber.Text = "";
			this.txtNumber.Enter += new EventHandler(this.txtNumber_Enter);
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.label1.Location = new Point(208, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 16);
			this.label1.TabIndex = 14;
			this.label1.Text = "Агент по приему оплат:";
			this.cmdApply.Enabled = false;
			this.cmdApply.FlatStyle = FlatStyle.Flat;
			this.cmdApply.Location = new Point(104, 8);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(88, 24);
			this.cmdApply.TabIndex = 5;
			this.cmdApply.Text = "Провести";
			this.cmdApply.Click += new EventHandler(this.cmdApply_Click);
			this.cmdOpen.FlatStyle = FlatStyle.Flat;
			this.cmdOpen.Location = new Point(8, 8);
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Size = new System.Drawing.Size(88, 24);
			this.cmdOpen.TabIndex = 1;
			this.cmdOpen.Text = "Открыть ТФ";
			this.cmdOpen.Click += new EventHandler(this.cmdOpen_Click);
			this.stBar1.Location = new Point(0, 351);
			this.stBar1.Name = "stBar1";
			StatusBar.StatusBarPanelCollection panels = this.stBar1.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.statusBarPanel1, this.statusBarPanel2, this.statusBarPanel3 };
			panels.AddRange(statusBarPanelArray);
			this.stBar1.ShowPanels = true;
			this.stBar1.Size = new System.Drawing.Size(754, 24);
			this.stBar1.TabIndex = 10;
			this.stBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Всего платежей";
			this.statusBarPanel1.Width = 99;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Text = "Платежей на сумму";
			this.statusBarPanel2.Width = 119;
			this.statusBarPanel3.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel3.Text = "Ошибок";
			this.statusBarPanel3.Width = 520;
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			this.lv.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader0, this.columnHeader7, this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 44);
			this.lv.MultiSelect = false;
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(752, 304);
			this.lv.TabIndex = 4;
			this.lv.View = View.Details;
			this.lv.ItemCheck += new ItemCheckEventHandler(this.lv_ItemCheck);
			this.columnHeader0.Text = "ОК";
			this.columnHeader0.Width = 27;
			this.columnHeader7.Text = "№";
			this.columnHeader7.Width = 30;
			this.columnHeader1.Text = "Л/счет из ТФ";
			this.columnHeader1.Width = 100;
			this.columnHeader2.Text = "ФИО из ТФ";
			this.columnHeader2.Width = 160;
			this.columnHeader3.Text = "Дата платежа";
			this.columnHeader3.Width = 85;
			this.columnHeader4.Text = "Сумма ";
			this.columnHeader4.Width = 85;
			this.columnHeader5.Text = "Л/счет из БД";
			this.columnHeader5.Width = 100;
			this.columnHeader6.Text = "ФИО из БД";
			this.columnHeader6.Width = 160;
			this.cmbAgent.AddItemSeparator = ';';
			this.cmbAgent.BorderStyle = 1;
			this.cmbAgent.Caption = "";
			this.cmbAgent.CaptionHeight = 17;
			this.cmbAgent.CharacterCasing = 0;
			this.cmbAgent.ColumnCaptionHeight = 17;
			this.cmbAgent.ColumnFooterHeight = 17;
			this.cmbAgent.ColumnHeaders = false;
			this.cmbAgent.ColumnWidth = 100;
			this.cmbAgent.ComboStyle = ComboStyleEnum.DropdownList;
			this.cmbAgent.ContentHeight = 15;
			this.cmbAgent.DataMode = DataModeEnum.AddItem;
			this.cmbAgent.DeadAreaBackColor = Color.Empty;
			this.cmbAgent.EditorBackColor = SystemColors.Window;
			this.cmbAgent.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.cmbAgent.EditorForeColor = SystemColors.WindowText;
			this.cmbAgent.EditorHeight = 15;
			this.cmbAgent.FlatStyle = FlatModeEnum.Flat;
			this.cmbAgent.Images.Add((Image)resourceManager.GetObject("resource"));
			this.cmbAgent.ItemHeight = 15;
			this.cmbAgent.Location = new Point(344, 8);
			this.cmbAgent.MatchEntryTimeout = (long)2000;
			this.cmbAgent.MaxDropDownItems = 5;
			this.cmbAgent.MaxLength = 32767;
			this.cmbAgent.MouseCursor = Cursors.Default;
			this.cmbAgent.Name = "cmbAgent";
			this.cmbAgent.RowDivider.Color = Color.DarkGray;
			this.cmbAgent.RowDivider.Style = LineStyleEnum.None;
			this.cmbAgent.RowSubDividerColor = Color.DarkGray;
			this.cmbAgent.Size = new System.Drawing.Size(192, 19);
			this.cmbAgent.TabIndex = 2;
			this.cmbAgent.PropBag = "<?xml version=\"1.0\"?><Blob><DataCols><C1DataColumn Level=\"0\" Caption=\"Column New\" DataField=\"\"><ValueItems /></C1DataColumn></DataCols><Styles type=\"C1.Win.C1List.Design.ContextWrapper\"><Data>Group{BackColor:ControlDark;Border:None,,0, 0, 0, 0;AlignVert:Center;}Style12{AlignHorz:Near;}Style2{}Style5{}Style4{}Style7{}Style6{}EvenRow{BackColor:Aqua;}Selected{ForeColor:HighlightText;BackColor:Highlight;}Style3{}Inactive{ForeColor:InactiveCaptionText;BackColor:InactiveCaption;}Footer{}Caption{AlignHorz:Center;}Normal{Font:Tahoma, 11world;}HighlightRow{ForeColor:HighlightText;BackColor:Highlight;}Style9{AlignHorz:Near;}OddRow{}RecordSelector{AlignImage:Center;}Heading{Wrap:True;AlignVert:Center;Border:Flat,ControlDark,0, 1, 0, 1;ForeColor:ControlText;BackColor:Control;}Style8{}Style10{}Style11{}Style14{}Style13{AlignHorz:Near;}Style1{}</Data></Styles><Splits><C1.Win.C1List.ListBoxView AllowColSelect=\"False\" Name=\"Split[0,0]\" CaptionHeight=\"17\" ColumnCaptionHeight=\"17\" ColumnFooterHeight=\"17\" VerticalScrollGroup=\"1\" HorizontalScrollGroup=\"1\"><ClientRect>0, 0, 116, 156</ClientRect><internalCols><C1DisplayColumn><HeadingStyle parent=\"Style2\" me=\"Style12\" /><Style parent=\"Style1\" me=\"Style13\" /><FooterStyle parent=\"Style3\" me=\"Style14\" /><ColumnDivider><Color>DarkGray</Color><Style>Single</Style></ColumnDivider><Height>15</Height><DCIdx>0</DCIdx></C1DisplayColumn></internalCols><Height>156</Height><VScrollBar><Width>16</Width></VScrollBar><HScrollBar><Height>16</Height></HScrollBar><CaptionStyle parent=\"Style2\" me=\"Style9\" /><EvenRowStyle parent=\"EvenRow\" me=\"Style7\" /><FooterStyle parent=\"Footer\" me=\"Style3\" /><GroupStyle parent=\"Group\" me=\"Style11\" /><HeadingStyle parent=\"Heading\" me=\"Style2\" /><HighLightRowStyle parent=\"HighlightRow\" me=\"Style6\" /><InactiveStyle parent=\"Inactive\" me=\"Style4\" /><OddRowStyle parent=\"OddRow\" me=\"Style8\" /><RecordSelectorStyle parent=\"RecordSelector\" me=\"Style10\" /><SelectedStyle parent=\"Selected\" me=\"Style5\" /><Style parent=\"Normal\" me=\"Style1\" /></C1.Win.C1List.ListBoxView></Splits><NamedStyles><Style parent=\"\" me=\"Normal\" /><Style parent=\"Normal\" me=\"Heading\" /><Style parent=\"Heading\" me=\"Footer\" /><Style parent=\"Heading\" me=\"Caption\" /><Style parent=\"Heading\" me=\"Inactive\" /><Style parent=\"Normal\" me=\"Selected\" /><Style parent=\"Normal\" me=\"HighlightRow\" /><Style parent=\"Normal\" me=\"EvenRow\" /><Style parent=\"Normal\" me=\"OddRow\" /><Style parent=\"Heading\" me=\"RecordSelector\" /><Style parent=\"Caption\" me=\"Group\" /></NamedStyles><vertSplits>1</vertSplits><horzSplits>1</horzSplits><Layout>Modified</Layout><DefaultRecSelWidth>16</DefaultRecSelWidth></Blob>";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(754, 375);
			base.Controls.Add(this.cmbAgent);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtNumber);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmdApply);
			base.Controls.Add(this.cmdOpen);
			base.Controls.Add(this.stBar1);
			base.Controls.Add(this.lv);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MinimumSize = new System.Drawing.Size(760, 400);
			base.Name = "frmGetTransportFile";
			this.Text = "Прием оплаты из транспортного файла";
			base.Closing += new CancelEventHandler(this.frmGetTransportFile_Closing);
			base.Load += new EventHandler(this.frmGetTransportFile_Load);
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			((ISupportInitialize)this.statusBarPanel3).EndInit();
			((ISupportInitialize)this.cmbAgent).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (this.DoCheck)
			{
				e.NewValue = e.CurrentValue;
			}
		}

		public static void ReplaceInFile(string filePath, string searchText, string replaceText)
		{
			try
			{
				StreamReader streamReader = new StreamReader(filePath);
				string end = streamReader.ReadToEnd();
				streamReader.Close();
				end = Regex.Replace(end, searchText, replaceText);
				StreamWriter streamWriter = new StreamWriter(filePath);
				streamWriter.Write(end);
				streamWriter.Close();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void SverkaLS()
		{
			AutoDocument text;
			try
			{
				AutoBatchs autoBatch = new AutoBatchs();
				DateTime dateTime = Convert.ToDateTime(this.lv.Items[0].SubItems[4].Text);
				int year = dateTime.Year;
				autoBatch.Load(this.filename, year);
				if (autoBatch.get_Count() <= 0)
				{
					this._autobatch = new AutoBatch();
				}
				else
				{
					this._autobatch = autoBatch[0];
					if (this._autobatch.oBatch != null)
					{
						this.txtNumber.Text = this._autobatch.oBatch.NumberBatch;
					}
				}
				this._autobatch.Path = this.filename;
				this._autobatch.BatchAmount = this.summaFF;
				this._autobatch.BatchCount = this.countFF;
				this._autobatch.BatchDate = Convert.ToDateTime(this.lv.Items[0].SubItems[4].Text);
				this._autobatch.Save();
				int num = 0;
				int num1 = 0;
				this._autodocs = new AutoDocuments();
				foreach (ListViewItem item in this.lv.Items)
				{
					Contracts contract = new Contracts();
					contract.Load(item.SubItems[2].Text);
					if (contract[0] == null)
					{
						item.BackColor = Color.Red;
						item.SubItems[6].Text = "9999999";
						item.SubItems[7].Text = "Не известно";
						num++;
					}
					else
					{
						item.SubItems[6].Text = contract[0].Account;
						if (contract[0].oPerson != null)
						{
							item.SubItems[7].Text = contract[0].oPerson.FullName;
						}
						if (item.SubItems[3].Text.ToUpper() != item.SubItems[7].Text.ToUpper())
						{
							item.BackColor = SystemColors.Info;
							num1++;
						}
					}
					AutoDocuments autoDocument = new AutoDocuments();
					autoDocument.Load(this._autobatch, item.SubItems[1].Text);
					text = (autoDocument.get_Count() <= 0 ? new AutoDocument() : autoDocument[0]);
					if (text.IDStatusAutoDocument != 1)
					{
						text.IDStatusAutoDocument = 2;
					}
					else
					{
						this.DoCheck = false;
						item.Checked = true;
						this.DoCheck = true;
					}
					text.oAutoBatch = this._autobatch;
					text.Account = item.SubItems[6].Text;
					text.FIO = item.SubItems[7].Text;
					text.DocumentAmount = Convert.ToDouble(item.SubItems[5].Text);
					text.DocumentDate = Convert.ToDateTime(item.SubItems[4].Text);
					text.Number = item.SubItems[1].Text;
					if (text.Save() != 0)
					{
						continue;
					}
					this._autodocs.Add(text);
				}
				this.stBar1.Panels[2].Text = string.Concat("Ошибок ", num.ToString(), ", предупреждений ", num1.ToString());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка при сверке ЛС ", exception.Message), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void txtNumber_Enter(object sender, EventArgs e)
		{
			this.txtNumber.SelectAll();
		}
	}
}