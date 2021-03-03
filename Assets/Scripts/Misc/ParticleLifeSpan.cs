using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeSpan : MonoBehaviour
{
    private void Awake() {
        Destroy(gameObject, 2f);
    }
}
