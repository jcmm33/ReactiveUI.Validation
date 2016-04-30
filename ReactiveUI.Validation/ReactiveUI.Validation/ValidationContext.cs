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

        public abstract class Component
        {
            public IObservable<bool> Valid => _valid.AsObservable();

            public string Message { get; set; }

            private readonly BehaviorSubject<bool> _valid;

            public Component(IObservable<bool> validObservable,string message)
            {
                _valid = new BehaviorSubject<bool>(false);
            }

            public bool IsValid
            {
                get
                {
                    return _valid.Value;
                }
            }
        }


        /// <summary>
        /// Represents a single property in an associated view model
        /// </summary>
        public class PropertyComponent:Component
        {
            // so how do we store the property - is it the name of the property
            // or some other way ?
            // When we issue bind, it currently uses some doohicky
            // mechanism that Paul has constructed
            // perhaps name is sufficient

            /// <summary>
            /// Get or set the name of the property associated with this validation
            /// </summary>
            public string Name { get; private set; }

            public PropertyComponent(string name,IObservable<bool> validObservable,string message):base(validObservable,message)
            {
                Name = name;
            }
        }

        public class GenericObservableComponent:Component
        {
            public GenericObservableComponent(IObservable<bool> validObservable,string message):base(validObservable,message)
            {
            }
        }

        public IObservable<bool> Valid
        {
            get { return _valid.AsObservable() ?? (_valid = new BehaviorSubject<bool>(GetValidState())).AsObservable(); }
        }

        public bool IsValid => _valid?.Value ?? GetValidState();

        private List<Component> _components;

        public IEnumerable<string> ValidationSummary
        {
            get
            {
                return _components.Where(p => p.IsValid).Select(p => p.Message);
            }
        }


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
  
        // need to have an overall observable for whether valid or not
        // this is a merge of all of the components that we currently have
        // any item which isn't valid results in an invalid 

        public IObservable<bool> BuildObservable()
        {
            return _components?.Select(p => p.Valid).Merge();
        }

        // now, when anything new is added to the list then we need 
        // to update our definition of global validation...

        // emit an observable....
        // but how do we deal with the addition /change of the underlying list
        // of items ?
         
        public IObservable<bool> ValidObservable()
        {
            return Observable.Cre
        }

        public IEnumerable<PropertyComponent> ValidatorsForPropety(string propertyName)
        {
            return _components?.Where(p => p.GetType() == typeof(PropertyComponent)).Cast<PropertyComponent>().Where(p => p.Name == propertyName);
        }
        // right then, we need to build up a collection of 
    }
}
