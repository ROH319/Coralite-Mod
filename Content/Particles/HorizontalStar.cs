﻿using Coralite.Core;
using Coralite.Core.Systems.ParticleSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Coralite.Content.Particles
{
   public class HorizontalStar : ModParticle
   {
      public const int phase_1 = 8;
      public const int phase_2 = 16;
      public const int phase_3 = 24;
      public const int phase_4 = 32;

      public override void OnSpawn(Particle particle)
      {
         particle.frame = new Rectangle(0, 0, 126, 93);
         particle.fadeIn = 0;
         particle.shader = new Terraria.Graphics.Shaders.ArmorShaderData(new Ref<Effect>(Coralite.Instance.Assets.Request<Effect>("Effects/StarsDust", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value), "StarsDustPass");
      }

      public override void Update(Particle particle)
      {
         particle.shader.UseColor(particle.color);

         do
         {
            //因为所有状态时间都相等所以这里就直接简单粗暴的填第一状态的时间了
            //如果有需要改的话请把所有的都改了
            if (particle.fadeIn < phase_1)
            {
               particle.scale *= 1.14f;
               float factor = particle.fadeIn / phase_1;
               particle.shader.UseOpacity(0.65f - factor * 0.25f);
               particle.shader.UseSaturation(1.5f + factor * 0.6f);
               break;
            }

            if (particle.fadeIn < phase_2)
            {
               particle.scale *= 0.86f;
               float factor = (particle.fadeIn - phase_1) / phase_1;
               particle.shader.UseOpacity(0.4f + factor * 0.4f);
               particle.shader.UseSaturation(2.3f - factor * 0.8f);
               break;
            }

            particle.velocity *= 0.96f;
            particle.color *= 0.96f;

            if (particle.fadeIn < phase_3)
            {
               particle.scale *= 1.1f;
               float factor = (particle.fadeIn - phase_2) / phase_1;
               particle.shader.UseOpacity(0.65f - factor * 0.25f);
               particle.shader.UseSaturation(1.5f + factor * 0.6f);
               break;
            }

            particle.scale *= 0.84f;
            float factor2 = (particle.fadeIn - phase_3) / phase_1;
            particle.shader.UseOpacity(0.4f + factor2 * 0.4f);
            particle.shader.UseSaturation(2.3f - factor2 * 0.8f);

         } while (false);

         Lighting.AddLight(particle.center, particle.color.ToVector3());

         particle.fadeIn++;

         if (particle.fadeIn > phase_4)
            particle.active = false;
      }
   }
}