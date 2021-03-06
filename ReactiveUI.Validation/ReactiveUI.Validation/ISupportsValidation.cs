﻿using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Validation
{
    /// <summary>
    /// Interface used by view models to indicate they have a validation context.
    /// </summary>
    public interface ISupportsValidation
    {
        /// <summary>
        /// Get the validation context
        /// </summary>
        ValidationContext ValidationContext { get; }
    }
}
