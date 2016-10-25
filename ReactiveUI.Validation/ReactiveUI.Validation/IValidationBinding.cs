using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;

namespace ReactiveUI.Validation
{
    /// <summary>
    /// Interface all Validation Bindings should implement.
    /// </summary>
    public interface IValidationBinding:IDisposable
    {
    }
}
