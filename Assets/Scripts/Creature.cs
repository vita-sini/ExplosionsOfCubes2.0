using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private Explosion _explosion;

    private float _scaleReduction = 0.5f;
    private int _minObjects = 2;
    private int _maxObjects = 6;
    private int _minChanceSplit = 0;
    private int _maxChanceSplit = 100;
    private float _splitChance = 300f;
    private float _chanceOfDivision = 0.5f;

    private float _explosionRadius = 40;
    private float _explosionForce = 700;

    private void OnMouseUpAsButton()
    {
        List<Rigidbody> explodableObjects = CreateObjects();

        if (explodableObjects.Count > 0)
            Detonate(CreateObjects());
        else
            TriggerExplosion();

        Destroy(gameObject);
    }

    private Creature CreateObject()
    {
        var newCube = Instantiate(this, transform.position, Quaternion.identity);
        newCube.Init(_splitChance);
        return newCube;
    }

    private void Init(float splitChance)
    {
        transform.localScale *= _scaleReduction;
        SetChance(splitChance);
        SetColor();
    }

    private void SetColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        GetComponent<Renderer>().material.color = randomColor;
    }

    private void SetChance(float splitChance)
    {
        _splitChance = splitChance;
    }

    private List<Rigidbody> CreateObjects()
    {
        List<Rigidbody> explodableObjects = new List<Rigidbody>();

        int count = Random.Range(_minObjects, _maxObjects + 1);
        int splitChance = Random.Range(_minChanceSplit, _maxChanceSplit + 1);

        _splitChance *= _chanceOfDivision;

        if (splitChance < _splitChance)
        {
            for (int i = 0; i < count; i++)
            {
                explodableObjects.Add(CreateObject().GetComponent<Rigidbody>());
            }
        }

        return explodableObjects;
    }

    private void TriggerExplosion()
    {
        _explosion.Blast();
        Instantiate(_effect, transform.position, transform.rotation);
    }

    private void Detonate(List<Rigidbody> explodableObjects)
    {
        foreach (Rigidbody explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
