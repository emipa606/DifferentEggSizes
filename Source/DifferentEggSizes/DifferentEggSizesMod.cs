using Mlie;
using UnityEngine;
using Verse;

namespace DifferentEggSizes;

[StaticConstructorOnStartup]
internal class DifferentEggSizesMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static DifferentEggSizesMod Instance;

    private static string currentVersion;

    /// <summary>
    ///     The private settings
    /// </summary>
    private DifferentEggSizesSettings settings;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public DifferentEggSizesMod(ModContentPack content) : base(content)
    {
        Instance = this;
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal DifferentEggSizesSettings Settings
    {
        get
        {
            settings ??= GetSettings<DifferentEggSizesSettings>();

            return settings;
        }
        set => settings = value;
    }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Different Egg Sizes";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.CheckboxLabeled("DES.nolimit.label".Translate(), ref Settings.NoLimit,
            "DES.nolimit.tooltip".Translate());
        if (!Settings.NoLimit)
        {
            listingStandard.Gap();
            Widgets.FloatRange(listingStandard.GetRect(28f), 192168110, ref Settings.MaxEggNutrition, 0f, 5f,
                "DES.MaxEggNutrition");
            listingStandard.Gap();
            Widgets.FloatRange(listingStandard.GetRect(28f), 192168120, ref Settings.MaxEggMass, 0f, 5f,
                "DES.MaxEggMass");
            listingStandard.Gap();
            Widgets.IntRange(listingStandard.GetRect(28f), 192168130, ref Settings.MaxEggHitPoints, 0, 200,
                "DES.MaxEggHitPoints");
        }

        listingStandard.Gap();
        listingStandard.CheckboxLabeled("DES.logging.label".Translate(), ref Settings.VerboseLogging,
            "DES.logging.tooltip".Translate());
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("DES.version.label".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        Settings.Write();
    }

    public override void WriteSettings()
    {
        base.WriteSettings();
        Main.UpdateEggDefinitions();
    }
}