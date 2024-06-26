﻿/***********
* Class: App
*
* Purpose:
*	Manage the application as a whole
*
* Manager Functions:
*	App
*		Initialize the app
*		
*
* Methods:
*	OnStart()
*		Set state for when fresh start
*	OnSleep()
*		Set state for when app goes to sleep
*	OnResume()
*		Set state for when app returns from sleep
*
***********/
using System;
using TrackIt_Mobile.Services;
using TrackIt_Mobile.Views;
using TrackIt_Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace TrackIt_Mobile
{
	public partial class App : Application
	{
		public static User User {get; set;}
		public static Database Database { get; set; }
		public static bool UserIsLoggedIn { get; set; }

		/* Purpose: Set state for when fresh start
		 * Input: App starts
		 * Output: App is usable
		 */
		public App()
		{
			InitializeComponent();

			User = new User();
			Database = new Database();

			MainPage = new AppShell();

			Shell.Current.CurrentItem = new LoginPage();
		}

		/* Purpose: Set state for when app goes to sleep
		 * Input: App starts up
		 * Output: App is usable
		 */
		protected override async void OnStart()
		{
			if (UserIsLoggedIn)
			{
				//On starting the app, if is logged in is true, 
				//Check which home needs to be sent to. 
				if (User.StudentAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(StudentsPage)}");
				}
				else if (User.JournalAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(JournalsPage)}");
				}
				else if (User.BudgetAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(BudgetsPage)}");
				}
				else
				{
					//Make user sign in again, so clear out all old data
					User.UserID = 0;
					User.Email = string.Empty;
					User.Username = string.Empty;
					User.Password = string.Empty;
					User.StudentAccess = false;
					User.JournalAccess = false;
					User.BudgetAccess = false;
					UserIsLoggedIn = false;

					await Shell.Current.GoToAsync("//LoginPage");
				}
			}
			else
			{
				//If not logged in, go to login page
				//await Shell.Current.GoToAsync("//LoginPage");
				await Shell.Current.GoToAsync("//LoginPage");
			}
		}

		/* Purpose: Set state for when app goes to sleep
		 * Input: App is running
		 * Output: App hides in background
		 */
		protected override async void OnSleep()
		{
			if (UserIsLoggedIn)
			{
				//On starting the app, if is logged in is true, 
				//Check which home needs to be sent to. 
				if (User.StudentAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(StudentsPage)}");
				}
				else if (User.JournalAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(JournalsPage)}");
				}
				else if (User.BudgetAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(BudgetsPage)}");
				}
				else
				{
					//Make user sign in again, so clear out all old data
					User.UserID = 0;
					User.Email = string.Empty;
					User.Username = string.Empty;
					User.Password = string.Empty;
					User.StudentAccess = false;
					User.JournalAccess = false;
					User.BudgetAccess = false;
					UserIsLoggedIn = false;

					await Shell.Current.GoToAsync("//LoginPage");
				}
			}
			else
			{
				//If not logged in, go to login page
				await Shell.Current.GoToAsync("//LoginPage");
			}
		}

		/* Purpose: Set state for when app returns from sleep
		 * Input: Opening app from closed state
		 * Output: App is usable
		 */
		protected override async void OnResume()
		{
			if (UserIsLoggedIn)
			{
				//On starting the app, if is logged in is true, 
				//Check which home needs to be sent to. 
				if (User.StudentAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(StudentsPage)}");
				}
				else if (User.JournalAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(JournalsPage)}");
				}
				else if (User.BudgetAccess)
				{
					await Shell.Current.GoToAsync($"//{nameof(BudgetsPage)}");
				}
				else
				{
					//Make user sign in again, so clear out all old data
					User.UserID = 0;
					User.Email = string.Empty;
					User.Username = string.Empty;
					User.Password = string.Empty;
					User.StudentAccess = false;
					User.JournalAccess = false;
					User.BudgetAccess = false;
					UserIsLoggedIn = false;

					await Shell.Current.GoToAsync("//LoginPage");
				}
			}
			else
			{
				//If not logged in, go to login page
				await Shell.Current.GoToAsync("//LoginPage");
			}
		}
	}
}
