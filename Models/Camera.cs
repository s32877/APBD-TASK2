namespace APBD_TASK2.Models;

public class Camera : Equipment
{
    public int MegaPixels { get; set; }
    public int NumberOfModes { get; set; }
    public int MaxResolution { get; set; }
    public Camera(string name, int megaPixels, int numberOfModes, int maxResolution) : base(name)
        {
        MegaPixels = megaPixels;
        NumberOfModes = numberOfModes;
        MaxResolution = maxResolution;
        }
}