   M S B A M L   `   `   `  ����b  ^PresentationFramework.Aero2, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TextBox.Static.Background  �� Color  ComboBox.Static.Border  ComboBox.Static.Glyph . *ComboBox.Static.Editable.Button.Background * &ComboBox.Static.Editable.Button.Border 1 -ComboBox.MouseOver.Editable.Button.Background �� EndPoint �� 
StartPoint � Color � Offset - )ComboBox.MouseOver.Editable.Button.Border / +ComboBox.Pressed.Editable.Button.Background + 'ComboBox.Pressed.Editable.Button.Border 0	 ,ComboBox.Disabled.Editable.Button.Background ,
 (ComboBox.Disabled.Editable.Button.Border  ComboBoxToggleButton �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� CornerRadius J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  ComboBoxTemplate �Name � ColumnDefinitions �Name � AllowsTransparency �� Mode �� RelativeSource �� MaxDropDownHeight �� ElementName ��Name �� ClearTypeHint ��Name-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation d�Name d� 	IsChecked ��Name �� SelectionBoxItemTemplate  �� SelectionBoxItem#! �� SelectionBoxItemStringFormat" �� ContentStringFormat# J� IsHitTestVisible$ ]� 
Conditions% �� Property& �� Value' �� Triggers�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      VThemesKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2  5      %z  &	        &	 #     &	 F     &	 i     &	 �     &	 �     &	 O    &	 r    &	     &		 1    &	
 V    &	 {    &	 !    7  '�� �    7  '�� �    �� 5      $  	#FFFFFFFF��68   �� 5      $  	#FFACACAC��65   �� 5	      $  	#FF606060��64   �� 5
      $  Transparent��6I   �� 5      $  Transparent��6E   �� 5      $	 0,1&�6P   $	 0,0&�6_   .��5   
   � $ 	#FFEBF4FC��6   $	 0.0Q�6)   � 5   
   $ 	#FFDCECFC��6   $	 1.0Q�6)   5      �� 5      $  	#FF7EB4EA��6H   �� 5      $	 0,1&�6N   $	 0,0&�6]   .��5   
   � $ 	#FFDAEBFC��6   $	 0.0Q�6)   � 5   
   $ 	#FFC4E0FC��6   $	 1.0Q�6)   5      �� 5      $  	#FF569DE5��6F   �� 5      $  Transparent��6K   �� 5      $  Transparent��6G   �� 5      " d�6)   .�5   
   ��  �@�OverridesDefaultStyle$
 true��62   �� 5   
    � ��$ false��6&   �� 5   
    � |�$ false��6&   �� 5   
    �@��	ClickMode$ Press=�6&   �� 5   
    � �� 5       �� 5!      " d�6"   .R�5"      �� $	 templateRoot��6   #��z��5#   !   #��z��5$   !   ���#DDD$
 5��5&   !   ._�5'      �� $	 splitBorder��6!   #��� r�66   $
 true��5(   %   $	��8,0q�6@   $��Right=�6M   $��1q�5)   %   
������ ._�5*      F� $
��path��5+   @   "����        � 	=  	= � z     	���#5DDD$��Center=�5*   F   $��Center=�5+   #   6@   5,      5-       5.      P� 5/       �@J�IsMouseOver$
 True��69   . �50      �� $
 path��6%    � �� �#AFFF6I   51      P� 52       � {�$ False��67   . �53      �� $ splitBorder��6%    � y�$ 	Collapsed=�6T   54      56      58      59      �� 5;      " ��6/   .R�5<   
   � $ templateRoot��6   $
 true��6%    5=      �� 5>      $��* �6#   �� 5?      #��� r�6#   $��0 �6v   .+�5A      � $ 
PART_Popup��6   $
 true��6(   $��2��6B   $��1q�5C      #��� .�6   $��Bottom=�5D      ��5B      �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.��5F      �� $	 dropDownBorder��6   #���  5G   !   $��1q�5H   !   $
 5��65   #��z 5I   !   ���#EFFF��5J   !   �� *ActualWidth+$ templateRoot��._�5L      �� $ DropDownScrollViewer��6'   
������ .��5M      � $
 grid��6#   $ Enabled=�61   .+�5N   "   �� $ ItemsPresenter��61   $ 	Contained=�6I   # z 6~   5O      5P      5Q      5R      d� 5S      $ toggleButton��6   #��z��61   $��0q�5T      #��z��5U      $��2��6G   8��  #{�z{�5X       5V      �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+5X      �� 5Y      $ contentPresenter��6   #��z 69   #��z��5Z   #   #��z  5[   #   #" z! 5\   #   #��z��5]   #   $# false��5^   #   #��z��6<   # z 5_   #   #��z��5`   #   5a       5b   
   P� 5c       �@��HasItems$ false��6*   . �5d      �� $ dropDownBorder��6+    � ��$ 95��6G   5e      ]� 5f      $ 5g      �� 5h      % �@��
IsGrouping$
& true��66   �� 5i      "% �@%�IsVirtualizingWhenGrouping$& false��6X   ./�5k      ��  � ��$ false��6B   5l      5n      �� 5p      " ��6   .�5q   
   ��  � �� �#1FFF6'   �� 5r   
    � �� �#9FFF6'   �� 5s   
    � ��$ 1q�6,   �� 5t   
    � ��$
 Auto=�6G   �� 5u   
    � ��$
 Auto=�6E   �� 5v   
    � ��$ 6,3,5,3q�6$   �� 5w   
    � ��$
 true��6:   �� 5x   
    �@��PanningMode$
 Both=�65   �� 5y   
    �@��IsFlicksEnabled$ False��63   �� 5z   
    � ��$ Center=�65   �� 5{   
    � ��8   6%   �� 5|   
    � ��$
 10,0q�6$   �� 5}   
    � ��$ 11(�6%   ' 5~   
   P� 5       � {�$ False��6+   . �5�      ��  � �� �#5FFF6/   5�      5�      �� 5�      " ��6   .�5�   
   ��  � ��$ 28��6#   �� 5�   
    � �� �#5556'   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� $��border��66   
������ $
 5��5�      ._�5�      �� $��Center=�6+   $
��10,0q�6F   5�       5�      P� 5�       �@J�IsMouseOver$
 True��69   . �5�      �� $ border��6%    � �� �#20006O   5�      P� 5�       �@��
IsSelected$
 True��68   . �5�      �� $ border��6%    � ��
 � ���6O   �� 5�       � ��
 �����6;   5�      5�      5�      5�      5�      