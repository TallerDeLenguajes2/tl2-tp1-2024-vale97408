# *Taller de Lenguajes II- TP1*

### Ejercicio 2a

#### *¿Cuál de estas relaciones considera que se realiza por composición y cuál por agregación?*
Considero que la relación realizada por *composición* es Pedido-Cliente, debido a que en esta ocurre que si la clase contenedora es destruida, los objetos contenidos también son destruidos. Es decir, si se elimina un pedido, el cliente también es eliminado.
Por otro lado, entre las relaciones realizadas por *agregación* tenemos la de Pedidos-Cadete y Cadete-Cadetería.La primera es debido a que independientemente de si el cadete es sustituido (eliminado) o no, la vida del objeto existe de manera independiente a la clase que lo contiene, es decir, un pedido puede reasignarse a otro cadete. Así también, Cadete-Cadetería  los cadetes existen independientemente fuera del contexto de la cadetería y luego añadidos a ella para la asignación de tareas.


#### *¿Qué métodos considera que debería tener la clase Cadetería y la clase Cadete?*
    La clase Cadetería deberia tener los métodos: 
- Asignar Pedido


    -La clase Cadete debería tener los métodos:
- Agregar Pedido

#### *Teniendo en cuenta los principios de abstracción y ocultamiento, que atributos,propiedades y métodos deberían ser públicos y cuáles privados.*


#### *¿Cómo diseñaría los constructores de cada una de las clases?*


#### *¿Se le ocurre otra forma que podría haberse realizado el diseño de clases?*



