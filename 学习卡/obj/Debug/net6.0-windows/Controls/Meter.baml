   M S B A M L   `   `   `  ����D  @学习卡, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:学习卡.Controls学习卡.Controls  5          学习卡.Controls.Meter   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:学习卡.Controls     5      .��5	      � .+�5
   
   �� -   5        $��back_border��5      ���#1c3866  J� Effect  5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6#      Opacity$	 0.5Q�63      ShadowDepth$ 0Q�5   $      Color$ #00cff8��64   5      F� 5   
   -   5        $��	pathBoser��5   4   ���#00cff8$��3��6     J� RenderTransformOrigin$ 0.5,0.5&�5      $��Center=�6/   $��Center=�6J   z�5      �� 5       �� Angle$	 -45Q�6"   5      �� 5   
   -   5         ��Name$ canvasPlate��5      F� 5   
   -   5        $��pointer��5      
���  ��$ 0.5,0.5&�5      z�5      �� 5      $	 135Q�6"   -   5        	rtPointer��5   .   5      �� 5   
   $��44��6   $��44��6    �� CornerRadius$ 22��6(   ���#0146ae  5!        5"      $ 20Q�6#   $	 0.3Q�63   $ 0Q�5#   #   $ Black��63   ._�5%      �� 
�������$��Center=�5&      $$��Center                        =�63   $��16(�5'      ��5%      �� *Value+	 �� RelativeSource	 �� 
 �� AncestorType
 UserControl �� StringFormat$ {0:0.#}��5'      5(      �� 5)   
   $��Bottom=�5*      $��Center=�5+      $��0,0,0,27q�5,      $��10(�6&   ���#2fff��5)      �� *Unit+	 �� 
 UserControl5-      �� 5.   
   $��Bottom=�5/      $��Center=�50      $��0,0,0,10q�51      $��9(�6    ���#5fff$��Microsoft YaHei)�52   !   ��5.      �� *Header+	 �� 
 UserControl52   !   53      54      