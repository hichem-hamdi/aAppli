   M S B A M L   `   `   `  ����A  =aAppli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:aAppli.ViewModelsaAppli.ViewModels  5   	   '    !aAppli.Views.HistoriqueCreditView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6
viewModelsclr-namespace:aAppli.ViewModels     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4TwpfJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5	   	     � Title$  HistoriqueCreditView��5   	   $	��300��5   	   $	��300��5   	    � WindowState$ 	Maximized=�5
   	    � 	Resources 5      1   +aAppli.ViewModels.HistoriqueCreditViewModel   HistoriqueCreditViewModel  ActionTemplate �� Orientation � Cursor ^� ToolTip.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions6  0System.Windows.Interactivity.InvokeCommandAction	   Command
 �� Source   CommandParameter��%8  &	        &	      7   7     5   
   6/   �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5       �� $'��!/aAppli;component/Images/save.png��6   $
 Hand��5!      $��5q�5"      $��16��5#      $��16��5$       5%      _� 5&      .��5'      �� $
��Save��6(   5(       5*        5+      $ MouseLeftButtonDown��6)   . 5,        	 64   �� *SaveCommand+8
    5-   4   �� 5.      50      �� 52      $,��&/aAppli;component/Images/DeleteRed.png��6   $
 Hand��53      $��5q�54      $��16��55      $��16��56       57      _� 58      .��59      �� $��Delete��6(   5:       5<        5=      $ MouseLeftButtonDown��6)   . 5>        	 64   �� *DeleteCommand+8
   5?   4   �� 5@      5B      5D      5E      5F       � DataContext 5G      �� 5H   
   #
 [  6    5J        5K   
   $ Loaded��6   . 5L        	 6$   �� *" HistoriqueVenteViewLoadedCommand+5M      .��5O      �  � RowDefinitions 5P   
   �� 5Q      �� 5R      .+�5T   
   �� $��5q�6   $��0��5U      .+�5W      �� 
�������$��
1, 1, 1, 1q�5X      ._�5Y      &   System.Windows.Controls.DataGrid  -   5          Name$ 	GridItems��5Z      $��Visible=�5[         
IsReadOnly .  $	��300��5]      $��1,0,1,0q�5^         GridLinesVisibility$	 All=�5_      "   VerticalScrollBarVisibility$ Visible=�5`      $   HorizontalScrollBarVisibility$ Visible=�5a         CanUserResizeColumns .    CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     HorizontalGridLinesBrush �#f0f0f0   VerticalGridLinesBrush �#f0f0f0   AlternatingRowBackground �#9FC5F8��5Y      �� *Items+   Columns 5i      0  *System.Windows.Controls.DataGridTextColumn  5j      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6-      
Visibility$ Hidden=�5k   -   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 5l   -   �� *Id+  5m      $ Désignation��6-      Width5
  /System.Windows.Controls.DataGridLengthConverter$ *
 5o   -    5n   -   �� *Designation+ �� Mode$ TwoWay=�  �� UpdateSourceTrigger$  PropertyChanged=�! 	  ElementStyle! 5p      �� 5q   "   " �� 
TargetType"" ��6(   .�5r   &   �� # �� Property# � ��$ �� Value$$ Center=�5s   -   �� 5t   &   # � ��$$ 5q�5u   -   5v   #   5x        5y      $	 Q.T��6-   $ 30
 5{   -    5z   -   �� *
Quantite+! 5|      �� 5}   "   "" ��6(   .�5~   &   �� # � ��$$ Center=�5   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�        5�      $ 
Prix Achat��6-    5�   -   �� *	PrixAchat+% �� StringFormat$% N3��$ TwoWay=�$  PropertyChanged=�! 5�      �� 5�   "   "" ��6(   .�5�   &   �� # � ��$$ Center=�5�   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�        5�      $ 
Prix Vente��6-    5�   -   �� *	PrixVente+$% N3��$ TwoWay=�$  PropertyChanged=�! 5�      �� 5�   "   "" ��6(   .�5�   &   �� # � ��$$ Center=�5�   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�        5�      $	 S/N��6-   $	 180
 5�   -    5�   -   �� *SN+$ TwoWay=�$  PropertyChanged=�! 5�      �� 5�   "   "" ��6(   .�5�   &   �� # � ��$$ Center=�5�   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�        5�      $ Date De Vente��6-    5�   -   �� *DateDeVente+$% 
dd/MM/yyyy��! 5�      �� 5�   "   "" ��6(   .�5�   &   �� # � ��$$ Center=�5�   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�        5�      $ 
Nom Client��6-   $	 120
 5�   -    5�   -   �� *	NomClient+$ TwoWay=�$  PropertyChanged=�! 5�      �� 5�   "   "" ��6(   .�5�   &   �� # � ��$$ Center=�5�   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�        5�      $ Coordonnees��6-   $ 50
 5�   -    5�   -   �� *Coordonnees+$ TwoWay=�$  PropertyChanged=�! 5�      �� 5�   "   "" ��6(   .�5�   &   �� # � ��$$ Center=�5�   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�        5�      $ Mode Paiement��6-   $	 250
 5�   -    5�   -   �� *ModePaiement+$ TwoWay=�$  PropertyChanged=�! 5�      �� 5�   "   "" ��6(   .�5�   &   �� # � ��$$ Center=�5�   -   �� 5�   &   # � ��$$ 5q�5�   -   5�   #   5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      $ Actions��61   $ 70
 5�   1   &   CellTemplate#& [ 5�   1   5�      5�      5�      �� 5�   
   
�������$��
1, 1, 1, 1q�5�      $��1��5�      ._�5�      �� $��Center=�6   $	��Top=�5�      .+�5�      �� $
��De :��6   $
��Bold"�5�      (  "System.Windows.Controls.DatePicker  5�      -   5        '  Name$' DateDe��5�      $��1��5�      $��5q�5�      $	��150��5�      �� 5�      $	��A :��6   $��5q�5�      $
��Bold"�5�        5�      -   5        $' DateA��5�      $��5q�5�      $	��150��5�      �� 5�      $��Nom Client :��6   $��5q�5�      $
��Bold"�5�      �� 5�      -   5        ( ��Name$( txtNomClient��5�      $	��350��5�      $��5q�5�      ) �� CharacterCasing$) Upper=�5�   0   �� 5�      $
��SN :��6   $��5q�5�      $
��Bold"�5�      �� 5�      -   5        $( txtSN��5�      $	��350��5�      $��5q�5�      5�      �� 5�      -   5        * ��Name$* btnReset��5     $��Reset��5�      $��5q�5      5     �� 5     -   5        $* btnVisualiser��5     $��
Visualiser��5     $��5q�5     5     �� 5     -   5        $* btnAccessoires��5
     $��Accéssoires��5     $��5q�5	     5     5     5     5w     5x     