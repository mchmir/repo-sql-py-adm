using System;
using WebSecurityDLL;

namespace Gefest
{
	public class Agent : SimpleClass
	{
		private TypeAgent _typeagent;

		public int IDSector
		{
			get
			{
				return (int)base.GetValue("IDSector");
			}
			set
			{
				base.SetValue("IDSector", value);
			}
		}

		public int NumberAgent
		{
			get
			{
				return (int)base.GetValue("NumberAgent");
			}
			set
			{
				base.SetValue("NumberAgent", value);
			}
		}

		public TypeAgent oTypeAgent
		{
			get
			{
				if (this._typeagent == null)
				{
					int value = (int)base.GetValue("IDTypeAgent");
					this._typeagent = new TypeAgent();
					if (this._typeagent.Load((long)value) != 0)
					{
						this._typeagent = null;
					}
				}
				return this._typeagent;
			}
			set
			{
				this._typeagent = value;
				if (this._typeagent != null)
				{
					base.SetValue("IDTypeAgent", this._typeagent.get_ID());
				}
			}
		}

		public Agent()
		{
			this.NameTable = "Agent";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}