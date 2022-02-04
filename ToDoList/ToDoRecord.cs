using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class ToDoRecord
    {
        public int Key;
        public string Title;
        public string Desc;

        public ToDoRecord(int key, string title, string desc)
        {
            this.Key = key;
            this.Title = title;
            this.Desc = desc;
        }

        public override string ToString()
        {
            return string.Format("Key: {0}\nTitle: {1}\nDesc: {2}", Key, Title, Desc);
        }
    }
}
