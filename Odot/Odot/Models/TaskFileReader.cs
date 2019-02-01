using System;
using System.Collections.Generic;
using System.Xml;

namespace Odot.Models
{
    static class TaskFileReader
    {
        private static TaskFile taskFile;



        public static void Read(TaskFile tFile, string filepath)
        {
            taskFile = tFile;

            XmlReader reader = XmlReader.Create(filepath);
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element))
                {
                    if (reader.Name == "Tasks")
                        readAllTasks(reader);
                    if (reader.Name == "Categories")
                        readAllCategories(reader);
                }
            }
        }



        private static void readAllTasks(XmlReader reader)
        {
            reader.Read();
            while (reader.Name == "Task" && reader.IsStartElement())
            {
                Models.Task newTask = readTask(reader);
                taskFile.Tasks.Add(newTask);
                reader.Read();
            }
        }

        private static Models.Task readTask(XmlReader reader)
        {
            if (reader.Name == "Task")
            {
                reader.Read();
                reader.Read();
                string name = reader.ReadContentAsString();

                reader.Read();
                Category category = readCategory(reader);
                addCategory(category);

                reader.Read();
                reader.Read();
                bool isCompleted = reader.ReadContentAsBoolean();

                reader.Read();
                List<Models.Task> children = readChildren(reader);

                Models.Task newTask = new Models.Task(name, category, isCompleted);
                foreach (Models.Task child in children)
                    newTask.AddChild(child);

                return newTask;
            }
            return null;
        }

        private static List<Models.Task> readChildren(XmlReader reader)
        {
            List<Models.Task> children = new List<Models.Task>();
            if (reader.Name == "Children")
            {
                if (reader.IsEmptyElement)
                {
                    reader.Read();
                    return children;
                }
                reader.Read();
                while ((reader.Name == "Task") && reader.IsStartElement())
                {
                    Models.Task newTask = readTask(reader);
                    children.Add(newTask);
                    reader.Read();
                }
                reader.Read();
            }

            return children;
        }

        private static void readAllCategories(XmlReader reader)
        {
            reader.Read();
            while (reader.Name == "Category" && reader.IsStartElement())
            {
                Category newCategory = readCategory(reader);
                addCategory(newCategory);
                reader.Read();
            }
        }

        private static Category readCategory(XmlReader reader)
        {
            if (reader.Name == "Category")
            {
                if (reader.IsEmptyElement)
                {
                    return null;
                }

                reader.Read();
                reader.Read();
                string name = reader.ReadContentAsString();

                reader.Read();
                reader.Read();
                string color = reader.ReadContentAsString();

                reader.Read();

                Enum.TryParse(color, out Colors newColor);
                Category newCategory = new Category(name, newColor);

                return newCategory;
            }
            return null;
        }

        private static void addCategory(Category category)
        {
            if ((category == null) || containsCategory(category))
                return;
            taskFile.Categories.Add(category);
        }

        private static bool containsCategory(Category category)
        {
            foreach (Category c in taskFile.Categories)
            {
                if (c.Name == category.Name && c.Color == category.Color)
                    return true;
            }
            return false;
        }
    }
}
