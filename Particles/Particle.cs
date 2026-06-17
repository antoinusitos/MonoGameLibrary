using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Entities;
using MonoGameLibrary.Managers;

namespace MonoGameLibrary.Particles;

public class Particle : Entity
{
    protected float lifeTime;
    public float LifeTime => lifeTime;

    protected float lifeElapsed;

    public Particle(string name) : base(name)
    {
        isParticle = true;
    }

    public void SetLifeTime(float lifeTime)
    {
        this.lifeTime = lifeTime;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);

        spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)position.X, (int)position.Y, (int)(scale), (int)(scale)), color);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        lifeElapsed += deltaTime;

        if (lifeElapsed >= lifeTime)
        {
            PendingDestroy = true;
            RegisterManager.Instance.UnregisterEntity(this);
        }

        SetPosition(position + Velocity);
    }
}
