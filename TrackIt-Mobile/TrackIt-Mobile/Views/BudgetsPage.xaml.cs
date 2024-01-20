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

		public BudgetsPage()
		{
			InitializeComponent();
		}

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

		private async Task SetBudgetEntries()
		{
			List<Budget> budgets = await App.Database.GetBudgetEntries();

			foreach (Budget budget in budgets)
			{
				budgetEntriesList.Add(budget);
			}
		}

		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Budget budget = e.CurrentSelection.FirstOrDefault() as Budget;
			if (budget != null)
			{
				await Navigation.PushAsync(new BudgetsDetailPage(budget));
				BudgetEntriesList.SelectedItem = null;
			}
				
		}

		private void BudgetSearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			var budgetsFilteredList = budgetEntriesList.Where(b => b.StoreName.Contains(textValues));
			BudgetEntriesList.ItemsSource = budgetsFilteredList;
		}
	}
}