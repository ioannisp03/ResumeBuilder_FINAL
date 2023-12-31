﻿using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder_FINAL
{public static class ExportToPDF
    {
        public static void exportToPDF(List<Contact> contactInfo, List<Experience> experiences, List<Education> diplomas)
        {
            Contact currentContact = contactInfo[0];

            PdfDocument pdfDocument = new PdfDocument();
            pdfDocument.Info.Title = $"{currentContact.FirstName} {currentContact.LastName} Resume";

            PdfPage page = pdfDocument.AddPage();
            page.Size = PdfSharp.PageSize.Letter;

            // -- styling --

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new XFont("Arial", 30, XFontStyleEx.Bold);
            XFont fontCategories = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
            XFont fontRegular = new XFont("Times New Roman", 11, XFontStyleEx.Regular);

            XTextFormatter tf = new XTextFormatter(gfx);

            XRect rect = new XRect(0, 0, page.Width, page.Height);
            gfx.DrawRectangle(XBrushes.DarkGray, rect);

            // Title
            rect = new XRect(0, 10, page.Width - 20, 50);
            tf.Alignment = XParagraphAlignment.Center;
            string title = $"{currentContact.FirstName} {currentContact.LastName}";
            tf.DrawString(title, fontTitle, XBrushes.Gold, rect);

            // Contact Information
            string contactText = $"{currentContact.Position}\n{currentContact.PhoneNumber} | {currentContact.Email} | {currentContact.Age} years old";
            rect = new XRect(0, 70, page.Width - 20, 30);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString(contactText, fontRegular, XBrushes.White, rect);

            // Professional Experience
            rect = new XRect(0, 110, page.Width - 20, 30);
            tf.Alignment = XParagraphAlignment.Center;
            string subtitleExperience = "Professional Experience";
            tf.DrawString(subtitleExperience, fontCategories, XBrushes.Gold, rect);

            string experiencesText = "";
            foreach (Experience exp in experiences)
            {
                experiencesText += $"\n{exp.Position} ({exp.StartedDate}-{exp.EndedDate}) at {exp.CompanyName}";
            }
            rect = new XRect(0, 140, page.Width - 20, 220);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(experiencesText, fontRegular, XBrushes.White, rect, XStringFormats.TopLeft);

            // Education
            rect = new XRect(0, 380, page.Width - 20, 30);
            tf.Alignment = XParagraphAlignment.Center;
            string subtitleEducation = "Education";
            tf.DrawString(subtitleEducation, fontCategories, XBrushes.Gold, rect);

            string educationText = "";
            foreach (Education education in diplomas)
            {
                educationText += $"\n{education.AcademicDegree}: {education.Major_FieldOfStudy} - {education.InstitutionName}, {education.YearOfCompletion}\n\t{education.Details}";
            }

            rect = new XRect(0, 410, page.Width - 20, 220);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(educationText, fontRegular, XBrushes.White, rect, XStringFormats.TopLeft);

            // Save PDF
            const string filename = "Resume.pdf";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files(*.pdf)|*.pdf|All files(*.*)|*.*";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = filename;
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                pdfDocument.Save(saveFileDialog.FileName);
            }
        }
    }

}
