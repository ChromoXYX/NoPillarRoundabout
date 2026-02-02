using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace NoPillarRoundabout
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(NoPillarRoundabout)}.{nameof(Mod)}").SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            updateSystem.UpdateAt<RemoveRoundaboutFlagSystem>(SystemUpdatePhase.PrefabUpdate);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }
}
