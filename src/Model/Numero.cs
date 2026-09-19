namespace ArraysMatrices.Model;

public class Numero
{
    public int Valor { get; private set; }

    public Numero(int valor)
    {
        Valor = valor;
    }

    public void SetValor(int valor)
    {
        Valor = valor;
    }

    public bool EsImpar()
    {
        return Valor % 2 != 0;
    }

    public void MostrarInfo()
    {
        Console.WriteLine(Valor);
    }
}
