using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Validation
{
    public class ValidationContext
    {
        private BehaviorSubject<bool> _valid;

        public class Component
        {
            public IObservable<bool> Valid => _valid.AsObservable();

            public string Message { get; set; }

            private readonly BehaviorSubject<bool> _valid;

            public Component(IObservable<bool> validObservable,string message)
            {
                _valid = new BehaviorSubject<bool>(false);
            }
        }

        public IObservable<bool> Valid
        {
            get { return _valid.AsObservable() ?? (_valid = new BehaviorSubject<bool>(GetValidState())).AsObservable(); }
        }

        public bool IsValid => _valid?.Value ?? GetValidState();

        private List<Component> _components;

        public List<string> ValidationSummary { get; }

        /// <summary>
        /// we need a list of items which form the validation context
        /// </summary>
        /// 
        /// 
        /// 
        public ValidationContext()
        {
            
        }

        private bool GetValidState()
        {

            return true;
        }

        public void AddComponent(ValidationContext.Component component)
        {
            (_components??(_components = new List<Component>())).Add(component);


        }


        // right then, we need to build up a collection of 
    }
}
