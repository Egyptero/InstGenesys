using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXConnect.Desktop.Modules.InstGenesys
{
    class InstInteraction
    {
        #region Members
        string _userId;
        string _mediaURL;
        string _mediaLikeCount;
        string _userName;
        string _profilePic;
        InstObject _instComment;
        bool _canComment;
        #endregion

        #region Properties
        public String ProfilePic
        {
            get { return _profilePic; }
            set { _profilePic = value; }
        }
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public Boolean CanComment
        {
            get { return _canComment; }
            set { _canComment = value; }
        }
        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public String MediaURL
        {
            get { return _mediaURL; }
            set { _mediaURL = value; }
        }
        public String MediaLikeCount
        {
            get { return _mediaLikeCount; }
            set { _mediaLikeCount = value; }
        }
        public InstObject InstComment
        {
            get { return _instComment; }
            set { _instComment = value; }
        }
        #endregion
    }
}
