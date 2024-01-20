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


		public StudentsPage()
		{
			InitializeComponent();
		}

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

		private async Task SetStudents()
		{
			List<Student> students = await App.Database.GetStudents();

			foreach(Student student in students)
			{
				studentsList.Add(student);
			}
		}

		public async void OnItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Student student = e.CurrentSelection.FirstOrDefault() as Student;
			if (student != null)
			{
				await Navigation.PushAsync(new StudentDetailPage(student));
				StudentsList.SelectedItem = null;
			}
		}


		private void RadioButtonClicked (object sender, EventArgs e)
		{
			RadioButton searchFilterButton = (RadioButton)sender;
			searchFilter = searchFilterButton.Content.ToString();
		}

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