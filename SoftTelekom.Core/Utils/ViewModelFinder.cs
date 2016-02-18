using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

namespace SoftTelekom.Core.Utils
{
    public class ViewModelFinder
    {
        private static ViewModelFinder _instance;
        private readonly Dictionary<Type, IMvxViewModel> _mvxViewModels;

        public static ViewModelFinder Instance
        {
            get { return _instance ?? (_instance = new ViewModelFinder()); }
        }

        public ViewModelFinder()
        {
            _mvxViewModels = new Dictionary<Type, IMvxViewModel>();
        }

        public T GetViewModel<T>(bool save = true) where T : IMvxViewModel
        {
            if (save)
            {
                if (_mvxViewModels.ContainsKey(typeof(T)))
                {
                    IMvxViewModel vm;
                    _mvxViewModels.TryGetValue(typeof(T), out vm);
                    return (T)vm;
                }
            }


            try
            {
                var viewModel = (T)Mvx.IocConstruct(typeof(T));
                if (save)
                {
                    _mvxViewModels.Add(typeof(T), viewModel);
                }
                return viewModel;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}