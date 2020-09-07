using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace MediaPlayer
{
    
        class MultimediaFile : INotifyPropertyChanged
        {
            private ImageSource _thumbnail;
            private String _filename;
            private String _filepath;

            public ImageSource Thumbnail
            {
                get { return _thumbnail; }
                set { _thumbnail = value; OnPropertyChanged("Thumbnail"); }

            }
            public String FileName
            {
                get { return _filename; }
                set { _filename = value; OnPropertyChanged("FileName"); }
            }
            public String FilePath
            {
                get { return _filepath; }
                set { _filepath = value; OnPropertyChanged("FilePath"); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }





            public MultimediaFile(string name, string path)
            {

                Thumbnail = ShellFile.FromFilePath(path).Thumbnail.BitmapSource;
                FileName = name;
                FilePath = path;
            }

        }

        class PlayList : INotifyPropertyChanged
        {
            public PlayList()
            {
                this.PlayLists = new ObservableCollection<MultimediaFile>();
            }

            private string _name;
            private string _path;
            public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
            public string Path { get { return _path; } set { _path = value; OnPropertyChanged("Path"); } }

            private MultimediaFile _sMultimediaFile;
            public MultimediaFile SMultimediaFile
            {
                get { return _sMultimediaFile; }
                set { _sMultimediaFile = value; OnPropertyChanged("SMultimediaFile"); }
            }

        private int _sIndex;
        public int SIndex
        {
            get { return _sIndex; }
            set { _sIndex = value; OnPropertyChanged("SIndex"); }
        }
        private ObservableCollection<MultimediaFile> _playLists;
            public ObservableCollection<MultimediaFile> PlayLists { get { return _playLists; } set { _playLists = value; OnPropertyChanged("PlayLists"); } }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        class ListPlayList : INotifyPropertyChanged
        {
            public ListPlayList()
            {
             
            }
         
            private PlayList _sPlayList;
            public PlayList SPlayList
            {
                get { return _sPlayList; }
                set { _sPlayList = value; OnPropertyChanged("SPlayList"); }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        private ObservableCollection<PlayList> _collectionMyList;
        public ObservableCollection<PlayList> ListPlayLists
        {
            get
            {
                if(_collectionMyList == null)
                {
                    _collectionMyList = new ObservableCollection<PlayList>();                   
                }
                return _collectionMyList;
            }
        }
        }

}



