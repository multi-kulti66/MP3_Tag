   M S B A M L   `   `   `  ����B  >MP3_Tag, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MP3_Tag.ViewMP3_Tag.View  5          MP3_Tag.View.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      T PMahApps.Metro, Version=1.3.0.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5      %viewclr-namespace:MP3_Tag.View  5      $��0q�5   U   (  "MahApps.Metro.Controls.MetroWindow    	GlowBrush
  �   � � 
ResizeMode$ CanResizeWithGrip=�5	   )      WindowTransitionsEnabled .   � WindowStartupLocation$ CenterScreen=�5
      $	��500��6<   $	��500��6I   $	��750��6Y   $	��750��6e    � Title$ MP3-Tag��5         TitleCharacterCasing$ Normal=�6'    � Icon$7 1pack://application:,,,/Resources/mp3_tag_icon.ico��5       � DataContext 5      �� *Main+   Locator �� Source# [  	 � 	Resources	 5      
 �� Source�� 5   
   $
 MainWindowResources.xaml?�6   %    .��5      ]� .+�5   
   ]� $	��Top=�6   -  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=�6)   .+�5      o� $ Cycle=�6    o� 
IsMainMenu . .@�5      m� $��_File��6   .@�5      m� $��_Close��6    m� Command 6/   ��  �� Path$ CloseCommand�5      5      5      ]� 5   
   $	��Top=�6   $
��Auto��6)   .+�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5       $��4 �6'   �� 5!      $��* �6'   .+�5$         MP3_Tag.View.MenuView  $��0��6     60   �� $ MenuViewModel�   MP3_Tag.View.MediaView  5%      $��2��6!    61   �� $ MediaViewModel�5&      5'      � 5)   
   $��4q�6    5*      �� 5+      $
��Auto �6#   �� 5,      $��4 �6#   �� 5-      $��* �6#   .+�50      &    MP3_Tag.View.CheckedElementsView  $��0��6'    67   �� $ CheckedElementsViewModel�   MP3_Tag.View.DataGridView  51      $��2��6     60   �� $ DataGridViewModel�53      54      55      