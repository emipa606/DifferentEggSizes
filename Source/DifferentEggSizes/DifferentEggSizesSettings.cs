using Verse;

namespace DifferentEggSizes;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class DifferentEggSizesSettings : ModSettings
{
    public IntRange MaxEggHitPoints = new(0, 100);
    public FloatRange MaxEggMass = new(0f, 2f);
    public FloatRange MaxEggNutrition = new(0f, 2f);
    public bool NoLimit;
    public bool VerboseLogging;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref VerboseLogging, "VerboseLogging");
        Scribe_Values.Look(ref NoLimit, "NoLimit");
        Scribe_Values.Look(ref MaxEggNutrition, "MaxEggNutrition", new FloatRange(0f, 2f));
        Scribe_Values.Look(ref MaxEggMass, "MaxEggMass", new FloatRange(0f, 2f));
        Scribe_Values.Look(ref MaxEggHitPoints, "MaxEggHitPoints", new IntRange(0, 100));
    }
}