using System;
using System.Collections;

namespace RA_HW_12
{
    public record ItemInfo
    {
        public enum Type
        {
            Directory,
            File,
        }

        Type _itemType;
        string _itemName;
        DateTime _itemUpdateTime;

        public Type itemType
        {
            get { return _itemType; }
            init { _itemType = value; }
        }

        public string itemName
        {
            get { return _itemName; }
            init { _itemName = value; }
        }

        public DateTime itemUpdateTime
        {
            get { return _itemUpdateTime; }
            init { _itemUpdateTime = value; }
        }

        public ItemInfo(Type itemType, string itemName, DateTime itemUpdateTime)
        {
            itemType = itemType;
            itemName = itemName;
            itemUpdateTime = itemUpdateTime;
        }

        public override string ToString() => $"{itemType} {itemName} {itemUpdateTime}";
    }
}
