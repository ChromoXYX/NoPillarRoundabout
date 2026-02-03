using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace NoPillarRoundabout
{
    public class Mod : IMod
    {
        public static ILog Log;

        public void OnLoad(UpdateSystem updateSystem)
        {
            Log = LogManager.GetLogger($"{nameof(NoPillarRoundabout)}.{nameof(Mod)}").SetShowsErrorsInUI(false);

            Log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                Log.Info($"Current mod asset at {asset.path}");

            updateSystem.UpdateAfter<RemoveRoundaboutFlagSystem>(SystemUpdatePhase.PrefabUpdate);
        }

        public void OnDispose()
        {
            Log.Info(nameof(OnDispose));
        }
    }
}
