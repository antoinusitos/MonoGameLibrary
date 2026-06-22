using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;

namespace MonoGameLibrary.Systems;

public class RenderSystem : GameSytem
{
    public bool defaultBatch = true;
    
    public override void Update(float deltaTime)
    {
        if (defaultBatch)
        {
            for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredRenderers.Count; entityIndex++)
            {
                if (RegisterManager.Instance.registeredRenderers[entityIndex] != null &&
                    RegisterManager.Instance.registeredRenderers[entityIndex].CanRender &&
                    RegisterManager.Instance.registeredRenderers[entityIndex].Active)
                {
                    RegisterManager.Instance.registeredRenderers[entityIndex].Render(Core.SpriteBatch);
                }
            }
        }
        else
        {
            for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredRenderers.Count; entityIndex++)
            {
                if (RegisterManager.Instance.registeredRenderers[entityIndex] != null &&
                    RegisterManager.Instance.registeredRenderers[entityIndex].CanRender &&
                    RegisterManager.Instance.registeredRenderers[entityIndex].Active)
                {
                    RegisterManager.Instance.registeredRenderers[entityIndex].Render();
                }
            }
        }

    }
}
