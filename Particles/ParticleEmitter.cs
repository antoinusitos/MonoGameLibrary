using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using System;

namespace MonoGameLibrary.Particles;

public class ParticleEmitter
{
    protected Vector2 _position;
    public Vector2 Position => _position;

    protected float _scale = 1f;
    public float Scale => _scale;

    protected float _spawnRate;
    public float SpawnRate => _spawnRate;

    protected float _spawnDelay;

    protected Vector2 _velocity;
    public Vector2 Velocity => _velocity;

    protected Vector2 _positionOffsetMin;
    public Vector2 PositionOffsetMin => _positionOffsetMin;

    protected Vector2 _positionOffsetMax;
    public Vector2 PositionOffsetMax => _positionOffsetMax;

    protected float _lifeTime;
    public float LifeTime => _lifeTime;


    public ParticleEmitter()
    {
        RegisterManager.Instance.RegisterEmitter(this);
    }

    public virtual void SetPosition(Vector2 position)
    {
        _position = position;
    }

    public virtual void SetOffsetMin(Vector2 offsetMin)
    {
        _positionOffsetMin = offsetMin;
    }

    public virtual void SetOffsetMax(Vector2 offsetMin)
    {
        _positionOffsetMax = offsetMin;
    }

    public virtual void SetPosition(float x, float y)
    {
        _position = new Vector2(x, y);
    }

    public virtual void SetScale(float scale)
    {
        _scale = scale;
    }

    public virtual void SetSpawnRate(float rate)
    {
        _spawnRate = rate;
    }

    public virtual void SetVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }

    public virtual void SetLifeTime(float time)
    {
        _lifeTime = time;
    }

    public void Register()
    {
        RegisterManager.Instance.RegisterEmitter(this);
    }

    public void Update(float deltaTime)
    {
        if (_spawnDelay > _spawnRate)
        {
            _spawnDelay = 0;

            Particle particle = new Particle("particle");
            particle.LoadContent(Core.Content);
            particle.Initialize();
            Vector2 offset = Vector2.Zero;
            Random random = new Random();
            offset.X = MathHelper.Lerp(_positionOffsetMin.X, _positionOffsetMax.X, (float)random.NextDouble());
            offset.Y = MathHelper.Lerp(_positionOffsetMin.Y, _positionOffsetMax.Y, (float)random.NextDouble());
            particle.SetPosition(_position + offset);
            particle.SetScale(_scale);
            particle.SetColor(Color.Red);
            particle.Velocity = _velocity;
            particle.SetLifeTime(_lifeTime);
            particle.Register();
        }
        else
        {
            _spawnDelay += deltaTime;
        }
    }
}
