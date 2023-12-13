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

        ExperienceDBHandler experienceDB = ExperienceDBHandler.Instance;

        Experience experience;
        public UpdateExperience(Experience experience)
        {
            InitializeComponent();
            this.experience = experience;

            CompagnyNameTextBox.Text = experience.CompagnyName;
            PositionTextBox.Text = experience.Position;
            FirstDayTestBox.Text = experience.StartedDate;
            LastDayTextBox.Text = experience.EndedDate;
        }

        private void btnEditExperience_Click(object sender, RoutedEventArgs e)
        {
            experienceDB.UpdateExperience(experience);
        }
    }
}
