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
	[Register ("boardView")]
	partial class boardView
	{
		[Outlet]
		UIKit.UIImageView imageBoard { get; set; }

		[Outlet]
		UIKit.UIButton nbDiDafToValid { get; set; }

		[Outlet]
		UIKit.UIButton nbDiDTToValid { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imageBoard != null) {
				imageBoard.Dispose ();
				imageBoard = null;
			}

			if (nbDiDafToValid != null) {
				nbDiDafToValid.Dispose ();
				nbDiDafToValid = null;
			}

			if (nbDiDTToValid != null) {
				nbDiDTToValid.Dispose ();
				nbDiDTToValid = null;
			}
		}
	}
}
