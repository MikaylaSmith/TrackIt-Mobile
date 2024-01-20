/***********
* Class: MySQLEnvironment
*
* Purpose:
*	The purpose of this class is to manage the database connection information.
*
* Manager Functions:
*	None
*		
*
* Methods:
*	None
*
***********/
using System;
using System.Collections.Generic;
using System.Text;

namespace TrackIt_Mobile.Models
{
	class MySQLEnvironment
	{
		public const string IdentityPool = "us-west-2:9e7b2228-a31e-4f43-aedc-f339fe2fefd3";
		public const string Hostname = "track-it.chyyw8mkct6p.us-west-2.rds.amazonaws.com";
		public const string Username = "rootAdmin";
		public const string Password = "3rmunWhK657QEM9g56qQ";
		public const string DBName = "trackit";
		public const string Port = "3306";
	}
}
