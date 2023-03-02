using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeBatchs : SimpleClasss
	{
		public TypeBatch this[int index]
		{
			get
			{
				return (TypeBatch)base.get_Item(index);
			}
		}

		public TypeBatchs()
		{
			this.NameTable = "TypeBatch";
		}

		public TypeBatch Add()
		{
			TypeBatch typeBatch = new TypeBatch();
			typeBatch.Init();
			typeBatch.set_Parent(this);
			return typeBatch;
		}

		public TypeBatch item(long ID)
		{
			TypeBatch elementByID;
			try
			{
				elementByID = (TypeBatch)base.GetElementByID(ID);
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
			TypeBatch typeBatch = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeBatch");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeBatch = new TypeBatch();
						typeBatch.SetState(row);
						base.Add(typeBatch);
					}
					typeBatch = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeBatch = null;
				num = 1;
			}
			return num;
		}
	}
}