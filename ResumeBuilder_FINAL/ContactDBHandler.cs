using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder_FINAL
{
    public sealed class ContactDBHandler
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private static readonly ContactDBHandler instance = new ContactDBHandler();

        private ContactDBHandler()
        {
            CreateContactTable();
        }

        public static ContactDBHandler Instance
        {
            get { return instance; }
        }

        public void CreateContactTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand dropCommand = new SQLiteCommand("drop table if exists CONTACTS;", con);
                dropCommand.ExecuteNonQuery();

                string contactTable = "create table CONTACTS (ID integer primary key, FirstName text, LastName text," +
                    "Age integer, PhoneNumber text, Email text, Position text)";
                SQLiteCommand createCommand = new SQLiteCommand(contactTable, con);
                createCommand.ExecuteNonQuery();
            }
        }

        public int AddContact(Contact contact) 
        { 
            int newId = 0;
            int rows = 0;

            using (SQLiteConnection con = new SQLiteConnection())
            {
                con.Open();

                string insertQuery = "INSERT INTO CONTACTS (FirstName, LastName, Age, PhoneNumber, Email, Position) " +
                "VALUES (@FirstName, @LastName, @Age, @PhoneNumber, @Email, @Position)";

                SQLiteCommand insertCom = new SQLiteCommand(insertQuery, con);

                insertCom.Parameters.AddWithValue("@FirstName", contact.FirstName);
                insertCom.Parameters.AddWithValue("@LastName", contact.LastName);
                insertCom.Parameters.AddWithValue("@Age", contact.Age);
                insertCom.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                insertCom.Parameters.AddWithValue("@Email", contact.Email);
                insertCom.Parameters.AddWithValue("@Position", contact.Position);

                try
                {
                    rows = insertCom.ExecuteNonQuery();
                }
                catch (SQLiteException ex) 
                { 
                
                }
            }

            return newId;
        }

    }
}
