using System;

namespace MonoGameLibrary.Managers; 

public class GameManager
{
    public static float GameScale = 1;
    public static float TileSize = 32;

    public static readonly float Gravity = 900;//9.81f;
    public static readonly bool UseYSorting = true;
    public static readonly bool UseInteractionSystem = false;

    public GameManager()
    {
       
    }
}

