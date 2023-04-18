using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera;

    public void Shake(float intensity, float time, bool slow=true)
    {
        CinemachineBasicMultiChannelPerlin perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;

       
        StartCoroutine(Timer(time));
        if (slow)
            StartCoroutine(Slow(time));
    }


    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        CinemachineBasicMultiChannelPerlin perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0f;
    }

    IEnumerator Slow(float time)
    {
        Time.timeScale = 0.3f;

        yield return new WaitForSeconds(time);

        Time.timeScale = 1f;

    }
}
