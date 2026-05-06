using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;

namespace MonoGameLibrary.Particles;

public class Particle : Entity
{
    protected float _lifeTime;
    public float LifeTime => _lifeTime;

    protected float _lifeElapsed;

    public Particle(string name) : base(name)
    {
        _isParticle = true;
    }

    public void SetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);

        spriteBatch.Draw(Debug.DebugTexture, new Rectangle((int)_position.X, (int)_position.Y, (int)(_scale), (int)(_scale)), _color);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _lifeElapsed += deltaTime;

        if (_lifeElapsed >= _lifeTime)
        {
            PendingDestroy = true;
            RegisterManager.Instance.UnregisterEntity(this);
        }

        SetPosition(_position + Velocity);
    }
}
