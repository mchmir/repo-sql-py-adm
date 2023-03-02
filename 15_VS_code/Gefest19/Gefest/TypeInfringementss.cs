using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeInfringementss : SimpleClasss
	{
		public TypeInfringements this[int index]
		{
			get
			{
				return (TypeInfringements)base.get_Item(index);
			}
		}

		public TypeInfringementss()
		{
			this.NameTable = "TypeInfringements";
		}

		public TypeInfringements Add()
		{
			TypeInfringements typeInfringement = new TypeInfringements();
			typeInfringement.Init();
			typeInfringement.set_Parent(this);
			return typeInfringement;
		}

		public TypeInfringements item(long ID)
		{
			TypeInfringements elementByID;
			try
			{
				elementByID = (TypeInfringements)base.GetElementByID(ID);
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
			TypeInfringements typeInfringement = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeInfringements");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeInfringement = new TypeInfringements();
						typeInfringement.SetState(row);
						base.Add(typeInfringement);
					}
					typeInfringement = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeInfringement = null;
				num = 1;
			}
			return num;
		}
	}
}