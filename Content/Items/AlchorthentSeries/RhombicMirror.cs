using Coralite.Content.Dusts;
using Coralite.Content.Items.Materials;
using Coralite.Content.Particles;
using Coralite.Content.Tiles.RedJades;
using Coralite.Core;
using Coralite.Core.Configs;
using Coralite.Core.Prefabs.Particles;
using Coralite.Core.Systems.MagikeSystem.Particles;
using Coralite.Helpers;
using InnoVault.GameContent.BaseEntity;
using InnoVault.PRT;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Coralite.Content.Items.AlchorthentSeries
{
    public class RhombicMirror : BaseAlchorthentItem
    {
        public static Color ShineCorruptionColor = new Color(180, 120, 220);
        public static Color CopperGreen = new Color(70, 90, 100);

        public override void SetOtherDefaults()
        {
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.useTime = Item.useAnimation = 30;
            Item.shoot = ModContent.ProjectileType<RhombicMirrorProj>();

            Item.SetWeaponValues(24, 4);
            Item.SetShopValues(Terraria.Enums.ItemRarityColor.Green2, Item.sellPrice(0, 1));

            Item.useTurn = false;
        }

        public override void Summon(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);

            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<RhombicMirrorHeldProj>(), damage, knockback, player.whoAmI, 0);

            //player.AddBuff(ModContent.BuffType<FaintEagleBuff>(), 60);

            Helper.PlayPitched(CoraliteSoundID.Swing_Item1, player.Center);
            //Helper.PlayPitched(CoraliteSoundID.FireBallExplosion_DD2_BetsyFireballImpact, player.Center, pitchAdjust: 0.4f);
        }

        public override void MinionAim(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PRTLoader.NewParticle<TestAlchSymbol>(Main.MouseWorld, Vector2.Zero, ShineCorruptionColor);

            //Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<FaintEagleHeldProj>(), damage, knockback, player.whoAmI, 0);
        }

        public override void SpecialAttack(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<CorruptionMirror>(), damage, knockback, player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 12)
                .AddIngredient<MagicalPowder>(3)
                .AddIngredient(ItemID.VilePowder, 12)
                .AddTile<MagicCraftStation>()
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 12)
                .AddIngredient<MagicalPowder>(3)
                .AddIngredient(ItemID.ViciousPowder, 12)
                .AddTile<MagicCraftStation>()
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.TinBar, 12)
                .AddIngredient<MagicalPowder>(3)
                .AddIngredient(ItemID.VilePowder, 12)
                .AddTile<MagicCraftStation>()
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.TinBar, 12)
                .AddIngredient<MagicalPowder>(3)
                .AddIngredient(ItemID.ViciousPowder, 12)
                .AddTile<MagicCraftStation>()
                .Register();
        }

        public static LineDrawer NewCorruptAlchSymbol()
        {
            Vector2 left = new Vector2(-0.9f, -0.8f);
            Vector2 right = new Vector2(0.8f, -1);

            return new LineDrawer([
                 new LineDrawer.StraightLine(new Vector2(0,-0.9f),new Vector2(0, 1),AlchorthentAssets.OneSideBigLine,1.4f),
                 new LineDrawer.WarpLine(left,30
                    ,f => Helper.TwoHandleBezierEase(f,left,right,new Vector2(-0.7f,0.7f), new Vector2(1,-0.1f))),
                 new LineDrawer.StraightLine(new Vector2(-1.2f, -0.6f), new Vector2(-0.6f, -0.8f),linwWidthScale:0.7f),
                 //对号的两个箭头
                 new LineDrawer.StraightLine(new Vector2(0.5f, -0.7f), new Vector2(0.9f, -1),linwWidthScale:0.7f),
                 new LineDrawer.StraightLine(new Vector2(1f, -0.5f), new Vector2(0.9f, -1),linwWidthScale:0.7f),
                 ]);
        }

        public static LineDrawer NewCopperAlchSymbol()
        {
            return new LineDrawer([
                 new LineDrawer.WarpLine(new Vector2(0,1.004f),36
                    ,f => (MathHelper.PiOver2+f*(MathHelper.TwoPi+0.1f)).ToRotationVector2(),linwWidthScale:1.4f),
                 new LineDrawer.StraightLine(new Vector2(0, 0.9f), new Vector2(0, 2.2f),AlchorthentAssets.OneSideBigLine,linwWidthScale:0.9f),
                 new LineDrawer.StraightLine(new Vector2(-0.6f, 1.6f), new Vector2(0.6f, 1.6f)),
                 ]);
        }
    }

    public class RhombicMirrorBuff : BaseAlchorthentBuff<RhombicMirrorProj>
    {
        public override string Texture => AssetDirectory.MinionBuffs + "DefaultAlchorthentSeries";
    }

    /// <summary>
    /// 召唤和右键时出现的手持弹幕<br></br>
    /// ai0传入类型，0表示召唤，1表示锁定
    /// </summary>
    public class RhombicMirrorHeldProj : BaseHeldProj
    {
        public override string Texture => AssetDirectory.AlchorthentSeriesItems + "RhombicMirror";

        public ref float State => ref Projectile.ai[0];
        public ref float Timer => ref Projectile.ai[1];

        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = Projectile.height = 16;
            Projectile.hide = true;
        }

        public override bool? CanDamage() => false;
        public override bool? CanCutTiles() => false;
        public override bool ShouldUpdatePosition() => false;

        public override void AI()
        {
            if (Owner.dead || Owner.HeldItem.type != ModContent.ItemType<RhombicMirror>())
            {
                Projectile.Kill();
                return;
            }

            SetHeld();

            if (State == 0)
                Summon();
            else
                AimTarget();
        }

        /// <summary>
        /// 光效展开后召唤物在人物背后缓慢旋转出现
        /// </summary>
        public void Summon()
        {
            Projectile.Center = Owner.Center + new Vector2(Owner.direction * 16.5f, -4 + Owner.gfxOffY);
            Owner.itemTime = Owner.itemAnimation = 2;

            if (Timer == 0)//生成光效
            {
                var p = PRTLoader.NewParticle<RhombicMirrorSummonParticle>(Projectile.Center, Vector2.Zero);
                p.OwnerProjIndex = Projectile.whoAmI;
            }

            if (Timer > 45)
            {
                Projectile.Kill();
            }

            Timer++;
        }

        public void AimTarget()
        {

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.QuickDraw(lightColor, 0, Owner.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            return false;
        }
    }

    /// <summary>
    /// 菱花镜召唤物，ai0控制是否强化形态
    /// </summary>
    [VaultLoaden(AssetDirectory.AlchorthentSeriesItems)]
    public class RhombicMirrorProj : BaseAlchorthentMinion<RhombicMirrorBuff>
    {
        /*
         * 神秘身体部分贴图
         * 1~17帧为伴随着攻击变得生锈，18~37帧为特殊攻击清除生锈
         */
        /// <summary> 下面的头，上面的尾巴，右上后腿，左上后腿 </summary>
        public static ATex RhombicMirrorProjPart1 { get; set; }
        /// <summary> 右下前腿，左下前腿 </summary>
        public static ATex RhombicMirrorProjPart2 { get; set; }

        public ref float Recorder => ref Projectile.ai[1];
        public ref float Recorder2 => ref Projectile.ai[2];
        public ref float Recorder3 => ref Projectile.localAI[1];
        public ref float Recorder4 => ref Projectile.localAI[2];

        /// <summary>
        /// 攻击次数
        /// </summary>
        public short attackCount;
        public float xScaleDirection = 1;
        public float xScale = 1;
        /// <summary> 控制身体部件距离中心点的长度 </summary>
        public float bodyPartLength = 0;
        /// <summary> 是否绘制身体部件 </summary>
        public bool canDrawBodyPart = false;
        public float bodyPartRotation;
        public float bodyPartExtraRotation;
        public float alpha = 0;

        /// <summary>
        /// 攻击状态
        /// </summary>
        public AttackTypes Corrupted { get; set; }

        const int totalFrameY = 37;
        const float Scale = 0.7f;

        const int TeleportDistance = 2000;


        /// <summary>
        /// 攻击状态，决定攻击方式
        /// </summary>
        public enum AttackTypes:byte
        {
            /// <summary> 正常状态，攻击时增加腐化值，一定次数后进入生锈形态 </summary>
            Clear,
            /// <summary> 生锈形态，攻击力减弱，可以被腐化镜子检测到 </summary>
            Corrupted,
            /// <summary> 除锈状态，发动一次强力攻击，攻击后回复正常状态 </summary>
            BreakCorrupt
        }

        private enum AIStates : byte
        {
            /// <summary> 刚召唤出来 </summary>
            OnSummon,
            /// <summary> 飞回玩家的过程 </summary>
            BackToOwner,
            /// <summary> 在玩家身边 </summary>
            Idle,
            /// <summary> 特殊待机动作1 </summary>
            IdleMove1,
            /// <summary> 射光束 </summary>
            Shoot,
            /// <summary> 经过一定攻击后变得腐化 </summary>
            Corrupt,
            /// <summary> 腐蚀光束 </summary>
            CorruptedShoot,
        }

        public override void SetOtherDefault()
        {
            Projectile.tileCollide = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.width = Projectile.height = 46;
            Projectile.scale = Scale;
            Projectile.decidesManualFallThrough = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override bool? CanDamage() => false;

        #region AI

        public override void Initialize()
        {
            
        }
         
        public override void AIMoves()
        {
            switch (State)
            {
                default:
                    break;
                case (byte)AIStates.OnSummon:
                    OnSummon();
                    break;
                case (byte)AIStates.BackToOwner:
                    BackToOwner();
                    break;
                case (byte)AIStates.Idle:
                    //寻敌，找到敌怪就进入攻击状态
                    if (Timer > 20 && Main.rand.NextBool(12))
                        if (FindEnemy())
                        {
                            SwitchState(AIStates.Shoot);
                            break;
                        }

                    Idle();
                    break;
                case (byte)AIStates.IdleMove1:
                    if (Timer > 20 && Main.rand.NextBool(12))
                        if (FindEnemy())
                        {
                            SwitchState(AIStates.Shoot);
                            break;
                        }

                    break;
                case (byte)AIStates.Shoot:
                    if (!Target.GetNPCOwner(out NPC owner, () => Target = -1))
                    {
                        SwitchState(AIStates.Shoot);
                        break;
                    }

                    break;
            }

            Timer++;

            //float length = MathF.Abs(Main.MouseWorld.X - Projectile.Center.X) ;

            //if (length > 16 * 5)
            //{
            //    xScale =1- Math.Clamp((length -  16 * 5) / (16 * 10), 0, 0.4f);
            //}
            //else
            //    xScale = 1;
        }

        public void OnSummon()
        {
            const float startRot = MathHelper.TwoPi + MathHelper.PiOver2;
            const float startScale = 0.4f;

            if (Timer == 0)
            {
                Projectile.scale = startScale;
                Projectile.rotation = startRot;
            }

            /*
             * 在玩家身后旋转着出现
             * 并且逐渐变大
             */

            float factor = Timer / 45f;

            Projectile.Center = Owner.MountedCenter + new Vector2(0, Owner.gfxOffY)
                + (Owner.direction > 0 ? (MathHelper.Pi + 0.95f) : -0.95f).ToRotationVector2() * (18+Helper.SqrtEase(factor) * 60);

            alpha = Helper.X2Ease(factor);
            bodyPartRotation = MathHelper.PiOver2;
            Projectile.rotation = startRot * (1 - Helper.BezierEase(factor));
            Projectile.scale = Helper.Lerp(startScale, Scale, factor);

            if (Timer > 45)
            {
                Projectile.velocity = (Projectile.Center - Owner.Center).SafeNormalize(Vector2.Zero) * 7;
                SwitchState(AIStates.BackToOwner);
            }
        }

        public void BackToOwner()
        {
            Helper.GetMyGroupIndexAndFillBlackList(Projectile, out int index, out int total);
            Vector2 aimPos = GetIdlePos(index, total);

            /*
             * 旋转后朝向目标位置移动
             * 
             * 记录与目标点位置，如果未能缩短距离那么就停下重新选取方向
             * 速度会随着玩家速度增加而增加
             */

            Projectile.tileCollide = false;
            float distanceToAimPos = Vector2.Distance(aimPos, Projectile.Center);

            if (distanceToAimPos > TeleportDistance || Timer > 60 * 16)
            {
                Teleport(aimPos);
                SwitchState(AIStates.Idle);

                return;
            }

            int dir = aimPos.X > Projectile.Center.X ? 1 : -1;

            switch (Recorder)
            {
                default:
                case 0://旋转
                    {
                        Projectile.velocity *= 0.7f;

                        const int rotTime = 30;
                        float factor = Recorder2 / rotTime;

                        canDrawBodyPart = true;
                        bodyPartLength = Helper.HeavyEase(factor) * 24;
                        bodyPartRotation = bodyPartRotation.AngleLerp((aimPos - Projectile.Center).ToRotation(), 0.2f);


                        Projectile.rotation += dir * factor * 0.2f;
                        Recorder2++;
                        if (Recorder2 > rotTime)
                        {
                            Recorder++;
                            Recorder2 = 0;
                            Recorder3 = (aimPos - Projectile.Center).Length();//记录距离

                            Projectile.velocity = (aimPos - Projectile.Center).SafeNormalize(Vector2.Zero) * 20;
                        }
                    }
                    break;
                case 1://向目标运动
                    {
                        const int resetTime= 60 * 3;
                        Recorder2++;

                        float speed = Owner.velocity.Length() + 5;
                        if (speed < 13)
                            speed = 13;

                        bodyPartExtraRotation = MathF.Sin(Recorder2 * 0.2f) * 0.4f;
                        bodyPartRotation = bodyPartRotation.AngleLerp(Projectile.velocity.ToRotation(), 0.25f);
                        Projectile.ChaseGradually(aimPos, speed, 29, 30);
                        if (Recorder2 > resetTime - 30)
                        {
                            bodyPartLength *= 0.97f;
                            Projectile.rotation += dir * 0.2f * (1 - (Recorder2 - resetTime) / 30);
                        }
                        else
                            Projectile.rotation += dir * 0.2f;

                        float distance = Vector2.Distance(Projectile.Center, aimPos);
                        if (distance > Recorder3 + 16 * 5
                            || Recorder2 > resetTime)//被甩开5格以上就重新旋转然后追踪
                        {
                            Recorder = 0;
                            Recorder2 = 0;
                            Recorder3 = 0;
                        }


                        if (distance < speed +5)
                            SwitchState(AIStates.Idle);

                        //bodyPartRotation = MathHelper.PiOver2;
                        //Projectile.rotation = 0;
                        //bodyPartLength = 28;
                        //Projectile.velocity = Vector2.Zero;

                        //if (Projectile.frame < 17)
                        //{
                        //    Projectile.UpdateFrameNormally(4, 22);
                        //}
                    }
                    break;
            }
        }

        public void Idle()
        {
            
            Helper.GetMyGroupIndexAndFillBlackList(Projectile, out int index, out int total);
            Vector2 aimPos = GetIdlePos(index, total) + new Vector2(0, Owner.gfxOffY);

            float distanceToAimPos = Vector2.Distance(aimPos, Projectile.Center);

            if (distanceToAimPos > TeleportDistance)
            {
                Teleport(aimPos);
                SwitchState(AIStates.Idle);

                return;
            }

            switch (Recorder)
            {
                default:
                case 0://距离近的时候，向目标点缓动
                    {
                        if (distanceToAimPos > 45 + Owner.velocity.Length()&&Recorder2>45)
                        {
                            Recorder = 1;
                            Recorder2 = 0;
                            Projectile.velocity = (Projectile.whoAmI * MathHelper.Pi / 3).ToRotationVector2() * 4;
                            return;
                        }

                        //根据计时器和自身索引调整缓动速率，做差异化，体现机械感
                        float lerpF = (Recorder2+Projectile.whoAmI*5) % 30 < 15 ? 0.4f : 0.2f;
                        Projectile.Center = Vector2.SmoothStep(Projectile.Center, aimPos, lerpF);
                        Projectile.velocity *= 0.5f;

                        //旋转，通过转过头再转回来加上停顿来搞点机械感
                        Recorder2++;
                        if (Recorder2 < 30)
                        {
                            Projectile.rotation = Projectile.rotation.AngleLerp(0, 0.1f);
                            bodyPartLength *= 0.9f;
                        }
                        else if (Recorder2 == 30)
                        {
                            canDrawBodyPart = false;
                            Projectile.rotation = 0;
                        }
                        else
                        {
                            float realTimer = (Recorder2 - 30) % 120;
                            if (realTimer < 20)//旋转
                            {
                                Projectile.rotation += (MathHelper.Pi / 3 + 0.3f) / 20;
                            }
                            else if (realTimer < 40)
                            {

                            }
                            else if (realTimer < 50)
                                Projectile.rotation -= 0.3f / 10;
                        }
                    }
                    break;
                case 1://追踪
                    {
                        float speed = Owner.velocity.Length() + 10;
                        if (speed < 13)
                            speed = 13;

                        Recorder2++;
                        if (Recorder2 > 60)
                            speed += (Recorder2 - 60) / 4;

                        int dir = aimPos.X > Projectile.Center.X ? 1 : -1;

                        Projectile.ChaseGradually(aimPos, speed, 29, 30);
                        if (Recorder2 % 10 == 0)//每隔一段时间纠正速度方向
                            Projectile.velocity = Projectile.velocity.ToRotation().AngleLerp((aimPos - Projectile.Center).ToRotation(), 0.4f).ToRotationVector2() * Projectile.velocity.Length();

                        Projectile.rotation += dir * Projectile.velocity.Length() / 75;

                        if (distanceToAimPos < 30)
                        {
                            Recorder = 0;
                            Recorder2 = 0;
                            return;
                        }
                    }
                    break;
            }

        }

        /// <summary>
        /// 传送到目标位置，生成炼金术符号
        /// </summary>
        /// <param name="teleportPos"></param>
        public void Teleport(Vector2 teleportPos)
        {
            Projectile.velocity *= 0;
            Projectile.rotation = 0;
            Projectile.Center = teleportPos;
            Recorder = Recorder2 = Recorder3 = 0;

            //PRTLoader.NewParticle<AlchSymbolFire>(Projectile.Center, Vector2.Zero, new Color(203, 66, 66));
        }

        public override Vector2 GetIdlePos(int selfIndex, int totalCount)
        {
            Vector2 basePos = Owner.MountedCenter + new Vector2(0, -16 * 4);
            if (selfIndex == 0)//第一个直接到目标位置
                return basePos;

            if (selfIndex <= 3)//第2~7个呈六边形环绕
            {
                return basePos + ((selfIndex - 1) * MathHelper.TwoPi / 3 - MathHelper.PiOver2).ToRotationVector2() * 42;
            }

            if (selfIndex <= 6)//第2~7个呈六边形环绕
            {
                return basePos + ((selfIndex - 4) * MathHelper.TwoPi / 3 - MathHelper.PiOver2 + MathHelper.Pi / 3).ToRotationVector2() * 42;
            }

            //其余的圆圈形环绕
            int restCount = totalCount - 6;
            float length = 70 + (totalCount - 7) * 15;
            return basePos + ((selfIndex - 7) * MathHelper.TwoPi / restCount - MathHelper.PiOver2).ToRotationVector2() * length;
        }

        private void SwitchState(AIStates targetState)
        {
            State = (byte)targetState;

            Recorder = 0;
            Recorder2 = 0;
            Recorder3 = 0;

            Timer = 0;
            alpha = 1;
        }

        #endregion

        #region Draw

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 dir = (Projectile.rotation + (xScaleDirection > 0 ? 0 : MathHelper.Pi)).ToRotationVector2();
            float xScaleFactor = Math.Clamp(1 - (xScale - 0.5f) / 0.5f, 0, 1);
            dir *= xScaleFactor;
            //仅在刚生成的时候使用的透明度
            lightColor *= alpha;

            if (canDrawBodyPart)
                DrawBodyParts(lightColor, xScaleFactor, dir);

            DrawSelf(lightColor, dir);
            return false;
        }

        public void DrawBodyParts(Color lightColor, float xScaleFactor, Vector2 dir)
        {
            const float PiOver3 = MathHelper.Pi / 3;
            Color darkColor = lightColor * 0.7f;
            darkColor.A = lightColor.A;

            Vector2 offset = dir * 2;
            Texture2D part1 = RhombicMirrorProjPart1.Value;

            //绘制头
            DrawBodyPart(part1, 0, 4, bodyPartRotation.ToRotationVector2() * bodyPartLength, offset, darkColor,lightColor);

            //绘制以把
            DrawBodyPart(part1, 1, 4, (bodyPartRotation - MathHelper.Pi).ToRotationVector2() * bodyPartLength, offset, darkColor, lightColor);

            float angleOffset = 0.3f + xScaleFactor * MathHelper.PiOver4 / 2;

            //绘制右边后腿
            DrawBodyPart(part1, 2, 4, (bodyPartRotation - PiOver3 * 2 - angleOffset / 2).ToRotationVector2() * bodyPartLength, offset, darkColor, lightColor, bodyPartExtraRotation / 2);

            //绘制左边后腿
            DrawBodyPart(part1, 3, 4, (bodyPartRotation + PiOver3 * 2 + angleOffset / 2).ToRotationVector2() * bodyPartLength, offset, darkColor, lightColor, -bodyPartExtraRotation / 2);

            Texture2D part2 = RhombicMirrorProjPart2.Value;

            //绘制右边前腿
            DrawBodyPart(part2, 0, 2, (bodyPartRotation - PiOver3 + angleOffset).ToRotationVector2() * bodyPartLength, offset, darkColor, lightColor, bodyPartExtraRotation);

            //绘制左边前腿
            DrawBodyPart(part2, 1, 2, (bodyPartRotation + PiOver3  - angleOffset).ToRotationVector2() * bodyPartLength, offset, darkColor, lightColor, -bodyPartExtraRotation);
        }

        public void DrawBodyPart(Texture2D tex, int xFrame, int totalXFrame, Vector2 posOffset,Vector2 offset, Color darkColor, Color lightColor,float exRot=0)
        {
            float rot = bodyPartRotation - MathHelper.PiOver2+exRot;
            DrawLayer(tex, xFrame, totalXFrame, posOffset, darkColor, rot);
            DrawLayer(tex, xFrame, totalXFrame, posOffset + offset, darkColor, rot);
            DrawLayer(tex, xFrame, totalXFrame, posOffset + offset*2, lightColor, rot);
            DrawLayer(tex, xFrame, totalXFrame, posOffset + offset*3, lightColor, rot);
        }

        public void DrawSelf(Color lightColor, Vector2 dir)
        {
            Color darkColor = lightColor * 0.7f;
            darkColor.A = lightColor.A;

            Vector2 offset = dir ;
            Texture2D tex = Projectile.GetTexture();

            //绘制底层
            DrawLayer(tex, 0, 2, Vector2.Zero, darkColor);
            DrawLayer(tex, 0, 2, offset, darkColor);
            DrawLayer(tex, 0, 2, offset * 2, darkColor);
            DrawLayer(tex, 0, 2, offset * 3, lightColor);
            DrawLayer(tex, 0, 2, offset * 4, lightColor);

            //绘制顶层
            DrawLayer(tex, 1, 2, offset * 5, darkColor);
            DrawLayer(tex, 1, 2, offset * 6, darkColor);
            DrawLayer(tex, 1, 2, offset * 7, lightColor);
        }

        /// <summary>
        /// 绘制一层
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="xFrame"></param>
        /// <param name="totalXFrame"></param>
        /// <param name="posOffset"></param>
        /// <param name="color"></param>
        public void DrawLayer(Texture2D tex, int xFrame, int totalXFrame, Vector2 posOffset, Color color, float? rotation = null)
        {
            var frameBox = tex.Frame(totalXFrame, totalFrameY, xFrame, Projectile.frame);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + posOffset, frameBox, color, rotation ?? Projectile.rotation, frameBox.Size() / 2, new Vector2(xScale, 1) * Projectile.scale, 0, 0);
        }

        #endregion
    }

    public class CorruptionMirror : ModProjectile
    {
        public override string Texture => AssetDirectory.AlchorthentSeriesItems + Name;

        public ref float State => ref Projectile.ai[0];
        public ref float Timer => ref Projectile.ai[1];
        public ref float Recorder => ref Projectile.ai[2];
        public ref float Recorder2 => ref Projectile.localAI[0];
        public int HitCount;
        public Player Owner => Main.player[Projectile.owner];

        private LineDrawer CorruptionEffect;

        const int channelTime = 40;

        public override void Load()
        {
            if (Main.dedServ)
                return;

            this.LoadGore(3);
        }

        public override bool? CanDamage()
        {
            if (State == 1)
                return null;

            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return Recorder == 0;
        }

        public override void SetStaticDefaults()
        {
            Projectile.QuickTrailSets(Helper.TrailingMode.RecordAll, 10);
        }

        public override void SetDefaults()
        {
            Projectile.penetrate = -1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
            Projectile.width = Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            /*
             * 先在头上蓄力，生成动画和腐化的符号
             * 然后丢出，命中后产生切割音效
             */
            switch (State)
            {
                default:
                case 0:
                    Channel();
                    UpdateCorruptionEffect();
                    break;
                case 1://飞出
                    {
                        if (Vector2.Distance(Projectile.Center, Owner.Center) > 1600)
                            SwitchToBreak();

                        Shoot();
                        if (Recorder > 0)
                            Recorder--;
                    }
                    break;
                case 2:
                    {
                        Projectile.Kill();
                    }
                    break;
            }
        }

        private void Channel()
        {
            Owner.heldProj = Projectile.whoAmI;
            Owner.itemTime = Owner.itemAnimation = 2;
            Owner.itemRotation = -MathHelper.PiOver2 + (Owner.direction > 0 ? 0 : MathHelper.Pi);
            Owner.direction = Main.MouseWorld.X > Owner.Center.X ? 1 : -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;

            //一开始举起在玩家头上，之后来到中心点，再之后伸到身前
            Vector2 exOffset = new Vector2(0, -45);

            if (Timer < channelTime)
            {
                if (Timer == 0)
                    Helper.PlayPitched("AlchSeries/FaintEagleExplosion", 0.02f, -0.2f, Projectile.Center);
            }
            else if (Timer == channelTime)
            {
                Helper.PlayPitched("AlchSeries/CorruptionMirrorChargeComplete", 0.08f, 1, Projectile.Center);
            }
            else if (Timer < channelTime + 16)
            {
                float f = (Timer - channelTime) / 16;
                exOffset = new Vector2(0, -45 + 35 * Helper.HeavyEase(f));
            }
            else
            {
                float f = (Timer - channelTime - 16) / 6;
                Owner.itemRotation = (-MathHelper.PiOver2).AngleLerp((Main.MouseWorld - Projectile.Center).ToRotation(), f) + (Owner.direction > 0 ? 0 : MathHelper.Pi);

                if (Projectile.IsOwnedByLocalPlayer())
                    exOffset = Vector2.Lerp(new Vector2(0, -10), (Main.MouseWorld - Projectile.Center).SafeNormalize(Vector2.Zero) * 24, Helper.HeavyEase(f));
                Projectile.netUpdate = true;
            }

            Projectile.Center = Owner.Center + new Vector2(0, Owner.gfxOffY) + exOffset;

            Projectile.rotation = Helper.Lerp(MathHelper.TwoPi, 0, Helper.BezierEase(Timer / 60));

            Timer++;
            if (Projectile.frame < 19)
                Projectile.UpdateFrameNormally(2, 20);

            if (Timer > channelTime + 16 + 6)//完成蓄力，丢出去
            {
                State = 1;
                Timer = 0;
                Projectile.hide = false;
                Projectile.tileCollide = true;
                Projectile.InitOldPosCache(10, false);
                Projectile.InitOldRotCache(10);

                if (Projectile.IsOwnedByLocalPlayer())
                {
                    Projectile.velocity = (Main.MouseWorld - Projectile.Center).SafeNormalize(Vector2.Zero) * 8;
                    Projectile.extraUpdates = 1;
                }
            }
        }

        public void Shoot()
        {
            Timer++;
            if (Recorder == 0)
            {
                Projectile.rotation -= MathF.Sign(Projectile.velocity.X) * Projectile.velocity.LengthSquared() / 35;
            }

            if (Timer % 2 == 0 && Main.rand.NextBool())
                Projectile.SpawnTrailDust(ModContent.DustType<PixelPoint>(), Main.rand.NextFloat(-0.2f, 0.2f), newColor: RhombicMirror.ShineCorruptionColor*0.75f, Scale: Main.rand.NextFloat(1, 2));
        }

        /// <summary>
        /// 检测是否有腐化镜子在附近，如果有那么立马碎裂
        /// </summary> 
        /// <returns></returns>
        public bool CheckCorruptedMirror()
        {
            int targetType = ModContent.ProjectileType<RhombicMirrorProj>();
            foreach (var proj in Main.ActiveProjectiles)
                if (proj.owner == Projectile.owner && proj.type == targetType&&Projectile.Distance(proj.Center)<800)
                    if ((proj.ModProjectile as RhombicMirrorProj).Corrupted == RhombicMirrorProj.AttackTypes.Corrupted)
                        return true;

            return false;
        }

        public void SwitchToBreak()
        {
            State = 2;
            Timer = 0;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Recorder == 0)
                Recorder = 5 * Projectile.MaxUpdates;

            HitVisualEffect(target);
            HitCount++;
        }

        public void HitVisualEffect(NPC target)
        {
            Helper.PlayPitched("Misc/BloodySlash2", 0.03f, -0.6f, Projectile.Center);

            if (VisualEffectSystem.HitEffect_SpecialParticles)
            {
                //菱形粒子
                var p2 = PRTLoader.NewParticle<MagikeLozengeParticleSPA>(Projectile.Center, Vector2.Zero, RhombicMirror.ShineCorruptionColor, 0.4f);

                float normalRot = (target.Center - Projectile.Center).ToRotation() + Main.rand.NextFloat(-0.3f, 0.3f);
                p2.Rotation = normalRot;
                p2.XScale = 0.9f;

                normalRot += MathHelper.PiOver2;
                //两侧亮线
                Vector2 dir = normalRot.ToRotationVector2();
                for (int i = -3; i < 3; i++)
                {
                    PRTLoader.NewParticle<SpeedLine>(Projectile.Center, (i < 0 ? -1 : 1) * dir.RotateByRandom(-0.1f, 0.1f) * Main.rand.NextFloat(2, 6), Main.rand.NextFromList(RhombicMirror.CopperGreen, RhombicMirror.ShineCorruptionColor), Scale: Main.rand.NextFloat(0.2f, 0.3f));
                }
            }

            if (VisualEffectSystem.HitEffect_Dusts)
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 dir2 = Helper.NextVec2Dir();
                    PRTLoader.NewParticle<CorruptionMirrorParticle>(Projectile.Center + dir2 * Main.rand.NextFloat(6, 14), dir2 * Main.rand.NextFloat(0.3f, 1.4f), Color.White);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (MathF.Abs(Projectile.velocity.X) < MathF.Abs(oldVelocity.X))
                Projectile.velocity.X = -oldVelocity.X;
            if (MathF.Abs(Projectile.velocity.Y) < MathF.Abs(oldVelocity.Y))
                Projectile.velocity.Y = -oldVelocity.Y;

            Recorder2++;
            if (Recorder2 > 6)
                SwitchToBreak();

            return false;
        }

        public void UpdateCorruptionEffect()
        {
            if (CorruptionEffect == null)
            {
                CorruptionEffect = RhombicMirror.NewCorruptAlchSymbol();
                CorruptionEffect.SetLineWidth(24);
            }

            if (Timer > channelTime)
                return;

            if (Timer < channelTime / 3)
            {
                float factor = Timer / (channelTime / 3);
                CorruptionEffect.SetScale(35 * factor);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (State == 0 && CorruptionEffect != null)
                DrawCorruptEffect();

            if (State == 1)//绘制残影
            {
                Projectile.DrawFramedShadowTrails(RhombicMirror.ShineCorruptionColor, 0.5f, 0.4f / 10, 1, 10, 2, Projectile.scale * 0.9f, new Rectangle(0, 19, 1, 20), 0);
            }

            Projectile.QuickFrameDraw(new Rectangle(0, Projectile.frame, 1, 20), lightColor, 0);

            return false;
        }

        private void DrawCorruptEffect()
        {
            float factor=1;
            Color c=Color.Transparent;

            if (Timer < channelTime / 3)
            {
                factor = Timer / (channelTime / 3);
                c = Color.Lerp(Color.Transparent, RhombicMirror.CopperGreen, factor);
            }
            else if (Timer < channelTime * 2 / 3)
            {
                factor = (Timer - channelTime / 3) / (channelTime / 3);
                c = Color.Lerp(RhombicMirror.CopperGreen, RhombicMirror.ShineCorruptionColor, factor);
            }
            else if (Timer < channelTime)
            {
                factor = (Timer - channelTime * 2 / 3) / (channelTime / 3);
                c = Color.Lerp(RhombicMirror.ShineCorruptionColor, Color.Transparent, factor);
            }

            float f = 1;
            if (Timer< channelTime / 2)
                f = Helper.BezierEase(Timer / (channelTime / 2));

            RhombicMirrorProj.DrawLine(shader =>
                {
                    shader.CurrentTechnique.Passes["MyNamePass"].Apply();
                    CorruptionEffect.Draw(Projectile.Center);
                }, CoraliteAssets.Laser.TwistLaser.Value
                   , (int)Main.timeForVisualEffects * 0.02f, 4, f, c, 0.2f, 0.5f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }

    public class RhombicMirrorSummonParticle : RhombicMirrorLaserParticle
    {
        public override void SetProperty()
        {
            base.SetProperty();
            LaserWidth = 8;
            LaserAngleOffset = 0.4f;
            LaserLength = 30;
        }

        public override void AI()
        {
            if (!OwnerProjIndex.GetProjectileOwner(out Projectile owner))
            {
                active = false;
                return;
            }

            Player p = Main.player[owner.owner];

            Position = owner.Center;
            Rotation = -MathHelper.PiOver2 + (p.direction > 0 ? -1 : 1) * 0.9f;

            Opacity++;
            if (Opacity < 15)
            {
                float f = Helper.HeavyEase(Opacity / 15);
                LaserAngleOffset = Helper.Lerp(0.4f, -0.4f, f);
                LaserLength = Helper.Lerp(30, 90, f);
                Color = Color.Lerp(Color.Transparent, new Color(180, 120, 220), f);
            }
            else if (Opacity < 45)
            {
                float f = Helper.X2Ease((Opacity - 15) / 30);
                LaserLength = Helper.Lerp(90, 60, f);
                Color = Color.Lerp(new Color(180, 120, 220), Color.Transparent, f);
            }
            else
            {
                active = false;
            }
        }
    }

    public class CorruptionMirrorParticle() : BaseFrameParticle(5, 8, 2, randRot: true)
    {
        public override string Texture => AssetDirectory.AlchorthentSeriesItems + Name;

        public override Color GetColor() => Color;
    }

    public class CorruptionMirrorRotParticle() : BaseFrameParticle(1, 8, 1, randRot: true)
    {
        public override string Texture => AssetDirectory.AlchorthentSeriesItems + Name;

        public override void Follow(Projectile proj)
        {
            Position = proj.Center;
        }

        public override Color GetColor()
        {
            return Color;
        }
    }

    /// <summary>
    /// 光束粒子，随机角度
    /// </summary>
    public class LightShotParticle : Particle
    {
        public override string Texture => AssetDirectory.Trails + "MeteorSPA";

        public int OwnerIndex;
        public int maxTime;

        public override void SetProperty()
        {
            PRTDrawMode = PRTDrawModeEnum.AlphaBlend;
        }

        public override void AI()
        {
            if (OwnerIndex.GetProjectileOwner(out Projectile owner))
            {
                if (Opacity<maxTime)
                    Opacity++;


                return;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch)
        {

            return false;
        }
    }

    /// <summary>
    /// 可以变成扇形的激光粒子
    /// </summary>
    public abstract class RhombicMirrorLaserParticle : Particle
    {
        public override string Texture => AssetDirectory.AlchorthentSeriesItems+ "EdgeSPA2";

        public int OwnerProjIndex;

        /// <summary>
        /// 激光长度
        /// </summary>
        public float LaserLength;
        /// <summary>
        /// 激光的扇形张角
        /// </summary>
        public float LaserAngleOffset;
        /// <summary>
        /// 激光宽度，建议不变
        /// </summary>
        public float LaserWidth;

        public override void SetProperty()
        {
            PRTDrawMode = PRTDrawModeEnum.AlphaBlend;
        }

        public virtual Color GetColor(float f)
        {
            return Color;
        }

        public override bool PreDraw(SpriteBatch spriteBatch)
        {
            CoraliteSystem.InitBars();

            Texture2D Texture = TexValue;

            Vector2 pos = Position - Main.screenPosition;
            Vector2 normal = (Rotation + MathHelper.PiOver2).ToRotationVector2();
            pos -= normal * LaserWidth;
            Vector2 dir = Rotation.ToRotationVector2();

            for (int i = 0; i <= 24; i++)
            {
                float factor = (float)i / 24;

                Vector2 Top = pos + normal * LaserWidth * 2f * factor;
                Vector2 Bottom = Top + dir.RotatedBy(Helper.Lerp(LaserAngleOffset, -LaserAngleOffset, factor)) * LaserLength;
                CoraliteSystem.Vertexes.Add(new(Top, Color, new Vector3(1, factor, 0)));
                CoraliteSystem.Vertexes.Add(new(Bottom, Color, new Vector3(0, factor, 0)));
            }

            Main.graphics.GraphicsDevice.Textures[0] = Texture;
            Main.graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            var arr = CoraliteSystem.Vertexes.ToArray();
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, arr, 0, CoraliteSystem.Vertexes.Count - 2);

            Main.graphics.GraphicsDevice.BlendState = BlendState.Additive;
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, arr, 0, CoraliteSystem.Vertexes.Count - 2);
            Main.graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            return false;
        }
    }
}
