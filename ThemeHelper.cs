using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pract6_trpo
{
    class ThemeHelper
    {
        private static readonly string[] _themePath = {
            "Styles/Colors/DefaultColors.xaml",
            "Styles/Colors/DarkTheme.xaml"
        };

        public static string Current
        {
            get
            {
                string savedPath = Properties.Settings.Default.ThemePath;
                return string.IsNullOrEmpty(savedPath) ? _themePath[0] : savedPath;
            }
            set
            {
                Properties.Settings.Default.ThemePath = value;
                Properties.Settings.Default.Save();
            }
        }

        public static void Apply(string themePath)
        {
            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            var oldTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && _themePath.Any(path =>
                    d.Source.OriginalString.EndsWith(path, StringComparison.OrdinalIgnoreCase)));

            if (oldTheme != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                Application.Current.Resources.MergedDictionaries[index] = newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            }
            Current = themePath;
        }

        public static void ApplySaved()
        {
            Apply(Current);
        }

        public static void Toggle()
        {
            var newTheme = Current == _themePath[0]
                ? _themePath[1]
                : _themePath[0];
            Apply(newTheme);
        }
    }
}