using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostaDeliveryApp.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OfflineDataAttribute : Attribute
    {
        private readonly string[] _items;

        public OfflineDataAttribute(params string[] items)
        {
            _items = items;
        }

        public IReadOnlyList<string> Items
        { get { return _items; } }
    }
}
