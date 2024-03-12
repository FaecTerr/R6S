using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack1 : BackgroundTile
    {
        public BackgroundPack1(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack1.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack1";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack2 : BackgroundTile
    {
        public BackgroundPack2(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack2.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack2";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack3 : BackgroundTile
    {
        public BackgroundPack3(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack3.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack3";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack4 : BackgroundTile
    {
        public BackgroundPack4(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack4.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack4";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack5 : BackgroundTile
    {
        public BackgroundPack5(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack5.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack5";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack6 : BackgroundTile
    {
        public BackgroundPack6(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack6.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack6";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack7 : BackgroundTile
    {
        public BackgroundPack7(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack7.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack7";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundPack8 : BackgroundTile
    {
        public BackgroundPack8(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/BGPack8.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Pack8";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Packground")]
    public class PBackgroundPack3 : BackgroundTile
    {
        public PBackgroundPack3(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/PGPack3.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "PPack3";
            layer = Layer.Background;
            depth = 1f;
        }
    }
    [EditorGroup("Faecterr's|Tiles|Packground")]
    public class PBackgroundPack4 : BackgroundTile
    {
        public PBackgroundPack4(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/PGPack4.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "PPack4";
            layer = Layer.Background;
            depth = 1f;
        }
    }
    [EditorGroup("Faecterr's|Tiles|Packground")]
    public class PBackgroundPack5 : BackgroundTile
    {
        public PBackgroundPack5(float xpos, float ypos) : base(xpos, ypos)
        {   
            graphic = new SpriteMap(GetPath("Sprites/Decorations/PGPack5.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "PPack5";
            layer = Layer.Background;
            depth = 1f;
        }
    }
    [EditorGroup("Faecterr's|Tiles|Packground")]
    public class PBackgroundPack6 : BackgroundTile
    {
        public PBackgroundPack6(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/PGPack6.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "PPack6";
            layer = Layer.Background;
            depth = 1f;
        }
    }
    [EditorGroup("Faecterr's|Tiles|Packground")]
    public class PBackgroundPack7 : BackgroundTile
    {
        public PBackgroundPack7(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/PGPack7.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "PPack7";
            layer = Layer.Background;
            depth = 1f;
        }
    }
    [EditorGroup("Faecterr's|Tiles|Packground")]
    public class PBackgroundPack8 : BackgroundTile
    {
        public PBackgroundPack8(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/PGPack8.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "PPack8";
            layer = Layer.Background;
            depth = 1f;
        }
    }

    [EditorGroup("Faecterr's|Tiles")]
    public class FlatPlat : AutoPlatform
    {
        public FlatPlat(float xpos, float ypos) : base(xpos, ypos, Mod.GetPath<R6S>("Sprites/Decorations/Flat.png"))
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/Flat.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Flat plat";
        }
    }
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class DKASBlock : AutoBlock
    {
        public DKASBlock(float xpos, float ypos) : base(xpos, ypos, GetPath<R6S>("Sprites/Decorations/DkasStyle.png"))
        {
            verticalWidth = 16f;
            verticalWidthThick = 16f;
            horizontalHeight = 16f;
            _editorName = "DKAS Block";
            physicsMaterial = PhysicsMaterial.Wood;
        }
    }
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class DKASBlockMetal : AutoBlock
    {
        public DKASBlockMetal(float xpos, float ypos) : base(xpos, ypos, GetPath<R6S>("Sprites/Decorations/DkasStyle2.png"))
        {
            verticalWidth = 16f;
            verticalWidthThick = 16f;
            horizontalHeight = 16f;
            _editorName = "DKAS M Block";
            physicsMaterial = PhysicsMaterial.Metal;
        }
    }
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class InvisBlock : AutoBlock
    {
        public InvisBlock(float xpos, float ypos) : base(xpos, ypos, Mod.GetPath<R6S>("Sprites/Decorations/Blank.png"))
        {
            graphic = new SpriteMap(GetPath("Sprites/Decorations/Blank.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Inviblock";
            layer = Layer.Blocks;
            depth = 1f;
        }

        public override void Draw()
        {
            if(Level.current is Editor)
            {
                SpriteMap _s = new SpriteMap(GetPath("Sprites/Decorations/BlankMarker.png"), 16, 16);
                _s.CenterOrigin();
                Graphics.Draw(_s, position.x, position.y);
            }
            base.Draw();
        }
    }

    [EditorGroup("Faecterr's|Tiles|Background")]
    public class VentBack : BackgroundTile
    {
        public VentBack(float xpos, float ypos) : base(xpos, ypos)
        {         
            graphic = new SpriteMap(GetPath("Sprites/Decorations/VentsMarker.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Vents";
            depth = 1f;
        }
    }
}
