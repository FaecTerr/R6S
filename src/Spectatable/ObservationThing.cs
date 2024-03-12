using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class ObservationThing : Rocky
    {
        public float Unjam;
        public bool observing;
        //public SpriteMap _jam;
        public bool broken;
        public bool controllable;
        public bool lightExpose;

        public bool moveLeft;
        public bool moveRight;
        public bool jump;
        public bool fire;
        public float _maxSpeed = 4f;

        public int cameraOverlay;

        public bool doLight = false;
        public float sizeOfLight = 1;
        public Color lightColor = Color.Gray;

        public bool observableOutside = true;
        public int outsideFrames;
        public int framesBeforeReconnect;

        //public PointLight _light;
        public SinWave _pulse = 0.6f;

        //public PointLight light;
        //public Vec2 lightPos;
        SpriteMap _l = new SpriteMap(Mod.GetPath<R6S>("Sprites/PointLight.png"), 48, 48, false);
        SpriteMap _scanner = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JammedLocationRect.png"), 22, 22, false);

        public ObservationThing(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Cameras/OBSCamera.png"), 12, 12);
            graphic = _sprite;
            _sprite.CenterOrigin();
            _enablePhysics = false;
            thickness = 0.4f;
            isGrenade = false;

            UpdatePhones();
            Set();
        }

        public override void DetonateFull()
        {

        }

        public override void Update()
        {
            /*if (_light != null)
            {
                if (broken || jammed)
                {
                    Level.Remove(_light);
                }
            }*/
            base.Update();
            //Restoring after getting outside camera back in
            if (framesBeforeReconnect > 0)
            {
                framesBeforeReconnect--;
                outsideFrames++;
            }
            if (framesBeforeReconnect <= 0)
            {
                outsideFrames = 0;
            }
            //Losing signal after getting camera outside
            if (outsideFrames > 0 && framesBeforeReconnect <= 0)
            {
                outsideFrames--;
            }

            //If camera stable
            if (outsideFrames > 180)
            {
                jammed = true;
                jammedFrames = 60;
            }

            //Moving light from camera
            /*if(!observing || (observing && light != null))
            {
                Level.Remove(light);
            }
            if (observing)
            {
                foreach (Operators op in Level.current.things[typeof(Operators)])
                {
                    if (op.local && op.team == team && !jammed)
                    {
                        lightPos = position;
                        light = new PointLight(lightPos.x, lightPos.y, lightColor, 280);
                        Level.Add(light);
                    }
                }
            }*/
        }

        //Overriding set for cameras
        public override void Set()
        {
            base.Set();

            if(oper != null) //Making so after placing this camera, players phone will automatically select it as last active
            {
                /*oper.GetPhone().camIndex = oper.GetPhone().ConnectedCameras() - 1;
                if(oper.GetPhone().camIndex < 0)
                {
                    oper.GetPhone().camIndex = 0;
                }*/
                oper.GetPhone().currentCategory = oper.GetPhone().categories.FindIndex(x => x == editorName);
            }
        }

        //Whenever player select this cam
        public virtual void Connect()
        {
            observing = true;
        }
        //Whenever player deselect this cam
        public virtual void Disconnect()
        {
            observing = false;
        }

        //overriding classic break for camera-devices
        public override void Break()
        {
            _sprite.frame = 1;
            broken = true;
            UpdatePhones();
            controllable = false;
            canPick = false;

            /*if (light != null)
            {
                Level.Remove(light);
            }*/

            base.Break();
        }

        public override void OnPickUp()
        {
            UpdatePhones();
            /*if (light != null) //Removing related light
            {
                Level.Remove(light);
            }*/
            base.OnPickUp();
        }

        //Calls to phones for an update of available cameras
        public virtual void UpdatePhones()
        {
            if (Level.current is GameLevel)
            {
                foreach (Phone p in Level.current.things[typeof(Phone)])
                {
                    p.GetAvailibleCameras();
                }
            }
        }    

        public override void Draw()
        {
            base.Draw();
            if (jammed && observing)
            {
                //Graphics.Draw(_jam, Level.current.camera.x, Level.current.camera.y, 1f);
            }
        }
        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (oper != null)
                {
                    if (observing)
                    {
                        lightExpose = true;
                        
                        //jamming effect if this is outside when it's not supposed to
                        if (oper.local && outsideFrames > 0)
                        {
                            Vec2 Pos = Level.current.camera.position;
                            _scanner.scale = Level.current.camera.size / new Vec2(22, 22);
                            _scanner.CenterOrigin();
                            _scanner.scale *= new Vec2(0.25f, 0.25f);
                            _scanner.frame = Rando.Int(2);
                            _scanner.alpha = 0.4f;
                            Graphics.Draw(_scanner, Pos.x + Level.current.camera.width / 2, Pos.y + Level.current.camera.height / 2);
                        }
                    }
                }
            }
            if(layer == Layer.Game)
            {
                //Show light flashing
                if (observing)
                {
                    if (doLight)
                    {
                        _l.CenterOrigin();

                        _l.color = lightColor;
                        _l.alpha = 0.3f + _pulse * 0.1f;
                        _l.alpha *= alpha;
                        _l.scale = new Vec2(0.4f - _pulse * 0.1f, 0.4f - _pulse * 0.1f) * sizeOfLight;
                        Graphics.Draw(_l, position.x, position.y, 0.15f);

                        _l.color = (lightColor.ToVector3() - new Vec3(55, 55, 55)).ToColor();
                        _l.alpha = 0.2f - _pulse * 0.1f;
                        _l.alpha *= alpha;
                        _l.scale = new Vec2(0.6f - _pulse * 0.1f, 0.6f - _pulse * 0.1f) * sizeOfLight;
                        Graphics.Draw(_l, position.x, position.y, 0.1f);
                    }
                }
            }
        }
    }
}
