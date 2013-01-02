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
using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister;
using EchelonTouchInc.Gister.Api.Credentials;
using WPFAutoCompleteTextbox;

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
            InitGistListValue();
            
        }

        /// <summary>
        /// insert the suggestion list for the text box
        /// </summary>
        private void InitGistListValue()
        {
            //TextBox1.AddItem(new AutoCompleteEntry("Chevy Impala", null));  // null matching string will default with just the name
            var credentials = GetGitHubCredentials();
            if (credentials == GitHubCredentials.Anonymous)
            {
                //TODO: need to check how to notify user that there cred is not available
                //NotifyUserThat("Cancelled Gist");
                return;
            }
            List<GistObject> gistList = ProcessGistList.GetUserGistList(credentials);
            foreach (var item in gistList)
            {
                TextBox1.AddItem(new AutoCompleteEntry(item.Description, null));
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {

                ITextEdit edit = _view.TextBuffer.CreateEdit();
                ITextSnapshot snapshot = edit.Snapshot;
                
                //TODO: read the textbox value and get the gist content
                
                
                //delete "gist:" and then add the content to editor
                int position = snapshot.GetText().IndexOf("gist:");
                edit.Delete(position, 5);
                edit.Insert(position, "billmo");
                edit.Apply();
            }));
            
        }

        
        private static GitHubCredentials GetGitHubCredentials()
        {
            var retrievers = new IRetrievesCredentials[]
                                 {
                                     new CachesGitHubCredentials(),
                                     new RetrievesUserEnteredCredentials()
                                 };

            var firstAppropriate = (from applier in retrievers
                                    where applier.IsAvailable()
                                    select applier).First();

            return firstAppropriate.Retrieve();
        }

    }
}
