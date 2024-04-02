/***********
* Class: JournalsDetailPage
*
* Purpose:
*	The purpose of this class is to show information about a selected log
*
* Manager Functions:
*	JournalsDetailPage()
*		Initialize a JournalsDetailPage 
*	JournalsDetailPage(Journal)
*		Initialize a JournalsDetailPage with a specific entry
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

namespace TrackIt_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JournalsDetailPage : ContentPage
	{
		/* Purpose: Initialize the page
		 * Input: None
		 * Output: Page initialized
		 */
		public JournalsDetailPage()
		{
			InitializeComponent();
		}

		/* Purpose: Initialize the page
		 * Input: Journal
		 * Output: Page initialized
		 */
		public JournalsDetailPage(Journal journal)
		{
			InitializeComponent();

			TitleContent.Text = journal.Title;
			NotesContent.Text = journal.Notes;
		}
	}
}