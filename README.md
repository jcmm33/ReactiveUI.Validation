# ReactiveUI.Validation

This is a first attempt at implementing Validation in a reactive way for ReactiveUI based solutions. At this stage, this proposed implementation is here for discussion about changes and enhancements that could be made to the proposed approach.

## Key Objectives


* Implement Validation inkeeping with the ReactiveUI way of doing things.
* Minimal effort in implementing within existing projects
* Sufficient customization capabilities

## Proposed Approach

Decorate existing ViewModel with IValidateable interface, which has a single member, ValidationContext. The ValidationContext contains all of the functionality surrounding the validation of the view model.  Most access to the specification of validation rules is performed through extension methods on the IValidateable interface.

```
    public class SampleViewModel:ReactiveObject,IValidateable
    {
        public ValidationContext ValidationContext { get; } = new ValidationContext();

```

Add validation to the view model

```
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

            // Save command only available when 'valid'
            Save = ReactiveCommand.CreateAsyncTask(this.Valid(), async _ =>
            {
                // some save code here
            });
        }

```

Add validation presentation to the view.

```
	public class MainActivity : Activity,IViewFor<SampleViewModel>
	{
	    private TextView nameValidation { get; set; }

        private TextView validationSummary { get; set; }

        private EditText age { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Bind validation text for view model property to a specified control
		    this.BindValidation(ViewModel, vm => vm.Name, v => v.nameValidation.Text);

            // Use bind validity of view model property to invoke custom action
            // in this case, change the background color of the age control
		    this.BindValidation(ViewModel, vm => vm.Age,(valid) =>
		    {
		        age.SetBackgroundColor(valid ? Color.Red : Color.Transparent); 
		    });

            // bind the overall validation summary to the specified control
            this.BindValidationSummary(ViewModel, v => v.validationSummary.Text);
        }

	    object IViewFor.ViewModel
	    {
	        get { return ViewModel; }
	        set { ViewModel = (SampleViewModel)value; }
	    }

	    public SampleViewModel ViewModel { get; set; }
	}

```

## Outstanding questions

* Do we need to consider any further validation specification forms, e.g. the more traditional validation attribute approach?
* How do we specify the presentation of the validation summary, asp.net used 2 have a control in which you specified the presentation style?
* The view model property is used to lookup the validation state for a an individual view model property (if specified). How would we access more complex validation elements e.g. an overall address validation, named perhaps (may want 2 look at what asp.net does)?
* How would we accomodate potentially multiple validation items in the view which have been specified for a property e.g. must have a value as one validation and a max value as another?
* Are there additional/preferred places for integration of the validation e.g. within the pipeline of the specification of a derived property in the view model?
