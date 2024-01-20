using System;
using System.Collections.Generic;
using TrackIt_Mobile.ViewModels;
using TrackIt_Mobile.Views;
using Xamarin.Forms;

namespace TrackIt_Mobile
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
			Routing.RegisterRoute(nameof(StudentsPage), typeof(StudentsPage));
			Routing.RegisterRoute(nameof(StudentDetailPage), typeof(StudentDetailPage));
			Routing.RegisterRoute(nameof(EducationLogDetailPage), typeof(EducationLogDetailPage));

			Routing.RegisterRoute(nameof(JournalsPage), typeof(JournalsPage));
			Routing.RegisterRoute(nameof(JournalsDetailPage), typeof(JournalsDetailPage));

			Routing.RegisterRoute(nameof(BudgetsPage), typeof(BudgetsPage));
			Routing.RegisterRoute(nameof(BudgetsDetailPage), typeof(BudgetsDetailPage));
		}

		/* Purpose: Removes currently logged in information and goes to login page. 
		 * Input: Button Clicked parameters.
		 * Output: None
		 */
		private async void OnLogOutClicked(object sender, EventArgs e)
		{
			//Destroy all of the current data
			App.User.UserID = 0;
			App.User.Email = string.Empty;
			App.User.Username = string.Empty;
			App.User.Password = string.Empty;

			App.User.StudentAccess = false;
			App.User.JournalAccess = false;
			App.User.BudgetAccess = false;

			App.UserIsLoggedIn = false;

			await Shell.Current.GoToAsync("//LoginPage");
		}
	}
}
