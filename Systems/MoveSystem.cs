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
            if (entity == null || !entity.CanMove || !entity.Active)
                continue;

            if (entity.UseGravity)
            {
                entity.Velocity += Vector2.UnitY * GameManager.Gravity * entity.Mass * deltaTime;
            }

            // Skip DYNAMIC colliders: CollisionSystem already applied their velocity
            if (entity.CanCollide && entity.CollisionType == CollisionType.DYNAMIC)
                continue;

            if (entity.Velocity != Vector2.Zero)
            {
                SceneManager.Instance.SetIsDirty(true);
            }

            entity.SetPosition(entity.Velocity + entity.Position);
        }
    }
}
