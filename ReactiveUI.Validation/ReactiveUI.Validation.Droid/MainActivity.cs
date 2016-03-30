using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ReactiveUI.Validation.Droid
{
	[Activity (Label = "ReactiveUI.Validation.Droid", MainLauncher = true, Icon = "@drawable/icon")]
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
}