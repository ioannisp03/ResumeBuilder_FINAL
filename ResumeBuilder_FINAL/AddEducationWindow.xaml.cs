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
    
    public partial class AddEducationWindow : Window
    {
        public AddEducationWindow()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            Education newEducation = new Education();
            newEducation.AcademicDegree = academicDegree.Text;
            newEducation.Major_FieldOfStudy = majorFieldOfStudy.Text;
            newEducation.InstitutionName = institutionName.Text;
            newEducation.YearOfCompletion = Convert.ToInt32(completionYear.Text);
            newEducation.Details = details.Text;


            EducationDBHandler educationDBHandler = EducationDBHandler.Instance;
            educationDBHandler.AddEducation(newEducation);
            Close();
        }
    }
}
//updateEducation,AddEducation
//AddContact,ContactInfoWindow,updateContact