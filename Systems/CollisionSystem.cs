using Microsoft.Xna.Framework;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;

namespace MonoGameLibrary.Systems;

public class CollisionSystem : GameSytem
{
    public override void Update(float deltaTime)
    {
        for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredColliders.Count; entityIndex++)
        {
            if (RegisterManager.Instance.registeredColliders[entityIndex] != null && RegisterManager.Instance.registeredColliders[entityIndex].CanCollide && RegisterManager.Instance.registeredColliders[entityIndex].CollisionType == CollisionType.DYNAMIC)
            {
                var self = RegisterManager.Instance.registeredColliders[entityIndex];
                bool collidedThisFrame = false;
                Entity collidedWith = null;

                // X RESOLUTION
                self.SetPosition(self.Position + new Vector2(self.Velocity.X, 0));
                for (int otherIndex = 0; otherIndex < RegisterManager.Instance.registeredColliders.Count; otherIndex++)
                {
                    var other = RegisterManager.Instance.registeredColliders[otherIndex];
                    if (other != null && self != other && other.CanCollide && self.Collider.Intersects(other.Collider))
                    {
                        //GO RIGHT
                        if (self.Velocity.X > 0)
                        {
                            self.SetPosition(other.Collider.Left - self.Collider.GetWidth(), self.Position.Y);
                            self.Velocity.X = 0;
                        }
                        //GO LEFT
                        else if (self.Velocity.X < 0)
                        {
                            self.SetPosition(other.Collider.Right, self.Position.Y);
                            self.Velocity.X = 0;
                        }
                        collidedThisFrame = true;
                        collidedWith = other;
                    }
                }

                // Y RESOLUTION
                self.SetPosition(self.Position + new Vector2(0, self.Velocity.Y));
                for (int otherIndex = 0; otherIndex < RegisterManager.Instance.registeredColliders.Count; otherIndex++)
                {
                    var other = RegisterManager.Instance.registeredColliders[otherIndex];
                    if (other != null && self != other && other.CanCollide && self.Collider.Intersects(other.Collider))
                    {
                        //GO UP
                        if (self.Velocity.Y < 0)
                        {
                            self.SetPosition(self.Position.X, other.Collider.Bottom);
                            self.Velocity.Y = 0;
                        }
                        //GO DOWN
                        else if (self.Velocity.Y > 0)
                        {
                            self.SetPosition(self.Position.X, other.Collider.Top - self.Collider.GetHeight());
                            self.Velocity.Y = 0;
                        }
                        collidedThisFrame = true;
                        collidedWith = other;
                    }
                }

                // Fire OnCollide once per frame, after both axes are resolved
                if (collidedThisFrame && collidedWith != null)
                {
                    self.OnCollide(collidedWith);
                    collidedWith.OnCollide(self);
                }
            }
        }
    }
}
