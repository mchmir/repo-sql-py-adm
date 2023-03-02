using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeAgents : SimpleClasss
	{
		public TypeAgent this[int index]
		{
			get
			{
				return (TypeAgent)base.get_Item(index);
			}
		}

		public TypeAgents()
		{
			this.NameTable = "TypeAgent";
		}

		public TypeAgent Add()
		{
			TypeAgent typeAgent = new TypeAgent();
			typeAgent.Init();
			typeAgent.set_Parent(this);
			return typeAgent;
		}

		public TypeAgent item(long ID)
		{
			TypeAgent elementByID;
			try
			{
				elementByID = (TypeAgent)base.GetElementByID(ID);
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
			TypeAgent typeAgent = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "Name desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeAgent = new TypeAgent();
						typeAgent.SetState(row);
						base.Add(typeAgent);
					}
					typeAgent = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeAgent = null;
				num = 1;
			}
			return num;
		}
	}
}