using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class LinkGroupPersons : SimpleClasss
	{
		public LinkGroupPerson this[int index]
		{
			get
			{
				return (LinkGroupPerson)base.get_Item(index);
			}
		}

		public LinkGroupPersons()
		{
			this.NameTable = "LinkGroupPerson";
		}

		public LinkGroupPerson Add()
		{
			LinkGroupPerson linkGroupPerson = new LinkGroupPerson();
			linkGroupPerson.Init();
			linkGroupPerson.set_Parent(this);
			return linkGroupPerson;
		}

		public LinkGroupPerson item(long ID)
		{
			LinkGroupPerson elementByID;
			try
			{
				elementByID = (LinkGroupPerson)base.GetElementByID(ID);
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
			LinkGroupPerson linkGroupPerson = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDLinkGroupPerson");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						linkGroupPerson = new LinkGroupPerson();
						linkGroupPerson.SetState(row);
						base.Add(linkGroupPerson);
					}
					linkGroupPerson = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				linkGroupPerson = null;
				num = 1;
			}
			return num;
		}
	}
}