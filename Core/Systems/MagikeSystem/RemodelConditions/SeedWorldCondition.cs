﻿using System;
using Terraria;

namespace Coralite.Core.Systems.MagikeSystem.RemodelConditions
{
    public class DontStarveWorldCondition: IMagikeCraftCondition
    {
        private static readonly Lazy<DontStarveWorldCondition> singleton = new Lazy<DontStarveWorldCondition>(() => new DontStarveWorldCondition());
        public static DontStarveWorldCondition Instance { get => singleton.Value; }

        public string Description => "在饥荒世界中可重塑";

        public bool CanCraft(Item item) => Main.dontStarveWorld;
    }

    public class NotDontDigUpCondition: IMagikeCraftCondition
    {
        private static readonly Lazy<NotDontDigUpCondition> singleton = new Lazy<NotDontDigUpCondition>(() => new NotDontDigUpCondition());
        public static NotDontDigUpCondition Instance { get => singleton.Value; }

        public string Description => "不非颠倒世界中可重塑";

        public bool CanCraft(Item item) => !Main.remixWorld && !Main.zenithWorld;
    }

    public class DontDigUpWorldCondition:IMagikeCraftCondition
    {
        private static readonly Lazy<DontDigUpWorldCondition> singleton = new Lazy<DontDigUpWorldCondition>(() => new DontDigUpWorldCondition());
        public static DontDigUpWorldCondition Instance { get => singleton.Value; }

        public string Description => "在颠倒世界中可重塑";

        public bool CanCraft(Item item) => Main.remixWorld||Main.zenithWorld;
    }
}