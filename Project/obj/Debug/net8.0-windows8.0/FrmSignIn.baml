   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ProjectProject  5   	       Project.FrmSignIn   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Project     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Sign In��5	   	   $	��450��6   $	��800��6&    � 	Resources 5
       �� 
TargetType   RoundButtonStyle �� Property �� Value �� 
TargetType �� CornerRadius  RoundBox ��Name  PasswordBoxStyle��%9  &	        &	 �     &	 �    �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 5��6!   #��z��62   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      �� 5   
   " ��6   .�5      ��  � ��$ 0q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� 
�������$��1q�64   $ 7��6H   ._�5      �� $ PART_ContentHost��6+   $��30��6E   $��Center=�6Q   5      5      5       5!      �� 5"   
   " G�6   .�5#      ��  � �� 5$      �� 5%      " G�6&   .R�5&      � .+�5'      �� 
�������$��1q�68   $ 7��6L   
�������._�5(   "   �� $ PART_ContentHost��6/   $��30��6I   $��Center=�6U   5)      5+      5,      5.      5/      50      .��51      � ��52   
   �� 53      	 � ColumnDefinitions	 55   
   �� 56      $��* �6   �� 57      $��* �6   .+�5;   
   �� ���#00A6996,   �� 5<   
   $��0��6   $��Center=�6&   .+�5=      �� $��40(�6   $
��Bold"�6"   
�������$��0,0,0,20q�6G   $��Center=�6Y   $��Sign Up��6v   $��Arial)�6�   �� 5>      $D��>Enter your personal info and create new account to connect us!��6   $��20(�6^   
�������$
��Wrap=�6   
 �� TextAlignment$
 Center=�6�   $
��20,0q�6�   $��Arial)�6�   �� 5?      -   5        $��Sign Up��5?      $	��150��6'   $��25��63   $��0,30,0,0q�6?   #��[  6Q   $��Center=�6{   $
��Bold"�6�   $��16(�6�   $��Arial)�6�   $��0,1,1,1q�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5@      V� 5A      .~�5B      ��  �� ScaleX$ 1Q�6)   �� 5C      �� 5D      T� 5E      5F      5H      5I      �� 5L   
   $��1��6   
�������$��Center=�68   $��Center=�6S   .+�5M      �� $��Sign in to App��6   $��40(�6.   ���#00A699$��0,0,0,20q�6Q   $��Center=�6c   $��Arial)�6�   $
��Bold"�6�   �� 5N       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0,0,0,20q�6O   .+�5O      �� $��50��6   $��50��6$   $��5q�60   ��5P      �� 5Q       �� ImageSource$� �https://yt3.googleusercontent.com/viNp17XpEF-AwWwOZSj_TvgobO1CGmUUgcTtQoAG40YaYctYMoUqaRup0rTxxxfQvWw3MvhXesw=s900-c-k-c0x00ffffff-no-rj��6%   5S      �� 5T      $��50��6   $��50��6%   $��5q�61   ��5U      �� 5V      $R Lhttps://cdn.icon-icons.com/icons2/4048/PNG/512/facebook_logo_icon_257007.png��6%   5X      �� 5Y      $��50��6   $��5q�6$   ��5Z      �� 5[      $� �https://images.rawpixel.com/image_png_800/czNmcy1wcml2YXRlL3Jhd3BpeGVsX2ltYWdlcy93ZWJzaXRlX2NvbnRlbnQvbHIvdjk4Mi1kMy0xMC5wbmc.png��6%   5]      5^      �� 5_      $��or use your account :��6   
���   �$��Center=�6H   $��0,0,0,10q�6e   $��Arial)�6w   $
��Bold"�6�   $��14(�6�   � 5b      $	��250��6   $��0,0,0,10q�6   .+�5c      �� -   5        $��txtEmail��5c      $��Center=�6*   #��[ 6E   $��5q�6�   $��1q�6�   
�������6�   �� 5d      -   5        $��EmailPlaceholder��5d      $��Email or Username��64   
�������$��Center=�6_    J� IsHitTestVisible$ False��6z   $��10,0,0,0q�6�   $��Arial)�6�   5e      � 5h      $	��250��6   $��0,0,0,20q�6   .+�5i      G� -   5        $��txtPassword��5i      $��Center=�61   #��[ 6L   $��5q�6�   $��1q�6�   
�������6�   �� 5j      -   5        $��txtVisiblePassword��5j      $��Center=�64   #��[ 6O   $��5q�6q   $��1q�6}   
�������$y�	Collapsed=�6�   �� 5k      -   5        $��PasswordPlaceholder��5k      $��Password��67   
�������$��Center=�6Y   $ False��6t   $��10,0,0,0q�6�   $��Arial)�6�   �� 5l      -   5        $��ShowHideButton��5l      #��[  6/   $��25��6Y   $��25��6d   $��Right=�6p   $��Center=�6�   $��0,0,5,0q�6�   .��5m      �� -   5        $��ShowHideImage��5m      $��15��61   $��15��6<   $S��Mhttps://static-00.iconduck.com/assets.00/eye-closed-icon-512x459-ivg8wjcx.png��6H   5n      5o      �� 5q      -	   5        $��	btnSignIn��5q   '   $��Sign In��6   $	��150��68   $��25��6D   $��Center=�6P   $��Arial)�6m   $
��Bold"�6�   $��16(�6�   ���	#FFDDDDDD#��[  6�    �� 	IsDefault$
 True��6�   �� 5r      -
   5         ��Name$ btnForgotPass��5r      $	��150��6,   $��Center=�68   $��Arial)�6U   $
��Bold"�6h   $��Forgotten Password?��6z   $��14(�6�   
�������$��0,10,0,0q�6�   
�������6�   5s      5t      5u      