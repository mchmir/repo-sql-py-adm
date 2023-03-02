using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class Agents : SimpleClasss
	{
		public Agent this[int index]
		{
			get
			{
				return (Agent)base.get_Item(index);
			}
		}

		public Agents()
		{
			this.NameTable = "Agent";
		}

		public Agent Add()
		{
			Agent agent = new Agent();
			agent.Init();
			agent.set_Parent(this);
			return agent;
		}

		public Agent item(long ID)
		{
			Agent elementByID;
			try
			{
				elementByID = (Agent)base.GetElementByID(ID);
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
			Agent agent = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "Name desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						agent = new Agent();
						agent.SetState(row);
						base.Add(agent);
					}
					agent = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				agent = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeAgent oTypeAgent)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Agent agent = null;
			try
			{
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idtypeagent" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { string.Concat("=", oTypeAgent.get_ID()) };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "Name desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						agent = new Agent();
						agent.SetState(row);
						base.Add(agent);
					}
					agent = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				agent = null;
				num = 1;
			}
			return num;
		}

		public int Load(TypeAgent[] oTypeAgent)
		{
			int num;
			int num1 = 0;
			DataTable dataTable = null;
			Agent agent = null;
			try
			{
				string str = " in (";
				TypeAgent[] typeAgentArray = oTypeAgent;
				for (int i = 0; i < (int)typeAgentArray.Length; i++)
				{
					long d = typeAgentArray[i].get_ID();
					str = string.Concat(str, d.ToString(), ",");
				}
				str = string.Concat(str.Substring(0, str.Length - 1), ")");
				string nameTable = this.NameTable;
				string[] strArrays = new string[] { "idtypeagent" };
				string[] strArrays1 = strArrays;
				strArrays = new string[] { str };
				num1 = Loader.LoadCollection(nameTable, ref dataTable, strArrays1, strArrays, "Name desc");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						agent = new Agent();
						agent.SetState(row);
						base.Add(agent);
					}
					agent = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				agent = null;
				num = 1;
			}
			return num;
		}
	}
}