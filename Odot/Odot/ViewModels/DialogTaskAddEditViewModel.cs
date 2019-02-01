using Odot.Models;

namespace Odot.ViewModels
{
    class DialogTaskAddEditViewModel : DialogViewModelBase
    {
        public DialogTaskAddEditViewModel(MainViewModel parent, bool addEdit) : base(parent)
        {
            AddEdit = addEdit;
            if (AddEdit == false) //Edit
            {
                Models.Task selected = ParentVM.TaskVM.SelectedTask;
                Namee = selected.Name;
                SelectedCategory = selected.Category;
            }
        }

        public bool AddEdit { get; }

        private string _name;
        public string Namee
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Namee);
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
            }
        }



        public void Ok()
        {
            if (AddEdit == true) //Add task
                ParentVM.TaskVM.Add(Namee, SelectedCategory);
            else //Edit task
                ParentVM.TaskVM.Edit(Namee, SelectedCategory);
            TryClose(true);
        }

        public void Cancel()
        {
            //Do nothing
            TryClose(null);
        }

    }
}
