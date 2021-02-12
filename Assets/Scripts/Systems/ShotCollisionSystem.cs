using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;

public class ShotCollisionSystem : ComponentSystem
{
    EndFixedStepSimulationEntityCommandBufferSystem commandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        commandBufferSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var commandBuffer = commandBufferSystem.CreateCommandBuffer();
        Entities.ForEach((Entity enemyEntity, ref EnemyComponent enemy, ref Translation enemyTranslation, ref HealthComponent enemyHealth) => {
            float3 enemyPosition = enemyTranslation.Value;
            float health = enemyHealth.value;
            Entities.ForEach((Entity shotEntity, ref ShotComponent shot, ref Translation shotTranslation) => {
                if (math.lengthsq(enemyPosition - shotTranslation.Value) < 0.7f) {
                    commandBuffer.SetComponent<HealthComponent>(enemyEntity, new HealthComponent{ value = health - 1.0f });
                    commandBuffer.SetComponent<HealthComponent>(shotEntity, new HealthComponent{ value = 0.0f });
                }
            });
        });
    }
}
