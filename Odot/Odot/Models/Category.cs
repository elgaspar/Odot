using System;

namespace Odot.Models
{
    public class Category : NotifyBase, IComparable<Category>
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

        private Colors _color;
        public Colors Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyPropertyChanged();
            }
        }



        public Category(string name, Colors color)
        {
            Name = name;
            Color = color;
        }



        public int CompareTo(Category other)
        {
            if (other == null)
                return -1;
            return string.Compare(this.Name, other.Name);
        }

        public Category Clone()
        {
            Category cloned = new Category(Name, Color);
            return cloned;
        }
    }
}
