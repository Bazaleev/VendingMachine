   M S B A M L   `   `   `  ����U  QVendingMachine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6781abdd364a2e36/clr-namespace:VendingMachineVendingMachine  5   	       VendingMachine.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:VendingMachine     5   	     � Title$  
MainWindow��5   	   $��574.582��6   $	��525��6-   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ customerWallet��5
      $��Stretch=�6+   $��
10,30,10,0q�6I   $	��Top=�6]      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+  5      $ Amount��6%    65   �� *Amount+4  .System.Windows.Controls.DataGridTemplateColumn  5      $  ��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ SizeToCells 63      
IsReadOnly .    CellTemplate 5      �� 5      .R�5      �� $	��Pay��6%   -   5        $	��Pay��5   B   	 � DataContext	 6P   �� *Value+5      5      5        5   
   -   5        $ vendingMachineWallet��5      $��Stretch=�61   $��10,180,10,0q�6O   $	��Top=�6d    .   5        5      $
 Name��6%    63   �� *Name+  5      $ Amount��6%    65   �� *Amount+5        5   
   -   5        $ products��5      $��Stretch=�6%   $��10,340,10,0q�6C   $	��Top=�6X    .   5        5      $
 Name��6%    63   �� *Name+  5       $ Price��6%    64   �� *Price+  5!      $ Amount��6%    65   �� *Amount+  5"      $  ��6)   $ SizeToCells 63    .  5#      �� 5$      .R�5%      �� $	��Buy��6%   -   5        $	��Buy��5%   B   	 6P   �� *
CategoryId+5&      5(      5*      �� 5+   
   -   5        
 ��Name$
 label��5+      $-��'Кошелек пользователя��6   $
��Left=�6>   $��10,2,0,0q�6Y   $	��Top=�6k   $	��172��6�   �� 5,   
   -   5        $
 label1��5,      $#��Внесенная сумма��6    $
��Left=�6:   $��10,10,10,10q�6U   $��Bottom=�6j   �� 5-   
   -   5         ��Name$ receivedMoneyButton��5-      $
��Left=�6/   $��23��6J   $��120,10,10,10q�6V    �� TextWrapping$
 Wrap=�6l   $�� ��6�   $��Bottom=�6�   $	��120��6�   �� 5.   
   -	   5        $
 label2��5.      $%��Кошелек автомата��6    $
��Left=�6;   $��
10,149,0,0q�6V   $	��Top=�6j   $	��172��6�   �� 5/   
   -
   5        $ changeTextBox��5/      $
��Left=�6)   $��23��6D   $��331,10,10,10q�6P   $
 Wrap=�6f   $�� ��6z   $��Bottom=�6�   $	��180��6�   �� 50   
   -   5         ��Name$ change��50      $��Выдать сдачу��6!   $
��Left=�6M   $��245,10,10,10q�6h   $��Bottom=�6~   $��81��6�   52      53      