using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Validation
{
    public static class IViewForValidateableMixins
    {
        /*            this.BindValidation(ViewModel, vm => vm.SaveName, v => v.textView);

            this.BindValidation(ViewModel, vm => vm.SaveName, (valid) => textView.Border = valid ? Red : Gray);

            this.BindValidationSummary(ViewModel, v => v.ValidationSummary.Text);
        */

        public static IDisposable BindValidation<TView, TViewModel, TViewModelProp,TViewProp>(this TView view,
            TViewModel viewModel, Expression<Func<TViewModel, TViewModelProp>> viewModelProperty,
            Expression<Func<TView,TViewProp>> viewProperty)
            where TViewModel : ReactiveObject, IValidateable
            where TView:IViewFor<TViewModel>
        {
            return Disposable.Empty;
        }

        public static IDisposable BindValidationSummary<TView, TViewModel, TViewProp>(this TView view,
            TViewModel viewModel, Expression<Func<TView, TViewProp>> viewProperty)
        {
            return Disposable.Empty;
        }

        public static IDisposable BindValidation<TView, TViewModel, TViewModelProp>(this TView view,TViewModel viewModel,
            Expression<Func<TViewModel, TViewModelProp>> viewModelProperty,
            Action<bool>  viewAction)
        {
            return Disposable.Empty;
        }
    }
}
