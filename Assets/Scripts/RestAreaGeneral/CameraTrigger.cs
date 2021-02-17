using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    //   private control1;
    //  public bool stage1;
    BoxCollider2D trigger1, trigger2;

    public CameraNextStageController control1;

    [SerializeField] int stage;

    void Start()
    {
        //  stage = 1;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (stage == 1)
            {
                control1.NextStage1 = true;
                control1.RestArea = false;
                //   stage = 2;

            }
            else if (stage == 1 && (control1.RestArea = false))
            {
                control1.RestArea = true;
                Debug.Log("Rest area true");

                control1.NextStage1 = false;
            }

       /*     else if (stage == 2)
            {
                control1.NextStage2 = true;
            }*/
            /*   else if (stage == 3)
               {
                   control1.NextStage3 = true;
               }*/
        }
    }
}