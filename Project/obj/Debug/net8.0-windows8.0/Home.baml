   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5   	       Project.Home   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Project     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$
 Home��5	   	   $	��600��6   $	��900��6#    � 	Resources 5
       �� 
TargetType   RoundButtonStyle �� Property �� Value �� 
TargetType �� CornerRadius  RoundButtonStyle2  RoundButtonStyle3&   System.Windows.Controls.DataGrid  StyledDataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader  CircularImageStyle @� Center	 @� RadiusX
 @� RadiusY��%E  &	        &	 �     &	 �    &	 }    &	 �    �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 5��6!   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 5��6!   
����� �._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5       �� 5!   
   " ��6   .�5"      ��  � �� 5#      �� 5$      " ��6&   .R�5%      �� $ 5��6!   
�������._�5&      �� $��Center=�6/   $��Center=�6L   5'      5(      5*      5+      �� 5,   
   "  6   .�5-      ��   �@ AlternatingRowBackground
 �����69   �� 5.       �@ RowBackground
 �����6.   �� 5/        �@ HorizontalGridLinesBrush
 �����69   �� 50       �@ VerticalGridLinesBrush
 �����67   �� 51       �@ ColumnHeaderHeight$ 35Q�63   �� 52       �@ 	RowHeight$ 35Q�6*   �� 53       � ��$ Arial)�6+   �� 54       � ��$ 14(�6)   �� 55       � ��
 �   �6+   �� 56       � ��
 �����6+   �� 57       � ��
 �����6,   �� 58       � ��$ 1q�60   �� 59       �@ CanUserAddRows$ False��6/   �� 5:       �@ CanUserDeleteRows$ False��62   �� 5;       �@ CanUserResizeRows$ False��62   �� 5<       �@ CanUserReorderColumns$ False��66   �� 5=       �@ ColumnHeaderStyle 5>      �� 5?      "  6   .�5@      ��  � �� �#007ACC67   �� 5A       � ��
 �����67   �� 5B       � ��$
 Bold"�67   �� 5C       � ��$ Center=�6G   5D      5F      5G      �� 5H   
   " ��6+   .�5I      ��  � ��$	 100��6&   �� 5J       � ��$	 100��6'   �� 5K       � ��$ UniformToFill=�6(   �� 5L       � ��$ Center=�64   �� 5M       � ��$ Center=�62   �� 5N       �@J�Clip 5O      @� 5P      $ 50,50&�6&   $	 50Q�65   $
 50Q�6B   5R      5S      5T      .��5V      �  � ColumnDefinitions 5W   
   �� 5X      $	��200 �6   �� 5Y      $��* �6   �� 5Z      $	��150 �6    � RowDefinitions 5]   
   �� 5^      $��60 �6   �� 5_      $��* �6   .+�5a   
   � 
�������$��0��6&   $��1��63   .+�5b      �� $��E:\logoxanhj.png��6   $��	0,-8,10,0q�6.   5c      � 5e   
   
�������$��0��6&   $��0��63   .+�5f      �� $��TURTLE TECH��6   $��Arial)�6+   $��16(�6>   $
��Bold"�6L   $��Center=�6^    � Cursor$
 Hand��6z   $��Center=�6�   5h      � 5i   
   $��0��6   $��2��6   
�������6E   �� 5j   
   $��1��6   $��0��6"   $��5 5 5 5q�62   .+�5l      �� -   5         ��Name$ 	ImgAvatar��5l      $	��100��6'   $	��100��63   #��[ 6@   �� 5n      -   5         ��Name$ tbHello��5n      $�� ��6)   $��Arial)�62   $��18(�6E   $��	5,05,5,05q�6S   $��Center=�6f   $��Center=�6�   $
 Hand��6�   $
��Bold"�6�   6�   �� 5p      -   5         ��Name$ btn_MyProfile��5p      #��[  6,   $��1,26,0,0q�6W   $	��190��6i   $
��Left=�6u   .��5q      � $��5,5,12,5q�6   $
��Left=�6.   .+�5r      �� $��20��6   $��20��6'   $;��5https://static.thenounproject.com/png/2821207-200.png��63   $
��Left=�6r   $��2,0,0,0q�6�   �� 5s      $��
My Profile��6    $��Center=�62   $��15,0,0,0q�6M   $��Arial)�6_   $��14(�6r   5t      5u      �� 5w      -   5        $ btn_Homepage��5w      #��[ 6+   $��0,26,0,0q�6W   $	��190��6i   $
��Left=�6u   .��5x      � $��5,5,34,5q�6   $
��Left=�6.   .+�5y      �� $��20��6   $��20��6'   $9��3https://cdn-icons-png.flaticon.com/512/25/25694.png��63   $
��Left=�6p   $��2,0,0,0q�6�   �� 5z      $
��Home��6    $��Center=�6,   $��14,0,0,0q�6G   $��Arial)�6Y   $��14(�6l   5{      5|      �� 5~      -   5        $ btn_Cart��5~      #��[  6'   $��2,26,0,0q�6R   $	��190��6d   $
��Left=�6p   $��Center=�6�   .��5      � $��5,5,46,5q�6   $
��Left=�6.   .+�5�      �� $��23��6   $��23��6'   $H��Bhttps://icons.veryicon.com/png/o/miscellaneous/unicons/cart-38.png��63   $��0,0,1,0q�6   $
��Left=�6�   �� 5�      $
��Cart��6    $��Center=�6,   $��12,0,0,0q�6G   $��Arial)�6Y   $��14(�6l   5�      5�      �� 5�      -   5        $��
btn_Logout��5�   '   $��3,26,0,0q�6   #��[  6:   ���	#FFD15555$	��190��6�   $
��Left=�6�   .��5�      � $��5,5,26,5q�6   .+�5�      �� $��19��6   $��19��6'   $R��Lhttps://static-00.iconduck.com/assets.00/log-out-icon-2048x2048-cru8zabe.png��63   $��2,0,0,0q�6�   �� 5�      $��Log out��6    $��Center=�6/   $��14,0,0,0q�6J   $��Arial)�6\   $��14(�6o   5�      5�      5�      5�      5�      