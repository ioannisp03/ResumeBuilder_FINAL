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
    /// Interaction logic for UpdateExperience.xaml
    /// </summary>
    public partial class UpdateExperience : Window
    {

        Experience experience;
        public UpdateExperience(Experience experience)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.experience = experience;

            CompanyNameTextBox.Text = experience.CompanyName;
            PositionTextBox.Text = experience.Position;
            FirstDayTestBox.Text = experience.StartedDate;
            LastDayTextBox.Text = experience.EndedDate;


        }

        private void btnEditExperience_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyNameTextBox.Text != null && PositionTextBox.Text != null && FirstDayTestBox.Text != null && LastDayTextBox.Text != null)
            {

                experience.CompanyName = CompanyNameTextBox.Text;
                experience.Position = PositionTextBox.Text;
                experience.StartedDate = FirstDayTestBox.Text;
                experience.EndedDate = LastDayTextBox.Text;

                ExperienceDBHandler experienceDB = ExperienceDBHandler.Instance;
                experienceDB.UpdateExperience(experience);

                ProfessionalExperienceWindow professionalExperienceWindow = Application.Current.Windows.OfType<ProfessionalExperienceWindow>().FirstOrDefault();
                professionalExperienceWindow.Close();
                Close();
            }
            else
            {
                MessageBox.Show("Need to have every field filled.",
                    "Null Fields Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
