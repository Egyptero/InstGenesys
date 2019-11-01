using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CXConnect.Desktop.Modules.InstGenesys
{
    class InstObject
    {
        #region Members
        string _text;
        string _reply;
        string _timestamp;
        string _userName;
        string _firstName;
        string _lastName;
        string _id;
        string _likeCount;
        string _name;
        string _profilePic;
        bool _selected;
        Color _color = Color.FromRgb(255, 240, 200);
        ObservableCollection<InstObject> _replies = new ObservableCollection<InstObject>();
        #endregion

        #region Properties
        public SolidColorBrush ItemColor
        {
            get { return new SolidColorBrush(_color); }
            set { _color = value.Color; }
        }
        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public String Reply
        {
            get { return _reply; }
            set { _reply = value; }
        }
        public String TimeStamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
        public String UserName
        {
            get {
                if (_userName == null)
                    return "Undefined!";
                else
                    return _userName;
            }
            set { _userName = value; }
        }
        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public String Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public String LikeCount
        {
            get {
                if (_likeCount == null)
                    return "0";
                else
                    return _likeCount;
            }
            set { _likeCount = value; }
        }
        public String Name
        {
            get { return _name;}
            set { _name = value; }
        }
        public String ProfilePic
        {
            get { return _profilePic; }
            set { _profilePic = value; }
        }
        public BitmapImage ProfilePicBitMap
        {
            get {
                if(_profilePic != null)
                    return new BitmapImage(new Uri(_profilePic));
                else return new BitmapImage(new Uri("Images/person.gif", UriKind.Relative));
            }
        }
        public Boolean Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        public ObservableCollection<InstObject> Replies
        {
            get { return _replies; }
            set { _replies = value; }
        }
        #endregion
    }
}
