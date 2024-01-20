/***********
* Class: Education
*
* Purpose:
*	The purpose of this class is to manage the storage and display of data presented in the Education formatting.
*
* Manager Functions:
*	Education()
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
	public class Education : Log
	{
		public Education()
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

        private int logID;
        public int LogID
        {
            get { return logID; }
            set { logID = value; }
        }

        private DateTime dateTimeInfo;
        public DateTime DateTimeInfo
        {
            get { return dateTimeInfo; }
            set { dateTimeInfo = value; }
        }

        private int studentID;
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        private string schoolName;
        public string SchoolName
        {
            get { return schoolName; }
            set { schoolName = value; }
        }

        private int gradeLevel;
        public int GradeLevel
        {
            get { return gradeLevel; }
            set { gradeLevel = value; }
        }

        private int sessionTime;
        public int SessionTime
        {
            get { return sessionTime; }
            set { sessionTime = value; }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
    }
}
