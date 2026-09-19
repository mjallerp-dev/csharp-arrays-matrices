using ArraysMatrices.Model;

namespace ArraysMatrices.Service;

public class Arreglos
{
    private Numero[] numeros = new Numero[10];

    public void CrearArreglo()
    {
        for (int i = 0; i < numeros.Length; i++)
        {
            numeros[i] = new Numero(Random.Shared.Next(100));
        }
    }

    public void MostrarConForClasico()
    {
        Console.WriteLine("Recorrido con for clásico:");
        for (int i = 0; i < numeros.Length; i++)
        {
            Console.WriteLine("[" + i + "] = " + numeros[i].Valor);
        }
    }

    public void MostrarConForEach()
    {
        Console.WriteLine("Recorrido con for-each:");
        foreach (Numero numero in numeros)
        {
            numero.MostrarInfo();
        }
    }

    public void CambiarImparesPorCero()
    {
        for (int i = 0; i < numeros.Length; i++)
        {
            if (numeros[i].EsImpar())
            {
                numeros[i].SetValor(0);
            }
        }
    }

    public void MultiplicarPorIndice()
    {
        for (int i = 0; i < numeros.Length; i++)
        {
            numeros[i].SetValor(numeros[i].Valor * i);
        }
    }

    public int BuscarLineal(int valor)
    {
        for (int i = 0; i < numeros.Length; i++)
        {
            if (numeros[i].Valor == valor)
            {
                return i;
            }
        }
        return -1;
    }
}
