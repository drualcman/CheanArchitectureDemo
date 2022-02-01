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

Flujo alterno (Error de validacion)
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