using System;
using System.Windows;
using System.Windows.Media;
using WPF_Base_Styling_App.UI.Theme;
using Application = System.Windows.Application;

namespace WPF_Base_Styling_App.UI
{
    internal static class ThemeManager
    {
        /// <summary>
        /// Sets the active color scheme.
        /// </summary>
        /// <param name="colorScheme"></param>
        public static void SetColorScheme(ColorScheme colorScheme)
        {
            ResourceDictionary dictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/UI/Theme/ColorScheme.xaml", UriKind.RelativeOrAbsolute)
            };

            dictionary["ControlSelected"] = new SolidColorBrush(colorScheme.ControlSelected);
            dictionary["ControlActive"] = new SolidColorBrush(colorScheme.ControlActive);
            dictionary["ControlNormalLighter"] = new SolidColorBrush(colorScheme.ControlNormalLighter);
            dictionary["ControlNormalLight"] = new SolidColorBrush(colorScheme.ControlNormalLight);
            dictionary["ControlNormal"] = new SolidColorBrush(colorScheme.ControlNormal);
            dictionary["ControlNormalDark"] = new SolidColorBrush(colorScheme.ControlNormalDark);
            dictionary["ControlNormalDarker"] = new SolidColorBrush(colorScheme.ControlNormalDarker);
            dictionary["ControlDisabled"] = new SolidColorBrush(colorScheme.ControlDisabled);
            dictionary["ControlBorder"] = new SolidColorBrush(colorScheme.ControlBorder);
            dictionary["ControlWindowActiveBorder"] = new SolidColorBrush(colorScheme.ControlWindowActiveBorder);
            dictionary["ControlWindowFrame"] = new SolidColorBrush(colorScheme.ControlWindowFrame);

            dictionary["ControlFontNormal"] = new SolidColorBrush(colorScheme.ControlFontNormal);
            dictionary["ControlFontActive"] = new SolidColorBrush(colorScheme.ControlFontActive);
            dictionary["ControlFontHighlight"] = new SolidColorBrush(colorScheme.ControlFontHighlight);
            dictionary["ControlFontSelected"] = new SolidColorBrush(colorScheme.ControlFontSelected);
            dictionary["ControlFontSelectedLight"] = new SolidColorBrush(colorScheme.ControlFontSelectedLight);
            dictionary["ControlFontDisabled"] = new SolidColorBrush(colorScheme.ControlFontDisabled);
            dictionary["ControlFontNormalDark"] = new SolidColorBrush(colorScheme.ControlFontNormalDark);
            dictionary["ControlFontWindowFrame"] = new SolidColorBrush(colorScheme.ControlFontWindowFrame);

            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            Application.Current.Resources.MergedDictionaries.Insert(0, dictionary);
        }
    }
}