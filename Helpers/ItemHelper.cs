using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Coralite.Helpers
{
    public partial class Helper
    {
        /// <summary>
        /// 此物品是否为工具（镐，斧，锤）
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsTool(this Item item)
            => item.pick > 0 || item.axe > 0 || item.hammer > 0;

        /// <summary>
        /// 默认设置：镐子
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="damage"></param>
        /// <param name="useTime"></param>
        /// <param name="knockback"></param>
        /// <param name="pick">镐力</param>
        public static void DefaultToPickaxe(this Item Item, int damage, int useTime, float knockback, int pick, int? useAnimation = null)
        {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = damage;
            Item.useTime = useTime;
            Item.useAnimation = useAnimation ?? useTime;
            Item.knockBack = knockback;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.pick = pick;
        }

        /// <summary>
        /// 默认设置：斧头
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="damage"></param>
        /// <param name="useTime"></param>
        /// <param name="knockback"></param>
        /// <param name="axe">斧力，请输入游戏内实际看到的斧力，传入后在内部会除以5</param>
        public static void DefaultToAxe(this Item Item, int damage, int useTime, float knockback, int axe, int? useAnimation = null)
        {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = damage;
            Item.useTime = useTime;
            Item.useAnimation = useAnimation ?? useTime;
            Item.knockBack = knockback;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.axe = axe / 5;
        }

        /// <summary>
        /// 默认设置：锤子
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="damage"></param>
        /// <param name="useTime"></param>
        /// <param name="knockback"></param>
        /// <param name="hammer">镐力</param>
        public static void DefaultToHammer(this Item Item, int damage, int useTime, float knockback, int hammer, int? useAnimation = null)
        {
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = damage;
            Item.useTime = useTime;
            Item.useAnimation = useAnimation ?? useTime;
            Item.knockBack = knockback;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.hammer = hammer;
        }

        /// <summary>
        /// 生成被物块破坏所产生的物品，带有同步功能
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public static int SpawnItemTileBreakNet(Point16 origin, int itemType)
        {
            int index = Item.NewItem(new EntitySource_TileBreak(origin.X, origin.Y), origin.ToWorldCoordinates()
                , itemType);

            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index, 1f);

            return index;
        }

        /// <summary>
        /// 生成被物块破坏所产生的物品，带有同步功能
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public static int SpawnItemTileBreakNet<T>(Point16 origin)
            where T : ModItem
        {
            int index = Item.NewItem(new EntitySource_TileBreak(origin.X, origin.Y), origin.ToWorldCoordinates()
                , ModContent.ItemType<T>());

            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index, 1f);

            return index;
        }
    }
}
