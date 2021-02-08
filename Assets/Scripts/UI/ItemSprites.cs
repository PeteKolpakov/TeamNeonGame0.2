using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites : MonoBehaviour
{
    private static ItemSprites _i;

    public static ItemSprites i
    {
        get {
            if(_i == null)
            {
                _i = (Instantiate(Resources.Load("ItemSprites")) as GameObject).GetComponent<ItemSprites>();
            }
            
            return _i; }
    }

    public Sprite circle;
    public Sprite square;
    public Sprite triangle;
}
