#pragma warning disable 1591

using System.Drawing;
using System.Windows.Forms;

namespace Defectively.UI
{
    public class ToolStripColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected => ColorTranslator.FromHtml("#91C9F7");
        public override Color MenuItemBorder => Color.White;
        public override Color MenuBorder => Color.White;
        public override Color ImageMarginGradientBegin => Color.White;
        public override Color ImageMarginGradientMiddle => Color.White;
        public override Color ImageMarginGradientEnd => Color.White;
    }
}
