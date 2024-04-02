/***********
* Class: BudgetsDetailPage
*
* Purpose:
*	The purpose of this class is to show information about certain entries in a particular store
*
* Manager Functions:
*	BudgetsDetailPage()
*		Initialize a BudgetsDetailPage 
*	BudgetsDetailPage(Budget)
*		Initialize a BudgetsDetailPage with a certain store name to search for 
*		
*
* Methods:
*	OnAppearing()
*		Initialize the list when the page appears to the user
*	SetStoreBudgetEntries()
*	    Query the database and set up the list
*	BudgetEntrySearchBarChanged(object, TextChangedEventArgs)
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
	public partial class BudgetsDetailPage : ContentPage
	{
		ObservableCollection<Budget> budgets;
		private string Storename { get; set; }

		/* Purpose: Initialize the page
		 * Input: None
		 * Output: Page initialized
		 */
		public BudgetsDetailPage()
		{
			InitializeComponent();
		}

		/* Purpose: Initialize the page
		 * Input: Budget
		 * Output: Page initialized
		 */
		public BudgetsDetailPage(Budget budget)
		{
			InitializeComponent();
			Storename = budget.StoreName;
		}

		/* Purpose: Queries the database to display information
		 * Input: None
		 * Output: Data is listed to be formated to user
		 */
		protected override async void OnAppearing()
		{
			base.OnAppearing();

			budgets = new ObservableCollection<Budget>();
			
			await SetStoreBudgetEntries();

			BudgetEntriesCollectionView.ItemsSource = budgets;
		}

		/* Purpose: Runs async method to add query results to list
		 * Input: None
		 * Output: List
		 */
		private async Task SetStoreBudgetEntries()
		{
			List<Budget> budgetEntriesForStore = await App.Database.GetBudgetEntriesForStore(Storename);

			foreach(Budget budget in budgetEntriesForStore)
			{
				budgets.Add(budget);
			}
		}

		/* Purpose: Refresh the list based on what is typed in search bar
		 * Input: object, TextChangedEventArgs
		 * Output: Filtered List
		 */
		private void BudgetEntrySearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			var budgetsFilteredList = budgets.Where(b => b.Date.ToString().ToLower().Contains(textValues));
			BudgetEntriesCollectionView.ItemsSource = budgetsFilteredList;
		}
	}
}