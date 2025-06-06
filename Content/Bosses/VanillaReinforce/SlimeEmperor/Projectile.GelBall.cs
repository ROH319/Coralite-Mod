﻿using Coralite.Core;
using Coralite.Helpers;
using InnoVault.GameContent.BaseEntity;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Bosses.VanillaReinforce.SlimeEmperor
{
    public class GelBall : BaseHeldProj
    {
        public override string Texture => AssetDirectory.SlimeEmperor + Name;

        protected Vector2 Scale
        {
            get => new(Projectile.localAI[1], Projectile.localAI[2]);
            set
            {
                Projectile.localAI[1] = value.X;
                Projectile.localAI[2] = value.Y;
            }
        }

        protected ref float ScaleState => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            Projectile.QuickTrailSets(Helper.TrailingMode.RecordAll, 8);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1200;

            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.netImportant = true;
        }

        public override void Initialize()
        {
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width, Projectile.height), DustID.t_Slime,
                      Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.6f, 0.6f)) * Main.rand.NextFloat(0.2f, 0.5f), 150, new Color(78, 136, 255, 80), Main.rand.NextFloat(1f, 1.4f));
                dust.noGravity = true;
            }
        }

        public override void AI()
        {
            if (Projectile.localAI[0] < 1)  //膨胀小动画
            {
                Projectile.localAI[0] += 0.1f;
                Projectile.localAI[1] += 0.1f;
                Projectile.localAI[2] += 0.1f;
                if (Projectile.localAI[0] > 1)
                {
                    Projectile.localAI[0] = 1f;
                    Projectile.localAI[1] = 1f;
                    Projectile.localAI[2] = 1f;
                }
            }

            switch ((int)ScaleState)
            {
                default:
                case 0:
                    Projectile.rotation += 0.1f;

                    break;
                case 1:
                    Scale = Vector2.Lerp(Scale, new Vector2(1.6f, 0.65f), 0.3f);
                    if (Scale.X > 1.55f)
                        ScaleState = 2;
                    break;
                case 2:
                    Scale = Vector2.Lerp(Scale, new Vector2(0.75f, 1.3f), 0.3f);
                    if (Scale.Y > 1.25f)
                        ScaleState = 3;
                    break;
                case 3:
                    Scale = Vector2.Lerp(Scale, Vector2.One, 0.2f);
                    if (Math.Abs(Scale.X - 1) < 0.05f)
                    {
                        Scale = Vector2.One;
                        ScaleState = 0;
                    }
                    break;
            }

            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y > 12)
                Projectile.velocity.Y = 12;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1;
            Projectile.ai[0]++;
            Projectile.netUpdate = true;

            //简易撞墙反弹
            float newVelX = Math.Abs(Projectile.velocity.X);
            float newVelY = Math.Abs(Projectile.velocity.Y);
            float oldVelX = Math.Abs(oldVelocity.X);
            float oldVelY = Math.Abs(oldVelocity.Y);
            if (oldVelX > newVelX)
                Projectile.velocity.X = -oldVelX * 0.8f;
            if (oldVelY > newVelY)
                Projectile.velocity.Y = -oldVelY * 0.8f;

            Projectile.rotation = Projectile.velocity.ToRotation();
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width, Projectile.height), DustID.t_Slime,
                       -Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.6f, 0.6f)) * Main.rand.NextFloat(0.1f, 0.3f), 150, new Color(78, 136, 255, 80), Main.rand.NextFloat(1f, 1.4f));
                dust.noGravity = true;
            }

            return Projectile.ai[0] > 3;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width, Projectile.height), DustID.t_Slime,
                     Helper.NextVec2Dir(1f, 2.5f), 150, new Color(78, 136, 255, 80), Main.rand.NextFloat(1.2f, 1.6f));
            }

            //FTW中才会有分裂弹幕
            if (Main.getGoodWorld && Projectile.IsOwnedByLocalPlayer())
                for (int i = 0; i < 2; i++)
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Helper.NextVec2Dir() * 10, ModContent.ProjectileType<SmallGelBall>(), Projectile.damage * 2 / 3, 0, Projectile.owner);
        }

        public static void DrawGelBall(Texture2D tex, Vector2 pos, Color drawColor, float ballRotation, float extraMiddleRotation, Vector2 scale
            , bool hasHighlight = true, bool hasOutline = false, Color? outlineColor = null, bool highlightUseRot = false)
        {
            //绘制底层
            Rectangle frameBox = tex.Frame(1, 4, 0, 2);
            Vector2 origin = frameBox.Size() / 2;
            Color c = drawColor * 0.45f;

            Main.spriteBatch.Draw(tex, pos, frameBox, c, ballRotation, origin, scale, 0, 0);

            //绘制中层，自转
            frameBox = tex.Frame(1, 4, 0, 1);
            c = drawColor * 0.75f;

            Main.spriteBatch.Draw(tex, pos, frameBox, c, extraMiddleRotation, origin, scale, 0, 0);

            //绘制高光
            if (hasHighlight)
            {
                frameBox = tex.Frame(1, 4, 0, 0);

                Main.spriteBatch.Draw(tex, pos, frameBox, c, highlightUseRot ? ballRotation : 0, origin, scale, 0, 0);
            }

            //绘制外边缘
            if (hasOutline && outlineColor != null)
            {
                frameBox = tex.Frame(1, 4, 0, 3);

                Main.spriteBatch.Draw(tex, pos, frameBox, outlineColor.Value, ballRotation, origin, scale, 0, 0);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D mainTex = Projectile.GetTexture();
            var pos = Projectile.Center - Main.screenPosition;

            float exRot = Projectile.whoAmI * 0.3f + Main.GlobalTimeWrappedHourly * 2;

            if (Main.zenithWorld)
                lightColor = SlimeEmperor.BlackSlimeColor;

            Vector2 scale = Scale;

            float factor = MathF.Sin(Main.GlobalTimeWrappedHourly);
            Color color = new Color(50, 152 + (int)(100 * factor), 225);
            color *= Projectile.localAI[0] * 0.75f;

            //绘制影子拖尾
            Vector2 toCenter = new(Projectile.width / 2, Projectile.height / 2);

            for (int i = 1; i < 8; i += 2)
                DrawGelBall(mainTex, Projectile.oldPos[i] + toCenter - Main.screenPosition
                    , color * (0.3f - (i * 0.03f)), Projectile.oldRot[i], exRot + i * 1.1f, scale, false);

            //绘制自己
            DrawGelBall(mainTex, pos, lightColor * Projectile.localAI[0]
                , Projectile.rotation, exRot, scale, true, true, color);

            return false;
        }
    }

    public class SmallGelBall : GelBall
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 12;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1200;

            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.netImportant = true;
        }

        public override void AI()
        {
            if (Projectile.localAI[0] < 1)  //膨胀小动画
            {
                Projectile.localAI[0] += 0.1f;
                Projectile.localAI[1] += 0.1f;
                Projectile.localAI[2] += 0.1f;
                if (Projectile.localAI[0] > 1)
                {
                    Projectile.localAI[0] = 1f;
                    Projectile.localAI[1] = 1f;
                    Projectile.localAI[2] = 1f;
                }
            }

            switch ((int)ScaleState)
            {
                default:
                case 0:
                    Projectile.rotation += 0.1f;

                    break;
                case 1:
                    Scale = Vector2.Lerp(Scale, new Vector2(1.6f, 0.65f), 0.3f);
                    if (Scale.X > 1.55f)
                        ScaleState = 2;
                    break;
                case 2:
                    Scale = Vector2.Lerp(Scale, new Vector2(0.75f, 1.3f), 0.3f);
                    if (Scale.Y > 1.25f)
                        ScaleState = 3;
                    break;
                case 3:
                    Scale = Vector2.Lerp(Scale, Vector2.One, 0.2f);
                    if (Math.Abs(Scale.X - 1) < 0.05f)
                    {
                        Scale = Vector2.One;
                        ScaleState = 0;
                    }
                    break;
            }

            Projectile.velocity.Y += 0.1f;
            if (Projectile.velocity.Y > 16)
                Projectile.velocity.Y = 16;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width, Projectile.height), DustID.t_Slime,
                     Helper.NextVec2Dir(0.5f, 1.5f), 150, new Color(78, 136, 255, 80), Main.rand.NextFloat(1.2f, 1.6f));
            }
        }
    }
}
