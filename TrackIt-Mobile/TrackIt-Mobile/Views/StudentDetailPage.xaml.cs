/***********
* Class: StudentDetailPage
*
* Purpose:
*	The purpose of this class is to show information about students
*
* Manager Functions:
*	StudentDetailPage()
*		Initialize a StudentsDetailPage 
*	StudentsDetailPage(Student)
*		Initialize a StudentsDetailPage with a certain student to search for 
*		
*
* Methods:
*	OnAppearing()
*		Initialize the list when the page appears to the user
*	SetLogs(int)
*	    Query the database and set up the list
*	OnItemChanged()
*	    Select item to display information for that selected item
*	RadioButtonClicked()
*	    Change what item is being searched on in the list
*	LogSearchBarChanged(object, TextChangedEventArgs)
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
using TrackIt_Mobile.Models;
using System.Collections.ObjectModel;

namespace TrackIt_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentDetailPage : ContentPage
	{
		private ObservableCollection<Education> educationLogs;
		private string searchFilter { get; set; }

		private Student SelectedStudent { get; set; }

		/* Purpose: Initialize the page
		 * Input: None
		 * Output: Page initialized
		 */
		public StudentDetailPage()
		{
			InitializeComponent();
		}

		/* Purpose: Initialize the page
		 * Input: Student
		 * Output: Page initialized
		 */
		public StudentDetailPage(Student student)
		{
			InitializeComponent();
			SelectedStudent = student;
		}

		/* Purpose: Queries the database to display information
		 * Input: None
		 * Output: Data is listed to be formated to user
		 */
		protected override async void OnAppearing()
		{
			base.OnAppearing();

			educationLogs = new ObservableCollection<Education>();

			await SetLogs(SelectedStudent.StudentID);

			if (educationLogs.Count == 0)
			{
				//The list is empty
				Label label = new Label
				{
					Text = "No data available.",
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center
				};

				EducationLogsCollectionView.EmptyView = label;
			}
			else
			{
				EducationLogsCollectionView.ItemsSource = educationLogs;
			}
		}

		/* Purpose: Runs async method to add query results to list
		 * Input: None
		 * Output: List
		 */
		public async Task SetLogs(int studentID)
		{
			//Get the log information for the particular student
			List<Education> studentsLogs = await App.Database.GetEducationLogs(studentID);

			foreach(Education log in studentsLogs)
			{
				educationLogs.Add(log);
			}
		}

		/* Purpose: Actions for itms from list beng selected
		 * Input: object, SelectionChangedEventArgs
		 * Output: EducationLogDetailPage visible
		 */
		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Education log = e.CurrentSelection.FirstOrDefault() as Education;
			if (log != null)
			{
				await Navigation.PushModalAsync(new EducationLogDetailPage(log, SelectedStudent.StudentName));
				EducationLogsCollectionView.SelectedItem = null;
			}
				
		}

		/* Purpose: Alter the filter option
		 * Input: object, EventArgs
		 * Output: Changed button
		 */
		private void RadioButtonClicked(object sender, EventArgs e)
		{
			RadioButton searchFilterButton = (RadioButton)sender;
			searchFilter = searchFilterButton.Content.ToString();
		}

		/* Purpose: Refresh the list based on what is typed in search bar
		 * Input: object, TextChangedEventArgs
		 * Output: Filtered List
		 */
		private void LogSearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			switch (searchFilter)
			{
				case "Time":
					var schoolFilteredList = educationLogs.Where(s => s.SessionTime.ToString().ToLower().Contains(textValues));

					EducationLogsCollectionView.ItemsSource = schoolFilteredList;
					break;
				default:
					var gradeFilteredList = educationLogs.Where(l => l.DateTimeInfo.ToString().ToLower().Contains(textValues));

					EducationLogsCollectionView.ItemsSource = gradeFilteredList;
					break;
			}
		}
	}
}