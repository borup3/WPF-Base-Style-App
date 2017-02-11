using System.Windows.Media;

namespace WPF_Base_Styling_App.UI.Theme
{
    public class VanillaColorScheme : ColorScheme
    {
        private string _name = "Vanilla";
        private string _author = "Parakeet 2";
        private Color _controlActive = Color.FromRgb(74, 183, 255);
        private Color _controlBorder = Color.FromRgb(169, 169, 160);
        private Color _controlDisabled = Color.FromRgb(201, 201, 199);
        private Color _controlFontActive = Color.FromRgb(10, 10, 0);
        private Color _controlFontDisabled = Color.FromRgb(146, 146, 142);
        private Color _controlFontHighlight = Color.FromRgb(255, 104, 4);
        private Color _controlFontNormal = Color.FromRgb(57, 57, 53);
        private Color _controlFontNormalDark = Color.FromRgb(203, 203, 199);
        private Color _controlFontSelected = Color.FromRgb(10, 10, 0);
        private Color _controlFontSelectedLight = Color.FromRgb(10, 10, 0);
        private Color _controlNormal = Color.FromRgb(227, 227, 225);
        private Color _controlNormalDark = Color.FromRgb(218, 218, 216);
        private Color _controlNormalDarker = Color.FromRgb(215, 215, 210);
        private Color _controlNormalLight = Color.FromRgb(221, 221, 219);
        private Color _controlNormalLighter = Color.FromRgb(213, 213, 210);
        private Color _controlSelected = Color.FromRgb(6, 128, 249);
        private Color _controlWindowFrame = Color.FromRgb(236, 236, 234);
        private Color _controlFontWindowFrame = Color.FromRgb(203, 203, 199);
        private Color _controlWindowActiveBorder = Color.FromRgb(182, 182, 175);

        public override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public override string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public override Color ControlSelected
        {
            get { return _controlSelected; }
            set { _controlSelected = value; }
        }

        public override Color ControlActive
        {
            get { return _controlActive; }
            set { _controlActive = value; }
        }

        public override Color ControlNormalLighter
        {
            get { return _controlNormalLighter; }
            set { _controlNormalLighter = value; }
        }

        public override Color ControlNormalLight
        {
            get { return _controlNormalLight; }
            set { _controlNormalLight = value; }
        }

        public override Color ControlNormal
        {
            get { return _controlNormal; }
            set { _controlNormal = value; }
        }

        public override Color ControlNormalDark
        {
            get { return _controlNormalDark; }
            set { _controlNormalDark = value; }
        }

        public override Color ControlNormalDarker
        {
            get { return _controlNormalDarker; }
            set { _controlNormalDarker = value; }
        }

        public override Color ControlDisabled
        {
            get { return _controlDisabled; }
            set { _controlDisabled = value; }
        }

        public override Color ControlBorder
        {
            get { return _controlBorder; }
            set { _controlBorder = value; }
        }

        public override Color ControlWindowActiveBorder
        {
            get { return _controlWindowActiveBorder; }
            set { _controlWindowActiveBorder = value; }
        }

        public override Color ControlWindowFrame
        {
            get { return _controlWindowFrame; }
            set { _controlWindowFrame = value; }
        }

        public override Color ControlFontNormal
        {
            get { return _controlFontNormal; }
            set { _controlFontNormal = value; }
        }

        public override Color ControlFontActive
        {
            get { return _controlFontActive; }
            set { _controlFontActive = value; }
        }

        public override Color ControlFontHighlight
        {
            get { return _controlFontHighlight; }
            set { _controlFontHighlight = value; }
        }

        public override Color ControlFontSelected
        {
            get { return _controlFontSelected; }
            set { _controlFontSelected = value; }
        }

        public override Color ControlFontSelectedLight
        {
            get { return _controlFontSelectedLight; }
            set { _controlFontSelectedLight = value; }
        }

        public override Color ControlFontDisabled
        {
            get { return _controlFontDisabled; }
            set { _controlFontDisabled = value; }
        }

        public override Color ControlFontNormalDark
        {
            get { return _controlFontNormalDark; }
            set { _controlFontNormalDark = value; }
        }

        public override Color ControlFontWindowFrame
        {
            get { return _controlFontWindowFrame; }
            set { _controlFontWindowFrame = value; }
        }
    }
}