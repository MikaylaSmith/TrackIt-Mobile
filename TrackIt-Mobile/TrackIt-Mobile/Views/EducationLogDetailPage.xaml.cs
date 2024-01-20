using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrackIt_Mobile.Models;

namespace TrackIt_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EducationLogDetailPage : ContentPage
	{
		public EducationLogDetailPage()
		{
			InitializeComponent();
		}

		public EducationLogDetailPage(Education log, string studentName)
		{
			InitializeComponent();

			//Display of student information
			StudentName.Text = studentName;
			SchoolName.Text = log.SchoolName;
			GradeLevel.Text = log.GradeLevel.ToString();

			//Display log information
			LogDate.Text = log.DateTimeInfo.ToString();
			SessionTime.Text = log.SessionTime.ToString();
			Notes.Text = log.Notes;
		}
	}
}