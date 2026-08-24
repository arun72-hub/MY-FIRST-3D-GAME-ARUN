using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
  
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 1.2f);
    }
}
