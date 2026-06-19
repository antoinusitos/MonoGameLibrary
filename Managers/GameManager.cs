using System;

namespace MonoGameLibrary.Managers; 

public class GameManager
{
    public static float GameScale = 1;
    public static float TileSize = 32;

    public static float Gravity = 900;//9.81f;
    public static bool UseYSorting = true;
    public static bool UseInteractionSystem = false;

    public GameManager()
    {
       
    }
}

