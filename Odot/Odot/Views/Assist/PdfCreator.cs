using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Odot.Views.Assist.Converters;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Odot.Views.Assist
{
    public class PdfCreator
    {
        private static readonly int ICON_SIZE = 14;
        private static readonly int PAGE_MARGIN = 20;
        private static readonly string FONT_NAME = "Times New Roman";

        private System.Windows.Thickness IconMargin
        {
            get
            {
                return new System.Windows.Thickness(0, 0, 5, -1);
            }
        }

        public PdfDocument Create(ObservableCollection<Models.Task> tasks)
        {
            // todo - smaller icons
            // todo - busy indicator
            var document = new Document();
            Section section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.LeftMargin = PAGE_MARGIN;
            section.PageSetup.TopMargin = PAGE_MARGIN;
            section.PageSetup.RightMargin = PAGE_MARGIN;
            section.PageSetup.BottomMargin = PAGE_MARGIN;

            addTable(section, tasks);

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = document;
            renderer.RenderDocument();
            return renderer.PdfDocument;
        }

        private void addTable(Section section, ObservableCollection<Models.Task> tasks)
        {
            var table = section.AddTable();
            float sectionWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
            double taskColumnWidth = sectionWidth * .75;
            double categoryColumnWidth = sectionWidth * .25;

            table.Style = "Table";
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;
            table.Format.Font.Name = FONT_NAME;

            // Before you can add a row, you must define the columns
            Column column = table.AddColumn();
            column.Width = taskColumnWidth;
            column.Format.Alignment = ParagraphAlignment.Center;
            column = table.AddColumn();
            column.Width = categoryColumnWidth;
            column.Format.Alignment = ParagraphAlignment.Right;

            addHeader(table);
            addBody(tasks, table);
        }

        private void addHeader(Table table)
        {
            Row headerRow = table.AddRow();
            headerRow.HeadingFormat = true;
            headerRow.Format.Alignment = ParagraphAlignment.Center;
            headerRow.Format.Font.Bold = true;
            headerRow.Format.Font.Size = 14;
            headerRow.BottomPadding = 5;
            headerRow.TopPadding = 5;
            headerRow.Shading.Color = new MigraDoc.DocumentObjectModel.Color(192, 192, 192);
            headerRow.Cells[0].AddParagraph("Tasks");
            headerRow.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            headerRow.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            headerRow.Cells[1].AddParagraph("Category");
            headerRow.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            table.SetEdge(0, 0, 2, 1, Edge.Box, BorderStyle.Single, 0.75, MigraDoc.DocumentObjectModel.Color.Empty);
        }

        private void addBody(ObservableCollection<Models.Task> tasks, Table table)
        {
            int taskCount = 0;
            addTaskRows(ref taskCount, tasks, table);
        }

        private void addTaskRows(ref int taskCount, ObservableCollection<Models.Task> tasks, Table table, int indent = 0)
        {
            foreach (var task in tasks)
            {
                addTaskRow(ref taskCount, task, table, indent);

                if (task.Children.Count > 0)
                    addTaskRows(ref taskCount, task.Children, table, indent + 1);
            }
        }

        private void addTaskRow(ref int taskCount, Models.Task task, Table table, int indent)
        {
            taskCount++;
            Row row = table.AddRow();
            row.TopPadding = 3;
            row.BottomPadding = 3;

            // output task
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            var taskPara = row.Cells[0].AddParagraph();
            for (int i = 0; i < indent; i++)
                taskPara.AddSpace(8);

            taskPara.AddImage(getMigraDocFilenameFromByteArray(getIconTask(task)));
            taskPara.AddSpace(1);            
            taskPara.AddText(task.Name);

            // output category
            if (task.Category != null)
            {
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                var categoryPara = row.Cells[1].AddParagraph();
                categoryPara.AddImage(getMigraDocFilenameFromByteArray(getIconCategory(task.Category)));
                categoryPara.AddSpace(1);
                categoryPara.AddText(task.Category.Name);
            }

            table.SetEdge(0, taskCount, 2, 1, Edge.Box, BorderStyle.Single, 0.75, MigraDoc.DocumentObjectModel.Color.Empty);
        }

        private string getMigraDocFilenameFromByteArray(byte[] image)
        {
            return "base64:" +
                   Convert.ToBase64String(image);
        }

        private byte[] getIconTask(Models.Task task)
        {
            Models.Colors enumColor = Models.Colors.None;
            if (task.Category != null)
                enumColor = task.Category.Color;
            Brush color = EnumToColorConverter.EnumToColor(enumColor);

            string iconPath = System.Windows.Application.Current.TryFindResource("TaskIncompleteIconPath") as string;
            if (task.IsCompleted)
                iconPath = System.Windows.Application.Current.TryFindResource("TaskCompletedIconPath") as string;

            return getImageBytes(createImage(iconPath, color).Source as DrawingImage);
        }

        private byte[] getIconCategory(Models.Category category)
        {
            Models.Colors enumColor = Models.Colors.None;
            if (category != null)
                enumColor = category.Color;
            Brush color = EnumToColorConverter.EnumToColor(enumColor);

            string iconPath = System.Windows.Application.Current.TryFindResource("TaskIncompleteIconPath") as string;
            return getImageBytes(createImage(iconPath, color).Source as DrawingImage);
        }

        private byte[] getImageBytes(DrawingImage drawingImage)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawDrawing(drawingImage.Drawing);
                dc.Close();
            }
            RenderTargetBitmap target = new RenderTargetBitmap((int)visual.Drawing.Bounds.Right, (int)visual.Drawing.Bounds.Bottom, 96.0, 96.0, PixelFormats.Pbgra32);
            target.Render(visual);

            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(target));
                encoder.Save(stream);

                return stream.ToArray();
            }
        }

        private Image createImage(string iconPath, Brush color)
        {
            Image img = new Image
            {
                Source = createImageSource(iconPath, color),
                Width = ICON_SIZE,
                Height = ICON_SIZE,
                Margin = IconMargin
            };
            return img;
        }

        private ImageSource createImageSource(string iconPath, Brush color)
        {
            Geometry geometry = Geometry.Parse(iconPath);
            GeometryDrawing drawing = new GeometryDrawing
            {
                Geometry = geometry,
                Brush = color,
                Pen = new Pen(Brushes.White, 0.25)
            };

            DrawingImage img = new DrawingImage(drawing);
            img.Freeze();

            return img;
        }


    }
}
