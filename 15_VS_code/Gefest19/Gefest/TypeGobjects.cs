using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeGobjects : SimpleClasss
	{
		public TypeGobject this[int index]
		{
			get
			{
				return (TypeGobject)base.get_Item(index);
			}
		}

		public TypeGobjects()
		{
			this.NameTable = "TypeGobject";
		}

		public TypeGobject Add()
		{
			TypeGobject typeGobject = new TypeGobject();
			typeGobject.Init();
			typeGobject.set_Parent(this);
			return typeGobject;
		}

		public TypeGobject item(long ID)
		{
			TypeGobject elementByID;
			try
			{
				elementByID = (TypeGobject)base.GetElementByID(ID);
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
			TypeGobject typeGobject = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeGobject");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeGobject = new TypeGobject();
						typeGobject.SetState(row);
						base.Add(typeGobject);
					}
					typeGobject = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeGobject = null;
				num = 1;
			}
			return num;
		}
	}
}