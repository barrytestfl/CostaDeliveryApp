using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostaDeliveryApp.Shared.Attributes
{

    public abstract class AdditionalFilesAttribute : Attribute
    {
        private readonly string[] _files;

        protected AdditionalFilesAttribute(params string[] files)
        {
            _files = files;
        }

        public IReadOnlyList<string> Files
        { get { return _files; } }
    }

    /// <summary>
    /// Attribute for annotating a sample with XAML layout files it uses.
    /// This should not be used for the primary layout on WPF, UWP, WinUI, MAUI.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class XamlFilesAttribute : AdditionalFilesAttribute
    {
        public XamlFilesAttribute(params string[] files) : base(files)
        {
        }
    }

    /// <summary>
    /// Attribute for annotating a sample with android layout files it uses.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AndroidLayoutAttribute : AdditionalFilesAttribute
    {
        public AndroidLayoutAttribute(params string[] files) : base(files)
        {
        }
    }

    /// <summary>
    /// Attribute for annotating a sample with additional class files.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassFileAttribute : AdditionalFilesAttribute
    {
        public ClassFileAttribute(params string[] files) : base(files)
        {
        }
    }

    /// <summary>
    /// Attribute for annotating a sample with additional embedded resources files.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EmbeddedResourceAttribute : AdditionalFilesAttribute
    {
        public EmbeddedResourceAttribute(params string[] files) : base(files)
        {
        }
    }
}
