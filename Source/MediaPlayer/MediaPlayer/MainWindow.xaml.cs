using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Collections.ObjectModel;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Đường dẫn thư mục chứa các playlist video và audio
        String pathVD = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/PlayListVD";       
        String pathMS = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/PlayListMS";
        String pathRc = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/recently.txt";

        //Danh sách các playlist video
        ListPlayList playLists;
        //Danh sách các playlist audio
        ListPlayList playLists1;
        //Danh sách chơi 
        List<FileInfo> _list = new List<FileInfo>();

        ListPlayList plSelected;
        int isChoosen = 0;

        //Last file
        MultimediaFile _lastfile;
        PlayList _lastPL;
        public MainWindow()
        {
           
            InitializeComponent();
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _lastPL = new PlayList();
            
            //Nếu chưa tồn tại 2 thư mục thì tạo 2 thư mục
            if (!Directory.Exists(pathVD))
                Directory.CreateDirectory(pathVD);
                              
            if(!Directory.Exists(pathMS))
                Directory.CreateDirectory(pathMS);

            if (!File.Exists(pathRc))
                File.Create(pathRc);

            //Khởi tạo các danh sách 
            playLists = new ListPlayList();
            playLists1 = new ListPlayList();
            ListPlayList plSelected = new ListPlayList();
            playLists = GetPlayLists(true);
            playLists1 = GetPlayLists(false);
            VideoPlaylist.ItemsSource = playLists.ListPlayLists;
            MusicPlaylist.ItemsSource = playLists1.ListPlayLists;           
            VideoPlaylist.DataContext = playLists;
            MusicPlaylist.DataContext = playLists1;
            GetRecently();
            
            
        }

        //Hàm lấy các playlist được lưu trong các thư mục
        private ListPlayList GetPlayLists(bool isVD)
        {
            ListPlayList lists = new ListPlayList();
            String[] array;
            if (isVD)
              array = Directory.GetFiles(pathVD);
            else
              array = Directory.GetFiles(pathMS);
            foreach (var item in array)
            {
               
                PlayList playList = new PlayList();
                playList.Name = Path.GetFileNameWithoutExtension(item);
                playList.Path = item;
                playList.PlayLists = GetList(item);  
                lists.ListPlayLists.Add(playList);
                
            }
            return lists;

        }

        //Hàm lấy danh sách các tệp đa phương tiện được lưu trong các playlist
        private ObservableCollection<MultimediaFile> GetList(string pathPlaylist)
        {
           var ListFiles = new ObservableCollection<MultimediaFile>();

            using (StreamReader reader = new StreamReader(pathPlaylist))
            {
                if (new FileInfo(pathPlaylist).Length > 0)
                {

                    string name, path;
                    while (!reader.EndOfStream)
                    {
                        name = reader.ReadLine();
                        path = reader.ReadLine();
                        if (File.Exists(path))
                        {
                            MultimediaFile file = new MultimediaFile(name, path);
                            ListFiles.Add(file);
                        }
                    }


                    reader.Close();
                }
            }
            return ListFiles;
        }
       
        //Nạp lên playlist chơi gần đây
        private void GetRecently()
        {
            if (new FileInfo(pathRc).Length > 0)
            {
                using (StreamReader reader = new StreamReader(pathRc))
                {
                    String pathpl, pathf, ext;
                    pathpl = reader.ReadLine();
                    pathf = reader.ReadLine(); 
                    ext = Path.GetExtension(pathpl);
                    if (ext == ".vpl")
                    {
                        plSelected = playLists;
                        isChoosen = 1;
                       
                    }
                    else
                    {
                        plSelected = playLists1;
                        isChoosen = 2;
                        
                    }

                    
                    _lastPL.Name = Path.GetFileName(pathpl); 
                    _lastPL.Path = pathpl; 
                  
                    
                        if (File.Exists(pathf))
                        {
                            _lastfile = new MultimediaFile(Path.GetFileNameWithoutExtension(pathf), pathf);
                           
                                this.txtPlaylist.DataContext = _lastPL;
                                this.imgThumbnail.DataContext = _lastfile;
                                this.txtName.DataContext = _lastfile;
                                                           
                        }
                        else
                            MessageBox.Show("Khong ton tai file");


                    reader.Close();
                    

                }

            }
           
            
        }
       


        //Hàm thêm một playlist mới
        private void AddPlayList_Click(object sender, RoutedEventArgs e)
        {                        
                OpenFileDialog dialog = new OpenFileDialog();
            string vfilter= "Video Playlist Files| *.vpl;*.VPL";
            string mfilter = "Music Playlist Files|*.mpl;*.MPL";
            string pathPlaylist = "";
          
          
            if (isChoosen == 1)
            {
                plSelected = playLists;
                dialog.Filter = vfilter;
                pathPlaylist = pathVD;
               
            }
            else
            {
                plSelected = playLists1;
                dialog.Filter = mfilter;
                pathPlaylist = pathMS;
                
            }
           
            dialog.Multiselect = true;
            if(dialog.ShowDialog()==true)
            {
               
                    var isAdd = true;
                  string name, path, spath;

                   foreach(var filepath in dialog.FileNames)
                   {
                        name = Path.GetFileNameWithoutExtension(filepath);
                        path = pathPlaylist + "\\"+Path.GetFileName(filepath);
                        spath = filepath;
                        foreach (var list in plSelected.ListPlayLists)
                        {
                            if(list.Path==path||list.Name==name)
                            {
                                isAdd = false;
                                break;
                            }
                        }
                        if (isAdd)
                        {
                           
                            PlayList list1 = new PlayList();
                            list1.Name = name;
                            
                            list1.PlayLists = GetList(spath);
                            plSelected.ListPlayLists.Add(list1);
                            File.Copy(spath, path);

                            list1.Path = path;


                        MessageBox.Show($"Succesfully to add playlist {name}");
                        }
                        else
                        {
                            MessageBox.Show($"Failed to add playlist {name}");
                        }
                   }                           
            }

        }

       
        //Thêm một file vào một playlist
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string filterMusic = "Music Files|*.wav;*.flac;*.mp3;*.ogg;*.m3u;*.acc;*.wma;*.midi;*.aif;*.m4a;*.mpa;*.pls;*.aif;*.aifc;*.aiff";
            string filterVideo = "Video Files|*.avi;*.wmv;*.mkv;*.mov;*.swf;*.mp4;*.flv;*.vob;*.aaf;*.3gp;*3gp2;*.mod;*.m4v;*.mgv;*.mpg,*.mp2,*.mpeg,*.mpe,*.mpv";

            PlayList item;
            if (isChoosen == 1)
            {
                item = playLists.SPlayList;
                dialog.Filter = filterVideo;
            }
            else {
                item = playLists1.SPlayList;
                dialog.Filter = filterMusic;
            }
            dialog.Multiselect = true;
            if (dialog.ShowDialog()==true)
            {
                var isAdd = true;
                string name, path;
                using (StreamWriter writer = new StreamWriter(item.Path, true))
                {
                    foreach (var filepath in dialog.FileNames)
                    {
                        name = Path.GetFileName(filepath);
                        path = filepath;
                        foreach (var file in item.PlayLists)
                        {
                            if (file.FilePath == path)
                            {
                                isAdd = false;
                                break;
                            }
                        }
                        if (isAdd)
                        {
                            MultimediaFile newfile = new MultimediaFile(name, path);
                            item.PlayLists.Add(newfile);
                            writer.WriteLine(name);
                            writer.WriteLine(path);                           
                        }
                        else
                        {
                            MessageBox.Show($"Failed to add file {name}");
                        }
                    }
                    writer.Close();
                }
               
            }

        }

        //Hàm xử lý chơi tệp tin được chọn khi double-click vào tệp tin
        private void ListBoxFile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (isChoosen != 0)
            {
                if (isChoosen == 1)
                    plSelected = playLists;
                else 
                    plSelected = playLists1;

                if (plSelected != null)
                {


                    _list.Clear();
                    foreach (var item in plSelected.SPlayList.PlayLists)
                    {
                        var fileInfo = new FileInfo(item.FilePath);
                        _list.Add(fileInfo);
                    }
                    var index = plSelected.SPlayList.SIndex;
                    this.txtPlaylist.DataContext = plSelected.SPlayList;
                    _lastPL = plSelected.SPlayList;
                    PlayerWindow playerWindow = new PlayerWindow(_list, index);
                    playerWindow.Handler += PlayerWindow_Handler;
                    playerWindow.Show();

                }

            }
          
        }


        //Hàm xử lý khi nhấp chuột trái vào một playlist video
        private void VideoPlaylist_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (playLists.SPlayList != null)
            {
                isChoosen = 1;
                var item = playLists.SPlayList as PlayList;
                MessageBox.Show(item.Name);
                ListBoxFile.ItemsSource = playLists.SPlayList.PlayLists;
                ListBoxFile.DataContext = playLists.SPlayList;
            }

        }

        //Hàm xử lý khi nhấp chuột trái vào một playlist audio
        private void MusicPlaylist_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (playLists1.SPlayList != null)
            {
                isChoosen = 2;
                var item = playLists1.SPlayList as PlayList;
                MessageBox.Show(item.Name);
                ListBoxFile.ItemsSource = playLists1.SPlayList.PlayLists;
                ListBoxFile.DataContext = playLists1.SPlayList;
            }

           

        }

      


        //Hàm xử lý chơi một danh sách 
        private void PlayPL_Click(object sender, RoutedEventArgs e)
        {
            
            if (isChoosen == 1)
            {
                plSelected = playLists;
            }
            else
                plSelected = playLists1;
           

           
             _list.Clear();
            foreach(var track in plSelected.SPlayList.PlayLists)
            {               
                FileInfo file = new FileInfo(track.FilePath);
                _list.Add(file);               
            }

            this.txtPlaylist.DataContext = plSelected.SPlayList;
            _lastPL= plSelected.SPlayList; 
            PlayerWindow playerWindow = new PlayerWindow(_list,0);
            playerWindow.Handler += PlayerWindow_Handler;
            playerWindow.Show();



        }

       
        //Hàm xử lý xoá một danh sách
        private void DeletePL_Click(object sender, RoutedEventArgs e)
        {
            

            if (isChoosen == 1)
            {
                plSelected = playLists;
            }
            else
            {
                plSelected = playLists1;
            }

            File.Delete(plSelected.SPlayList.Path);
            plSelected.SPlayList.PlayLists.Clear();
            plSelected.ListPlayLists.Remove(plSelected.SPlayList);
        }


        //Hàm xử lý tạo một danh sách mới
        private void NewPlayList_Click(object sender, RoutedEventArgs e)
        {
            
            string pathPL;
            string ext;
            if (isChoosen == 1)
            {
                plSelected = playLists;
                pathPL = pathVD;
                ext = ".vpl";
            }
            else {
                plSelected = playLists1;
                pathPL = pathMS;
                ext = ".mpl";
            }

            string newName = "PL" + DateTime.Now.ToString("yyyyMMddhhmmss");
            string newPath = pathPL + "//" + newName + ext;
            File.Create(newPath);

            PlayList playList = new PlayList();
            playList.Name = newName;
            playList.Path = newPath;
            plSelected.ListPlayLists.Add(playList);                                   
        }


        //Chơi tệp tin
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            
            if (isChoosen == 1)
            {
                plSelected = playLists;
            }
            else
                plSelected = playLists1;



            _list.Clear();
            foreach (var track in plSelected.SPlayList.PlayLists)
            {
                FileInfo file = new FileInfo(track.FilePath);
                _list.Add(file);
            }

            var index = plSelected.SPlayList.SIndex;
            this.txtPlaylist.DataContext = plSelected.SPlayList;
            _lastPL = plSelected.SPlayList;
            PlayerWindow playerWindow = new PlayerWindow(_list, index);
            playerWindow.Handler +=  PlayerWindow_Handler;
            playerWindow.Show();
           
            
        }

        private void PlayerWindow_Handler(string path)
        {
           
            _lastfile = new MultimediaFile(Path.GetFileNameWithoutExtension(path), path);
            this.imgThumbnail.DataContext = _lastfile;
            this.txtName.DataContext = _lastfile;
                                       
        }


        //Xoá một tệp ra khỏi playlist
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            if (isChoosen == 1)
            {
                plSelected = playLists;
            }
            else
                plSelected = playLists1;
            plSelected.SPlayList.PlayLists.Remove(plSelected.SPlayList.SMultimediaFile);
            File.WriteAllText(plSelected.SPlayList.Path, String.Empty);
            using (StreamWriter writer =new StreamWriter(plSelected.SPlayList.Path))
            {
               foreach(var file in plSelected.SPlayList.PlayLists)
                {
                    writer.WriteLine(file.FileName);
                    writer.WriteLine(file.FilePath);
                }
                writer.Close();
            }

        }

        private void VideoPlaylist_MouseUp(object sender, MouseButtonEventArgs e)
        {          
                isChoosen = 1;             
        }

        private void MusicPlaylist_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isChoosen = 2;
        }

        private void VideoMenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isChoosen = 1;
        }

        private void MusicMenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isChoosen = 2;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
            using( StreamWriter writer =new StreamWriter(pathRc))
            {
               if(_lastPL!=null && _lastfile!=null)
                {
                    writer.WriteLine(_lastPL.Path);
                    writer.WriteLine(_lastfile.FilePath);
                }
                writer.Close();
            }
                
        }

      
    }
}
