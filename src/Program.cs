using System.Text;
using ArraysMatrices.Model;
using ArraysMatrices.Service;

namespace ArraysMatrices;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Arreglos arreglos = new Arreglos();
        Matrices matrices = new Matrices();
        AnalizadorTexto analizador = new AnalizadorTexto();
        bool arregloCreado = false;
        bool matrizCreada = false;
        Texto? texto = null;
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine();
            Console.WriteLine("1. Gestión de Arreglos");
            Console.WriteLine("2. Gestión de Matrices");
            Console.WriteLine("3. Analizar Texto");
            Console.WriteLine("0. Salir");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine()?.Trim() ?? "";

            if (opcion == "1")
            {
                arregloCreado = MenuArreglos(arreglos, arregloCreado);
            }
            else if (opcion == "2")
            {
                matrizCreada = MenuMatrices(matrices, matrizCreada);
            }
            else if (opcion == "3")
            {
                texto = MenuTexto(analizador, texto);
            }
            else if (opcion == "0")
            {
                salir = true;
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }
    }

    static bool MenuArreglos(Arreglos arreglos, bool creado)
    {
        bool volver = false;
        while (!volver)
        {
            Console.WriteLine();
            Console.WriteLine("Gestión de Arreglos");
            Console.WriteLine("1. Crear arreglo (10 enteros aleatorios)");
            Console.WriteLine("2. Mostrar con for clásico");
            Console.WriteLine("3. Mostrar con for-each");
            Console.WriteLine("4. Cambiar impares por cero");
            Console.WriteLine("5. Multiplicar por índice");
            Console.WriteLine("6. Búsqueda lineal");
            Console.WriteLine("0. Volver");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine()?.Trim() ?? "";

            if (opcion == "1")
            {
                arreglos.CrearArreglo();
                creado = true;
                Console.WriteLine("Arreglo creado.");
            }
            else if (opcion == "0")
            {
                volver = true;
            }
            else if (!creado)
            {
                Console.WriteLine("Primero debe crear el arreglo.");
            }
            else if (opcion == "2")
            {
                arreglos.MostrarConForClasico();
            }
            else if (opcion == "3")
            {
                arreglos.MostrarConForEach();
            }
            else if (opcion == "4")
            {
                arreglos.CambiarImparesPorCero();
                Console.WriteLine("Impares cambiados por cero.");
            }
            else if (opcion == "5")
            {
                arreglos.MultiplicarPorIndice();
                Console.WriteLine("Valores multiplicados por su índice.");
            }
            else if (opcion == "6")
            {
                Console.Write("Valor a buscar: ");
                string entrada = Console.ReadLine()?.Trim() ?? "";
                if (!int.TryParse(entrada, out int valor))
                {
                    Console.WriteLine("Debe ingresar un número entero.");
                }
                else
                {
                    int indice = arreglos.BuscarLineal(valor);
                    if (indice >= 0)
                    {
                        Console.WriteLine("Encontrado en el índice " + indice);
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el valor");
                    }
                }
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }

        return creado;
    }

    static bool MenuMatrices(Matrices matrices, bool creada)
    {
        bool volver = false;
        while (!volver)
        {
            Console.WriteLine();
            Console.WriteLine("Gestión de Matrices");
            Console.WriteLine("1. Crear matriz 3x3 (1 a 9)");
            Console.WriteLine("2. Sumar elementos");
            Console.WriteLine("3. Intercambiar primera y última fila");
            Console.WriteLine("0. Volver");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine()?.Trim() ?? "";

            if (opcion == "1")
            {
                matrices.CrearMatriz();
                creada = true;
                Console.WriteLine("Matriz creada.");
                matrices.MostrarComoTabla();
                matrices.RecorrerPorColumnas();
            }
            else if (opcion == "2")
            {
                if (!creada)
                {
                    Console.WriteLine("Primero debe crear la matriz.");
                }
                else
                {
                    Console.WriteLine("Suma de elementos: " + matrices.SumarElementos());
                }
            }
            else if (opcion == "3")
            {
                if (!creada)
                {
                    Console.WriteLine("Primero debe crear la matriz.");
                }
                else
                {
                    matrices.IntercambiarPrimeraYUltimaFila();
                    Console.WriteLine("Primera y última fila intercambiadas.");
                    matrices.MostrarComoTabla();
                }
            }
            else if (opcion == "0")
            {
                volver = true;
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }

        return creada;
    }

    static Texto? MenuTexto(AnalizadorTexto analizador, Texto? texto)
    {
        bool volver = false;
        while (!volver)
        {
            Console.WriteLine();
            Console.WriteLine("Analizar Texto");
            Console.WriteLine("1. Leer párrafo");
            Console.WriteLine("2. Mostrar Top 5 de palabras frecuentes");
            Console.WriteLine("0. Volver");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine()?.Trim() ?? "";

            if (opcion == "1")
            {
                Console.WriteLine("Ingrese un párrafo:");
                string parrafo = Console.ReadLine() ?? "";
                texto = new Texto(parrafo);
                analizador.GuardarPalabras(texto);
                Console.WriteLine("Párrafo leído y palabras guardadas en el arreglo.");
            }
            else if (opcion == "2")
            {
                if (texto == null)
                {
                    Console.WriteLine("Primero debe leer un párrafo.");
                }
                else
                {
                    analizador.ContarRepeticiones(texto);
                    int top = analizador.ObtenerTop5(texto);
                    Console.WriteLine("Top 5 palabras más frecuentes:");
                    if (top == 0)
                    {
                        Console.WriteLine("(no hay palabras para ranking)");
                    }
                    else
                    {
                        for (int i = 0; i < top; i++)
                        {
                            Console.WriteLine((i + 1) + ". " + texto.Frecuentes[i].Texto + " -> " + texto.Frecuentes[i].Frecuencia);
                        }
                    }
                }
            }
            else if (opcion == "0")
            {
                volver = true;
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }

        return texto;
    }
}
