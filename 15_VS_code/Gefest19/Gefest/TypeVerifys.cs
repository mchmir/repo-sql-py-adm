using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeVerifys : SimpleClasss
	{
		public TypeVerify this[int index]
		{
			get
			{
				return (TypeVerify)base.get_Item(index);
			}
		}

		public TypeVerifys()
		{
			this.NameTable = "TypeVerify";
		}

		public TypeVerify Add()
		{
			TypeVerify typeVerify = new TypeVerify();
			typeVerify.Init();
			typeVerify.set_Parent(this);
			return typeVerify;
		}

		public TypeVerify item(long ID)
		{
			TypeVerify elementByID;
			try
			{
				elementByID = (TypeVerify)base.GetElementByID(ID);
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
			TypeVerify typeVerify = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeVerify");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeVerify = new TypeVerify();
						typeVerify.SetState(row);
						base.Add(typeVerify);
					}
					typeVerify = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeVerify = null;
				num = 1;
			}
			return num;
		}
	}
}