   M S B A M L   `   `   `  ����>  :APC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:APCAPC  5   	       APC.WelcomeToTheSystem   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:APC     5   	     � Title$  WelcomeToTheSystem��5   	    � WindowState$ 	Maximized=�6$    � WindowStyle$
 None=�6<    � WindowStartupLocation$ CenterScreen=�6O    � 
ResizeMode$ NoResize=�6t   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��47* �6   �� 5      $	��81* �6   �� 5      $	��57* �6    � RowDefinitions 5   
   �� 5      $
��174* �6   �� 5      $
��142* �6   �� 5      $	��66* �6   �� 5      $
��141* �6   �� 5      $	��13* �6   .+�5   
   �� -   5         ��Name$ button��5      $��Opsæt!��6!   $��10.8,12.8,10.2,0q�62   $��2��6L   $��1��6Y   $��46��6~   $	��Top=�6�   �� 5   
   -   5         ��Name$ tbxName��5      $��35��6$   $��108.8,24.4,10.2,0q�60   	 �� TextWrapping$
	 Wrap=�6K   $	��Top=�6_   $��1��6w   $��1��6�   6�   �� 5   
   -   5        
 ��Name$
 label��5      $
��Navn��6   $
��Left=�6.   $��10.8,24.4,0,0q�6I   $	��Top=�6`   $��1��6x   $��1��6�   $��26��6�   $��38��6�   �� 5   
   -   5        $
 
label_Copy��5      $��Fødselsdato��6$   $��10.8,85.4,0,0q�6:   $	��Top=�6Q   $��1��6i    J� RenderTransformOrigin$ 0.416,0.821&�6v   $��1��6�   $
��Left=�6�   $��78��6�   $��26��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DateP��5      $��108.8,85.4,10.2,22.2q�6$   $��1��6B   $��1��6O   �� 5   
   -   5         ��Name$ image��5      $��Center=�6   $ ��IMAGE/doctorsLikeMoney.png��6<   $��1��6`   $��0��6p   $��Bottom=�6}   $��1,-1,75.2,0.6q�6�   $	��174��6�   $	��186��6�    � ToolTip$ I like Money��6�   �� 5   
   -   5         ��Name$ Speech��5      $��3��6&   $��SpeechSynth��63   $��Tahoma)�6F   $��20(�6Z   ���	#FF880909$
��Bold"�6   $��0.8,25.4,0.2,0.4q�6�   $
��Wrap=�6�   $��1��6�   5      5      