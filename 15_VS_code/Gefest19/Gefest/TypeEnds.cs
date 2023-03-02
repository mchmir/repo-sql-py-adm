using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeEnds : SimpleClasss
	{
		public TypeEnd this[int index]
		{
			get
			{
				return (TypeEnd)base.get_Item(index);
			}
		}

		public TypeEnds()
		{
			this.NameTable = "TypeEnd";
		}

		public TypeEnd Add()
		{
			TypeEnd typeEnd = new TypeEnd();
			typeEnd.Init();
			typeEnd.set_Parent(this);
			return typeEnd;
		}

		public TypeEnd item(long ID)
		{
			TypeEnd elementByID;
			try
			{
				elementByID = (TypeEnd)base.GetElementByID(ID);
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
			TypeEnd typeEnd = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeEnd");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeEnd = new TypeEnd();
						typeEnd.SetState(row);
						base.Add(typeEnd);
					}
					typeEnd = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeEnd = null;
				num = 1;
			}
			return num;
		}
	}
}