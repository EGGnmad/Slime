using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera;

    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        Time.timeScale = 0.3f;

        StartCoroutine(Timer(time));
    }


    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        CinemachineBasicMultiChannelPerlin perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = 0f;
        Time.timeScale = 1f;
    }
}
