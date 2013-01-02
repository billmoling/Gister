using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;

namespace GistTextAdornment
{
    ///<summary>
    ///GistTextAdornment places red boxes behind all the "A"s in the editor window
    ///</summary>
    public class GistTextAdornment
    {
        IAdornmentLayer _layer;
        IWpfTextView _view;
        Brush _brush;
        Pen _pen;

        public GistTextAdornment(IWpfTextView view)
        {
            
            _view = view;
            _layer = view.GetAdornmentLayer("GistTextAdornment");

            //Listen to any event that changes the layout (text changes, scrolling, etc)
            _view.LayoutChanged += OnLayoutChanged;

            //Create the pen and brush to color the box behind the a's
            Brush brush = new SolidColorBrush(Color.FromArgb(0x20, 0x00, 0x00, 0xff));
            brush.Freeze();
            Brush penBrush = new SolidColorBrush(Colors.Red);
            penBrush.Freeze();
            Pen pen = new Pen(penBrush, 0.5);
            pen.Freeze();

            _brush = brush;
            _pen = pen;
        }

        /// <summary>
        /// On layout change add the adornment to any reformatted lines
        /// </summary>
        private void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            
            foreach (ITextViewLine line in e.NewOrReformattedLines)
            {
                this.CreateVisuals(line);
            }
        }

        /// <summary>
        /// Within the given line add the scarlet box behind the a
        /// </summary>
        private void CreateVisuals(ITextViewLine line)
        {
            if (line.Extent.GetText().ToLower().Contains("gist:"))
            {
                var searchBox = new GistSearchBox(_view);
                if (line.Extent.GetText().Length>4)
                {
                    searchBox.TextBox1.Text = line.Extent.GetText().Trim().Substring(5);
                }
                Canvas.SetTop(searchBox, line.TextTop);
                Canvas.SetLeft(searchBox, line.TextRight);
                _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, line.Extent, null, searchBox, null);
            }
        }
    }
}
