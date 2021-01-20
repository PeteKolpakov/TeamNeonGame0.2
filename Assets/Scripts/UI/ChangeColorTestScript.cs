using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorTestScript : MonoBehaviour
{
    public GameObject target;
    public Color publicColor;

    public void ChangeRandomColor()
    {
        Image targetImage = target.GetComponent<Image>();
        targetImage.color = new Color (Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    public void changePublicColor()
    {
        Image targetImage = target.GetComponent<Image>();
        targetImage.color = publicColor;
    }
}
