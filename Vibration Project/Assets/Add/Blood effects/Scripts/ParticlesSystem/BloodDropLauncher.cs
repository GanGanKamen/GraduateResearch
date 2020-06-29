using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BloodDropLauncher : MonoBehaviour
{   
    [Tooltip("Count per single burst")]
    public int SpawnCount;
    public bool IsPlayOnAwake;
    public BloodDecalPool BloodDecalPool;

    private ParticleSystem _currentParticles;
    private List<ParticleCollisionEvent> _collisionEvents;

    private void Awake()
    {
        _currentParticles = GetComponent<ParticleSystem>();
        _collisionEvents = new List<ParticleCollisionEvent>();
        if (IsPlayOnAwake)
            Spawn();
    }
    /// <summary>
    /// Run simulation
    /// </summary>
    [ContextMenu("Spawn")]
    public void Spawn()
    {
        BloodDecalPool.SetCount(SpawnCount);
        _currentParticles.Emit(SpawnCount);
    }

    private void OnParticleCollision(GameObject collidedObject)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(_currentParticles, collidedObject, _collisionEvents);

        for (int i = 0; i < _collisionEvents.Count; i++)
        {
            BloodDecalPool.ParticleHit(_collisionEvents[i]);
        }
    }
    
}
