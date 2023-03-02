using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Settings1s : SimpleClasss
	{
		public Settings1 this[int index]
		{
			get
			{
				return (Settings1)base.get_Item(index);
			}
		}

		public Settings1s()
		{
			this.NameTable = "Settings1";
		}

		public Settings1 Add()
		{
			Settings1 settings1 = new Settings1();
			settings1.Init();
			settings1.set_Parent(this);
			return settings1;
		}

		public Settings1 item(long ID)
		{
			Settings1 elementByID;
			try
			{
				elementByID = (Settings1)base.GetElementByID(ID);
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
			Settings1 settings1 = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDSettings1");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						settings1 = new Settings1();
						settings1.SetState(row);
						base.Add(settings1);
					}
					settings1 = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				settings1 = null;
				num = 1;
			}
			return num;
		}

		public int Load(Correspondent oCorrespondent)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Settings1 settings1 = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "IdCorrespondent" };
				string[] strArrays1 = strArrays;
				strArrays = new string[1];
				long d = oCorrespondent.get_ID();
				strArrays[0] = string.Concat("=", d.ToString());
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "IDCorrespondent");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						settings1 = new Settings1();
						settings1.SetState(row);
						base.Add(settings1);
					}
					settings1 = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				settings1 = null;
				num = 1;
			}
			return num;
		}
	}
}