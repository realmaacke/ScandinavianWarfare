using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int CurrentWeapon;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {

        int previousSelectedWeapon = CurrentWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (CurrentWeapon >= transform.childCount - 1)
            {
                CurrentWeapon = 0;
            }

            else
            {
                CurrentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (CurrentWeapon <= 0)
            {
                CurrentWeapon = transform.childCount - 1;
            }

            else
            {
                CurrentWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            CurrentWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 2)
        {
            CurrentWeapon = 2;
        }

        if (previousSelectedWeapon != CurrentWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == CurrentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }

            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
