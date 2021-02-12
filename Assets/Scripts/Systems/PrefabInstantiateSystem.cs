using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Entities;

public class PrefabInstantiateSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithNone<ViewComponent>().ForEach(
            (Entity entity, ref PrefabComponent prefabComponent, ref Translation translation, ref Rotation rotation) => {
                var instantiatedPrefab = EntityManager.Instantiate(prefabComponent.prefab);
                EntityManager.SetComponentData<Translation>(instantiatedPrefab, translation);
                EntityManager.SetComponentData<Rotation>(instantiatedPrefab, rotation);
                EntityManager.AddComponentData<ViewComponent>(entity, new ViewComponent{ entity = instantiatedPrefab });
            });
    }
}
