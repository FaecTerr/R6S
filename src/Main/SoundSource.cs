using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SoundSource : Thing
    {
        public float range;
        /// <summary>
        /// G - global (doesn't jam from walls)
        /// J - jamming of walls
        /// S - Can't go through walls
        /// </summary>
        string sType;
        public string sound;
        public float showTime = 2f;
        public bool ShowRange = true;

        public int soundID;

        public bool spawnNewOne = true;

        private Sound sfxsound;
        private float minVolume = 0.15f;
        public float volumeMod = 1;
        public Thing attachedTo;

        public int layerID = -1;

        public SoundSource(float xpos, float ypos, float radius, string source,  string Type = "G", Thing obj = null) : base(xpos, ypos)
        {
            range = radius;
            sType = Type;
            sound = source;
            attachedTo = obj;
            if (spawnNewOne)
            {
                //DuckNetwork.SendToEveryone(new NMSoundSource(new Vec2(xpos, ypos), radius, source, Type));
            }
            foreach(MapLayer mapLayer in Level.current.things[typeof(MapLayer)])
            {
                if(position.x > mapLayer.position.x && position.y > mapLayer.position.y && 
                    position.x < mapLayer.position.x + mapLayer.xTiles * 16 && position.y < mapLayer.position.y + mapLayer.yTiles * 16)
                {
                    layerID = mapLayer.LayerID;
                }
            }
            Play();
        }

        public virtual float CalculateVolume()
        {
            float volume = 1f;
            float r = range;
            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op.local)
                {
                    if (op.deafenFrames > 0)
                    {
                        if (op.deafenFrames > 80)
                        {
                            r *= 0.8f;
                            volume *= 0.9f;
                        }
                        else
                        {
                            r += 0.8f - 0.025f * op.deafenFrames;
                            volume *= 0.9f - 0.025f;
                        }
                    }

                    if (!op.observing && (op.position - position).length < range)
                    {
                        float jam = 0;
                        if (sType.Contains("J"))
                        {
                            foreach (Block b in Level.CheckLineAll<Block>(op.position, position))
                            {
                                jam++;
                            }
                        }
                        if (sType.Contains("S"))
                        {
                            if (Level.CheckLine<Block>(op.position, position) != null)
                            {
                                volume = 0;
                            }
                        }
                        volume -= (float)Math.Pow(Math.Abs((op.position - position).length / range), 2);
                        if (volume < minVolume)
                        {
                            volume = minVolume;
                        }
                        volume -= jam * 0.2f;
                        if(layerID > -1 && op.layerID != layerID)
                        {
                            volume = 0;
                        }
                        if (volume < 0)
                        {
                            volume = 0;
                        }
                    }
                    else if (!op.observing)
                    {
                        volume = 0;
                    }

                    if (op.observing && op.GetPhone().GetCurrentObservable() != null && (op.GetPhone().GetCurrentObservable().position).length < range)
                    {
                        float jam = 0;
                        Vec2 pos = op.GetPhone().GetCurrentObservable().position;
                        if (sType.Contains("J"))
                        {
                            foreach (Block b in Level.CheckLineAll<Block>(pos, position))
                            {
                                jam++;
                            }
                        }
                        if (sType.Contains("S"))
                        {
                            if (Level.CheckLine<Block>(pos, position) != null)
                            {
                                volume = 0;
                            }
                        }
                        volume -= (float)Math.Pow(Math.Abs((pos - position).length / range), 2);
                        if (volume < minVolume)
                        {
                            volume = minVolume;
                        }
                        volume -= jam * 0.2f;
                        if (volume < 0)
                        {
                            volume = 0;
                        }
                    }
                    else if (op.observing)
                    {
                        volume = 0;
                    }
                }
            }
            volume *= volumeMod;
            return volume;
        }

        public virtual float CalculatePan()
        {
            float pan = 0f;
            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op.local)
                {
                    if (!op.observing && (op.position - position).length < range)
                    {
                        pan = (position.x - op.position.x) / (range);
                        if (sType.Contains("J"))
                        {
                            float jam = 0;
                            foreach (Block b in Level.CheckLineAll<Block>(op.position, position))
                            {
                                jam++;
                            }
                        }
                    }
                    else
                    {
                        Vec2 pos = Level.current.camera.position + Level.current.camera.size / 2;
                        pan = (position.x - pos.x) / (range);
                        if (sType.Contains("J"))
                        {
                            float jam = 0;
                            foreach (Block b in Level.CheckLineAll<Block>(pos, position))
                            {
                                jam++;
                            }
                        }
                    }
                }
            }

            if(pan > 1)
            {
                pan = 1;
            }
            if(pan < -1)
            {
                pan = -1;
            }
            return pan;
        }

        public virtual float CalculatePitch()
        {
            float pitch = 0f;
            /*foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op.local)
                {
                    if (!op.observing && (op.position - position).length < range)
                    {
                        if (sType == "J")
                        {
                            foreach (Block b in Level.CheckLineAll<Block>(op.position, position))
                            {
                                pitch -= 0.2f;
                            }
                        }
                    }
                    else
                    {
                        if (sType == "J")
                        {
                            Vec2 pos = Level.current.camera.position + Level.current.camera.size / 2;
                            foreach (Block b in Level.CheckLineAll<Block>(pos, position))
                            {
                                pitch -= 0.2f;
                            }
                        }
                    }
                }
            }

            if (pitch > 1)
            {
                pitch = 1;
            }
            if (pitch < -1)
            {
                pitch = -1;
            }*/


            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op.local)
                {
                    if(op.deafenFrames > 0)
                    {
                        if (op.deafenFrames > 80)
                        {
                            pitch += 0.3f;
                        }
                        else
                        {
                            pitch += 0.0375f * op.deafenFrames;
                        }
                    }
                }
            }

            return pitch;
        }

        public virtual void Play()
        {
            bool loop = false;
            if (sType.Contains("L"))
            {
                loop = true;
            }
            sfxsound = SFX.Play(GetPath(sound), CalculateVolume(), CalculatePitch(), CalculatePan(), loop);
        }

        public override void Update()
        {
            if(showTime > 0)
            {
                showTime -= 0.01666666f;
            }
            else
            {
                Level.Remove(this);
            }

            if(attachedTo != null)
            {
                position = attachedTo.position;
            }

            if(sfxsound != null)
            {
                sfxsound.Volume = CalculateVolume();
                if (showTime > 0.2f)
                {
                    //sfxsound.Pan = CalculatePan();
                    //sfxsound.Pitch = CalculatePitch();
                }
            }

            base.Update();
        }

        public virtual void Cancel()
        {
            if(sfxsound != null)
            {
                sfxsound.Kill();
            }
        }

        public override void Draw()
        {
            if (DevConsole.showCollision && ShowRange)
            {
                Graphics.DrawCircle(position, range, Color.Blue, 1, 1f, 32);
            }
            base.Draw();
        }
    }
}
