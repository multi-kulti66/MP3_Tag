   M S B A M L   `   `   `  ����B  >MP3_Tag, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MP3_Tag.ViewModelMP3_Tag.ViewModel  5      5clr-namespace:MP3_Tag.ConverterMP3_Tag.Converter  6G   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0897    1MP3_Tag.Converter.Mp3CollectionToBooleanConverter #  Mp3CollectionToBooleanConverter3   -MP3_Tag.Converter.ImageToBitmapImageConverter  ImageToBitmapImageConverter  ImageCommandTemplate  �� DataType%   MP3_Tag.ViewModel.MenuViewModel �� Path �� Orientation-   'MP3_Tag.ViewModel.ImageCommandViewModel �� 	Converter � ToolTip  CheckedElementsTemplate0   *MP3_Tag.ViewModel.CheckedElementsViewModel  dataGridContextMenu � DataContext �� RelativeSource �� 
TargetType �� Property	 �� Value  MainBorderStyle  MainHeaderStyle
 �� CornerRadius J� SnapsToDevicePixels �� 
TargetType  NonTabCellDataGridStyle*  $System.Windows.Controls.DataGridCell �� BasedOn �� Triggers P� Property P� Value  TabCellDataGridStyle 	 RowDataGridStyle)  #System.Windows.Controls.DataGridRow�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      -	viewModelclr-namespace:MP3_Tag.ViewModel  5      -	converterclr-namespace:MP3_Tag.Converter  6G   8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      %v  &	        &	      &	 &     7  &	 �    &	     &	 I    &	 B    &	 _    7 &	 �    7 &		 �
    7 7     5      60     5	      6,   �� 5      "   60   .R�5   
   �� ��6   �� $ Commands���5      �� 5      .R�5      �� $ 
Horizontal=�6!   5      ��5      �� 5      "   6   (��       *, +).R�5      �� $��40��6C   $��40��6N   $��	10,0,10,0q�6Z   ��6   �� $ RelayCommand�.��5      �� ��6    �� $ Image�8    5      �� 5      ��6(   �� $ DisplayName�5      5      5      5      �� 5!      "   63   .R�5"   
   �� ��6   �� $ Commands���5#      �� 5$      .R�5%      �� 5&      ��5(      �� 5)      .R�5*      �� $��
0, 5, 0, 5q�5+      ��5*      �� $ RelayCommand���6C   �� $ DisplayName�5+      5,      5.      5/      �� 52       53      �� *PlacementTarget.DataContext+ �� *Self+��54      �� $ Commands���55   
   �� 56      .R�57      �� ��6   �� $ DisplayName�58      ��5:   
   �� 5;      " m�6   .�5<      ��  �@m�Command	 6,   �� $ RelayCommand�5=      5?      �� 5B      " ��6$   .�5C   
   ��  � ��
	 �����6'   �� 5D   
    � ��
	 �����6(   �� 5E   
    � ��$	 1q�6,   �� 5F   
    �@��CornerRadius$	 5��6)   �� 5G   
    �@J�SnapsToDevicePixels	 . 60   5H      �� 5K      " ��6$   .�5M   
   ��  � ��	 5N      �� 5O      .R�5P      �� 
��� ���
�������$��1q�6M   $
 5��6a   $��4q�5Q       . ._�5R      �� $��14(�6$   $
��Bold"�62   
�������$��Center=�6W   #��z��5S   $   5T      5U      5W      �� 5X   
    � ��	 .  6&   �� 5Z   
    � ��	 5[      �� 5\      " ��6"   .R�5]      ]� .+�5^      �� $	��Top=�6+   $��Header��6@   #��z��5_   +   �� 5`      $��Content��6+   #��z��6C   5a      5b      5d      5e      �� 5h      8   "  5i      .�5j   
   ��  � ��	 .  6&   �� 5k   
    � ��$	 0q�6,   �� 5l   
    � ��$	 Center=�6.   �� 5m   
    � ��$	 Stretch=�65    5n   
   P� 5o       �@ 
IsSelected . . �5p      ��  � ��
	 � ���6/   �� 5q       � ��$	 0q�64   �� 5r       � ��
	 �����6/   5s      5u      �� 5w      8   "  5x      .�5y   
   ��  � ��$	 0q�6,   �� 5z   
    � ��$	 Center=�6.   �� 5{   
    � ��$	 Stretch=�65    5|   
   P� 5}       �@ 
IsSelected . . �5~      ��  � ��
	 � ���6/   �� 5       � ��$	 0q�64   �� 5�       � ��
	 �����6/   5�      P� 5�       �@=�HasError . . �5�      ��  �@�ToolTip	 5�      ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�      5�      �� 5�      8   "  6U    5�   
   P� 5�       �@J�IsMouseOver . . �5�      ��  � ��
	 �`���6/   �� 5�       � ��
	 �����6/   5�      P� 5�       �@ 
IsSelected . . �5�      ��  � ��
	 � ���6/   5�      .�5�   
   ��  �@�ContextMenu8	  6(   5�      5�      