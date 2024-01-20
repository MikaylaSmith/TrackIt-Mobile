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
	public partial class JournalsPage : ContentPage
	{
		private ObservableCollection<Journal> journalEntries;

		public JournalsPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			journalEntries = new ObservableCollection<Journal>();

			await SetEntries();

			if (journalEntries.Count == 0)
			{
				//The list is empty
				Label label = new Label
				{
					Text = "No data available.",
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center
				};

				JournalEntriesList.EmptyView = label;
			}
			else
			{
				JournalEntriesList.ItemsSource = journalEntries;
			}
		}

		public async Task SetEntries()
		{
			//Get the log information for the particular student
			List<Journal> journals = await App.Database.GetJournalEntries();

			foreach (Journal log in journals)
			{
				journalEntries.Add(log);
			}
		}

		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Journal journal = e.CurrentSelection.FirstOrDefault() as Journal;
			if (journal != null)
			{
				await Navigation.PushAsync(new JournalsDetailPage(journal));
				JournalEntriesList.SelectedItem = null;
			}
		}

		private void JournalSearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			var journalsFilteredList = journalEntries.Where(j => j.Title.ToLower().Contains(textValues));

			JournalEntriesList.ItemsSource = journalsFilteredList;
		}
	}
}