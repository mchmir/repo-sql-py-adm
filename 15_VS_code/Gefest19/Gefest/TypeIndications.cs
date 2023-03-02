using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeIndications : SimpleClasss
	{
		public TypeIndication this[int index]
		{
			get
			{
				return (TypeIndication)base.get_Item(index);
			}
		}

		public TypeIndications()
		{
			this.NameTable = "TypeIndication";
		}

		public TypeIndication Add()
		{
			TypeIndication typeIndication = new TypeIndication();
			typeIndication.Init();
			typeIndication.set_Parent(this);
			return typeIndication;
		}

		public TypeIndication item(long ID)
		{
			TypeIndication elementByID;
			try
			{
				elementByID = (TypeIndication)base.GetElementByID(ID);
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
			TypeIndication typeIndication = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "Name desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeIndication = new TypeIndication();
						typeIndication.SetState(row);
						base.Add(typeIndication);
					}
					typeIndication = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeIndication = null;
				num = 1;
			}
			return num;
		}
	}
}