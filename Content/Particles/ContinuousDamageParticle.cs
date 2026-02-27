using Coralite.Core;
using Coralite.Helpers;
using InnoVault.PRT;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace Coralite.Content.Particles
{
    public class ContinuousDamageParticle : Particle
    {
        public override string Texture => AssetDirectory.Blank;

        public float aimRot;
        public int damage;
        public int waitTime;

        public Vector2 offset;
        public Func<Vector2> FollowCenter;
        public Action Release;

        public override bool ShouldUpdatePosition() => Opacity < 0;
        /// <summary>
        /// 结算伤害跳字
        /// </summary>
        public void Settlement()
        {
            Opacity = 1;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="damage"></param>
        public void AddDamage(int damage)
        {
            if (Opacity>0)
                Opacity = waitTime;

            this.damage += damage;
            aimRot = Main.rand.NextFloat(-0.3f, 0.3f);
            if (Scale < 1.5f)
            {
                Scale += 0.04f;
            }
        }

        public override void AI()
        {
            Opacity--;
            Rotation = Rotation.AngleLerp(aimRot, 0.02f);
            if (Opacity > 0)
            {
                Velocity *= 0.9f;
                offset += Velocity;
                Position = FollowCenter() + offset;
            }
            else if (Opacity == 0)//结束，释放掉
            {
                Release();
            }
            else if (Opacity < -30)//缩小
            {
                Scale *= 0.95f;
            }
            else
                active = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch)
        {
            DynamicSpriteFont value = FontAssets.MouseText.Value;
            Vector2 vector = value.MeasureString(damage.ToString());
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, value, damage.ToString(), Position, Color, Rotation, new Vector2(0.5f, 0.5f) * vector, Vector2.One * Scale, -1f, 1.5f);

            return false;
        }

        public static ContinuousDamageParticle Spawn(Vector2 pos, int startDamage, int waitTime, Func<Vector2> FollowCenter, Action Release, Color c)
        {
            if (VaultUtils.isServer)
                return null;

            var p = PRTLoader.NewParticle<ContinuousDamageParticle>(pos, new Vector2(0, -Main.rand.NextFloat(1, 4)).RotateByRandom(-0.2f, 0.2f), c);

            p.offset = pos - FollowCenter();
            p.FollowCenter = FollowCenter;
            p.Release = Release;
            p.damage = startDamage;
            p.waitTime = waitTime;
            p.Opacity = waitTime;
            p.aimRot = Main.rand.NextFloat(-0.3f, 0.3f);
            return p;
        }
    }
}
