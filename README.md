# Mini-POS — Sistema Gestor de Ventas e Inventario

**Estudiante:** Ivan Rafael Casasbuenas Casasbuenas

## Descripción del proyecto

Prototipo de consola en **.NET 8** que actúa como un Mini-POS (punto de venta) para una tienda de barrio. Permite registrar productos, consultar el inventario, procesar ventas con IVA (19%) y descuento de cliente frecuente (10%), y ver un reporte de caja y estadísticas del día. Toda la información se maneja **en memoria** (listas), sin base de datos.

## Restricciones del reto (Unidad 1)

Este proyecto usa únicamente:
- Variables y tipos primitivos (`int`, `decimal`, `string`, `bool`)
- Arreglos y `List<T>`
- Estructuras de control (`if/else`, `switch`, `for`, `foreach`, `while`, `do-while`)
- Métodos estáticos propios (`static`)
- Manejo de errores con `try/catch` e `int.TryParse` / `decimal.TryParse`

No incluye clases personalizadas (POO), constructores, herencia, interfaces, ni bases de datos/ORMs.

## Funcionalidades

1. **Registrar producto**: nombre, precio unitario y stock inicial, con validaciones y control de duplicados.
2. **Consultar inventario**: listado formateado con alerta de bajo stock (< 5 unidades).
3. **Registrar venta**: selección de producto, validación de stock, descuento opcional del 10%, cálculo de IVA (19%) y ticket detallado.
4. **Reporte de caja**: total de ventas, total en caja, promedio por venta y producto más vendido.
5. **Salir**: cierra la aplicación.

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download) instalado.
- Verifica la instalación con:
  ```bash
  dotnet --version
  ```

## Cómo clonar y ejecutar el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tu-usuario/nombre-del-repo.git
   cd nombre-del-repo
   ```
2. Si el repositorio no incluye el archivo de proyecto (`.csproj`), créalo una vez dentro de la carpeta:
   ```bash
   dotnet new console -o .
   ```
   (esto no sobrescribe `Program.cs` si ya existe con contenido; si te lo pide, confirma mantener tu `Program.cs`).
3. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```
4. Navega el menú ingresando el número de la opción deseada (1-5) y sigue las instrucciones en pantalla.

## Ejemplos de ejecución

**Menú principal:**
```
====================================================
   SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)
====================================================
1. Registrar nuevo producto en inventario
2. Consultar inventario completo
3. Registrar una venta
4. Ver reporte de caja y estadísticas diarias
5. Salir
====================================================
Seleccione una opción (1-5): 1
```

**Registrar un producto:**
```
--- REGISTRAR NUEVO PRODUCTO ---
Nombre del producto: Gaseosa 400ml
Precio unitario ($): 2500
Stock inicial (cantidad disponible): 20

Producto "Gaseosa 400ml" registrado con éxito.
```

**Consultar inventario:**
```
--- INVENTARIO COMPLETO ---
1. Gaseosa 400ml | Precio: $2,500.00 | Stock: 20
2. Pan tajado | Precio: $4,200.00 | Stock: 3  [ALERTA: BAJO STOCK]
```

**Registrar una venta:**
```
--- REGISTRAR VENTA ---
1. Gaseosa 400ml | Precio: $2,500.00 | Stock: 20
2. Pan tajado | Precio: $4,200.00 | Stock: 3  [ALERTA: BAJO STOCK]

Seleccione el número del producto a vender: 1
Cantidad a vender: 3
¿Aplica Descuento de Cliente Frecuente (10%)? (S/N): S

=========== TICKET DE VENTA ===========
Producto: Gaseosa 400ml
Precio unitario: $2,500.00
Cantidad: 3
Subtotal: $7,500.00
Descuento aplicado: $750.00
IVA (19%): $1,282.50
----------------------------------------
TOTAL A PAGAR: $8,032.50
========================================
¡Gracias por su compra!
```

**Reporte de caja:**
```
--- REPORTE DE CAJA Y ESTADÍSTICAS DEL DÍA ---
Total de ventas realizadas: 1
Total acumulado en caja: $8,032.50
Promedio de dinero por venta: $8,032.50
Producto más vendido: Gaseosa 400ml (3 unidades)
```

## Estructura del repositorio

```
MiniPOS/
├── Program.cs   # Lógica completa del sistema
└── README.md    # Este archivo
```
