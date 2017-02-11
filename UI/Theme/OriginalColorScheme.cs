using System.Windows.Media;

namespace WPF_Base_Styling_App.UI.Theme
{
    public class OriginalColorScheme : ColorScheme
    {
        private string _name = "Original";
        private string _author = "Parakeet 2";
        private Color _controlActive = Color.FromRgb(0, 151, 251);
        private Color _controlBorder = Color.FromRgb(53, 53, 58);
        private Color _controlDisabled = Color.FromRgb(75, 75, 79);
        private Color _controlFontActive = Color.FromRgb(245, 245, 255);
        private Color _controlFontDisabled = Color.FromRgb(109, 109, 113);
        private Color _controlFontHighlight = Color.FromRgb(0, 151, 251);
        private Color _controlFontNormal = Color.FromRgb(198, 198, 202);
        private Color _controlFontNormalDark = Color.FromRgb(52, 52, 56);
        private Color _controlFontSelected = Color.FromRgb(245, 245, 255);
        private Color _controlFontSelectedLight = Color.FromRgb(245, 245, 255);
        private Color _controlNormal = Color.FromRgb(40, 40, 42);
        private Color _controlNormalDark = Color.FromRgb(30, 30, 32);
        private Color _controlNormalDarker = Color.FromRgb(20, 20, 22);
        private Color _controlNormalLight = Color.FromRgb(50, 50, 52);
        private Color _controlNormalLighter = Color.FromRgb(70, 70, 72);
        private Color _controlSelected = Color.FromRgb(0, 122, 204);
        private Color _controlWindowFrame = Color.FromRgb(30, 30, 34);
        private Color _controlFontWindowFrame = Color.FromRgb(52, 52, 56);
        private Color _controlWindowActiveBorder = Color.FromRgb(30, 30, 34);

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