using System.Globalization;
using System.Text.RegularExpressions;
using ArraysMatrices.Model;

namespace ArraysMatrices.Service;

public class AnalizadorTexto
{
    public string Normalizar(string contenido)
    {
        if (string.IsNullOrWhiteSpace(contenido))
        {
            return "";
        }

        string enMinusculas = contenido.ToLower(new CultureInfo("es-ES"));
        string reemplazado = Regex.Replace(enMinusculas, @"[^\p{L}\p{N}]+", " ");
        return reemplazado.Trim();
    }

    public void GuardarPalabras(Texto texto)
    {
        string normalizado = Normalizar(texto.Contenido);
        if (normalizado.Length == 0)
        {
            texto.Palabras = Array.Empty<Palabra>();
            return;
        }

        string[] tokens = normalizado.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Palabra[] palabras = new Palabra[tokens.Length];
        for (int i = 0; i < tokens.Length; i++)
        {
            palabras[i] = new Palabra(tokens[i]);
        }
        texto.Palabras = palabras;
    }

    public void ContarRepeticiones(Texto texto)
    {
        Palabra[] palabras = texto.Palabras;
        PalabraFrecuencia[] unicas = new PalabraFrecuencia[palabras.Length];
        int cantidad = 0;

        for (int i = 0; i < palabras.Length; i++)
        {
            string palabra = palabras[i].Valor;
            int indice = Buscar(unicas, cantidad, palabra);
            if (indice == -1)
            {
                unicas[cantidad] = new PalabraFrecuencia(palabra, 1);
                cantidad++;
            }
            else
            {
                unicas[indice] = unicas[indice] with { Frecuencia = unicas[indice].Frecuencia + 1 };
            }
        }

        PalabraFrecuencia[] frecuentes = new PalabraFrecuencia[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            frecuentes[i] = unicas[i];
        }

        texto.Frecuentes = frecuentes;
    }

    public int ObtenerTop5(Texto texto)
    {
        PalabraFrecuencia[] frecuentes = texto.Frecuentes;
        int n = frecuentes.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int mejor = i;
            for (int j = i + 1; j < n; j++)
            {
                if (VaAntes(frecuentes, j, mejor))
                {
                    mejor = j;
                }
            }

            if (mejor != i)
            {
                PalabraFrecuencia temporal = frecuentes[i];
                frecuentes[i] = frecuentes[mejor];
                frecuentes[mejor] = temporal;
            }
        }

        return Math.Min(5, n);
    }

    private bool VaAntes(PalabraFrecuencia[] frecuentes, int origen, int destino)
    {
        if (frecuentes[origen].Frecuencia != frecuentes[destino].Frecuencia)
        {
            return frecuentes[origen].Frecuencia > frecuentes[destino].Frecuencia;
        }

        return frecuentes[origen].Texto.CompareTo(frecuentes[destino].Texto) < 0;
    }

    private int Buscar(PalabraFrecuencia[] unicas, int cantidad, string palabra)
    {
        for (int i = 0; i < cantidad; i++)
        {
            if (unicas[i].Texto.Equals(palabra))
            {
                return i;
            }
        }

        return -1;
    }
}
