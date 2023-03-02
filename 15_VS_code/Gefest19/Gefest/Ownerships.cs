using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Ownerships : SimpleClasss
	{
		private int _level;

		public Ownership this[int index]
		{
			get
			{
				return (Ownership)base.get_Item(index);
			}
		}

		public Ownerships()
		{
			this.NameTable = "Ownership";
		}

		public Ownership Add()
		{
			Ownership ownership = new Ownership();
			ownership.Init();
			ownership.set_Parent(this);
			ownership.Level = this._level;
			return ownership;
		}

		public Ownership item(long ID)
		{
			Ownership elementByID;
			try
			{
				elementByID = (Ownership)base.GetElementByID(ID);
			}
			catch
			{
				elementByID = null;
			}
			return elementByID;
		}

		public override int Load()
		{
			return 1;
		}

		public int Load(int Level)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Ownership ownership = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "Level" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", Level.ToString()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "Name");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						ownership = new Ownership();
						ownership.SetState(row);
						base.Add(ownership);
					}
					ownership = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				ownership = null;
				num = 1;
			}
			return num;
		}
	}
}