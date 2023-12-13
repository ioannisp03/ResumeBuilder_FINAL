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
using static System.Net.Mime.MediaTypeNames;

namespace ResumeBuilder_FINAL
{
    /// <summary>
    /// Interaction logic for AddExperience.xaml
    /// </summary>
    public partial class AddExperience : Window
    {


        public AddExperience()
        {
            InitializeComponent();
        }


        private void btnAddExperience_Click(object sender, RoutedEventArgs e)
        {

            if (CompagnyNameTextBox.Text != null && PositionTextBox.Text != null && FirstDayTextBox.Text != null && LastDayTextBox.Text != null) {
                Experience addedExperience = new Experience();

                addedExperience.CompagnyName = CompagnyNameTextBox.Text;
                addedExperience.Position = PositionTextBox.Text ;
                addedExperience.StartedDate = FirstDayTextBox.Text;
                addedExperience.EndedDate = LastDayTextBox.Text;

                ExperienceDBHandler experienceDB = ExperienceDBHandler.Instance;
                experienceDB.AddExperience(addedExperience);
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
