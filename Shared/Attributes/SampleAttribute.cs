using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostaDeliveryApp.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SampleAttribute : System.Attribute
    {
        private readonly string _name;
        private readonly string _category;
        private readonly string _description;
        private readonly string _instructions;
        private readonly string[] _tags;

        public SampleAttribute(string name, string category, string description, string instructions, params string[] tags)
        {
            _name = name;
            _category = category;
            _description = description;
            _instructions = instructions;
            _tags = tags;
        }

        public string Name
        { get { return _name; } }

        public string Category
        { get { return _category; } }

        public string Description
        { get { return _description; } }

        public string Instructions
        { get { return _instructions; } }

        public System.Collections.Generic.IReadOnlyList<string> Tags
        { get { return _tags; } }
    }
}
