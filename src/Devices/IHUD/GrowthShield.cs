using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class HandShield : BallisticShield
    {
        public HandShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/HandShield.png"), 32, 32, false);
            graphic = _sprite;
            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(12f, 29f);
            collisionOffset = new Vec2(-6f, -14.5f);

            _holdOffset = new Vec2(4.5f, -2f);

            reqFrame = 7;

            shieldKnife = new GrowthShieldKnife(position.x, position.y);
        }
        public virtual void UpdateState()
        {
            if (user != null)
            {
                if (_sprite.frame == 0)
                {
                    collisionSize = new Vec2(12f, 29f);
                    collisionOffset = new Vec2(-6f, -14.5f);
                }
                if (_sprite.frame == 1 || _sprite.frame == 2)
                {
                    collisionSize = new Vec2(12f, 27f);
                    collisionOffset = new Vec2(-6f, -13.5f);
                    user.hSpeed *= 0.1f;
                }
                if (_sprite.frame == 3 || _sprite.frame == 4)
                {
                    collisionSize = new Vec2(12f, 25f);
                    collisionOffset = new Vec2(-6f, -12.5f);
                    user.hSpeed *= 0.1f;
                }
                if (_sprite.frame == 5)
                {
                    collisionSize = new Vec2(12f, 23f);
                    collisionOffset = new Vec2(-6f, -11.5f);
                    user.hSpeed *= 0.1f;
                }
                if (_sprite.frame == 6)
                {
                    collisionSize = new Vec2(12f, 21f);
                    collisionOffset = new Vec2(-6f, -10.5f);
                    user.hSpeed *= 0.1f;
                }
                if (_sprite.frame == 7)
                {
                    collisionSize = new Vec2(12f, 19f);
                    collisionOffset = new Vec2(-6f, -9.5f);
                }
            }
        }
        public override void Update()
        {
            base.Update();
            UpdateState();
            if (user != null)
            {
                if (user.holdObject == this)
                {
                    if (user.mode == "slide")
                    {
                        opened = false;
                        user.ChangeWeapon(10, 2);
                    }

                    if (opened)
                    {
                        if (user.concussionFrames > 0)
                        {
                            opened = false;
                        }
                        oneHand = false;

                        if (user.holdObject == this)
                        {
                            user.lockedTakeOut[0] = 6;
                            user.lockedTakeOut[1] = 6;
                            user.lockedTakeOut[2] = 6;
                            user.lockedTakeOut[4] = 6;
                            user.lockedTakeOut[5] = 6;
                            user.lockedTakeOut[6] = 6;
                            user.lockedTakeOut[7] = 6;

                            user.hSpeed *= 0.9f;
                            user.holdObject.handAngle = 0;
                            user.holdAngle = 0;
                            angle = 0;
                            user.unableToSprint = 6;
                            user.unableToCrouch = 6;
                        }

                        foreach (Operators d in Level.CheckRectAll(topLeft, bottomRight, new List<Operators>() { user }))
                        {
                            if (d != null && d != user)
                            {
                                d.hSpeed = 1f * offDir;
                            }
                        }

                        foreach (PhysicsObject po in Level.CheckRectAll<PhysicsObject>(topLeft, bottomRight))
                        {
                            if (po != null)
                            {
                                if (po.enablePhysics == true && po != oper)
                                {
                                    if ((po.hSpeed > offDir / 5f && offDir < 0) || (po.hSpeed < offDir / 5f && offDir > 0))
                                    {
                                        po.hSpeed = 0f;
                                    }
                                }
                            }
                        }
                    }
                }

                if (opened)
                {
                    if (_sprite.frame > 0)
                    {
                        _sprite.frame--;
                    }
                    if (_sprite.frame > 4)
                    {
                        thickness = 4f;
                    }
                    oneHand = false;
                }
                else
                {
                    if (_sprite.frame < 7)
                    {
                        _sprite.frame++;
                    }
                    thickness = 4f;
                    oneHand = true;
                }

                if (user.runFrames == 2)
                {
                    Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/WalkWithShield.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/WalkWithShield.wav", "J"));
                }

                if (offDir > 0)
                {
                    scale = new Vec2(1, 1);
                }
                else
                {
                    scale = new Vec2(1, 1);
                }
            }
        }
    }
}

