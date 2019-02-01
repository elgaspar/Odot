using Odot.Models;
using System;

namespace Odot.ViewModels
{
    class DialogCategoryAddEditViewModel : DialogViewModelBase
    {
        public DialogCategoryAddEditViewModel(MainViewModel parent, bool addEdit) : base(parent)
        {
            AddEdit = addEdit;
            if (AddEdit == false) //Edit
            {
                Category selected = ParentVM.CategoryVM.SelectedCategory;
                Namee = selected.Name;
                SelectedColor = selected.Color.ToString();
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

        private string _selectedColor;
        public string SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                _selectedColor = value;
                NotifyOfPropertyChange(() => SelectedColor);
            }
        }
        


        public void Ok()
        {
            Enum.TryParse(SelectedColor, out Colors newColor);
            if (AddEdit == true) //Add category
                ParentVM.CategoryVM.Add(Namee, newColor);
            else //Edit category
                ParentVM.CategoryVM.Edit(Namee, newColor);
            TryClose(true);
        }

        public void Cancel()
        {
            //Do nothing
            TryClose(null);
        }

    }
}
