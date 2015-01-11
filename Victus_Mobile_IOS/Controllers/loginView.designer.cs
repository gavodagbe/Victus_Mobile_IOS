// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Victus_Mobile_IOS
{
	[Register ("loginView")]
	partial class loginView
	{
		[Outlet]
		UIKit.UIButton ConnexionButton { get; set; }

		[Outlet]
		UIKit.UITextField InputPwd { get; set; }

		[Outlet]
		UIKit.UITextField InputUser { get; set; }

		[Outlet]
		UIKit.UITableView loginTableObj { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (loginTableObj != null) {
				loginTableObj.Dispose ();
				loginTableObj = null;
			}

			if (ConnexionButton != null) {
				ConnexionButton.Dispose ();
				ConnexionButton = null;
			}

			if (InputPwd != null) {
				InputPwd.Dispose ();
				InputPwd = null;
			}

			if (InputUser != null) {
				InputUser.Dispose ();
				InputUser = null;
			}
		}
	}
}
