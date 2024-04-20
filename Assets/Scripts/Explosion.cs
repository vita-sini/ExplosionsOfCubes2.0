using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _radius = 40;
    private float _force = 700;
    private float _forceEffort = 2;
    private float _radiusEffort = 2;

    public void Blast()
    {
        _radius *= _forceEffort;
        _force *= _radiusEffort;

        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_force, transform.position, _radius);
    }

    private IEnumerable<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
