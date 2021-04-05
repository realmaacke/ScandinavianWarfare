using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    //Marcus
public class MiniMap : MonoBehaviour
{       // Follows the player
    public Transform player;

     void LateUpdate()
    {       // Makes the minimap follow the player on more than just the y axis
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        
       // transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0); 
       // Enable this to make mini map rotate around the player
    }
}
