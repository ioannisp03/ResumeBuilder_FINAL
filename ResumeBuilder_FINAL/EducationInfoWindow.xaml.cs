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
    /// <summary>
    /// Interaction logic for EducationInfoWindow.xaml
    /// </summary>
    public partial class EducationInfoWindow : Window
    {
        Education education;
        public EducationInfoWindow(Education education)
        {
            InitializeComponent();
            this.education = education;
            academicDegreeTextBlock.Text = education.AcademicDegree;
            majorFieldOfStudyTextBlock.Text = education.Major_FieldOfStudy;
            institutionTextBlock.Text = education.InstitutionName;
            yearOfCompletionTextBlock.Text = education.YearOfCompletion.ToString();
            detailsTextBlock.Text = education.Details;



        }

        private void btnEditEducation_Click(object sender, RoutedEventArgs e)
        {
            UpdateEducationWindow updateEducationWindow = new UpdateEducationWindow(education);
            updateEducationWindow.ShowDialog();
            Close();
        }

        private void btnDeleteEducation_Click(object sender, RoutedEventArgs e)
        {
            EducationDBHandler educationDBHandler = EducationDBHandler.Instance;
            educationDBHandler.DeleteEducation(education);
            Close();
        }
    }
}
