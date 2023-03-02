using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace GGSLC.Eservice
{
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Web.Services", "4.6.1055.0")]
	public class _ServerCommandGetCompletedEventArgs : AsyncCompletedEventArgs
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

		internal _ServerCommandGetCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
}