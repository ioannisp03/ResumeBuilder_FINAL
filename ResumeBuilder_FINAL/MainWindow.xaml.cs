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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResumeBuilder_FINAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ContactDBHandler contactDBHandler = ContactDBHandler.Instance;
        List<Contact> contacts = new List<Contact>();
        
        EducationDBHandler educationDBHandler = EducationDBHandler.Instance;
        List<Education> educations = new List<Education>();

        public MainWindow()
        {
            InitializeComponent();

            RefreshAllResources();
            lblCreated.Content = DateTime.Now.ToString();
            lblUpdated.Content = DateTime.Now.ToString();
        }
         
        private void RefreshAllResources()
        {
            ResumeContact.ItemsSource = null;
            contacts = contactDBHandler.ReadAllContacts();
            ResumeContact.ItemsSource = contacts;

            ResumeExperience.ItemsSource = null;
            educations = educationDBHandler.ReadAllEducations();
            ResumeExperience.ItemsSource= educations;

            lblUpdated.Content = DateTime.Now.ToString();
        }

        private void ResumeContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact contact = (Contact)ResumeContact.SelectedItem;

            if(contact != null)
            {
                ContactInfoWindow contactInfoWindow = new ContactInfoWindow(contact);
                contactInfoWindow.ShowDialog();
                RefreshAllResources();
            }
        }

        private void ResumeExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ResumeEducation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            if (ResumeContact.Items == null)
            {
                AddContact addContact = new AddContact();
                addContact.ShowDialog();
                RefreshAllResources();
            }
            else
            {
                MessageBox.Show("Cannot create multiple contact informations.",
                    "Contact Information Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddExperience_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddEducation_Click(object sender, RoutedEventArgs e)
        {
            AddEducationWindow addEducationWindow = new AddEducationWindow();
            addEducationWindow.ShowDialog();
            RefreshAllResources();
        }

        private void ExportPDF_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
