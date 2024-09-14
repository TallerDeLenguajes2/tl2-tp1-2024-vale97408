# *Taller de Lenguajes II- TP1*

### Ejercicio 2a

#### *¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?*
Considero que la relación realizada por *composición* es Pedido-Cliente, debido a que en esta ocurre que si la clase contenedora es destruida, los objetos contenidos también son destruidos. Es decir, si se elimina un pedido, el cliente también es eliminado.
Por otro lado, entre las relaciones realizadas por *agregación* tenemos la de Pedidos-Cadete y Cadete-Cadetería.La primera es debido a que independientemente de si el cadete es sustituido (eliminado) o no, la vida del objeto existe de manera independiente a la clase que lo contiene, es decir, un pedido puede reasignarse a otro cadete. Así también, Cadete-Cadetería  los cadetes existen independientemente fuera del contexto de la cadetería y luego añadidos a ella para la asignación de tareas.


#### *¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?*
     -La clase Cadeteria deberia tener los métodos: 

- `DarDeALtaPedidos`
- `Asignar Pedido`
- `ReasignarPedidos`
- `ModificarEstadoPedido`
- `InformeFinalJornada`

    -La clase Cadete debería tener los métodos:
    
- `EnviosCompletos`
- `CantidadPedidosCompletados`
- `JornalACobrar`

#### *Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos,propiedades y métodos deberían ser públicos y cuáles privados.*
 Según los principios de abstracción y ocultamiento, deberían ser privados todos los **atributos** debido a que son detalles internos del objeto a los que no deben ser accedidos o modificados directamente. Respecto a las **propiedades y métodos**, todos son públicos debido a que este sistema de gestión  consta de una interfaz controlada para interactuar con los datos del objeto. No tuve ningún caso para realizarlo privado.

#### *¿Cómo diseñaría los constructores de cada una de las clases?*
 - `Cadete`: Obtiene la información del ID, Nombre, Dirección, Teléfono y su Lista de Pedidos vacía.
  - `Cadeteria`: Contiene la información del Nombre, Teléfono y su Lista de Cadetes cargada con antelación.
  - `Pedido`: Consta del NroPedido, Observación del Pedido, Estado y los datos para construir el cliente como ser  Nombre, Dirección, Teléfono, y Datos de referencia de la dirección(Opcional).
  - `Cliente`: Incluye los datos del  Nombre, Dirección, Teléfono, y Datos de referencia de la dirección del cliente (Opcional).

#### *¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?*

Otra forma de realizar el diseño de clases con los temas vistos hasta este práctico sería agregar una clase específica para el Estado de los pedidos (Así lo implementé y fue útil). También, podría modificar la relación entre Cliente y Pedido de composición a agregación, ya que esto permitiría mantener los pedidos en el sistema incluso si el cliente es eliminado, con el fin de conservar un historial de ventas sin depender de la existencia del cliente. 


