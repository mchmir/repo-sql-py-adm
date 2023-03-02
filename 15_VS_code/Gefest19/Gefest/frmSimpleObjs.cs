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
	public class frmSimpleObjs : Form
	{
		private ListViewSortManager m_sortMgr1;

		private ToolBarButton cmdAdd;

		private ToolBarButton cmdEdit;

		private ToolBarButton cmdDel;

		private ToolBarButton cmdExcel;

		private ToolBarButton toolBarButton4;

		private IContainer components;

		public ListView lvData;

		private ToolBar tbData;

		private ImageList imageList1;

		private SimpleClasss _objects;

		private string[] NameColumns;

		private int[] WidthColumns;

		private string[] ValueColumns;

		private Type[] TypeValues;

		private string[] FullListControls;

		private string[] FullListNameLabels;

		private StatusBarPanel statusBarPanel1;

		private StatusBarPanel statusBarPanel2;

		private StatusBar sbSimple;

		private StatusBarPanel statusBarPanel3;

		private string[] FullListNameRefObjects;

		public frmSimpleObjs(string vCaption, SimpleClasss oObjects, string[] NameColumns, int[] WidthColumns, string[] ValueColumns, Type[] TypeValues)
		{
			this.Text = vCaption;
			this._objects = oObjects;
			this.NameColumns = NameColumns;
			this.WidthColumns = WidthColumns;
			this.ValueColumns = ValueColumns;
			this.TypeValues = TypeValues;
			this.InitializeComponent();
		}

		private void AddObject()
		{
			if (<PrivateImplementationDetails>.$$method0x6000823-1 == null)
			{
				<PrivateImplementationDetails>.$$method0x6000823-1 = new Hashtable(50, 0.5f)
				{
					{ "Address", 0 },
					{ "Street", 1 },
					{ "House", 2 },
					{ "Country", 3 },
					{ "Place", 4 },
					{ "Region", 5 },
					{ "TypePlace", 6 },
					{ "TypeStreet", 7 },
					{ "TypeHouse", 8 },
					{ "TypeAddress", 9 },
					{ "TypeGobject", 10 },
					{ "TypeDocument", 11 },
					{ "Agent", 12 },
					{ "TypeAgent", 13 },
					{ "TypeBatch", 14 },
					{ "TypeReasonDisconnect", 15 },
					{ "TypeContract", 16 },
					{ "TypeInfringements", 17 },
					{ "TypeGMeter", 18 },
					{ "TypeOperation", 19 },
					{ "TypePay", 20 },
					{ "Stavka", 21 },
					{ "Ownership", 22 },
					{ "Classifier", 23 },
					{ "GRU", 24 }
				};
			}
			SimpleClass simpleClass = null;
			string str = "";
			string nameTable = this._objects.NameTable;
			object obj = nameTable;
			if (nameTable != null)
			{
				object item = <PrivateImplementationDetails>.$$method0x6000823-1[obj];
				obj = item;
				if (item != null)
				{
					switch ((int)obj)
					{
						case 0:
						{
							simpleClass = ((Addresss)this._objects).Add();
							break;
						}
						case 1:
						{
							simpleClass = ((Streets)this._objects).Add();
							break;
						}
						case 2:
						{
							simpleClass = ((Houses)this._objects).Add();
							break;
						}
						case 3:
						{
							simpleClass = ((Countrys)this._objects).Add();
							break;
						}
						case 4:
						{
							simpleClass = ((Places)this._objects).Add();
							break;
						}
						case 5:
						{
							simpleClass = ((Provinces)this._objects).Add();
							break;
						}
						case 6:
						{
							simpleClass = ((TypePlaces)this._objects).Add();
							break;
						}
						case 7:
						{
							simpleClass = ((TypeStreets)this._objects).Add();
							break;
						}
						case 8:
						{
							simpleClass = ((TypeHouses)this._objects).Add();
							break;
						}
						case 9:
						{
							simpleClass = ((TypeAddresss)this._objects).Add();
							break;
						}
						case 10:
						{
							simpleClass = ((TypeGobjects)this._objects).Add();
							break;
						}
						case 11:
						{
							simpleClass = ((TypeDocuments)this._objects).Add();
							break;
						}
						case 12:
						{
							simpleClass = ((Agents)this._objects).Add();
							break;
						}
						case 13:
						{
							simpleClass = ((TypeAgents)this._objects).Add();
							break;
						}
						case 14:
						{
							simpleClass = ((TypeBatchs)this._objects).Add();
							break;
						}
						case 15:
						{
							simpleClass = ((TypeReasonDisconnects)this._objects).Add();
							break;
						}
						case 16:
						{
							simpleClass = ((TypeContracts)this._objects).Add();
							break;
						}
						case 17:
						{
							simpleClass = ((TypeInfringementss)this._objects).Add();
							break;
						}
						case 18:
						{
							simpleClass = ((TypeGMeters)this._objects).Add();
							break;
						}
						case 19:
						{
							simpleClass = ((TypeOperations)this._objects).Add();
							break;
						}
						case 20:
						{
							simpleClass = ((TypePays)this._objects).Add();
							break;
						}
						case 21:
						{
							simpleClass = ((Stavkas)this._objects).Add();
							break;
						}
						case 22:
						{
							simpleClass = ((Ownerships)this._objects).Add();
							break;
						}
						case 23:
						{
							simpleClass = ((Classifiers)this._objects).Add();
							break;
						}
						case 24:
						{
							simpleClass = ((GRUs)this._objects).Add();
							break;
						}
					}
				}
			}
			str = this.initSingleObject(this._objects.NameTable, 1);
			frmSimpleObj _frmSimpleObj = new frmSimpleObj(str, simpleClass, this.FullListControls, this.FullListNameLabels, this.FullListNameRefObjects);
			_frmSimpleObj.ShowDialog(this);
			_frmSimpleObj = null;
			simpleClass = null;
			this.FillList();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			this._objects = null;
			base.Dispose(disposing);
		}

		private void EditObject()
		{
			if (<PrivateImplementationDetails>.$$method0x6000824-1 == null)
			{
				<PrivateImplementationDetails>.$$method0x6000824-1 = new Hashtable(50, 0.5f)
				{
					{ "Address", 0 },
					{ "Street", 1 },
					{ "House", 2 },
					{ "Country", 3 },
					{ "Place", 4 },
					{ "Region", 5 },
					{ "TypeAddress", 6 },
					{ "TypeHouse", 7 },
					{ "TypePlace", 8 },
					{ "TypeStreet", 9 },
					{ "TypeGobject", 10 },
					{ "Agent", 11 },
					{ "TypeAgent", 12 },
					{ "TypeBatch", 13 },
					{ "TypeReasonDisconnect", 14 },
					{ "TypeContract", 15 },
					{ "TypeInfringements", 16 },
					{ "TypeGMeter", 17 },
					{ "TypeDocument", 18 },
					{ "TypeOperation", 19 },
					{ "TypePay", 20 },
					{ "Stavka", 21 },
					{ "Ownership", 22 },
					{ "Classifier", 23 },
					{ "GRU", 24 }
				};
			}
			SimpleClass simpleClass = null;
			string str = "";
			if (this.lvData.SelectedItems.Count > 0)
			{
				string nameTable = this._objects.NameTable;
				object obj = nameTable;
				if (nameTable != null)
				{
					object item = <PrivateImplementationDetails>.$$method0x6000824-1[obj];
					obj = item;
					if (item != null)
					{
						switch ((int)obj)
						{
							case 0:
							{
								simpleClass = ((Addresss)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 1:
							{
								simpleClass = ((Streets)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 2:
							{
								simpleClass = ((Houses)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 3:
							{
								simpleClass = ((Countrys)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 4:
							{
								simpleClass = ((Places)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 5:
							{
								simpleClass = ((Provinces)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 6:
							{
								simpleClass = ((TypeAddresss)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 7:
							{
								simpleClass = ((TypeHouses)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 8:
							{
								simpleClass = ((TypePlaces)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 9:
							{
								simpleClass = ((TypeStreets)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 10:
							{
								simpleClass = ((TypeGobjects)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 11:
							{
								simpleClass = ((Agents)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 12:
							{
								simpleClass = ((TypeAgents)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 13:
							{
								simpleClass = ((TypeBatchs)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 14:
							{
								simpleClass = ((TypeReasonDisconnects)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 15:
							{
								simpleClass = ((TypeContracts)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 16:
							{
								simpleClass = ((TypeInfringementss)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 17:
							{
								simpleClass = ((TypeGMeters)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 18:
							{
								simpleClass = ((TypeDocuments)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 19:
							{
								simpleClass = ((TypeOperations)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 20:
							{
								simpleClass = ((TypePays)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 21:
							{
								simpleClass = ((Stavkas)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 22:
							{
								simpleClass = ((Ownerships)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 23:
							{
								simpleClass = ((Classifiers)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
							case 24:
							{
								simpleClass = ((GRUs)this._objects).item(Convert.ToInt64(this.lvData.SelectedItems[0].Tag));
								break;
							}
						}
					}
				}
				str = this.initSingleObject(this._objects.NameTable, 0);
				frmSimpleObj _frmSimpleObj = new frmSimpleObj(str, simpleClass, this.FullListControls, this.FullListNameLabels, this.FullListNameRefObjects);
				_frmSimpleObj.ShowDialog(this);
				_frmSimpleObj = null;
				simpleClass = null;
				this.FillList();
			}
		}

		private void FillList()
		{
			this.lvData.Items.Clear();
			foreach (SimpleClass _object in this._objects)
			{
				ListViewItem listViewItem = new ListViewItem(_object.GetValue(this.ValueColumns[0]).ToString())
				{
					Tag = _object.get_ID().ToString()
				};
				for (int i = 1; i < (int)this.ValueColumns.Length; i++)
				{
					listViewItem.SubItems.Add(_object.GetValue(this.ValueColumns[i]).ToString());
				}
				this.lvData.Items.Add(listViewItem);
			}
			this.sbSimple.Panels[1].Text = this._objects.get_Count().ToString();
			this.sbSimple.Panels[2].Text = this._objects.NameTable;
		}

		private void frmSimpleObjs_Closing(object sender, CancelEventArgs e)
		{
			Tools.SaveWindows(this);
		}

		private void frmSimpleObjs_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Insert:
				{
					this.AddObject();
					return;
				}
				case Keys.Delete:
				{
					try
					{
						if (this.lvData.SelectedItems.Count > 0 && MessageBox.Show(string.Concat("Удалить ", this.lvData.SelectedItems[0].Text, "?"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
						{
							if (this._objects.Remove(Convert.ToInt64(this.lvData.SelectedItems[0].Tag)) != 0)
							{
								MessageBox.Show(string.Concat("Не удается удалить ", this.lvData.SelectedItems[0].Text, "?"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								this.FillList();
							}
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						MessageBox.Show(string.Concat("Ошибка ", exception.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void frmSimpleObjs_Load(object sender, EventArgs e)
		{
			Tools.LoadWindows(this);
			this.lvData.Columns.Clear();
			for (int i = 0; i < (int)this.NameColumns.Length; i++)
			{
				this.lvData.Columns.Add(this.NameColumns[i], this.WidthColumns[i], HorizontalAlignment.Left);
			}
			this.m_sortMgr1 = new ListViewSortManager(this.lvData, this.TypeValues);
			this.frmSimpleObjs_Resize(null, null);
			this.FillList();
		}

		private void frmSimpleObjs_Resize(object sender, EventArgs e)
		{
			this.lvData.Width = base.Width - 10;
			this.lvData.Height = base.Height - this.tbData.Height - 50;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ResourceManager resourceManager = new ResourceManager(typeof(frmSimpleObjs));
			this.tbData = new ToolBar();
			this.cmdAdd = new ToolBarButton();
			this.cmdEdit = new ToolBarButton();
			this.cmdDel = new ToolBarButton();
			this.toolBarButton4 = new ToolBarButton();
			this.cmdExcel = new ToolBarButton();
			this.imageList1 = new ImageList(this.components);
			this.lvData = new ListView();
			this.sbSimple = new StatusBar();
			this.statusBarPanel1 = new StatusBarPanel();
			this.statusBarPanel2 = new StatusBarPanel();
			this.statusBarPanel3 = new StatusBarPanel();
			((ISupportInitialize)this.statusBarPanel1).BeginInit();
			((ISupportInitialize)this.statusBarPanel2).BeginInit();
			((ISupportInitialize)this.statusBarPanel3).BeginInit();
			base.SuspendLayout();
			this.tbData.Appearance = ToolBarAppearance.Flat;
			ToolBar.ToolBarButtonCollection buttons = this.tbData.Buttons;
			ToolBarButton[] toolBarButtonArray = new ToolBarButton[] { this.cmdAdd, this.cmdEdit, this.cmdDel, this.toolBarButton4, this.cmdExcel };
			buttons.AddRange(toolBarButtonArray);
			this.tbData.DropDownArrows = true;
			this.tbData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.tbData.ImageList = this.imageList1;
			this.tbData.Location = new Point(0, 0);
			this.tbData.Name = "tbData";
			this.tbData.ShowToolTips = true;
			this.tbData.Size = new System.Drawing.Size(556, 28);
			this.tbData.TabIndex = 1;
			this.tbData.TabStop = true;
			this.tbData.ButtonClick += new ToolBarButtonClickEventHandler(this.tbData_ButtonClick);
			this.cmdAdd.ImageIndex = 0;
			this.cmdAdd.Tag = "Add";
			this.cmdAdd.ToolTipText = "Добавить";
			this.cmdEdit.ImageIndex = 1;
			this.cmdEdit.Tag = "Edit";
			this.cmdEdit.ToolTipText = "Редактировать";
			this.cmdDel.ImageIndex = 2;
			this.cmdDel.Tag = "Del";
			this.cmdDel.ToolTipText = "Удалить";
			this.toolBarButton4.Style = ToolBarButtonStyle.Separator;
			this.cmdExcel.ImageIndex = 3;
			this.cmdExcel.Tag = "Excel";
			this.cmdExcel.ToolTipText = "Конвертировать в Excel";
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.lvData.BorderStyle = BorderStyle.FixedSingle;
			this.lvData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 204);
			this.lvData.FullRowSelect = true;
			this.lvData.GridLines = true;
			this.lvData.Location = new Point(0, 28);
			this.lvData.Name = "lvData";
			this.lvData.Size = new System.Drawing.Size(556, 508);
			this.lvData.TabIndex = 2;
			this.lvData.View = View.Details;
			this.lvData.KeyPress += new KeyPressEventHandler(this.lvData_KeyPress);
			this.lvData.DoubleClick += new EventHandler(this.lvData_DoubleClick);
			this.lvData.SelectedIndexChanged += new EventHandler(this.lvData_SelectedIndexChanged);
			this.sbSimple.Location = new Point(0, 535);
			this.sbSimple.Name = "sbSimple";
			StatusBar.StatusBarPanelCollection panels = this.sbSimple.Panels;
			StatusBarPanel[] statusBarPanelArray = new StatusBarPanel[] { this.statusBarPanel1, this.statusBarPanel2, this.statusBarPanel3 };
			panels.AddRange(statusBarPanelArray);
			this.sbSimple.ShowPanels = true;
			this.sbSimple.Size = new System.Drawing.Size(556, 22);
			this.sbSimple.TabIndex = 2;
			this.statusBarPanel1.Alignment = HorizontalAlignment.Center;
			this.statusBarPanel1.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel1.Text = "Записей";
			this.statusBarPanel1.Width = 59;
			this.statusBarPanel2.AutoSize = StatusBarPanelAutoSize.Contents;
			this.statusBarPanel2.Width = 10;
			this.statusBarPanel3.AutoSize = StatusBarPanelAutoSize.Spring;
			this.statusBarPanel3.Width = 471;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(556, 557);
			base.Controls.Add(this.sbSimple);
			base.Controls.Add(this.lvData);
			base.Controls.Add(this.tbData);
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "frmSimpleObjs";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Resize += new EventHandler(this.frmSimpleObjs_Resize);
			base.Closing += new CancelEventHandler(this.frmSimpleObjs_Closing);
			base.Load += new EventHandler(this.frmSimpleObjs_Load);
			base.KeyUp += new KeyEventHandler(this.frmSimpleObjs_KeyUp);
			((ISupportInitialize)this.statusBarPanel1).EndInit();
			((ISupportInitialize)this.statusBarPanel2).EndInit();
			((ISupportInitialize)this.statusBarPanel3).EndInit();
			base.ResumeLayout(false);
		}

		private string initSingleObject(string vName, int Action)
		{
			string str;
			string[] strArrays;
			if (<PrivateImplementationDetails>.$$method0x6000822-1 == null)
			{
				<PrivateImplementationDetails>.$$method0x6000822-1 = new Hashtable(82, 0.5f)
				{
					{ "Address", 0 },
					{ "House", 1 },
					{ "Street", 2 },
					{ "Place", 3 },
					{ "Country", 4 },
					{ "Region", 5 },
					{ "TypeAddress", 6 },
					{ "TypeHouse", 7 },
					{ "TypeStreet", 8 },
					{ "TypePlace", 9 },
					{ "GRU", 10 },
					{ "TypePG", 11 },
					{ "TypeEnd", 12 },
					{ "EndHead", 13 },
					{ "Gascarter", 14 },
					{ "TypeConstant", 15 },
					{ "TypeInfringements", 16 },
					{ "Personal", 17 },
					{ "Status", 18 },
					{ "FillingLevel", 19 },
					{ "Product", 20 },
					{ "TypeGobject", 21 },
					{ "TypeSklad", 22 },
					{ "Kontragent", 23 },
					{ "Norm", 24 },
					{ "DensityTemperature", 25 },
					{ "DensityStructure", 26 },
					{ "FactorTranslation", 27 },
					{ "ContentGas", 28 },
					{ "Agent", 29 },
					{ "TypeAgent", 30 },
					{ "TypeBatch", 31 },
					{ "TypeReasonDisconnect", 32 },
					{ "TypeContract", 33 },
					{ "TypeGMeter", 34 },
					{ "TypeDocument", 35 },
					{ "TypeOperation", 36 },
					{ "TypePay", 37 },
					{ "Stavka", 38 },
					{ "Ownership", 39 },
					{ "Classifier", 40 }
				};
			}
			str = (Action != 1 ? "Редактирование " : "Добавление ");
			string str1 = vName;
			object obj = str1;
			if (str1 != null)
			{
				object item = <PrivateImplementationDetails>.$$method0x6000822-1[obj];
				obj = item;
				if (item != null)
				{
					switch ((int)obj)
					{
						case 0:
						{
							str = string.Concat(str, "Квартиры");
							strArrays = new string[] { "Flat" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Номер:", "Тип:" };
							this.FullListNameLabels = strArrays;
							strArrays = new string[] { "TypeAddress" };
							this.FullListNameRefObjects = strArrays;
							break;
						}
						case 1:
						{
							str = string.Concat(str, "дома");
							strArrays = new string[] { "HouseNumber", "HouseNumberChar", "IsEvenSide", "IsComfortable", "GRU", "VGP", "NGP", "ZU" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Номер:", "Буква:", "Четная сторона:", "Комфортабельный:", "ГРУ в соб-ти:", "ВГП в соб-ти:", "НГП в соб-ти:", "Зем. уч. в соб-ти:", "Тип:" };
							this.FullListNameLabels = strArrays;
							strArrays = new string[] { "TypeHouse" };
							this.FullListNameRefObjects = strArrays;
							break;
						}
						case 2:
						{
							str = string.Concat(str, "улицы");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Тип улицы:" };
							this.FullListNameLabels = strArrays;
							strArrays = new string[] { "TypeStreet" };
							this.FullListNameRefObjects = strArrays;
							break;
						}
						case 3:
						{
							str = string.Concat(str, "нас.пункта");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Тип нас.пункта:" };
							this.FullListNameLabels = strArrays;
							strArrays = new string[] { "TypePlace" };
							this.FullListNameRefObjects = strArrays;
							break;
						}
						case 4:
						{
							str = string.Concat(str, "страны");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 5:
						{
							str = string.Concat(str, "области");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Область:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 6:
						{
							str = "Добавление типа адреса";
							strArrays = new string[] { "Name", "Memo" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Описание:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 7:
						{
							str = "Добавление типа дома";
							strArrays = new string[] { "Name", "Memo" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Описание:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 8:
						{
							str = "Добавление типа улицы";
							strArrays = new string[] { "Name", "ShortName" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Сокр. название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 9:
						{
							str = "Добавление типа города";
							strArrays = new string[] { "Name", "ShortName" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Сокр. название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 10:
						{
							str = string.Concat(str, "ГРУ");
							strArrays = new string[] { "InvNumber", "Name", "Memo", "DateIn", "DateVerify", "Note" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Номер ГРУ:", "Название:", "Старый номер:", "Дата ввода", "Дата проверки", "Примечание:", "Контролер:", "Адрес:" };
							this.FullListNameLabels = strArrays;
							strArrays = new string[] { "Agent", "Address" };
							this.FullListNameRefObjects = strArrays;
							break;
						}
						case 11:
						{
							str = string.Concat(str, "параметра");
							strArrays = new string[] { "Name", "NameTable" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "sysname of table" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 12:
						{
							str = string.Concat(str, "типа окончания");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 13:
						{
							str = string.Concat(str, "окончания сосуда");
							strArrays = new string[] { "Memo" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Тип окончания:" };
							this.FullListNameLabels = strArrays;
							strArrays = new string[] { "TypeEnd" };
							this.FullListNameRefObjects = strArrays;
							break;
						}
						case 14:
						{
							str = string.Concat(str, "газовоза");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Гос. номер:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 15:
						{
							str = string.Concat(str, "типа константы");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Тип константы:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 16:
						{
							str = string.Concat(str, "типа нарушения");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Тип нарушения:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 17:
						{
							str = string.Concat(str, "персонала");
							strArrays = new string[] { "Name", "Department" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Ф.И.О.:", "Отдел:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 18:
						{
							str = string.Concat(str, "статуса");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Статус:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 19:
						{
							str = string.Concat(str, "уровня заполнения");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Уровень заполн.:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 20:
						{
							str = string.Concat(str, "товара");
							strArrays = new string[] { "Name", "Unit" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Ед.измр.:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 21:
						{
							str = string.Concat(str, "Объект");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 22:
						{
							str = string.Concat(str, "типа склада");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Тип склада:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 23:
						{
							str = string.Concat(str, "контрагента");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 24:
						{
							str = string.Concat(str, "нормы естественной убыли");
							strArrays = new string[] { "Quarter", "Temperature", "Value" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Квартал:", "Ср. температура:", "Значение:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 25:
						{
							str = string.Concat(str, "зависимости пл-ти ЖФ от тем-ры");
							strArrays = new string[] { "Temperature", "DensityPropane", "DensityButane" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Температура, С:", "Пл-ть пр.:", "Пл-ть бут.:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 26:
						{
							str = string.Concat(str, "зависимости пл-ти ПФ СУГ от состава при НУ");
							strArrays = new string[] { "Density", "Propane", "Butane" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Плотность:", "Пропан, %:", "Бутан, %:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 27:
						{
							str = string.Concat(str, "коэффициента перевода ПФ СУГ к НУ");
							strArrays = new string[] { "Pressure", "Temperature", "Factor" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Давление ПФ:", "Температура, С:", "Коэффициент:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 28:
						{
							str = string.Concat(str, "содержания пр. и бут. в ПФ СУГ в зав-ти от состава ЖФ СУГ");
							strArrays = new string[] { "Temperature", "Propane", "Butane", "ContentPropane", "ContentButane" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Температура, C:", "Пропан в ЖФ:", "Бутан в ЖФ:", "Пропан в ПФ:", "Бутан в ПФ:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 29:
						{
							str = string.Concat(str, "агента");
							strArrays = new string[] { "Name", "NumberAgent", "IdSector" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "№ агента:", "Сектор:", "Тип агента" };
							this.FullListNameLabels = strArrays;
							strArrays = new string[] { "TypeAgent" };
							this.FullListNameRefObjects = strArrays;
							break;
						}
						case 30:
						{
							str = string.Concat(str, "типа агента");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 31:
						{
							str = string.Concat(str, "типа пачки");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 32:
						{
							str = string.Concat(str, "тип причины");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 33:
						{
							str = string.Concat(str, "типа контракта");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 34:
						{
							str = string.Concat(str, "типа прибора учета");
							strArrays = new string[] { "Name", "ClassAccuracy", "CountDigital", "ServiceLife", "Memo" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Класс точности:", "Разрядность шкалы:", "Период проверки:", "Примечание:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 35:
						{
							str = string.Concat(str, "типа документа");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 36:
						{
							str = string.Concat(str, "типа операции");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 37:
						{
							str = string.Concat(str, "типа оплаты");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 38:
						{
							str = string.Concat(str, "Годовая ставка");
							strArrays = new string[] { "Name", "Date" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:", "Дата:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 39:
						{
							str = string.Concat(str, "");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
						case 40:
						{
							str = string.Concat(str, "");
							strArrays = new string[] { "Name" };
							this.FullListControls = strArrays;
							strArrays = new string[] { "Название:" };
							this.FullListNameLabels = strArrays;
							this.FullListNameRefObjects = null;
							break;
						}
					}
				}
			}
			return str;
		}

		private void lvData_DoubleClick(object sender, EventArgs e)
		{
			this.EditObject();
		}

		private void lvData_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.EditObject();
			}
		}

		private void lvData_SelectedIndexChanged(object sender, EventArgs e)
		{
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
					if ((object)str1 == (object)"Add")
					{
						this.AddObject();
					}
					else if ((object)str1 == (object)"Edit")
					{
						this.EditObject();
					}
					else if ((object)str1 == (object)"Del")
					{
						try
						{
							if (this.lvData.SelectedItems.Count > 0 && MessageBox.Show(string.Concat("Удалить ", this.lvData.SelectedItems[0].Text, "?"), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
							{
								if (this._objects.Remove(Convert.ToInt64(this.lvData.SelectedItems[0].Tag)) != 0)
								{
									MessageBox.Show(string.Concat("Не удается удалить ", this.lvData.SelectedItems[0].Text, "?"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								else
								{
									this.FillList();
								}
							}
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							MessageBox.Show(string.Concat("Ошибка ", exception.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else if ((object)str1 == (object)"Excel")
					{
						Tools.ConvertToExcel(this.lvData);
					}
				}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				MessageBox.Show(string.Concat("Ошибка ", exception2.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}