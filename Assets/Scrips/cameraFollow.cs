/// <summary>
/// Permite a la cámara seguir al jugador en la escena.
/// </summary>
/// <author>Fidel Alexander Bonilla Montalvo</author>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    private Vector3 targetPosition;
    public float cameraSpeed;
     private Vector3 minLimits, maxLimits;
     private float halfHeight, halfWidth;


    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
        
    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position,
                                               targetPosition,
                                               Time.deltaTime * cameraSpeed);
    }
}
