   M S B A M L   `   `   `  ����C  ?WpfApp11, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfApp11WpfApp11  5   	       WpfApp11.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfApp11     5   	     � Title$  
MainWindow��5   	   $	��720��6   $
��1270��6)   ���	#FF2D833D$
��1270��6M   $	��720��6]    � WindowStartupLocation$ CenterScreen=�6m   .��5
      �  � RowDefinitions 5   
   �� 5      $	��42* �6   �� 5      $
��377* �6   .+�5   
   �� -   5         ��Name$ CartShoping��5   )   $��Корзина��6>   $	��120��6P   $��30��6\   $��Right=�6h   $��Center=�6�   $��0,0,20,0q�6�   ���	#FFD49140{�.  6�   �� 5   
   -   5         ��Name$ ListProducts��5      $��Disabled=�6*   $��1��6`   ��5      �� 5      .R�5      � $��Center=�6     � Orientation$ 
Horizontal=�6=   5      ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!    � ContextMenu 5      �� 5      .@�5   "   m� $��
AddInOrder��6F   -   5        $&�� Добавить к заказу��5   +   6s   5      .+�5      �� $	��150��69   $	��150��6E   ��6    �� *Image+6E   �� 5       $ Vertical=�6%   .+�5!      �� ��6$   �� *FullViewName+�� 5"      ��6$   �� *FullViewDescription+�� 5#      ��6$   �� *FullViewManufacturer+�� 5$      ��6$   �� *FullViewPrice+�� 5%      ��6$   �� *FullViewDiscount+�� 5&      $ 
AddProduct��6M   -   5        $	��120��5&   %   $&�� Добавить к заказу��61   $
��Left=�6}   {�6�   ��  �� ElementName$ ListProducts��	 �� Path$	 SelectedItems.Count�5)      5*      5+      5-      5/      5a      