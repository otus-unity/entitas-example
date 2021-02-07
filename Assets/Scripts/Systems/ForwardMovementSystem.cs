using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovementSystem : IExecuteSystem
{
    IGroup<GameEntity> entities;

    public ForwardMovementSystem(Contexts contexts)
    {
        entities = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.ForwardMovement,
            GameMatcher.Position,
            GameMatcher.Rotation));
    }

    public void Execute()
    {
        foreach (var e in entities) {
            var angle = e.rotation.angle * Mathf.Deg2Rad;
            var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            e.ReplacePosition(e.position.value + dir * e.forwardMovement.speed * Time.deltaTime);
        }
    }
}
