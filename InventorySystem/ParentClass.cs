using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    class ParentClass
    {

        private string itemList;
        private float totalPrice;
        private string updateQuery;

        public string ItemList { get => itemList; set => itemList = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public string UpdateQuery { get => updateQuery; set => updateQuery = value; }
    }
    class GetTexBox
    {
        private string textBox1;
        private string textBox7;

        public string TextBox1 { get => textBox1; set => textBox1 = value; }
        public string TextBox7 { get => textBox7; set => textBox7 = value; }
    }
}
