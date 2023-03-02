using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmGetTFPhones : Form
	{
		private Button cmdApply;

		private Button cmdOpen;

		private StatusBar stBar1;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private StatusBarPanel statusBarPanel3;

		private ListView lv;

		private ColumnHeader columnHeader0;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private System.ComponentModel.Container components = null;

		private string filepath;

		private string filename;

		private double summaFF;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private int countFF;

		public frmGetTFPhones()
		{
			this.InitializeComponent();
		}

		private void CheckingPhones()
		{
			try
			{
				bool flag = false;
				bool flag1 = false;
				foreach (ListViewItem item in this.lv.Items)
				{
					if (!item.Checked)
					{
						continue;
					}
					Phone phone = new Phone();
					Contracts contract = new Contracts();
					contract.Load(item.SubItems[1].Text);
					if (contract[0] == null || contract[0].oPerson == null)
					{
						continue;
					}
					flag = true;
					flag1 = true;
					foreach (Phone oPhone in contract[0].oPerson.oPhones)
					{
						phone = oPhone;
						if (item.SubItems[2].Text.IndexOf(oPhone.NumberPhone) < 0)
						{
							flag = true;
							flag1 = false;
						}
						else
						{
							flag1 = true;
							flag = false;
							if (item.SubItems[2].Text != oPhone.NumberPhone)
							{
								continue;
							}
							flag1 = false;
						}
					}
					try
					{
						if (flag)
						{
							phone.NumberPhone = string.Concat(phone.NumberPhone, "/", item.SubItems[2].Text);
							if (phone.NumberPhone == null)
							{
								phone.NumberPhone = item.SubItems[2].Text;
							}
						}
						if (flag1)
						{
							phone.NumberPhone = item.SubItems[2].Text;
						}
						if (phone.NumberPhone == null)
						{
							phone = contract[0].oPerson.oPhones.Add();
							phone.NumberPhone = item.SubItems[2].Text;
						}
						if (phone.NumberPhone.Trim().Substring(0, 1) == "/")
						{
							phone.NumberPhone = phone.NumberPhone.Trim().Substring(1, phone.NumberPhone.Trim().Length - 1);
						}
						item.SubItems[6].Text = phone.NumberPhone;
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		private void cmdApply_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				int num = 0;
				int num1 = 0;
				foreach (ListViewItem item in this.lv.Items)
				{
					if (!item.Checked)
					{
						continue;
					}
					Phone phone = new Phone();
					Contracts contract = new Contracts();
					contract.Load(item.SubItems[1].Text);
					if (contract[0] == null || contract[0].oPerson == null)
					{
						continue;
					}
					foreach (Phone oPhone in contract[0].oPerson.oPhones)
					{
						phone = oPhone;
					}
					try
					{
						if (phone.NumberPhone == null)
						{
							phone = contract[0].oPerson.oPhones.Add();
						}
						string str = item.SubItems[6].Text.Trim();
						phone.NumberPhone = str;
						phone.Save();
						item.BackColor = Color.Green;
						num++;
					}
					catch
					{
						item.BackColor = Color.Red;
						num1++;
					}
				}
				this.Cursor = Cursors.Default;
				string[] strArrays = new string[] { "Успешно загружено ", num.ToString(), " новых номеров, ", num1.ToString(), " - не загружено!" };
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
				Filter = "ГТМ (*.txt)|*.txt|Все типы файлов (*.*)|*.*"
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.filepath = openFileDialog.FileName;
				string str = this.filepath;
				char[] chrArray = new char[] { char.Parse("\\") };
				string[] strArrays = str.Split(chrArray);
				this.filename = strArrays[(int)strArrays.Length - 1];
				this.Text = string.Concat("Загрузка телефонов из транспортного файла: ", this.filepath);
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
			Encoding encoding;
			StreamReader streamReader;
			string str;
			string[] strArrays;
			byte[] numArray;
			try
			{
				double num = 0;
				this.lv.Items.Clear();
				ListViewItem listViewItem = new ListViewItem();
				string lower = this.filepath.Substring(this.filepath.Length - 4).ToLower();
				".txt";
				string str1 = lower;
				string str2 = str1;
				if (str1 != null)
				{
					str2 = string.IsInterned(str2);
					if ((object)str2 != (object)".txt")
					{
						goto Label1;
					}
					encoding = Encoding.GetEncoding(1251);
					streamReader = new StreamReader(path, encoding);
					this.countFF = 0;
					while (streamReader.Peek() != -1)
					{
						str = streamReader.ReadLine();
						numArray = new byte[] { 63 };
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
							frmGetTFPhones frmGetTFPhone = this;
							frmGetTFPhone.Text = string.Concat(frmGetTFPhone.Text, " ", strArrays[1].ToString());
						}
						if ((int)strArrays.Length < 4 || !(strArrays[0].Trim() == "2"))
						{
							continue;
						}
						listViewItem = new ListViewItem("")
						{
							Checked = false
						};
						listViewItem.SubItems.Add(strArrays[1].Trim());
						listViewItem.SubItems.Add(strArrays[3].Trim());
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						listViewItem.SubItems.Add("");
						this.lv.Items.Add(listViewItem);
						this.countFF++;
					}
					streamReader.Close();
					goto Label0;
				}
			Label1:
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
						frmGetTFPhones frmGetTFPhone1 = this;
						frmGetTFPhone1.Text = string.Concat(frmGetTFPhone1.Text, " ", strArrays[1].ToString());
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
					ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
					DateTime dateTime = Convert.ToDateTime(strArrays[3]);
					subItems.Add(dateTime.ToShortDateString());
					listViewItem.SubItems.Add(strArrays[2].Trim().Replace(".", ","));
					listViewItem.SubItems.Add("");
					listViewItem.SubItems.Add("");
					this.lv.Items.Add(listViewItem);
					listViewItem.SubItems[1].Text = Convert.ToString(listViewItem.Index + 1);
					num += Convert.ToDouble(strArrays[2].Replace(".", ","));
					this.countFF++;
				}
				streamReader.Close();
			Label0:
				streamReader = null;
				listViewItem = null;
				StatusBarPanel item = this.stBar1.Panels[0];
				int count = this.lv.Items.Count;
				item.Text = string.Concat("Всего ", count.ToString());
				this.stBar1.Panels[1].Text = string.Concat("Количество из ТФ ", this.summaFF.ToString());
				this.stBar1.Panels[2].Text = "Ошибок 0";
				this.SverkaLS();
				this.CheckingPhones();
			}
			catch
			{
				MessageBox.Show("Не верный формат файла!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void frmGetTFPhones_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmGetTFPhones_Load(object sender, EventArgs e)
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
			this.stBar1 = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.statusBarPanel3 = new StatusBarPanel();
			this.lv = new ListView();
			this.columnHeader0 = new ColumnHeader();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			((ISupportInitialize)this.statusBarPanel3).BeginInit();
			base.SuspendLayout();
			this.cmdApply.Enabled = false;
			this.cmdApply.FlatStyle = FlatStyle.Flat;
			this.cmdApply.Location = new Point(104, 8);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(88, 24);
			this.cmdApply.TabIndex = 5;
			this.cmdApply.Text = "Загрузить";
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
			this.stBar1.Size = new System.Drawing.Size(794, 24);
			this.stBar1.TabIndex = 10;
			this.stBar1.Text = "statusBar1";
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Width = 10;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Width = 10;
			this.statusBarPanel3.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel3.Width = 758;
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			this.lv.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader0, this.columnHeader1, this.columnHeader2, this.columnHeader5, this.columnHeader3, this.columnHeader6, this.columnHeader4 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(0, 44);
			this.lv.MultiSelect = false;
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(792, 304);
			this.lv.TabIndex = 4;
			this.lv.View = View.Details;
			this.lv.ItemCheck += new ItemCheckEventHandler(this.lv_ItemCheck);
			this.columnHeader0.Text = "ОК";
			this.columnHeader0.Width = 27;
			this.columnHeader1.Text = "Л/счет из ТФ";
			this.columnHeader1.Width = 100;
			this.columnHeader2.Text = "Номер телефона";
			this.columnHeader2.Width = 160;
			this.columnHeader5.Text = "Л/счет из БД";
			this.columnHeader5.Width = 100;
			this.columnHeader3.Text = "Телефон из БД";
			this.columnHeader3.Width = 109;
			this.columnHeader6.Text = "ФИО из БД";
			this.columnHeader6.Width = 160;
			this.columnHeader4.Text = "Будет записан в БД";
			this.columnHeader4.Width = 160;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(794, 375);
			base.Controls.Add(this.cmdApply);
			base.Controls.Add(this.cmdOpen);
			base.Controls.Add(this.stBar1);
			base.Controls.Add(this.lv);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MinimumSize = new System.Drawing.Size(800, 400);
			base.Name = "frmGetTFPhones";
			this.Text = "Загрузка телефонов из транспортного файла";
			base.Closing += new CancelEventHandler(this.frmGetTFPhones_Closing);
			base.Load += new EventHandler(this.frmGetTFPhones_Load);
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			((ISupportInitialize)this.statusBarPanel3).EndInit();
			base.ResumeLayout(false);
		}

		private void lv_ItemCheck(object sender, ItemCheckEventArgs e)
		{
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
			try
			{
				int num = 0;
				int num1 = 0;
				bool flag = false;
				foreach (ListViewItem item in this.lv.Items)
				{
					Contracts contract = new Contracts();
					contract.Load(item.SubItems[1].Text);
					if (contract[0] == null)
					{
						item.BackColor = Color.Red;
						item.SubItems[3].Text = "9999999";
						item.SubItems[4].Text = "Не известно";
						num++;
					}
					else
					{
						item.SubItems[3].Text = contract[0].Account;
						if (contract[0].oPerson == null)
						{
							continue;
						}
						item.SubItems[5].Text = contract[0].oPerson.FullName;
						flag = true;
						foreach (Phone oPhone in contract[0].oPerson.oPhones)
						{
							item.SubItems[4].Text = oPhone.NumberPhone;
							if (item.SubItems[2].Text.IndexOf(oPhone.NumberPhone) < 0)
							{
								continue;
							}
							flag = true;
							if (item.SubItems[2].Text != oPhone.NumberPhone)
							{
								continue;
							}
							flag = false;
						}
						if (!flag)
						{
							item.BackColor = Color.LightGreen;
						}
						else
						{
							item.BackColor = Color.Orange;
							item.Checked = true;
						}
					}
				}
				this.stBar1.Panels[2].Text = string.Concat("Ошибок ", num.ToString(), ", предупреждений ", num1.ToString());
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка при сверке ЛС ", exception.Message), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}