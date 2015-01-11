
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Net.Http;

using Foundation;
using UIKit;

namespace Victus_Mobile_IOS
{
	public partial class boardView : UIViewController
	{
		public boardView () : base ("boardView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			/*var stringValue = this.LoadImage("http://blog.jcmultimedia.com.au/images/Table1-iOS-Versions-over-time.png");
			Console.WriteLine (stringValue);*/
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public async Task<UIImage> LoadImage (string imageUrl)
		{
			var httpClient = new HttpClient();

			Task<byte[]> contentsTask = httpClient.GetByteArrayAsync (imageUrl);

			// await! control returns to the caller and the task continues to run on another thread
			var contents = await contentsTask;

			// load from bytes
			return UIImage.LoadFromData (NSData.FromArray (contents));
		}
	}
}

