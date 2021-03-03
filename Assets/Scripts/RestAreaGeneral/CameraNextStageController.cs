using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNextStageController : MonoBehaviour
{

    [SerializeField]
    GameObject camera1, camera2;//, camera3, camera4, camera5;

    public bool NextStage1, NextStage2;//, NextStage3, NextStage4, NextStage5;
    public bool RestArea;
    
    //private Triggers trig;
    private void Start()
    {
        NextStage1 = false;
        RestArea = true;
    }

    private void Update()
    {
        NextCamera();
        PreviousCam();
        /*   if (trig.stage1 == true)
           {
               NextCamera();
           }*/
    }


    private void NextCamera()
    {
        if (NextStage1 == true)
        {
            Debug.Log("Nextt");
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }

    private void PreviousCam()
    {
        if (RestArea == true)
        {
            Debug.Log("Previous");
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
    }
}
        
        
        /*
        if (NextStage1 == true && NextStage2 == true)
        {

            camera2.SetActive(false);
            camera3.SetActive(true);
            Debug.Log("Stage3");
        }
        if (NextStage2 == true && NextStage3 == true)
        {
            camera3.SetActive(false);
            camera4.SetActive(true);
            Debug.Log("Stage4");

        }*/


        /*   if (NextStage2 == true && NextStage3 == true)
           {

               camera2.SetActive(false);
            //   camera3.SetActive(true);
           }*/

  
