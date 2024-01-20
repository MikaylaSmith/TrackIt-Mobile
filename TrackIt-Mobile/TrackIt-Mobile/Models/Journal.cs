/***********
* Class: Journal
*
* Purpose:
*	The purpose of this class is to manage the storage and display of data presented in the Journal formatting.
*
* Manager Functions:
*	Journal()
*		Create a blank class into which data can be entered and stored. 
*		
*
* Methods:
*	Setters - One for each member variable
*	Getters - One for each member variable
*
***********/
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackIt_Mobile.Models
{
	public class Journal : Log
	{
		public Journal()
		{

		}

		public void SetUserID(int value)
		{
			UserID = value; 
		}

		public int GetUserID()
		{
			return UserID; 
		}

		private int journalID;
		public int JournalID
		{
			get { return journalID; }
			set { journalID = value; }
		}

		private string title;
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		private string notes;
		public string Notes
		{
			get { return notes; }
			set { notes = value; }
		}
	}
}
