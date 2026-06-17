using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using System;

namespace MonoGameLibrary.Particles;

public class ParticleEmitter
{
    protected Vector2 position;
    public Vector2 Position => position;

    protected float scale = 1f;
    public float Scale => scale;

    protected float spawnRate;
    public float SpawnRate => spawnRate;

    protected float spawnDelay;

    protected Vector2 velocity;
    public Vector2 Velocity => velocity;

    protected Vector2 positionOffsetMin;
    public Vector2 PositionOffsetMin => positionOffsetMin;

    protected Vector2 positionOffsetMax;
    public Vector2 PositionOffsetMax => positionOffsetMax;

    protected float lifeTime;
    public float LifeTime => lifeTime;

    protected bool active = true;
    public bool Active => active;


    public ParticleEmitter()
    {
        RegisterManager.Instance.RegisterEmitter(this);
    }

    public virtual void SetPosition(Vector2 position)
    {
        this.position = position;
    }

    public virtual void SetOffsetMin(Vector2 offsetMin)
    {
        positionOffsetMin = offsetMin;
    }

    public virtual void SetOffsetMax(Vector2 offsetMin)
    {
        positionOffsetMax = offsetMin;
    }

    public virtual void SetPosition(float x, float y)
    {
        position = new Vector2(x, y);
    }

    public virtual void SetScale(float scale)
    {
        this.scale = scale;
    }

    public virtual void SetSpawnRate(float rate)
    {
        spawnRate = rate;
    }

    public virtual void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    public virtual void SetLifeTime(float time)
    {
        lifeTime = time;
    }

    public void Register()
    {
        RegisterManager.Instance.RegisterEmitter(this);
    }

    public void Update(float deltaTime)
    {
        if (spawnDelay > spawnRate)
        {
            spawnDelay = 0;

            Particle particle = new Particle("particle");
            particle.LoadContent(Core.Content);
            particle.Initialize();
            Vector2 offset = Vector2.Zero;
            Random random = new Random();
            offset.X = MathHelper.Lerp(positionOffsetMin.X, positionOffsetMax.X, (float)random.NextDouble());
            offset.Y = MathHelper.Lerp(positionOffsetMin.Y, positionOffsetMax.Y, (float)random.NextDouble());
            particle.SetPosition(position + offset);
            particle.SetScale(scale);
            particle.SetColor(Color.Red);
            particle.Velocity = velocity;
            particle.SetLifeTime(lifeTime);
            particle.Register();
        }
        else
        {
            spawnDelay += deltaTime;
        }
    }
}
