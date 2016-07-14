using System;

namespace PO_Beoordeling
{
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public class _Default : System.Web.UI.Page
	{
//		static MDbgEngine _Debugger = null;
//		static MDbgProcess _WorkerProcess = null;


		private void Page_Load(object sender, System.EventArgs e)
		{
//			SetDebugger();
			Response.Redirect("./Gebruiker/beoAlgemeen_Login.aspx");
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

//		private void SetDebugger()
//		{
//			// iWPid = Worker Process Id
//			int iWPid = 0;
//
//			iWPid = GetASPWorkerProcessId();
//
//			if (iWPid != -1)
//			{
//				_Debugger = new MDbgEngine();
//
//				_WorkerProcess = _Debugger.Attach(iWPid);
//
//				_WorkerProcess.Go().WaitOne();
//
//				do
//				{
//				} while (!Console.KeyAvailable);
//
//				_WorkerProcess.Detach().WaitOne();
//
//				_Debugger = null;
//			}
//		}
	}
}
