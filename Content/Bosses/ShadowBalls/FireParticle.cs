﻿using Coralite.Core;
using Coralite.Core.Systems.ParticleSystem;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Coralite.Content.Bosses.ShadowBalls
{
    public class FireParticle : Particle
    {
        public override string Texture => AssetDirectory.ShadowBalls + Name;

        private SpriteEffects effect;

        public override void OnSpawn()
        {
            //color.A = 0;

            Frame = new Rectangle(0, Main.rand.Next(3), 1, 1);
            effect = Main.rand.NextBool() ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Rotation = Velocity.ToRotation() - MathHelper.PiOver2;
        }

        public override void Update()
        {
            fadeIn++;

            if (fadeIn % 5 == 0)
            {
                Frame.Y++;
                if (Frame.Y > 15)
                    active = false;
            }

            Velocity *= 0.95f;
            color *= 0.96f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D mainTex = GetTexture().Value;
            Rectangle frame = mainTex.Frame(1, 16, 0, this.Frame.Y);
            Vector2 origin = frame.Size() / 2;

            spriteBatch.Draw(mainTex, Center - Main.screenPosition, frame, color, Rotation, origin, Scale
                , effect, 0f);
            spriteBatch.Draw(mainTex, Center - Main.screenPosition, frame, color * 0.5f, Rotation, origin, Scale
                , effect, 0f);
        }
    }
}
