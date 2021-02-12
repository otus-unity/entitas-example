using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class DeathSystem : ComponentSystem
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
        Entities.ForEach((Entity entity, ref HealthComponent health) => {
                if (health.value <= 0)
                    commandBuffer.DestroyEntity(entity);
            });
    }
}
