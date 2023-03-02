using System;
using WebSecurityDLL;

namespace Gefest
{
	public class GmeterPlombHistory : SimpleClass
	{
		public DateTime DatePlomb
		{
			get
			{
				return (DateTime)base.GetValue("DatePlomb");
			}
			set
			{
				base.SetValue("DatePlomb", value);
			}
		}

		public int IDAgentPlomb
		{
			get
			{
				return (int)base.GetValue("IDAgentPlomb");
			}
			set
			{
				base.SetValue("IDAgentPlomb", value);
			}
		}

		public int IDGMeter
		{
			get
			{
				return (int)base.GetValue("IDGMeter");
			}
			set
			{
				base.SetValue("IDGMeter", value);
			}
		}

		public double IndicationPlomb
		{
			get
			{
				return (double)base.GetValue("IndicationPlomb");
			}
			set
			{
				base.SetValue("IndicationPlomb", value);
			}
		}

		public string PlombMemo
		{
			get
			{
				return (string)base.GetValue("PlombMemo");
			}
			set
			{
				base.SetValue("PlombMemo", value);
			}
		}

		public string PlombNumber1
		{
			get
			{
				return (string)base.GetValue("PlombNumber1");
			}
			set
			{
				base.SetValue("PlombNumber1", value);
			}
		}

		public string PlombNumber2
		{
			get
			{
				return (string)base.GetValue("PlombNumber2");
			}
			set
			{
				base.SetValue("PlombNumber2", value);
			}
		}

		public GmeterPlombHistory()
		{
			this.NameTable = "GmeterPlombHistory";
		}
	}
}