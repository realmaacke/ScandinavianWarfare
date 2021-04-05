using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Position")]
    public float amount = 0.02f;
    public float smoothAmount = 0.06f;
    public float maxAmount = 6f;

    private Vector3 initialPosition;
    private float InputX;
    private float InputY;

    void Start()
    {   // Localposition is used to not affect parrents or childs.
        initialPosition = transform.localPosition; 
    }
    void Update()
    {           
        calculateSway();
        moveSway();
    }
    private void calculateSway()
    {
        InputX = -Input.GetAxis("Mouse X");
        InputY = -Input.GetAxis("Mouse Y");
    }

    private void moveSway()
    {
        float moveX = Mathf.Clamp(InputX * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(InputY * amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(InputX, InputY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }

}
