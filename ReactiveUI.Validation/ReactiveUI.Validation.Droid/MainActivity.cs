using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using ReactiveUI.Framework.Android.Views;

namespace ReactiveUI.Validation.Droid
{
    [Activity(Label = "ReactiveUI.Validation.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ReactiveAppCompatActivity<SampleViewModel>
    {
        public EditText nameEdit { get; set; }

        public EditText ageEdit { get; set; }


        public TextView nameValidation { get; set; }

        public TextView ageValidation { get; set; }

        public TextView validationSummary { get; set; }

        public Button myButton { get; set; }

        public TextInputLayout til { get; set; }

        public TextInputEditText tiet { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            try
            {
                SetContentView(Resource.Layout.Main);
            }
            catch (Exception ex)
            {
                
                throw;
            }

            // Set our view from the "main" layout resource


            this.WireUpControls();

            ViewModel = new SampleViewModel();

		    this.BindCommand(ViewModel, vm => vm.Save, v => v.myButton);

		    this.Bind(ViewModel, vm => vm.Name, v => v.nameEdit.Text);

            this.Bind(ViewModel, vm => vm.Age, v => v.ageEdit.Text);

            this.BindValidation(ViewModel, vm => vm.NameRule, v => v.nameValidation.Text);

            this.BindValidation(ViewModel, vm => vm.Age, v => v.ageValidation.Text);

            // need to be able to bind the summary now
		    this.BindValidation(ViewModel, v => v.validationSummary.Text);

            this.BindValidation(ViewModel, vm => vm.ComplexRule,til);

            // outstanding binding scenarios : 
            // 3. strict and loose matching resulting in multiple bringing togethers of same property

            // Use bind validity of view model property to invoke custom action
            // in this case, change the background color of the age control
            /*
            this.BindValidation(ViewModel, vm => vm.Age,(valid) =>
            {
                age.SetBackgroundColor(valid ? Color.Red : Color.Transparent); 
            });
            */
            // bind the overall validation summary to the specified control
            //this.BindValidationSummary(ViewModel, v => v.validationSummary.Text);

        }
	}
}