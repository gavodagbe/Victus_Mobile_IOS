using System;
using System.Xml;
using System.Linq;
using System.Json;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using Victus_Mobile_IOS;
using MonoTouch.Dialog;
using FlyoutNavigation;

namespace Victus_Mobile_IOS
{
	public partial class loginView : UIViewController
	{
		public loginView () : base ("loginView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		/// <summary>
		/// Views the did load.
		/// Variables Initilisation
		/// </summary>
		FlyoutNavigationController navigation;

		// Data we'll use to create our flyout menu and views:
		string[] Tasks = {
			"Accueil",
			"Liste des DI",
			"Liste des Plans",
			"Liste des Budgets",
			"Se Déconnecter",
		};

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			// Style appliqué au button de connexion
			ConnexionButton.Layer.BorderWidth = 1F;
			ConnexionButton.Layer.BorderColor = UIColor.Black.CGColor;
			ConnexionButton.Layer.CornerRadius = 4;

			//Actio du bouton connexion
			ConnexionButton.TouchUpInside += (object sender, EventArgs e) => {

				NSUserDefaults.StandardUserDefaults.SetString("","CFID"); 

				//Affiche le waitActivity
				//WaitActivity.Hidden = false;
				//labelError.Text = "";

				//Récupération des identitifants / mot de passe
				string userName = InputUser.Text;
				string password = InputPwd.Text;
				//Console.WriteLine(userName + " / " +password);

				//Récupération du token
				string token = getToken();
				Console.WriteLine("Token"+token);
				//Hash du mot de passe
				string hashPwd = MD5Core.GetHashString(password);

				//Concatène le pwd hashé et le token
				string connectionToken = MD5Core.GetHashString(token+hashPwd);

				//Envoie au WS de connexion
				//string resultConnection = Victus_Mobile_IOS.networkManager.callURL("http://vincennes.votreextranet.fr/services/Logger/login.cfm?loginPassword="+connectionToken+"&loginUsername="+userName,"");
				string resultConnection = Victus_Mobile_IOS.networkManager.callURL("http://192.168.0.221/victus/services/Logger/login.cfm?loginPassword="+connectionToken+"&loginUsername="+userName+"&CFID="+NSUserDefaults.StandardUserDefaults["CFID"]+"&CFTOKEN="+NSUserDefaults.StandardUserDefaults["CFTOKEN"],"");
				//string resultConnection = Victus_Mobile_IOS.networkManager.callURL("http://victuspp.sourceamax.com/services/Logger/login.cfm?loginPassword="+connectionToken+"&loginUsername="+userName+"&CFID="+NSUserDefaults.StandardUserDefaults["CFID"]+"&CFTOKEN="+NSUserDefaults.StandardUserDefaults["CFTOKEN"],"");

				//Parse le résultat
				JsonValue jsonResult = Victus_Mobile_IOS.parseManager.parseJSON(resultConnection);

				//Si réponse ok : Accès à la vue board
				if(jsonResult["success"] == "true"){
					Console.WriteLine("Connexion ok");
					//labelError.Text = "Connexion réussi";


					/*--------------------------------------------------------------------------------------------------------------------
					 *	Affichage du tableau de bord 
					 *--------------------------------------------------------------------------------------------------------------------*/ 

					navigation = new FlyoutNavigationController ();
					navigation.Position = FlyOutNavigationPosition.Left;
					//navigation.View.Frame = UIScreen.MainScreen.Bounds;

					// Create the menu:

					navigation.NavigationRoot = new RootElement (string.Empty) {
						new Section () {
							from page in Tasks
							select new ImageStringElement(page.ToUpper(), delegate {
								Console.WriteLine(page.ToUpper());
							}, UIImage.FromFile("logo.PNG")) as Element,

						}

					};

					// Style sur le Menu
					navigation.NavigationTableView.SeparatorColor = UIColor.Red;
					navigation.NavigationTableView.BackgroundColor = UIColor.Gray;

					// Create an array of UINavigationControllers that correspond to your
					// menu items:

					/*
					 * TODO : Rendre dynamique cette partie.
					*/
					navigation.ViewControllers = new [] {
						// Tableau de board
						new boardView(),
						new boardView(),
						new boardView(),
						new boardView(),
						new loginView(),
						new UIViewController {}, // Pour le moment, enlever cette ligne fait planter les traitements. Ligne à enlever après.
					};

					View.AddSubview (navigation.View);

					NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Action, delegate {
						navigation.ToggleMenu ();
					});




				}else{
					Console.WriteLine("Connexion KO");
					new UIAlertView("ERREUR", "Login ou mot de passe incorrect. Veuillez Réessayer.\n Merci."
						, null, "VALIDER", "QUITTER").Show();
					//Sinon vide le champs mot de passe et met un message d'erreur
					InputPwd.Text = "";
					//labelError.Text = "Erreur d'authentification";
				}

			};
		}

		class TaskPageController : DialogViewController
		{
			public TaskPageController (FlyoutNavigationController navigation, string title) : base (null)
			{
				Root = new RootElement (title) {
					new Section {
						new CheckboxElement (title)
					}
				};
				NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Action, delegate {
					navigation.ToggleMenu ();
				});
			}
		}

		public string getToken(){
			//return Victus_Mobile_IOS.networkManager.callURL("http://vincennes.votreextranet.fr/services/Logger/getToken.cfm","");
			return Victus_Mobile_IOS.networkManager.callURL("http://192.168.0.221/victus/services/Logger/getToken.cfm","");
			//return Victus_Mobile_IOS.networkManager.callURL("http://victuspp.sourceamax.com/services/Logger/getToken.cfm","");
		}
	}
}

