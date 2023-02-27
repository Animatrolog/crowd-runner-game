using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParticles : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particles;

    private void OnEnable()
    {
        foreach(ParticleSystem particle in _particles)
        {
            particle.gameObject.SetActive(true);
        }
    }
}
