using System.ComponentModel;
using System.Windows.Media;

namespace WPF_Base_Styling_App.UI.Theme
{
    public abstract class ColorScheme : INotifyPropertyChanged
    {
        public abstract string Name { get; set; }

        public abstract string Author { get; set; }

        public abstract Color ControlSelected { get; set; }

        public abstract Color ControlActive { get; set; }

        public abstract Color ControlNormalLighter { get; set; }

                public abstract Color ControlNormalLight { get; set; }

                public abstract Color ControlNormal { get; set; }

                public abstract Color ControlNormalDark { get; set; }

                public abstract Color ControlNormalDarker { get; set; }

                public abstract Color ControlDisabled { get; set; }

                public abstract Color ControlBorder { get; set; }

                public abstract Color ControlWindowActiveBorder { get; set; }

                public abstract Color ControlWindowFrame { get; set; }

                public abstract Color ControlFontNormal { get; set; }

                public abstract Color ControlFontActive { get; set; }

                public abstract Color ControlFontHighlight { get; set; }

                public abstract Color ControlFontSelected { get; set; }

                public abstract Color ControlFontSelectedLight { get; set; }

                public abstract Color ControlFontDisabled { get; set; }

                public abstract Color ControlFontNormalDark { get; set; }

                public abstract Color ControlFontWindowFrame { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}