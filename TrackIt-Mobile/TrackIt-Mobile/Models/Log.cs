/***********
* Class: Log
*
* Purpose:
*	The purpose of this abstract class is to manage the storage of the user ID that is assigned to any given log format.
*
* Manager Functions:
*	None
*		
*
* Methods:
*	Setter
*	Getter
*
***********/
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackIt_Mobile.Models
{
	public abstract class Log
	{
		protected int userID;
		public int UserID
		{
			get { return userID; }
			set { userID = value; }
		}
	}
}
