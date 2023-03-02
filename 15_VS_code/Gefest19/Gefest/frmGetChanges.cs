using C1.Win.C1Command;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WebSecurityDLL;

namespace Gefest
{
	public class frmGetChanges : Form
	{
		private Button cmdOK;

		private Label label1;

		private Button cmdOpenFile;

		private ToolTip toolTip1;

		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private TabPage tabPage2;

		private TabPage tabPage3;

		private ListView lv;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private ColumnHeader columnHeader7;

		private ColumnHeader columnHeader8;

		private ColumnHeader columnHeader9;

		private ListView lv1;

		private ColumnHeader columnHeader10;

		private ColumnHeader columnHeader11;

		private ColumnHeader columnHeader12;

		private ColumnHeader columnHeader13;

		private ColumnHeader columnHeader14;

		private ColumnHeader columnHeader15;

		private ColumnHeader columnHeader16;

		private ColumnHeader columnHeader17;

		private ColumnHeader columnHeader18;

		private ColumnHeader columnHeader19;

		private ListView lv2;

		private ColumnHeader columnHeader20;

		private ColumnHeader columnHeader22;

		private ColumnHeader columnHeader23;

		private ColumnHeader columnHeader24;

		private ColumnHeader columnHeader25;

		private ColumnHeader columnHeader26;

		private Button cmdCreateData;

		private ColumnHeader columnHeader21;

		private ColumnHeader columnHeader27;

		private ColumnHeader columnHeader28;

		private ColumnHeader columnHeader29;

		private ColumnHeader columnHeader30;

		private ColumnHeader columnHeader31;

		private ColumnHeader columnHeader32;

		private ColumnHeader columnHeader33;

		private ColumnHeader columnHeader34;

		private ColumnHeader columnHeader36;

		private ColumnHeader columnHeader37;

		private ColumnHeader columnHeader38;

		private ColumnHeader columnHeader39;

		private TabPage tabPage4;

		private ListView lv3;

		private ColumnHeader columnHeader35;

		private ColumnHeader columnHeader42;

		private ColumnHeader columnHeader43;

		private ColumnHeader columnHeader40;

		private ColumnHeader columnHeader41;

		private ColumnHeader columnHeader44;

		private int correspondent;

		private string filename;

		private Sending tmpSending;

		private System.Windows.Forms.ContextMenu contextMenu1;

		private MenuItem menuAdd;

		private MenuItem menuEdit;

		private MenuItem menuDel;

		private MenuItem menuItem4;

		private MenuItem menuPrint;

		private TypeGMeters _typegmeters;

		public frmGetChanges()
		{
			this.InitializeComponent();
		}

		private void cmdCreateData_Click(object sender, EventArgs e)
		{
			string str;
			string[] text;
			long d;
			object[] objArray;
			Sendings sending = new Sendings();
			if (sending.Load(this.tmpSending.IDCorrespondent, this.tmpSending.NumberSending) == 0 && sending.get_Count() > 0)
			{
				MessageBox.Show("Данные из этого файла уже загружались!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string str1 = " if @@error<>0 ROLLBACK TRANSACTION ";
			string str2 = "";
			int num = 0;
			foreach (ListViewItem item in this.lv3.Items)
			{
				if (!item.Checked)
				{
					continue;
				}
				"изменение";
				string lower = item.SubItems[0].Text.Trim().ToLower();
				str = lower;
				if (lower == null)
				{
					continue;
				}
				str = string.IsInterned(str);
				if ((object)str != (object)"изменение")
				{
					continue;
				}
				for (int i = 2; i <= 4; i++)
				{
					if (item.SubItems[i].BackColor == SystemColors.Info)
					{
						num++;
					}
				}
				if (num == 0)
				{
					continue;
				}
				str2 = string.Concat(str2, "update person set ");
				if (item.SubItems[2].BackColor == SystemColors.Info)
				{
					str2 = string.Concat(str2, "surname='", item.SubItems[2].Text.Trim(), "',");
				}
				if (item.SubItems[3].BackColor == SystemColors.Info)
				{
					str2 = string.Concat(str2, " name='", item.SubItems[3].Text.Trim(), "',");
				}
				if (item.SubItems[4].BackColor == SystemColors.Info)
				{
					str2 = string.Concat(str2, " patronic='", item.SubItems[4].Text.Trim(), "',");
				}
				str2 = str2.Substring(0, str2.Length - 1);
				str2 = string.Concat(str2, " where idperson=", item.SubItems[5].Text.Trim(), " ");
				str2 = string.Concat(str2, str1);
			}
			string str3 = "";
			num = 0;
			foreach (ListViewItem listViewItem in this.lv2.Items)
			{
				if (!listViewItem.Checked)
				{
					continue;
				}
				"изменение";
				string lower1 = listViewItem.SubItems[0].Text.Trim().ToLower();
				str = lower1;
				if (lower1 == null)
				{
					continue;
				}
				str = string.IsInterned(str);
				if ((object)str != (object)"добавление")
				{
					if ((object)str == (object)"изменение")
					{
						for (int j = 2; j <= 6; j++)
						{
							if (listViewItem.SubItems[j].BackColor == SystemColors.Info)
							{
								num++;
							}
						}
						if (num == 0)
						{
							continue;
						}
						str3 = string.Concat(str3, "update typegmeter set ");
						if (listViewItem.SubItems[2].BackColor == SystemColors.Info)
						{
							str3 = string.Concat(str3, "name='", listViewItem.SubItems[2].Text.Trim(), "',");
						}
						if (listViewItem.SubItems[6].BackColor == SystemColors.Info)
						{
							str3 = string.Concat(str3, " memo='", listViewItem.SubItems[6].Text.Trim(), "',");
						}
						if (listViewItem.SubItems[3].BackColor == SystemColors.Info)
						{
							str3 = string.Concat(str3, " classaccuracy=", listViewItem.SubItems[3].Text.Trim().Replace(",", "."), ",");
						}
						if (listViewItem.SubItems[4].BackColor == SystemColors.Info)
						{
							str3 = string.Concat(str3, " countdigital=", listViewItem.SubItems[4].Text.Trim(), ",");
						}
						if (listViewItem.SubItems[5].BackColor == SystemColors.Info)
						{
							str3 = string.Concat(str3, " servicelife=", listViewItem.SubItems[5].Text.Trim(), ",");
						}
						str3 = str3.Substring(0, str3.Length - 1);
						str3 = string.Concat(str3, " where idtypegmeter=");
						if (listViewItem.SubItems[1].Text.Trim() == "0")
						{
							text = new string[] { str3, "(select top 1 case when dbo.fgetconformity(3,", this.correspondent.ToString(), ",0,", listViewItem.Tag.ToString(), ") = 0 then ", listViewItem.Tag.ToString(), " else dbo.fgetconformity(3,", this.correspondent.ToString(), ",0,", listViewItem.Tag.ToString(), ") end) ", str1 };
							str3 = string.Concat(text);
						}
						else
						{
							text = new string[] { str3, listViewItem.SubItems[1].Text.Trim(), " ", str1, "update conformitytypegmeter set correspondent", this.correspondent.ToString().Trim(), "=", listViewItem.Tag.ToString(), " where correspondent3=", listViewItem.SubItems[1].Text.Trim(), " ", str1 };
							str3 = string.Concat(text);
						}
					}
				}
				else if (listViewItem.SubItems[1].Text.Trim() == "0")
				{
					text = new string[] { str3, " insert into typegmeter(name,memo,classaccuracy,countdigital,servicelife) values('", listViewItem.SubItems[2].Text.Trim(), "','", listViewItem.SubItems[6].Text.Trim(), "',", listViewItem.SubItems[3].Text.Trim().Replace(",", "."), ",", listViewItem.SubItems[4].Text.Trim(), ",", listViewItem.SubItems[5].Text.Trim(), ") ", str1, "insert into conformitytypegmeter(correspondent1,correspondent2,correspondent3) values(", null, null, null, null, null };
					text[14] = (this.correspondent == 1 ? listViewItem.Tag.ToString() : "null");
					text[15] = ",";
					text[16] = (this.correspondent == 2 ? listViewItem.Tag.ToString() : "null");
					text[17] = ",SCOPE_IDENTITY()) ";
					text[18] = str1;
					str3 = string.Concat(text);
				}
				else if (listViewItem.SubItems[1].BackColor != SystemColors.Info)
				{
					text = new string[] { str3, " update conformitytypegmeter set correspondent", this.correspondent.ToString().Trim(), "=", listViewItem.Tag.ToString(), " where correspondent3=", listViewItem.SubItems[1].Text, " ", str1 };
					str3 = string.Concat(text);
				}
				else
				{
					text = new string[] { str3, " insert into conformitytypegmeter(correspondent1,correspondent2,correspondent3) values(", null, null, null, null, null, null, null };
					text[2] = (this.correspondent == 1 ? listViewItem.Tag.ToString() : "null");
					text[3] = ",";
					text[4] = (this.correspondent == 2 ? listViewItem.Tag.ToString() : "null");
					text[5] = ",";
					text[6] = listViewItem.SubItems[1].Text;
					text[7] = ") ";
					text[8] = str1;
					str3 = string.Concat(text);
				}
			}
			string str4 = "";
			num = 0;
			foreach (ListViewItem item1 in this.lv1.Items)
			{
				if (!item1.Checked)
				{
					continue;
				}
				"удаление";
				string lower2 = item1.SubItems[0].Text.Trim().ToLower();
				str = lower2;
				if (lower2 == null)
				{
					continue;
				}
				str = string.IsInterned(str);
				if ((object)str == (object)"добавление")
				{
					if (item1.SubItems[1].Text.Trim() != "0")
					{
						text = new string[] { str4, " update conformitygmeter set correspondent", this.correspondent.ToString().Trim(), "=", item1.Tag.ToString(), " where correspondent3=", item1.SubItems[1].Text, " ", str1 };
						str4 = string.Concat(text);
					}
					else
					{
						Contract contract = new Contract();
						if (contract.Load(Convert.ToInt64(item1.SubItems[13].Text.Trim())) == 0)
						{
							d = contract.oGobjects[0].get_ID();
							string str5 = d.ToString();
							text = new string[] { str4, " insert into gmeter(IDStatusGMeter, IDTypeGMeter, SerialNumber, BeginValue, DateInstall, DateVerify, Memo, DateFabrication, IDGObject, IDTypeVerify) values(", item1.SubItems[12].Text.Trim(), " , case when dbo.fgetconformity(3,", this.correspondent.ToString(), ",0,", item1.SubItems[13].Text.Trim(), ") = 0 then ", item1.SubItems[13].Text.Trim(), " else dbo.fgetconformity(3,", this.correspondent.ToString(), ",0,", item1.SubItems[13].Text.Trim(), ") end ,'", item1.SubItems[4].Text.Trim(), "',", item1.SubItems[5].Text.Trim().Replace(",", "."), ",", Tools.ConvertDateFORSQL(Convert.ToDateTime(item1.SubItems[6].Text.Trim())), ",", Tools.ConvertDateFORSQL(Convert.ToDateTime(item1.SubItems[7].Text.Trim())), ",'", item1.SubItems[10].Text.Trim(), "',", Tools.ConvertDateFORSQL(Convert.ToDateTime(item1.SubItems[11].Text.Trim())), ",", str5, ",", item1.SubItems[15].Text.Trim(), ") ", str1, "insert into conformitygmeter(correspondent1,correspondent2,correspondent3) values(", null, null, null, null, null };
							text[32] = (this.correspondent == 1 ? item1.Tag.ToString() : "null");
							text[33] = ",";
							text[34] = (this.correspondent == 2 ? item1.Tag.ToString() : "null");
							text[35] = ",SCOPE_IDENTITY()) ";
							text[36] = str1;
							str4 = string.Concat(text);
						}
						else
						{
							MessageBox.Show("Ошибка при подготовке загрузки данных!");
							return;
						}
					}
				}
				else if ((object)str == (object)"изменение")
				{
					num = 0;
					for (int k = 3; k <= 11; k++)
					{
						if (item1.SubItems[k].BackColor == SystemColors.Info)
						{
							num++;
						}
					}
					if (num == 0)
					{
						continue;
					}
					str4 = string.Concat(str4, "update gmeter set ");
					if (item1.SubItems[9].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, "idstatusgmeter=", item1.SubItems[12].Text.Trim(), ",");
					}
					if (item1.SubItems[3].BackColor == SystemColors.Info)
					{
						text = new string[] { str4, " idtypegmeter=case when dbo.fgetconformity(3,", this.correspondent.ToString(), ",0,", item1.SubItems[13].Text.Trim(), ") = 0 then ", item1.SubItems[13].Text.Trim(), " else dbo.fgetconformity(3,", this.correspondent.ToString(), ",0,", item1.SubItems[13].Text.Trim(), ") end ," };
						str4 = string.Concat(text);
					}
					if (item1.SubItems[4].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, " serialnumber='", item1.SubItems[4].Text.Trim(), "',");
					}
					if (item1.SubItems[5].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, " beginvalue=", item1.SubItems[5].Text.Trim().Replace(",", "."), ",");
					}
					if (item1.SubItems[6].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, " dateinstall=", Tools.ConvertDateFORSQL(Convert.ToDateTime(item1.SubItems[6].Text.Trim())), ",");
					}
					if (item1.SubItems[7].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, " dateverify=", Tools.ConvertDateFORSQL(Convert.ToDateTime(item1.SubItems[7].Text.Trim())), ",");
					}
					if (item1.SubItems[10].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, " memo='", item1.SubItems[10].Text.Trim(), "',");
					}
					if (item1.SubItems[11].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, " datefabrication=", Tools.ConvertDateFORSQL(Convert.ToDateTime(item1.SubItems[11].Text.Trim())), ",");
					}
					if (item1.SubItems[8].BackColor == SystemColors.Info)
					{
						str4 = string.Concat(str4, " idtypeverify=", item1.SubItems[15].Text.Trim(), ",");
					}
					str4 = str4.Substring(0, str4.Length - 1);
					str4 = string.Concat(str4, " where idgmeter=");
					if (item1.SubItems[1].Text.Trim() == "0")
					{
						text = new string[] { str4, "(select top 1 case when dbo.fgetconformity(2,", this.correspondent.ToString(), ",0,", item1.Tag.ToString(), ") = 0 then ", item1.Tag.ToString(), " else dbo.fgetconformity(2,", this.correspondent.ToString(), ",0,", item1.Tag.ToString(), ") end) ", str1 };
						str4 = string.Concat(text);
					}
					else
					{
						text = new string[] { str4, item1.SubItems[1].Text.Trim(), " ", str1, "update conformitygmeter set correspondent", this.correspondent.ToString().Trim(), "=", item1.Tag.ToString(), " where correspondent3=", item1.SubItems[1].Text.Trim(), " ", str1 };
						str4 = string.Concat(text);
					}
				}
				else if ((object)str == (object)"удаление")
				{
					str4 = string.Concat(str4, " delete gmeter where idgmeter=");
					if (item1.SubItems[1].Text.Trim() == "0")
					{
						str4 = string.Concat(str4, item1.Tag.ToString(), " ", str1);
					}
					else
					{
						text = new string[] { str4, item1.SubItems[1].Text.Trim(), " ", str1, "update conformitygmeter set correspondent", this.correspondent.ToString().Trim(), "=null where correspondent3=", item1.SubItems[1].Text.Trim(), " ", str1 };
						str4 = string.Concat(text);
					}
				}
			}
			string str6 = "declare @iddoc as int declare @idgmeter as int declare @dateverify as datetime ";
			num = 0;
			foreach (ListViewItem listViewItem1 in this.lv.Items)
			{
				if (!listViewItem1.Checked || !(listViewItem1.SubItems[9].Text.Trim().ToLower() == "22"))
				{
					continue;
				}
				"удаление";
				string lower3 = listViewItem1.SubItems[0].Text.Trim().ToLower();
				str = lower3;
				if (lower3 == null)
				{
					continue;
				}
				str = string.IsInterned(str);
				if ((object)str == (object)"добавление")
				{
					objArray = new object[] { str6, " insert into document(IDContract, IDPeriod, IDBatch, IDTypeDocument, DocumentNumber, DocumentDate, DocumentAmount, IDGru, IdUser, DateAdd, IdModify, DateModify, Note) values(", listViewItem1.SubItems[10].Text.Trim(), ",", Depot.CurrentPeriod.get_ID(), ", null,", listViewItem1.SubItems[9].Text.Trim(), ", '", listViewItem1.SubItems[4].Text.Trim(), "' ,", Tools.ConvertDateFORSQL(Convert.ToDateTime(listViewItem1.SubItems[5].Text.Trim())), ",", listViewItem1.SubItems[6].Text.Trim().Replace(",", "."), ",null, null, null, null, null, '", listViewItem1.SubItems[7].Text.Trim(), "') ", str1, "set @iddoc=SCOPE_IDENTITY() insert into pd(IDTypePD, IDDocument, Value) values(30,@iddoc,'", listViewItem1.SubItems[11].Text.Trim(), "') ", str1, "insert into pd(IDTypePD, IDDocument, Value) values(27,@iddoc,'", listViewItem1.SubItems[12].Text.Trim().Replace(",", "."), "') ", str1, "insert into pd(IDTypePD, IDDocument, Value) values(32,@iddoc,'", listViewItem1.SubItems[13].Text.Trim(), "') ", str1, "insert into pd(IDTypePD, IDDocument, Value) values(7,@iddoc,'", listViewItem1.SubItems[14].Text.Trim(), "') ", str1, "insert into pd(IDTypePD, IDDocument, Value) values(16,@iddoc,'", null, null, null, null, null, null, null, null, null };
					objArray[34] = (this.correspondent == 1 ? "98" : "94");
					objArray[35] = "') ";
					objArray[36] = str1;
					objArray[37] = "insert into conformitydocument(correspondent1,correspondent2,correspondent3) values(";
					objArray[38] = (this.correspondent == 1 ? listViewItem1.Tag.ToString() : "null");
					objArray[39] = ",";
					objArray[40] = (this.correspondent == 2 ? listViewItem1.Tag.ToString() : "null");
					objArray[41] = ",@iddoc) ";
					objArray[42] = str1;
					str6 = string.Concat(objArray);
				}
				else if ((object)str == (object)"изменение")
				{
					text = new string[33];
					text[0] = str6;
					text[1] = "update document set IDContract=";
					text[2] = listViewItem1.SubItems[10].Text.Trim();
					text[3] = ", IDPeriod=";
					d = Depot.CurrentPeriod.get_ID();
					text[4] = d.ToString();
					text[5] = ", IDBatch=null, IDTypeDocument=";
					text[6] = listViewItem1.SubItems[9].Text.Trim();
					text[7] = ", DocumentNumber='";
					text[8] = listViewItem1.SubItems[4].Text.Trim();
					text[9] = "', DocumentDate=";
					text[10] = Tools.ConvertDateFORSQL(Convert.ToDateTime(listViewItem1.SubItems[5].Text.Trim()));
					text[11] = ", DocumentAmount=";
					text[12] = listViewItem1.SubItems[6].Text.Trim().Replace(",", ".");
					text[13] = ", Note='";
					text[14] = listViewItem1.SubItems[7].Text.Trim();
					text[15] = "' where iddocument=(select top 1 dbo.fgetconformity(1,";
					text[16] = this.correspondent.ToString();
					text[17] = ",0,";
					text[18] = listViewItem1.Tag.ToString();
					text[19] = ") ) ";
					text[20] = str1;
					text[21] = "set @iddoc=(select top 1 dbo.fgetconformity(1,";
					text[22] = this.correspondent.ToString();
					text[23] = ",0,";
					text[24] = listViewItem1.Tag.ToString();
					text[25] = ") ) update pd set value='";
					text[26] = listViewItem1.SubItems[11].Text.Trim();
					text[27] = "' where idtypepd=30 and iddocument=@iddoc ";
					text[28] = str1;
					text[29] = "update pd set value='";
					text[30] = listViewItem1.SubItems[12].Text.Trim().Replace(",", ".");
					text[31] = "' where idtypepd=27 and iddocument=@iddoc ";
					text[32] = str1;
					str6 = string.Concat(text);
				}
				else if ((object)str == (object)"удаление")
				{
					text = new string[] { str6, "set @iddoc=(select top 1 dbo.fgetconformity(1,", this.correspondent.ToString(), ",0,", listViewItem1.Tag.ToString(), ") ) ", str1, "set @idgmeter=(select value from pd where iddocument=@iddoc and idtypepd=7) ", str1, "set @dateverify=(select convert(datetime,value,104) from pd where iddocument=@iddoc and idtypepd=32) ", str1, "update gmeter set idtypeverify=1, DateVerify=@dateverify where idgmeter=@idgmeter ", str1, "delete document where iddocument=@iddoc ", str1, "update conformitydocument set correspondent", this.correspondent.ToString().Trim(), "=null where correspondent3=@iddoc ", str1 };
					str6 = string.Concat(text);
				}
			}
			objArray = new object[] { "BEGIN TRANSACTION insert into sending(DateSending,IDCorrespondent,NumberSending) values(", Tools.ConvertDateFORSQL(this.tmpSending.DateSending), ",", this.tmpSending.IDCorrespondent, ",", this.tmpSending.NumberSending, ") ", str1, str2, str3, str4, str6, " COMMIT TRANSACTION " };
			string str7 = string.Concat(objArray);
			using (StreamWriter streamWriter = new StreamWriter("C:\\Gefest.txt", true))
			{
				streamWriter.WriteLine(str7);
			}
			if (Saver.ExecuteQuery(str7, 0))
			{
				this.filename = null;
				MessageBox.Show("Загрузка данных прошла успешно!");
				return;
			}
			MessageBox.Show("Ошибка загрузки данных!");
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			Contract contract;
			Gmeter gmeter;
			TypeGMeter typeGMeter;
			string[] str;
			int dCorrespondent;
			DateTime dateTime;
			double documentAmount;
			long d;
			StreamReader streamReader = null;
			try
			{
				try
				{
					string str1 = "";
					this.lv.Items.Clear();
					this.lv1.Items.Clear();
					this.lv2.Items.Clear();
					this.lv3.Items.Clear();
					this.tmpSending = new Sending();
					Encoding encoding = Encoding.GetEncoding(1251);
					streamReader = new StreamReader(this.filename, encoding);
					while (streamReader.Peek() != -1)
					{
						string str2 = streamReader.ReadLine();
						byte[] numArray = new byte[] { 182 };
						string[] strArrays = str2.Split(encoding.GetChars(numArray));
						if ((int)strArrays.Length < 4)
						{
							MessageBox.Show("Ошибка импорта!");
							return;
						}
						else
						{
							if (strArrays[0] == "1")
							{
								if (!(strArrays[2] != "1") || !(strArrays[2] != "2"))
								{
									this.correspondent = Convert.ToInt32(strArrays[2]);
									this.tmpSending.IDCorrespondent = Convert.ToInt32(strArrays[2]);
									this.tmpSending.DateSending = Convert.ToDateTime(strArrays[1]);
									this.tmpSending.NumberSending = Convert.ToInt32(strArrays[3]);
								}
								else
								{
									MessageBox.Show("Данные из этого файла не могут быть импортированы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									return;
								}
							}
							if (strArrays[0] != "2")
							{
								continue;
							}
							"contract";
							string lower = strArrays[2].ToLower();
							string str3 = lower;
							if (lower == null)
							{
								continue;
							}
							str3 = string.IsInterned(str3);
							if ((object)str3 == (object)"document")
							{
								if (strArrays[1].Trim() == "1" || strArrays[1].Trim() == "2")
								{
									long num = (long)0;
									string str4 = "";
									ListViewItem listViewItem = new ListViewItem(Depot.oTypeActions.item(Convert.ToInt64(strArrays[1])).get_Name())
									{
										Tag = strArrays[3]
									};
									if (strArrays[6] == "1")
									{
										listViewItem.Checked = true;
										num = (long)22;
										str4 = "ПУ проверку прошел? = ";
										str4 = (strArrays[11] != "1" ? string.Concat(str4, "НЕТ;") : string.Concat(str4, "ДА;"));
										str4 = string.Concat(str4, "Кон. показание = ", strArrays[12]);
									}
									if (strArrays[6] == "2")
									{
										num = (long)18;
									}
									if (strArrays[6] == "3")
									{
										num = (long)12;
										str4 = "Подключается новый ПУ? = ";
										str4 = (strArrays[11] != "1" ? string.Concat(str4, "НЕТ;") : string.Concat(str4, "ДА;"));
									}
									listViewItem.SubItems.Add(Depot.oTypeDocuments.item(num).get_Name());
									contract = new Contract();
									contract.Load(Convert.ToInt64(strArrays[4]));
									listViewItem.SubItems.Add(contract.Account);
									str1 = string.Concat(str1, Convert.ToString(contract.get_ID()), ",");
									if (this.tmpSending.IDCorrespondent == 0)
									{
										return;
									}
									else
									{
										str = new string[] { "2", null, null, null };
										dCorrespondent = this.tmpSending.IDCorrespondent;
										str[1] = dCorrespondent.ToString();
										str[2] = "0";
										str[3] = strArrays[5];
										object obj = Saver.ExecuteFunction("fGetConformity", str);
										if (obj != null)
										{
											string str5 = "";
											str5 = (obj.ToString() != "0" ? obj.ToString() : strArrays[5]);
											gmeter = new Gmeter();
											if (gmeter.Load(Convert.ToInt64(str5)) != 0)
											{
												listViewItem.SubItems.Add("н.у.");
											}
											else
											{
												listViewItem.SubItems.Add(gmeter.SerialNumber);
											}
											listViewItem.SubItems.Add(strArrays[7]);
											ListViewItem.ListViewSubItemCollection subItems = listViewItem.SubItems;
											dateTime = Convert.ToDateTime(strArrays[8]);
											subItems.Add(dateTime.ToShortDateString());
											listViewItem.SubItems.Add(strArrays[9]);
											listViewItem.SubItems.Add(strArrays[10]);
											listViewItem.SubItems.Add(str4);
											listViewItem.SubItems.Add(num.ToString());
											listViewItem.SubItems.Add(strArrays[4]);
											listViewItem.SubItems.Add(strArrays[11]);
											if ((int)strArrays.Length <= 12)
											{
												listViewItem.SubItems.Add("");
											}
											else
											{
												listViewItem.SubItems.Add(strArrays[12]);
											}
											if ((int)strArrays.Length <= 13)
											{
												listViewItem.SubItems.Add("");
											}
											else
											{
												listViewItem.SubItems.Add(strArrays[13]);
											}
											listViewItem.SubItems.Add(str5);
											this.lv.Items.Add(listViewItem);
										}
										else
										{
											MessageBox.Show("Ошибка импорта!");
											return;
										}
									}
								}
								else
								{
									ListViewItem listViewItem1 = new ListViewItem(Depot.oTypeActions.item(Convert.ToInt64(strArrays[1])).get_Name())
									{
										Tag = strArrays[3]
									};
									if (this.tmpSending.IDCorrespondent == 0)
									{
										return;
									}
									else
									{
										str = new string[] { "1", null, null, null };
										dCorrespondent = this.tmpSending.IDCorrespondent;
										str[1] = dCorrespondent.ToString();
										str[2] = "0";
										str[3] = strArrays[3];
										object obj1 = Saver.ExecuteFunction("fGetConformity", str);
										if (obj1 != null)
										{
											string str6 = "";
											str6 = (obj1.ToString() != "0" ? obj1.ToString() : strArrays[3]);
											Document document = new Document();
											if (document.Load(Convert.ToInt64(str6)) != 0)
											{
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
												listViewItem1.SubItems.Add("н.у.");
											}
											else
											{
												listViewItem1.SubItems.Add(document.oTypeDocument.get_Name());
												listViewItem1.SubItems.Add(document.oContract.Account);
												gmeter = new Gmeter();
												gmeter.Load(Convert.ToInt64(document.GetNamePD(7)));
												listViewItem1.SubItems.Add(gmeter.SerialNumber);
												listViewItem1.SubItems.Add(document.DocumentNumber);
												ListViewItem.ListViewSubItemCollection listViewSubItemCollections = listViewItem1.SubItems;
												dateTime = document.DocumentDate;
												listViewSubItemCollections.Add(dateTime.ToShortDateString());
												ListViewItem.ListViewSubItemCollection subItems1 = listViewItem1.SubItems;
												documentAmount = document.DocumentAmount;
												subItems1.Add(documentAmount.ToString());
												listViewItem1.SubItems.Add(document.Note);
												listViewItem1.SubItems.Add("");
												ListViewItem.ListViewSubItemCollection listViewSubItemCollections1 = listViewItem1.SubItems;
												d = document.oTypeDocument.get_ID();
												listViewSubItemCollections1.Add(d.ToString());
												ListViewItem.ListViewSubItemCollection subItems2 = listViewItem1.SubItems;
												d = document.oContract.get_ID();
												subItems2.Add(d.ToString());
												listViewItem1.SubItems.Add("");
												listViewItem1.SubItems.Add("");
												listViewItem1.SubItems.Add("");
												listViewItem1.SubItems.Add("");
												if (document.oTypeDocument.get_ID() == (long)22)
												{
													listViewItem1.Checked = true;
												}
											}
											this.lv.Items.Add(listViewItem1);
										}
										else
										{
											MessageBox.Show("Ошибка импорта!");
											return;
										}
									}
								}
							}
							else if ((object)str3 == (object)"gmeter")
							{
								if (strArrays[1].Trim() == "1" || strArrays[1].Trim() == "2")
								{
									ListViewItem red = new ListViewItem(Depot.oTypeActions.item(Convert.ToInt64(strArrays[1])).get_Name())
									{
										UseItemStyleForSubItems = false,
										Tag = strArrays[3]
									};
									red.SubItems.Add(strArrays[4]);
									contract = new Contract();
									contract.Load(Convert.ToInt64(strArrays[13]));
									red.SubItems.Add(contract.Account);
									if (this.tmpSending.IDCorrespondent == 0)
									{
										return;
									}
									else
									{
										str = new string[] { "3", null, null, null };
										dCorrespondent = this.tmpSending.IDCorrespondent;
										str[1] = dCorrespondent.ToString();
										str[2] = "0";
										str[3] = strArrays[6];
										object obj2 = Saver.ExecuteFunction("fGetConformity", str);
										if (obj2 != null)
										{
											string str7 = "";
											str7 = (obj2.ToString() != "0" ? obj2.ToString() : strArrays[6]);
											typeGMeter = new TypeGMeter();
											if (typeGMeter.Load(Convert.ToInt64(str7)) != 0)
											{
												red.SubItems.Add("н.у.");
											}
											else
											{
												red.SubItems.Add(typeGMeter.Fullname);
											}
											red.SubItems.Add(strArrays[7]);
											red.SubItems.Add(strArrays[8]);
											ListViewItem.ListViewSubItemCollection listViewSubItemCollections2 = red.SubItems;
											dateTime = Convert.ToDateTime(strArrays[9]);
											listViewSubItemCollections2.Add(dateTime.ToShortDateString());
											ListViewItem.ListViewSubItemCollection subItems3 = red.SubItems;
											dateTime = Convert.ToDateTime(strArrays[10]);
											subItems3.Add(dateTime.ToShortDateString());
											red.SubItems.Add(Depot.oTypeVerifys.item(Convert.ToInt64(strArrays[14])).get_Name());
											red.SubItems.Add(Depot.oStatusGMeters.item(Convert.ToInt64(strArrays[5])).get_Name());
											red.SubItems.Add(strArrays[11]);
											red.SubItems.Add(strArrays[12]);
											red.SubItems.Add(strArrays[5]);
											red.SubItems.Add(strArrays[6]);
											red.SubItems.Add(strArrays[13]);
											red.SubItems.Add(strArrays[14]);
											try
											{
												red.Checked = false;
												if (strArrays[1].Trim() == "2" && red.SubItems[1].Text.Trim() != "0")
												{
													if (contract.oGobjects[0].oGmeters.item(Convert.ToInt64(red.SubItems[1].Text)).oTypeGMeter.get_ID().ToString().Trim() != red.SubItems[13].Text.Trim())
													{
														red.SubItems[3].BackColor = Color.Red;
													}
													else
													{
														red.SubItems[3].BackColor = SystemColors.Info;
													}
													if (contract.oGobjects[0].oGmeters.item(Convert.ToInt64(red.SubItems[1].Text)).SerialNumber.Trim() != red.SubItems[4].Text.Trim())
													{
														red.SubItems[4].BackColor = Color.Red;
													}
													else
													{
														red.SubItems[4].BackColor = SystemColors.Info;
													}
													if (contract.oGobjects[0].oGmeters.item(Convert.ToInt64(red.SubItems[1].Text)).BeginValue.ToString().Trim() != red.SubItems[5].Text.Trim())
													{
														red.SubItems[5].BackColor = Color.Red;
													}
													else
													{
														red.SubItems[5].BackColor = SystemColors.Info;
													}
													if (contract.oGobjects[0].oGmeters.item(Convert.ToInt64(red.SubItems[1].Text)).DateInstall.ToShortDateString().Trim() != red.SubItems[6].Text.Trim())
													{
														red.SubItems[6].BackColor = Color.Red;
													}
													else
													{
														red.SubItems[6].BackColor = SystemColors.Info;
													}
													if (Math.Abs((contract.oGobjects[0].oGmeters.item(Convert.ToInt64(red.SubItems[1].Text)).DateVerify - Convert.ToDateTime(red.SubItems[7].Text.Trim())).TotalDays) <= 30)
													{
														red.SubItems[7].BackColor = Color.Red;
													}
													else
													{
														red.SubItems[7].BackColor = SystemColors.Info;
													}
													red.SubItems[8].BackColor = SystemColors.Info;
													if (contract.oGobjects[0].oGmeters.item(Convert.ToInt64(red.SubItems[1].Text)).Memo.ToString().Trim() != red.SubItems[10].Text.Trim())
													{
														red.SubItems[10].BackColor = Color.Red;
													}
													else
													{
														red.SubItems[10].BackColor = SystemColors.Info;
													}
													if (contract.oGobjects[0].oGmeters.item(Convert.ToInt64(red.SubItems[1].Text)).DateFabrication.ToShortDateString().Trim() != red.SubItems[11].Text.Trim())
													{
														red.SubItems[11].BackColor = Color.Red;
													}
													else
													{
														red.SubItems[11].BackColor = SystemColors.Info;
													}
													red.Checked = true;
												}
												this.lv1.Items.Add(red);
											}
											catch
											{
											}
										}
										else
										{
											MessageBox.Show("Ошибка импорта!");
											return;
										}
									}
								}
								else
								{
									ListViewItem listViewItem2 = new ListViewItem(Depot.oTypeActions.item(Convert.ToInt64(strArrays[1])).get_Name())
									{
										Tag = strArrays[3]
									};
									if (this.tmpSending.IDCorrespondent == 0)
									{
										return;
									}
									else
									{
										str = new string[] { "2", null, null, null };
										dCorrespondent = this.tmpSending.IDCorrespondent;
										str[1] = dCorrespondent.ToString();
										str[2] = "0";
										str[3] = strArrays[3];
										object obj4 = Saver.ExecuteFunction("fGetConformity", str);
										if (obj4 != null)
										{
											string str8 = "";
											str8 = (obj4.ToString() != "0" ? obj4.ToString() : strArrays[3]);
											gmeter = new Gmeter();
											listViewItem2.SubItems.Add(str8);
											if (gmeter.Load(Convert.ToInt64(str8)) != 0)
											{
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
												listViewItem2.SubItems.Add("н.у.");
											}
											else
											{
												listViewItem2.SubItems.Add(gmeter.oGobject.oContract.Account);
												listViewItem2.SubItems.Add(gmeter.oTypeGMeter.Fullname);
												listViewItem2.SubItems.Add(gmeter.SerialNumber);
												ListViewItem.ListViewSubItemCollection listViewSubItemCollections3 = listViewItem2.SubItems;
												documentAmount = gmeter.BeginValue;
												listViewSubItemCollections3.Add(documentAmount.ToString());
												ListViewItem.ListViewSubItemCollection subItems4 = listViewItem2.SubItems;
												dateTime = gmeter.DateInstall;
												subItems4.Add(dateTime.ToShortDateString());
												ListViewItem.ListViewSubItemCollection listViewSubItemCollections4 = listViewItem2.SubItems;
												dateTime = gmeter.DateVerify;
												listViewSubItemCollections4.Add(dateTime.ToShortDateString());
												listViewItem2.SubItems.Add(gmeter.oTypeVerify.get_Name());
												listViewItem2.SubItems.Add(gmeter.oStatusGMeter.get_Name());
												listViewItem2.SubItems.Add(gmeter.Memo);
												ListViewItem.ListViewSubItemCollection subItems5 = listViewItem2.SubItems;
												dateTime = gmeter.DateFabrication;
												subItems5.Add(dateTime.ToShortDateString());
												ListViewItem.ListViewSubItemCollection listViewSubItemCollections5 = listViewItem2.SubItems;
												d = gmeter.oStatusGMeter.get_ID();
												listViewSubItemCollections5.Add(d.ToString());
												ListViewItem.ListViewSubItemCollection subItems6 = listViewItem2.SubItems;
												d = gmeter.oTypeGMeter.get_ID();
												subItems6.Add(d.ToString());
												ListViewItem.ListViewSubItemCollection listViewSubItemCollections6 = listViewItem2.SubItems;
												d = gmeter.oGobject.oContract.get_ID();
												listViewSubItemCollections6.Add(d.ToString());
												ListViewItem.ListViewSubItemCollection subItems7 = listViewItem2.SubItems;
												d = gmeter.oTypeVerify.get_ID();
												subItems7.Add(d.ToString());
												listViewItem2.Checked = true;
											}
											this.lv1.Items.Add(listViewItem2);
										}
										else
										{
											MessageBox.Show("Ошибка импорта!");
											return;
										}
									}
								}
							}
							else if ((object)str3 == (object)"typegmeter")
							{
								if (!(strArrays[1].Trim() == "1") && !(strArrays[1].Trim() == "2"))
								{
									continue;
								}
								ListViewItem info = new ListViewItem(Depot.oTypeActions.item(Convert.ToInt64(strArrays[1])).get_Name())
								{
									UseItemStyleForSubItems = false,
									Tag = strArrays[3]
								};
								info.SubItems.Add(strArrays[4]);
								info.SubItems.Add(strArrays[5]);
								info.SubItems.Add(strArrays[7]);
								info.SubItems.Add(strArrays[8]);
								info.SubItems.Add(strArrays[9]);
								info.SubItems.Add(strArrays[6]);
								if (strArrays[1].Trim() == "2" && info.SubItems[1].Text.Trim() != "0")
								{
									typeGMeter = new TypeGMeter();
									typeGMeter.Load(Convert.ToInt64(info.SubItems[1].Text.Trim()));
									if (typeGMeter.get_Name().Trim() != info.SubItems[2].Text.Trim())
									{
										info.SubItems[2].BackColor = Color.Red;
									}
									else
									{
										info.SubItems[2].BackColor = SystemColors.Info;
									}
									if (typeGMeter.ClassAccuracy.ToString().Trim() != info.SubItems[3].Text.Trim())
									{
										info.SubItems[3].BackColor = Color.Red;
									}
									else
									{
										info.SubItems[3].BackColor = SystemColors.Info;
									}
									if (typeGMeter.CountDigital.ToString().Trim() != info.SubItems[4].Text.Trim())
									{
										info.SubItems[4].BackColor = Color.Red;
									}
									else
									{
										info.SubItems[4].BackColor = SystemColors.Info;
									}
									if (typeGMeter.ServiceLife.ToString().Trim() != info.SubItems[5].Text.Trim())
									{
										info.SubItems[5].BackColor = Color.Red;
									}
									else
									{
										info.SubItems[5].BackColor = SystemColors.Info;
									}
									if (typeGMeter.Memo.ToString().Trim() != info.SubItems[6].Text.Trim())
									{
										info.SubItems[6].BackColor = Color.Red;
									}
									else
									{
										info.SubItems[6].BackColor = SystemColors.Info;
									}
								}
								info.Checked = true;
								this.lv2.Items.Add(info);
							}
							else if ((object)str3 == (object)"contract")
							{
								if (strArrays[1].Trim() != "2")
								{
									continue;
								}
								ListViewItem listViewItem3 = new ListViewItem(Depot.oTypeActions.item(Convert.ToInt64(strArrays[1])).get_Name())
								{
									UseItemStyleForSubItems = false,
									Tag = strArrays[3]
								};
								listViewItem3.SubItems.Add(strArrays[4]);
								listViewItem3.SubItems.Add(strArrays[5]);
								listViewItem3.SubItems.Add(strArrays[6]);
								listViewItem3.SubItems.Add(strArrays[7]);
								listViewItem3.SubItems.Add(strArrays[8]);
								this.lv3.Items.Add(listViewItem3);
							}
						}
					}
					streamReader.Close();
					this.cmdCreateData.Enabled = true;
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageBox.Show(string.Concat("Ошибка загрузки!", exception.Message), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
		}

		private void cmdOpenFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "Текстовый файл (*.txt)|*.txt"
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.label1.Text = openFileDialog.FileName;
				this.filename = openFileDialog.FileName;
			}
			if (this.filename != null)
			{
				this.cmdOK.Enabled = true;
				return;
			}
			this.cmdOK.Enabled = false;
			this.cmdCreateData.Enabled = false;
		}

		private void CompareContract()
		{
			if (this.lv3.SelectedItems.Count > 0)
			{
				if (this.lv3.SelectedItems[0].SubItems[0].Text.Trim().ToLower() != "изменение")
				{
					return;
				}
				Contract contract = new Contract();
				if (contract.Load(Convert.ToInt64(this.lv3.SelectedItems[0].Tag.ToString())) != 0)
				{
					return;
				}
				int[] numArray = new int[3];
				for (int i = 2; i <= 4; i++)
				{
					if (this.lv3.SelectedItems[0].SubItems[i].BackColor != SystemColors.Info)
					{
						numArray[i - 2] = 0;
					}
					else
					{
						numArray[i - 2] = 1;
					}
				}
				if ((new frmComparisonContract(contract, numArray)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					for (int j = 2; j <= 4; j++)
					{
						if (numArray[j - 2] != 1)
						{
							this.lv3.SelectedItems[0].SubItems[j].BackColor = SystemColors.Window;
						}
						else
						{
							this.lv3.SelectedItems[0].SubItems[j].BackColor = SystemColors.Info;
						}
					}
				}
				contract = null;
				base.Focus();
			}
		}

		private void CompareGMeter()
		{
			if (this.lv1.SelectedItems.Count > 0)
			{
				if (this.lv1.SelectedItems[0].SubItems[0].Text.Trim().ToLower() != "изменение")
				{
					return;
				}
				if (this.lv1.SelectedItems[0].SubItems[1].Text.Trim() == "0")
				{
					return;
				}
				Gmeter gmeter = new Gmeter();
				if (gmeter.Load(Convert.ToInt64(this.lv1.SelectedItems[0].SubItems[1].Text)) != 0)
				{
					return;
				}
				int[] numArray = new int[9];
				for (int i = 3; i <= 11; i++)
				{
					if (this.lv1.SelectedItems[0].SubItems[i].BackColor != SystemColors.Info)
					{
						numArray[i - 3] = 0;
					}
					else
					{
						numArray[i - 3] = 1;
					}
				}
				if ((new frmComparisonGMeter(gmeter, numArray)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					for (int j = 3; j <= 11; j++)
					{
						if (numArray[j - 3] != 1)
						{
							this.lv1.SelectedItems[0].SubItems[j].BackColor = SystemColors.Window;
						}
						else
						{
							this.lv1.SelectedItems[0].SubItems[j].BackColor = SystemColors.Info;
						}
					}
				}
				gmeter = null;
				base.Focus();
			}
		}

		private void CompareTypeGMeter()
		{
			if (this.lv2.SelectedItems.Count > 0)
			{
				if (this.lv2.SelectedItems[0].SubItems[0].Text.Trim().ToLower() == "добавление")
				{
					frmSetConformityTypeGMeter _frmSetConformityTypeGMeter = new frmSetConformityTypeGMeter();
					int num = 0;
					if (_frmSetConformityTypeGMeter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						_frmSetConformityTypeGMeter.GetData(ref num);
						this.lv2.SelectedItems[0].SubItems[1].Text = num.ToString();
						if (num == 0)
						{
							this.lv2.SelectedItems[0].SubItems[1].BackColor = SystemColors.Window;
						}
						else
						{
							this.lv2.SelectedItems[0].SubItems[1].BackColor = SystemColors.Info;
						}
					}
					_frmSetConformityTypeGMeter = null;
				}
				if (this.lv2.SelectedItems[0].SubItems[0].Text.Trim().ToLower() != "изменение")
				{
					return;
				}
				TypeGMeter typeGMeter = null;
				if (this.lv2.SelectedItems[0].SubItems[1].Text.Trim() == "0")
				{
					return;
				}
				typeGMeter = this._typegmeters.item(Convert.ToInt64(this.lv2.SelectedItems[0].SubItems[1].Text));
				if (typeGMeter == null)
				{
					return;
				}
				int[] numArray = new int[5];
				for (int i = 2; i <= 6; i++)
				{
					if (this.lv2.SelectedItems[0].SubItems[i].BackColor != SystemColors.Info)
					{
						numArray[i - 2] = 0;
					}
					else
					{
						numArray[i - 2] = 1;
					}
				}
				if ((new frmComparisonTypeGMeter(typeGMeter, numArray)).ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					for (int j = 2; j <= 6; j++)
					{
						if (numArray[j - 2] != 1)
						{
							this.lv2.SelectedItems[0].SubItems[j].BackColor = SystemColors.Window;
						}
						else
						{
							this.lv2.SelectedItems[0].SubItems[j].BackColor = SystemColors.Info;
						}
					}
				}
				typeGMeter = null;
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

		private void frmGetChanges_Load(object sender, EventArgs e)
		{
			if (this.filename == null)
			{
				this.cmdOK.Enabled = false;
				this.cmdCreateData.Enabled = false;
			}
			this._typegmeters = new TypeGMeters();
			this._typegmeters.Load();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.cmdOK = new Button();
			this.label1 = new Label();
			this.cmdOpenFile = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.lv = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader28 = new ColumnHeader();
			this.columnHeader32 = new ColumnHeader();
			this.columnHeader33 = new ColumnHeader();
			this.columnHeader34 = new ColumnHeader();
			this.columnHeader36 = new ColumnHeader();
			this.columnHeader37 = new ColumnHeader();
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.tabPage2 = new TabPage();
			this.lv1 = new ListView();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader27 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.columnHeader12 = new ColumnHeader();
			this.columnHeader13 = new ColumnHeader();
			this.columnHeader14 = new ColumnHeader();
			this.columnHeader15 = new ColumnHeader();
			this.columnHeader16 = new ColumnHeader();
			this.columnHeader38 = new ColumnHeader();
			this.columnHeader19 = new ColumnHeader();
			this.columnHeader17 = new ColumnHeader();
			this.columnHeader18 = new ColumnHeader();
			this.columnHeader29 = new ColumnHeader();
			this.columnHeader30 = new ColumnHeader();
			this.columnHeader31 = new ColumnHeader();
			this.columnHeader39 = new ColumnHeader();
			this.tabPage3 = new TabPage();
			this.lv2 = new ListView();
			this.columnHeader20 = new ColumnHeader();
			this.columnHeader21 = new ColumnHeader();
			this.columnHeader22 = new ColumnHeader();
			this.columnHeader23 = new ColumnHeader();
			this.columnHeader24 = new ColumnHeader();
			this.columnHeader25 = new ColumnHeader();
			this.columnHeader26 = new ColumnHeader();
			this.tabPage4 = new TabPage();
			this.lv3 = new ListView();
			this.columnHeader35 = new ColumnHeader();
			this.columnHeader42 = new ColumnHeader();
			this.columnHeader43 = new ColumnHeader();
			this.columnHeader40 = new ColumnHeader();
			this.columnHeader41 = new ColumnHeader();
			this.columnHeader44 = new ColumnHeader();
			this.cmdCreateData = new Button();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuAdd = new MenuItem();
			this.menuEdit = new MenuItem();
			this.menuDel = new MenuItem();
			this.menuItem4 = new MenuItem();
			this.menuPrint = new MenuItem();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			base.SuspendLayout();
			this.cmdOK.FlatStyle = FlatStyle.Flat;
			this.cmdOK.Location = new Point(332, 8);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(196, 28);
			this.cmdOK.TabIndex = 2;
			this.cmdOK.Text = "Прием данных";
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			this.label1.BackColor = SystemColors.Info;
			this.label1.Location = new Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(284, 28);
			this.label1.TabIndex = 141;
			this.label1.Text = "Укажите файл!";
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			this.cmdOpenFile.FlatStyle = FlatStyle.Flat;
			this.cmdOpenFile.Location = new Point(292, 8);
			this.cmdOpenFile.Name = "cmdOpenFile";
			this.cmdOpenFile.Size = new System.Drawing.Size(32, 28);
			this.cmdOpenFile.TabIndex = 1;
			this.cmdOpenFile.Text = "...";
			this.toolTip1.SetToolTip(this.cmdOpenFile, "Выбор файла");
			this.cmdOpenFile.Click += new EventHandler(this.cmdOpenFile_Click);
			this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv.BorderStyle = BorderStyle.FixedSingle;
			this.lv.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns = this.lv.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader9, this.columnHeader28, this.columnHeader32, this.columnHeader33, this.columnHeader34, this.columnHeader36, this.columnHeader37 };
			columns.AddRange(columnHeaderArray);
			this.lv.FullRowSelect = true;
			this.lv.GridLines = true;
			this.lv.Location = new Point(4, 4);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(772, 355);
			this.lv.TabIndex = 142;
			this.lv.View = View.Details;
			this.columnHeader1.Text = "действие";
			this.columnHeader1.Width = 68;
			this.columnHeader2.Text = "Тип док-та";
			this.columnHeader2.Width = 98;
			this.columnHeader3.Text = "л/счет";
			this.columnHeader4.Text = "№ ПУ";
			this.columnHeader5.Text = "№ док-та";
			this.columnHeader6.Text = "Дата док-та";
			this.columnHeader6.Width = 77;
			this.columnHeader7.Text = "Показания";
			this.columnHeader7.Width = 70;
			this.columnHeader8.Text = "Примечание";
			this.columnHeader8.Width = 98;
			this.columnHeader9.Text = "Параметры";
			this.columnHeader9.Width = 175;
			this.columnHeader28.Text = "IDTypeDocument";
			this.columnHeader28.Width = 0;
			this.columnHeader32.Text = "IDContract";
			this.columnHeader32.Width = 0;
			this.columnHeader33.Text = "PD1/4/3";
			this.columnHeader33.Width = 0;
			this.columnHeader34.Text = "PD2/-/4";
			this.columnHeader34.Width = 0;
			this.columnHeader36.Text = "PD5/-/-";
			this.columnHeader36.Width = 0;
			this.columnHeader37.Text = "IDGMeter";
			this.columnHeader37.Width = 0;
			this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new Point(4, 44);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(788, 388);
			this.tabControl1.TabIndex = 143;
			this.tabPage1.Controls.Add(this.lv);
			this.tabPage1.Location = new Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(780, 362);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Документы";
			this.tabPage2.Controls.Add(this.lv1);
			this.tabPage2.Location = new Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(780, 362);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Приборы учеты";
			this.lv1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv1.BorderStyle = BorderStyle.FixedSingle;
			this.lv1.CheckBoxes = true;
			ListView.ColumnHeaderCollection columnHeaderCollections = this.lv1.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader10, this.columnHeader27, this.columnHeader11, this.columnHeader12, this.columnHeader13, this.columnHeader14, this.columnHeader15, this.columnHeader16, this.columnHeader38, this.columnHeader19, this.columnHeader17, this.columnHeader18, this.columnHeader29, this.columnHeader30, this.columnHeader31, this.columnHeader39 };
			columnHeaderCollections.AddRange(columnHeaderArray);
			this.lv1.FullRowSelect = true;
			this.lv1.GridLines = true;
			this.lv1.Location = new Point(4, 4);
			this.lv1.MultiSelect = false;
			this.lv1.Name = "lv1";
			this.lv1.Size = new System.Drawing.Size(772, 355);
			this.lv1.TabIndex = 143;
			this.lv1.View = View.Details;
			this.lv1.MouseUp += new MouseEventHandler(this.lv1_MouseUp);
			this.lv1.ItemCheck += new ItemCheckEventHandler(this.lv1_ItemCheck);
			this.columnHeader10.Text = "действие";
			this.columnHeader10.Width = 68;
			this.columnHeader27.Text = "IDConformity";
			this.columnHeader27.Width = 75;
			this.columnHeader11.Text = "л/счет";
			this.columnHeader11.Width = 57;
			this.columnHeader12.Text = "Тип ПУ";
			this.columnHeader12.Width = 70;
			this.columnHeader13.Text = "№ ПУ";
			this.columnHeader14.Text = "Нач. пок.";
			this.columnHeader15.Text = "Дата уст.";
			this.columnHeader15.Width = 63;
			this.columnHeader16.Text = "Дата пов.";
			this.columnHeader16.Width = 62;
			this.columnHeader38.Text = "Рез-т поверки";
			this.columnHeader38.Width = 72;
			this.columnHeader19.Text = "Статус";
			this.columnHeader19.Width = 64;
			this.columnHeader17.Text = "Примечание";
			this.columnHeader17.Width = 59;
			this.columnHeader18.Text = "Дата изг.";
			this.columnHeader18.Width = 61;
			this.columnHeader29.Text = "IDStatusGMeter";
			this.columnHeader29.Width = 0;
			this.columnHeader30.Text = "IDTypeGMeter";
			this.columnHeader30.Width = 0;
			this.columnHeader31.Text = "IDContract";
			this.columnHeader31.Width = 0;
			this.columnHeader39.Text = "IDTypeVerify";
			this.columnHeader39.Width = 0;
			this.tabPage3.Controls.Add(this.lv2);
			this.tabPage3.Location = new Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(780, 362);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Типы приборов учета";
			this.lv2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv2.BorderStyle = BorderStyle.FixedSingle;
			this.lv2.CheckBoxes = true;
			ListView.ColumnHeaderCollection columns1 = this.lv2.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader20, this.columnHeader21, this.columnHeader22, this.columnHeader23, this.columnHeader24, this.columnHeader25, this.columnHeader26 };
			columns1.AddRange(columnHeaderArray);
			this.lv2.FullRowSelect = true;
			this.lv2.GridLines = true;
			this.lv2.Location = new Point(4, 4);
			this.lv2.MultiSelect = false;
			this.lv2.Name = "lv2";
			this.lv2.Size = new System.Drawing.Size(772, 355);
			this.lv2.TabIndex = 144;
			this.lv2.View = View.Details;
			this.lv2.MouseUp += new MouseEventHandler(this.lv2_MouseUp);
			this.lv2.ItemCheck += new ItemCheckEventHandler(this.lv2_ItemCheck);
			this.columnHeader20.Text = "действие";
			this.columnHeader20.Width = 68;
			this.columnHeader21.Text = "IDConformity";
			this.columnHeader21.Width = 73;
			this.columnHeader22.Text = "Тип ПУ";
			this.columnHeader22.Width = 93;
			this.columnHeader23.Text = "класс точности";
			this.columnHeader23.Width = 100;
			this.columnHeader24.Text = "Разрядность шкалы";
			this.columnHeader24.Width = 119;
			this.columnHeader25.Text = "Период поверки";
			this.columnHeader25.Width = 104;
			this.columnHeader26.Text = "Примечание";
			this.columnHeader26.Width = 152;
			this.tabPage4.Controls.Add(this.lv3);
			this.tabPage4.Location = new Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(780, 362);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Л/счета";
			this.lv3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.lv3.BorderStyle = BorderStyle.FixedSingle;
			this.lv3.CheckBoxes = true;
			ListView.ColumnHeaderCollection columnHeaderCollections1 = this.lv3.Columns;
			columnHeaderArray = new ColumnHeader[] { this.columnHeader35, this.columnHeader42, this.columnHeader43, this.columnHeader40, this.columnHeader41, this.columnHeader44 };
			columnHeaderCollections1.AddRange(columnHeaderArray);
			this.lv3.FullRowSelect = true;
			this.lv3.GridLines = true;
			this.lv3.Location = new Point(4, 4);
			this.lv3.MultiSelect = false;
			this.lv3.Name = "lv3";
			this.lv3.Size = new System.Drawing.Size(772, 355);
			this.lv3.TabIndex = 145;
			this.lv3.View = View.Details;
			this.lv3.MouseUp += new MouseEventHandler(this.lv3_MouseUp);
			this.lv3.ItemCheck += new ItemCheckEventHandler(this.lv3_ItemCheck);
			this.columnHeader35.Text = "действие";
			this.columnHeader35.Width = 68;
			this.columnHeader42.Text = "Л/счет";
			this.columnHeader42.Width = 75;
			this.columnHeader43.Text = "Фамилия";
			this.columnHeader43.Width = 119;
			this.columnHeader40.Text = "Имя";
			this.columnHeader40.Width = 104;
			this.columnHeader41.Text = "Отчество";
			this.columnHeader41.Width = 108;
			this.columnHeader44.Text = "IDPerson";
			this.columnHeader44.Width = 0;
			this.cmdCreateData.FlatStyle = FlatStyle.Flat;
			this.cmdCreateData.Location = new Point(536, 8);
			this.cmdCreateData.Name = "cmdCreateData";
			this.cmdCreateData.Size = new System.Drawing.Size(196, 28);
			this.cmdCreateData.TabIndex = 144;
			this.cmdCreateData.Text = "Загрузка данных в БД";
			this.cmdCreateData.Click += new EventHandler(this.cmdCreateData_Click);
			System.Windows.Forms.Menu.MenuItemCollection menuItems = this.contextMenu1.MenuItems;
			MenuItem[] menuItemArray = new MenuItem[] { this.menuAdd, this.menuEdit, this.menuDel, this.menuItem4, this.menuPrint };
			menuItems.AddRange(menuItemArray);
			this.menuAdd.Index = 0;
			this.menuAdd.Text = "Добавить";
			this.menuEdit.Index = 1;
			this.menuEdit.Text = "Изменить";
			this.menuDel.Index = 2;
			this.menuDel.Text = "Удалить";
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "-";
			this.menuPrint.Index = 4;
			this.menuPrint.Text = "Печать";
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(794, 435);
			base.Controls.Add(this.cmdCreateData);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.cmdOpenFile);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cmdOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Name = "frmGetChanges";
			this.Text = "Прием транспортного файла от Поверителя";
			base.Load += new EventHandler(this.frmGetChanges_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void lv1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Unchecked)
			{
				for (int i = 3; i <= 11; i++)
				{
					this.lv1.Items[e.Index].SubItems[i].BackColor = SystemColors.Window;
				}
			}
		}

		private void lv1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Right || Tools.GetColumnByClickedMouse(this.lv1, e.X) == 0)
			{
				this.lv1.ContextMenu = this.contextMenu1;
				return;
			}
			this.lv1.ContextMenu = null;
			this.CompareGMeter();
		}

		private void lv2_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Unchecked)
			{
				for (int i = 2; i <= 6; i++)
				{
					this.lv2.Items[e.Index].SubItems[i].BackColor = SystemColors.Window;
				}
			}
		}

		private void lv2_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Right || Tools.GetColumnByClickedMouse(this.lv2, e.X) != 0)
			{
				this.lv2.ContextMenu = this.contextMenu1;
				return;
			}
			this.lv2.ContextMenu = null;
			this.CompareTypeGMeter();
		}

		private void lv3_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue == CheckState.Unchecked)
			{
				for (int i = 2; i <= 4; i++)
				{
					this.lv3.Items[e.Index].SubItems[i].BackColor = SystemColors.Window;
				}
			}
		}

		private void lv3_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Right || Tools.GetColumnByClickedMouse(this.lv3, e.X) != 0)
			{
				this.lv3.ContextMenu = this.contextMenu1;
				return;
			}
			this.lv3.ContextMenu = null;
			this.CompareContract();
		}

		private void menuItem1_Click(object sender, ClickEventArgs e)
		{
			switch (this.tabControl1.SelectedIndex)
			{
				case 0:
				{
					Tools.ConvertToExcel(this.lv);
					return;
				}
				case 1:
				{
					Tools.ConvertToExcel(this.lv1);
					return;
				}
				case 2:
				{
					Tools.ConvertToExcel(this.lv2);
					return;
				}
				case 3:
				{
					Tools.ConvertToExcel(this.lv3);
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void menuItem2_Click(object sender, ClickEventArgs e)
		{
			switch (this.tabControl1.SelectedIndex)
			{
				case 0:
				{
					Tools.ConvertToExcelByCheck(this.lv, false);
					return;
				}
				case 1:
				{
					Tools.ConvertToExcelByCheck(this.lv1, false);
					return;
				}
				case 2:
				{
					Tools.ConvertToExcelByCheck(this.lv2, false);
					return;
				}
				case 3:
				{
					Tools.ConvertToExcelByCheck(this.lv3, false);
					return;
				}
				default:
				{
					return;
				}
			}
		}
	}
}