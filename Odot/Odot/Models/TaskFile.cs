using System;
using System.Collections.ObjectModel;

namespace Odot.Models
{
    public class TaskFile
    {
        public ObservableCollection<Models.Task> Tasks { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public TaskFile()
        {
            Tasks = new ObservableCollection<Models.Task>();
            Categories = new ObservableCollection<Category>();
        }



        public void SortTasks()
        {
            bubbleSortTasks(Tasks);
        }

        public void SortCategories()
        {
            bubbleSortCategories(Categories);
        }

        public bool Save(string filepath)
        {
            try
            {
                TaskFileWriter.Write(this, filepath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Load(string filepath)
        {
            Tasks = new ObservableCollection<Models.Task>();
            Categories = new ObservableCollection<Category>();
            try
            {
                TaskFileReader.Read(this, filepath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Clear()
        {
            Tasks.Clear();
            Categories.Clear();
        }



        private static void bubbleSortCategories(ObservableCollection<Category> collection)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Category o1 = collection[j - 1];
                    Category o2 = collection[j];
                    if (o1.CompareTo(o2) > 0)
                    {
                        int oldIndex = collection.IndexOf(o1);
                        collection.Move(oldIndex, j);
                    }
                }
            }
        }

        private static void bubbleSortTasks(ObservableCollection<Models.Task> collection)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Models.Task o1 = collection[j - 1];
                    Models.Task o2 = collection[j];
                    if (o1.CompareTo(o2) > 0)
                    {
                        int oldIndex = collection.IndexOf(o1);
                        collection.Move(oldIndex, j);
                    }
                }
                bubbleSortTasks(collection[i].Children);
            }
        }

    }
}
