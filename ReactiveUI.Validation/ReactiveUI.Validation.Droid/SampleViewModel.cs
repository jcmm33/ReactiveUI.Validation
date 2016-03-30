using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ReactiveUI.Fody.Helpers;

namespace ReactiveUI.Validation.Droid
{
    public class SampleViewModel:ReactiveObject,IValidateable
    {
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        [Reactive]
        public string Name { get; set; }

        [Reactive]
        public int Age { get; set; }

        public int ShoeSize { get; set; }

        public ReactiveCommand<Unit> Save;

        public SampleViewModel()
        {
            // name must be at least 3 chars
            this.ValidationRule(vm => vm.Name, name => name.Length > 3, "You must specify a valid name");

            // age must be between 13 and 100, message includes the silly length
            this.ValidationRule(vm => vm.Age, age => age >= 13 && age <= 100, "{0} is a silly age");

            // age must be greater than 10 and shoe size > 4 for a valid model
            this.ValidationRule(this.WhenAny(vm => vm.Age, vm => vm.ShoeSize, 
                (a, s) => new { Age = a.Value, Size = s.Value }).
                 Select(v => v.Age > 10 && v.Size > 4)
                , "Thats a ridiculous shoe / age combination");

            // Save command only valid when all validators are valid
            Save = ReactiveCommand.CreateAsyncTask(this.Valid(), async _ =>
            {
                // some save code here
            });


        }
    }
}