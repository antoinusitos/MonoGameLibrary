using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;

namespace MonoGameLibrary.Systems;

public class MoveSystem : GameSytem
{
    public override void Update(float deltaTime)
    {
        for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredUpdaters.Count; entityIndex++)
        {
            var entity = RegisterManager.Instance.registeredUpdaters[entityIndex];
            if (entity == null || !entity.CanMove)
                continue;

            // Skip DYNAMIC colliders: CollisionSystem already applied their velocity
            if (entity.CanCollide && entity.CollisionType == CollisionType.DYNAMIC)
                continue;

            entity.SetPosition(entity.Velocity + entity.Position);
        }
    }
}
