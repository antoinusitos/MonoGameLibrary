using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;

namespace MonoGameLibrary.Systems;

public class MoveSystem : GameSytem
{
    public override void Update(GameTime gameTime)
    {
        for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredUpdaters.Count; entityIndex++)
        {
            if (RegisterManager.Instance.registeredUpdaters[entityIndex] != null && RegisterManager.Instance.registeredUpdaters[entityIndex].CanMove)
            {
                RegisterManager.Instance.registeredUpdaters[entityIndex].SetPosition(RegisterManager.Instance.registeredUpdaters[entityIndex].Velocity + RegisterManager.Instance.registeredUpdaters[entityIndex].Position);
            }
        }
    }
}
