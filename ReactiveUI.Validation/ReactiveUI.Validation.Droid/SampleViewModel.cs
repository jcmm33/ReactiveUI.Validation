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
    public class SampleViewModel:ReactiveObject,ISupportsValidation
    {
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        [Reactive]
        public string Name { get; set; }

        [Reactive]
        public int Age { get; set; }

        public int ShoeSize { get; set; }

        public ReactiveCommand<Unit> Save;

        public ValidationHelper ComplexRule { get; set; }

        public ValidationHelper AgeRule { get; set; }

        public ValidationHelper NameRule { get; set; }

        
        // but should be noted that this is wired into an IObservable pipeline
        private Func<string, bool> _nameValidator = (n) => n.Length > 3;

        private readonly Func<string, bool> _isDefined = (n) => !string.IsNullOrEmpty(n);

        public SampleViewModel()
        {
            // name must be at least 3 chars - the selector heee is the property name and its a single property validator
            NameRule = this.ValidationRule(vm => vm.Name, _isDefined, "You must specify a valid name");
           
            // age must be between 13 and 100, message includes the silly length being passed in
            AgeRule = this.ValidationRule(vm => vm.Age, age => age >= 13 && age <= 100,(v) => $"{v} is a silly age");

           // create a rule using an observable.
            ComplexRule = this.ValidationRule(_ => this.WhenAny(vm => vm.Age, vm => vm.Name, (a, n) => new { Age = a.Value, Name = n.Value }).Select(v => v.Age > 10 && !string.IsNullOrEmpty(v.Name)),
                (vm,state) => (!state) ? "Thats a ridiculous shoe / age combination" : string.Empty);


            // Save command only valid when all validators are valid

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            Save = ReactiveCommand.CreateAsyncTask(this.IsValid(), async _ =>
            {
            });
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously


        }


    }
}