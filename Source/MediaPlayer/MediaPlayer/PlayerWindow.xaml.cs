using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    /// 
   

    //Cửa sổ chơi tệp tin đa phương tiện
    public partial class PlayerWindow : Window
    {

        public delegate void MyDelegate(string path);
        public event MyDelegate Handler;
            
       
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;       
        private bool userIsDraggingVolumeSlider = false;
        private bool isSpeaker = true;
        DispatcherTimer timer;
        List<FileInfo> FileInfos;
        int _index;
        bool isRepeatAll = false;
        bool isRepeatOnce = false;
        bool isRandomPlay = false;
        Random random;
        private IKeyboardMouseEvents _next;
        private IKeyboardMouseEvents _previous;
        private IKeyboardMouseEvents _play;
        

        public PlayerWindow(List<FileInfo> fileInfos, int index)
        {           
            InitializeComponent();
            random = new Random();
            FileInfos = new List<FileInfo>(); 
            FileInfos = fileInfos; 
            _index = index;


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;

            _next = Hook.GlobalEvents();
            _previous = Hook.GlobalEvents();
            _play = Hook.GlobalEvents();
            _next.KeyUp += KeyUp_next;
            _previous.KeyUp += _previous_KeyUp;
            _play.KeyUp += _play_KeyUp;



            Handler?.Invoke(FileInfos[index].FullName);
            _PlayFileSelected(_index);


        }

        // Hàm hook chơi hoặc ngừng chơi một tệp  Ctrl+Shift+B
        private void _play_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.Shift && (e.KeyCode == Keys.B))
            {
                if (mediaPlayerIsPlaying)
                {
                    MyPlayer.Pause();
                    mediaPlayerIsPlaying = false;
                }
                else
                {
                    MyPlayer.Play();
                    mediaPlayerIsPlaying = true;
                }
            
            }
        }

        // Hàm hook chơi tệp trước đó  Ctrl+Shift+V
        private void _previous_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.Shift && (e.KeyCode == Keys.V))
            {
                if (isRandomPlay)
                {
                    _MediaEnded();
                }
                else if (isRepeatOnce)
                {
                    _PlayFileSelected(_index);
                }
                else
                {
                    if (_index > 0)
                    {
                        _index--;
                        _PlayFileSelected(_index);
                    }
                }

                
            
            }
        }

        // Hàm hook chơi tệp kế tiếp  Ctrl+Shift+N
        private void KeyUp_next(object sender, System.Windows.Forms.KeyEventArgs e)
        {
           if(e.Control && e.Shift && (e.KeyCode == Keys.N))
            {
                _MediaEnded();
            }
        }



        private void timer_Tick(object sender, EventArgs e)
        {
            if ((MyPlayer.Source != null) && (MyPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = MyPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = MyPlayer.Position.TotalSeconds;
            }
        }
      
        
        /*Xử lý thanh hiển thị thanh process */
        
        
        private void sliProgress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            MyPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
            if (MyPlayer.NaturalDuration.HasTimeSpan)
            {
                lblTimeLeft.Text = TimeSpan.FromSeconds(MyPlayer.NaturalDuration.TimeSpan.TotalSeconds - sliProgress.Value).ToString(@"hh\:mm\:ss");
            }
        }

        /**/

        /*Xử lý các button điều khiển */

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!mediaPlayerIsPlaying)
            {
                MyPlayer.Play();
                mediaPlayerIsPlaying = true;
            }else
            {
                MyPlayer.Pause();
                mediaPlayerIsPlaying = false;
            }
        }
    

        /*Cài đặt các button điều khiển*/
     
        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
             e.CanExecute = (MyPlayer != null) && (MyPlayer.Source != null);
         }



        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MyPlayer.Stop();
            mediaPlayerIsPlaying = false;
        }


        private void RandomPlay_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isRandomPlay;
        }

        private void RandomPlay_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            isRandomPlay = true;
            isRepeatAll = false;
            isRepeatOnce = false;
        }

        private void RepeatAll_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isRepeatAll;
        }

        private void RepeatAll_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            isRandomPlay = false;
            isRepeatAll = true;
            isRepeatOnce = false;
        }

        private void RepeatOnce_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isRepeatOnce;
        }

        private void RepeatOnce_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            isRandomPlay = false;
            isRepeatAll = false;
            isRepeatOnce = true;
        }


      

        private void NextTrack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (MyPlayer != null) && (MyPlayer.Source != null);
        }

        private void NextTrack_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            MyPlayer.Pause();
            _MediaEnded();
            
        }

        private void PreviousTrack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (MyPlayer != null) && (MyPlayer.Source != null) && (_index > 0);
        }
        private void PreviousTrack_Execute(object sender, ExecutedRoutedEventArgs e)
        {

            if (isRandomPlay)
            {
                _MediaEnded();
            }
            else if (isRepeatOnce)
            {
                _PlayFileSelected(_index);
            }
            else
            {
                if (_index > 0)
                {
                    _index--;
                    _PlayFileSelected(_index);
                }
            }
        }

        /* */
        public void _MediaEnded()
        {
            if (!(isRandomPlay && isRepeatOnce) && isRepeatAll)
            {
                if (_index == FileInfos.Count - 1) _index = -1; 
                _index += 1;
                _PlayFileSelected(_index);
            }else if(!(isRandomPlay && isRepeatAll) && isRepeatOnce)
            {
                _PlayFileSelected(_index);
            }else if(!(isRepeatOnce && isRepeatAll) && isRandomPlay)
            {
                int prev = _index;
                _index = random.Next(FileInfos.Count);
                while (_index == prev)
                   _index = random.Next(FileInfos.Count);
                _PlayFileSelected(_index);
            }
            else
            {
                if (_index < FileInfos.Count - 1)
                {
                    _index += 1;
                    _PlayFileSelected(_index);
                }else
                {
                    MyPlayer.Stop();
                    mediaPlayerIsPlaying = false;
                }
            }
            
        }
        public void _PlayFileSelected( int index)
        {
            Handler?.Invoke(FileInfos[index].FullName);
            string filename=FileInfos[index].FullName;
            MyPlayer.Source = new Uri(filename, UriKind.Absolute);
            System.Threading.Thread.Sleep(500);
            TiltleVideo.Text = System.IO.Path.GetFileNameWithoutExtension(filename);
            MyPlayer.Play();
            mediaPlayerIsPlaying = true;
            timer.Start();

        }
        private void MyPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            _MediaEnded();
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (volumeSlider.Value < 0)
                isSpeaker = false;
            else
                isSpeaker = true;
        }

        private void MuteVolume_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isSpeaker || !isSpeaker;
        }

        private void MuteVolume_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            if (volumeSlider.Value > 0)
            {
                volumeSlider.Value = 0;
                MyPlayer.Volume = volumeSlider.Value;
                
            }
            else
            {
                volumeSlider.Value = 50;
                MyPlayer.Volume = volumeSlider.Value;
            }
        }

        private void volumeSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDraggingVolumeSlider = true;
        }

        private void volumeSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingVolumeSlider = false;
            MyPlayer.Volume = volumeSlider.Value;
        }

       
        private void volumeSlider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (volumeSlider.Value > 0)
            {
                volumeSlider.Value = 0;
                MyPlayer.Volume = volumeSlider.Value;
            }
            else
            {
                volumeSlider.Value = 100;
                MyPlayer.Volume = volumeSlider.Value;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _next.KeyUp -= KeyUp_next;
            _next.Dispose();
            _previous.KeyUp -= _previous_KeyUp ;
            _previous.Dispose();
            _play.KeyUp -= _play_KeyUp;
            _play.Dispose();
            
        }

    }


    public static class CustomCommands
    {

        //Command button RandomPlay
        public static readonly RoutedUICommand RandomPlay = new RoutedUICommand
            (
               "RandomPlay", "RandomPlay", typeof(CustomCommands),
               new InputGestureCollection()
               {
                   new KeyGesture(Key.R, ModifierKeys.Alt)
               }
               );

        //Command button RepeatAll
        public static readonly RoutedUICommand RepeatAll = new RoutedUICommand
            (
               "RepeatAll", "RepeatAll", typeof(CustomCommands),
               new InputGestureCollection()
               {
                   new KeyGesture(Key.A,ModifierKeys.Alt)
               }
               );

        //Command button RepeatOnce
        public static readonly RoutedUICommand RepeatOnce = new RoutedUICommand
            (
               "RepeatOnce", "RepeatOnce", typeof(CustomCommands),
               new InputGestureCollection()
               {
                   new KeyGesture(Key.O,ModifierKeys.Alt)
               }
               );

    }

}
