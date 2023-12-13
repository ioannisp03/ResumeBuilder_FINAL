using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder_FINAL
{
    public static class ExportToPDF
    {
        public static void exportToPDF(List<Contact> contactInfo, List<Experience> experiences, List<Education> diplomas)
        {
            Contact currentContact = new Contact();
            currentContact = contactInfo[0];

            PdfDocument pdfDocument = new PdfDocument();
            pdfDocument.Info.Title = currentContact.FirstName + " " + currentContact.LastName + " Resume";

            PdfPage page = pdfDocument.AddPage();
            page.Size = PdfSharp.PageSize.Letter;

            // -- styling --

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new XFont("Arial", 30, XFontStyleEx.Bold);
            XFont fontCategories = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
            XFont fontRegular = new XFont("Times New Roman", 11, XFontStyleEx.Regular);

            XTextFormatter tf = new XTextFormatter(gfx);

            XRect rect = new XRect(0, 10, page.Width - 20, 50);
            tf.Alignment = XParagraphAlignment.Center;
            string title = currentContact.FirstName + " " + currentContact.LastName;
            tf.DrawString(title, fontTitle, XBrushes.BlueViolet, rect);

            string contactText = String.Format("{0}\n{1} - {2} - {3} years old", currentContact.Position, currentContact.PhoneNumber, currentContact.Email, currentContact.Age);
            rect = new XRect(0, 5, page.Width - 20, 30);
            tf.Alignment = XParagraphAlignment.Center;
            tf.DrawString(contactText, fontRegular, XBrushes.Black, rect);

            rect = new XRect(0, 5, page.Width - 20, 30);
            tf.Alignment = XParagraphAlignment.Center;


            string subtitleExperience = "Professional Experience";
            tf.DrawString(subtitleExperience, fontCategories, XBrushes.BlueViolet, rect);

            

            string subtitleEducation = "Education";
            tf.DrawString(subtitleEducation, fontCategories, XBrushes.BlueViolet, rect);

        }
    }
}
