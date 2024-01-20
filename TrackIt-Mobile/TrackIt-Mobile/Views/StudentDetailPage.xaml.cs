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

		public StudentDetailPage()
		{
			InitializeComponent();
		}

		public StudentDetailPage(Student student)
		{
			InitializeComponent();
			SelectedStudent = student;
		}

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

		public async Task SetLogs(int studentID)
		{
			//Get the log information for the particular student
			List<Education> studentsLogs = await App.Database.GetEducationLogs(studentID);

			foreach(Education log in studentsLogs)
			{
				educationLogs.Add(log);
			}
		}

		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Education log = e.CurrentSelection.FirstOrDefault() as Education;
			if (log != null)
			{
				await Navigation.PushModalAsync(new EducationLogDetailPage(log, SelectedStudent.StudentName));
				EducationLogsCollectionView.SelectedItem = null;
			}
				
		}

		private void RadioButtonClicked(object sender, EventArgs e)
		{
			RadioButton searchFilterButton = (RadioButton)sender;
			searchFilter = searchFilterButton.Content.ToString();
		}

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