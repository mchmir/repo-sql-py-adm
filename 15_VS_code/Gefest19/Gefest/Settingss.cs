using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Settingss : SimpleClasss
	{
		public Settings this[int index]
		{
			get
			{
				return (Settings)base.get_Item(index);
			}
		}

		public Settingss()
		{
			this.NameTable = "Settings";
		}

		public Settings Add()
		{
			Settings setting = new Settings();
			setting.Init();
			setting.set_Parent(this);
			return setting;
		}

		public Settings item(long ID)
		{
			Settings elementByID;
			try
			{
				elementByID = (Settings)base.GetElementByID(ID);
			}
			catch
			{
				elementByID = null;
			}
			return elementByID;
		}

		public override int Load()
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Settings setting = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDSettings");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						setting = new Settings();
						setting.SetState(row);
						base.Add(setting);
					}
					setting = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				setting = null;
				num = 1;
			}
			return num;
		}

		public int Load(SYSUser oUser)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Settings setting = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "IdUser" };
				string[] strArrays1 = strArrays;
				strArrays = new string[1];
				long d = oUser.get_ID();
				strArrays[0] = string.Concat("=", d.ToString());
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDUser");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						setting = new Settings();
						setting.SetState(row);
						base.Add(setting);
					}
					setting = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				setting = null;
				num = 1;
			}
			return num;
		}
	}
}