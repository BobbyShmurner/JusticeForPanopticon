using BepInEx;

using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace JusticeForPanopticon
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("ULTRAKILL.exe")]
    public class Plugin : BaseUnityPlugin {
        void Awake() {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            StartCoroutine(DisabledInstakill(SceneManager.GetActiveScene()));
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadMode) {
            StartCoroutine(DisabledInstakill(scene));
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        IEnumerator DisabledInstakill(Scene scene) {
            if (scene.name != "Level P-2") yield break;
            yield return null;

            bool disabledInstakill = false;

            foreach (ObjectActivator activator in Resources.FindObjectsOfTypeAll<ObjectActivator>()) {
                if (activator.gameObject.name != "DelayedInstakill") continue;
                activator.enabled = false;
                disabledInstakill = true;
            }

            if (disabledInstakill) {
                Logger.LogInfo("Disabled Panopticon Instalkill!");
            } else {
                Logger.LogError("Failed to disabled Panopticon Instalkill!");
            }
        }
    }
}
