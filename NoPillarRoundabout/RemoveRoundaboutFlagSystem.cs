using Colossal.Logging;
using Game;
using Game.Prefabs;
using Unity.Collections;
using Unity.Entities;

namespace NoPillarRoundabout
{
    public partial class RemoveRoundaboutFlagSystem : GameSystemBase
    {
        private bool m_Initialized = false;
        private PrefabSystem m_PrefabSystem;
        private ILog m_log;

        protected override void OnCreate()
        {
            m_log = Mod.log;
            m_log.Info($"{nameof(RemoveRoundaboutFlagSystem)}.{nameof(OnCreate)}");

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            base.OnCreate();
        }

        protected override void OnUpdate()
        {
            if (m_Initialized)
                return;

            m_Initialized = true;

            var em = EntityManager;
            var query = GetEntityQuery(
                ComponentType.ReadWrite<NetObjectData>(),
                ComponentType.ReadOnly<PrefabData>()
            );

            var entities = query.ToEntityArray(Allocator.Temp);

            int cnt = 0;
            foreach (var entity in entities)
            {
                var netObjectData = em.GetComponentData<NetObjectData>(entity);

                if ((netObjectData.m_CompositionFlags.m_General & CompositionFlags.General.Roundabout) == 0)
                    continue;

                var prefabData = em.GetComponentData<PrefabData>(entity);
                if (m_PrefabSystem.TryGetPrefab(prefabData, out PrefabBase prefabBase) && prefabBase != null)
                {
                    if (prefabBase.name != null && prefabBase.name.Contains("Pillar"))
                    {
                        cnt++;
                        netObjectData.m_CompositionFlags.m_General &= ~CompositionFlags.General.Roundabout;
                        em.SetComponentData(entity, netObjectData);
                    }
                }
            }

            entities.Dispose();

            m_log.Info($"{nameof(RemoveRoundaboutFlagSystem)}.{nameof(OnUpdate)} {cnt} prefabs affected");
        }
    }
}
