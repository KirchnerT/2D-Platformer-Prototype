using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        float direction = Input.GetAxisRaw("Vertical");

        if (direction == 0)
        {
            waitTime = 0.5f;
        } 
        else if (direction < 0)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
                StartCoroutine("ResetRotation");
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }

    IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(0.5f);
        effector.rotationalOffset = 0;
    }


}
