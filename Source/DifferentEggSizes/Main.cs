using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace DifferentEggSizes
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        private static float baseBodySize;
        private static float baseNutrition;
        private static float baseMass;
        private static float baseMaxHitPoints;
        private static bool allIsWell;

        static Main()
        {
            UpdateBaseline();
            UpdateEggDefinitons();
        }

        private static void UpdateBaseline()
        {
            // Chicken is the base animal
            var chicken = DefDatabase<ThingDef>.GetNamedSilentFail("Chicken");
            if (chicken == null)
            {
                Log.Warning(
                    "[DifferentEggSizes]: Could not find Chicken, all calcultions are based on its values so will not change anything.");
                return;
            }

            var chickenEgg = chicken.GetCompProperties<CompProperties_EggLayer>()?.eggUnfertilizedDef;
            if (chickenEgg == null)
            {
                Log.Warning(
                    "[DifferentEggSizes]: Could not find Chicken-egg, all calcultions are based on its values so will not change anything.");
                return;
            }

            baseBodySize = chicken.race.baseBodySize;
            baseNutrition = chickenEgg.GetStatValueAbstract(StatDefOf.Nutrition);
            baseMass = chickenEgg.BaseMass;
            baseMaxHitPoints = chickenEgg.BaseMaxHitPoints;
            allIsWell = true;
        }

        public static void UpdateEggDefinitons()
        {
            if (!allIsWell)
            {
                return;
            }

            Log.Message(
                $"[DifferentEggSizes]: Starting egg-updating based on Chicken-eggs. Nutrition: {baseNutrition}, Mass: {baseMass}, MaxHitPoints: {baseMaxHitPoints}");
            var eggsAndLayers = new Dictionary<ThingDef, ThingDef>();

            foreach (var eggLayer in DefDatabase<ThingDef>.AllDefsListForReading.Where(def =>
                def.HasComp(typeof(CompEggLayer))))
            {
                var eggComp = eggLayer.GetCompProperties<CompProperties_EggLayer>();
                if (eggComp.eggFertilizedDef != null)
                {
                    eggsAndLayers[eggComp.eggFertilizedDef] = eggLayer;
                }

                if (eggComp.eggUnfertilizedDef != null)
                {
                    eggsAndLayers[eggComp.eggUnfertilizedDef] = eggLayer;
                }
            }

            foreach (var eggInfo in eggsAndLayers)
            {
                var egg = eggInfo.Key;
                var layerBodySize = eggInfo.Value.race?.baseBodySize;
                if (layerBodySize == null)
                {
                    Log.Message(
                        $"[DifferentEggSizes]: {eggInfo.Value.defName} does not have a defined body-size, ignoring its eggs");
                    continue;
                }

                var bodyFactor = (float)layerBodySize / baseBodySize;
                var newNutrition = (float)Math.Round((decimal)(baseNutrition * bodyFactor), 3);
                var newMass = (float)Math.Round((decimal)(baseMass * bodyFactor), 3);
                var newMaxHitPoints = (float)Math.Round((decimal)(baseMaxHitPoints * bodyFactor), 0);

                if (!DifferentEggSizesMod.instance.Settings.NoLimit)
                {
                    newNutrition =
                        Math.Max(Math.Min(newNutrition, DifferentEggSizesMod.instance.Settings.MaxEggNutrition.max),
                            DifferentEggSizesMod.instance.Settings.MaxEggNutrition.min);
                    newMass = Math.Max(Math.Min(newMass, DifferentEggSizesMod.instance.Settings.MaxEggMass.max),
                        DifferentEggSizesMod.instance.Settings.MaxEggMass.min);
                    newMaxHitPoints =
                        Math.Max(Math.Min(newMaxHitPoints, DifferentEggSizesMod.instance.Settings.MaxEggHitPoints.max),
                            DifferentEggSizesMod.instance.Settings.MaxEggHitPoints.min);
                }

                egg.SetStatBaseValue(StatDefOf.Nutrition, newNutrition);
                egg.SetStatBaseValue(StatDefOf.Mass, newMass);
                egg.SetStatBaseValue(StatDefOf.MaxHitPoints, newMaxHitPoints);

                if (DifferentEggSizesMod.instance.Settings.VerboseLogging)
                {
                    Log.Message(
                        $"[DifferentEggSizes]: {egg.defName} updated. Nutrition: {newNutrition}, Mass: {newMass}, MaxHitPoints: {newMaxHitPoints}");
                }
            }

            Log.Message($"[DifferentEggSizes]: Updated {eggsAndLayers.Count} egg-definitions");
        }
    }
}