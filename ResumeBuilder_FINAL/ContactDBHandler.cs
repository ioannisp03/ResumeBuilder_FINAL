using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

            using (SQLiteConnection con = new SQLiteConnection(ConString))
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
                    insertCom.CommandText = "select last_insert_rowid()";
                    Int64 LastRowID64 = Convert.ToInt64(insertCom.ExecuteScalar());
                    newId = Convert.ToInt32(LastRowID64);
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
            }
            return newId;
        }

        public Contact GetContact(int id)
        {
            Contact contact = new Contact();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                SQLiteCommand getCom = new SQLiteCommand("Select * from CONTACTS WHERE Id = @Id", con);
                getCom.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getCom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int idCheck))
                        {
                            contact.Id = idCheck;
                        }
                        contact.FirstName = reader["FirstName"].ToString();
                        contact.LastName = reader["LastName"].ToString();

                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            contact.Age = age;
                        }

                        contact.PhoneNumber = reader["PhoneNumber"].ToString();
                        contact.Email = reader["Email"].ToString();
                        contact.Position = reader["Position"].ToString();
                    }
                }
            }
            return contact;
        }

        public int UpdateContactInfo(Contact contact)
        {
            int row = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                string updateQuery = "UPDATE CONTACTS SET FirstName = @FirstName, LastName = @LastName, " +
                    "Age = @Age, PhoneNumber = @PhoneNumber, Email = @Email, Position = @Position WHERE Id = @Id";

                SQLiteCommand updateCom = new SQLiteCommand(updateQuery, con);

                updateCom.Parameters.AddWithValue("@Id", contact.Id);
                updateCom.Parameters.AddWithValue("@FirstName", contact.FirstName);
                updateCom.Parameters.AddWithValue("@LastName", contact.LastName);
                updateCom.Parameters.AddWithValue("@Age", contact.Age);
                updateCom.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                updateCom.Parameters.AddWithValue("@Email", contact.Email);
                updateCom.Parameters.AddWithValue("@Position", contact.Position);

                try
                {
                    row = updateCom.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }

                MainWindow mainWindow = new MainWindow();
                mainWindow.btnAddContact.IsEnabled = true;
                mainWindow.btnAddContact.Content = "Add contact information";
            }
            return row;
        }

        public int DeleteContact(Contact contact)
        {
            int row = 0;
            

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                SQLiteCommand deleteCom = new SQLiteCommand("DELETE FROM CONTACTS WHERE id = @Id", con);
                deleteCom.Parameters.AddWithValue("@Id", contact.Id);

                try
                {
                    deleteCom.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
               
            }
            return row;
        }

        public List<Contact> ReadAllContacts()
        {
            List<Contact> list = new List<Contact>();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("SELECT * FROM CONTACTS", con);

                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contact contact = new Contact();

                        if (Int32.TryParse(reader["Id"].ToString(), out int idChecking))
                        {
                            contact.Id = idChecking;
                        }

                        contact.FirstName = reader["FirstName"].ToString();
                        contact.LastName = reader["LastName"].ToString();

                        if (Int32.TryParse(reader["Age"].ToString(), out int age))
                        {
                            contact.Age = age;
                        }

                        contact.PhoneNumber = reader["PhoneNumber"].ToString();
                        contact.Email = reader["Email"].ToString();
                        contact.Position = reader["Position"].ToString();

                        list.Add(contact);
                    }
                }
                return list;
            }
        }

    }
}
