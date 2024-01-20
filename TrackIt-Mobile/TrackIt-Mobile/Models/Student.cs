/***********
* Class: Student
*
* Purpose:
*	The purpose of this class is to manage the storage and display of data presented in the Student formatting.
*
* Manager Functions:
*	Student()
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
	public class Student
	{
		public Student()
		{

		}

        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private int studentID;
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        private string studentName;
        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
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
    }
}
