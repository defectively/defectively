#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defectively.UI
{
    public class ConsoleStyle
    {
        public Color Foreground { get; set; } = SystemColors.WindowFrame;
        public Color Background { get; set; } = Color.White;

        public ConsoleStyle() { }

        public ConsoleStyle(Color foreground, Color background) {
            Foreground = foreground;
            Background = background;
        }
    }
}
