   M S B A M L   `   `   `  ���� `  \CXConnect.Desktop.Modules.InstGenesys, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:CXConnect.Desktop.Modules.InstGenesys%CXConnect.Desktop.Modules.InstGenesys  ;    5CXConnect.Desktop.Modules.InstGenesys.InstGenesysView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Elocal3clr-namespace:CXConnect.Desktop.Modules.InstGenesys     $
��auto��.��� $��10,10q�  � 	Resources     BoolToVisConverter  CloseButton��%G   &	        &	      �� �� $��Images/close.png��$��25��$��25�� � ColumnDefinitions �� $��1* ��� $��3* �.+��� $��0��$��Center=�$	��Top=�.+��� $
��auto��$
��auto��$	��5,0q����� *
MediaImage+� $��Center=�$	��5,5q�.+��� $
��Bold"�$	��5,0q����� *MediaInsights+�� $��Like(s)��$
��auto��$	��0,0q��� $��1��.+�� ���#FDB92C �� $��0.03* ��� $��5* �.+�� $��1������  �� Path$ Comment.ItemColor� � RowDefinitions �� $��1* ��� $��2* �.+�� $��0��.+��� $��30��$��30��$	��5,5q����� $ Comment.ProfilePicBitMap��� .+��� $
��Bold"�$	��5,0q����� $ Comment.UserName�� .+��� $	��5,0q����� $ Comment.TimeStamp��� $��-��$��
ExtraBlack"�$	��0,0q��� $	��5,0q����� $ Comment.LikeCount��� $��Like(s)��$	��0,0q��� $��1��.+��� $	��5,2q�
������ $
��Wrap=����� $ Comment.Text�� $��Right=�$	��5,2q�.+��� -   $��Comment�� � ToolTip$ Reply to user's comment��$��Comment��
������ $��0q�$	��5,0q�
 � Tag �� $ 
Comment.Id�y��� *
CanComment+ �� 	Converter# [  �� -   $��Delete��$ Delete user's comment��
������ $��0q�$	��5,0q� �� $ 
Comment.Id�y��� *
CanComment+# [  .���� .+��� $��Images/Trash.png��$��20��$��20���� -   $
��Hide��$ Hide user's comment��
������ $��0q�$	��5,0q� �� $ 
Comment.Id�y��� *
CanComment+# [  .���� .+��� $��Images/Hide.png��$��20��$��20���� -   $��	ReplyList��$	��200��$��0q�$��5,0,0,0q�$��Stretch=�$
��Auto=����� *	Replies+����  �� 
TargetType" ��.���  �� Property �@��
IsSelected	 �� Value	 �� $ Selected����� .R�� ���#FDB92C �� $��0.03* ��� $��5* �.+�� $��1������ $ 	ItemColor� �� $��1* ��� $��2* �.+�� $��0��.+��� $��30��$��30��$	��5,5q����� $ ProfilePicBitMap��� .+��� $
��Bold"�$	��5,0q����� $ UserName�� .+��� $	��5,0q����� $ 	TimeStamp��� $��-��$��
ExtraBlack"�$	��0,0q��� $	��5,0q����� $ 	LikeCount��� $��Like(s)��$	��0,0q��� $��1��.+��� $	��5,2q�
������ $
��Wrap=����� $
 Text�� $��Right=�$	��5,2q�.+��� -   $ Reply to user's comment��$��Comment��
������ $��0q�$	��5,0q� �� $ Id�y��� $ 
Visibility�
 �� ElementName$
 Comment���� -   $ Delete user's comment��
������ $��0q�$	��5,0q� �� $ Id�y��� $ 
Visibility�$
 Delete��.���� .+��� $��Images/Trash.png��$��20��$��20���� -   $ Hide user's comment��
������ $��0q�$	��5,0q� �� $ Id�y��� $ 
Visibility�$

 Hide��.���� .+��� $��Images/Hide.png��$��20��$��20���� $	��0,5q�y��� *
CanReply+# [  .+�� $��Stretch=� �� $
��0.4* ��� $��6* �.+��� $��0��$��Center=�$	��5,0q�.+��� -   #��[ 
������ $��0q��� -	   $��Reply�� �� MinLines$ 3�� �� MaxLines$ 3��$��1�� �� TextWrapping$
 Wrap=�$��Stretch=�$��Enter reply here...��� $��0,5,0,0q�$��Right=�.+��� -
   $
��Send��$
��Send��$ Send reply to user��$
��Bold"�{�.  