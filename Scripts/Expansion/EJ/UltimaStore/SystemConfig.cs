using System;
using Server.Engines.Points;

namespace Server.Engines.UOStore
{
    public enum CurrencyType
    {
        None,
        Sovereigns,
        Gold,
        PointsSystem,
        Custom
    }

    public delegate int CustomCurrencyHandler(Mobile m, int consume);

    public static class Configuration
    {
        public static bool Enabled { get; set; }
        public static Expansion Expansion { get; set; }
        public static string Website { get; set; }

        /// <summary>
        ///     A hook to allow handling of custom currencies.
        ///     This implementation should be treated as such;
        ///     If 'consume' is less than zero, return the currency total.
        ///     Else deduct from the currency total, return the amount consumed.
        /// </summary>
        public static CustomCurrencyHandler ResolveCurrency { get; set; }

        public static CurrencyType CurrencyImpl { get; set; }
        public static string CurrencyName { get; set; }
        public static bool CurrencyDisplay { get; set; }
        
        public static PointsType PointsImpl { get; set; }

        public static double CostMultiplier { get; set; }

        public static int CartCapacity { get; set; }

        static Configuration()
        {
            Enabled = INI.Get("Store.Enabled", true);
            Expansion = INI.GetEnum("Store.Expansion", Expansion.TOL);
            Website = INI.Get("Store.Website", "https://uo.com/ultima-store/");

            ResolveCurrency = INI.GetDelegate("Store.ResolveCurrency", (CustomCurrencyHandler)null);

            CurrencyImpl = INI.GetEnum("Store.CurrencyImpl", CurrencyType.Sovereigns);
            CurrencyName = INI.Get("Store.CurrencyName", "Sovereigns");
            CurrencyDisplay = INI.Get("Store.CurrencyDisplay", true);

            PointsImpl = INI.GetEnum("Store.PointsImpl", PointsType.None);

            CostMultiplier = INI.Get("Store.CostMultiplier", 1.0);
            CartCapacity = INI.Get("Store.CartCapacity", 10);
        }
        
        public static int GetCustomCurrency(Mobile m)
        {
            if (ResolveCurrency != null)
            {
                return ResolveCurrency(m, -1);
            }

            m.SendMessage(1174, "Currency is not set up for this system. Contact a shard administrator.");

            Utility.WriteConsoleColor(ConsoleColor.Red, "[Ultima Store]: No custom currency method has been implemented.");
            
            return 0;
        }

        public static int DeductCustomCurrecy(Mobile m, int amount)
        {
            if (ResolveCurrency != null)
            {
                return ResolveCurrency(m, amount);
            }

            m.SendMessage(1174, "Currency is not set up for this system. Contact a shard administrator.");

            Utility.WriteConsoleColor(ConsoleColor.Red, "[Ultima Store]: No custom currency deduction method has been implemented.");
            
            return 0;
        }
    }
}
