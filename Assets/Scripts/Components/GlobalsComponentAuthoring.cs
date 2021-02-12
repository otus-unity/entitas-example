using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class GlobalsComponentAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject shotPrefab;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var component = new GlobalsComponent();
        component.shotPrefab = conversionSystem.GetPrimaryEntity(shotPrefab);
        dstManager.AddSharedComponentData(entity, component);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(shotPrefab);
    }
}
