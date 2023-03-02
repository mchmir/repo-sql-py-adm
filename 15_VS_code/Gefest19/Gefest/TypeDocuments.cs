using System;
using System.Collections;
using System.Data;
using System.Reflection;
using WebSecurityDLL;

namespace Gefest
{
	public class TypeDocuments : SimpleClasss
	{
		public TypeDocument this[int index]
		{
			get
			{
				return (TypeDocument)base.get_Item(index);
			}
		}

		public TypeDocuments()
		{
			this.NameTable = "TypeDocument";
		}

		public TypeDocument Add()
		{
			TypeDocument typeDocument = new TypeDocument();
			typeDocument.Init();
			typeDocument.set_Parent(this);
			return typeDocument;
		}

		public TypeDocument item(long ID)
		{
			TypeDocument elementByID;
			try
			{
				elementByID = (TypeDocument)base.GetElementByID(ID);
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
			TypeDocument typeDocument = null;
			try
			{
				num1 = Loader.LoadCollection(this.NameTable, ref dataTable, "IDTypeDocument");
				if (num1 == 0)
				{
					foreach (DataRow row in dataTable.Rows)
					{
						typeDocument = new TypeDocument();
						typeDocument.SetState(row);
						base.Add(typeDocument);
					}
					typeDocument = null;
				}
				num = num1;
			}
			catch
			{
				dataTable = null;
				typeDocument = null;
				num = 1;
			}
			return num;
		}
	}
}