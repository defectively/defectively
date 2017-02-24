#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defectively
{
    public class ConsoleStyle
    {
        public Color Foreground { get; set; } = SystemColors.WindowFrame;
        public Color Background { get; set; } = Color.White;
        public ConsoleColor ForegroundAlt { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundAlt { get; set; } = ConsoleColor.Black;

        public ConsoleStyle() { }

        public ConsoleStyle(Color foreground, Color background, ConsoleColor foregroundAlt, ConsoleColor backgroundAlt) {
            Foreground = foreground;
            Background = background;
            ForegroundAlt = foregroundAlt;
            BackgroundAlt = backgroundAlt;
        }
    }
}
