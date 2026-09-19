using ArraysMatrices.Model;

namespace ArraysMatrices.Service;

public class Matrices
{
    private Celda[][] matriz = new Celda[3][];

    public Matrices()
    {
        for (int i = 0; i < matriz.Length; i++)
        {
            matriz[i] = new Celda[3];
        }
    }

    public void CrearMatriz()
    {
        int valor = 1;
        for (int i = 0; i < matriz.Length; i++)
        {
            for (int j = 0; j < matriz[i].Length; j++)
            {
                matriz[i][j] = new Celda(valor);
                valor++;
            }
        }
    }

    public void MostrarComoTabla()
    {
        Console.WriteLine("Matriz 3x3 (tabla):");
        for (int i = 0; i < matriz.Length; i++)
        {
            for (int j = 0; j < matriz[i].Length; j++)
            {
                Console.Write(matriz[i][j].Valor + "\t");
            }
            Console.WriteLine();
        }
    }

    public void RecorrerPorColumnas()
    {
        Console.WriteLine("Recorrido por columnas:");
        int columnas = matriz[0].Length;
        for (int j = 0; j < columnas; j++)
        {
            Console.Write("Columna " + j + ": ");
            for (int i = 0; i < matriz.Length; i++)
            {
                if (i > 0)
                {
                    Console.Write(" ");
                }
                Console.Write(matriz[i][j].Valor);
            }
            Console.WriteLine();
        }
    }

    public int SumarElementos()
    {
        int suma = 0;
        for (int i = 0; i < matriz.Length; i++)
        {
            for (int j = 0; j < matriz[i].Length; j++)
            {
                suma += matriz[i][j].Valor;
            }
        }
        return suma;
    }

    public void IntercambiarPrimeraYUltimaFila()
    {
        int ultima = matriz.Length - 1;
        Celda[] temporal = matriz[0];
        matriz[0] = matriz[ultima];
        matriz[ultima] = temporal;
    }
}
