using Odot.Models;
using Odot.Views.Assist.Converters;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Odot.Views.Assist
{
    class PrintDocument : FlowDocument
    {
        public bool SmallIcon { get; set; }

        private int IconSize {
            get
            {
                if (SmallIcon)
                    return 6;
                else
                    return 14;
            }
        }

        private Thickness IconMargin
        {
            get
            {
                if (SmallIcon)
                    return new Thickness(0,0,5,2);
                else
                    return new Thickness(0, 0, 5, -1);
            }
        }





        public PrintDocument()
        {
            SmallIcon = false;
        }

        public PrintDocument(bool smallIcon)
        {
            SmallIcon = smallIcon;
        }

        public FlowDocument Create(ObservableCollection<Models.Task> tasks)
        {
            FlowDocument doc = new FlowDocument();
            doc.Blocks.Add(getTable(tasks));
            return doc;
        }

        public FlowDocument CreateWithIncompleteOnly(ObservableCollection<Models.Task> tasks)
        {
            ObservableCollection<Models.Task> incompleteTasks = getIncompleteOnly(tasks);
            return Create(incompleteTasks);
        }





        private ObservableCollection<Models.Task> getIncompleteOnly(ObservableCollection<Models.Task> tasks)
        {
            ObservableCollection<Models.Task> incompleteTasks = new ObservableCollection<Models.Task>();
            foreach (Models.Task task in tasks)
            {
                ObservableCollection<Models.Task> children = getIncompleteOnly(task.Children);
                if (task.IsCompleted == false || children.Count != 0)
                {
                    Models.Task t = task.Clone();
                    foreach (Models.Task child in children)
                        t.AddChild(child);
                    incompleteTasks.Add(t);
                }
            }
            return incompleteTasks;
        }

        private Table getTable(ObservableCollection<Models.Task> tasks)
        {
            Table table = new Table
            {
                CellSpacing = 0,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1)
            };

            TableColumn column1 = new TableColumn();
            TableColumn column2 = new TableColumn();

            column1.Width = GridLength.Auto;
            column2.Width = new GridLength(170);

            table.Columns.Add(column1);
            table.Columns.Add(column2);

            TableRowGroup rowGroup = new TableRowGroup();
            table.RowGroups.Add(rowGroup);

            TableRow headerRow = new TableRow
            {
                Background = Brushes.Silver,
                FontSize = 20,
                FontWeight = FontWeights.Bold
            };
            headerRow.Cells.Add( getCell(new Paragraph(new Run("Tasks"))) );
            headerRow.Cells.Add( getCell(new Paragraph(new Run("Category"))) );
            table.RowGroups[0].Rows.Add(headerRow);

            addToRowGroup(rowGroup, tasks, "");

            return table;
        }

        private void addToRowGroup(TableRowGroup rowGroup, ObservableCollection<Models.Task> tasks, string space)
        {
            foreach (Models.Task task in tasks)
            {
                rowGroup.Rows.Add(getRow(task, task.Category, space));

                addToRowGroup(rowGroup, task.Children, space + "        ");
            }
        }

        private TableRow getRow(Models.Task task, Category category, string space)
        {
            TableRow row = new TableRow();
            row.Cells.Add(getCellTask(task, space));
            row.Cells.Add(getCellCategory(category, space));
            return row;
        }

        private TableCell getCellTask(Models.Task task, string space)
        {
            Paragraph p = new Paragraph();
            p.Inlines.Add(space);
            p.Inlines.Add(getIconTask(task));
            p.Inlines.Add(task.Name);
            return getCell(p);
        }

        private TableCell getCellCategory(Category category, string space)
        {
            Paragraph p = new Paragraph();
            if (category != null)
            {
                p.Inlines.Add(getIconCategory(category));
                p.Inlines.Add(category.Name);
            }
            return getCell(p);
        }

        private TableCell getCell(Paragraph p)
        {
            TableCell cell = new TableCell(p)
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black,
                Padding = new Thickness(5),
            };
            return cell;
        }

        private Image getIconTask(Models.Task task)
        {
            Models.Colors enumColor = Models.Colors.None;
            if (task.Category != null)
                enumColor = task.Category.Color;
            Brush color = EnumToColorConverter.EnumToColor(enumColor);

            string iconPath = Application.Current.TryFindResource("TaskIncompleteIconPath") as string;
            if (task.IsCompleted)
                iconPath = Application.Current.TryFindResource("TaskCompletedIconPath") as string;

            return createImage(iconPath, color);
        }

        private Image getIconCategory(Category category)
        {
            Models.Colors enumColor = Models.Colors.None;
            if (category != null)
                enumColor = category.Color;
            Brush color = EnumToColorConverter.EnumToColor(enumColor);

            string iconPath = Application.Current.TryFindResource("TaskIncompleteIconPath") as string;
            return createImage(iconPath, color); ;
        }

        private Image createImage(string iconPath, Brush color)
        {
            Image img = new Image
            {
                Source = createImageSource(iconPath, color),
                Width = IconSize,
                Height = IconSize,
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
