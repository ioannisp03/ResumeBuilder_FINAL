using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResumeBuilder_FINAL
{
   
    public partial class UpdateEducationWindow : Window
    {
        Education education;
        public UpdateEducationWindow(Education education)
        {
            InitializeComponent();
            this.education = education;

            //display information in textbox
            academicDegreeTextBox.Text = education.AcademicDegree;
            majorFieldOfStudyTextBox.Text = education.Major_FieldOfStudy;
            institutionNameTextBox.Text = education.InstitutionName;
            completionYearTextBox.Text = education.YearOfCompletion.ToString();
            detailsTextBox.Text = education.Details;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            education.AcademicDegree = academicDegreeTextBox.Text;
            education.Major_FieldOfStudy = majorFieldOfStudyTextBox.Text;
            education.InstitutionName = institutionNameTextBox.Text;
            education.YearOfCompletion = Convert.ToInt32(completionYearTextBox.Text);

            EducationDBHandler educationDBHandler = EducationDBHandler.Instance;
            educationDBHandler.UpdateEducation(education);
            Close();
            Close();
        }
    }
}
