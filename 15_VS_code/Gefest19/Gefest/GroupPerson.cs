using System;
using WebSecurityDLL;

namespace Gefest
{
	public class GroupPerson : SimpleClass
	{
		public GroupPerson()
		{
			this.NameTable = "GroupPerson";
		}

		protected override void Finalize()
		{
			base.Finalize();
		}
	}
}