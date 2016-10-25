using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ReactiveUI.Validation.Droid
{
    public class CommaSeparatedTextStrategy:IValidationPresentationStategy
    {
        private readonly Action<string> _set;

        public CommaSeparatedTextStrategy(Action<string> set)
        {
            _set = set;
        }
        public void IsChanged(bool isValid, ValidationText text)
        {
            var finalText = string.Join(",", text);
            _set(finalText);
        }
    }
}