   M S B A M L   `   `   `  ����A  =aAppli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:aAppli.ViewModelsaAppli.ViewModels  5	   	   #    aAppli.Views.ManageCategories   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4TwpfJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5   	   0
viewModelsclr-namespace:aAppli.ViewModels  5	   	     � Title$  ManageCategories��5   	   $	��600��6"   $
��1000��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ Normal=�5
   	    � 	Resources 5      1   +aAppli.ViewModels.ManageCategoriesViewModel   ManageCategoriesViewModel  ActionTemplate �� Orientation � Cursor ^� ToolTip.  (System.Windows.Interactivity.Interaction   Triggers/  )System.Windows.Interactivity.EventTrigger   	EventName.  (System.Windows.Interactivity.TriggerBase	   Actions6  0System.Windows.Interactivity.InvokeCommandAction
   Command �� Source   CommandParameter��%�  &	        &	      7   7     5   
   6/   �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� $'��!/aAppli;component/Images/save.png��6   $
 Hand��5      $��5q�5      $��16��5      $��16��5      y�5      �� *ActionVisiblity+ 5      _� 5      .��5      �� $
��Save��6(   5       5        5      $ MouseLeftButtonDown��6)   .	 5        
 64   �� *SaveCommand+8    5   4   �� 5      5!      �� 5#      $,��&/aAppli;component/Images/DeleteRed.png��6   $
 Hand��5$      $��5q�5%      $��16��5'      $��16��5(      y�5&      �� *ActionVisiblity+ 5)      _� 5*      .��5+      �� $��Delete��6(   5,       5.        5/      $ MouseLeftButtonDown��6)   .	 50        
 64   �� *DeleteCommand+8   51   4   �� 52      54      56      57      58       � DataContext 59      �� 5:   
   # [  6    5<        5=   
   $ Loaded��6   .	 5>        
 6$   �� *LoadedCommand+5?      .��5A      .  (Microsoft.Windows.Controls.BusyIndicator  -   5          Name$
 busy��5A      .��5B   
        IsBusy 6   �� *IsBusy+.��5C      � .+�5D      �� $��5q�6   .+�5E      �� 
�������$��
1, 1, 1, 1q�5F      ._�5G      &   System.Windows.Controls.DataGrid  -   5          Name$ 
myDataGrid��5H      $��Visible=�5I      $	��300��5J      $��1,0,1,0q�5K         GridLinesVisibility$	 All=�5L      "   VerticalScrollBarVisibility$ Visible=�5M      $   HorizontalScrollBarVisibility$ Visible=�5N         CanUserResizeColumns .     CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     HorizontalGridLinesBrush �#f0f0f0   VerticalGridLinesBrush �#f0f0f0   AlternatingRowBackground �#9FC5F8��5G   #   �� *
Categories+   Columns 5V      0  *System.Windows.Controls.DataGridTextColumn  5W   "   ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Id��65    	  
Visibility$ Hidden=�5X   -   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 5Y   -   �� *Id+  5Z   "   $	 Nom��65    	  Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5\   -    5[   -   �� *Name+  �� Mode$  TwoWay=�! �� UpdateSourceTrigger$! PropertyChanged=�" 
  ElementStyle" 5]   &   �� 5^   *   # �� 
TargetType"# ��60   .�5_   .   �� $ �� Property$ � ��% �� Value$% Center=�5`   -   �� 5a   .   $ � ��$% 5q�5b   -   5c   +   & 
  EditingElementStyle& 5e   &   �� 5f   *   "# ��60   .�5g   .   �� $ �@��CharacterCasing$% Upper=�6P   5h   +   5j   #   4  .System.Windows.Controls.DataGridComboBoxColumn  5k   "   '   DisplayMemberPath$
' Name��69   $ Famille��5l   )   $ * 5m   )   (   SelectedItemBinding( 5n   )   �� *SelectedFamily+$  TwoWay=�$! PropertyChanged=�)   ElementStyle) 5o   &   �� 5p   *   "# ��60   .�5q   .   �� $ � ��% 6L   �� *
Families+5r   +   *   EditingElementStyle* 5t   &   �� 5u   *   "# ��60   .�5v   .   �� $ � ��% 6L   �� *
Families+5w   +   5y   #   4  .System.Windows.Controls.DataGridTemplateColumn  5z   "   $ Actions��69   $ 70 5{   1   +   CellTemplate#+ [ 5|   1   5~      5      �� 5�      
�������$��
1, 1, 1, 1q�5�      ._�5�      �� .+�5�      �� -   5        $��+��5�   %   $
��Left=�5�      $��Center=�5�      5�      5�      5�      5�      5�      5�      5�      5�      