using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics.Material;

namespace MonoGameLibrary.Utils;

public static class ContentManagerExtension
{
    public static Material LoadMaterial(this ContentManager manager, string assetName)
    {
        return new Material(manager.Load<Effect>(assetName), (uint)MaterialManager.Instance.Materials.Count);
    }
    
    public static Material GetOrLoadMaterial(this ContentManager manager, string assetName)
    {
        if (!MaterialManager.TryGetMaterial(assetName, out Material material))
        {
            material = LoadMaterial(manager, assetName);
            MaterialManager.Instance.Materials[assetName] = material;
        }
        
        return material;
    }
}