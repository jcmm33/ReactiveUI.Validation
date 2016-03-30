using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Validation
{
    /// <summary>
    /// Interface used by view models to indicate they have validatable behaviours
    /// </summary>
    public interface IValidateable
    {
        /// <summary>
        /// Get the validation context
        /// </summary>
        ValidationContext ValidationContext { get; }
    }

    public static class ViewModelIValidateableMixins
    {
        public static IDisposable Validator<TViewModel, TViewModelProp>(this TViewModel viewModel,
            Expression<Func<TViewModel,TViewModelProp>> viewModelProperty,
            IObservable<bool> validObservable,
            string message)
            where TViewModel:ReactiveObject,IValidateable
        {
            return Disposable.Empty;
        }

        public static IDisposable Validator<TViewModel>(this TViewModel viewModel,
            IObservable<bool> validObservable,
            string message)
            where TViewModel : ReactiveObject, IValidateable
        {
            return Disposable.Empty;
        }

        public static IDisposable Validator<TViewModel, TViewModelProp>(this TViewModel viewModel,
            Expression<Func<TViewModel, TViewModelProp>> viewModelProperty,
            Func<TViewModelProp, bool> viewPropertyValid,
            string message)
            where TViewModel : ReactiveObject, IValidateable
        {
            return Disposable.Empty;
        }
    }
}
