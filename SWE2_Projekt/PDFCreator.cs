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
        public bool CreateReport(PictureModel Picture)
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
                PdfPage page = document.AddPage();

                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.SmoothingMode = XSmoothingMode.HighQuality;
                XTextFormatter tf = new XTextFormatter(gfx);

                XFont font_header = new XFont("Verdana", 20, XFontStyle.Bold);
                XFont font_text = new XFont("Verdana", 11, XFontStyle.Regular);
                XRect rect = new XRect(gfx.PageOrigin, gfx.PageSize);

                string title = "Bericht von " + Picture.Title;
                XImage image = XImage.FromFile(file);


                gfx.DrawRectangle(
                        new XLinearGradientBrush(
                            rect,
                            XColor.FromKnownColor(XKnownColor.White),
                            XColor.FromKnownColor(XKnownColor.White),
                            XLinearGradientMode.ForwardDiagonal),
                        rect);
                gfx.DrawString(title, font_header, XBrushes.Black, rect, XStringFormats.TopCenter);
                DrawImage(gfx, page, file);

                rect = new XRect(0, image.PixelHeight, page.Width, 400);
                gfx.DrawRectangle(XBrushes.White, rect);
                tf.Alignment = XParagraphAlignment.Center;
                tf.DrawString(INFO, font_text, XBrushes.Black, rect, XStringFormats.TopLeft);

                string filename = "Bericht_" + Picture.Title + ".pdf";
                string CompletePath = FilePath + filename;
                CompletePath = Path.GetFullPath(CompletePath);
                document.Save(CompletePath);
            }

            return true;
        }

        public bool TagReport()
        {
            BusinessLayer _bl = new BusinessLayer();

            using (PdfDocument document = new PdfDocument())
            {

            }

            return true;
        }

        private void DrawImage(XGraphics gfx, PdfPage page, string file)
        {
            XImage image = XImage.FromFile(file);
            gfx.DrawImage(image, image.PixelWidth, 70);
        }
    }
}
