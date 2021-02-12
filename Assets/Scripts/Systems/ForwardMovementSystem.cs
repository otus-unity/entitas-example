using Entitas;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;
using Unity.Entities;

public class ForwardMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        var job = Entities.ForEach(
            (ref ForwardMovementComponent forward, ref Translation translation, ref Rotation rotation) => {
                var dir = math.mul(rotation.Value, new float3(1, 0, 0));
                translation.Value += dir * forward.speed * deltaTime;
            }).Schedule(inputDeps);

        return job;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /*
    struct EnemyInfo
    {
        public float angle;
        public float speed;
    }

    [BurstCompile]
    struct UpdateJob : IJobParallelFor
    {
        public float deltaTime;
        public NativeArray<float2> enemyPositions;
        public NativeArray<EnemyInfo> enemyInfo;

        public void Execute(int enemyIndex)
        {
            var info = enemyInfo[enemyIndex];
            var angle = math.radians(info.angle);
            var dir = new float2(math.cos(angle), math.sin(angle));
            enemyPositions[enemyIndex] += dir * info.speed * deltaTime;
        }
    }

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
        int enemyCount = entities.count;

        UpdateJob job = new UpdateJob();
        job.deltaTime = Time.deltaTime;
        job.enemyPositions = new NativeArray<float2>(enemyCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        job.enemyInfo = new NativeArray<EnemyInfo>(enemyCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

        int i = 0;
        foreach (var e in entities) {
            job.enemyInfo[i] = new EnemyInfo{ angle = e.rotation.angle, speed = e.forwardMovement.speed };
            job.enemyPositions[i] = e.position.value;
            ++i;
        }

        JobHandle jobHandle = job.Schedule(enemyCount, 1);
        jobHandle.Complete();

        i = 0;
        foreach (var e in entities) {
            e.ReplacePosition(job.enemyPositions[i]);
            ++i;
        }

        job.enemyPositions.Dispose();
        job.enemyInfo.Dispose();
    }
    */
}
