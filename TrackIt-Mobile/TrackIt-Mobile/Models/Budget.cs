/***********
* Class: Budget
*
* Purpose:
*	The purpose of this class is to manage the storage and display of data presented in the Budget formatting.
*
* Manager Functions:
*	Budget()
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
	public class Budget : Log
	{
		public Budget()
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

        private int budgetID;
        public int BudgetID
        {
            get { return budgetID; }
            set { budgetID = value; }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private string storeName;
        public string StoreName
        {
            get { return storeName; }
            set { storeName = value; }
        }

        private float amountSpent;
        public float AmountSpent
        {
            get { return amountSpent; }
            set { amountSpent = value; }
        }
    }
}
