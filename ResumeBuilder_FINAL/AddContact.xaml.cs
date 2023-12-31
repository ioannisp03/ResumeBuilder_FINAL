﻿using System;
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
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {
        public AddContact()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (fNameTextBox.Text != "" && lNameTextBox.Text != "" && ageTextBox.Text != "" && phoneNumberTextBox.Text != ""
                && emailTextBox.Text != "" && positionTextBox.Text != "" )
            {
                
                Contact addedContact = new Contact();
                addedContact.FirstName = fNameTextBox.Text;
                addedContact.LastName = lNameTextBox.Text;
                addedContact.Age = Convert.ToInt32(ageTextBox.Text);
                addedContact.PhoneNumber = phoneNumberTextBox.Text;
                addedContact.Email = emailTextBox.Text;
                addedContact.Position = positionTextBox.Text;

                ContactDBHandler db = ContactDBHandler.Instance;
                db.AddContact(addedContact);
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
