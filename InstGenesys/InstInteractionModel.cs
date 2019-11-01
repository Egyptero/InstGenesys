using Genesyslab.Desktop.Modules.Core.Model.Interactions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CXConnect.Desktop.Modules.InstGenesys
{
    class InstInteractionModel : IInstGenesysModel
    {
        string header = "InstGenesys";
        TimeSpan counter = TimeSpan.Zero;
        ICase @case;

        #region Members
        InstInteraction _instInteraction;
        #endregion
        public InstInteractionModel(string commentId,string userId)
        {
            LoadInteraction(commentId, userId);
        }
        #region Properties
        public InstInteraction InstInteraction
        {
            get { return _instInteraction; }
            set { _instInteraction = value;
                RaisePropertyChanged("InstInteraction");
            }
        }   
        public BitmapImage MediaImage
        {
            get {
                if (_instInteraction.MediaURL != null)
                    return new BitmapImage(new Uri(_instInteraction.MediaURL));
                else return new BitmapImage(new Uri("Images/noimg.png", UriKind.Relative));
            }
            set
            {
                _instInteraction.MediaURL = value.UriSource.ToString();
                RaisePropertyChanged("MediaImage");
            }
        }
        public String MediaInsights
        {
            get {
                if (_instInteraction.MediaLikeCount == null)
                    return "0";
                else
                    return _instInteraction.MediaLikeCount;
            }
        }
        public ObservableCollection<InstObject> Replies
        {
            get { return _instInteraction.InstComment.Replies; }
            set { _instInteraction.InstComment.Replies = value;
                RaisePropertyChanged("Replies");
            }
        }
        public InstObject Comment
        {
            get { return _instInteraction.InstComment; }
            set { _instInteraction.InstComment = value;
                RaisePropertyChanged("Comment");
            }
        }
        public Boolean CanComment
        {
            get { return _instInteraction.CanComment; }
            set { _instInteraction.CanComment = value;
                RaisePropertyChanged("CanComment");
                RaisePropertyChanged("CanReply");
            }
        }
        public Boolean CanReply
        {
            get { return !_instInteraction.CanComment; }
            set { _instInteraction.CanComment = !value;
                RaisePropertyChanged("CanComment");
                RaisePropertyChanged("CanReply");
            }
        }

        /// <summary>
        /// Gets or sets the header to set in the parent view.
        /// </summary>
        /// <value>The header.</value>
        public string Header
        {
            get { return header; }
            set { if (header != value) { header = value; RaisePropertyChanged("Header"); } }
        }

        /// <summary>
        /// Gets or sets the counter.
        /// </summary>
        /// <value>The counter.</value>
        public TimeSpan Counter
        {
            get { return counter; }
            set { if (counter != value) { counter = value; RaisePropertyChanged("Counter"); } }
        }

        /// <summary>
        /// Gets or sets the case.
        /// </summary>
        /// <value>The case.</value>
        public ICase Case
        {
            get { return @case; }
            set { if (@case != value) { @case = value; RaisePropertyChanged("Case"); } }
        }

        #endregion
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public bool PushReply(string commentId,string text)
        {
            string targetUser = string.Empty;
            if (commentId.Equals(_instInteraction.InstComment.Id))
                targetUser = _instInteraction.InstComment.UserName;
            else
            {
                foreach(InstObject instObject in _instInteraction.InstComment.Replies)
                {
                    if (commentId.Equals(instObject.Id))
                        targetUser = instObject.UserName;
                }
            }
            text = "@" + targetUser + " " + text;
            string url = "http://emp.istnetworks.com:8080/InstGenesys/rest/InstClientRest/addReply?id=" + _instInteraction.UserId+"&commentId="+commentId+"&text="+text;
            string result = SendHTTPRequest(url, null, null, "GET","Application/Json");
            
            if (result == null)
            {
                MessageBox.Show("Error posting reply", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            dynamic outcome = JObject.Parse(result);
            if (outcome.error == true)
            {
                string errorMsg = outcome.errorMessage;
                MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _instInteraction = new InstInteraction()
                {
                    CanComment = true,
                    InstComment = new InstObject()
                };
                return false;
            }
            InstObject lastReply = new InstObject()
            {
                Id = outcome.generatedid,
                UserName = _instInteraction.UserName,
                ProfilePic = _instInteraction.ProfilePic,
                Text = text,
                TimeStamp = "Now",
                ItemColor = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            _instInteraction.InstComment.Replies.Add(lastReply);
            return true;
        }
        public bool HideReply(string commentId)
        {
            string targetUser = string.Empty;
            if (commentId.Equals(_instInteraction.InstComment.Id))
                targetUser = _instInteraction.InstComment.UserName;
            else
            {
                foreach (InstObject instObject in _instInteraction.InstComment.Replies)
                {
                    if (commentId.Equals(instObject.Id))
                        targetUser = instObject.UserName;
                }
            }
            string url = "http://emp.istnetworks.com:8080/InstGenesys/rest/InstClientRest/hideReply?id=" + _instInteraction.UserId + "&commentId=" + commentId;
            string result = SendHTTPRequest(url, null, null, "GET", "Application/Json");

            if (result == null)
            {
                MessageBox.Show("Error hiding reply", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            dynamic outcome = JObject.Parse(result);
            if (outcome.error == true)
            {
                string errorMsg = outcome.errorMessage;
                MessageBox.Show("Error hiding comment", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _instInteraction = new InstInteraction()
                {
                    CanComment = true,
                    InstComment = new InstObject()
                };
                return false;
            }
            InstObject selectedComment = null;
            foreach(InstObject hideReply in _instInteraction.InstComment.Replies)
            {
                if (hideReply.Id.Equals(commentId))
                    selectedComment = hideReply;
            }

            if(selectedComment != null)
                _instInteraction.InstComment.Replies.Remove(selectedComment);
            return true;
        }
        public bool DeleteReply(string commentId)
        {
            string targetUser = string.Empty;
            if (commentId.Equals(_instInteraction.InstComment.Id))
                targetUser = _instInteraction.InstComment.UserName;
            else
            {
                foreach (InstObject instObject in _instInteraction.InstComment.Replies)
                {
                    if (commentId.Equals(instObject.Id))
                        targetUser = instObject.UserName;
                }
            }
            string url = "http://emp.istnetworks.com:8080/InstGenesys/rest/InstClientRest/deleteReply?id=" + _instInteraction.UserId + "&commentId=" + commentId;
            string result = SendHTTPRequest(url, null, null, "GET", "Application/Json");

            if (result == null)
            {
                MessageBox.Show("Error deleting reply", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            dynamic outcome = JObject.Parse(result);
            if (outcome.error == true)
            {
                string errorMsg = outcome.errorMessage;
                MessageBox.Show("Error deleting comment", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _instInteraction = new InstInteraction()
                {
                    CanComment = true,
                    InstComment = new InstObject()
                };
                return false;
            }
            InstObject selectedComment = null;
            foreach (InstObject deleteReply in _instInteraction.InstComment.Replies)
            {
                if (deleteReply.Id.Equals(commentId))
                    selectedComment = deleteReply;
            }

            if (selectedComment != null)
                _instInteraction.InstComment.Replies.Remove(selectedComment);
            return true;
        }
        public void LoadInteraction(string commentId, string userId)
        {
            string url = "http://emp.istnetworks.com:8080/InstGenesys/rest/InstClientRest/getInteraction?id=" + userId + "&commentId=" + commentId;
            string result = SendHTTPRequest(url, null, null, "GET","Application/Json");
            if(result == null)
            {
                _instInteraction = new InstInteraction()
                {
                    CanComment = true,
                    InstComment = new InstObject()
                };
                return;
            }

            dynamic outcome = JObject.Parse(result);
            if (outcome.error == true)
            {
                string errorMsg = outcome.errorMessage;
                MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                _instInteraction = new InstInteraction()
                {
                    CanComment = true,
                    InstComment = new InstObject()
                };
                return;
            }
            _instInteraction = new InstInteraction
            {
                CanComment = true,
                MediaURL = (outcome.instMedia != null) ?outcome.instMedia.media_url:null,
                MediaLikeCount = (outcome.instMedia != null) ? outcome.instMedia.like_count:null,
                UserId = outcome.id,
                UserName = (outcome.instMedia != null && outcome.instMedia.owner != null) ? outcome.instMedia.owner.username : null,
                ProfilePic = (outcome.instMedia != null && outcome.instMedia.owner != null) ? outcome.instMedia.owner.profile_picture_url : null,

                InstComment = new InstObject()
            };
            _instInteraction.InstComment.ProfilePic = (outcome.instComment != null && outcome.instComment.user != null) ? outcome.instComment.user.profile_picture_url:null;
            _instInteraction.InstComment.UserName = (outcome.instComment != null && outcome.instComment.user != null) ? outcome.instComment.user.username:null;
            _instInteraction.InstComment.Id = (outcome.instComment != null) ? outcome.instComment.id:null;
            _instInteraction.InstComment.LikeCount = (outcome.instComment != null) ? outcome.instComment.like_count:null;
            _instInteraction.InstComment.Text = (outcome.instComment != null) ? outcome.instComment.text:null;
            _instInteraction.InstComment.TimeStamp = (outcome.instComment != null) ? outcome.instComment.timestamp:null;

            if (_instInteraction.InstComment.UserName.Equals(_instInteraction.UserName)) // If comment from owner
            {
                _instInteraction.InstComment.ProfilePic = _instInteraction.ProfilePic;
                _instInteraction.InstComment.ItemColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }

            if (outcome.instComment != null && outcome.instComment.replies != null)
            {
                foreach (dynamic replyJson in outcome.instComment.replies.data)
                {
                    InstObject instReply = new InstObject
                    {
                        ProfilePic = replyJson.user.profile_picture_url,
                        UserName = replyJson.user.username,
                        Text = replyJson.text,
                        LikeCount = replyJson.like_count,
                        TimeStamp = replyJson.timestamp,
                        Id = replyJson.id
                    };
                    if (instReply.UserName.Equals(_instInteraction.UserName))
                    { // If reply from owner
                        instReply.ProfilePic = _instInteraction.ProfilePic;
                        instReply.ItemColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    }

                    _instInteraction.InstComment.Replies.Insert(0,instReply);
                }
            }
        }
        private string SendHTTPRequest(string uri, string userName, string password, string method, string type)
        {
            string result;
            string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                if(userName != null && password != null)
                    request.Headers.Add("Authorization", "Basic " + encoded);
                request.Method = method;
                request.Accept = type;

                // Log the response from Redmine RESTful service
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {

                    result = reader.ReadToEnd();
                    reader.Close();
                    response.Close();
                }
            }
            catch (WebException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
            return result;
        }
        #endregion
    }
}
