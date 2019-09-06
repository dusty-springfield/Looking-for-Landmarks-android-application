using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Coursework.ViewModel
{
    public class CustomPicker : Picker
    {

        public CustomPicker(){}

        public string DoneButtonText
        {
            get
            {
                return (string)GetValue(DoneButtonTextProperty);
            }
            set
            {
                SetValue(DoneButtonTextProperty, value);
            }
        }

        public static readonly BindableProperty DoneButtonTextProperty = BindableProperty.Create(
                                                          propertyName: "DoneButtonTextProperty",
                                                          returnType: typeof(string),
                                                          declaringType: typeof(CustomPicker),
                                                          defaultValue: string.Empty,
                                                          propertyChanged: DoneButtonTextPropertyChanged);

        private static void DoneButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomPicker).DoneButtonText = newValue.ToString();
        }

        public string CancelButtonText
        {
            get
            {
                return (string)GetValue(CancelButtonTextProperty);
            }
            set
            {
                SetValue(CancelButtonTextProperty, value);
            }
        }

        public static readonly BindableProperty CancelButtonTextProperty = BindableProperty.Create(
                                                          propertyName: "CancelButtonTextProperty",
                                                          returnType: typeof(string),
                                                          declaringType: typeof(CustomPicker),
                                                          defaultValue: string.Empty,
                                                          propertyChanged: CancelButtonTextPropertyChanged);

        private static void CancelButtonTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomPicker).CancelButtonText = newValue.ToString();
        }


    }
}
