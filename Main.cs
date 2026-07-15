global using static RCF.Main.Logger;
using System.IO;
using System.Reflection;
using ModLoader.Framework;
using ModLoader.Framework.Attributes;
using HarmonyLib;

namespace RCF.Main;

[ItemId("esker.RCF")]
public class Main : VtolMod
{
    public string ModFolder;
    private Harmony harmonyInstance;

    /// <summary>
    /// Initialising Harmony patches
    /// </summary>
    private void Awake()
    {
        ModFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Log($"Awake at {ModFolder}");

        try
        {
            harmonyInstance = new Harmony("esker.RCF");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            Log("Harmony patches applied successfully!");
        }
        catch (System.Exception e)
        {
            Log($"Failed to apply Harmony patches: {e.Message}");
        }
    }

    /// <summary>
    /// Unloading and destroying Harmony patches
    /// </summary>
    public override void UnLoad()
    {
        if (harmonyInstance != null)
        {
            harmonyInstance.UnpatchSelf();
            Log("Harmony patches unapplied.");
        }
    }
}