   M S B A M L   `   `   `  ����A  =aAppli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:aAppli.ViewModelsaAppli.ViewModels  5   	       aAppli.Views.StockView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6
viewModelsclr-namespace:aAppli.ViewModels     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4TwpfJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5   	     � WindowState$  	Maximized=�5   	    � Title$ 	StockView��5   	   $	��600��5	   	   $
��1000��5
   	    � 	Resources 5      &    aAppli.ViewModels.StockViewModel   StockViewModel  ActionTemplate �� Orientation � Cursor ^� ToolTip.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase   Actions6  0System.Windows.Interactivity.InvokeCommandAction	   Command
 �� Source   CommandParameter��%�  &	        &	      7   7   7     5   
   6$   �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� $'��!/aAppli;component/Images/save.png��6   $
 Hand��5      $��5q�5      $��16��5      $��16��5       5      _� 5      .��5      �� $
��Save��6(   5       5        5      $ MouseLeftButtonDown��6)   . 5        	 64   �� *SaveCommand+8
    5   4   �� 5       5"      �� 5$      $,��&/aAppli;component/Images/DeleteRed.png��6   $
 Hand��5%      $��5q�5&      $��16��5'      $��16��5(       5)      _� 5*      .��5+      �� $��Delete��6(   5,       5.        5/      $ MouseLeftButtonDown��6)   . 50        	 64   �� *DeleteCommand+8
   51   4   �� 52      54      �� 56      $'��!/aAppli;component/Images/Edit.png��6   $
 Hand��57      $��5q�58      $��16��59      $��16��5:       5;      _� 5<      .��5=      �� $
��Edit��6(   5>       5@        5A      $ MouseLeftButtonDown��6)   . 5B        	 64   �� *EditCommand+8
   5C   4   �� 5D      5F      5H      5I      5J       � DataContext 5K      �� 5L   
   #
 [  6   .��5N      ��  5O   
     5P      $ Loaded��6   . 5Q        	 6(   �� *StockViewLoadedCommand+5R      .��5T   
   .  (Microsoft.Windows.Controls.BusyIndicator  -   5          Name$
 busy��5T      .��5U           IsBusy 6    �� *IsBusy+.��5V      �  � RowDefinitions 5W      �� 5X      $
��Auto �6(   �� 5Y      .+�5[      �� $��5q�6!   $��1��5\      .+�5^      �� 
�������$��
1, 1, 1, 1q�5_      ._�5`      &   System.Windows.Controls.DataGrid  -   5          Name$ 
myDataGrid��5a      $	��400��5b   '   $��Visible=�5c      $��1,0,1,0q�5d         GridLinesVisibility$	 All=�5e      "   VerticalScrollBarVisibility$ Visible=�5f      $   HorizontalScrollBarVisibility$ Visible=�5g         CanUserResizeColumns .    CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     HorizontalGridLinesBrush �#f0f0f0   VerticalGridLinesBrush �#f0f0f0   AlternatingRowBackground �#9FC5F8��5`   '   �� *
Articles+ J� InputBindings 5o   "   �� 5p   &   
 �� Key$ Enter��61    �� Command 5q   )   �� *EnterCommand+ �� CommandParameter 5r   )   �� *SelectedItem+ �� ElementName$ 
myDataGrid��    Columns  5t   "   0  *System.Windows.Controls.DataGridTextColumn  5u   &   ,	  &System.Windows.Controls.DataGridColumn! 	  Header$! Id��69   " 	  
Visibility$" Hidden=�5v   1   1
  +System.Windows.Controls.DataGridBoundColumn# 
  Binding# 5w   1   �� *Id+$ �� Mode$$ TwoWay=�% �� UpdateSourceTrigger$% PropertyChanged=�  5x   &   $! Désignation��69   & 	  Width5  /System.Windows.Controls.DataGridLengthConverter$& * 5z   1   # 5y   1   �� *DesignationText+$$ TwoWay=�' 
  ElementStyle' 5{   *   �� 5|   .   ( �� 
TargetType"( ��64   .�5}   2   �� ) �� Property) � ��* �� Value$* Center=�5~   1   �� 5   2   ) � ��$* 5q�5�   1   5�   /   5�   '     5�   &   $! 
Prix Achat��69   # 5�   1   �� *	PrixAchat++ �� StringFormat$+ N3��$$ TwoWay=�$% PropertyChanged=�' 5�   *   �� 5�   .   "( ��64   .�5�   2   �� ) � ��$* Center=�5�   1   �� 5�   2   ) � ��$* 5q�5�   1   5�   /   5�   '     5�   &   $! 
Prix Vente��69   # 5�   1   �� *	PrixVente+$+ N3��$$ TwoWay=�$% PropertyChanged=�' 5�   *   �� 5�   .   "( ��64   .�5�   2   �� ) � ��$* Center=�5�   1   �� 5�   2   ) � ��$* 5q�5�   1   5�   /   5�   '     5�   &   $	! S/N��69   $	& 100 5�   1   # 5�   1   �� *SN+$$ TwoWay=�$% PropertyChanged=�' 5�   *   �� 5�   .   "( ��64   .�5�   2   �� ) � ��$* Center=�5�   1   �� 5�   2   ) � ��$* 5q�5�   1   5�   /   5�   '     5�   &   $! QT��69   # 5�   1   �� *QT+$$ TwoWay=�$% PropertyChanged=�' 5�   *   �� 5�   .   "( ��64   .�5�   2   �� ) � ��$* Center=�5�   1   �� 5�   2   ) � ��$* 5q�5�   1   5�   /   5�   '   4  .System.Windows.Controls.DataGridCheckBoxColumn  5�   &   $! Générique��6=   # 5�   5   �� *IsGenerique+$$ TwoWay=�$% PropertyChanged=�' 5�   *   �� 5�   .   "( ��64   .�5�   2   �� ) � ��$* Center=�5�   1   �� 5�   2   ) � ��$* Center=�5�   1   5�   /   5�   '   4  .System.Windows.Controls.DataGridTemplateColumn  5�   &   $! Actions��6=   $& 90 5�   5   ,   CellTemplate#, [ 5�   5   5�      5�      �� 5�      
�������$��
1, 1, 1, 1q�5�      ._�5�      �� .+�5�   "   �� -   5        $��+��5�   )   $
��Left=�5�   !   $��Center=�5�   !    5�   &     5�   *   $ Click��69   . 5�   .     	 6D   �� *
AddCommand+#
 [   5�   <   �� 5�   +   5�   #   �� 5�   "   $!��Masquer articles manquants?��6+   - d� 	IsChecked- 5�   +   �� *	IsMasquer+$$ TwoWay=�$% PropertyChanged=�6~   �� 5�   "   $��20q�6,   $%��Nbr. d'article dans le stock : ��5�   $   �� 5�      �6"   �� *
NbrArticle+#
 [  5�   #   �� 5�   "   $3��-/aAppli;component/Images/téléchargement.jpg��6(   $y�Hidden=�5�   (   $��50��5�       $��50��5�       $
 Hand��5�        5�   &   _� 5�   *   .��5�   .   �� $��Refresh��68   5�   +    5�   &     5�   *   $ MouseLeftButtonDown��69   . 5�   .     	 6D   �� *StockViewLoadedCommand+5�   +   5�   #   5�      5�      1  +Microsoft.Windows.Controls.WatermarkTextBox  5�      -   5        .  Name$. SearchInOther��5�   /   /   	Watermark$/ Vérifier existence ....��5�   /   ��5�   /   �� *SearchInOther+$$ TwoWay=�$% PropertyChanged=�.��6~    5�      �� 5�   "   $ Enter��6-    5�   !   �� *EnterCommandSearchInOther+.��5�   B   5�      �� 5�      $��Recherche avancée��6#   $��15q�5�   #   .��5�      � 0 � ColumnDefinitions0 5�   "   �� 5�   &   �� 5�   &   �� 5�   &   �� 5�   &   �� 5�   &   �� 5�   &   �� 5�   &   .+�5   "   �� $��Famille��6+   .��5  &   �� $ 
Horizontal=�61   .+�5  *   �� -   5        $��
cbFamilies��5  3   1 �� DisplayMemberPath$
1 Name��5     2 �� SelectedValuePath$2 Id��5     $	��125��62   5  +   �� 5  *   -   5        $��SearchByFamily��5  >   $��2q�63   $��Center=�5  )   5  T   5	  '   5
  #   �� 5  "   $��
Catégorie��6+   $��1��5  +   .��5  &   �� $ 
Horizontal=�61   .+�5  *   �� -   5        $��cbCategories��5  3   $
1 Name��5     $2 Id��5     $	��125��62   5  +   �� 5  *   -   5        $��SearchByCategory��5  >   $��2q�63   $��Center=�5  )   5  V   5  '   5  #   �� 5  "   $��Sous-Catégorie��6+   $��2��5  +   .��5  &   �� $ 
Horizontal=�61   .+�5  *   �� -	   5        $��cbSubCategories��5  3   $
1 Name��5     $2 Id��5     $	��125��62   5  +   �� 5   *   -
   5        $��SearchBySubCategory��5   >   $��2q�63   $��Center=�5!  )   5"  Y   5#  '   5$  #   �� 5&  "   $��Marque��6+   $��3��5'  +   .��5(  &   �� $ 
Horizontal=�61   .+�5)  *   �� -   5        $��cbBrands��5)  3   $
1 Name��5*     $2 Id��5+     $	��125��62   5,  +   �� 5-  *   -   5        $��SearchByBrand��5-  >   $��2q�63   $��Center=�5.  )   5/  S   50  '   51  #   �� 53  "   $��Modèle��6+   $��4��54  +   .��55  &   �� $ 
Horizontal=�61   .+�56  *   �� -   5        $��cbModels��56  3   $
1 Name��57     $2 Id��58     $	��125��62   59  +   �� 5:  *   -   5        $��SearchByModel��5:  >   $��2q�63   $��Center=�5;  )   5<  S   5=  '   5>  #   �� 5@  "   $��Taille��6+   $��5��5A  +   .��5B  &   �� $ 
Horizontal=�61   .+�5C  *   �� -   5        $��cbSizes��5C  3   $
1 Name��5D     $2 Id��5E     $	��125��62   5F  +   �� 5G  *   -   5        $��SearchBySize��5G  >   $��2q�63   $��Center=�5H  )   5I  R   5J  '   5K  #   �� 5M  "   $��Fournisseur��6+   $��6��5N  +   .��5O  &   �� $ 
Horizontal=�61   .+�5P  *   �� -   5        $��cbSuppliers��5P  3   $
1 Name��5Q     $2 Id��5R     $	��125��62   5S  +   �� 5T  *   -   5        $��SearchBySupplier��5T  >   $��2q�63   $��Center=�5U  )   5V  V   5W  '   5X  #   5Z     5[     �� 5]     
�������$	��150q�5^     $��
1, 1, 1, 1q�5_     ._�5`     �� .+�5a  "     -   5        $. search��5b  /   $/ 
Search ...��5a  7   5d  /   �� 5e  "   -   5        $��Effacer��5e  )   $��5q�5f  !   $
��Left=�5g  !   5h  !   �� 5i  "   $ 
Horizontal=�6-   $��Center=�5j  %   .+�5k  &   �� -   5        $��	Supprimer��5k  -   $��5q�5l  !   $
��Left=�5m  !   5n  !   �� 5o  &   $��Quantité : ��6,   $��5q�6B   $
��Bold"�6M   �� 5p  &   $��5q�5q  &   ��5p  .   �� *NbrArticleToBeDeleted+$$ TwoWay=�$% PropertyChanged=�5q  &   5r  #   �� 5s  "   -   5        $��btnVerifier��5s  B   $��Verifier Stock��6)   $��5q�5t  !   $��Center=�5u  !   �� 5v  "   $y�Hidden=�6)   $!��Supprimer Article en manque��6=   $��Center=�6c   $��50q�6�    5w  &     5x  *   $ Click��69   . 5y  .     	 6D   �� *DeleteManque+5z  +   5|  #   5}     5~     5�     5�     5�     5�     5�     5�     