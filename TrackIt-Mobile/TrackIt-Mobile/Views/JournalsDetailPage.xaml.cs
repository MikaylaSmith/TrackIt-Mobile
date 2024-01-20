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
	public partial class JournalsDetailPage : ContentPage
	{
		public JournalsDetailPage()
		{
			InitializeComponent();
		}

		public JournalsDetailPage(Journal journal)
		{
			InitializeComponent();

			TitleContent.Text = journal.Title;
			NotesContent.Text = journal.Notes;
		}
	}
}