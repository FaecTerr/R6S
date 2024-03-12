using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Prism : Throwable
    {
        public Prism(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Prism.png"), 12, 12, false);
            graphic = _sprite;
            center = new Vec2(4f, 3f);
            collisionOffset = new Vec2(-4f, -3f);
            collisionSize = new Vec2(8f, 6f);

            scannable = true;

            UsageCount = 4;
            index = 30;
            setTime = 0.5f;
            delay = 0.5f;

            weightR = 7;
            DeviceCost = 15;
            descriptionPoints = "Prism activated";

            placeSound = "SFX/Devices/PrismPlaced.wav";
            drawTraectory = true; 
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new PrismAP(position.x, position.y);
            base.SetRocky();
        }
    }

    public class PrismAP : Rocky
    {
        public bool wasGrounded = false;
        public PrismCopy _prism;

        public int actuationFrames;

        public PrismAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Prism.png"), 12, 12, false);
            graphic = _sprite;
            center = new Vec2(6f, 10f);
            collisionOffset = new Vec2(-4f, -1f);
            collisionSize = new Vec2(8f, 2f);

            grounded = false;
            breakable = true;
            bulletproof = false;
            destructingByHands = true;

            thickness = 0;

            bouncy = 0.4f;
            //friction = 1;
        }
        public override void Update()
        {
            base.Update();

            if (!grounded)
            {
                setted = false;
                actuationFrames = 30;
            }
            else
            {
                if (angle < 0.01f)
                {
                    _sprite.frame = 1;

                    if (oper != null && _prism != null)
                    {
                        if (oper.inventory.Count >= 2)
                        {
                            _prism._gun = (oper.inventory[1]._sprite).CloneMap();
                        }
                        //_prism.headlocation = oper.headLocation;
                        _prism._prismHead = oper._head.CloneMap();
                    }
                }
                if (!wasGrounded)
                {
                    wasGrounded = true;

                    Level.Add(new SoundSource(position.x, position.y, 240, "SFX/Devices/PrismDeploying.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/Devices/PrismDeploying.wav", "J"));

                    if(_prism == null)
                    {
                        _prism = new PrismCopy(position.x, position.y) { main = this };
                        Level.Add(_prism);
                    }
                    else
                    {

                    }
                }

                if(_prism != null)
                {
                    _prism.position = position + new Vec2(0, -11f);
                    _prism.offDir = (sbyte)(offDir * -1);                    
                }

                angle *= 0.8f;

                setted = true;
                hSpeed *= 0.8f;
                vSpeed *= 0.1f;
            } 
        }

        public override void OnPickUp()
        {
            if(_prism != null)
            {
                Level.Remove(_prism);
            }
            base.OnPickUp();
        }

        public override void Break()
        {
            if (_prism != null)
            {
                Level.Remove(_prism);
            }
            base.Break();
        }        
    }

    public class PrismCopy : Device
    {
        public PrismAP main;
        public SinWave _pulse = 0.02f;
        public SinWave _pulse2 = 0.0245f; 

        public float dir = Rando.Float(-0.6f, 0.6f);

        public bool disabled;
        public string headlocation = "Sprites/Operators/alibiHead.png";
        public string gunlocation = "Sprites/Guns/G36.png";
        public SpriteMap _gun = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/G36.png"), 32 ,32);
        public SpriteMap _prismHead = new SpriteMap(Mod.GetPath<R6S>("Sprites/Operators/AlibiHead.png"), 32, 32);

        public PrismCopy(float xval, float yval) : base(xval, yval)
        {
            collisionSize = new Vec2(8, 22);
            collisionOffset = new Vec2(-4, -11);
            setted = false;
            breakable = false;
            thickness = 0;
            health = 9999;
        }

        public override void HittedFrom(Operators operators)
        {
            if(operators != null && main != null)
            {
                if(operators.team != main.team && operators.undetectable <= 0)
                {
                    if (!operators.HasEffect("PingedByAlibi"))
                    {
                        operators.effects.Add(new PingedByAlibi());
                        if (main.oper != null)
                        {
                            if (main.oper.local)
                            {
                                PlayerStats.renown += 15;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Enemy tricked", amount = 15 });
                            }
                        }
                    }
                    else
                    {
                        operators.GetEffect("PingedByAlibi").charge = 5;
                    }
                }

                main.actuationFrames = 30;
            }
            base.HittedFrom(operators);

            Level.Add(new SoundSource(position.x, position.y, 120, "SFX/Devices/PrismTriggered.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 120, "SFX/Devices/PrismTriggered.wav", "J"));
        }

        public override void Update()
        {
            base.Update();
            if (main != null)
            {
                if (Level.CheckRect<Operators>(topLeft, bottomRight) != null && !disabled)
                {
                    Operators operators = Level.CheckRect<Operators>(topLeft, bottomRight);
                    main.actuationFrames = 30;
                    disabled = true;

                    Level.Add(new SoundSource(position.x, position.y, 120, "SFX/Devices/PrismTriggered.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 120, "SFX/Devices/PrismTriggered.wav", "J"));

                    if (operators.operatorID % 2 == 1)
                    {
                        if (!operators.HasEffect("PingedByAlibi"))
                        {
                            operators.effects.Add(new PingedByAlibi());

                            if (main.oper != null)
                            {
                                if (main.oper.local)
                                {
                                    PlayerStats.renown += 15;
                                    PlayerStats.Save();
                                    Level.Add(new RenownGained() { description = "Enemy tricked", amount = 15 });
                                }
                            }
                        }
                        else
                        {
                            operators.GetEffect("PingedByAlibi").charge = 5;
                        }
                    }
                }
                if(Level.CheckRect<Operators>(topLeft, bottomRight) == null && disabled)
                {
                    main.actuationFrames = 30;
                    disabled = false;

                    Level.Add(new SoundSource(position.x, position.y, 120, "SFX/Devices/PrismTriggered.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 120, "SFX/Devices/PrismTriggered.wav", "J"));
                }
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            base.OnDrawLayer(layer);
            if (layer == Layer.Game && main != null)
            {
                if (main.grounded && main.angle < 0.01f)
                {
                    float move = _pulse * 0.1f + _pulse2 * 0.12f;

                    if(move > 0.1f)
                    {
                        move = 0.1f;
                    }
                    if(move < -0.1f)
                    {
                        move = -0.1f;
                    }

                    //SpriteMap _prismHead = new SpriteMap(Mod.GetPath<R6S>(headlocation), 32, 32, false);
                    _prismHead.alpha = alpha;
                    _prismHead.CenterOrigin();
                    _prismHead.angle = dir + move;

                    SpriteMap _prismBody = new SpriteMap(GetPath("Sprites/Operators/gis.png"), 32, 32, false);
                    _prismBody.alpha = alpha;
                    _prismBody.CenterOrigin();

                    //SpriteMap _gun = new SpriteMap(Mod.GetPath<R6S>(gunlocation), 32, 32);
                    _gun.alpha = alpha;
                    _gun.CenterOrigin();
                    _gun.angle = _prismHead.angle * 2;

                    SpriteMap _hand = new SpriteMap(GetPath("Sprites/Operators/AlibiHand.png"), 8, 8, false);
                    _hand.alpha = alpha;
                    _hand.CenterOrigin();
                    _hand.angle = _prismHead.angle * 2;

                    //_gun.position = new Vec2(position.x, position.y);

                    _prismHead.flipH = offDir == 1;
                    _prismBody.flipH = offDir == 1;
                    _gun.flipH = offDir == 1;
                    _hand.flipH = offDir == 1;

                    Material material = Graphics.material;

                    if (main.actuationFrames > 0)
                    {
                        
                        Graphics.material = new MaterialGlitch(this) { amount = Rando.Float(2, 4), yoffset = Rando.Float(-1, 1)};

                        main.actuationFrames--;
                        
                        _prismHead.alpha *= 0.4f;
                        _prismBody.alpha *= 0.4f;
                        _gun.alpha *= 0.4f;
                        _hand.alpha *= 0.4f;
                    }

                    if (!disabled || main.actuationFrames > 0)
                    {
                        Graphics.Draw(_gun, position.x + 3 * offDir, position.y + 4, 0.98f);
                        Graphics.Draw(_prismHead, position.x, position.y - 6, 0.99f);
                        Graphics.Draw(_prismBody, position.x, position.y - 3, 0.97f);
                        Graphics.Draw(_hand, position.x + 2 * offDir, position.y + 6, 0.985f);
                    }

                    Graphics.material = material;
                }
            }
        }
    }
}
