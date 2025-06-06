﻿using Coralite.Content.Particles;
using Coralite.Core;
using Coralite.Core.Attributes;
using Coralite.Core.Configs;
using Coralite.Core.Prefabs.Projectiles;
using Coralite.Helpers;
using InnoVault.GameContent.BaseEntity;
using InnoVault.PRT;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;

namespace Coralite.Content.Items.HyacinthSeries
{
    [AutoLoadTexture(Path = AssetDirectory.HyacinthSeriesItems)]
    public class StarsBreathHeldProj : BaseGunHeldProj
    {
        public StarsBreathHeldProj() : base(0.1f, 22, -6, AssetDirectory.HyacinthSeriesItems) { }

        public static ATex StarsBreathEffect { get; private set; }

        protected override float HeldPositionY => -2;

        public override void InitializeGun()
        {
            Projectile.scale = 0.8f;
            float rotation = TargetRot + (DirSign > 0 ? 0 : MathHelper.Pi);
            Vector2 dir = rotation.ToRotationVector2();
            Vector2 center = Projectile.Center + (dir * 54);
            for (int i = 0; i < 3; i++)
            {
                Color color = Main.rand.Next(3) switch
                {
                    0 => new Color(126, 70, 219),
                    1 => new Color(219, 70, 178),
                    _ => Color.White
                };
                PRTLoader.NewParticle(center + Main.rand.NextVector2Circular(6, 6), dir.RotatedBy(Main.rand.NextFloat(-0.5f, 0.5f)) * Main.rand.NextFloat(1.2f, 2.3f), CoraliteContent.ParticleType<HorizontalStar>(), color, Main.rand.NextFloat(0.05f, 0.15f));
            }
        }

        public override void ModifyAI(float factor)
        {
            if (Projectile.timeLeft != MaxTime && Projectile.timeLeft % 2 == 0)
            {
                Projectile.frame++;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            base.PreDraw(ref lightColor);

            if (Projectile.frame > 4)
                return false;

            Texture2D effect = StarsBreathEffect.Value;
            Rectangle frameBox = effect.Frame(1, 5, 0, Projectile.frame);
            SpriteEffects effects = DirSign > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            float rot = Projectile.rotation + (DirSign > 0 ? 0 : MathHelper.Pi);
            float n = rot - DirSign * MathHelper.PiOver2;

            Main.spriteBatch.Draw(effect, Projectile.Center + rot.ToRotationVector2() * 55 + n.ToRotationVector2() * 4 - Main.screenPosition, frameBox, Color.White
                , rot, new Vector2(0, frameBox.Height / 2), Projectile.scale * 2, 0, 0f);
            return false;
        }
    }

    /// <summary>
    /// ai0用于控制弹幕颜色
    /// </summary>
    public class StarsBreathBullet : BaseHeldProj
    {
        public override string Texture => AssetDirectory.Blank;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.timeLeft = 70;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.localNPCHitCooldown = 10;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.netImportant = true;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void Initialize()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 31 && Projectile.timeLeft > 5)
            {
                if (Projectile.timeLeft % 10 == 0 && Projectile.IsOwnedByLocalPlayer())
                {
                    float factor = (30 - Projectile.timeLeft) / 10;
                    float scale = 0.4f + (0.1f * factor);
                    Vector2 center = Projectile.Center + Main.rand.NextVector2CircularEdge(8, 8);

                    Projectile.NewProjectileFromThis<StarsBreathExplosion>(center, Vector2.Zero, (int)(Projectile.damage * 0.45f), Projectile.knockBack, scale);
                    PRTLoader.NewParticle(center, Vector2.Zero, CoraliteContent.ParticleType<RainbowHalo>(), Color.White, scale + 0.1f);
                    if (factor == 0)
                        PlaySound();
                }
            }

            if (Projectile.timeLeft < 10)
                Projectile.Kill();

        }

        public void PlaySound()
        {
            SoundStyle style = CoraliteSoundID.TerraBlade_Item60;
            style.Volume = 0.9f;
            style.Pitch = 1f;
            SoundEngine.PlaySound(style, Projectile.Center);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.timeLeft > 32)
                Projectile.timeLeft = 31;

            Projectile.damage = (int)(Projectile.damage * 0.75f);
            if (Projectile.damage < 5)
                Projectile.damage = 5;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.IsOwnedByLocalPlayer())
            {
                Vector2 center = Projectile.Center + Main.rand.NextVector2CircularEdge(8, 8);

                Projectile.NewProjectileFromThis<StarsBreathExplosion>(center, Vector2.Zero, (int)(Projectile.damage * 0.45f), Projectile.knockBack, 0.6f);
                if (VisualEffectSystem.HitEffect_SpecialParticles)
                    PRTLoader.NewParticle(center, Vector2.Zero, CoraliteContent.ParticleType<RainbowHalo>(), Color.White, 0.6f);
                if (Projectile.timeLeft > 31)
                    PlaySound();
            }

            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 center = Projectile.Center - Main.screenPosition;
            Color shineColor = Projectile.ai[0] switch
            {
                0 => new Color(126, 70, 219),
                1 => new Color(219, 70, 178),
                _ => Color.White
            };
            shineColor *= 0.8f;

            Helper.DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, center
                , new Color(255, 255, 255, 0) * 0.7f, shineColor,
                0.5f, 0f, 0.5f, 0.5f, 1f,
                Projectile.rotation, new Vector2(3.3f, 1f), Vector2.One * 1.7f);
            Helper.DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, center + ((Projectile.rotation + 1.57f + 0.785f).ToRotationVector2() * 8)
                , new Color(255, 255, 255, 0) * 0.7f, shineColor,
                0.5f, 0f, 0.5f, 0.5f, 1f,
                Projectile.rotation, new Vector2(1f, 0.3f), Vector2.One);
            Helper.DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, center - ((Projectile.rotation + 1.57f + 0.785f).ToRotationVector2() * 8)
                , new Color(255, 255, 255, 0) * 0.7f, shineColor,
                0.5f, 0f, 0.5f, 0.5f, 1f,
                Projectile.rotation, new Vector2(1f, 0.3f), Vector2.One);
            return false;
        }
    }

    /// <summary>
    /// ai0用于控制弹幕大小
    /// </summary>
    public class StarsBreathExplosion : ModProjectile, IDrawAdditive
    {
        public override string Texture => AssetDirectory.HyacinthSeriesItems + Name;

        public ref float frameX => ref Projectile.localAI[0];

        public bool cantDraw = true;
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 112;
            Projectile.timeLeft = 200;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.localNPCHitCooldown = 30;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.netImportant = true;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] == 0)
            {
                Projectile.rotation = Main.rand.NextFloat(6.282f);

                Vector2 center = Projectile.Center;
                Projectile.scale = Projectile.ai[0];
                Projectile.width = Projectile.height = (int)(Projectile.scale * 132);

                Projectile.Center = center;
                Projectile.localAI[1] = 1;
            }
            else
            {
                frameX += 1f;
                if (frameX > 3)
                {
                    if (Projectile.frame < 3)
                    {
                        Color color = Projectile.frame switch
                        {
                            0 => new Color(126, 70, 219),
                            1 => new Color(219, 70, 178),
                            _ => Color.White
                        };

                        int width = (int)(Projectile.width * 0.25f);
                        PRTLoader.NewParticle(Projectile.Center + Main.rand.NextVector2CircularEdge(width, width), Vector2.Zero,
                            CoraliteContent.ParticleType<HorizontalStar>(), color, Projectile.scale * 0.3f);
                    }

                    frameX = 0;
                    Projectile.frame += 1;
                    if (Projectile.frame > 3)
                        Projectile.Kill();
                }
            }
            Projectile.rotation += 0.01f;

            //控制帧图
            //Projectile.frameCounter++;
            //if (Projectile.frameCounter > 2)
            //{
            //    Projectile.frameCounter = 0;

            //}
        }

        public override bool PreDraw(ref Color lightColor) => false;

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            if (cantDraw)
            {
                cantDraw = false;
                return;
            }
            Texture2D mainTex = Projectile.GetTexture();

            spriteBatch.Draw(mainTex, Projectile.Center - Main.screenPosition, mainTex.Frame(4, 4, (int)frameX, Projectile.frame)
                , Color.White * 0.8f, Projectile.rotation, new Vector2(64, 64), Projectile.scale, SpriteEffects.None, 0f);
        }
    }
}
