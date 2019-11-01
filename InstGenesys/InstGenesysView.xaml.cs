using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Modules.Windows.Common.DimSize;
using Genesyslab.Desktop.Modules.Windows.Views.Interactions.Container;

namespace CXConnect.Desktop.Modules.InstGenesys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class InstGenesysView : UserControl, IInstGenesysView
    {
        string id;
        readonly IObjectContainer container;

        public InstGenesysView(IInstGenesysModel instGenesysModel, IObjectContainer container)
        {
            //We should find the interaction now
            //instGenesysModel.LoadInteraction("17917907209064079", "10159831212825333");
            this.Model = instGenesysModel;
            this.container = container;

            InitializeComponent();

            Width = Double.NaN;
            Height = Double.NaN;
            MinSize = new MSize() { Width = 400.0, Height = 400.0 };
            System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new System.Action(() =>
            {
                if (ReplyList.Items != null && ReplyList.Items.Count > 0)
                    ReplyList.ScrollIntoView(ReplyList.Items[ReplyList.Items.Count - 1]);
            }));
        }

        public IInstGenesysModel Model {
            get { return this.DataContext as IInstGenesysModel; }
            set { this.DataContext = value; }
        }
        public object Context { get; set; }
        MSize _MinSize;
        public MSize MinSize
        {
            get { return _MinSize; }  // (MSize)base.GetValue(MinSizeProperty); }
            set
            {
                _MinSize = value; // base.SetValue(MinSizeProperty, value);
                OnPropertyChanged("MinSize");
            }
        }

        private void Action_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string Tooltip = button.ToolTip as string;
            string content = Tooltip.Split(' ')[0];
            if (content.Equals("Reply"))
            {
                InstInteractionModel _instInteractionModel = this.DataContext as InstInteractionModel;
                _instInteractionModel.CanComment = false;
                id = button.Tag as string;
                Reply.Focus();
            }
            else if (content.Equals("Delete"))
            {
                id = button.Tag as string;
                InstInteractionModel _instInteractionModel = this.DataContext as InstInteractionModel;
                _instInteractionModel.DeleteReply(id);
            }
            else if (content.Equals("Hide"))
            {
                id = button.Tag as string;
                InstInteractionModel _instInteractionModel = this.DataContext as InstInteractionModel;
                _instInteractionModel.HideReply(id);
            }
            else if (content.Equals("Send"))
            {
                InstInteractionModel _instInteractionModel = this.DataContext as InstInteractionModel;
                bool outcome = _instInteractionModel.PushReply(id, Reply.Text);
                if (outcome)
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new System.Action(() =>
                    {
                        if (ReplyList.Items != null && ReplyList.Items.Count > 0)
                            ReplyList.ScrollIntoView(ReplyList.Items[ReplyList.Items.Count - 1]);
                    }));
                    _instInteractionModel.CanComment = true;
                    Reply.Text = "Enter reply here...";
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            InstInteractionModel _instInteractionModel = this.DataContext as InstInteractionModel;
            _instInteractionModel.CanComment = true;
        }

        private void Reply_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Reply.Text == null || Reply.Text.Equals("") || Reply.Text.Equals("Enter reply here..."))
            {
                if(Send != null)
                    Send.IsEnabled = false;
            }
            else
                Send.IsEnabled = true;

        }
        private void RemoveText(object sender, EventArgs e)
        {
            Reply.Text = "";
        }

        private void AddText(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Reply.Text))
                Reply.Text = "Enter reply here...";
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Create()
        {
        }

        public void Destroy()
        {
        }


        #endregion

    }
}
