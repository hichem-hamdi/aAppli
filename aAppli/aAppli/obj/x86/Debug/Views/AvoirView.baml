   M S B A M L   `   `   `  ����A  =aAppli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:aAppli.ViewModelsaAppli.ViewModels  5   	       aAppli.Views.AvoirView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6
viewModelsclr-namespace:aAppli.ViewModels     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4TwpfJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5   	     � WindowState$  	Maximized=�5   	    � Title$ 	AvoirView��5   	   $	��300��5	   	   $	��300��5
   	    � 	Resources 5      &    aAppli.ViewModels.AvoirViewModel   AvoirViewModel��%   &	          5   
   6$   5       � DataContext 5      �� 5   
    �� Source# [  6   .��5      � $��Center=�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��S/N :��6   $��Center=�5      $��Center=�5       �� 5!   
   -   5         ��Name$ sn��5!      $��Center=�5"      $��5q�5$      $��1��5%      ��5#      �� *Sn+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�.��5%   "   
 J� InputBindings
 5&      �� 5'      
 �� Key$ Enter��6    �� Command 5(      �� *EnterCommand+.��5)   %   5*      �� 5+   
   $��Désignation :��6   $��1��5,      $��Center=�5-      $��Center=�5.      �� 5/   
   -   5        $ designation��50      {�.  $��1��51      $��Center=�52      $��5q�54      $��1��55      ��53      ��  �� Path$ Article.Designation�$ TwoWay=�55      &   System.Windows.Controls.DataGrid  57   
   $��10q�58      $��2��5:      $��2��5;      $��Visible=�5<         
IsReadOnly . $	��300��5>      $��1,0,1,0q�5?         GridLinesVisibility$	 All=�5@      "   VerticalScrollBarVisibility$ Visible=�5A      $   HorizontalScrollBarVisibility$ Visible=�5B         CanUserResizeColumns .     CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     HorizontalGridLinesBrush �#f0f0f0   VerticalGridLinesBrush �#f0f0f0   AlternatingRowBackground �#9FC5F8��57      �� *Items+ �� SelectedItem 59      �� *SelectedItemd+$ TwoWay=�   Columns 5J      0  *System.Windows.Controls.DataGridTextColumn  5K      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      
Visibility$ Hidden=�5L   -   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5M   -   �� *Id+  5N      $ Désignation��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5P   -    5O   -   �� *Designation+$ TwoWay=�$	 PropertyChanged=�   ElementStyle 5Q      �� 5R        �� 
TargetType"  ��6    .�5S      �� ! �� Property! � ��" �� Value$" Center=�5T   -   �� 5U      ! � ��$" 5q�5V   -   5W      5Y        5Z      $	 Q.T��6%    5[   -   �� *
Quantite+ 5\      �� 5]      "  ��6    .�5^      �� ! � ��$" Center=�5_   -   �� 5`      ! � ��$" 5q�5a   -   5b      5d        5e      $ 
Prix Achat��6%    5f   -   �� *	PrixAchat+# �� StringFormat$# N3��$ TwoWay=�$	 PropertyChanged=� 5g      �� 5h      "  ��6    .�5i      �� ! � ��$" Center=�5j   -   �� 5k      ! � ��$" 5q�5l   -   5m      5o        5p      $ 
Prix Vente��6%    5q   -   �� *	PrixVente+$# N3��$ TwoWay=�$	 PropertyChanged=� 5r      �� 5s      "  ��6    .�5t      �� ! � ��$" Center=�5u   -   �� 5v      ! � ��$" 5q�5w   -   5x      5z        5{      $	 S/N��6%   $	 250 5|   -    5}   -   �� *SN+$ TwoWay=�$	 PropertyChanged=� 5~      �� 5      "  ��6    .�5�      �� ! � ��$" Center=�5�   -   �� 5�      ! � ��$" 5q�5�   -   5�      5�        5�      $ Date De Vente��6%    5�   -   �� *DateDeVente+$# 
dd/MM/yyyy�� 5�      �� 5�      "  ��6    .�5�      �� ! � ��$" Center=�5�   -   �� 5�      ! � ��$" 5q�5�   -   5�      5�      5�      �� 5�   
   $	��O.k��6   $��3��5�      $��10q�5�      $��Center=�5�      $��Center=�5�      $��2��5�      {�5�      �� *IsOkEnabled+.  (System.Windows.Interactivity.Interaction$   Triggers$ 5�      /  )System.Windows.Interactivity.EventTrigger  5�      %   	EventName$% Click��6!   .	  (System.Windows.Interactivity.TriggerBase& 	  Actions.& 5�      6
  0System.Windows.Interactivity.InvokeCommandAction
  ' 
  Command' 6,   �� *	OkCommand+# [  5�      5�      5�      5�      