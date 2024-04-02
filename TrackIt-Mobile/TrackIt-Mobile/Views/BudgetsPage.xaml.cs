/***********
* Class: BudgetsPage
*
* Purpose:
*	The purpose of this class is to display the list of budget entries to the user
*
* Manager Functions:
*	BudgetsPage()
*		Create an instance of the BudgetsPage
*		
*
* Methods:
*	OnAppearing()
*		Initialize the list when the page appears to the user
*	SetBudgetEntries()
*	    Query the database and set up the list
*	OnItemChanged(object, SelectionChangedEventArgs)
*	   Select item to display information for that selected item
*	BudgetSearchBarChanged(object, TextChangedEventArgs)
*	    Refresh list for search results
*
***********/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using TrackIt_Mobile.Models;

namespace TrackIt_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BudgetsPage : ContentPage
	{
		ObservableCollection<Budget> budgetEntriesList;
		private string searchFilter { get; set; }

		/* Purpose: Initialize the page
		 * Input: None
		 * Output: Page initialized
		 */
		public BudgetsPage()
		{
			InitializeComponent();
		}

		/* Purpose: Queries the database to display information
		 * Input: None
		 * Output: Data is listed to be formated to user
		 */
		protected override async void OnAppearing()
		{
			base.OnAppearing();

			budgetEntriesList = new ObservableCollection<Budget>();

			await SetBudgetEntries();

			if (budgetEntriesList.Count == 0)
			{
				//The list is empty
				Label label = new Label
				{
					Text = "No data available.",
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center
				};

				BudgetEntriesList.EmptyView = label;
			}
			else
			{
				BudgetEntriesList.ItemsSource = budgetEntriesList;
			}
		}

		/* Purpose: Runs async method to add query results to list
		 * Input: None
		 * Output: List
		 */
		private async Task SetBudgetEntries()
		{
			List<Budget> budgets = await App.Database.GetBudgetEntries();

			foreach (Budget budget in budgets)
			{
				budgetEntriesList.Add(budget);
			}
		}

		/* Purpose: Actions for itms from list beng selected
		 * Input: object, SelectionChangedEventArgs
		 * Output: BudgetDetailPage visible
		 */
		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Budget budget = e.CurrentSelection.FirstOrDefault() as Budget;
			if (budget != null)
			{
				await Navigation.PushAsync(new BudgetsDetailPage(budget));
				BudgetEntriesList.SelectedItem = null;
			}
				
		}

		/* Purpose: Refresh the list based on what is typed in search bar
		 * Input: object, TextChangedEventArgs
		 * Output: Filtered List
		 */
		private void BudgetSearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			var budgetsFilteredList = budgetEntriesList.Where(b => b.StoreName.Contains(textValues));
			BudgetEntriesList.ItemsSource = budgetsFilteredList;
		}
	}
}