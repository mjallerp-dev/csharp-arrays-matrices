# Arrays, Matrices y Análisis de Texto (C#)

Aplicación de consola educativa en **C# / .NET 8**. Parte de un ejercicio original en **Java** (arreglos, matrices y análisis de texto) que se portó a C# conservando menús, mensajes y algoritmos. Después se adaptó para guardar **objetos, structs y records** como ítems de los arreglos y de la matriz.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Terminal con UTF-8 (Windows: la app asigna `Console.OutputEncoding = Encoding.UTF8`)

## Cómo ejecutar

Desde la raíz del repositorio:

```bash
dotnet run --project src
```

O:

```bash
cd src
dotnet run
```

Compilar sin ejecutar:

```bash
dotnet build src
```

No hay dependencias NuGet extra. La entrada se lee siempre con `Console.ReadLine()` (línea completa), nunca con `Read` / `ReadKey`.

## Uso

Menú principal:

```
1. Gestión de Arreglos
2. Gestión de Matrices
3. Analizar Texto
0. Salir
```

Hay que **crear** el arreglo o la matriz (opción 1 del submenú) antes del resto de operaciones. En texto, primero **leer el párrafo**.

### Arreglos

1. Crear arreglo (10 enteros aleatorios 0–99, cada uno encapsulado en un `Numero`)
2. Mostrar con for clásico → `[i] = n`
3. Mostrar con for-each → solo el número (`MostrarInfo()`)
4. Cambiar impares por cero (`SetValor(0)`)
5. Multiplicar por índice (la posición 0 queda en 0)
6. Búsqueda lineal por valor entero

### Matrices

1. Crear matriz 3×3 (valores 1 a 9, cada uno en un `Celda`)
2. Sumar elementos (la matriz inicial debe dar **45**)
3. Intercambiar primera y última fila (swap de referencias de filas, no celda a celda)

### Texto

1. Leer párrafo
2. Mostrar Top 5 de palabras frecuentes

## Datos para probar

### Matrices (sin datos extra)

Tras **Crear matriz**:

```
1 2 3
4 5 6
7 8 9
```

Recorrido por columnas:

```
Columna 0: 1 4 7
Columna 1: 2 5 8
Columna 2: 3 6 9
```

Suma: `45`. Tras intercambiar filas:

```
7 8 9
4 5 6
1 2 3
```

### Texto (párrafo de prueba)

En **Leer párrafo** pegar exactamente:

```
Hola, hola! Mundo. El mundo es bello, hola mundo.
```

Top 5 esperado:

```
1. hola -> 3
2. mundo -> 3
3. bello -> 1
4. el -> 1
5. es -> 1
```

`hola` y `mundo` empatan en frecuencia; gana `hola` por orden alfabético. El resto (frecuencia 1) también va alfabético: bello, el, es.

### Arreglos

Los 10 valores son aleatorios (0–99). Comprobar:

- for clásico: `[0] = n` … `[9] = n`
- for-each: un número por línea
- impares pasan a 0
- multiplicar por índice: índice 0 queda 0
- búsqueda: índice o `No se encontró el valor`
- texto no entero: `Debe ingresar un número entero.`

## De Java a C#

El enunciado original era Java: `int[]`, `int[][]`, `Scanner`, `Random.nextInt(100)`, `split("\\s+")`, `compareTo`.

Se portó a C# **sin cambiar la lógica de negocio**:

| Java | C# |
|------|-----|
| `Scanner.nextLine()` | `Console.ReadLine()` + `Trim()` en opciones |
| `Random.nextInt(100)` | `Random.Shared.Next(100)` (0–99) |
| `int[][]` (filas intercambiables) | `T[][]` jagged, **no** `int[,]` |
| `split("\\s+")` | `Split(..., RemoveEmptyEntries)` |
| `[^\\p{L}\\p{N}]+` | `Regex.Replace(..., @"[^\p{L}\p{N}]+", " ")` |
| `toLowerCase()` | `ToLower(new CultureInfo("es-ES"))` |
| `String.compareTo` | `string.CompareTo` (ordinal por defecto) |

Restricciones que se mantuvieron (como en Java): **no** `List`, `Dictionary`, LINQ ni `HashSet` en la lógica; arrays nativos, `for` clásico, `foreach` y búsqueda lineal.

Lo que sí cambió respecto al Java original: los ítems dejan de ser primitivos `int` / `String` y pasan a ser **clase, struct y record**.

## Arquitectura

```
src/
├── ArraysMatrices.csproj      # consola net8.0
├── Program.cs                 # menús y estado
├── Model/
│   ├── Numero.cs              # class (ítem del arreglo)
│   ├── Celda.cs               # struct (ítem de la matriz)
│   ├── Palabra.cs             # record Palabra y PalabraFrecuencia
│   └── Texto.cs               # class contenedora del análisis
└── Service/
    ├── Arreglos.cs            # Numero[10]
    ├── Matrices.cs            # Celda[3][]
    └── AnalizadorTexto.cs     # Palabra[] y PalabraFrecuencia[]
```

Namespaces: `ArraysMatrices`, `ArraysMatrices.Model`, `ArraysMatrices.Service`.

```
Program
  ├─ Arreglos        → Numero[]            (objetos)
  ├─ Matrices        → Celda[][]           (structs)
  └─ AnalizadorTexto → Texto
                         ├─ Palabra[]
                         └─ PalabraFrecuencia[]   (records)
```

`Program` solo orquesta menús y flags (`arregloCreado`, `matrizCreada`, `texto`). Los algoritmos viven en `Service`. Los tipos de ítem viven en `Model`.

## Objetos, struct y record como ítems

No se introdujo un dominio de “Estudiante”. Se aplicaron los tres conceptos **sobre el mismo ejercicio** (números, celdas, palabras).

### Objeto — `Numero` en el arreglo

`Arreglos` guarda `Numero[]`, no `int[]`.

- Tipo **referencia** (`class`).
- Estado encapsulado: `Valor` con `SetValor`.
- Comportamiento: `EsImpar()`, `MostrarInfo()`.
- Mutar un elemento **no copia** el objeto: `numeros[i].SetValor(0)` cambia la instancia que ya está en el arreglo.

Equivale a la actividad de clases: declarar, instanciar, guardar en arreglo, recorrer llamando un método, modificar con un setter.

### Struct — `Celda` en la matriz

`Matrices` guarda `Celda[][]` (jagged), no `int[][]`.

- Tipo **valor**. Cada celda es un `struct` con `Valor`.
- La matriz 3×3 se llena con `new Celda(valor)` (1 a 9).
- Suma y tabla leen `matriz[i][j].Valor`.
- Intercambiar filas sigue siendo swap de **referencias de filas** (`Celda[]`), igual que en Java con `int[][]`. No se copian celdas una a una.

Si se sacara una `Celda` a una variable local y se mutara, el cambio no se escribiría en la matriz (copia). Por eso las escrituras van sobre `matriz[i][j]`.

### Record — `Palabra` y `PalabraFrecuencia` en texto

`Texto` ya no usa `string[]` + `int[]` paralelos.

- `Palabra(string Valor)`: cada token del párrafo.
- `PalabraFrecuencia(string Texto, int Frecuencia)`: ranking.

Los records son **inmutables**. Al contar repeticiones no se hace `conteos[i]++`; se reemplaza el ítem del arreglo:

```csharp
unicas[indice] = unicas[indice] with { Frecuencia = unicas[indice].Frecuencia + 1 };
```

El Top 5 ordena el arreglo de records (selection sort) con el mismo criterio: mayor frecuencia y, en empate, `Texto` alfabético.

## Comparativa

### Java vs C# (mismo ejercicio)

| Aspecto | Java (origen) | C# (este repo) |
|---------|----------------|----------------|
| Lenguaje / runtime | JDK, `main` | .NET 8, `Program` |
| Entrada | `Scanner` | `Console.ReadLine()` |
| Arreglo 1D | `int[10]` | `Numero[10]` |
| Matriz | `int[3][3]` | `Celda[3][]` jagged |
| Palabras | `String[]` + `int[]` frecuencias | `Palabra[]` + `PalabraFrecuencia[]` |
| Matriz rectangular | No aplica igual | Se evita `int[,]`: no permite swap de filas por referencia |
| Colecciones | Prohibidas en lógica | Igual: sin `List` / LINQ / `Dictionary` |
| Menús y mensajes | Español, textos fijos | Los mismos |

### Class vs struct vs record (en este código)

| | `Numero` (class) | `Celda` (struct) | `Palabra` / `PalabraFrecuencia` (record) |
|--|------------------|------------------|------------------------------------------|
| Semántica | Referencia | Valor | Referencia inmutable (con igualdad por valor) |
| Dónde se guarda | Arreglo 1D | Matriz 3×3 | Arreglos de texto |
| Cómo se modifica | `SetValor` sobre la instancia | Asignar de nuevo la celda en `matriz[i][j]` | `with { Frecuencia = ... }` |
| Métodos | `MostrarInfo`, `EsImpar` | Datos (`Valor`) | Posicionales, sin mutadores |
| Riesgo típico | Varias posiciones podrían apuntar al mismo objeto | Copia accidental al leer en una variable | Hay que reasignar el slot del arreglo |

En una frase: el **objeto** muta in-place en el arreglo, el **struct** es una copia de valor en cada celda, el **record** se reemplaza entero cuando cambia la frecuencia.
