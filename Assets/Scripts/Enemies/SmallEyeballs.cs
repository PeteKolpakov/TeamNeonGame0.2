using System.Collections;
using UnityEngine;

public class SmallEyeballs : MonoBehaviour
{
    public ParticleSystem AfterImage;
    public GameObject OneShotBeam;
    public void FireBeam()
    {
        StartCoroutine(LoadUpTheBeam());
    }

    private IEnumerator LoadUpTheBeam()
    {
        yield return new WaitForSeconds(0.2f);
        OneShotBeam.SetActive(true);
        yield return new WaitForSeconds(1f);
        OneShotBeam.SetActive(false);

    }
}
