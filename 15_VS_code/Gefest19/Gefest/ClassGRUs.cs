using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class ClassGRUs : SimpleClasss
	{
		public ClassGRU this[int index]
		{
			get
			{
				return (ClassGRU)base.get_Item(index);
			}
		}

		public ClassGRUs()
		{
			this.NameTable = "ClassGRU";
		}

		public ClassGRU Add()
		{
			ClassGRU classGRU = new ClassGRU();
			classGRU.Init();
			classGRU.set_Parent(this);
			return classGRU;
		}

		public ClassGRU item(long ID)
		{
			return (ClassGRU)base.GetElementByID(ID);
		}

		public override int Load()
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			ClassGRU classGRU = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "Name");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						classGRU = new ClassGRU();
						classGRU.SetState(row);
						base.Add(classGRU);
					}
					classGRU = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				classGRU = null;
				num = 1;
			}
			return num;
		}
	}
}