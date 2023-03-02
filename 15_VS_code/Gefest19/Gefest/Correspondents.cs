using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Correspondents : SimpleClasss
	{
		public Correspondent this[int index]
		{
			get
			{
				return (Correspondent)base.get_Item(index);
			}
		}

		public Correspondents()
		{
			this.NameTable = "Correspondent";
		}

		public Correspondent Add()
		{
			Correspondent correspondent = new Correspondent();
			correspondent.Init();
			correspondent.set_Parent(this);
			return correspondent;
		}

		public Correspondent item(long ID)
		{
			Correspondent elementByID;
			try
			{
				elementByID = (Correspondent)base.GetElementByID(ID);
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
			Correspondent correspondent = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDCorrespondent");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						correspondent = new Correspondent();
						correspondent.SetState(row);
						base.Add(correspondent);
					}
					correspondent = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				correspondent = null;
				num = 1;
			}
			return num;
		}
	}
}