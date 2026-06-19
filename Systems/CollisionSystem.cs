using Microsoft.Xna.Framework;
using MonoGameLibrary.Entities;
using MonoGameLibrary.Interfaces;
using MonoGameLibrary.Managers;
using MonoGameLibrary.Misc;
using System.Collections.Generic;

namespace MonoGameLibrary.Systems;

public class CollisionSystem : GameSytem
{
    public override void Update(float deltaTime)
    {
        List<Trigger> triggers = new List<Trigger>();

        for (int entityIndex = 0; entityIndex < RegisterManager.Instance.registeredColliders.Count; entityIndex++)
        {
            if (RegisterManager.Instance.registeredColliders[entityIndex] != null && RegisterManager.Instance.registeredColliders[entityIndex].CanCollide && RegisterManager.Instance.registeredColliders[entityIndex].CollisionType == CollisionType.DYNAMIC && RegisterManager.Instance.registeredColliders[entityIndex].Active)
            {
                var self = RegisterManager.Instance.registeredColliders[entityIndex];
                if (self.IsTrigger)
                {
                    if (!triggers.Contains((Trigger)self))
                    {
                        triggers.Add((Trigger)self);
                    }
                }
                bool collidedThisFrame = false;
                Entity collidedWith = null;

                // X RESOLUTION
                self.SetPosition(self.Position + new Vector2(self.Velocity.X, 0) * deltaTime);

                if (self.Velocity.X !=  0)
                {
                    SceneManager.Instance.SetIsDirty(true);
                }
                for (int otherIndex = 0; otherIndex < RegisterManager.Instance.registeredColliders.Count; otherIndex++)
                {
                    var other = RegisterManager.Instance.registeredColliders[otherIndex];
                    if (other != null && self != other && other.CanCollide && self.Collider.Intersects(other.Collider) && other.Active && !self.IgnoreCollisions.Contains(other))
                    {
                        if (!other.IsTrigger && !self.IsTrigger)
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
                        }
                        else
                        {
                            if (other.IsTrigger)
                            {
                                if (!triggers.Contains((Trigger)other))
                                {
                                    triggers.Add((Trigger)other);
                                }

                                if (self.IsTrigger)
                                {
                                    if (other.Parent != self.Parent && !((Trigger)other).entitiesThisFrame.Contains(other)/*  && self.Parent is IInteractable*/)
                                    {
                                        ((Trigger)self).entitiesThisFrame.Add(other.Parent);
                                    }
                                }
                                else
                                {
                                    if (self != other.Parent && !((Trigger)other).entitiesThisFrame.Contains(self)/*  && self is IInteractable*/)
                                    {
                                        ((Trigger)other).entitiesThisFrame.Add(self);
                                    }
                                }
                            }
                            if (self.IsTrigger)
                            {
                                if (other.IsTrigger)
                                {
                                    if (other.Parent != self.Parent && !((Trigger)self).entitiesThisFrame.Contains(other)/*  && other.Parent is IInteractable*/)
                                    {
                                        ((Trigger)self).entitiesThisFrame.Add(other.Parent);
                                    }
                                }
                                else
                                {
                                    if (other != self.Parent && !((Trigger)self).entitiesThisFrame.Contains(other)/* && other is IInteractable*/)
                                    {
                                        ((Trigger)self).entitiesThisFrame.Add(other);
                                    }
                                }
                            }
                        }
                        collidedThisFrame = true;
                        collidedWith = other;
                    }
                }

                // Y RESOLUTION
                self.SetPosition(self.Position + new Vector2(0, self.Velocity.Y) * deltaTime);
                if (self.Velocity.Y != 0)
                {
                    SceneManager.Instance.SetIsDirty(true);
                }
                for (int otherIndex = 0; otherIndex < RegisterManager.Instance.registeredColliders.Count; otherIndex++)
                {
                    var other = RegisterManager.Instance.registeredColliders[otherIndex];
                    if (other != null && self != other && other.CanCollide && self.Collider.Intersects(other.Collider) && other.Active && !self.IgnoreCollisions.Contains(other))
                    {
                        if (!other.IsTrigger && !self.IsTrigger)
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
                        }
                        else
                        {
                            if (other.IsTrigger)
                            {
                                if (!triggers.Contains((Trigger)other))
                                {
                                    triggers.Add((Trigger)other);
                                }

                                if (self.IsTrigger)
                                {
                                    if (other.Parent != self.Parent && !((Trigger)other).entitiesThisFrame.Contains(other) && self.Parent is IInteractable)
                                    {
                                        ((Trigger)self).entitiesThisFrame.Add(other.Parent);
                                    }
                                }
                                else
                                {
                                    if (self != other.Parent && !((Trigger)other).entitiesThisFrame.Contains(self) && self is IInteractable)
                                    {
                                        ((Trigger)other).entitiesThisFrame.Add(self);
                                    }
                                }
                            }
                            if (self.IsTrigger)
                            {
                                if (other.IsTrigger)
                                {
                                    if (other.Parent != self.Parent && !((Trigger)self).entitiesThisFrame.Contains(other) && other.Parent is IInteractable)
                                    {
                                        ((Trigger)self).entitiesThisFrame.Add(other.Parent);
                                    }
                                }
                                else
                                {
                                    if (other != self.Parent && !((Trigger)self).entitiesThisFrame.Contains(other) && other is IInteractable)
                                    {
                                        ((Trigger)self).entitiesThisFrame.Add(other);
                                    }
                                }
                            }
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

        for (int triggerIndex = 0; triggerIndex < triggers.Count; triggerIndex++)
        {
            triggers[triggerIndex].UpdateTrigger();
        }
    }
}
