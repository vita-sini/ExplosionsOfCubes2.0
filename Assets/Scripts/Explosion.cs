using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _explosionRadius = 40;
    private float _explosionForce = 700;
    private float _explosionForceEffort = 2;
    private float _explosionRadiusEffort = 2;

    public void Blasting()
    {
        _explosionRadius *= _explosionForceEffort;
        _explosionForce *= _explosionRadiusEffort;

        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private IEnumerable<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
