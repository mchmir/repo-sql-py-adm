using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeReasonDisconnects : SimpleClasss
	{
		public TypeReasonDisconnect this[int index]
		{
			get
			{
				return (TypeReasonDisconnect)base.get_Item(index);
			}
		}

		public TypeReasonDisconnects()
		{
			this.NameTable = "TypeReasonDisconnect";
		}

		public TypeReasonDisconnect Add()
		{
			TypeReasonDisconnect typeReasonDisconnect = new TypeReasonDisconnect();
			typeReasonDisconnect.Init();
			typeReasonDisconnect.set_Parent(this);
			return typeReasonDisconnect;
		}

		public TypeReasonDisconnect item(long ID)
		{
			TypeReasonDisconnect elementByID;
			try
			{
				elementByID = (TypeReasonDisconnect)base.GetElementByID(ID);
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
			TypeReasonDisconnect typeReasonDisconnect = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeReasonDisconnect");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeReasonDisconnect = new TypeReasonDisconnect();
						typeReasonDisconnect.SetState(row);
						base.Add(typeReasonDisconnect);
					}
					typeReasonDisconnect = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeReasonDisconnect = null;
				num = 1;
			}
			return num;
		}
	}
}