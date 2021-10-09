#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
#endregion

namespace S7NetWrapper
{
    public class Tag
    {
        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        private object _itemValue;
        public object ItemValue
        {
            get { return _itemValue; }
            set { _itemValue = value; }
        }

        private bool _effective;
        public bool Effective
        {
            get { return _effective; }
            set { _effective = value; }
        }


        private string _itemAddress;
        public string itemAddress
        {
            get { return _itemAddress; }
            set { _itemAddress = value; }
        }

        public Tag()
        {
            
        }

        public Tag(string itemName) 
        {
            this.ItemName = itemName;
        }

        public Tag(string itemName, object itemValue,string itemAddress) 
        {
            this.ItemName = itemName;
            this.ItemValue = itemValue;
            this.itemAddress = itemAddress;
        }        
    }
}
