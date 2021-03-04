using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEyeballs : MonoBehaviour
{
    public ParticleSystem AfterImage;
    public GameObject oneShotBeam;
    public void FireBeam()
    {
        StartCoroutine(LoadUpTheBeam());
    }

    private IEnumerator LoadUpTheBeam(){
        yield return new WaitForSeconds(0.2f);
        oneShotBeam.SetActive(true);
        yield return new WaitForSeconds(1f);
        oneShotBeam.SetActive(false);

    }
}
