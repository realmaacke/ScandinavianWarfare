using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] weapons;
    [SerializeField]
    private float switchDelay = 4;

    private int index;
    private bool isSwitching;

    void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
    }

   
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel")> 0 && !isSwitching)
        {
            index++;

            if(index >= weapons.Length)
            {
                index = 0;
            }
            StartCoroutine(switchAfterDelay(index));
        }
        else if( (Input.GetAxis("Mouse ScrollWheel") < 0 && !isSwitching))
        {
            index++;

            if(index <= 0)
            {
                index = weapons.Length - 1;
            }
            StartCoroutine(switchAfterDelay(index));
        }

    }

    private IEnumerator switchAfterDelay(int newIndex)
    {
        isSwitching = true;
        yield return new WaitForSeconds(switchDelay);
        isSwitching = false;

        SwitchWeapons(newIndex);
        
    }

    private void SwitchWeapons(int newIndex) 
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
    }
}
