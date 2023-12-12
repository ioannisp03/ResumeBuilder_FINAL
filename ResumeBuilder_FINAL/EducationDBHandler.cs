using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder_FINAL
{
    //added this
    public sealed class EducationDBHandler
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private static readonly EducationDBHandler instance = new EducationDBHandler();

        private EducationDBHandler()
        {
            CreateTable();
            Education education1 = new Education
            {
                AcademicDegree = "MBAExample",
                Major_FieldOfStudy = "BusinessExample",
                InstitutionName = "ConcordiaExample",
                YearOfCompletion = 2010,
                Details = "Graduated with honors, received a 4.9/5.0 GPA"
            };
            AddEducation(education1);

        }

        public static EducationDBHandler Instance
        {
            get { return instance; }
        }

        public void CreateTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                string drop = "drop table if exists EDUCATIONS;";
                SQLiteCommand cmd1 = new SQLiteCommand(drop, con);
                cmd1.ExecuteNonQuery();

                string table = "create table EDUCATIONS (ID integer primary key, AcademicDegree text, Major_FieldOfStudy text, InstitutionName text, YearOfCompletion integer, Details text)";
                SQLiteCommand cmd2 = new SQLiteCommand(table, con);
                cmd2.ExecuteNonQuery();

            }
        }

        public int AddEducation(Education education)
        {
            int newId = 0;
            int rows = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {

                con.Open();

                string query = "INSERT INTO EDUCATIONS (AcademicDegree,Major_FieldOfStudy,InstitutionName,YearOfCompletion "
                    + "VALUES (@AcademicDegree, @Major_FieldOfStudy, @InstitutionName, @YearOfCompletion";

                SQLiteCommand addEducationCMD = new SQLiteCommand(query, con);
                addEducationCMD.Parameters.AddWithValue("@AcademicDegree", education.AcademicDegree);
                addEducationCMD.Parameters.AddWithValue("@Major_FieldOfStudy", education.Major_FieldOfStudy);
                addEducationCMD.Parameters.AddWithValue("@InstitutionName", education.InstitutionName);
                addEducationCMD.Parameters.AddWithValue("@YearOfCompletion", education.YearOfCompletion);
                addEducationCMD.Parameters.AddWithValue("@Details", education.Details);

                try
                {
                    rows = addEducationCMD.ExecuteNonQuery();
                    // get the rowid inserted
                    addEducationCMD.CommandText = "select last_insert_rowid()";
                    Int64 LastRowID64 = Convert.ToInt64(addEducationCMD.ExecuteScalar());
                    // grab the bottom 32-bits as the unique id of the row
                    newId = Convert.ToInt32(LastRowID64);
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
            }
            return newId;
        }

        public Education GetEducation(int id)
        {
            Education education = new Education();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand getEducationCMD = new SQLiteCommand("select * from EDUCATIONS where Id = @Id", con);
                getEducationCMD.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = getEducationCMD.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            education.Id = id2;
                        }
                        education.AcademicDegree = reader["AcademicDegree"].ToString();
                        education.Major_FieldOfStudy = reader["Major_FieldOfStudy"].ToString();
                        education.InstitutionName = reader["InstitutionName"].ToString();
                        education.Details = reader["Details"].ToString();


                        if (Int32.TryParse(reader["YearOfCompletion"].ToString(), out int age))
                        {
                            education.YearOfCompletion = age;
                        }
                    }
                }
            }
            return education;
        }

        public int UpdateEducation(Education education)
        {
            int row = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand updateEducationCMD = new SQLiteCommand("UPDATE EDUCATIONS SET AcademicDegree = @AcademicDegree, Major_FieldOfStudy = @Major_FieldOfStudy, " +
                    "InstitutionName = @InstitutionName, YearOfCompletion = @YearOfCompletion, Details = @Details WHERE Id = @Id", con);


                updateEducationCMD.Parameters.AddWithValue("@Id", education.Id);
                updateEducationCMD.Parameters.AddWithValue("@AcademicDegree", education.AcademicDegree);
                updateEducationCMD.Parameters.AddWithValue("@Major_FieldOfStudy", education.Major_FieldOfStudy);
                updateEducationCMD.Parameters.AddWithValue("@InstitutionName", education.InstitutionName);
                updateEducationCMD.Parameters.AddWithValue("@YearOfCompletion", education.YearOfCompletion);
                updateEducationCMD.Parameters.AddWithValue("@Details", education.Details);

                try
                {
                    row = updateEducationCMD.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }

            }
            return row;

        }

        public int DeleteEducation(Education education)
        {
            int row = 0;

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();

                SQLiteCommand deleteEducationCMD = new SQLiteCommand("DELETE FROM EDUCATIONS WHERE id = @Id", con);
                deleteEducationCMD.Parameters.AddWithValue("@Id", education.Id);

                try
                {
                    row = deleteEducationCMD.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error Generated. Details: " + ex.ToString());
                }
            }
            return row;
        }

        public List<Education> ReadAllEducations()
        {
            List<Education> listEducations = new List<Education>();

            using (SQLiteConnection con = new SQLiteConnection(ConString))
            {
                con.Open();
                SQLiteCommand com = new SQLiteCommand("SELECT * FROM EDUCATIONS", con);

                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Create a education object
                        Education education = new Education();

                        if (Int32.TryParse(reader["Id"].ToString(), out int id2))
                        {
                            education.Id = id2;
                        }
                        education.AcademicDegree = reader["AcademicDegree"].ToString();
                        education.Major_FieldOfStudy = reader["Major_FieldOfStudy"].ToString();
                        education.InstitutionName = reader["InstitutionName"].ToString();
                        education.Details = reader["Details"].ToString();

                        if (Int32.TryParse(reader["YearOfCompletion"].ToString(), out int age))
                        {
                            education.YearOfCompletion = age;
                        }

                        listEducations.Add(education);
                    }
                }
                return listEducations;
            }
        }

    }
}

