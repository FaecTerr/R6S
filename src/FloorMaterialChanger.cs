using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class FloorMaterialChanger : Thing, IDrawToDifferentLayers
    {
        public EditorProperty<int> style;
        public EditorProperty<bool> invisible;
        public bool stop; 
        SpriteMap _tile = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/FloorMaterial.png"), 16, 4);
        public FloorMaterialChanger(float xpos, float ypos) : base(xpos, ypos, null)
        {
            _collisionSize = new Vec2(16f, 16f);
            _collisionOffset = new Vec2(-8f, -8f);
            _editorIcon = new Sprite(GetPath("Sprites/Decorations/FloorMaterialEditor.png"), 0f, 0f);
            _editorName = "Floor material";

            style = new EditorProperty<int>(0, null, 0, 5, 1);
            invisible = new EditorProperty<bool>(true);
            depth = 0.7f;
            _tile.CenterOrigin();

            layer = Layer.Foreground;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            base.Update();

            if (!stop)
            {
                PhysicsMaterial p = PhysicsMaterial.Wood;
                if (style == 0)
                {
                    p = PhysicsMaterial.Plastic;
                }
                if (style == 1)
                {
                    p = PhysicsMaterial.Crust;
                }
                if (style == 2)
                {
                    p = PhysicsMaterial.Metal;
                }
                if (style == 3)
                {
                    p = PhysicsMaterial.Paper;
                }
                if (style == 4)
                {
                    p = PhysicsMaterial.Wood;
                }
                if (style == 5)
                {
                    p = PhysicsMaterial.Rubber;
                }

                int changed = 0;
                int required = 1;

                if (Level.CheckPoint<Block>(position) != null)
                {
                    Block b = Level.CheckPoint<Block>(position);
                    b.physicsMaterial = p;

                    if (b is AutoBlock)
                    {
                        if ((b as AutoBlock)._bLeftNub != null)
                        {
                            (b as AutoBlock)._bLeftNub.physicsMaterial = b.physicsMaterial;
                        }
                        if ((b as AutoBlock)._bRightNub != null)
                        {
                            (b as AutoBlock)._bRightNub.physicsMaterial = b.physicsMaterial;
                        }
                    }
                    if(b.physicsMaterial == p)
                    {
                        changed++;
                    }
                }
                if(changed >= required)
                {
                    stop = true;
                }
            }
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Blocks)
            {
                if (Level.current is Editor)
                {
                    bool right = false, left = false;
                    if (Level.CheckPoint<FloorMaterialChanger>(position + new Vec2(-16f, 0)) != null)
                    {
                        left = true;
                    }
                    if (Level.CheckPoint<FloorMaterialChanger>(position + new Vec2(16f, 0)) != null)
                    {
                        right = true;
                    }
                    if (right == left)
                    {
                        _tile.frame = 1 + 3 * style;
                    }
                    else if (right)
                    {
                        _tile.frame = 3 * style;
                    }
                    else
                    {
                        _tile.frame = 2 + 3 * style;
                    }
                    Graphics.Draw(_tile, position.x, position.y - 6, depth);
                    //Graphics.DrawRect(position + new Vec2(-16f, 0) + new Vec2(-1, -1), position + new Vec2(-16f, 0) + new Vec2(1, 1), Color.White);
                    //Graphics.DrawRect(position + new Vec2(16f, 0) + new Vec2(-1, -1), position + new Vec2(16f, 0) + new Vec2(1, 1), Color.White);
                }
                else
                {
                    if (!invisible)
                    {
                        bool right = false, left = false;
                        if (Level.CheckPoint<FloorMaterialChanger>(position + new Vec2(-16f, 0)) != null)
                        {
                            left = true;
                        }
                        if (Level.CheckPoint<FloorMaterialChanger>(position + new Vec2(16f, 0)) != null)
                        {
                            right = true;
                        }
                        if (right == left)
                        {
                            _tile.frame = 1 + 3 * style;
                        }
                        else if (right)
                        {
                            _tile.frame = 3 * style;
                        }
                        else
                        {
                            _tile.frame = 2 + 3 * style;
                        }
                        Graphics.Draw(_tile, position.x, position.y - 6, depth);
                    }
                }
            }
        }
    }
}
