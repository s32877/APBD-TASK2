namespace APBD_TASK2.Models;

public class Projector : Equipment
{
    public int Resulution {get; set;}
    public int Width {get; set;}
    public int Height {get; set;}
    public int Length {get; set;}

    public Projector(string name, int resulution, int width, int height, int length)
        : base(name)
    {
        Resulution = resulution;
        Width = width;
        Height = height;
        Length = length;
    }
}