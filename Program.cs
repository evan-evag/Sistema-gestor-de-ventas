using System;
using System.Collections.Generic;

class Program
{
   
    // Estructuras de datos en memoria (listas paralelas por producto)

    static List<string> nombres = new List<string>();
    static List<decimal> precios = new List<decimal>();
    static List<int> stocks = new List<int>();
    static List<int> unidadesVendidas = new List<int>(); // acumula cantidad vendida por producto

    // Estadísticas de caja de la sesión
    static int ventasRealizadas = 0;
    static decimal totalCaja = 0;

    static void Main(string[] args)
    {
        int opcion;

        do
        {
            MostrarMenu();
            opcion = LeerOpcionMenu();

            switch (opcion)
            {
                case 1:
                    RegistrarProducto();
                    break;
                case 2:
                    ConsultarInventario();
                    break;
                case 3:
                    RegistrarVenta();
                    break;
                case 4:
                    ReporteCaja();
                    break;
                case 5:
                    Console.WriteLine("\n¡Gracias por usar el Mini-POS! Hasta pronto.");
                    break;
                default:
                    Console.WriteLine("\nOpción inválida. Por favor seleccione un número entre 1 y 5.");
                    break;
            }

            if (opcion != 5)
            {
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 5);
    }

    // Menú

    static void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("====================================================");
        Console.WriteLine("   SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)");
        Console.WriteLine("====================================================");
        Console.WriteLine("1. Registrar nuevo producto en inventario");
        Console.WriteLine("2. Consultar inventario completo");
        Console.WriteLine("3. Registrar una venta");
        Console.WriteLine("4. Ver reporte de caja y estadísticas diarias");
        Console.WriteLine("5. Salir");
        Console.WriteLine("====================================================");
        Console.Write("Seleccione una opción (1-5): ");
    }

    static int LeerOpcionMenu()
    {
        string entrada = Console.ReadLine();
        int opcion;

        if (int.TryParse(entrada, out opcion))
        {
            return opcion;
        }

        return -1; // fuerza el caso "default" del switch
    }

    // OPCIÓN 1: Registrar nuevo producto
   
    static void RegistrarProducto()
    {
        Console.WriteLine("\n--- REGISTRAR NUEVO PRODUCTO ---");

        string nombre;
        while (true)
        {
            Console.Write("Nombre del producto: ");
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío. Intente de nuevo.");
                continue;
            }

            bool existe = false;
            foreach (string n in nombres)
            {
                if (n.ToLower() == nombre.ToLower())
                {
                    existe = true;
                    break;
                }
            }

            if (existe)
            {
                Console.WriteLine("Ya existe un producto con ese nombre. Intente con otro.");
                continue;
            }

            break;
        }

        decimal precio;
        while (true)
        {
            Console.Write("Precio unitario ($): ");
            string entradaPrecio = Console.ReadLine();

            if (decimal.TryParse(entradaPrecio, out precio) && precio > 0)
            {
                break;
            }
            Console.WriteLine("Precio inválido. Debe ser un número decimal mayor a cero.");
        }

        int stock;
        while (true)
        {
            Console.Write("Stock inicial (cantidad disponible): ");
            string entradaStock = Console.ReadLine();

            if (int.TryParse(entradaStock, out stock) && stock >= 0)
            {
                break;
            }
            Console.WriteLine("Stock inválido. Debe ser un número entero mayor o igual a cero.");
        }

        nombres.Add(nombre);
        precios.Add(precio);
        stocks.Add(stock);
        unidadesVendidas.Add(0);

        Console.WriteLine($"\nProducto \"{nombre}\" registrado con éxito.");
    }


    // 2: Consultar inventario completo

    static void ConsultarInventario()
    {
        Console.WriteLine("\n--- INVENTARIO COMPLETO ---");

        if (nombres.Count == 0)
        {
            Console.WriteLine("No hay productos registrados todavía.");
            return;
        }

        for (int i = 0; i < nombres.Count; i++)
        {
            string alerta = stocks[i] < 5 ? "  [ALERTA: BAJO STOCK]" : "";
            Console.WriteLine($"{i + 1}. {nombres[i]} | Precio: {precios[i]:C} | Stock: {stocks[i]}{alerta}");
        }
    }

    //3: Registrar una venta

    static void RegistrarVenta()
    {
        Console.WriteLine("\n--- REGISTRAR VENTA ---");

        if (nombres.Count == 0)
        {
            Console.WriteLine("No hay productos registrados. Registre productos antes de vender.");
            return;
        }

        ConsultarInventario();

        int indice;
        while (true)
        {
            Console.Write("\nSeleccione el número del producto a vender: ");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out indice) && indice >= 1 && indice <= nombres.Count)
            {
                indice = indice - 1; // convertir a índice de lista (0-based)
                break;
            }
            Console.WriteLine("Producto inválido. Intente de nuevo.");
        }

        int cantidad;
        while (true)
        {
            Console.Write("Cantidad a vender: ");
            string entradaCantidad = Console.ReadLine();

            if (!int.TryParse(entradaCantidad, out cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad inválida. Ingrese un número entero mayor a cero.");
                continue;
            }

            if (cantidad > stocks[indice])
            {
                Console.WriteLine($"Stock insuficiente. Solo quedan {stocks[indice]} unidades disponibles.");
                continue;
            }

            break;
        }

        bool aplicaDescuento = false;
        while (true)
        {
            Console.Write("¿Aplica Descuento de Cliente Frecuente (10%)? (S/N): ");
            string respuesta = Console.ReadLine();

            if (respuesta != null && respuesta.Trim().ToUpper() == "S")
            {
                aplicaDescuento = true;
                break;
            }
            else if (respuesta != null && respuesta.Trim().ToUpper() == "N")
            {
                aplicaDescuento = false;
                break;
            }
            Console.WriteLine("Respuesta inválida. Ingrese S o N.");
        }

        // Cálculos obligatorios
        decimal subtotal = precios[indice] * cantidad;
        decimal descuento = aplicaDescuento ? subtotal * 0.10m : 0;
        decimal iva = (subtotal - descuento) * 0.19m;
        decimal total = subtotal - descuento + iva;

        // Actualizar inventario y estadísticas de caja
        stocks[indice] -= cantidad;
        unidadesVendidas[indice] += cantidad;
        ventasRealizadas++;
        totalCaja += total;

        // Ticket
        Console.WriteLine("\n=========== TICKET DE VENTA ===========");
        Console.WriteLine($"Producto: {nombres[indice]}");
        Console.WriteLine($"Precio unitario: {precios[indice]:C}");
        Console.WriteLine($"Cantidad: {cantidad}");
        Console.WriteLine($"Subtotal: {subtotal:C}");
        Console.WriteLine($"Descuento aplicado: {descuento:C}");
        Console.WriteLine($"IVA (19%): {iva:C}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"TOTAL A PAGAR: {total:C}");
        Console.WriteLine("========================================");
        Console.WriteLine("¡Gracias por su compra!");
    }

    //4: Reporte de caja y estadísticas diarias

    static void ReporteCaja()
    {
        Console.WriteLine("\n--- REPORTE DE CAJA Y ESTADÍSTICAS DEL DÍA ---");

        if (ventasRealizadas == 0)
        {
            Console.WriteLine("Aún no se han registrado ventas en esta sesión.");
            return;
        }

        decimal promedio = totalCaja / ventasRealizadas;

        int indiceMasVendido = 0;
        for (int i = 1; i < unidadesVendidas.Count; i++)
        {
            if (unidadesVendidas[i] > unidadesVendidas[indiceMasVendido])
            {
                indiceMasVendido = i;
            }
        }

        Console.WriteLine($"Total de ventas realizadas: {ventasRealizadas}");
        Console.WriteLine($"Total acumulado en caja: {totalCaja:C}");
        Console.WriteLine($"Promedio de dinero por venta: {promedio:C}");

        if (unidadesVendidas[indiceMasVendido] > 0)
        {
            Console.WriteLine($"Producto más vendido: {nombres[indiceMasVendido]} ({unidadesVendidas[indiceMasVendido]} unidades)");
        }
        else
        {
            Console.WriteLine("Aún no hay un producto con ventas registradas.");
        }
    }
}