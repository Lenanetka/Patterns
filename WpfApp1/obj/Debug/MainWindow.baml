   M S B A M L   `   `   `  ����C  ?Patterns, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       Patterns.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  Patterns��5   	    � Icon$ icon.png��6   $	��750��6*   $
��1200��67   .��5	      � .+�5   
   � -   5         �Name$ gridGovernment��5      $	��100��6'   $��	30,30,0,0q�64   $	��Top=�6G   $
��Left=�6_   $	��610��6z    � ColumnDefinitions 5      �� 5      .+�5      �� -   5         ��Name$ 	labelKing��5      $��Король��6'   $
��Left=�68   $��	10,38,0,0q�6S   $	��Top=�6f   $��26��6~   $��77��6�    J� RenderTransformOrigin$ 0.429,1.692&�6�   �� 5      -   5        $ 
labelQueen��5      $��Королева��6(   $
��Left=�6;   $��
308,38,0,0q�6V   $	��Top=�6j   $��26��6�   $��78��6�   5      �� 5   
   -   5         ��Name$ 
buttonNext��5      $��Дальше��6%   $
��Left=�66   $��
838,94,0,0q�6Q   $	��Top=�6e   $	��144��6}   6�   �� 5   
   -   5        $ 	labelYear��5      $��0 г.��6#   $
��Left=�62   $��
645,68,0,0q�6M   $	��Top=�6a   $	��537��6y   $��Center=�6�   �� 5   
   -   5         ��Name$ textLog��5      $	��576��6#   $��30,135,29.6,0q�60    �� TextWrapping$
 Wrap=�6G   $	��Top=�6[   ��. 	 �� 
IsReadOnly	 . 6�   5      5      