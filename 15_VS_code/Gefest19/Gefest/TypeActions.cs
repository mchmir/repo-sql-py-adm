using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeActions : SimpleClasss
	{
		public TypeAction this[int index]
		{
			get
			{
				return (TypeAction)base.get_Item(index);
			}
		}

		public TypeActions()
		{
			this.NameTable = "TypeAction";
		}

		public TypeAction Add()
		{
			TypeAction typeAction = new TypeAction();
			typeAction.Init();
			typeAction.set_Parent(this);
			return typeAction;
		}

		public TypeAction item(long ID)
		{
			TypeAction elementByID;
			try
			{
				elementByID = (TypeAction)base.GetElementByID(ID);
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
			TypeAction typeAction = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeAction");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeAction = new TypeAction();
						typeAction.SetState(row);
						base.Add(typeAction);
					}
					typeAction = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeAction = null;
				num = 1;
			}
			return num;
		}
	}
}