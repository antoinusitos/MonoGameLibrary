using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;

namespace MonoGameLibrary.Systems;

public class UpdateSystem : GameSytem
{
    public override void Update(GameTime gameTime)
    {
        Entity[] entities = SceneManager.Instance.ActiveScene.Entities;
        for (int entityIndex = 0; entityIndex < entities.Length; entityIndex++)
        {
            if (entities[entityIndex] != null && entities[entityIndex].CanUpdate)
            {
                entities[entityIndex].Update(gameTime);
            }
        }
    }
}
