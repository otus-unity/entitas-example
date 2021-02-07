using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiateSystem : ReactiveSystem<GameEntity>
{
    Contexts contexts;

    public PrefabInstantiateSystem(Contexts contexts)
        : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Prefab);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPrefab && !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
            e.AddView(GameObject.Instantiate(e.prefab.prefab));
    }
}
