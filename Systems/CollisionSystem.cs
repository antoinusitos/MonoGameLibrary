using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;

namespace MonoGameLibrary.Systems;

public class CollisionSystem : GameSytem
{
    public override void Update(GameTime gameTime)
    {
        Entity[] entities = SceneManager.Instance.ActiveScene.Entities;
        for (int entityIndex = 0; entityIndex < entities.Length; entityIndex++)
        {
            if (entities[entityIndex] != null && entities[entityIndex].CanCollide)
            {
                for (int otherIndex = 0; otherIndex < entities.Length; otherIndex++)
                {
                    if (entities[otherIndex] != null && 
                        entities[entityIndex] != entities[otherIndex] && 
                        entities[otherIndex].CanCollide &&
                        entities[entityIndex].Collider.Intersects(entities[otherIndex].Collider))
                    {
                        entities[entityIndex].OnCollide(entities[otherIndex]);
                        entities[otherIndex].OnCollide(entities[entityIndex]);
                    }
                }
            }
        }
    }
}
