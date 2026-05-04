using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;

namespace MonoGameLibrary.Systems;

public class CollisionSystem : GameSytem
{
    public override void Update(GameTime gameTime)
    {
        for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredColliders.Count; entityIndex++)
        {
            if (RegisterManager.Instance.registeredColliders[entityIndex] != null && RegisterManager.Instance.registeredColliders[entityIndex].CanCollide && RegisterManager.Instance.registeredColliders[entityIndex].CollisionType == CollisionType.DYNAMIC)
            {
                // X RESOLUTION
                RegisterManager.Instance.registeredColliders[entityIndex].SetPosition(RegisterManager.Instance.registeredColliders[entityIndex].Position + new Vector2(RegisterManager.Instance.registeredColliders[entityIndex].Velocity.X, 0));
                for (int otherIndex = 0; otherIndex < RegisterManager.Instance.registeredColliders.Count; otherIndex++)
                {
                    if (RegisterManager.Instance.registeredColliders[otherIndex] != null && 
                        RegisterManager.Instance.registeredColliders[entityIndex] != RegisterManager.Instance.registeredColliders[otherIndex] && 
                        RegisterManager.Instance.registeredColliders[otherIndex].CanCollide &&
                        RegisterManager.Instance.registeredColliders[entityIndex].Collider.Intersects(RegisterManager.Instance.registeredColliders[otherIndex].Collider))
                    {
                        //GO LEFT
                        if (RegisterManager.Instance.registeredColliders[entityIndex].Velocity.X > 0)
                        {
                            RegisterManager.Instance.registeredColliders[entityIndex].SetPosition(RegisterManager.Instance.registeredColliders[otherIndex].Collider.Left - RegisterManager.Instance.registeredColliders[entityIndex].Collider.GetWidth(), RegisterManager.Instance.registeredColliders[entityIndex].Position.Y);
                            RegisterManager.Instance.registeredColliders[entityIndex].Velocity.X = 0;
                        }
                        //GO RIGHT
                        else if (RegisterManager.Instance.registeredColliders[entityIndex].Velocity.X < 0)
                        {
                            RegisterManager.Instance.registeredColliders[entityIndex].SetPosition(RegisterManager.Instance.registeredColliders[otherIndex].Collider.Right, RegisterManager.Instance.registeredColliders[entityIndex].Position.Y);
                            RegisterManager.Instance.registeredColliders[entityIndex].Velocity.X = 0;
                        }
                        RegisterManager.Instance.registeredColliders[entityIndex].OnCollide(RegisterManager.Instance.registeredColliders[otherIndex]);
                        RegisterManager.Instance.registeredColliders[otherIndex].OnCollide(RegisterManager.Instance.registeredColliders[entityIndex]);
                    }
                }

                // Y RESOLUTION
                RegisterManager.Instance.registeredColliders[entityIndex].SetPosition(RegisterManager.Instance.registeredColliders[entityIndex].Position + new Vector2(0, RegisterManager.Instance.registeredColliders[entityIndex].Velocity.Y));
                for (int otherIndex = 0; otherIndex < RegisterManager.Instance.registeredColliders.Count; otherIndex++)
                {
                    if (RegisterManager.Instance.registeredColliders[otherIndex] != null &&
                        RegisterManager.Instance.registeredColliders[entityIndex] != RegisterManager.Instance.registeredColliders[otherIndex] &&
                        RegisterManager.Instance.registeredColliders[otherIndex].CanCollide &&
                        RegisterManager.Instance.registeredColliders[entityIndex].Collider.Intersects(RegisterManager.Instance.registeredColliders[otherIndex].Collider))
                    {
                        //GO UP
                        if (RegisterManager.Instance.registeredColliders[entityIndex].Velocity.Y < 0)
                        {
                            RegisterManager.Instance.registeredColliders[entityIndex].SetPosition(RegisterManager.Instance.registeredColliders[entityIndex].Position.X, RegisterManager.Instance.registeredColliders[otherIndex].Collider.Bottom);
                            RegisterManager.Instance.registeredColliders[entityIndex].Velocity.Y = 0;
                        }
                        //GO DOWN
                        else if (RegisterManager.Instance.registeredColliders[entityIndex].Velocity.Y > 0)
                        {
                            RegisterManager.Instance.registeredColliders[entityIndex].SetPosition(RegisterManager.Instance.registeredColliders[entityIndex].Position.X, RegisterManager.Instance.registeredColliders[otherIndex].Collider.Top - RegisterManager.Instance.registeredColliders[entityIndex].Collider.GetHeight());
                            RegisterManager.Instance.registeredColliders[entityIndex].Velocity.Y = 0;
                        }
                        RegisterManager.Instance.registeredColliders[entityIndex].OnCollide(RegisterManager.Instance.registeredColliders[otherIndex]);
                        RegisterManager.Instance.registeredColliders[otherIndex].OnCollide(RegisterManager.Instance.registeredColliders[entityIndex]);
                    }
                }
            }
        }
    }
}
