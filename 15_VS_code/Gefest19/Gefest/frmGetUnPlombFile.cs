using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmGetUnPlombFile : Form
	{
		private Button cmdApply;

		private Button cmdOpen;

		private ListView lv;

		private ColumnHeader columnHeader0;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

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

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel3;

		private StatusBar stBar1;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ColumnHeader columnHeader10;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader12;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label label9;

		private Label label10;

		private bool DoCheck = true;

		public frmGetUnPlombFile()
		{
			this.InitializeComponent();
		}

		private void cmdApply_Click(object sender, EventArgs e)
		{
			try
			{
				int num = 0;
				this.Cursor = Cursors.WaitCursor;
				(new UslugiVDGOs()).Load();
				(new Accountings()).Load();
				int num1 = 0;
				int num2 = 0;
				foreach (ListViewItem item in this.lv.Items)
				{
					if (!item.Checked)
					{
						continue;
					}
					try
					{
						try
						{
							num = 0;
							this.Cursor = Cursors.WaitCursor;
							Contracts contract = new Contracts();
							contract.Load(item.SubItems[9].Text);
							if (contract[0] != null)
							{
								Contract item1 = contract[0];
								if (item.SubItems[11].Text != null)
								{
									Gmeter gmeter = new Gmeter();
									gmeter.Load((long)Convert.ToInt32(item.SubItems[12].Text));
									if (gmeter != null)
									{
										string str = "";
										if (item.SubItems[7].Text != "")
										{
											gmeter.PlombNumber1 = "";
											str = "пломба 1 снята";
										}
										if (item.SubItems[8].Text != "")
										{
											gmeter.PlombNumber2 = "";
											str = (str.Length <= 0 ? "пломба 2 снята" : "пломбы 1, 2 сняты");
										}
										gmeter.DatePlomb = Convert.ToDateTime(item.SubItems[5].Text);
										gmeter.IndicationPlomb = Convert.ToDouble(item.SubItems[6].Text);
										gmeter.IDAgentPlomb = 137;
										gmeter.IDTypePlombWork = 2;
										gmeter.PlombMemo = str;
										if (gmeter.Save() == 0)
										{
											goto Label0;
										}
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
						catch (Exception exception)
						{
							item.BackColor = Color.Red;
							num1++;
						}
					}
					finally
					{
						this.Cursor = Cursors.Default;
					}
				}
				this.cmdApply.Enabled = false;
				this.Cursor = Cursors.Default;
				string[] strArrays = new string[] { "Успешно сохранено ", num2.ToString(), " ПУ, ", num1.ToString(), " не сохранено!" };
				MessageBox.Show(string.Concat(strArrays), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
				Filter = "Реестр распломбировки (*.xlsx)|*распломбировки*.xlsx"
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.filepath = openFileDialog.FileName;
				string str = this.filepath;
				char[] chrArray = new char[] { char.Parse("\\") };
				string[] strArrays = str.Split(chrArray);
				this.filename = strArrays[(int)strArrays.Length - 1];
				this.Text = string.Concat("Прием реестра распломбировки ПУ: ", this.filepath);
				base.Invalidate();
				this.FillList(this.filepath);
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

		private void CompareGMeter()
		{
			if (this.lv.SelectedItems.Count > 0)
			{
				if (this.lv.SelectedItems[0].SubItems[9].Text.Trim() == "9999999")
				{
					return;
				}
				Gmeter gmeter = new Gmeter();
				frmGetGMeter _frmGetGMeter = new frmGetGMeter(this.lv.SelectedItems[0].SubItems[9].Text.Trim());
				if (_frmGetGMeter.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					_frmGetGMeter.GetGMeter(ref gmeter);
					if (gmeter != null)
					{
						this.lv.SelectedItems[0].SubItems[11].Text = string.Concat(gmeter.oTypeGMeter.Fullname, ", №", gmeter.SerialNumber);
						this.lv.SelectedItems[0].SubItems[12].Text = gmeter.get_ID().ToString();
						this.lv.SelectedItems[0].BackColor = SystemColors.Window;
						this.lv.SelectedItems[0].Checked = true;
					}
				}
				_frmGetGMeter = null;
				base.Focus();
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

		private void FillList(string path)
		{
			try
			{
				this.lv.Items.Clear();
				ListViewItem listViewItem = new ListViewItem();
				string str = "";
				int i = 7;
				Microsoft.Office.Interop.Excel.Application applicationClass = new ApplicationClass();
				Workbook workbook = applicationClass.get_Workbooks().Open(path, 0, true, 5, "", "", true, (XlPlatform)2, "\t", false, false, 0, true, 1, 0);
				Worksheet item = (Worksheet)workbook.get_Worksheets().get_Item(1);
				Range usedRange = item.get_UsedRange();
				int num = i;
				while ((usedRange.get_Cells().get__Default(num, 1) as Range).get_Value2() != null)
				{
					num++;
				}
				int num1 = num - i;
				num--;
				for (i = 7; i <= num; i++)
				{
					try
					{
						str = "";
						listViewItem = new ListViewItem("")
						{
							Checked = false
						};
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						try
						{
							listViewItem.SubItems[2].Text = (string)(usedRange.get_Cells().get__Default(i, 2) as Range).get_Value2();
						}
						catch
						{
							str = string.Concat(str, "нет л/с!");
						}
						try
						{
							listViewItem.SubItems[3].Text = (string)(usedRange.get_Cells().get__Default(i, 6) as Range).get_Value2();
						}
						catch
						{
							str = string.Concat(str, "нет фио!");
						}
						try
						{
							string str1 = string.Concat((usedRange.get_Cells().get__Default(i, 7) as Range).get_Value2().ToString(), ", №", (usedRange.get_Cells().get__Default(i, 8) as Range).get_Value2().ToString());
							listViewItem.SubItems[4].Text = str1.Trim();
						}
						catch
						{
							str = string.Concat(str, "нет ПУ!");
						}
						try
						{
							listViewItem.SubItems[5].Text = (string)(usedRange.get_Cells().get__Default(i, 9) as Range).get_Value2();
						}
						catch
						{
							str = string.Concat(str, "нет даты!");
						}
						try
						{
							string str2 = (usedRange.get_Cells().get__Default(i, 12) as Range).get_Value2().ToString();
							listViewItem.SubItems[6].Text = str2;
						}
						catch
						{
							str = string.Concat(str, "нет показаний!");
						}
						try
						{
							listViewItem.SubItems[7].Text = (string)(usedRange.get_Cells().get__Default(i, 10) as Range).get_Value2();
						}
						catch
						{
							str = string.Concat(str, "нет пломбы 1!");
						}
						try
						{
							listViewItem.SubItems[8].Text = (string)(usedRange.get_Cells().get__Default(i, 11) as Range).get_Value2();
						}
						catch
						{
							str = "нет пломбы2!";
						}
						this.lv.Items.Add(listViewItem);
						listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
						if (str.Length > 0)
						{
							listViewItem.SubItems[3].Text = string.Concat("ВНИМАНИЕ! ОШИБКА! ", str);
						}
					}
					catch
					{
						listViewItem.SubItems[3].Text = string.Concat("ВНИМАНИЕ! ОШИБКА! ", str);
						this.lv.Items.Add(listViewItem);
						listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
					}
				}
				this.stBar1.Panels[0].Text = string.Concat("Всего записей: ", Convert.ToString(num1));
				workbook.Close(true, null, null);
				applicationClass.Quit();
				this.SverkaLS();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Неверный формат файла!", exception.Message), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void frmGetUnPlombFile_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmGetUnPlombFile_Load(object sender, EventArgs e)
		{
			try
			{
				Tools.LoadWindows(this);
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.cmdApply = new Button();
			this.cmdOpen = new Button();
			this.lv = new ListView();
			this.columnHeader0 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel3 = new StatusBarPanel();
			this.stBar1 = new StatusBar();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.label7 = new Label();
			this.label8 = new Label();
			this.label9 = new Label();
			this.label10 = new Label();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel3).BeginInit();
			base.SuspendLayout();
			this.cmdApply.Enabled = false;
			this.cmdApply.FlatStyle = FlatStyle.Flat;
			this.cmdApply.Location = new Point(120, 8);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(104, 24);
			this.cmdApply.TabIndex = 5;
			this.cmdApply.Text = "Сохранить в БД";
			this.cmdApply.Click += new EventHandler(this.cmdApply_Click);
			this.cmdOpen.FlatStyle = FlatStyle.Flat;
			this.cmdOpen.Location = new Point(8, 8);
			this.cmdOpen.Name = "cmdOpen";
			this.cmdOpen.Size = new System.Drawing.Size(104, 24);
			this.cmdOpen.TabIndex = 1;
			this.cmdOpen.Text = "Открыть реестр";
			this.cmdOpen.Click += new EventHandler(this.cmdOpen_Click);
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			this.lv.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader0, this.columnHeader7, this.columnHeader1, this.columnHeader2, this.columnHeader10, this.columnHeader3, this.columnHeader4, this.columnHeader8, this.columnHeader9, this.columnHeader5, this.columnHeader6, this.columnHeader11, this.columnHeader12 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 44);
			this.lv.MultiSelect = false;
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(1048, 304);
			this.lv.TabIndex = 4;
			this.lv.View = View.Details;
			this.lv.MouseUp += new MouseEventHandler(this.lv_MouseUp);
			this.lv.ItemCheck += new ItemCheckEventHandler(this.lv_ItemCheck);
			this.columnHeader0.Text = "ОК";
			this.columnHeader0.Width = 27;
			this.columnHeader7.Text = "№";
			this.columnHeader7.Width = 30;
			this.columnHeader1.Text = "Л/счет из ТФ";
			this.columnHeader1.Width = 80;
			this.columnHeader2.Text = "ФИО из ТФ";
			this.columnHeader2.Width = 160;
			this.columnHeader10.Text = "ПУ из ТФ";
			this.columnHeader10.Width = 160;
			this.columnHeader3.Text = "Дата";
			this.columnHeader3.Width = 70;
			this.columnHeader4.Text = "Показания";
			this.columnHeader4.Width = 70;
			this.columnHeader8.Text = "Пломба №1";
			this.columnHeader8.Width = 70;
			this.columnHeader9.Text = "Пломба №2";
			this.columnHeader9.Width = 70;
			this.columnHeader5.Text = "Л/счет из БД";
			this.columnHeader5.Width = 80;
			this.columnHeader6.Text = "ФИО из БД";
			this.columnHeader6.Width = 160;
			this.columnHeader11.Text = "ПУ из БД";
			this.columnHeader11.Width = 160;
			this.columnHeader12.Text = "ИД ПУ";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Всего записей";
			this.statusBarPanel1.Width = 90;
			this.statusBarPanel3.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel3.Text = "Ошибок";
			this.statusBarPanel3.Width = 944;
			this.stBar1.Location = new Point(0, 351);
			this.stBar1.Name = "stBar1";
			StatusBar.StatusBarPanelCollection panels = this.stBar1.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.statusBarPanel1, this.statusBarPanel3 };
			panels.AddRange(statusBarPanelArray);
			this.stBar1.ShowPanels = true;
			this.stBar1.Size = new System.Drawing.Size(1050, 24);
			this.stBar1.TabIndex = 10;
			this.stBar1.Text = "statusBar1";
			this.label1.BackColor = Color.LightGreen;
			this.label1.Location = new Point(232, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 24);
			this.label1.TabIndex = 11;
			this.label2.Location = new Point(264, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 12;
			this.label2.Text = "- все хорошо;";
			this.label3.Location = new Point(376, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 14;
			this.label3.Text = "- ошибка;";
			this.label4.BackColor = Color.Red;
			this.label4.Location = new Point(344, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 24);
			this.label4.TabIndex = 13;
			this.label5.Location = new Point(472, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 16);
			this.label5.TabIndex = 16;
			this.label5.Text = "- на ПУ нет пломбы;";
			this.label6.BackColor = Color.Yellow;
			this.label6.Location = new Point(440, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(24, 24);
			this.label6.TabIndex = 15;
			this.label7.BackColor = Color.Orange;
			this.label7.Location = new Point(592, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 24);
			this.label7.TabIndex = 17;
			this.label8.Location = new Point(624, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(120, 16);
			this.label8.TabIndex = 18;
			this.label8.Text = "- пустая пломба;";
			this.label9.Location = new Point(768, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(184, 16);
			this.label9.TabIndex = 20;
			this.label9.Text = "- номера пломб не совпадают;";
			this.label10.BackColor = Color.SkyBlue;
			this.label10.Location = new Point(736, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(24, 24);
			this.label10.TabIndex = 19;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(1050, 375);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.label10);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmdApply);
			base.Controls.Add(this.cmdOpen);
			base.Controls.Add(this.stBar1);
			base.Controls.Add(this.lv);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MinimumSize = new System.Drawing.Size(1056, 407);
			base.Name = "frmGetUnPlombFile";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Прием реестра распломбировки ПУ";
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel3).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_ItemCheck(object sender, ItemCheckEventArgs e)
		{
		}

		private void lv_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				this.CompareGMeter();
			}
		}

		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void SverkaLS()
		{
			try
			{
				int num = 0;
				int num1 = 0;
				foreach (ListViewItem item in this.lv.Items)
				{
					Contracts contract = new Contracts();
					contract.Load(item.SubItems[2].Text);
					if (contract[0] == null)
					{
						item.BackColor = Color.Red;
						item.SubItems[9].Text = "9999999";
						item.SubItems[10].Text = "Не известно";
						num++;
					}
					else
					{
						Contract item1 = contract[0];
						item.SubItems[9].Text = item1.Account;
						if (item1.oPerson != null)
						{
							item.SubItems[10].Text = item1.oPerson.FullName;
						}
						if (item.SubItems[3].Text.ToUpper() == item.SubItems[10].Text.ToUpper())
						{
							item.Checked = true;
						}
						else
						{
							item.BackColor = SystemColors.Info;
							num1++;
						}
						if (item1.oActiveGobject == null)
						{
							num++;
							item.Checked = false;
						}
						else
						{
							Gmeter activeGMeter = item1.oActiveGobject.GetActiveGMeter();
							if (activeGMeter == null)
							{
								Gmeters gmeter = item1.oActiveGobject.oGmeters;
								if (gmeter != null)
								{
									IEnumerator enumerator = gmeter.GetEnumerator();
									try
									{
										if (enumerator.MoveNext())
										{
											activeGMeter = (Gmeter)enumerator.Current;
											item.SubItems[11].Text = string.Concat(activeGMeter.oTypeGMeter.Fullname, ", №", activeGMeter.SerialNumber, " (отключен)");
											item.SubItems[12].Text = activeGMeter.get_ID().ToString();
											if (activeGMeter.PlombNumber1.Length == 0 & item.SubItems[7].Text.Length > 0)
											{
												item.BackColor = Color.Yellow;
												num++;
												item.Checked = false;
											}
											if ((activeGMeter.PlombNumber1 != item.SubItems[7].Text) & (item.BackColor != Color.Yellow) & item.SubItems[7].Text.Length > 0)
											{
												item.BackColor = Color.SkyBlue;
												num++;
												item.Checked = false;
											}
											if (activeGMeter.PlombNumber2.Length == 0 & item.SubItems[8].Text.Length > 0)
											{
												item.BackColor = Color.Yellow;
												num++;
												item.Checked = false;
											}
											if ((activeGMeter.PlombNumber2 != item.SubItems[8].Text) & (item.BackColor != Color.Yellow) & item.SubItems[8].Text.Length > 0)
											{
												item.BackColor = Color.SkyBlue;
												num++;
												item.Checked = false;
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
							else
							{
								item.SubItems[11].Text = string.Concat(activeGMeter.oTypeGMeter.Fullname, ", №", activeGMeter.SerialNumber);
								item.SubItems[12].Text = activeGMeter.get_ID().ToString();
								if (activeGMeter.PlombNumber1.Length == 0 & item.SubItems[7].Text.Length > 0)
								{
									item.BackColor = Color.Yellow;
									num++;
									item.Checked = false;
								}
								if ((activeGMeter.PlombNumber1 != item.SubItems[7].Text) & (item.BackColor != Color.Yellow) & item.SubItems[7].Text.Length > 0)
								{
									item.BackColor = Color.SkyBlue;
									num++;
									item.Checked = false;
								}
								if (activeGMeter.PlombNumber2.Length == 0 & item.SubItems[8].Text.Length > 0)
								{
									item.BackColor = Color.Yellow;
									num++;
									item.Checked = false;
								}
								if ((activeGMeter.PlombNumber2 != item.SubItems[8].Text) & (item.BackColor != Color.Yellow) & item.SubItems[8].Text.Length > 0)
								{
									item.BackColor = Color.SkyBlue;
									num++;
									item.Checked = false;
								}
							}
						}
						if (item.SubItems[4].Text.ToUpper() != item.SubItems[11].Text.ToUpper())
						{
							item.BackColor = Color.Red;
							num++;
							item.Checked = false;
						}
						else if ((item.BackColor != Color.Yellow) & (item.BackColor != Color.SkyBlue))
						{
							item.Checked = true;
						}
						if (item.SubItems[3].Text.StartsWith("ВНИМАНИЕ! ОШИБКА!"))
						{
							item.BackColor = Color.Red;
							num++;
							item.Checked = false;
						}
						if (!((item.SubItems[7].Text == "") & (item.SubItems[8].Text == "")))
						{
							continue;
						}
						item.BackColor = Color.Orange;
						num++;
						item.Checked = false;
					}
				}
				this.stBar1.Panels[1].Text = string.Concat("Ошибок ", num.ToString(), ", предупреждений ", num1.ToString());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка при сверке ЛС ", exception.Message), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}