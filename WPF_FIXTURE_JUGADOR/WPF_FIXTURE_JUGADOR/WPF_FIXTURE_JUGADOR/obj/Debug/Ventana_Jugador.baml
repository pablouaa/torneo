   M S B A M L   `   `   `  ����N  JWPF_FIXTURE_JUGADOR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPF_FIXTURE_JUGADORWPF_FIXTURE_JUGADOR  5   	   )    #WPF_FIXTURE_JUGADOR.Ventana_Jugador   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:WPF_FIXTURE_JUGADOR     5   	   -   5          � Title$  Jugador��5      $	��584��6   $��575.5��6%   .��5	      � .+�5
   
   �� $
��Left=�6   $	��255��60   $��	10,10,0,0q�6=   $	��Top=�6P   $	��279��6h   .+�5      �� -   5         ��Name$ label1_Copy2��5      $��Número de Documento��6*   �� 5      -   5         ��Name$ txtDocumento��5      $��23��6,    �� TextWrapping$
 Wrap=�68   �� 5      -   5        $ label��5      $��Nombre��6#   �� 5      -   5        $ 	txtNombre��5      $��23��6)   $
 Wrap=�65   �� 5      -   5        $ label1��5      $��Apellido��6$   $��	0,0,0.4,0q�67   �� 5      -   5        $ txtApellido��5      $��23��6+   $
 Wrap=�67   �� 5      -   5        $ label1_Copy��5      $%��Fecha de Nacimiento  dd/mm/aaaa��6)   $��	0,0,0.4,0q�6S   �� 5      -	   5        $ txtFecha��5      $��23��6(   $
 Wrap=�64   �� 5      -
   5        $ label1_Copy1��5      $��Goles ��6*   $��	0,0,0.4,0q�6;   �� 5      -   5        $ txtGoles��5      $��23��6(   $
 Wrap=�64   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgGrilla��5      $
��Left=�6%   $��
10,329,0,0q�6@   $	��Top=�6T   $	��210��6l   $	��540��6y   5      �� 5   
   -   5         ��Name$ 
btnAgregar��5      $��Agregar��6%   $
��Left=�67   $��
13,270,0,0q�6R   $	��Top=�6f   $	��111��6~   $��45��6�   6�   �� 5    
   -   5        $ btnModificar��5       $��	Modificar��6'   $
��Left=�6;   $��155,270,0,0q�6V   $	��Top=�6k   $	��112��6�   $��45��6�   6�   �� 5!   
   -   5        $ 
btnLimpiar��5!      $��Limpiar��6%   $
��Left=�67   $��297,270,0,0q�6R   $	��Top=�6g   $	��112��6   $��45��6�   6�   �� 5"   
   -   5        $ btnEliminar��5"      $��Eliminar��6&   $
��Left=�69   $��439,270,0,0q�6T   $	��Top=�6i   $	��111��6�   $��45��6�   6�   �� 5#   
   -   5        $ btnCambiarImagen��5#      $��Cambiar Imagen��6+   $
��Left=�6D   $��360,173,0,0q�6_   $	��Top=�6t   $	��170��6�   $��32��6�   6�   �� 5$   
   -   5         ��Name$ imgFoto��5$      $
��Left=�6!   $	��158��6<   $��
360,15,0,0q�6I   $	��Top=�6]   $	��170��6u   �� 5%   
   -   5        $ 	btnBuscar��5%      $��Buscar Jugador��6$   $
��Left=�6=   $��360,218,0,0q�6X   $	��Top=�6m   $	��170��6�   $��36��6�   6�   5&      5'      