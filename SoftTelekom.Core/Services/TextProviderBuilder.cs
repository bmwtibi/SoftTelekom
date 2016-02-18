using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Plugins.JsonLocalisation;
using SoftTelekom.Core.Utils;

namespace SoftTelekom.Core.Services
{
    public class TextProviderBuilder : MvxTextProviderBuilder
    {
        public TextProviderBuilder()
            : base(Constants.GeneralNamespace, Constants.RootFolderForResources)
        {
        }

        protected override IDictionary<string, string> ResourceFiles
        {
            get
            {
                var dictionary = this.GetType().GetTypeInfo().Assembly.CreatableTypes().EndingWith("ViewModel").ToDictionary(t => t.Name, t => t.Name);

                dictionary[Constants.Shared] = Constants.Shared;

                return dictionary;
            }
        }
    }
}