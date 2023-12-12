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
    /// Interaction logic for ContactInfoWindow.xaml
    /// </summary>
    public partial class ContactInfoWindow : Window
    {
        Contact contact;
        public ContactInfoWindow(Contact contact)
        {
            InitializeComponent();
            this.contact = contact;

            fNameTextBlock.Text = contact.FirstName;
            lNameTextBlock.Text = contact.LastName;
            ageTextBlock.Text = contact.Age.ToString();
            phoneNumberTextBlock.Text = contact.PhoneNumber;
            emailTextBlock.Text = contact.Email;
            positionTextBlock.Text = contact.Position;
        }

        private void btnEditContact_Click(object sender, RoutedEventArgs e)
        {
            UpdateContact updateWindow = new UpdateContact(contact);
            updateWindow.ShowDialog();
            Close();
        }

        private void btnDeleteContact_Click(object sender, RoutedEventArgs e)
        {
            ContactDBHandler db = ContactDBHandler.Instance;
            db.DeleteContact(contact);
            Close();
        }
    }
}
