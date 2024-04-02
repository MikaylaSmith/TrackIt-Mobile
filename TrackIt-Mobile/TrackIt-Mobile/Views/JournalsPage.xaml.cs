/***********
* Class: JournalsPage
*
* Purpose:
*	The purpose of this class is to display the list of journal entries to the user
*
* Manager Functions:
*	JournalsPage()
*		Create an instance of the JournalsPage
*
* Methods:
*	OnAppearing()
*		Initialize the list when the page appears to the user
*	SetEntries()
*	    Query the database and set up the list
*	OnItemChanged(object, SelectionChangedEventArgs)
*	   Select item to display information for that selected item
*	JournalSearchBarChanged(object, TextChangedEventArgs)
*	    Refresh list for search results 
*
***********/using System;
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

		/* Purpose: Initialize the page
		 * Input: None
		 * Output: Page initialized
		 */
		public JournalsPage()
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

		/* Purpose: Runs async method to add query results to list
		 * Input: None
		 * Output: List
		 */
		public async Task SetEntries()
		{
			//Get the log information for the particular student
			List<Journal> journals = await App.Database.GetJournalEntries();

			foreach (Journal log in journals)
			{
				journalEntries.Add(log);
			}
		}

		/* Purpose: Actions for itms from list beng selected
		 * Input: object, SelectionChangedEventArgs
		 * Output: JournalsDetailPage visible
		 */
		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Journal journal = e.CurrentSelection.FirstOrDefault() as Journal;
			if (journal != null)
			{
				await Navigation.PushAsync(new JournalsDetailPage(journal));
				JournalEntriesList.SelectedItem = null;
			}
		}

		/* Purpose: Refresh the list based on what is typed in search bar
		 * Input: object, TextChangedEventArgs
		 * Output: Filtered List
		 */
		private void JournalSearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			var journalsFilteredList = journalEntries.Where(j => j.Title.ToLower().Contains(textValues));

			JournalEntriesList.ItemsSource = journalsFilteredList;
		}
	}
}