using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour
{
    Systems systems;
    public GameObject shotPrefab;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;

        contexts.game.SetGlobals(shotPrefab);

        systems = new Systems();
        systems.Add(new DeathSystem(contexts));
        systems.Add(new PrefabInstantiateSystem(contexts));
        systems.Add(new ViewDestroySystem(contexts));
        systems.Add(new PlayerInputSystem(contexts));
        systems.Add(new ForwardMovementSystem(contexts));
        systems.Add(new ShotCollisionSystem(contexts));
        systems.Add(new TransformApplySystem(contexts));
        systems.Initialize();
    }

    void OnDestroy()
    {
        systems.TearDown();
    }

    void Update()
    {
        systems.Execute();
        systems.Cleanup();
    }
}
