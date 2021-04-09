using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private bool _isCameraShaking = false;

    public void ShakeCamera()
    {
        
            if (_isCameraShaking == false)
            {
                StartCoroutine(ShakeCameraRoutine());
            }
        
        
    }

    public void BossShake()
    {
        if (_isCameraShaking == false) 
        {
            StartCoroutine(BossShakeRoutine());
        }
    }

    IEnumerator ShakeCameraRoutine()
    {
        _isCameraShaking = true;
        for (int i = 0; i < 5; i++)
        {
            this.transform.Rotate(0, 0, 1.0f);
            yield return new WaitForSeconds(0.1f);
            this.transform.Rotate(0, 0, -1.0f);
            yield return new WaitForSeconds(0.1f);
            this.transform.Rotate(0, 0, 0);
        }
        _isCameraShaking = false;
    }

    IEnumerator BossShakeRoutine()
    {
        _isCameraShaking = true;
        for (int i = 0; i < 5; i++)
        {
            this.transform.Rotate(0, 0, 1.0f);
            yield return new WaitForSeconds(0.1f);
            this.transform.Rotate(0, 0, -1.0f);
            yield return new WaitForSeconds(0.1f);
            this.transform.Rotate(0, 0, 0);
        }
        _isCameraShaking = false;
    }
}
