using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
 {
    [Header("Weapon Settings")]
     public int bulletsPerMag = 30; // Bullets per each mag
     public int bulletsLeft; // Total bullets
     public int currentBullets; // total in the mag
     public float range = 100f;
     public float fireRate = 0.1f;
     public float damage = 20f;
     public float spreadFactor = 0.1f;
     public float reloadtime = 2f;
     float fireTimer;

     public enum ShootMode { Auto, Semi }     // Nameable int almost
     [Header("Shoot Type")]
     public ShootMode shootingMode;

     [Header("ShootPoint & Inpact")]
     public Transform shootPoint;    // Shootpoint
     public GameObject bulletImpact;

     [Header("UI")]
     public Text ammoText;

     [Header("Animations")]
     public Animator anim;

     [Header("Particles")]
     public ParticleSystem muzzleFlash; // TODO: Fix a good Muzzle flash prefab (Broken)
     public GameObject hitParticles;

               //TODO: Repair Audio that stopped working
     private AudioSource _AudioSource;
     [Header("Sound Effects")]
     public AudioClip shootSound;

     //Bools 
     private bool isReloading;
     private bool shootInput;

     void OnEnable()
     {  // Update when active state is changed
        UpdateAmmoText();

        isReloading = false; // reset states
     }

    void Start()
     {
         currentBullets = bulletsPerMag;
         anim = GetComponent<Animator>();
          _AudioSource = GetComponent<AudioSource>();
     }

     void Update()
     {
         switch (shootingMode)
         {
             case ShootMode.Auto:
                 shootInput = Input.GetButton("Fire1");
                
                 break;
             case ShootMode.Semi:
                 shootInput = Input.GetButtonDown("Fire1");
                
             break;
         }
            if (shootInput)
            {
                if (currentBullets > 0) { Fire(); }

                else if (bulletsLeft > 0 && !isReloading)
                {
                    StartCoroutine(DoReload());
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentBullets < bulletsPerMag && bulletsLeft > 0 && !isReloading)
                {
                    StartCoroutine(DoReload());
                }
            }

         if (fireTimer < fireRate)
         {
            fireTimer += Time.deltaTime;
         }     
     }

     void FixedUpdate()
     {
         AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
         isReloading = info.IsName("Reload");
     }
    public void Fire()
    {
       if (fireTimer < fireRate || currentBullets <= 0 || isReloading)
            return;
         //Removes 1 bullet,  Updating ammo txt,  playing shoot sound
         currentBullets--;

            UpdateAmmoText();
        
         PlayShootSound();

         RaycastHit hit;
                // Randomising bullet spread
        Vector3 shootDirection = shootPoint.transform.forward;
        shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
        shootDirection.y += Random.Range(-spreadFactor, spreadFactor);

        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
         {
                GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                Destroy(hitParticleEffect, 1f); Destroy(bulletHole, 2f);

                if (hit.transform.GetComponent<HealthController>())
                {
                   // hit.transform.GetComponent<HealthController>().ApplyDamage(damage);
                }
         }
         anim.CrossFadeInFixedTime("Fire", 0.1f);
         muzzleFlash.Play();         
        fireTimer = 0.0f; // Restore FireTimer
    }
    public void Reload()
    {
       if (bulletsLeft <= 0) return;
       int bulletsToLoad = bulletsPerMag - currentBullets;
       //                          IF                       Then           Else
       int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;
       bulletsLeft -= bulletsToDeduct;
       currentBullets += bulletsToDeduct;
        UpdateAmmoText();
    }
    private IEnumerator DoReload()
    {
       AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        isReloading = true; 
       anim.CrossFadeInFixedTime("Reload", 0.1f); // Transition to reload

        //Waiting 4 reload time based on anmim time
        yield return new WaitForSeconds(reloadtime);
        isReloading = false;
        Reload();
    }
    private void PlayShootSound()
    {
       _AudioSource.PlayOneShot(shootSound);
    }
    public void UpdateAmmoText()
    {
       ammoText.text = currentBullets + "/" + bulletsLeft;
    }
        
}
