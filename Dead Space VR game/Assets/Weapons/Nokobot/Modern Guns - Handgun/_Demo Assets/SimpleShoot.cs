using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public Magazine magazine;
    public XRBaseInteractor socketInteractor;
    public TextMeshProUGUI ammoCount;
    public AudioSource gunSound;

    public bool holdingGun;


    private void Update()
    {
        UpdateAmmoCount();
    }

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.selectEntered.AddListener(AddMagazine);
        socketInteractor.selectExited.AddListener(RemoveMagazine);
        //ammoCount = this.GetComponent<TextMeshProUGUI>();

    }

   public void RemoveMagazine(SelectExitEventArgs arg0)
    {
        if (magazine)
        {
            magazine = null;
        }

        throw new NotImplementedException();
    }

    public void AddMagazine(SelectEnterEventArgs arg0)
    {
        magazine = arg0.interactableObject.transform.gameObject.GetComponent<Magazine>();
            
          
      

        throw new NotImplementedException();
    }
    

    public void PullTheTrigger()
    {
        if (magazine && magazine.bulletsLeft > 0)
        {
            gunAnimator.SetTrigger("Fire");
           
        }
        else
        {
            Console.WriteLine("empty");

        }
    }

    public void AmHoldingGun(SelectEnterEventArgs arg0)
    {
        holdingGun = true;
    }

    public void AmNotHoldingGun(SelectExitEventArgs arg0)
    {
        holdingGun = false; ;
    }




    /*public void AddMagazine(XRBaseControllerInteractor interactable)
    {
        Console.WriteLine("addMag");
        magazine = interactable.GetComponent<Magazine>();
    }

    public void RemoveMagazine(XRBaseControllerInteractor interactable)
    {
        magazine = null;
    }
    */



    void UpdateAmmoCount()
    {
        if(!magazine)
        {
            ammoCount.text = "0";
            return;
        }
        ammoCount.text = magazine.bulletsLeft.ToString();


    }



    //This function creates the bullet behavior
    void Shoot()
    {
        magazine.bulletsLeft -= 1;
        gunSound.Play();
        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
