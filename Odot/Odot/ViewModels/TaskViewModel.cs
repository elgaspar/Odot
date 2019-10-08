using GongSolutions.Wpf.DragDrop;
using Odot.Models;
using Odot.Views.Assist;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Odot.ViewModels
{
    public class TaskViewModel : ViewModelBase, IDropTarget
    {
        public TaskViewModel(MainViewModel parent) : base(parent)
        {
            MarkCompleteCommand = new RelayCommand((a) => MarkAsCompleted(), (a) => SelectedTask != null);
            MarkIncompleteCommand = new RelayCommand((a) => MarkAsIncomplete(), (a) => SelectedTask != null);
        }

        public ObservableCollection<Models.Task> Tasks { get { return ParentVM.File.Tasks; } } 

        public bool AreThereTasks { get { return Tasks.Count != 0; } }

        private Models.Task _selectedTask;
        public Models.Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                NotifyOfPropertyChange(() => SelectedTask);
                NotifyOfPropertyChange(() => IsTaskSelected);
                NotifyOfPropertyChange(() => IsSelectedTaskCompleted);
                NotifyOfPropertyChange(() => IsSelectedTaskIncomplete);
            }
        }

        public bool IsTaskSelected { get { return SelectedTask != null; } }

        public bool IsSelectedTaskCompleted { get { return IsTaskSelected && SelectedTask.IsCompleted; } }

        public bool IsSelectedTaskIncomplete { get { return IsTaskSelected && !SelectedTask.IsCompleted; } }

        public ICommand EditCommand { get; } = new RelayCommand((a) => Actions.TaskEdit());
        public ICommand DeleteCommand { get; } = new RelayCommand((a) => Actions.TaskRemove());
        public ICommand MarkCompleteCommand { get; }
        public ICommand MarkIncompleteCommand { get; }
        public void Add(string name, Category category)
        {
            Models.Task newTask = new Models.Task(name, category);
            Models.Task parent = null;
            if (SelectedTask != null)
                parent = SelectedTask;
            if (parent == null)
                ParentVM.File.Tasks.Add(newTask);
            else
                SelectedTask.AddChild(newTask);
            newTask.SetIsCompleted(false);

            onChangeHappened();
        }

        public void Edit(string newName, Category newCategory)
        {
            ParentVM.TaskVM.SelectedTask.Name = newName;
            ParentVM.TaskVM.SelectedTask.Category = newCategory;

            onChangeHappened();
        }

        public void Remove()
        {
            if (SelectedTask == null)
                return;
            removeSelectedTask();
            ParentVM.AnyChangeHappened = true;
            NotifyAll();
        }

        public void MarkAsCompleted()
        {
            MarkSelectedTaskCompleted(true);
        }

        public void MarkAsIncomplete()
        {
            MarkSelectedTaskCompleted(false);
        }

        public void NotifyAll()
        {
            NotifyOfPropertyChange(() => Tasks);
            NotifyOfPropertyChange(() => AreThereTasks);
            NotifyOfPropertyChange(() => IsTaskSelected);
            NotifyOfPropertyChange(() => IsSelectedTaskCompleted);
            NotifyOfPropertyChange(() => IsSelectedTaskIncomplete);
        }





        private void onChangeHappened()
        {
            ParentVM.AnyChangeHappened = true;
            ParentVM.File.SortTasks();
            NotifyAll();
        }

        private void removeSelectedTask()
        {
            Models.Task parent = SelectedTask.Parent;
            if (parent == null)
                Tasks.Remove(SelectedTask);
            else
            {
                SelectedTask.Parent = null;
                parent.Children.Remove(SelectedTask);
            }
        }

        private void MarkSelectedTaskCompleted(bool value)
        {
            if (SelectedTask == null)
                return;
            SelectedTask.SetIsCompleted(value);
            ParentVM.AnyChangeHappened = true;
            NotifyAll();
        }



        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            Models.Task source = dropInfo.Data as Models.Task;
            Models.Task target = dropInfo.TargetItem as Models.Task;
            if (validTarget(source, target))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = System.Windows.DragDropEffects.Copy;
            }
        }

        private bool validTarget(Models.Task source, Models.Task target)
        {
            if (source==target || target==source.Parent || targetIsChildOrGrandchild(source, target))
                return false;
            return true;
        }

        private bool targetIsChildOrGrandchild(Models.Task source, Models.Task target)
        {
            if (source.Children.Contains(target))
                return true;
            foreach (Models.Task child in source.Children)
            {
                if (targetIsChildOrGrandchild(child, target))
                    return true;
            }
            return false;
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            Models.Task source = dropInfo.Data as Models.Task;
            Models.Task target = dropInfo.TargetItem as Models.Task;
            moveTask(source, target);
        }

        private void moveTask(Models.Task source, Models.Task target)
        {
            removeSelectedTask();
            if (target == null)
                ParentVM.File.Tasks.Add(source);
            else
            {
                target.AddChild(source);
                source.SetIsCompleted(source.IsCompleted);
            }
            ParentVM.AnyChangeHappened = true;
            NotifyAll();
        }

    }
}
