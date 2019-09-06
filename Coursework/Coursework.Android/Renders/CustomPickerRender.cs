using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Coursework.Droid.Renders;
using Coursework.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace Coursework.Droid.Renders
{
    public class CustomPickerRenderer : PickerRenderer
    {

        AlertDialog _dialog;
        IElementController ElementController => Element as IElementController;
        CustomPicker customPicker;
        public CustomPickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            
            if (e.NewElement == null || e.OldElement != null) return;
            customPicker = e.NewElement as CustomPicker;
            if (e.NewElement != null)
            {
                if (Control == null)
                {

                    var textField = CreateNativeControl();
                    textField.SetOnClickListener(CustomPickerListener.Instance);
                    textField.InputType = InputTypes.Null;
                    SetNativeControl(textField);
                }
            }
            Control.Gravity = GravityFlags.CenterHorizontal;
            Control.Click += OnClick;
            
            base.OnElementChanged(e);

        }


        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                GC.Collect();
            }catch(ObjectDisposedException ex)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine(ex.Message);
            }
        }
        private void OnClick(object sender, EventArgs e)
        {
            try
            {
                Picker model = Element;

                var picker = new NumberPicker(Context);
                if (model.Items != null && model.Items.Any())
                {
                    picker.MaxValue = model.Items.Count - 1;
                    picker.MinValue = 0;
                    picker.SetDisplayedValues(model.Items.ToArray());
                    picker.WrapSelectorWheel = false;
                    picker.DescendantFocusability = Android.Views.DescendantFocusability.BlockDescendants;
                    picker.Value = model.SelectedIndex;
                }

                var layout = new LinearLayout(Context) { Orientation = Orientation.Vertical };
                layout.AddView(picker);

                ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);

                var builder = new AlertDialog.Builder(Context);
                builder.SetView(layout);
                builder.SetTitle(model.Title ?? "");
                builder.SetNegativeButton(customPicker.CancelButtonText ?? "Cancel", (s, a) =>
                {
                    ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                    _dialog = null;
                });
                builder.SetPositiveButton(customPicker.DoneButtonText ?? "Accept", (s, a) =>
                {
                    ElementController.SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
                // It is possible for the Content of the Page to be changed on SelectedIndexChanged. 
                // In this case, the Element & Control will no longer exist.
                if (Element != null)
                    {
                        if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
                            Control.Text = model.Items[Element.SelectedIndex];
                        ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                    }
                    _dialog = null;
                });

                _dialog = builder.Create();
                _dialog.DismissEvent += (senderr, args) =>
                {
                    ElementController?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                };
                _dialog.Show();
            }
            catch (Exception)
            {
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine("OnClick");
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine(); Console.WriteLine();
            }
        }




        class CustomPickerListener : Java.Lang.Object, IOnClickListener
        {
            public static readonly CustomPickerListener Instance = new CustomPickerListener();

            public void OnClick(global::Android.Views.View v)
            {
                var renderer = v.Tag as CustomPickerRenderer;
                if (renderer == null)
                    return;

                renderer.OnClick(null,null);
            }
        }
    }
}