   M S B A M L   `   `   `  ���� F  BMediaPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MediaPlayerMediaPlayer      MediaPlayer.PlayerWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MediaPlayer     -     � Title$  PlayerWindow��$	��450��$	��800�� � 	Resources    	CloseMenu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  OpenMenu��%�  &	        &	 8    �� .��U� $ (UIElement.Opacity)�$ StatusBarPlayer��.\�  $ 0��$ 1Q�  $ 0:0:0.5��$ 0Q�U� $ (UIElement.Opacity)�$ TiltleVideo��.\�  $ 0��$ 1Q�  $ 0:0:0.5��$ 0Q�U� $ (UIElement.Opacity)�$ ControlGrid��.\�  $ 0��$ 1Q�  $ 0:0:0.5��$ 0Q��� .��U� $ (UIElement.Opacity)�$ StatusBarPlayer��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q�U� $ (UIElement.Opacity)�$ TiltleVideo��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q�U� $ (UIElement.Opacity)�$ ControlGrid��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q� � Triggers :�  :� RoutedEvent$ Grid.MouseEnter�� :� 
SourceName$ GridBackground��.[��� #��[ :� $ Grid.MouseLeave��$ GridBackground��.[��� #��[  	 J� CommandBindings	 )  #System.Windows.Input.CommandBinding  -   
   Command$
 MediaCommands.Play��  -   $
 MediaCommands.Stop��  -   $
 MediaCommands.NextTrack��  -   $!
 MediaCommands.PreviousTrack��  -   $
 MediaCommands.MuteVolume��  -   $%
 local:CustomCommands.RandomPlay��  -   $$
 local:CustomCommands.RepeatAll��  -	   $%
 local:CustomCommands.RepeatOnce��.��� -
    �Name$ GridBackground��
���   � � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��3������  �� ImageSource$ Images/music.jpg�� j� Stretch$
 None=�._�r� -   $��MyPlayer�� r� LoadedBehavior$ Manual=� r� Stretch$ Uniform=��� -    ��Name$ TiltleVideo��$��0��$��18(�$	��Top=�$��NoWrap=�
�������$��5,1,5,1q��� -   $��StatusBarPlayer��$��1��$
��Auto��$��Bottom=�$
��Auto����Z� ���� .R��  � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.@��� .���� -   $��lblProgressStatus��
�������$��18(�$��5q�
00:00:00�� $��1��$��Stretch=�.���� -   $��sliProgress��
������ 
������ $��0q�$��18(��� $��2��.���� -   $��lblTimeLeft��
�������$��18(�$��5q�
00:00:00� -   $ ControlGrid��$��2��$��	0,10,0,20q�.+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ 
Volume_bnt��$��30��$��30��$��5q�
���   �$��MediaCommands.MuteVolume��$
��Left=�$��0q�
������ .���� -    ��Name$ 
img_volume��$��Images\volume.png��$��Uniform=��� -   $��volumeSlider��$��Center=� � Minimum$ 0Q� � Maximum$	 100Q� � Value$ 50Q�$	��120��$��5q�
������ 
������ $��0q��� $ 
Horizontal=�$��Center=�.+��� $!��MediaCommands.PreviousTrack��$��30��$��30��$��5q�$��0q�
������ 
������ .���� $��Images/rewind.png���� -   $ 
StopButton��$��MediaCommands.Stop��$��30��$��30��$��30,0,0,0q�$��0q�
������ 
������ .���� $��Images/stop.png���� -   $ 
PlayButton��$��MediaCommands.Play��$��30��$��30��$��	30,0,30,0q�$��0q�
������ 
������ .���� $��Images/play.png���� $��MediaCommands.NextTrack��$��30��$��30��$��5q�$��0q�
������ 
������ .���� $��Images/fast-forward.png���� $ 
Horizontal=�$��Right=�.+��� $%��local:CustomCommands.RandomPlay��$��30��$��30��$��5q�$��0q�
������ 
������ .���� $��Images/random.png���� $%��local:CustomCommands.RepeatOnce��$��30��$��30��$��	30,0,30,0q�$��0q�
������ 
������ .���� $��Images/repeat_one.png���� $$��local:CustomCommands.RepeatAll��$��30��$��30��$��5q�$��0q�
������ 
������ .���� $��Images/repeat.png��