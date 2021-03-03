using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEyeballs : MonoBehaviour
{
    public GameObject oneShotBeam;
    private void Awake() {
        Destroy(gameObject, 1.61f);
        StartCoroutine(LoadUpTheBeam());
        
    }

    private IEnumerator LoadUpTheBeam(){
        yield return new WaitForSeconds(0.6f);
        oneShotBeam.SetActive(true);
        yield return new WaitForSeconds(1f);
        oneShotBeam.SetActive(false);

    }
}
