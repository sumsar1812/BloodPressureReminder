   M S B A M L   `   `   `  ����>  :APC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:APCAPC  5   	       APC.BloodPresure   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:APC     5   	   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5   	     � WindowStyle$
  None=�5	   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowState$ 	Maximized=�6.   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��30* �6   �� 5      $
��118* �6   �� 5      $
��473* �6   �� 5      $
��118* �6   �� 5      $	��29* �6    � RowDefinitions 5   
   �� 5      $
��102* �6   �� 5      $
��308* �6   �� 5      $	��83* �6   �� 5      $	��42* �6   .+�5   
   �� -   5         ��Name$ TimerStatusBox��5      $��05:00��6(   $��1��68   $��10.4,0,11.2,5.4q�6H   $��72(�6a   $��Haettenschweiler)�6o   $��87��6�   $��Bottom=�6�   $��3��6�   �� 5   
   -   5         ��Name$ Speech��5      $��2��6&   $�� ��63   $��Tahoma)�6;   $��20(�6O   ���	#FF880909$
��Bold"�6t   $��0.2,0,0.2,10.2q�6�   $
��Wrap=�6�   $��2��6�   $��2��6�   $	��105��6�   $��Bottom=�6�   �� 5   
   -   5         ��Name$ InstructionImageWater��5   G   "  WpfAnimatedGif.ImageBehavior	   AnimatedSource$	 IMAGE/waterFall.gif��6   $��2��6f   $
��Left=�6v   $��1��6�   $	��Top=�6�   �� 5   
   -   5        $ InstructionCuffOn��5   D   $	 IMAGE/Putown.png��6   $y�Hidden=�6_   $��2��6s   $
��Left=�6�   $��1��6�   $	��Top=�6�   �� 5   
   -   5        $ InstructionCuffOff��5   E   $	 IMAGE/takeoff.png��6   $y�Hidden=�6a   $��2��6u   $
��Left=�6�   $��1��6�   $	��Top=�6�   5       5!      