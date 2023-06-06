using BepInEx;
using HarmonyLib;

namespace JusticeForPanopticon
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("ULTRAKILL.exe")]
    public class Plugin : BaseUnityPlugin {
        public static Harmony harmony;

        void Awake() {
            harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }

    [HarmonyPatch(typeof(FleshPrison), "Start")]
    public static class FleshPrison_Start
    {
        [HarmonyPostfix]
        public static void Postfix(FleshPrison __instance)
        {
            if (__instance.altVersion)
                __instance.onFirstHeal = new UltrakillEvent();
        }
    }
}
