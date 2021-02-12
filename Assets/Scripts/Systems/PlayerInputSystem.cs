using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Entities;

public class PlayerInputSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref PlayerComponent player, ref Translation translation, ref Rotation rotation) => {
                float positionDelta = 0.0f;
                if (Input.GetKey(KeyCode.W))
                    positionDelta += 1.0f;
                if (Input.GetKey(KeyCode.S))
                    positionDelta -= 1.0f;

                if (!Mathf.Approximately(positionDelta, 0.0f)) {
                    var data = new ForwardMovementComponent{ speed = 10.0f * positionDelta };
                    if (EntityManager.HasComponent<ForwardMovementComponent>(entity))
                        EntityManager.SetComponentData<ForwardMovementComponent>(entity, data);
                    else
                        EntityManager.AddComponentData<ForwardMovementComponent>(entity, data);
                } else {
                    if (EntityManager.HasComponent<ForwardMovementComponent>(entity))
                        EntityManager.RemoveComponent<ForwardMovementComponent>(entity);
                }

                // rotation

                float rotationDelta = 0.0f;
                if (Input.GetKey(KeyCode.A))
                    rotationDelta += 1.0f;
                if (Input.GetKey(KeyCode.D))
                    rotationDelta -= 1.0f;

                if (!Mathf.Approximately(rotationDelta, 0.0f)) {
                    float angle = rotationDelta * math.radians(120.0f) * Time.DeltaTime;
                    rotation.Value = math.mul(rotation.Value, quaternion.AxisAngle(new float3(0, 0, 1), angle));
                }

                // shooting

                if (Input.GetMouseButtonDown(0)) {
                    Entity prefabEntity = default;
                    Entities.ForEach((GlobalsComponent component) => {
                            prefabEntity = component.shotPrefab;
                        });

                    var dir = math.mul(rotation.Value, new float3(1, 0, 0));
                    var shotEntity = EntityManager.CreateEntity();
                    EntityManager.AddComponentData<Translation>(shotEntity, new Translation{ Value = translation.Value + dir });
                    EntityManager.AddComponentData<Rotation>(shotEntity, new Rotation{ Value = rotation.Value });
                    EntityManager.AddComponentData<PrefabComponent>(shotEntity, new PrefabComponent{ prefab = prefabEntity });
                    EntityManager.AddComponentData<ShotComponent>(shotEntity, new ShotComponent{});
                    EntityManager.AddComponentData<ForwardMovementComponent>(shotEntity, new ForwardMovementComponent{ speed = 20.0f });
                    EntityManager.AddComponentData<HealthComponent>(shotEntity, new HealthComponent{ value = 1.0f });
                }
            });
    }
}
