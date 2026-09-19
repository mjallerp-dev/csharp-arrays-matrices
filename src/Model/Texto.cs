namespace ArraysMatrices.Model;

public class Texto
{
    public string Contenido { get; }
    public Palabra[] Palabras { get; set; }
    public PalabraFrecuencia[] Frecuentes { get; set; }

    public Texto(string contenido)
    {
        Contenido = contenido;
        Palabras = Array.Empty<Palabra>();
        Frecuentes = Array.Empty<PalabraFrecuencia>();
    }
}
