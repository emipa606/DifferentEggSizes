using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace ChooseWildAnimalSpawns
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        static Main()
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

            var baseBodySize = chicken.race.baseBodySize;
            var baseNutrition = chickenEgg.GetStatValueAbstract(StatDefOf.Nutrition);
            var baseMass = chickenEgg.BaseMass;
            var baseMaxHitPoints = chickenEgg.BaseMaxHitPoints;

            Log.Message(
                $"[DifferentEggSizes]: Starting egg-updating based on Chicken-eggs. Nutrition: {baseNutrition}, Mass: {baseMass}, MaxHitPoints: {baseMaxHitPoints}");
            var eggsAndLayers = new Dictionary<ThingDef, ThingDef>();

            foreach (var eggLayer in DefDatabase<ThingDef>.AllDefsListForReading.Where(def =>
                def.HasComp(typeof(CompEggLayer)) && def.defName != "Chicken"))
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

                egg.SetStatBaseValue(StatDefOf.Nutrition, newNutrition);
                egg.SetStatBaseValue(StatDefOf.Mass, newMass);
                egg.SetStatBaseValue(StatDefOf.MaxHitPoints, newMaxHitPoints);

#if DEBUG
                Log.Message(
                    $"[DifferentEggSizes]: {egg.defName} updated. Nutrition: {newNutrition}, Mass: {newMass}, MaxHitPoints: {newMaxHitPoints}");
#endif
            }

            Log.Message($"[DifferentEggSizes]: Updated {eggsAndLayers.Count} egg-definitions");
        }
    }
}