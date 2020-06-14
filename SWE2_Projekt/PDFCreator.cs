using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using SWE2_Projekt.Models;
using SWE2_Projekt.ViewModels;
using System.Diagnostics;
using PdfSharp.Drawing.Layout;

namespace SWE2_Projekt
{
    public class PDFCreator
    {
        public void Create(PictureModel Picture)
        {
            string FilePath = Path.GetFullPath("../../../PDF_Reports/");

            string file = Picture.PicturePath;
            string IPTC_title = "Titel: " + Picture.IPTC.Title;
            string IPTC_Creator = "Urheber: " + Picture.IPTC.Creator;
            string IPTC_Description = "Beschreibung: " + Picture.IPTC.Description;
            string EXIF_Camera = "Kameramodell: " + Picture.EXIF.Camera;
            string EXIF_Resolution = "Auflösung: " + Picture.EXIF.Resolution;
            string EXIF_Date = "Erstelldatum: " + Picture.EXIF.Date;
            string EXIF_Place = "Ort: " + Picture.EXIF.Place;
            string EXIF_Country = "Land: " + Picture.EXIF.Country;
            string INFO = IPTC_title + "\n" + IPTC_Creator + "\n" + IPTC_Description + "\n" + EXIF_Camera + "\n" + EXIF_Resolution + "\n" + EXIF_Date + "\n" + EXIF_Place + "\n" + EXIF_Country;

            using (PdfDocument document = new PdfDocument())
            {
                document.Info.Title = "Created with PDFsharp";
                // Create an empty page
                PdfPage page = document.AddPage();

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.SmoothingMode = XSmoothingMode.HighQuality;
                XTextFormatter tf = new XTextFormatter(gfx);

                // Create a font
                XFont font_header = new XFont("Verdana", 20, XFontStyle.Bold);
                XFont font_text = new XFont("Verdana", 11, XFontStyle.Regular);
                XRect rect = new XRect(gfx.PageOrigin, gfx.PageSize);
                //XRect smallRect = new XRect(0, 0, page.Width, page.Height/2);

                string title = "Bericht von: " + Picture.Title;
                // Draw the text
                
                
                //gfx.DrawString(INFO, font_text, XBrushes.Black, rect, XStringFormats.Center);
                gfx.DrawRectangle(
                        new XLinearGradientBrush(
                            rect,
                            XColor.FromKnownColor(XKnownColor.White),
                            XColor.FromKnownColor(XKnownColor.White),
                            XLinearGradientMode.ForwardDiagonal),
                        rect);
                gfx.DrawString(title, font_header, XBrushes.Black, rect, XStringFormats.TopCenter);
                DrawImage(gfx, page, file);
                /*gfx.DrawString(
                        IPTC_title,
                        font_text,
                        XBrushes.Black,
                        rect.Center,
                        XStringFormats.Center);*/

                tf.DrawString(INFO, font_text, XBrushes.Black, rect);

                string filename = "Bericht_" + Picture.Title + ".pdf";
                string CompletePath = FilePath + filename;
                CompletePath = Path.GetFullPath(CompletePath);
                document.Save(CompletePath);

                /*
                    graphics.DrawRectangle(
                        new XLinearGradientBrush(
                            bounds,
                            XColor.FromKnownColor(XKnownColor.Red),
                            XColor.FromKnownColor(XKnownColor.White),
                            XLinearGradientMode.ForwardDiagonal),
                        bounds);
                    graphics.DrawString(
                        "Hello World!",
                        new XFont("Arial", 20),
                        XBrushes.Black,
                        bounds.Center,
                        XStringFormats.Center);

                    document.Save("test.pdf");
                    document.Close();
                    */
            }
        }

        private void DrawImage(XGraphics gfx, PdfPage page, string file)
        {
            XImage image = XImage.FromFile(file);
            gfx.DrawImage(image, image.PixelWidth, 50);
        }
    }
}
