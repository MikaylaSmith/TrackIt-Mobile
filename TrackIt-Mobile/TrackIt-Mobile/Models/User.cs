/***********
* Class: User
*
* Purpose:
*	The purpose of this class is to manage signed in user's information and access
*
* Manager Functions:
*	User()
*		Create a new user object within which new user data can be stored
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
using System.ComponentModel;

namespace TrackIt_Mobile.Models
{
	public class User : INotifyPropertyChanged
	{
		public User()
		{

		}

        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private bool studentAccess;
        public bool StudentAccess
        {
            get { return studentAccess; }
            set 
            { 
                if (studentAccess != value)
				{
                    studentAccess = value;
                    OnPropertyChanged(nameof(StudentAccess));
                }
            }
        }

        private bool journalAccess;
        public bool JournalAccess
        {
            get { return journalAccess; }
            set 
            { 
                if(journalAccess != value)
				{
                    journalAccess = value;
                    OnPropertyChanged(nameof(JournalAccess));
				}
            }
        }

        private bool budgetAccess;
        public bool BudgetAccess
        {
            get { return budgetAccess; }
            set
            { 
                if(budgetAccess != value)
				{
                    budgetAccess = value;
                    OnPropertyChanged(nameof(BudgetAccess));
				}
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /* Purpose: Changes the property so that only certain items can be seen by who is allowed to see them. 
		 * Input: The string for the property being changed
		 * Output: Property gets changed.
		 */
        protected virtual void OnPropertyChanged(string propertyName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
    }
}
