using MonoGameLibrary.Managers;

namespace MonoGameLibrary.Systems;

public class ParticleSystem : GameSytem
{
    public override void Update(float deltaTime)
    {
        for (int emitterIndex = 0; emitterIndex < RegisterManager.Instance.registeredEmitters.Count; emitterIndex++)
        {
            var emitter = RegisterManager.Instance.registeredEmitters[emitterIndex];
            if (emitter == null)
                continue;

            emitter.Update(deltaTime);
        }

        for (int particleIndex = 0; particleIndex < RegisterManager.Instance.registeredParticles.Count; particleIndex++)
        {
            var particle = RegisterManager.Instance.registeredParticles[particleIndex];
            if (particle == null)
                continue;

            particle.Update(deltaTime);
        }
    }

    public void Render(float deltaTime)
    {
        for (int particleIndex = 0; particleIndex < RegisterManager.Instance.registeredParticles.Count; particleIndex++)
        {
            var particle = RegisterManager.Instance.registeredParticles[particleIndex];
            if (particle == null)
                continue;

            particle.Render(Core.SpriteBatch);
        }
    }
}
