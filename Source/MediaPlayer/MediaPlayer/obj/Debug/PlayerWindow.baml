   M S B A M L   `   `   `  ����F  BMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MediaPlayerMediaPlayer  5   	       MediaPlayer.PlayerWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MediaPlayer     5   	   -   5          � Title$  PlayerWindow��5   	   $	��450��6   $	��800��6.    � 	Resources 5
         	CloseMenu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  OpenMenu��%4  &	        &	     �� 5   
   .��5      U� $ (UIElement.Opacity)�6,   $ StatusBarPlayer��6\   .\�5        $ 0��6'   $ 1Q�63     5      $ 0:0:0.5��6'   $ 0Q�69   5      U� 5      $ (UIElement.Opacity)�6,   $ TiltleVideo��6\   .\�5        $ 0��6'   $ 1Q�63     5      $ 0:0:0.5��6'   $ 0Q�69   5      U� 5      $ (UIElement.Opacity)�6,   $ ControlGrid��6\   .\�5        $ 0��6'   $ 1Q�63     5      $ 0:0:0.5��6'   $ 0Q�69   5      5      �� 5   
   .��5      U� $ (UIElement.Opacity)�6,   $ StatusBarPlayer��6\   .\�5        $ 0��6'   $ 0Q�63     5      $ 0:0:0.5��6'   $ 1Q�69   5      U� 5      $ (UIElement.Opacity)�6,   $ TiltleVideo��6\   .\�5         $ 0��6'   $ 0Q�63     5!      $ 0:0:0.5��6'   $ 1Q�69   5"      U� 5#      $ (UIElement.Opacity)�6,   $ ControlGrid��6\   .\�5$        $ 0��6'   $ 0Q�63     5%      $ 0:0:0.5��6'   $ 1Q�69   5&      5'      5,       � Triggers 5-      :� 5.   
    :� RoutedEvent$ Grid.MouseEnter��6    :� 
SourceName$ GridBackground��65   .[�5/      �� #��[ 6   50      :� 51   
   $ Grid.MouseLeave��6   $ GridBackground��65   .[�52      �� #��[  6   53      	 J� CommandBindings	 56      )  #System.Windows.Input.CommandBinding  58   
   -   5        
   Command$
 MediaCommands.Play��58      6S     59   
   -   5        $
 MediaCommands.Stop��59      6S     5:   
   -   5        $
 MediaCommands.NextTrack��5:      6]     5;   
   -   5        $!
 MediaCommands.PreviousTrack��5;      6e     5<   
   -   5        $
 MediaCommands.MuteVolume��5<      6_     5=   
   -   5        $%
 local:CustomCommands.RandomPlay��5=      6f     5>   
   -   5        $$
 local:CustomCommands.RepeatAll��5>      6d     5?   
   -	   5        $%
 local:CustomCommands.RepeatOnce��5?      6f   .��5B      � -
   5         �Name$ GridBackground��5B      
���   � � RowDefinitions 5D   
   �� 5E      $
��Auto �6   �� 5F      $��* �6   �� 5G      $
��Auto �6   .+�5J   
   �� $��0��6   $��3��6   ��5K      �� 5L       �� ImageSource$ Images/music.jpg��6    j� Stretch$
 None=�6<   ._�5N      r� -   5        $��MyPlayer��5N       r� LoadedBehavior$ Manual=�6,    r� Stretch$ Uniform=�6D   6V   5O      �� 5P   
   -   5         ��Name$ TiltleVideo��5P      $��0��6*   $��18(�67   $	��Top=�6E   $��NoWrap=�6]   
�������$��5,1,5,1q�6�   �� 5R   
   -   5        $��StatusBarPlayer��5R      $��1��6,   $
��Auto��69   $��Bottom=�6F   $
��Auto��6a   ��6o   Z� ��5S      �� 5T      .R�5U      �  � ColumnDefinitions 5V      �� 5W      $
��Auto �6/   �� 5X      $��* �6/   �� 5Y      $
��Auto �6/   5[      5\      .@�5^      �� .��5_      �� -   5        $��lblProgressStatus��5_      
�������$��18(�6H   $��5q�6V   
00:00:006k   5`      �� 5a      $��1��6   $��Stretch=�6.   .��5b      �� -   5        $��sliProgress��5b      
������ 
������ $��0q�6^   $��18(�6r   6�   5c      �� 5d      $��2��6   .��5e      �� -   5        $��lblTimeLeft��5e      
�������$��18(�6B   $��5q�6P   
00:00:006e   5f      5g      � 5h   
   -   5        $ ControlGrid��5h      $��2��6&   $��	0,10,0,20q�63   .+�5i      ��  �� Orientation$ 
Horizontal=�6   .+�5j      �� -   5         ��Name$ 
Volume_bnt��5j      $��30��6-   $��30��69   $��5q�6D   
���   �$��MediaCommands.MuteVolume��6b   $
��Left=�6�   $��0q�6�   
������ .��5k      �� -   5         ��Name$ 
img_volume��5k      $��Images\volume.png��60   $��Uniform=�6K   5l      �� 5m      -   5        $��volumeSlider��5m      $��Center=�6-    � Minimum$ 0Q�6j    � Maximum$	 100Q�6v    � Value$ 50Q�6�   $	��120��6�   $��5q�6�   
������ 
������ $��0q�5n   �   6�   5o      �� 5q      $ 
Horizontal=�6   $��Center=�62   .+�5s      �� $!��MediaCommands.PreviousTrack��6   $��30��6?   $��30��6K   $��5q�6V   $��0q�6a   
������ 
������ .��5t      �� $��Images/rewind.png��6   5u      �� 5w      -   5        $ 
StopButton��5w      $��MediaCommands.Stop��6-   $��30��6J   $��30��6V   $��30,0,0,0q�6a   $��0q�6t   
������ 
������ .��5x      �� $��Images/stop.png��6   5y      �� 5{      -   5        $ 
PlayButton��5{      $��MediaCommands.Play��6-   $��30��6K   $��30��6W   $��	30,0,30,0q�6b   $��0q�6v   
������ 
������ .��5|      �� $��Images/play.png��6   5}      �� 5�      $��MediaCommands.NextTrack��6   $��30��6;   $��30��6G   $��5q�6R   $��0q�6]   
������ 
������ .��5�      �� $��Images/fast-forward.png��6   5�      5�      �� 5�      $ 
Horizontal=�6   $��Right=�66   .+�5�      �� $%��local:CustomCommands.RandomPlay��6   $��30��6C   $��30��6O   $��5q�6Z   $��0q�6e   
������ 
������ .��5�      �� $��Images/random.png��6   5�      �� 5�      $%��local:CustomCommands.RepeatOnce��6   $��30��6C   $��30��6O   $��	30,0,30,0q�6[   $��0q�6n   
������ 
������ .��5�      �� $��Images/repeat_one.png��6   5�      �� 5�      $$��local:CustomCommands.RepeatAll��6   $��30��6B   $��30��6N   $��5q�6Y   $��0q�6d   
������ 
������ .��5�      �� $��Images/repeat.png��6   5�      5�      5�      5�      5�      