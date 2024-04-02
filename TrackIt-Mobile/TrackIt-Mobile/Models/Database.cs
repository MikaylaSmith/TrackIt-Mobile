/***********
* Class: Database
*
* Purpose:
*	The purpose of this class is to manage the interactions between users and the attached database.
*
* Manager Functions:
*	Database()
*		Create a Database object
*		
*
* Methods:
*	GetUser(string, string)
*	    Queries the database to log a user in
*	    
*	GetStudents()
*	    Queries the database for all of the students information assigned to the currently signed in user. 
*	    Returns a list of all of the student objects based on what was pulled from the database. 
*	GetEducationLogs(int)
*	    Queries the database for the logs for a particular student 
*	    
*	GetJournalEntries()
*	    Queries the database for the journal entries
*	    
*   GetBudgetEntries()
*	    Queries the database for the budget entries
*	GetBudgetEntriesByStore(string)
*	    Queries the database for the entries for a particular store
*
***********/

using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using System.Threading.Tasks;

namespace TrackIt_Mobile.Models
{
    public class Database
    {
        public Database()
        {
            
        }
        
        private static readonly string connectionString = $"Server={MySQLEnvironment.Hostname};Port={MySQLEnvironment.Port};Database={MySQLEnvironment.DBName};Uid={MySQLEnvironment.Username};Pwd={MySQLEnvironment.Password};";

        /* Purpose: Query for a user's information
		 * Input: string, string
		 * Output: User infromation
		 */
        public async Task GetUser(string username, string password)
		{
            //If got to this function, then there is no chance of injection
            string query;
            int queriedUserID = -1;
            string queriedPassword = string.Empty;

            //Get user ID
            using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
                connection.Open();

                query = $"SELECT id FROM users WHERE username=\'{username}\' LIMIT 1;";
                using (MySqlCommand commands = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = commands.ExecuteReader())
                    {
                        //Get result for user ID
                        while (await reader.ReadAsync())
                        {
                            queriedUserID = reader.GetInt32(0);
                        }
                    }
                }

                connection.Close();
            }

            //Get password stored in database. 
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                
                query = $"SELECT password FROM passwords_for_users WHERE user_id = {queriedUserID} LIMIT 1;";
                using (MySqlCommand commands = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = await commands.ExecuteReaderAsync())
                    {
                        //Read the results for password
                        while (await reader.ReadAsync())
                        {
                            queriedPassword = reader.GetString(0);
                        }
                    }
                }
                connection.Close();
            }

            //Check if the password stored in the db matches 
            if (queriedPassword == password)
			{
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    //Found the proper user so get all of their information
                    query = $"SELECT * FROM users WHERE username=\'{username}\' LIMIT 1;";
                    using (MySqlCommand commands = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = await commands.ExecuteReaderAsync())
                        {
                            //Check if there are any matches found in the database
                            if (reader.HasRows)
                            {
                                //If there are results, fill out the returning value
                                string tempStudentAccess = string.Empty;
                                string tempJournalAccess = string.Empty;
                                string tempBudgetAccess = string.Empty;

                                /* Query Results Values
                                    user_id = 0
                                    email = 1
                                    username = 2
                                    studentAccess = 3
                                    journalAccess = 4
                                    budgetAccess = 5
                                 */

                                while (await reader.ReadAsync())
                                {
                                    App.User.UserID = reader.GetInt32(0);
                                    App.User.Email = reader.GetString(1);
                                    App.User.Username = reader.GetString(2);
                                    tempStudentAccess = reader.GetString(3);
                                    tempJournalAccess = reader.GetString(4);
                                    tempBudgetAccess = reader.GetString(5);
                                }

                                //Take the string from the ENUM value in the db and convert strings
                                //Into a boolean value.
                                App.User.StudentAccess = Convert.ToBoolean(tempStudentAccess);
                                App.User.JournalAccess = Convert.ToBoolean(tempJournalAccess);
                                App.User.BudgetAccess = Convert.ToBoolean(tempBudgetAccess);
                            }
                            else
                            {
                                //If no results returned, then set the user ID to -1 (a not possible user ID)
                                App.User.UserID = -1;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            else
			{
                //If not matching passwords, then set the user ID to -1 (a not possible user ID)
                App.User.UserID = -1;
            }
		}

        /* Purpose: Query for education log information
		 * Input: int
		 * Output: List of education logs
		 */
        public async Task<List<Education>> GetEducationLogs(int studentID)
		{
            List<Education> AllLogs = new List<Education>();

            //Execute the query and get values from the database.
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                //Run Query to get all of the logs
                string query = $"SELECT * FROM educationLogs WHERE user_id = {App.User.UserID} AND student_id = {studentID} ORDER BY log_date DESC;";
                using (MySqlCommand commands = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = await commands.ExecuteReaderAsync())
                    {
                        //Go through the list of results, make new Education objects
                        //Then insert into the list
                        while (await reader.ReadAsync())
                        {
                            /* Query Results Values
                                LogID - 0
                                UserID - 1
                                StudentID - 2
                                DateTimeInfo - 3
                                SchoolName - 4
                                Grade - 5
                                SessionTime - 6
                                Notes - 7
                            */

                            Education log = new Education
                            {
                                LogID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                StudentID = reader.GetInt32(2),
                                DateTimeInfo = reader.GetDateTime(3),
                                SchoolName = reader.GetString(4),
                                GradeLevel = reader.GetInt32(5),
                                SessionTime = reader.GetInt32(6),
                                Notes = reader.GetString(7)
                            };
                            AllLogs.Add(log);
                        }
                    }
                }
                connection.Close();
            }
            return AllLogs;
        }

        /* Purpose: Query for student information
		 * Input: None
		 * Output: List of student information
		 */
        public async Task<List<Student>> GetStudents()
        {
            List<Student> AllStudents = new List<Student>();

            //Execute the query and get values from the database.
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM students WHERE user_id = {App.User.UserID} ORDER BY student_name ASC;";
                using (MySqlCommand commands = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = await commands.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            /* Query Result Values
                                StudentID - 0
                                UserID - 1
                                StudentName = 2
                                SchoolName - 3
                                GradeLevel - 4
                            */

                            Student student = new Student
                            {
                                StudentID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                StudentName = reader.GetString(2),
                                SchoolName = reader.GetString(3),
                                GradeLevel = reader.GetInt32(4)
                            };

                            AllStudents.Add(student);
                        }
                    }
                }

                connection.Close();
            }

            return AllStudents;
        }

        /* Purpose: Query for journal entries
		 * Input: None
		 * Output: List of journal entries
		 */
        public async Task<List<Journal>> GetJournalEntries()
        {
            List<Journal> AllEntries = new List<Journal>();

            //Execute the query and get values from the database.
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM journalLogs WHERE user_id = {App.User.UserID} ORDER BY title ASC;";
                using (MySqlCommand commands = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = await commands.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            /* Query Result Values
                                LogID - 0
                                UserID - 1
                                Title = 2
                                Notes - 3
                            */

                            Journal journal = new Journal
                            {
                                JournalID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                Title = reader.GetString(2),
                                Notes = reader.GetString(3)
                            };

                            AllEntries.Add(journal);
                        }
                    }
                }
                connection.Close();
            }
            return AllEntries;
        }

        /* Purpose: Query for budget entries
		 * Input: None
		 * Output: List of budget entries
		 */
        public async Task<List<Budget>> GetBudgetEntries()
        {
            List<Budget> AllBudgetEntries = new List<Budget>();

            //Execute the query and get values from the database.
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT store_name, SUM(amount_spent) AS total_amount, MAX(entry_date) AS latest_date FROM budgetLogs WHERE user_id = {App.User.UserID} GROUP BY store_name ORDER BY store_name ASC;";
                using (MySqlCommand commands = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = await commands.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            /* Query Result Values
                                StoreName - 0
                                AmountSpent - 1
                                LastDate = 2
                            */

                            Budget entry = new Budget
                            {
                                BudgetID = 0,
                                UserID = App.User.UserID,
                                StoreName = reader.GetString(0),
                                AmountSpent = reader.GetInt32(1),
                                Date = reader.GetDateTime(2)
                            };

                            AllBudgetEntries.Add(entry);
                        }
                    }
                }
                connection.Close();
            }
            return AllBudgetEntries;
        }

        /* Purpose: Query for budget entries by a particular store name
		 * Input: string
		 * Output: List of budget entries for a given store
		 */
        public async Task<List<Budget>> GetBudgetEntriesForStore(string storeName)
        {
            List<Budget> AllBudgetEntries = new List<Budget>();

            //Execute the query and get values from the database.
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM budgetLogs WHERE user_id = {App.User.UserID} AND store_name = \"{storeName}\" ORDER BY entry_date DESC;";
                using (MySqlCommand commands = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = await commands.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            /* Query Result Values
                                BudgetID - 0
                                UserID - 1
                                Date = 2
                                StoreName - 3
                                AmountSpent - 4
                            */

                            Budget entry = new Budget
                            {
                                BudgetID = reader.GetInt32(0),
                                UserID = reader.GetInt32(1),
                                Date = reader.GetDateTime(2),
                                StoreName = reader.GetString(3),
                                AmountSpent = reader.GetInt32(4)
                            };

                            AllBudgetEntries.Add(entry);
                        }
                    }
                }
                connection.Close();
            }
            return AllBudgetEntries;
        }
    }
}
