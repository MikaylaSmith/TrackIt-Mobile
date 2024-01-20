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

		public BudgetsDetailPage()
		{
			InitializeComponent();
		}

		public BudgetsDetailPage(Budget budget)
		{
			InitializeComponent();
			Storename = budget.StoreName;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			budgets = new ObservableCollection<Budget>();
			
			await SetStoreBudgetEntries();

			BudgetEntriesCollectionView.ItemsSource = budgets;
		}

		private async Task SetStoreBudgetEntries()
		{
			List<Budget> budgetEntriesForStore = await App.Database.GetBudgetEntriesForStore(Storename);

			foreach(Budget budget in budgetEntriesForStore)
			{
				budgets.Add(budget);
			}
		}

		private void BudgetEntrySearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			var budgetsFilteredList = budgets.Where(b => b.Date.ToString().ToLower().Contains(textValues));
			BudgetEntriesCollectionView.ItemsSource = budgetsFilteredList;
		}
	}
}