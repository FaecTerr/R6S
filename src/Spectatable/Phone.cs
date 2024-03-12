using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Furniture")]
    public class Phone : Device, IDrawToDifferentLayers
    {
        private List<ObservationThing> cameras = new List<ObservationThing>();
        private List<ObservationThing> removeCameras = new List<ObservationThing>();
        private List<ObservationThing> addCameras = new List<ObservationThing>();

        public int camIndex;
        public bool observing;
        public int availible;

        public List<string> categories = new List<string>();
        public int currentCategory;

        public int unable;
        public bool hacked;

        public bool called;
        public bool locked;

        public RenderTarget2D _captureTarget = null;
        public SinWave _pulse;

        public int photoDelay;

        public int phoneType = 0;
        public Vec2 monitorSize = new Vec2(0.25f, 0.25f);
        public Vec2 phoneSize = new Vec2(1f, 1f);

        public int lagFrames;

        SpriteMap _phone = new SpriteMap(Mod.GetPath<R6S>("Sprites/Phone.png"), 96, 59, false);
        SpriteMap _phoneScreen = new SpriteMap(Mod.GetPath<R6S>("Sprites/PhoneScreen.png"), 96, 59, false); 

        SpriteMap _gui = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/ObservationGUI.png"), 160, 50);
        SpriteMap _marker = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/ObservationMarker.png"), 24, 24);
        SpriteMap _category = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/OBSCameraMark.png"), 24, 24);

        SpriteMap _widebutton = new SpriteMap(Mod.GetPath<R6S>("Sprites/Keys.png"), 34, 17);
        SpriteMap _button = new SpriteMap(Mod.GetPath<R6S>("Sprites/Keys.png"), 17, 17);
        SpriteMap _phoneScreen1 = new SpriteMap(Mod.GetPath<R6S>("Sprites/PhoneScreen.png"), 96, 59, false);

        SpriteMap _jamm = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JammedLocationRect.png"), 22, 22, false);
        Sprite _call = new Sprite(Mod.GetPath<R6S>("Sprites/DokkaebiCall.png"));

        SpriteMap _guiFilter = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/GUIFilters.png"), 64, 36, false);
        public Phone(float xpos, float ypos) : base(xpos, ypos) 
        {
            team = "Def";
            center = new Vec2(6f, 6f);
            collisionSize = new Vec2(12f, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            _sprite = new SpriteMap(GetPath("Sprites/Cameras/Phone.png"), 12, 12);
            graphic = _sprite;
            UIHD = true;
            placeable = false;

            //layer = Layer.Foreground;
        }

        public override void Initialize()
        {
            _button.CenterOrigin();
            _widebutton.CenterOrigin();
            _phone.CenterOrigin();
            _phoneScreen.CenterOrigin();
            _phoneScreen1.CenterOrigin();
            _jamm.CenterOrigin();
            _call.CenterOrigin();
            _phoneScreen1.frame = phoneType;
            _phone.frame = phoneType;
            _phoneScreen.frame = phoneType;
            base.Initialize();
        }

        public virtual void GetAvailibleCameras(/*ObservationThing cam = null*/)
        {
            availible = 0;
            int count = 0;
            categories.Clear();
            foreach(ObservationThing obs in Level.current.things[typeof(ObservationThing)])
            {
                count += 1;
                if (/*obs != cam &&*/ ((obs.team == team && !hacked) || hacked) && obs.connectable)
                {
                    addCameras.Add(obs);
                    
                    if (categories.Count > 0)
                    {
                        bool same = false;

                        foreach (string name in categories)
                        {
                            if (obs.editorName == name)
                            {
                                same = true;
                            }
                        }

                        if (!same)
                        {
                            categories.Add(obs.editorName);
                            //DevConsole.Log(obs.editorName + "not same");
                        }
                       
                    }
                    else
                    {
                        categories.Add(obs.editorName);
                        //DevConsole.Log(obs.editorName);
                    }

                    /*if (!obs.broken)
                    {
                        availible += 1;
                    }*/
                }
                if(cameras.Contains(obs) && obs.broken)
                {
                    removeCameras.Add(obs);
                }
            }
            if(camIndex >= count)
            {
                camIndex = count - 1;
            }
            if(camIndex < 0)
            {
                camIndex = 0;
            }
            //DevConsole.Log(Convert.ToString(count));
            //UpdateAvailible();
        }

        public virtual void MoveNext(int iteration = 0)
        {
            int len = cameras.Count;
            int max = 0;

            int workingCams = 0;
            for (int i = 0; i < cameras.Count; i++)
            {
                if(!cameras[i].broken && !cameras[i].jammed)
                {
                    workingCams++;
                }
            }
            if(workingCams <= 0)
            {
                return;
            }

            cameras[camIndex].Disconnect();
            if (camIndex == len - 1)
            {
                camIndex = 0;
            }
            else
            {
                camIndex += 1;
            }

            int max2 = 0;

            if (PlayerStats.CycleCameras)
            {
                while (cameras[camIndex].editorName != categories[currentCategory] && max2 < len)
                {
                    cameras[camIndex].Disconnect();
                    if (camIndex == len - 1)
                    {
                        camIndex = 0;
                    }
                    else
                    {
                        camIndex += 1;
                    }
                    max2++;
                }
            }

            while (cameras[camIndex].broken && cameras.Count > 0 && max < 40)
            {
                //GetAvailibleCameras();
                if (camIndex == len - 1)
                {
                    camIndex = 0;
                }
                else
                {
                    camIndex += 1;
                }
                max += 1;
            }


            if (camIndex >= len)
            {
                camIndex = 0;
            }
            if (camIndex < 0)
            {
                camIndex = 0;
            }

            cameras[camIndex].Connect();

            int sameCategory = 0;

            foreach (ObservationThing obs in cameras)
            {
                if (obs.editorName == categories[currentCategory])
                {
                    sameCategory++;
                }
            }
        }
        public virtual void MovePrev(int iteration = 0)
        {
            int max = 0;
            int len = cameras.Count;

            int workingCams = 0;
            for (int i = 0; i < cameras.Count; i++)
            {
                if (!cameras[i].broken && !cameras[i].jammed)
                {
                    workingCams++;
                }
            }
            if (workingCams <= 0)
            {
                return;
            }

            cameras[camIndex].Disconnect();
            if (camIndex == 0)
            {
                camIndex = len - 1;
            }
            else
            {
                camIndex -= 1;
            }


            int max2 = 0;
            if (PlayerStats.CycleCameras)
            {
                while (cameras[camIndex].editorName != categories[currentCategory] && max2 < len)
                {
                    cameras[camIndex].Disconnect();
                    if (camIndex == 0)
                    {
                        camIndex = len - 1;
                    }
                    else
                    {
                        camIndex -= 1;
                    }
                    max2++;
                }
            }


            while (cameras[camIndex].broken && cameras.Count > 0 && max < 40)
            {
                //GetAvailibleCameras();
                if (camIndex == 0)
                {
                    camIndex = len - 1;
                }
                else
                {
                    camIndex -= 1;
                }
                max += 1;
            }

            if (camIndex >= len)
            {
                camIndex = len - 1;
            }
            if (camIndex < 0)
            {
                camIndex = 0;
            }

            cameras[camIndex].Connect();

            int sameCategory = 0;
            foreach (ObservationThing obs in cameras)
            {
                if (obs.editorName == categories[currentCategory])
                {
                    sameCategory++;
                }
            }
        }

        public virtual void PrevCategory()
        {
            currentCategory++;

            if (currentCategory >= categories.Count)
            {
                currentCategory = 0;
            }
            if (currentCategory < 0)
            {
                currentCategory = categories.Count - 1;
            }
            if (categories.Count == 0)
            {
                currentCategory = 0;
            }
        }        
        public virtual void NextCategory()
        {
            currentCategory--;

            if (currentCategory >= categories.Count)
            {
                currentCategory = 0;
            }
            if (currentCategory < 0)
            {
                currentCategory = categories.Count - 1;
            }
            if (categories.Count == 0)
            {
                currentCategory = 0;
            }

        }

        public virtual void TakePhoto()
        {
            if (camIndex < cameras.Count && !cameras[camIndex].jammed && oper != null && !oper.HasEffect("Phone called"))
            {
                //Level.current.camera.size *= new Vec2(10f, 10f);
                //Level.current.camera.position -= Level.current.camera.size * 0.5f;

                Vec2 renderTargetSize = new Vec2(320f, 180f);
                Vec2 pos = cameras[camIndex].position;

                Vec2 prevPos = Level.current.camera.position;
                Level.current.camera.position = pos - Level.current.camera.size * 0.5f;

                if (_captureTarget == null)
                {
                    _captureTarget = new RenderTarget2D((int)renderTargetSize.x, (int)renderTargetSize.y, false);
                }
                float renderTargetZoom = 5.625f;
                Camera camera = new Camera(0.0f, 0.0f, _captureTarget.width * renderTargetZoom, _captureTarget.height * renderTargetZoom);

                bool prevForeSet = Layer.Foreground.visible;
                bool prevHUDSet = Layer.HUD.visible;
                bool prevLightSet = Layer.Lighting.visible;

                Layer.Foreground.visible = false;
                Layer.HUD.visible = false;
                Layer.Lighting.visible = false;

                MonoMain.RenderGame(MonoMain.screenCapture);

                Layer.HUD.visible = prevForeSet;
                Layer.Foreground.visible = prevHUDSet;
                Layer.Lighting.visible = prevLightSet;

                Matrix result;
                Matrix.CreateOrthographicOffCenter(0.0f, MonoMain.screenCapture.width, MonoMain.screenCapture.height, 0.0f, 0.0f, -1f, out result);
                result.M41 += -0.5f * result.M11;
                result.M42 += -0.5f * result.M22;
                Matrix matrix = Level.current.camera.getMatrix();
                Vec3 vec3 = Graphics.viewport.Project(new Vec3(pos.x, pos.y, 0.0f), result, matrix, Matrix.Identity);
                Graphics.SetRenderTarget(_captureTarget);
                camera.center = new Vec2(vec3.x, vec3.y);
                Graphics.Clear(Color.Black);
                Graphics.screen.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone, null, camera.getMatrix());
                Graphics.Draw(MonoMain.screenCapture, 0.0f, 0.0f, 1f, 1f, depth);
                Graphics.screen.End();
                Graphics.SetRenderTarget(null);
                Level.current.camera.position = prevPos;
            }
        }

        public override void Update()
        {
            int prevCamIndex = camIndex;
            while (cameras.Count <= camIndex)
            {
                camIndex--;
            }
            if(camIndex < 0)
            {
                camIndex = 0;
                locked = true;
            }
            else
            {
                locked = false;
            }

            if (currentCategory >= categories.Count && categories.Count > 0)
            {
                currentCategory--;
            }

            if(unable > 0)
            {
                unable--;
            }

            //GetAvailibleCameras();
            observing = false;
            if(availible <= 0)
            {
                if(oper != null)
                {
                    //oper.ChangeWeapon(10, 1);
                }
            }
            if(cameras.Count <= 0)
            {
                GetAvailibleCameras();
                if(oper != null)
                {
                    if (oper.HasEffect("Phone called"))
                    {
                        camIndex = 0;
                    }
                    else
                    {
                        //oper.BackToWeapon(20);
                        //oper.immobilized = false;
                    }
                }
            }
            if(oper != null)
            {
                if(oper.holdObject == this)
                {
                    oper.immobilized = true;
                }
                else
                {
                    oper.immobilized = false;
                }
            }
            

            if (oper != null && cameras.Count > 0)
            {
                if (oper.holdObject != null)
                {
                    if (oper.holdObject == this)
                    {
                        observing = true;
                        if (oper.controller && oper.genericController != null && unable <= 0)
                        {
                            if (oper.duckOwner.inputProfile.genericController.MapPressed(2097152, false))
                            {
                                MovePrev();
                            }
                            if (oper.duckOwner.inputProfile.genericController.MapPressed(1073741824, false))
                            {
                                MoveNext();
                            }
                            if (oper.duckOwner.inputProfile.genericController.MapPressed(8388608, false))
                            {
                                PrevCategory();
                                MovePrev();
                            }
                            if (oper.duckOwner.inputProfile.genericController.MapPressed(4194304, false))
                            {
                                NextCategory();
                                MoveNext();
                            }
                            if (currentCategory < 0)
                            {
                                currentCategory = categories.Count - 1;
                            }
                            if (currentCategory >= categories.Count)
                            {
                                currentCategory = 0;
                            }
                            if (categories.Count == 0)
                            {
                                currentCategory = 0;
                            }
                        }
                        else if (unable <= 0)
                        {
                            if (Keyboard.Pressed(PlayerStats.keyBindings[15]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[15]))
                            {
                                MovePrev();
                            }
                            if (Keyboard.Pressed(PlayerStats.keyBindings[16]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[16]))
                            {
                                MoveNext();
                            }

                            if (Keyboard.Pressed(PlayerStats.keyBindings[17]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[17]))
                            {
                                PrevCategory();
                                MovePrev();
                            }
                            if (Keyboard.Pressed(PlayerStats.keyBindings[18]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[18]))
                            {
                                NextCategory();
                                MoveNext();
                            }
                            if (currentCategory < 0)
                            {
                                currentCategory = categories.Count - 1;
                            }
                            if (currentCategory >= categories.Count)
                            {
                                currentCategory = 0;
                            }
                            if (categories.Count == 0)
                            {
                                currentCategory = 0;
                            }
                        }


                        if (cameras.Count > camIndex)
                        {
                            //Level.current.camera.position = cameras[camIndex].position - Level.current.camera.size / 2;

                            if (cameras[camIndex].controllable)
                            {
                                if (cameras[camIndex].oper == oper && unable <= 0)
                                {
                                    if (oper.controller && oper.genericController != null)
                                    {
                                        cameras[camIndex].moveLeft = oper.duckOwner.inputProfile.genericController.MapDown(2097152, false);
                                        cameras[camIndex].moveRight = oper.duckOwner.inputProfile.genericController.MapDown(1073741824, false);
                                        cameras[camIndex].jump = oper.duckOwner.inputProfile.genericController.MapDown(4096, false);
                                        cameras[camIndex].fire = oper.duckOwner.inputProfile.genericController.MapDown(4194304, false);
                                    }
                                    else
                                    {
                                        cameras[camIndex].moveLeft = Keyboard.Down(PlayerStats.keyBindings[2]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[2]);
                                        cameras[camIndex].moveRight = Keyboard.Down(PlayerStats.keyBindings[3]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[3]);
                                        cameras[camIndex].jump = Keyboard.Pressed(PlayerStats.keyBindings[0]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[0]);
                                        cameras[camIndex].fire = Keyboard.Pressed(PlayerStats.keyBindings[13]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[13]);
                                    }
                                    /*
                                    cameras[camIndex].moveLeft = input.Down("LEFT");
                                    cameras[camIndex].moveRight = input.Down("RIGHT");
                                    cameras[camIndex].jump = input.Pressed("JUMP");
                                    cameras[camIndex].fire = input.Pressed("FIRE");
                                     */
                                }
                                else
                                {
                                    cameras[camIndex].moveLeft = false;
                                    cameras[camIndex].moveRight = false;
                                    cameras[camIndex].jump = false;
                                    cameras[camIndex].fire = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        observing = false;
                        oper.observing = false;
                    }
                }
            }
            if (observing && !locked)
            {
                if(photoDelay <= 0)
                {
                    TakePhoto();
                    //photoDelay = 1;
                }
                else
                {
                    photoDelay--;
                }
            }

            if(camIndex != prevCamIndex)
            {
                lagFrames = 10;
            }

            base.Update();

            foreach(ObservationThing obs in removeCameras)
            {
                if (cameras.Contains(obs))
                {
                    cameras.Remove(obs);
                }
            }
            foreach (ObservationThing obs in addCameras)
            {
                if (!cameras.Contains(obs))
                {
                    if (obs.oper != null && obs.oper == oper)
                    {
                        camIndex = ConnectedCameras();
                    }
                    cameras.Add(obs);
                }
            }

            removeCameras.Clear();
        }

        public virtual void ConnectObservable(ObservationThing camera)
        {
            if (!addCameras.Contains(camera))
            {
                addCameras.Add(camera);
            }
        }
        public virtual void DisconnectObservable(ObservationThing camera)
        {
            if (!removeCameras.Contains(camera))
            {
                removeCameras.Add(camera);
            }
        }
        public virtual ObservationThing GetObservable(int index)
        {
            if (cameras.Count > index)
            {
                return cameras[index];
            }
            return null;
        }
        public virtual ObservationThing GetCurrentObservable()
        {
            return GetObservable(camIndex);
        }
        public virtual int ConnectedCameras()
        {
            return cameras.Count;
        }

        public void DrawPicture()
        {
            Vec2 camPos = Level.current.camera.position;
            Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);

            if (!locked)
            {
                if (!jammed)
                {
                    Material material = Graphics.material;
                    if (lagFrames > 0)
                    {
                        Graphics.material = new MaterialGlitch(this) { amount = Rando.Float(0.8f, 1) * (lagFrames * 0.05f), yoffset = Rando.Float(-0.5f, 0.5f) };
                        lagFrames--;
                        Graphics.Draw(_captureTarget, camPos + new Vec2(160, 89.5f) * Unit, null, Color.White, 0, new Vec2(160f, 90f), monitorSize, SpriteEffects.None, depth);
                        Graphics.material = material;
                    }
                    else
                    {
                        Graphics.Draw(_captureTarget, camPos + new Vec2(160, 89.5f) * Unit, null, Color.White, 0, new Vec2(160f, 90f), monitorSize, SpriteEffects.None, depth);
                    }
                }

                _guiFilter.CenterOrigin();
                if (cameras.Count > camIndex)
                {
                    _guiFilter.frame = 1 + cameras[camIndex].cameraOverlay;
                    Parasyte p = new Parasyte(position.x, position.y);
                    if (Level.CheckCircle<Parasyte>(cameras[camIndex].position, p.radius + 16f) != null)
                    {
                        SpriteMap _not = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ParasyteNotification.png"), 23, 23, false);
                        _not.CenterOrigin();
                        _not.alpha = (float)Math.Sin(DateTime.Now.Millisecond * 0.00314) * 0.2f + 0.8f;
                        Graphics.Draw(_not, Level.current.camera.position.x + Level.current.camera.size.x * 0.5f, Level.current.camera.position.y + Level.current.camera.size.y * 0.7f, depth.value + 0.011f);
                    }
                }

                //_gui.alpha = 0.5f;
                _guiFilter.scale = new Vec2(Level.current.camera.size.x / 63.9f, Level.current.camera.size.y / 35.9f) * monitorSize;

                Graphics.Draw(_guiFilter, Level.current.camera.position.x + Level.current.camera.size.x * 0.5f, Level.current.camera.position.y + Level.current.camera.size.y * 0.5f, depth.value + 0.1f);
            }
        }

        List<ObservationThing> sameCategory = new List<ObservationThing>();
        List<int> ids = new List<int>();
        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                Vec2 Scale = Level.current.camera.size / new Vec2(320, 180);
                if (_captureTarget != null && observing)
                {
                    DrawPicture();
                }

                if (oper != null && oper.local)
                {
                    _phone.scale = Scale * (monitorSize * new Vec2(4f, 4f)) * phoneSize;
                    Graphics.Draw(_phone, Level.current.camera.position.x + Level.current.camera.size.x * 0.5f, 
                        Level.current.camera.position.y + Level.current.camera.size.y * 0.5f, -0.997f);
                    
                    _phoneScreen.scale = Scale * (monitorSize * new Vec2(4f, 4f)) * phoneSize;
                    Graphics.Draw(_phoneScreen, Level.current.camera.position.x + Level.current.camera.size.x * 0.5f, 
                        Level.current.camera.position.y + Level.current.camera.size.y * 0.5f, -0.999f);
                    
                    if (locked)
                    {
                        Material material = Graphics.material;
                        _phoneScreen1.scale = Scale * (monitorSize * new Vec2(4f, 4f)) * phoneSize;                        
                        Graphics.material = new MaterialGlitch(this) { amount = Rando.Float(1), yoffset = Rando.Float(-0.5f, 0.5f) };
                        Graphics.Draw(_phoneScreen1, Level.current.camera.position.x + Level.current.camera.size.x * 0.5f, 
                            Level.current.camera.position.y + Level.current.camera.size.y * 0.5f, -0.998f);
                        Graphics.material = material;
                        //Graphics.Draw(_phoneScreen, Level.current.camera.position.x + Level.current.camera.size.x * 0.5f, Level.current.camera.position.y + Level.current.camera.size.y * 0.5f, -1.02f);
                    }


                    if (observing)
                    {
                        _category.CenterOrigin();

                        if (oper.HasEffect("Phone called"))
                        {
                            unable = 20;
                            locked = true;
                        }
                        if (cameras.Count > camIndex)
                        {
                            Vec2 camPos = Level.current.camera.position;
                            Vec2 camSize = Level.current.camera.size;
                            _gui.scale = Scale * new Vec2(0.6667f, 0.6667f);
                            _marker.scale = _gui.scale * new Vec2(0.75f, 0.75f);
                            Graphics.Draw(_gui, camPos.x + _gui.scale.x * 6, camPos.y + camSize.y - _gui.scale.y * 60, 0.9f);

                            sameCategory.Clear();
                            ids.Clear();

                            int l = 0;
                            _category.scale = _gui.scale;

                            if (!locked)
                            {
                                foreach (string name in categories)
                                {
                                    //DevConsole.Log(Convert.ToString(categories.Count));
                                    if (name == "OBS cam")
                                    {
                                        _category.frame = 0;
                                    }
                                    if (name == "OBS drone")
                                    {
                                        _category.frame = 1;
                                    }
                                    if (name == "OBS shock")
                                    {
                                        _category.frame = 2;
                                    }
                                    if (name == "OBS blackeye")
                                    {
                                        _category.frame = 3;
                                    }
                                    if (name == "OBS yokai")
                                    {
                                        _category.frame = 4;
                                    }
                                    if (name == "OBS evileye")
                                    {
                                        _category.frame = 5;
                                    }
                                    if (name == "OBS bproof")
                                    {
                                        _category.frame = 6;
                                    }
                                    if (name == "OBS Ratero")
                                    {
                                        _category.frame = 7;
                                    }
                                    if (name == "OBS ARGUS")
                                    {
                                        _category.frame = 8;
                                    }
                                    float center = 84;

                                    if (categories.Count > currentCategory && categories[currentCategory] == name)
                                    {
                                        _category.scale *= 1.2f;
                                        Graphics.DrawRect(new Vec2(camPos.x + _gui.scale.x * (center - 28 * (categories.Count - 1) * 0.5f + 28 * l) - 9, 
                                            camPos.y + camSize.y - _gui.scale.y * 60), new Vec2(camPos.x + _gui.scale.x * (center - 28 * (categories.Count - 1) * 0.5f + 28 * l) + 9, 
                                            camPos.y + camSize.y - _gui.scale.y * 60 + 16), Color.Black * 0.3f, 0.92f);
                                    }
                                    else
                                    {
                                        _category.scale = _gui.scale;
                                    }
                                    Graphics.Draw(_category, camPos.x + _gui.scale.x * (center - 28 * (categories.Count - 1) * 0.5f + 28 * l), 
                                        camPos.y + camSize.y - _gui.scale.y * 48, 0.91f);
                                    l++;
                                }
                            }

                            if (oper.observing)
                            {
                                if (cameras[camIndex].jammed || cameras[camIndex].broken)
                                {
                                    Vec2 Pos = Level.current.camera.position;
                                    _jamm.scale = Level.current.camera.size * new Vec2(0.4545f, 0.4545f);
                                    _jamm.scale *= monitorSize;
                                    _jamm.frame = Rando.Int(2);
                                    _jamm.alpha = 1f;

                                    Graphics.Draw(_jamm, Pos.x + Level.current.camera.width * 0.5f, Pos.y + Level.current.camera.height * 0.5f, depth.value + 0.09f);

                                    Material m = Graphics.material;
                                    Graphics.material = new MaterialGlitch(this) { amount = Rando.Float(2, 4), yoffset = Rando.Float(-1, 1) };
                                    Graphics.Draw(_jamm, Pos.x + Level.current.camera.width * 0.5f, Pos.y + Level.current.camera.height * 0.5f, depth.value + 0.1f);

                                    Graphics.material = m;
                                }
                                if (unable > 0)
                                {
                                    Vec2 Pos = Level.current.camera.position;

                                    if (!oper.HasEffect("Phone called"))
                                    {
                                        called = false;
                                    }

                                    _call.scale = Level.current.camera.size * new Vec2(0.1587f, 0.0286f);
                                    _call.scale *= monitorSize;
                                    _call.alpha = 1f;
                                    Graphics.Draw(_call, Pos.x + Level.current.camera.width * 0.5f, Pos.y + Level.current.camera.height * 0.5f, depth.value + 0.12f);
                                }
                            }



                            for (int i = 0; i < cameras.Count; i++)
                            {
                                if (categories.Count > currentCategory && cameras[i].editorName == categories[currentCategory])
                                {
                                    sameCategory.Add(cameras[i]);
                                    ids.Add(i);
                                }
                            }
                            for (int i = 0; i < sameCategory.Count; i++)
                            {
                                _marker.frame = 0;
                                if (cameras[ids[i]].jammed)
                                {
                                    _marker.frame = 1;
                                }
                                if (ids[i] == camIndex)
                                {
                                    _marker.frame = 3;
                                }
                                if (cameras[ids[i]].broken)
                                {
                                    _marker.frame = 2;
                                }

                                Graphics.Draw(_marker, camPos.x + _gui.scale.x * (75 - 14 * (sameCategory.Count - 1) * 0.5f + 14 * i), 
                                    camPos.y + camSize.y - _gui.scale.y * 36, 0.9f);
                            }

                            if (!(Level.current is Editor))
                            {
                                if (!PlayerStats.hideKeyBindIcons)
                                {
                                    DrawControlls();
                                }
                            }
                        }
                    }
                }
                base.OnDrawLayer(Layer.Foreground);
            }
        }
        public void DrawControlls()
        {
            float Unit = 320 / Level.current.camera.size.x;

            _widebutton.scale = new Vec2(0.6f, 0.6f) * Unit;

            _button.scale = new Vec2(0.6f, 0.6f) * Unit;
            if (oper.controller)
            {
                if (!locked)
                {
                    //Previous camera
                    _button.frame = PlayerStats.GetFrameOfButtonGP("LStickLEFT");
                    Graphics.Draw(_button, Level.current.camera.position.x + 10 * Unit, Level.current.camera.position.y + 161 * Unit, 1);


                    //Next camera
                    _button.frame = PlayerStats.GetFrameOfButtonGP("LStickRIGHT");
                    Graphics.Draw(_button, Level.current.camera.position.x + 104 * Unit, Level.current.camera.position.y + 161 * Unit, 1);



                    //Previous category
                    _button.scale = new Vec2(0.9f, 0.9f) * Unit;
                    _button.frame = PlayerStats.GetFrameOfButtonGP("LB");
                    Graphics.Draw(_button, Level.current.camera.position.x + 17 * Unit, Level.current.camera.position.y + 148 * Unit, 1);


                    //Next category
                    _button.scale = new Vec2(0.9f, 0.9f) * Unit;
                    _button.frame = PlayerStats.GetFrameOfButtonGP("RB");
                    Graphics.Draw(_button, Level.current.camera.position.x + 96 * Unit, Level.current.camera.position.y + 148 * Unit, 1);
                }

                //Close phone

                _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                _button.frame = PlayerStats.GetFrameOfButtonGP("B");
                Graphics.Draw(_button, Level.current.camera.position.x + 26 * Unit, Level.current.camera.position.y + 170 * Unit, 1);

                Graphics.DrawStringOutline("- to close cameras", new Vec2(Level.current.camera.position.x + 32 * Unit, Level.current.camera.position.y + 168 * Unit), 
                    Color.White, Color.Black, 1f, null, 0.5f);
            }
            else
            {
                if (!locked)
                {
                    //Previous camera
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[15]))
                    {
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[15]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 10 * Unit, Level.current.camera.position.y + 161 * Unit, 1);
                    }
                    else
                    {
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[15]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 10 * Unit, Level.current.camera.position.y + 161 * Unit, 1);
                    }

                    //Next camera
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[16]))
                    {
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[16]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 104 * Unit, Level.current.camera.position.y + 161 * Unit, 1);
                    }
                    else
                    {
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[16]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 104 * Unit, Level.current.camera.position.y + 161 * Unit, 1);
                    }


                    //Previous category
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[17]))
                    {
                        _widebutton.scale = new Vec2(0.6f, 0.6f) * Unit;
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[17]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 17 * Unit, Level.current.camera.position.y + 148 * Unit, 1);
                    }
                    else
                    {
                        _button.scale = new Vec2(0.9f, 0.9f) * Unit;
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[17]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 17 * Unit, Level.current.camera.position.y + 148 * Unit, 1);
                    }

                    //Next category
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[18]))
                    {
                        _widebutton.scale = new Vec2(0.6f, 0.6f) * Unit;
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[18]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 96 * Unit, Level.current.camera.position.y + 148 * Unit, 1);
                    }
                    else
                    {
                        _button.scale = new Vec2(0.9f, 0.9f) * Unit;
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[18]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 96 * Unit, Level.current.camera.position.y + 148 * Unit, 1);
                    }
                }

                //Close phone
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[11]))
                {
                    _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[11]);
                    Graphics.Draw(_widebutton, Level.current.camera.position.x + 30 * Unit, Level.current.camera.position.y + 170 * Unit, 1);
                }
                else
                {
                    _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[11]);
                    Graphics.Draw(_button, Level.current.camera.position.x + 26 * Unit, Level.current.camera.position.y + 170 * Unit, 1);
                }
                Graphics.DrawStringOutline("- to close cameras", new Vec2(Level.current.camera.position.x + 32 * Unit, Level.current.camera.position.y + 168 * Unit), 
                    Color.White, Color.Black, 1f, null, 0.5f);
            }
        }
    }
}