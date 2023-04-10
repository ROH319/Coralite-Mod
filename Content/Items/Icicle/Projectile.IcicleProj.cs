﻿using Coralite.Core;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Coralite.Helpers;

namespace Coralite.Content.Items.Icicle
{
    /// <summary>
    /// ai[0]用于控制弹幕射出时机
    /// </summary>
    public class IcicleProj : ModProjectile
    {
        public override string Texture => AssetDirectory.IcicleProjectiles + Name;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 16;
            Projectile.timeLeft = 1200;
            Projectile.aiStyle = -1;

            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57f;

            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0] -= 1f;
                return;
            }

            Projectile.velocity.Y += 0.02f;
            if (Projectile.velocity.Y > 8f)
                Projectile.velocity.Y = 8f;

        }

        public override bool ShouldUpdatePosition()
        {
            return Projectile.ai[0] < 0.5f;
        }

        public override bool? CanDamage()
        {
            return Projectile.ai[0] < 0.5f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return Projectile.ai[0] < 0.5f;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Ice, -Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f)) * Main.rand.NextFloat(0.2f, 0.5f));
                Dust.NewDustPerfect(Projectile.Center, DustID.SnowBlock, -Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f)) * Main.rand.NextFloat(0.1f, 0.3f));
            }

            Helper.NotOnServer(() =>
            {
                SoundEngine.PlaySound(CoraliteSoundID.CrushedIce_Item27, Projectile.Center);
            });

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frozen, 180);
        }
    }
}