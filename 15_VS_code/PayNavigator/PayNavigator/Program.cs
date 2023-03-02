using System;
using System.Windows.Forms;

namespace PayNavigator
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new f_main());
		}
	}
}