# Entrenamiento
## Implementando Clean Architecture en aplicaciones .NET - Primeros pasos - Enero 2022

### Primer entrenamiento. Introduccion
Crear orden de compra.

Como usuario del siustem, deseo poder crear una orden de compra para solicitar productos del almacen.

Caso de uso

Datos de entrada:
	- Identificador del cliente
	- Direccion de envio
	- Ciudad de envio
	- Pais de envio
	- Codigo postal de envio
	- Lista de productos incluyendo:
		- Identificador del producto
		- Precio
		- Cantidad

Flujo Primario
	1. El usuario envia la solicitud "Crear order de compra" con los datos de entrada.
	2. EL sistema registra la orden de compra.
	3. El sistema confirma al usuario que su solicitud ha sido procesada notificandole el numero de la orden creada.

Consideraciones
	- Northwind maneja 4 tipos de transporte de mercancias: Maritimo, aereo, ferroviario y terreste. El tipo de transporte predeterminado es terrestre.
	- Northwind maneja 2 formas para especificar descuents: Mediante porcentaje y mediante cantidades absolutas. El descuento predeterminado de una compra es del 10%.
	- La fecha de la orde de compra correspondera a la fecha y hora en la que sea creada.
	- Si en la orden se especifican productos con el mismo identificador de producto, solo se agregara un producto con ese identificador y la cantidad registrada sera la suma de las cantidades de los productos con el mismo identificador.

### Segundo entrenamiendo. Validacion

Flujo Primario
	1. El usuario envia la solicitud "Crear order de compra" con los datos de entrada.
	2. El sistema valida los datos
	3. EL sistema registra la orden de compra.
	4. El sistema confirma al usuario que su solicitud ha sido procesada notificandole el numero de la orden creada.

Flujo alterno: Error de validacion
	1. El procesamiento de la solicitud es cancelado.
	2. El sistema muestra el error al usuario.

Validacion
	- El identificador del cliente es requerido y debe ser de 5 caracteres alfanumericos.
	- La direccion de envio es requerida y debe ser de una longitud maxima de 60 caracteres.
	- La ciudad es requerida y debe de tener una longitud minima de 3 caracteres y maxima de 15.
	- El pais es requerida y debe de tener una longitud minima de 3 caracteres y maxima de 15.
	- El codigo postal es opcional y debe tener una longitud maxima de 10 caracteres.
	- Debe especificarse al menos un producto en la orden.
	- De cada producto especificado enla order, sera requerido el id del producto, la cantidad y el precio.
	******************** validaciones adicionales nuevo requerimientos **************************************************
	- El identificador del cliente debe de existir en la base de datos.
	- El cliente no debe tener adeudos para que se pueda crear la orden de compra
	- El identificador de producto debe de existir en la base de datos
	- La cantidad en existencia del producto solicitado debe ser mayor o igual a la cantidad solicitada del producto.

## Ejercicio
Crear (agregar) la aplicacion NorthWind.Inventory para implementar

Caso de uso "Agregar nuevo Producto"

1. El usuario realiza una peticion de creacion de nuevo producto enviando los datos de entrada.
2. El sistema valida los datos de entrada.
3. El sistema registra el producto.
4. El sistema devuelve al usuario el id del producto creado.

Datos de entrada
	- Name (menor de 40 caractered y requerido)
	- UnitsInStock (Mayor o igual a cero)
	- UnitPrice (Payor que cero)

Consideraciones
	- De manera predeterminada un producto no esta discontinuado.


### Tercer entrenamiendo. Eventos de dominio

Flujo Primario
	1. El usuario envia la solicitud "Crear order de compra" con los datos de entrada.
	2. El sistema valida los datos
	3. EL sistema registra la orden de compra.
	4. Cuando el nnumero de productos de la ordern sea mayor que 3, el sistema enviara un correo electronico de notificacion de "Orden especial creada" al administrador de la empresa
	5. El sistema confirma al usuario que su solicitud ha sido procesada notificandole el numero de la orden creada.


### Cuarto entrenamiendo. Logs de aplicacion y dominio

Flujo Primario
	1. El usuario envia la solicitud "Crear order de compra" con los datos de entrada.
	2. El sistema valida los datos
	3. El sistema registra la accion "Inicio de creacion de ordend e compra" con fines de auditoria log dominio
	4. EL sistema registra la orden de compra.
	5. El sistems registra la accion "Orden de compra <numero de orden> creada" con fines de auditoria log dominio
	6. Cuando el nnumero de productos de la ordern sea mayor que 3, el sistema enviara un correo electronico de notificacion de "Orden especial creada" al administrador de la empresa
	7. El sistema confirma al usuario que su solicitud ha sido procesada notificandole el numero de la orden creada.

Flujo alterno: Error de en el funcionamiento de la aplicacion
	1. El procesamiento de la solicitud es cancelado
	2. El sistema registra el error encontrado con fines de diagnostico
	3. El sistema muestra el error al usuario



### Quinto entrenamiento. Manejo de transactiones entre repositorios

Flujo alterno: Error al crear la order
	1. El procesamiento de la solicitud es cancelado
	2. El sistema registra la accion "Creacion de orden cancelada" indicando de ser posible, el identificadod ela orde que haya sido cancelada.
	3. El sistema muestra el error al usuario.

### Sexto entrenamiento. Manejo de transactiones entre repositorios

Consideraciones
	- Solo usuario authenticados pueden crear ordenes.
	- Al crear una orden debera registrar el usuario que crea la orden.
	- Si hay usuario authenticado, cancelar el proceso e informar al usuario.

Flujo alterno: El usuario no esta autenticado
	1. El procesamiento de la solicitud es cancelado.
	2. El sistema muestra el error al usuario.


### Septimo entrenamiento. Autenticacion y autorizacion

# Caso de uso registar usuario
El sistema debera permitir el registro de datos del usuario

Datos de entrada:
	- Correo
	- Clave de acceso

Flujo primario
	1. El usuario envia los datos.
	2. El sistema crea el usuario.
	3. El sistema confirma al usuario el registro de la informacion.

Flujo alterno: error al crear los datos.
	1. El proceso es cancelado.
	2. El sistema indica el error al usuario.

# Caso de uso login
El usuario proporciona sus credenciales para optener un Token.

Datos de entrada:
	- Usuario (correo)
	- Clave de acceso

Flujo primario:
	1. El usuarioenvia los datos.
	2. El sistema valida los datos.
	3. El sistema devulve un token de acceso al usuario.

Flujo alterno: Error de autenticacion
	1. El sistem indica el error al usuario.

{
  "email": "string@yo.com",
  "password": "Demo12345$"
}