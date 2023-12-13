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
    /// Interaction logic for UpdateContact.xaml
    /// </summary>
    public partial class UpdateContact : Window
    {
        Contact contact;
        public UpdateContact(Contact contact)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

            this.contact = contact;

            fNameTextBox.Text = contact.FirstName;
            lNameTextBox.Text = contact.LastName;
            ageTextBox.Text = contact.Age.ToString();
            phoneNumberTextBox.Text = contact.PhoneNumber;
            emailTextBox.Text = contact.Email;
            positionTextBox.Text = contact.Position;
        }

        private void btnContactChanges_Click(object sender, RoutedEventArgs e)
        {
            if (fNameTextBox.Text != "" && lNameTextBox.Text != "" && ageTextBox.Text != "" && phoneNumberTextBox.Text != ""
                && emailTextBox.Text != "" && positionTextBox.Text != "")
            {
                contact.FirstName = fNameTextBox.Text;
                contact.LastName = lNameTextBox.Text;
                contact.Age = Convert.ToInt32(ageTextBox.Text);
                contact.PhoneNumber = phoneNumberTextBox.Text;
                contact.Email = emailTextBox.Text;
                contact.Position = positionTextBox.Text;

                ContactDBHandler db = ContactDBHandler.Instance;

                db.UpdateContactInfo(contact);

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
