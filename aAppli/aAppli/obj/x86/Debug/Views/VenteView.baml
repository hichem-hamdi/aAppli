   M S B A M L   `   `   `  ����A  =aAppli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:aAppli.ViewModelsaAppli.ViewModels  5   	       aAppli.Views.VenteView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6
viewModelsclr-namespace:aAppli.ViewModels     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4TwpfJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5   	   -   5          � WindowState$  	Maximized=�5   	    � Title$ 	VenteView��5   	   $	��300��5	   	   $	��300��5
   	    � 	Resources 5      &    aAppli.ViewModels.VenteViewModel   VenteViewModel  ActionTemplate �� Orientation � Cursor ^� ToolTip.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions6  0System.Windows.Interactivity.InvokeCommandAction	   Command
 �� Source   CommandParameter��%�  &	        &	      7     5   
   6$   �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� $,��&/aAppli;component/Images/DeleteRed.png��6   $
 Hand��5      $��5q�5      $��16��5      $��16��5       5      _� 5      .��5      �� $��Delete��6(   5       5        5      $ MouseLeftButtonDown��6)   . 5        	 64   �� *DeleteCommand+8
    5   4   �� 5      5!      5#      5$      5%       � DataContext 5'      �� 5(   
   #
 [  6   .��5*      .  (Microsoft.Windows.Controls.BusyIndicator  -   5          Name$
 busy��5*      .��5+   
        IsBusy 6   �� *IsBusy+.��5,      �  � RowDefinitions 5-      �� 5.      $
��Auto �6$   �� 5/      .+�51      �  � ColumnDefinitions 52      �� 53      �� 54      .+�56      �� $��Right=�57       $��Bottom=�58      $��5,-75q�59      
���  ��$��15(�5;      $
��Bold"�5<      ��56       �� *
QtRestante+5<      &   System.Windows.Controls.DataGrid  5=      $	��150��6      AutoGenerateColumns .     CanUserAddRows .     AlternatingRowBackground �#9FC5F8$��2��5B      $��	101,0,0,0q�5C      ��5A      �� *ArticlesAVendre+ �� Mode$ TwoWay=�   Columns 5D      0  *System.Windows.Controls.DataGridTextColumn  5E      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Id��61    	  
Visibility$ Hidden=�5F   1   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 5G   1   �� *Id+  5H      $ Désignation��61    	  Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5J   1    5I   1   �� *Designation+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� 
  ElementStyle 5K   "   �� 5L   &    �� 
TargetType" ��6,   .�5M   *   ��  �� Property � �� �� Value$ Center=�5N   1   �� 5O   *    � ��$ 5q�5P   1   5Q   '   5S        5T      $ 
Prix Achat��61    5U   1   �� *	PrixAchat+ �� StringFormat$ N3��$ TwoWay=�$ PropertyChanged=� 5V   -   �� *PrixAchatVisibility+#
 [   5W   "   �� 5X   &   " ��6,   .�5Y   *   ��  � ��$ Center=�5Z   1   �� 5[   *    � ��$ 5q�5\   1   5]   '   5_        5`      $ 
Prix Vente��61    5a   1   �� *	PrixVente+$ N3��$ TwoWay=�$ PropertyChanged=� 5b   "   �� 5c   &   " ��6,   .�5d   *   ��  � ��$ Center=�5e   1   �� 5f   *    � ��$ 5q�5g   1   5h   '   5j        5k      $ S/N à Vendre��61   $	 250 5l   1    5m   1   �� *	SNAVendre+$ TwoWay=�$ PropertyChanged=� 5n   "   �� 5o   &   " ��6,   .�5p   *   ��  � ��$ Center=�5q   1   �� 5r   *    � ��$ 5q�5s   1   5t   '   5v        5w      $ QT à Vendre��61    5x   1   �� *	QTAVendre+$ TwoWay=�$ PropertyChanged=� 5y   "   �� 5z   &   " ��6,   .�5{   *   ��  � ��$ Center=�5|   1   �� 5}   *    � ��$ 5q�5~   1   5   '   5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      $ Actions��65   $ 70 5�   5       CellTemplate#  [ 5�   5   5�      5�      � 5�      $��1��6    5�      �� 5�      $
��Auto �6+   �� 5�       5�      �� 5�      $��* �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $	��100 �6(   .+�5�      �� $ Vertical=�6!   .+�5�      �� $ 
Horizontal=�6%   .+�5�      �� $
��5,14q�6(   $
��Bold"�66   $��Center=�6H   
��� � �$��Totale Vente:��6v   6�   �� 5�      $
��5,14q�6(   $
��Bold"�66   $��Center=�5�      
��� � ���6F   �� *PrixVenteTotale+$ TwoWay=�$ N3��6�   5�      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $
��5,14q�6(   $
��Bold"�66   $��Center=�5�      
��� � �$��Totale Marchandise:��6F   6e   �� 5�      $
��5,14q�6(   $
��Bold"�66   $��Center=�5�      
��� � ���6F   �� *PrixAchatTotale+$ TwoWay=�$ N3��6�   5�      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $
��5,14q�6(   $
��Bold"�66   $��Center=�5�      
��� � �$��Bénéfices TND:��6F   6`   �� 5�      $
��5,14q�6(   $
��Bold"�66   $��Center=�5�      
��� � ���6F   �� *BeneficeTnd+$ TwoWay=�$ N3��6�   5�      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $
��5,14q�6(   $
��Bold"�66   $��Center=�5�      
��� � �$��Bénéfices:��6F   5�      �� 5�      $
��5,14q�6(   $
��Bold"�66   $��Center=�5�      
��� � ���6F   �� *
Benefice+$ TwoWay=�6o   5�      5�      �� 5�      $��Désignation : ��6    $
��Bold"�5�      $��5q�5�      
����d�$��Center=�5�      $��2��64   �� 5�      $��Center=�5�      ! �� AcceptsReturn! . " �� TextWrapping$
" Wrap=�5�      $
��Bold"�5�      $��15(�5�      {�.  $��1��5�      $��2��6'   ��5�      �� *Designation+5�   '   �� 5�      -   5        # ��Name$# lblPrixAchat��5�       $��Prix Achat :��5�       $��5q�5�       
����d�$
��Bold"�5�      $��Center=�5�      $��3��5�      �� 5�      -   5        $ ��Name$$ txtPrixAchat��5�      $��Center=�5�      {�.  $��3��5�      $��1��5�      $
��0,14q�6&   ��5�      �� *	PrixAchat+$ TwoWay=�$ PropertyChanged=�$ N3��5�   &   �� 5�      $��Prix Vente :��6    $��5q�5�      
����d�$
��Bold"�5�      $��Center=�5�      $��4��5�      �� 5�      $��Center=�5�      $
��Bold"�5�      $��4��5�      $��1��5�      ��5�      �� *	PrixVente+$ TwoWay=�$ PropertyChanged=�$ N3��5�      �� 5�      $��Q.T :��6    $��5q�5�      
����d�$
��Bold"�5�      $��Center=�5�      $��5��5�      �� 5�      $��Center=�5�      $
��Bold"�5�      $��5��5�      $��1��5�      ��5�      �� *qt+$ TwoWay=�$ PropertyChanged=�5�      �� 5�      $��S/N :��6    $��5q�5�      
����d�$
��Bold"�5�      $��Center=�5�      $��6��5�      �� 5�      -   5        $$ sn��5�      $��Center=�5�      $��1��5�      $��6��5�      |�. ��. ��5�      �� *SN+$ TwoWay=�$ PropertyChanged=�.��5�   '   % J� InputBindings% 5�      �� 5�      
& �� Key$& Enter��6)   ' �� Command' 5�   !   �� *EnterCommand+.��5�   1   5�      �� 5�      $��7��6!   $��2��5�      $ 
Horizontal=�5�      $��Center=�5�      .+�5�      �� $��Vendre��6!   $��Center=�5�      $��Center=�5�      $��5q�5       5       5  "   $ Click��61   . 5  &     	 6<   �� *VendreCommand+#
 [   5  4   �� 5  #   5     �� 5     $��Effacer��6!   $��Center=�5	     $��Center=�5
     $��5q�5      5       5  "   $ Click��61   . 5  &     	 6<   �� *EffacerCommand+#
 [   5  4   �� 5  #   5     5     5     5     5     5     5     