   M S B A M L   `   `   `  ����A  =aAppli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:aAppli.ViewModelsaAppli.ViewModels  5   	       aAppli.Views.CreditView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6
viewModelsclr-namespace:aAppli.ViewModels     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4TwpfJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5   	   -   5          � WindowState$  	Maximized=�5   	    � Title$ 
CreditView��5
   	   $	��300��6   $	��300��6)    � 	Resources 5      '   !aAppli.ViewModels.CreditViewModel   CreditViewModel  ActionTemplate �� Orientation � Cursor ^� ToolTip.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions6  0System.Windows.Interactivity.InvokeCommandAction	   Command
 �� Source   CommandParameter��%�  &	        &	      7     5   
   6%   �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� $,��&/aAppli;component/Images/DeleteRed.png��6   $
 Hand��5      $��5q�5      $��16��5      $��16��5       5      _� 5      .��5      �� $��Delete��6(   5       5        5      $ MouseLeftButtonDown��6)   . 5        	 64   �� *DeleteCommand+8
    5   4   �� 5      5      5!      5"      5#       � DataContext 5%      �� 5&   
   #
 [  6   .��5(      .  (Microsoft.Windows.Controls.BusyIndicator  -   5          Name$
 busy��5(      .��5)   
        IsBusy 6   �� *IsBusy+.��5*      �  � RowDefinitions 5+      �� 5,      $
��Auto �6$   �� 5-      .+�5/      �  � ColumnDefinitions 50      �� 51      �� 52      .+�54      �� $��Bottom=�55      $��5,-75q�56      
���  ��$��15(�58      $
��Bold"�59      ��54       �� *
QtRestante+59      &   System.Windows.Controls.DataGrid  5:      $	��150��6      AutoGenerateColumns .     CanUserAddRows .     AlternatingRowBackground �#9FC5F8$��2��5?      $��	101,0,0,0q�5@      ��5>      �� *ArticlesAVendre+ �� Mode$ TwoWay=�   Columns 5A      0  *System.Windows.Controls.DataGridTextColumn  5B      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Id��61    	  
Visibility$ Hidden=�5C   -   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 5D   -   �� *Id+  5E      $ Désignation��61    	  Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5G   -    5F   -   �� *Designation+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� 
  ElementStyle 5H   "   �� 5I   &    �� 
TargetType" ��6,   .�5J   *   ��  �� Property � �� �� Value$ Center=�5K   -   �� 5L   *    � ��$ 5q�5M   -   5N   '   5P        5Q      $ 
Prix Achat��61    5R   -   �� *	PrixAchat+ �� StringFormat$ N3��$ TwoWay=�$ PropertyChanged=� 5S   1   �� *PrixAchatVisibility+#
 [   5T   "   �� 5U   &   " ��6,   .�5V   *   ��  � ��$ Center=�5W   -   �� 5X   *    � ��$ 5q�5Y   -   5Z   '   5\        5]      $ 
Prix Vente��61    5^   -   �� *	PrixVente+$ N3��$ TwoWay=�$ PropertyChanged=� 5_   "   �� 5`   &   " ��6,   .�5a   *   ��  � ��$ Center=�5b   -   �� 5c   *    � ��$ 5q�5d   -   5e   '   5g        5h      $ S/N à Vendre��61   $	 250 5i   -    5j   -   �� *	SNAVendre+$ TwoWay=�$ PropertyChanged=� 5k   "   �� 5l   &   " ��6,   .�5m   *   ��  � ��$ Center=�5n   -   �� 5o   *    � ��$ 5q�5p   -   5q   '   5s        5t      $ QT à Vendre��61    5u   -   �� *	QTAVendre+$ TwoWay=�$ PropertyChanged=� 5v   "   �� 5w   &   " ��6,   .�5x   *   ��  � ��$ Center=�5y   -   �� 5z   *    � ��$ 5q�5{   -   5|   '   5~      4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Actions��65   $ 70 5�   1       CellTemplate#  [ 5�   1   5�      5�      � 5�      $��1��6    5�      �� 5�      $��* �6+   �� 5�       5�      �� 5�      $��* �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $��50 �6(   �� 5�      $	��100 �6(   .+�5�      �  5�      �� 5�      $��* �6/   �� 5�       5�      �� 5�      �� 5�      .+�5�      �� $ Vertical=�6%   $��90,60,79,50q�5�   !   $��3��5�   !   $��2��5�   !   $��Right=�5�   $   $	��Top=�5�   !   .+�5�      �� $��Nom Client :��6(   
����d�$
��Bold"�5�   $   $��5q�5�   $   �� 5�      $��5q�6&   $	��400��5�   "   ��5�   "   �� *	NomClient+$ TwoWay=�$ PropertyChanged=��� 5�      $��Coordonnées :��6(   
����d�$
��Bold"�5�   $   $��5q�5�   $   �� 5�      $��5q�6&   $	��400��5�   "   ��5�   "   �� *Coordonnees+$ TwoWay=�$ PropertyChanged=��� 5�      $��Mode paiement :��6(   
����d�$
��Bold"�5�   $   $��5q�5�   $   �� 5�      $��5q�6&   $	��400��5�   "   ��5�   "   �� *ModePaiement+$ TwoWay=�$ PropertyChanged=�5�      �� 5�      $��Désignation : ��6$   $
��Bold"�5�      $��5q�5�      $��1��5�       
����d�$��Center=�5�      �� 5�      $��Center=�5�      ! �� AcceptsReturn! . " �� TextWrapping$
" Wrap=�5�      $
��Bold"�5�      $��15(�5�      {�.  $��1��5�      $��1��5�      ��5�   "   �� *Designation+5�      5�      �� 5�      -   5        # ��Name$# lblPrixAchat��5�       $��Prix Achat :��5�       $��5q�5�      
����d�$
��Bold"�5�      $��Center=�5�      $��1��5�      �� 5�      -   5        $ ��Name$$ txtPrixAchat��5�      $��Center=�5�      {�.  $��1��5�      $��1��5�      $
��0,14q�5�      ��5�      �� *	PrixAchat+$ TwoWay=�$ PropertyChanged=�$ N3��5�      �� 5�      $��Prix Vente :��6    $��5q�5�      
����d�$
��Bold"�5�      $��Center=�5�      $��2��5�      �� 5�      $��Center=�5�      $
��Bold"�5�      $��2��5�      $��1��5�      ��5�      �� *	PrixVente+$ TwoWay=�$ PropertyChanged=�$ N3��5�      �� 5�      $��Q.T :��6    $��5q�5�      
����d�$
��Bold"�5�      $��Center=�5�      $��3��5�      �� 5�      $��Center=�5�      $
��Bold"�5�      $��3��5�      $��1��5�      ��5�      �� *qt+$ TwoWay=�$ PropertyChanged=�5�      �� 5�      $��S/N :��6    $��5q�5�      
����d�$
��Bold"�5�      $��Center=�5�      $��4��5�      �� 5�      -   5        $$ sn��5�      $��Center=�5�      $��1��5�      $��4��5�      |�. ��. ��5�      �� *SN+$ TwoWay=�$ PropertyChanged=�.��5�   +   % J� InputBindings% 5�      �� 5�      
& �� Key$& Enter��6)   ' �� Command' 5�   %   �� *EnterCommand+.��5�   1   5      �� 5     $��5��6!   $��2��5     $ 
Horizontal=�5     $��Center=�5     .+�5     �� $��Valider��6!   $��Center=�5     $��Center=�5     $��5q�5      5	       5
  "   $ Click��61   . 5  &     	 6<   �� *VendreCommand+#
 [   5  8   �� 5  #   5     �� 5     $��Effacer��6!   $��Center=�5     $��Center=�5     $��5q�5      5       5  "   $ Click��61   . 5  &     	 6<   �� *EffacerCommand+#
 [   5  8   �� 5  #   5     �� 5     $��
Historique��6!   $��Center=�5     $��Center=�5     $��5q�5      5       5   "   $ Click��61   . 5!  &     	 6<   �� *HistoriqueCommand+#
 [   5"  8   �� 5#  #   5%     5'     5)     5*     5+     5,     5-     