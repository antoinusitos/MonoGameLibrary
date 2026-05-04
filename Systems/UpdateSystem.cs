using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;

namespace MonoGameLibrary.Systems;

public class UpdateSystem : GameSytem
{
    public override void Update(float deltaTime)
    {
        for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredUpdaters.Count; entityIndex++)
        {
            if (RegisterManager.Instance.registeredUpdaters[entityIndex] != null && RegisterManager.Instance.registeredUpdaters[entityIndex].CanUpdate)
            {
                RegisterManager.Instance.registeredUpdaters[entityIndex].Update(deltaTime);
            }
        }
    }
}
