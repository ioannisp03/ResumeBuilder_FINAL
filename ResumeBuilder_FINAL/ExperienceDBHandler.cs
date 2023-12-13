using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ResumeBuilder_FINAL
{
    public sealed class ExperienceDBHandler
    {
        private static readonly ExperienceDBHandler instance = new ExperienceDBHandler();

        static readonly string ConString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        private ExperienceDBHandler() { }

        public static ExperienceDBHandler Instance { 
            get {  return instance; } 
        }

        public void CreateTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                string drop = "drop table if exists Experiences;";
                SQLiteCommand command1 = new SQLiteCommand(drop, con);
                command1.ExecuteNonQuery();

                string table = "create table Experiences (ID integer primary key, CompagnyName text, StartedDate text," +
                    "EndedDate text, Position text);";
                SQLiteCommand command2 = new SQLiteCommand(table, con);
                command2.ExecuteNonQuery();
            }
        }

        public int AddExperience(Experience experience)
        {
            int newId = 0;
            int rows = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                //create oarameterized query
                string query = "INSERT INTO Experiences (CompagnyName, StartedDate, EndedDate, Position) " +
                    "VALUES (@CompagnyName, @StartedDate, @EndedDate, @Position)";

                SQLiteCommand insertcom = new SQLiteCommand(query, con);

                // Pass values to the query parameters
                insertcom.Parameters.AddWithValue("@CompagnyName", experience.CompagnyName);
                insertcom.Parameters.AddWithValue("@StartedDate", experience.StartedDate);
                insertcom.Parameters.AddWithValue("@EndedDate", experience.EndedDate);
                insertcom.Parameters.AddWithValue("@Position", experience.Position);

                try
                {
                    rows = insertcom.ExecuteNonQuery();
                    //lets get the rowid inserted
                    insertcom.CommandText = "select last_insert_rowid()";
                    Int64 LastRowID64 = Convert.ToInt64(insertcom.ExecuteScalar());
                    //Then grab the bottom 32-bits as the unique id of the row
                    newId = Convert.ToInt32(LastRowID64);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
            }
            return newId;
        }

        public Experience GetPerson(int id)
        {
            Experience experience = new Experience();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                SQLiteCommand getcom = new SQLiteCommand("Select * from Experiences WHERE ID = @Id", con);
                getcom.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getcom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            experience.Id = id2;
                        }
                        experience.CompagnyName = reader["CompagnyName"].ToString();
                        experience.StartedDate = reader["StartedDate"].ToString();
                        experience.EndedDate = reader["EndedDate"].ToString();
                        experience.Position = reader["Position"].ToString();

                    }
                }
            }
            return experience;
        }

        public int UpdateExperience(Experience experience)
        {
            int row = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                string query = "Update Experiences Set CompagnyName = @CompagnyName, StartedDate = @StartedDate, EndedDate = @EndedDate, Position = @Position WHERE ID = @Id";

                SQLiteCommand updatecom = new SQLiteCommand(query, con);
                updatecom.Parameters.AddWithValue("@Id", experience.Id);
                updatecom.Parameters.AddWithValue("@FirstName", experience.CompagnyName);
                updatecom.Parameters.AddWithValue("@LastName", experience.StartedDate);
                updatecom.Parameters.AddWithValue("@City", experience.EndedDate);
                updatecom.Parameters.AddWithValue("@Age", experience.Position);

                try
                {
                    row = updatecom.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                }
            }
            return row;
        }

        public int DeleteExperience(Experience experience)
        {
            int row = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                string query = "DELETE FROM Persons WHERE ID = @Id";
                SQLiteCommand deletecom = new SQLiteCommand(query, con);
                deletecom.Parameters.AddWithValue("@Id", experience.Id);

                try
                {
                    row = deletecom.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
            }
            return row;
        }

        public List<Experience> ReadAllExperience()
        {
            List<Experience> listExperiences = new List<Experience>();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("SELECT * FROM Experiences", con);

                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Create a person object
                        Experience experience = new Experience();

                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            experience.Id = id2;
                        }
                        experience.CompagnyName = reader["CompagnyName"].ToString();
                        experience.StartedDate = reader["StartedDate"].ToString();
                        experience.EndedDate = reader["EndedDate"].ToString();
                        experience.Position = reader["Position"].ToString();


                        listExperiences.Add(experience);
                    }
                }
                return listExperiences;
            }
        }

    }
}
