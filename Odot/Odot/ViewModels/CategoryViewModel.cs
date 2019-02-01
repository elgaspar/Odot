using Odot.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Odot.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        public CategoryViewModel(MainViewModel parent) : base(parent)
        {
        }

        public List<string> Colors
        {
            get
            {
                List<string> list = new List<string>();
                foreach (string c in Enum.GetNames(typeof(Colors)))
                    list.Add(c);
                return list;
            }
        }

        public ObservableCollection<Category> Categories { get { return ParentVM.File.Categories; } }

        public bool AreThereCategories { get{ return Categories.Count != 0; } }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
                NotifyOfPropertyChange(() => IsCategorySelected);
            }
        }

        public bool IsCategorySelected { get { return SelectedCategory != null; } }



        public void Add(string name, Colors color)
        {
            Category newCategory = new Category(name, color);
            Categories.Add(newCategory);

            onChangeHappened();
        }

        public void Edit(string newName, Colors newColor)
        {
            Category oldCategory = SelectedCategory.Clone();

            SelectedCategory.Name = newName;
            SelectedCategory.Color = newColor;

            UpdateCategoryInTasks(oldCategory, SelectedCategory);

            onChangeHappened();
        }

        public void Remove()
        {
            if (SelectedCategory == null)
                return;
            UpdateCategoryInTasks(SelectedCategory, null);
            Categories.Remove(SelectedCategory);
            ParentVM.AnyChangeHappened = true;
            NotifyAll();
        }

        public void NotifyAll()
        {
            NotifyOfPropertyChange(() => Categories);
        }





        private void onChangeHappened()
        {
            ParentVM.AnyChangeHappened = true;
            ParentVM.File.SortCategories();
            NotifyAll();
        }

        private void UpdateCategoryInTasks(Category oldCategory, Category newCategory)
        {
            updateCategoryInTasksRecursive(ParentVM.File.Tasks, oldCategory, newCategory);
            ParentVM.TaskVM.NotifyAll();
        }

        private void updateCategoryInTasksRecursive(ObservableCollection<Models.Task> tasks, Category oldCategory, Category newCategory)
        {
            foreach (Models.Task task in tasks)
            {
                Category cat = task.Category;
                if (cat != null && cat.Name == oldCategory.Name && cat.Color == oldCategory.Color)
                    task.Category = newCategory;
                updateCategoryInTasksRecursive(task.Children, oldCategory, newCategory);
            }
        }

    }
}
