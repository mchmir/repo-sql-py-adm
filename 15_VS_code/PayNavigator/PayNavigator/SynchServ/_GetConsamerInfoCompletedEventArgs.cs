using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace PayNavigator.SynchServ
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "4.0.30319.17929")]
	public class _GetConsamerInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public object[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (object[])this.results[0];
			}
		}

		internal _GetConsamerInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}