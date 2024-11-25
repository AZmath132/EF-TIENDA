using System;

namespace Menu
{
    public class GestorVentas
    {
        private string[,] productos = new string[0, 4];
        private string[,] ventas = new string[0, 7]; 
        private string[,] clientes = new string[0, 3];

        public void RegistrarProducto()
        {
            Console.Write("Código del producto: ");
            string codigo = Console.ReadLine();
            Console.Write("Nombre del producto: ");
            string producto = Console.ReadLine();
            Console.Write("Stock del producto: ");
            string stock = Console.ReadLine();
            Console.Write("Precio del producto: ");
            string precio = Console.ReadLine();

            for (int i = 0; i < productos.GetLongLength(0); i++)
            {
                if (productos[i, 0] == codigo || productos[i, 1] == producto)
                {
                    Console.WriteLine("Este código o nombre ya está registrado.");
                    return;
                }
            }

            string[,] nuevosProductos = new string[productos.GetLongLength(0) + 1, 4];
            for (int i = 0; i < productos.GetLongLength(0); i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    nuevosProductos[i, j] = productos[i, j];
                }
            }

            string[] nuevoProducto = { codigo, producto, stock, precio };
            for (int j = 0; j < 4; j++)
            {
                nuevosProductos[productos.GetLongLength(0), j] = nuevoProducto[j];
            }

            productos = nuevosProductos;

            Console.WriteLine("Producto registrado exitosamente.");
        }

        public void EditarProducto()
        {
            Console.Write("Código del producto a editar: ");
            string codigo = Console.ReadLine();
            for (int i = 0; i < productos.GetLongLength(0); i++)
            {
                if (productos[i, 0] == codigo)
                {
                    Console.Write("Nuevo nombre del producto: ");
                    productos[i, 1] = Console.ReadLine();
                    Console.Write("Nuevo stock del producto: ");
                    productos[i, 2] = Console.ReadLine();
                    Console.Write("Nuevo precio del producto: ");
                    productos[i, 3] = Console.ReadLine();
                    Console.WriteLine("Producto actualizado exitosamente.");
                    return;
                }
            }
            Console.WriteLine("Producto no encontrado.");
        }

        public void EliminarProducto()
        {
            Console.Write("Código del producto a eliminar: ");
            string codigo = Console.ReadLine();
            for (int i = 0; i < productos.GetLongLength(0); i++)
            {
                if (productos[i, 0] == codigo)
                {
                    string[,] nuevosProductos = new string[productos.GetLongLength(0) - 1, 4];
                    for (int j = 0, k = 0; j < productos.GetLongLength(0); j++)
                    {
                        if (j == i) continue;
                        for (int l = 0; l < 4; l++)
                        {
                            nuevosProductos[k, l] = productos[j, l];
                        }
                        k++;
                    }
                    productos = nuevosProductos;
                    Console.WriteLine("Producto eliminado exitosamente.");
                    return;
                }
            }
            Console.WriteLine("Producto no encontrado.");
        }

        public void RegistrarVenta()
        {
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido del cliente: ");
            string apellido = Console.ReadLine();

            // Verificar si el cliente ya existe
            string codigoCliente = null;
            bool clienteEncontrado = false;

            for (int i = 0; i < clientes.GetLongLength(0); i++)
            {
                if (clientes[i, 1] == nombre && clientes[i, 2] == apellido)
                {
                    clienteEncontrado = true;
                    codigoCliente = clientes[i, 0];
                    Console.WriteLine("Este usuario ya está registrado. ¿Desea seguir comprando (s) o volver a seleccionar su nombre (n)?");
                    string respuesta = Console.ReadLine();
                    if (respuesta.ToLower() == "s")
                    {
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // Registrar nuevo cliente si no existe
            if (!clienteEncontrado) 
            { string nuevoCodigoCliente; 
                bool codigoClienteExistente; 
                do { 
                    Console.Write("Código del nuevo cliente: "); 
                    nuevoCodigoCliente = Console.ReadLine(); 
                    codigoClienteExistente = false; 
                    for (int i = 0; i < clientes.GetLongLength(0); i++) 
                    { 
                        if (clientes[i, 0] == nuevoCodigoCliente) 
                        { codigoClienteExistente = true; 
                            Console.WriteLine("Este código de cliente ya está registrado. Ingrese un código de cliente diferente.");
                            break; } 
                    } 
                } while (codigoClienteExistente); 
                codigoCliente = nuevoCodigoCliente; 
                RegistrarCliente(codigoCliente, nombre, apellido); 
            }

            string nroVenta;
            bool ventaExistente;
            do
            {
                Console.Write("Número de venta: ");
                nroVenta = Console.ReadLine();
                ventaExistente = false;
                for (int i = 0; i < ventas.GetLongLength(0); i++)
                {
                    if (ventas[i, 0] == nroVenta)
                    {
                        ventaExistente = true;
                        Console.WriteLine("Este número de venta ya está registrado. Ingrese un número de venta diferente.");
                        break;
                    }
                }
            } while (ventaExistente);

            Console.WriteLine("Listado de Productos:"); 
            for (int i = 0; i < productos.GetLongLength(0); i++) 
            { 
                Console.WriteLine($"Código: {productos[i, 0]}, Producto: {productos[i, 1]}, Stock: {productos[i, 2]}"); 
            }
            Console.Write("Código del producto: "); 
            string codigoProducto = Console.ReadLine(); 
            bool productoEncontrado = false; 
            int indiceProducto = -1; 
            for (int i = 0; i < productos.GetLongLength(0); i++) 
            {
                if (productos[i, 0] == codigoProducto) 
                { productoEncontrado = true;
                    indiceProducto = i; 
                    Console.WriteLine($"Producto seleccionado: {productos[i, 1]}, Stock disponible: {productos[i, 2]}"); 
                    break; } 
            }

            if (!productoEncontrado)
            {
                Console.WriteLine("Código de producto no encontrado.");
                return;
            }

            Console.Write("Cantidad: ");
            int cantidad = int.Parse(Console.ReadLine());

            if (cantidad > int.Parse(productos[indiceProducto, 2]))
            {
                Console.WriteLine("Stock insuficiente.");
                return;
            }

            string[,] nuevasVentas = new string[ventas.GetLongLength(0) + 1, 7];
            for (int i = 0; i < ventas.GetLongLength(0); i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    nuevasVentas[i, j] = ventas[i, j];
                }
            }

            string[] nuevaVenta = { nroVenta, codigoProducto, cantidad.ToString(), productos[indiceProducto, 3], codigoCliente, nombre, apellido };
            for (int j = 0; j < 7; j++)
            {
                nuevasVentas[ventas.GetLongLength(0), j] = nuevaVenta[j];
            }

            ventas = nuevasVentas;

            productos[indiceProducto, 2] = (int.Parse(productos[indiceProducto, 2]) - cantidad).ToString();
            Console.WriteLine("Venta registrada exitosamente.");
        }

        public void ListarProductos()
        {
            Console.WriteLine("Productos registrados:");
            for (int i = 0; i < productos.GetLongLength(0); i++)
            {
                Console.WriteLine($"Código: {productos[i, 0]}, Producto: {productos[i, 1]}, Stock: {productos[i, 2]}, Precio: {productos[i, 3]}");
            }
        }

        public void ListarVentas()
        {
            Console.WriteLine("Ventas registradas:");
            for (int i = 0; i < ventas.GetLongLength(0); i++)
            {
                Console.WriteLine($"Venta #{ventas[i, 0]}, Código Producto: {ventas[i, 1]}, Cantidad: {ventas[i, 2]}, Precio Unitario: {ventas[i, 3]}, Código Cliente: {ventas[i, 4]}, Nombre: {ventas[i, 5]}, Apellido: {ventas[i, 6]}");
            }
        }

        public void RegistrarCliente(string codigo, string nombre, string apellido)
        {
            for (int i = 0; i < clientes.GetLongLength(0); i++)
            {
                if (clientes[i, 0] == codigo || (clientes[i, 1] == nombre && clientes[i, 2] == apellido))
                {
                    Console.WriteLine("Este código o nombre de cliente ya está registrado.");
                    return;
                }
            }

            string[,] nuevosClientes = new string[clientes.GetLongLength(0) + 1, 3];
            for (int i = 0; i < clientes.GetLongLength(0); i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    nuevosClientes[i, j] = clientes[i, j];
                }
            }

            string[] nuevoCliente = { codigo, nombre, apellido };
            for (int j = 0; j < 3; j++)
            {
                nuevosClientes[clientes.GetLongLength(0), j] = nuevoCliente[j];
            }

            clientes = nuevosClientes;

            Console.WriteLine("Cliente registrado exitosamente.");
        }

        public void ListarClientes()
        {
            Console.WriteLine("Clientes registrados:");
            for (int i = 0; i < clientes.GetLongLength(0); i++)
            {
                Console.WriteLine($"Código: {clientes[i, 0]}, Nombre: {clientes[i, 1]}, Apellido: {clientes[i, 2]}");
            }
        }
    }
}

