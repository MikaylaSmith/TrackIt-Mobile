using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIt_Mobile.ViewModels;
using TrackIt_Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackIt_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			this.BindingContext = new LoginViewModel();
		}

		/* Purpose: Query Users data table and get information on the users stored in the table
		 * Input: Username string, Password string 
		 * Output: If true, go to Home. If false, return
		 */
		private async void OnLogIn(object sender, EventArgs e)
		{
			//Check username and password for SQL injections
			string Username = UserNameField.Text;
			string Password = PasswordField.Text;
			//string Username = "Admin";
			//string Password = "Password";

			//Check if what was submitted is blank
			if (Username == null || Password == null)
			{
				//If blank, display alert and go back
				await DisplayAlert("Field is Blank", "There is a field left blank.\nPlease Enter the Login Information", "Ok");
			}
			else
			{
				//If not blank

				//Make New Passwords object to test and use the functions of
				Passwords testingPassword = new Passwords();

				//Set Injection Status of both username and password
				bool InjectionTestUsername = testingPassword.InjectionCheck(Username);
				bool InjectionTestPassword = testingPassword.InjectionCheck(Password);

				//Test if there are "bad" characters in the strings that were submitted
				if (!InjectionTestUsername || !InjectionTestPassword)
				{
					//Display a different message based on where the invalid character is
					if (!InjectionTestUsername)
					{
						await DisplayAlert("Username Contains Invalid Characters", "Only capital and lowercase letters, underscores, ! and @ are allowed.\nPlease Enter Valid Information", "Ok");
					}
					else if (!InjectionTestPassword)
					{
						await DisplayAlert("Password Contains Invalid Characters", "Only capital and lowercase letters, underscores, ! and @ are allowed.\nPlease Enter Valid Information", "Ok");
					}
				}
				else
				{
					//If passes the injection test
					try
					{
						//If able to access, store results
						await App.Database.GetUser(Username, Password);
					}
					catch (Exception ex)
					{
						//If there is an error, display message and return from function
						string errorMessage = ex.Message;
						Console.WriteLine("Error Message: {0}", errorMessage);

						Console.WriteLine("Error Reading from database - LoginPage");
						await DisplayAlert("Database Error", "Error Getting Information From Database. Please try again later. ", "Ok");
						PasswordField.Text = string.Empty;
						return;
					}

					//If nothing matching was found, foundUser will be null. 
					//If not null, a valid user was found so log them in
					if (App.User.UserID != -1)
					{
						//Set sign in status here
						App.UserIsLoggedIn = true;

						//Navigation to the proper home page based on access 
						if (App.User.StudentAccess)
						{
							await Shell.Current.GoToAsync($"//{nameof(StudentsPage)}");
						}
						else if (App.User.JournalAccess)
						{
							await Shell.Current.GoToAsync($"//{nameof(JournalsPage)}");
						}
						else if (App.User.BudgetAccess)
						{
							await Shell.Current.GoToAsync($"//{nameof(BudgetsPage)}");
						}
					}
					else
					{
						await DisplayAlert("Username or Password Does Not Match", "Please Enter the Correct Information", "Ok");
						PasswordField.Text = string.Empty;
						return;
					}
				}
			}
		}
	}
}