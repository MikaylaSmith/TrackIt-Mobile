/***********
* Class: StudentsPage
*
* Purpose:
*	The purpose of this class is to display the list of students to the user
*
* Manager Functions:
*	StudentsPage()
*		Create an instance of the StudentsPage
*		
*
* Methods:
*	OnAppearing()
*		Initialize the list when the page appears to the user
*	SetStudents()
*	    Query the database and set up the list
*	OnItemChanged()
*	    Select item to display information for that selected item
*	RadioButtonClicked()
*	    Change what item is being searched on in the list
*	StudentSearchBarChanged(object, TextChangedEventArgs)
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
	public partial class StudentsPage : ContentPage
	{
		ObservableCollection<Student> studentsList;
		private string searchFilter { get; set; }

		/* Purpose: Initialize the page
		 * Input: None
		 * Output: Page initialized
		 */
		public StudentsPage()
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

			studentsList = new ObservableCollection<Student>();

			await SetStudents();

			if (studentsList.Count == 0)
			{
				//The list is empty
				Label label = new Label
				{
					Text = "No data available.",
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center
				};

				StudentsList.EmptyView = label;
			}
			else
			{
				StudentsList.ItemsSource = studentsList;
			}
		}

		/* Purpose: Runs async method to add query results to list
		 * Input: None
		 * Output: List
		 */
		private async Task SetStudents()
		{
			List<Student> students = await App.Database.GetStudents();

			foreach(Student student in students)
			{
				studentsList.Add(student);
			}
		}

		/* Purpose: Actions for itms from list beng selected
		 * Input: object, SelectionChangedEventArgs
		 * Output: StudentDetailPage visible
		 */
		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Student student = e.CurrentSelection.FirstOrDefault() as Student;
			if (student != null)
			{
				await Navigation.PushAsync(new StudentDetailPage(student));
				StudentsList.SelectedItem = null;
			}
		}

		/* Purpose: Alter the filter option
		 * Input: object, EventArgs
		 * Output: Changed button
		 */
		private void RadioButtonClicked (object sender, EventArgs e)
		{
			RadioButton searchFilterButton = (RadioButton)sender;
			searchFilter = searchFilterButton.Content.ToString();
		}

		/* Purpose: Refresh the list based on what is typed in search bar
		 * Input: object, TextChangedEventArgs
		 * Output: Filtered List
		 */
		private void StudentSearchBarChanged(object sender, TextChangedEventArgs e)
		{
			string textValues = e.NewTextValue.ToLower();
			switch(searchFilter)
			{
				case "Grade Level":
					var gradeFilteredList = studentsList.Where(s => s.GradeLevel.ToString().Contains(textValues));

					StudentsList.ItemsSource = gradeFilteredList;
					break;
				case "School":
					var schoolFilteredList = studentsList.Where(s => s.SchoolName.ToLower().Contains(textValues));

					StudentsList.ItemsSource = schoolFilteredList;
					break;
				default:
					var studentNameFilteredList = studentsList.Where(s => s.StudentName.ToLower().Contains(textValues));

					StudentsList.ItemsSource = studentNameFilteredList;
					break;
			}
		}
	}
}