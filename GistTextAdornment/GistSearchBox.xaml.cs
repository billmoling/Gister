using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text;

namespace GistTextAdornment
{
    /// <summary>
    /// Interaction logic for GistSearchBox.xaml
    /// </summary>
    public partial class GistSearchBox : UserControl
    {
        IWpfTextView _view;
        public GistSearchBox(IWpfTextView view)
        {
            InitializeComponent();
            _view = view;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {

                ITextEdit edit = _view.TextBuffer.CreateEdit();
                ITextSnapshot snapshot = edit.Snapshot;
                
                int position = snapshot.GetText().IndexOf("gist:");
                edit.Delete(position, 5);
                edit.Insert(position, "billmo");
                edit.Apply();
            }));
            
        }
    }
}
