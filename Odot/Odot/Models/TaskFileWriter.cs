using System.Xml;

namespace Odot.Models
{
    static class TaskFileWriter
    {
        private static TaskFile taskFile;



        public static void Write(TaskFile tFile, string filepath)
        {
            taskFile = tFile;

            XmlWriter writer = XmlWriter.Create(filepath);
            writer.WriteStartDocument();
            writer.WriteStartElement("TaskFile");

            writeAllTasks(writer);
            writeAllCategories(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }



        private static void writeAllTasks(XmlWriter writer)
        {
            writer.WriteStartElement("Tasks");
            foreach (Models.Task task in taskFile.Tasks)
                writeTask(writer, task);
            writer.WriteEndElement();
        }

        private static void writeAllCategories(XmlWriter writer)
        {
            writer.WriteStartElement("Categories");
            foreach (Category category in taskFile.Categories)
                writeCategory(writer, category);
            writer.WriteEndElement();
        }

        private static void writeTask(XmlWriter writer, Models.Task task)
        {
            writer.WriteStartElement("Task");

            writer.WriteStartElement("Name");
            writer.WriteString(task.Name);
            writer.WriteEndElement();

            writeCategory(writer, task.Category);

            writer.WriteStartElement("IsCompleted");
            writer.WriteString(boolToString(task.IsCompleted));
            writer.WriteEndElement();

            writer.WriteStartElement("Children");
            foreach (Models.Task child in task.Children)
                writeTask(writer, child);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        private static void writeCategory(XmlWriter writer, Category category)
        {
            writer.WriteStartElement("Category");
            if (category != null)
            {
                writer.WriteStartElement("Name");
                writer.WriteString(category.Name);
                writer.WriteEndElement();

                writer.WriteStartElement("Color");
                writer.WriteString(category.Color.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private static string boolToString(bool b)
        {
            if (b)
                return "true";
            else
                return "false";
        }
    }
}
