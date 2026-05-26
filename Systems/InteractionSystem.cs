using MonoGameLibrary.Entities;
using MonoGameLibrary.Managers;

namespace MonoGameLibrary.Systems;

public class InteractionSystem : GameSytem
{
    public override void Update(float deltaTime)
    {
        for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredUpdaters.Count; entityIndex++)
        {
            var entity = RegisterManager.Instance.registeredUpdaters[entityIndex];
            if (entity == null || !entity.Active)
            {
                continue;
            }
            if (entity.GetType().IsSubclassOf(typeof(Character)))
            {
                Character character = (Character)entity;
                if (character == null || !character.CanInteract || !character.WantToInteract)
                    continue;

                character.InteractWithTarget();
                entity.SetWantToInteract(false);
            }
        }
    }
}
