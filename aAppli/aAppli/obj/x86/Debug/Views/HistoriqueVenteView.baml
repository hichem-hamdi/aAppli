   M S B A M L   `   `   `  ����A  =aAppli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:aAppli.ViewModelsaAppli.ViewModels  5   	   &     aAppli.Views.HistoriqueVenteView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6
viewModelsclr-namespace:aAppli.ViewModels     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4TwpfJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5   	   -   5          � WindowState$  	Maximized=�5   	    � Title$ HistoriqueVenteView��5   	   $	��300��5	   	   $	��300��5
   	    � 	Resources 5      0   *aAppli.ViewModels.HistoriqueVenteViewModel   HistoriqueVenteViewModel��%   &	          5   
   6.   5       � DataContext 5      �� 5   
    �� Source# [  6   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *" HistoriqueVenteViewLoadedCommand+5      .��5      � 	 � RowDefinitions	 5   
   �� 5      �� 5      .+�5   
   �� $��5q�6   $��0��5      .+�5       �� 
�������$��
1, 1, 1, 1q�5!      ._�5"      &   System.Windows.Controls.DataGrid  $��Visible=�5#      
   
IsReadOnly
 . $	��300��5%      $��1,0,1,0q�5&         GridLinesVisibility$	 All=�5'      "   VerticalScrollBarVisibility$ Visible=�5(      $   HorizontalScrollBarVisibility$ Visible=�5)         CanUserResizeColumns .     CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     HorizontalGridLinesBrush �#f0f0f0   VerticalGridLinesBrush �#f0f0f0   AlternatingRowBackground �#9FC5F8��5"      �� *Items+   Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6-      
Visibility$ Hidden=�53   -   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 54   -   �� *Id+  55      $ Désignation��6-      Width5
  /System.Windows.Controls.DataGridLengthConverter$ *
 57   -    56   -   �� *Designation+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� 	  ElementStyle 58      �� 59   "    �� 
TargetType" ��6(   .�5:   &   ��  �� Property � �� �� Value$ Center=�5;   -   �� 5<   &    � ��$ 5q�5=   -   5>   #   5@        5A      $	 Q.T��6-    5B   -   �� *
Quantite+ 5C      �� 5D   "   " ��6(   .�5E   &   ��  � ��$ Center=�5F   -   �� 5G   &    � ��$ 5q�5H   -   5I   #   5K        5L      $ 
Prix Achat��6-    5M   -   �� *	PrixAchat+  �� StringFormat$  N3��$ TwoWay=�$ PropertyChanged=� 5N      �� 5O   "   " ��6(   .�5P   &   ��  � ��$ Center=�5Q   -   �� 5R   &    � ��$ 5q�5S   -   5T   #   5V        5W      $ 
Prix Vente��6-    5X   -   �� *	PrixVente+$  N3��$ TwoWay=�$ PropertyChanged=� 5Y      �� 5Z   "   " ��6(   .�5[   &   ��  � ��$ Center=�5\   -   �� 5]   &    � ��$ 5q�5^   -   5_   #   5a        5b      $	 S/N��6-   $	 250
 5c   -    5d   -   �� *SN+$ TwoWay=�$ PropertyChanged=� 5e      �� 5f   "   " ��6(   .�5g   &   ��  � ��$ Center=�5h   -   �� 5i   &    � ��$ 5q�5j   -   5k   #   5m        5n      $ Date De Vente��6-    5o   -   �� *DateDeVente+$  
dd/MM/yyyy�� 5p      �� 5q   "   " ��6(   .�5r   &   ��  � ��$ Center=�5s   -   �� 5t   &    � ��$ 5q�5u   -   5v   #   5x      5{      5|      5}      �� 5~   
   $��
69,9,41,32q�6   $��1��5      .��5�   
   �� 
�������$��
1, 1, 1, 1q�5�      $��Stretch=�5�      $��Center=�5�      ._�5�      � $��Center=�6   ! � ColumnDefinitions! 5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6+   	 5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   .+�5�      �� $��Select a date :��6   $��Center=�5�      $��Center=�5�      (  "System.Windows.Controls.DatePicker  5�      -   5        "  Name$
" Date��5�      $��1��5�      $��5q�5�      $��Center=�5�      $��Center=�5�      �� 5�      -   5        $��Reset��5�      $��1��5�      $��30��5�      $��154,0,-100,0q�5�      $	��100��5�      �� 5�      $
��De :��6   $��1��5�        5�      -   5        $" DateDe��5�      $��1��5�      $��Center=�5�   "   $��Center=�5�      $��	36,0,0,10q�5�      $	��100��5�      �� 5�      $	��A :��6   $��1��5�      $��1��5�        5�      -   5        $" DateA��5�      $��1��5�      $��1��5�      $��Center=�5�      $��Center=�5�      $��	24,1,88,9q�5�      $	��100��5�      �� 5�      -   5        $
��Rest��5�      $��1��5�      $��1��5�      $��138,0,-35,0q�5�      $��30��5�      $	��109��5�      �� 5�      -   5        $��Géner Rapport��5�      $��Center=�5�      $	��100��5�      $��2��5�      $��5q�5�      $��2��5�      5�      �� 5�      -   5        $��Rapport Détaillé��5�      $��Center=�5�      $	��100��5�      $��3��5�      $��5q�5�      $��2��5�      5�      �� 5�      -	   5        $��Rapport Quantité��5�      $��Center=�5�      $	��100��5�      $��4��5�      $��2��5�      5�      �� 5�      -
   5        $��Rapport Excel��5�      $��Center=�5�      $	��100��5�      $��5��5�      $��5q�5�      $��2��5�      5�      �� 5�      -   5        $��Rapport Stock��5�      $��Center=�5�      $	��100��5�      $��6��5�      $��2��5�      $��5q�5�      5�      �� 5�      -   5        # ��Name$# WithSn��5�      $��6��5�      $��SN��5�      $��1��5�      $��96,59,22,59q�5�      �� 5�      -   5        $ ��Name$$ RapportType��5�      % �� SelectedIndex$% 0��5�      $��6��5�      $��1��5�      $��100,80,22,80q�5�      .@�5�      �� $��Aucun��6#   �� 5�      $��
Disponible��6#   �� 5�      $��Non disponible��6#   5�      �� 5�      -   5        $��Rapport Filtre��5�      $
��Left=�5�      $��Center=�5�      $��6��5�      $	��100��5�      5�      �� 5�      -   5        $��Rapport Financier��5�      $��Right=�5�      $��Center=�5�      $��6��5�      $��2��5�      $	��100��5�      5�      5�      5      5     5     5     