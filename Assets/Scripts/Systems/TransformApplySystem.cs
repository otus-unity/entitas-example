using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class TransformApplySystem : ReactiveSystem<GameEntity>
{
    Contexts contexts;

    public TransformApplySystem(Contexts contexts)
        : base(contexts.game)
    {
        this.contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return new Collector<GameEntity>(
            new [] {
                context.GetGroup(GameMatcher.AnyOf(GameMatcher.Position, GameMatcher.Rotation)),
                context.GetGroup(GameMatcher.View),
            }, new [] {
                GroupEvent.Added,
                GroupEvent.Added,
            }
        );
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities) {
            var t = e.view.gameObject.transform;
            if (e.hasPosition)
                t.position = e.position.value;
            if (e.hasRotation)
                t.rotation = Quaternion.Euler(0.0f, 0.0f, e.rotation.angle);
        }
    }
}
