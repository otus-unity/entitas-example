using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct GlobalsComponent : ISharedComponentData, IEquatable<GlobalsComponent>
{
    public Entity shotPrefab;

    public bool Equals(GlobalsComponent other)
    {
        return shotPrefab == other.shotPrefab;
    }

    public override int GetHashCode()
    {
        return shotPrefab.GetHashCode();
    }
}
