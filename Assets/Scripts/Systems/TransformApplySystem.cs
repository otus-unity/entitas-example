using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Entities;

public class TransformApplySystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref ViewComponent view, ref Translation translation) => {
                EntityManager.SetComponentData<Translation>(view.entity, translation);
            });

        Entities.ForEach((ref ViewComponent view, ref Rotation rotation) => {
                EntityManager.SetComponentData<Rotation>(view.entity, rotation);
            });
    }
}
