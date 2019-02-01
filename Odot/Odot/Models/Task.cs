using System;
using System.Collections.ObjectModel;

namespace Odot.Models
{
    public class Task : NotifyBase, IComparable<Models.Task>
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        private Category _category;
        public Category Category
        {
            get { return _category; }
            set
            {
                _category = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            private set
            {
                _isCompleted = value;
                NotifyPropertyChanged();
            }
        }

        public Models.Task Parent { get; set; }
        public ObservableCollection<Models.Task> Children { get; set; }



        public Task(string name, Category category) : this(name, category, false)
        {
        }

        public Task(string name, Category category, bool isCompleted)
        {
            Name = name;
            Category = category;
            IsCompleted = isCompleted;
            Parent = null;
            Children = new ObservableCollection<Models.Task>();
        }



        public void AddChild(Models.Task task)
        {
            this.Children.Add(task);
            task.Parent = this;
        }

        public void SetIsCompleted(bool value)
        {
            IsCompleted = value;
            if (value)
                markChildrenAsCompleted(Children);
            else
                markParentAsIncomplete(Parent);
        }

        public Models.Task Clone()
        {
            Task cloned = new Models.Task(Name, Category, IsCompleted);
            return cloned;
        }

        public int CompareTo(Task other)
        {
            if (other == null)
                return -1;
            return string.Compare(this.Name, other.Name);
        }





        private void markChildrenAsCompleted(ObservableCollection<Models.Task> children)
        {
            foreach(Models.Task task in children)
            {
                task.IsCompleted = true;
                markChildrenAsCompleted(task.Children);
            }
        }

        private void markParentAsIncomplete(Models.Task parent)
        {
            if (parent == null)
                return;
            parent.IsCompleted = false;
            markParentAsIncomplete(parent.Parent);
        }

    }
}
