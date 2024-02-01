using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlaform : MonoBehaviour
{
   private PlatformEffector2D effector;
   public float waitTime;

   void Start()
   {
        //this Gets the 2D effector component
       effector = GetComponent<PlatformEffector2D>();
   }

   void Update()
   {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            waitTime = 0.1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //when pressing the down key it will rotate the coillons of the platform
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.1f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            effector.rotationalOffset = 0;
        }
   }

}
