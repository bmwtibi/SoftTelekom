using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.ViewModels;

namespace SoftTelekom.Core.Utils
{
    class ViewModelLoader : IMvxViewModelLoader
    {
        private IMvxViewModel _dashboardViewModel;

        private IMvxViewModelLocatorCollection _locatorCollection;

        protected IMvxViewModelLocatorCollection LocatorCollection
        {
            get
            {
                _locatorCollection = _locatorCollection ?? Mvx.Resolve<IMvxViewModelLocatorCollection>();
                return _locatorCollection;
            }
        }

        public IMvxViewModel LoadViewModel(MvxViewModelRequest request, IMvxBundle savedState)
        {
            var viewModelLocator = FindViewModelLocator(request);
            if (request.ViewModelType == typeof(DashboardViewModel))
            {
                return _dashboardViewModel ?? (_dashboardViewModel = LoadViewModel(request, savedState, viewModelLocator));
            }

            return LoadViewModel(request, savedState, viewModelLocator);
        }

        private IMvxViewModel LoadViewModel(MvxViewModelRequest request, IMvxBundle savedState,
            IMvxViewModelLocator viewModelLocator)
        {
            IMvxViewModel viewModel = null;
            var parameterValues = new MvxBundle(request.ParameterValues);

            try
            {
                viewModel = viewModelLocator.Load(request.ViewModelType, parameterValues, savedState);
            }
            catch (Exception)
            {
                throw new MvxException(
                    "Failed to construct and initialise ViewModel for type {0} from locator {1} - check MvxTrace for more information",
                    request.ViewModelType, viewModelLocator.GetType().Name);
            }
            //if (!viewModelLocator.TryLoad(request.ViewModelType, parameterValues, savedState, out viewModel))
            //{
            //    throw new MvxException(
            //        "Failed to construct and initialise ViewModel for type {0} from locator {1} - check MvxTrace for more information",
            //        request.ViewModelType, viewModelLocator.GetType().Name);
            //}

            viewModel.RequestedBy = request.RequestedBy;
            return viewModel;
        }

        private IMvxViewModelLocator FindViewModelLocator(MvxViewModelRequest request)
        {
            var viewModelLocator = LocatorCollection.FindViewModelLocator(request);

            if (viewModelLocator == null)
            {
                throw new MvxException("Sorry - somehow there's no viewmodel locator registered for {0}",
                    request.ViewModelType);
            }

            return viewModelLocator;
        }
    }
}