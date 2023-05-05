using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EditTestClient.Common.Extensions
{
    public class BoolToVisibilityConverter : MarkupConverterBase
    {
        [ConstructorArgument("TrueValue")]
        public Visibility TrueValue { get; set; }

        [ConstructorArgument("FalseValue")]
        public Visibility FalseValue { get; set; }

        [ConstructorArgument("NullValue")]
        public Visibility NullValue { get; set; }

        public BoolToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
            NullValue = Visibility.Collapsed;
        }

        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return NullValue;

            if (!(value is bool))
                return null;

            return (bool)value ? TrueValue : FalseValue;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;

            return null;
        }
    }

    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public abstract class MarkupConverterBase : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        protected abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        protected abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Convert(value, targetType, parameter, culture);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ConvertBack(value, targetType, parameter, culture);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
