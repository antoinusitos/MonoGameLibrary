using System.Collections.Generic;

namespace MonoGameLibrary.Graphics.Material;

public class MaterialManager
{
    private Dictionary<string, Material> materials = new();
    
    private static MaterialManager instance;

    public Dictionary<string, Material> Materials => materials;

    public static MaterialManager Instance => instance;

    public static void Initialize()
    {
        instance = new();
    }

    public static bool TryGetMaterial(string name, out Material material) => Instance.materials.TryGetValue(name, out material);
}