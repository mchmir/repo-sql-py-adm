using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using WebAddress;
using WebSecurityDLL;

namespace Gefest
{
	public class frmSimpleObj : Form
	{
		private Button cmdCancel;

		private Button cmdOk;

		private IContainer components;

		private SimpleClass _object;

		private SimpleClasss[] _objects;

		private Address _address;

		private string[] NameColumns2;

		private int[] WidthColumns2;

		private string[] ValueColumns2;

		private Type[] TypeValues2;

		private string[] FullListControls;

		private string[] FullListNameLabels;

		private ImageList ilFolders;

		private ToolTip toolTip1;

		private string[] FullListNameRefObjects;

		public frmSimpleObj(string vCaption, SimpleClass oObject, string[] FullListControls, string[] FullListNameLabels, string[] FullListNameRefObjects)
		{
			this._object = oObject;
			this.Text = vCaption;
			this.FullListControls = FullListControls;
			this.FullListNameLabels = FullListNameLabels;
			this.FullListNameRefObjects = FullListNameRefObjects;
			this.InitializeComponent();
		}

		private void but_Click(object sender, EventArgs e)
		{
			string[] strArrays;
			int[] numArray;
			Type[] typeArray;
			Button button = new Button();
			button = (Button)sender;
			int num = Convert.ToInt32(button.Tag.ToString());
			if (this.FullListNameRefObjects[num] == "Address")
			{
				frmAddress _frmAddress = new frmAddress()
				{
					oAddress = this._address
				};
				_frmAddress.ShowDialog(this);
				if (_frmAddress.DialogResult == System.Windows.Forms.DialogResult.OK)
				{
					this._address = _frmAddress.oAddress;
					Label label = new Label();
					label = (Label)base.Controls[(int)this.FullListControls.Length + num];
					label.Text = this._address.get_ShortAddress();
				}
				_frmAddress = null;
				return;
			}
			"Agent";
			string fullListNameRefObjects = this.FullListNameRefObjects[num];
			string str = fullListNameRefObjects;
			if (fullListNameRefObjects != null)
			{
				str = string.IsInterned(str);
				if ((object)str == (object)"TypePlace")
				{
					strArrays = new string[] { "Тип нас.пункта" };
					this.NameColumns2 = strArrays;
					numArray = new int[] { 150 };
					this.WidthColumns2 = numArray;
					strArrays = new string[] { "Name" };
					this.ValueColumns2 = strArrays;
					typeArray = new Type[] { typeof(ListViewTextSort) };
					this.TypeValues2 = typeArray;
				}
				else if ((object)str == (object)"TypeStreet")
				{
					strArrays = new string[] { "Тип улицы" };
					this.NameColumns2 = strArrays;
					numArray = new int[] { 150 };
					this.WidthColumns2 = numArray;
					strArrays = new string[] { "Name" };
					this.ValueColumns2 = strArrays;
					typeArray = new Type[] { typeof(ListViewTextSort) };
					this.TypeValues2 = typeArray;
				}
				else if ((object)str == (object)"TypeHouse")
				{
					strArrays = new string[] { "Тип дома" };
					this.NameColumns2 = strArrays;
					numArray = new int[] { 150 };
					this.WidthColumns2 = numArray;
					strArrays = new string[] { "Name" };
					this.ValueColumns2 = strArrays;
					typeArray = new Type[] { typeof(ListViewTextSort) };
					this.TypeValues2 = typeArray;
				}
				else if ((object)str == (object)"TypeAddress")
				{
					strArrays = new string[] { "Тип квартиры" };
					this.NameColumns2 = strArrays;
					numArray = new int[] { 150 };
					this.WidthColumns2 = numArray;
					strArrays = new string[] { "Name" };
					this.ValueColumns2 = strArrays;
					typeArray = new Type[] { typeof(ListViewTextSort) };
					this.TypeValues2 = typeArray;
				}
				else if ((object)str == (object)"TypeAgent")
				{
					strArrays = new string[] { "Тип агента" };
					this.NameColumns2 = strArrays;
					numArray = new int[] { 150 };
					this.WidthColumns2 = numArray;
					strArrays = new string[] { "Name" };
					this.ValueColumns2 = strArrays;
					typeArray = new Type[] { typeof(ListViewTextSort) };
					this.TypeValues2 = typeArray;
				}
				else if ((object)str == (object)"Agent")
				{
					strArrays = new string[] { "Агент" };
					this.NameColumns2 = strArrays;
					numArray = new int[] { 150 };
					this.WidthColumns2 = numArray;
					strArrays = new string[] { "Name" };
					this.ValueColumns2 = strArrays;
					typeArray = new Type[] { typeof(ListViewTextSort) };
					this.TypeValues2 = typeArray;
				}
			}
			frmSimpleObjs frmSimpleObj = new frmSimpleObjs("Справочник", this._objects[num], this.NameColumns2, this.WidthColumns2, this.ValueColumns2, this.TypeValues2);
			frmSimpleObj.ShowDialog(this);
			frmSimpleObj = null;
			ComboBox comboBox = new ComboBox();
			comboBox = (ComboBox)base.Controls[(int)this.FullListControls.Length + num];
			Tools.FillCMB(this._objects[num], comboBox, (long)((int)this._object.GetValue(string.Concat("ID", this.FullListNameRefObjects[num]))));
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < (int)this.FullListControls.Length; i++)
				{
					"system.object";
					string lower = this._object.GetValue((string)base.Controls[i].Tag).GetType().ToString().ToLower();
					string str = lower;
					if (lower != null)
					{
						str = string.IsInterned(str);
						if ((object)str == (object)"system.int32")
						{
							this._object.SetValue(this.FullListControls[i], Convert.ToInt32(base.Controls[i].Text));
						}
						else if ((object)str == (object)"system.string")
						{
							this._object.SetValue(this.FullListControls[i], base.Controls[i].Text);
						}
						else if ((object)str == (object)"system.datetime")
						{
							this._object.SetValue(this.FullListControls[i], ((DateTimePicker)base.Controls[i]).Value);
						}
						else if ((object)str == (object)"system.int64")
						{
							this._object.SetValue(this.FullListControls[i], Convert.ToInt64(base.Controls[i].Text));
						}
						else if ((object)str == (object)"system.double")
						{
							this._object.SetValue(this.FullListControls[i], Convert.ToDouble(base.Controls[i].Text.Replace(".", ",")));
						}
						else if ((object)str == (object)"system.boolean")
						{
							this._object.SetValue(this.FullListControls[i], Convert.ToBoolean(((CheckBox)base.Controls[i]).Checked));
						}
						else if ((object)str != (object)"system.object")
						{
						}
					}
				}
				if (this.FullListNameRefObjects != null)
				{
					int length = (int)this.FullListControls.Length;
					ComboBox comboBox = new ComboBox();
					for (int j = 0; j < (int)this.FullListNameRefObjects.Length; j++)
					{
						if (this.FullListNameRefObjects[j] == "Address")
						{
							this._object.SetValue("IDAddress", this._address.get_ID());
						}
						else
						{
							comboBox = (ComboBox)base.Controls[length];
							SimpleClass simpleClass = this._object;
							string str1 = string.Concat("ID", this.FullListNameRefObjects[j]);
							long d = this._objects[j].get_Item(comboBox.SelectedIndex).get_ID();
							simpleClass.SetValue(str1, Convert.ToInt16(d.ToString()));
						}
						length++;
					}
				}
				int num = this._object.Save();
				if (num == 0 || num == 13)
				{
					base.Close();
				}
				else
				{
					MessageBox.Show("Ошибка сохранения объекта!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			catch (Exception exception)
			{
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			this._object = null;
			this._objects = null;
			if (this._address != null)
			{
				this._address = null;
			}
			base.Dispose(disposing);
		}

		private void frmSimpleObj_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmSimpleObj_Load(object sender, EventArgs e)
		{
			Label label;
			string str;
			try
			{
				System.Drawing.Size size = new System.Drawing.Size(250, 20);
				System.Drawing.Size size1 = new System.Drawing.Size(250, 21);
				System.Drawing.Size size2 = new System.Drawing.Size(250, 20);
				System.Drawing.Size size3 = new System.Drawing.Size(116, 24);
				System.Drawing.Size size4 = new System.Drawing.Size(20, 20);
				Point point = new Point(4, 8);
				Point y = new Point(106, 8);
				Point point1 = new Point(357, 8);
				int num = 0;
				Tools.LoadWindows(this);
				for (int i = 0; i < (int)this.FullListControls.Length; i++)
				{
					label = new Label();
					base.Controls.Add(label);
					label.Text = this.FullListNameLabels[i];
					label.Size = size2;
					point.Y = point.Y + num;
					y.Y = y.Y + num;
					point1.Y = point1.Y + num;
					label.Location = point;
					num = 25;
					"system.boolean";
					string lower = this._object.GetValue(this.FullListControls[i]).GetType().ToString().ToLower();
					str = lower;
					if (lower != null)
					{
						str = string.IsInterned(str);
						if ((object)str == (object)"system.int32")
						{
							TextBox textBox = new TextBox()
							{
								Tag = this.FullListControls[i],
								BorderStyle = BorderStyle.FixedSingle
							};
							base.Controls.Add(textBox);
							base.Controls.SetChildIndex(textBox, i);
							textBox.Text = this._object.GetValue(this.FullListControls[i]).ToString();
							textBox.Size = size;
							textBox.Location = y;
						}
						else if ((object)str == (object)"system.string")
						{
							TextBox textBox1 = new TextBox()
							{
								Tag = this.FullListControls[i],
								BorderStyle = BorderStyle.FixedSingle
							};
							base.Controls.Add(textBox1);
							base.Controls.SetChildIndex(textBox1, i);
							textBox1.Text = this._object.GetValue(this.FullListControls[i]).ToString();
							textBox1.Size = size;
							textBox1.Location = y;
						}
						else if ((object)str == (object)"system.datetime")
						{
							DateTimePicker dateTimePicker = new DateTimePicker()
							{
								Tag = this.FullListControls[i]
							};
							base.Controls.Add(dateTimePicker);
							base.Controls.SetChildIndex(dateTimePicker, i);
							if (dateTimePicker.MinDate > (DateTime)this._object.GetValue(this.FullListControls[i]))
							{
								dateTimePicker.Value = dateTimePicker.MinDate;
							}
							else if (dateTimePicker.MaxDate >= (DateTime)this._object.GetValue(this.FullListControls[i]))
							{
								dateTimePicker.Value = (DateTime)this._object.GetValue(this.FullListControls[i]);
							}
							else
							{
								dateTimePicker.Value = dateTimePicker.MaxDate;
							}
							dateTimePicker.Size = size;
							dateTimePicker.Location = y;
						}
						else if ((object)str == (object)"system.int64")
						{
							TextBox str1 = new TextBox()
							{
								Tag = this.FullListControls[i],
								BorderStyle = BorderStyle.FixedSingle
							};
							base.Controls.Add(str1);
							base.Controls.SetChildIndex(str1, i);
							str1.Text = this._object.GetValue(this.FullListControls[i]).ToString();
							str1.Size = size;
							str1.Location = y;
						}
						else if ((object)str == (object)"system.double")
						{
							TextBox textBox2 = new TextBox()
							{
								Tag = this.FullListControls[i],
								BorderStyle = BorderStyle.FixedSingle
							};
							base.Controls.Add(textBox2);
							base.Controls.SetChildIndex(textBox2, i);
							textBox2.Text = this._object.GetValue(this.FullListControls[i]).ToString();
							textBox2.Size = size;
							textBox2.Location = y;
						}
						else if ((object)str == (object)"system.boolean")
						{
							CheckBox checkBox = new CheckBox()
							{
								Tag = this.FullListControls[i],
								FlatStyle = FlatStyle.Flat
							};
							base.Controls.Add(checkBox);
							base.Controls.SetChildIndex(checkBox, i);
							checkBox.Checked = Convert.ToInt16(this._object.GetValue(this.FullListControls[i])) == 1;
							checkBox.Location = y;
						}
					}
				}
				if (this.FullListNameRefObjects != null)
				{
					int length = (int)this.FullListControls.Length;
					this._objects = new SimpleClasss[(int)this.FullListNameRefObjects.Length];
					for (int j = 0; j < (int)this.FullListNameRefObjects.Length; j++)
					{
						label = new Label();
						base.Controls.Add(label);
						label.Text = this.FullListNameLabels[length];
						label.Size = size2;
						point.Y = point.Y + num;
						y.Y = y.Y + num;
						point1.Y = point1.Y + num;
						label.Location = point;
						num = 25;
						if (this.FullListNameRefObjects[j] == "Address")
						{
							label = new Label();
							base.Controls.Add(label);
							base.Controls.SetChildIndex(label, length);
							label.BackColor = Color.LightYellow;
							label.BorderStyle = BorderStyle.Fixed3D;
							label.Size = size2;
							label.Location = y;
						}
						else
						{
							ComboBox comboBox = new ComboBox();
							base.Controls.Add(comboBox);
							comboBox.Tag = this.FullListNameRefObjects[j];
							base.Controls.SetChildIndex(comboBox, length);
							comboBox.Size = size1;
							comboBox.Location = y;
						}
						Button button = new Button();
						base.Controls.Add(button);
						button.Tag = j.ToString();
						button.ImageList = this.ilFolders;
						button.ImageIndex = 0;
						button.FlatStyle = FlatStyle.Flat;
						button.Size = size4;
						button.Location = point1;
						this.toolTip1.SetToolTip(button, "Справочник ");
						button.Click += new EventHandler(this.but_Click);
						"Agent";
						string fullListNameRefObjects = this.FullListNameRefObjects[j];
						str = fullListNameRefObjects;
						if (fullListNameRefObjects != null)
						{
							str = string.IsInterned(str);
							if ((object)str == (object)"Address")
							{
								this._objects[j] = new Addresss();
							}
							else if ((object)str == (object)"TypePlace")
							{
								this._objects[j] = new TypePlaces();
							}
							else if ((object)str == (object)"TypeStreet")
							{
								this._objects[j] = new TypeStreets();
							}
							else if ((object)str == (object)"TypeHouse")
							{
								this._objects[j] = new TypeHouses();
							}
							else if ((object)str == (object)"TypeAddress")
							{
								this._objects[j] = new TypeAddresss();
							}
							else if ((object)str == (object)"TypeAgent")
							{
								this._objects[j] = new TypeAgents();
							}
							else if ((object)str == (object)"Agent")
							{
								this._objects[j] = new Agents();
							}
						}
						if (this.FullListNameRefObjects[j] == "Address")
						{
							Label item = new Label();
							item = (Label)base.Controls[length];
							int value = (int)this._object.GetValue(string.Concat("ID", this.FullListNameRefObjects[j]));
							this._address = new Address();
							this._address.Load((long)value);
							item.Text = this._address.get_ShortAddress();
						}
						else
						{
							this._objects[j].Load();
							ComboBox comboBox1 = new ComboBox();
							comboBox1 = (ComboBox)base.Controls[length];
							Tools.FillCMB(this._objects[j], comboBox1, (long)((int)this._object.GetValue(string.Concat("ID", this.FullListNameRefObjects[j]))));
						}
						length++;
					}
				}
				base.Height = point1.Y + 94;
				base.Width = point1.X + 30;
				y.Y = base.Height - 60;
				y.X = base.Width - 256;
				this.cmdOk.Location = y;
				y.X = y.X + 124;
				this.cmdCancel.Location = y;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(string.Concat("Ошибка ", exception.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmSimpleObj));
			this.cmdCancel = new Button();
			this.cmdOk = new Button();
			this.ilFolders = new ImageList(this.components);
			this.toolTip1 = new ToolTip(this.components);
			base.SuspendLayout();
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.FlatStyle = FlatStyle.Flat;
			this.cmdCancel.Location = new Point(360, 256);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(116, 24);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Отмена";
			this.toolTip1.SetToolTip(this.cmdCancel, "Отмена");
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			this.cmdOk.FlatStyle = FlatStyle.Flat;
			this.cmdOk.Location = new Point(240, 256);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(116, 24);
			this.cmdOk.TabIndex = 1;
			this.cmdOk.Text = "Ok";
			this.toolTip1.SetToolTip(this.cmdOk, "Сохранить и закрыть");
			this.cmdOk.Click += new EventHandler(this.cmdOk_Click);
			this.ilFolders.ImageSize = new System.Drawing.Size(16, 16);
			this.ilFolders.ImageStream = (ImageListStreamer)resourceManager.GetObject("ilFolders.ImageStream");
			this.ilFolders.TransparentColor = Color.Transparent;
			base.AcceptButton = this.cmdOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = this.cmdCancel;
			base.ClientSize = new System.Drawing.Size(476, 287);
			base.Controls.Add(this.cmdOk);
			base.Controls.Add(this.cmdCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.Name = "frmSimpleObj";
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Closing += new CancelEventHandler(this.frmSimpleObj_Closing);
			base.Load += new EventHandler(this.frmSimpleObj_Load);
			base.ResumeLayout(false);
		}
	}
}