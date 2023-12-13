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
    /// Interaction logic for ProfessionalExperienceWindow.xaml
    /// </summary>
    public partial class ProfessionalExperienceWindow : Window
    {

        Experience experience;

        ExperienceDBHandler experienceDB = ExperienceDBHandler.Instance;
        public ProfessionalExperienceWindow(Experience experience)
        {
            InitializeComponent();
            this.experience = experience;

            CompagnyNameTextBox.Text = experience.CompagnyName;
            PositionTextBox.Text = experience.Position;
            FirstDayTestBox.Text = experience.StartedDate;
            LastDayTextBox.Text = experience.EndedDate;
        }

        private void btnExperienceEdit_Click(object sender, RoutedEventArgs e)
        {
            UpdateExperience update = new UpdateExperience(experience);
            update.ShowDialog();
        }

        private void btnDeleteExperience_Click(object sender, RoutedEventArgs e)
        {
            experienceDB.DeleteExperience(experience);
            Close();
        }
    }
}
