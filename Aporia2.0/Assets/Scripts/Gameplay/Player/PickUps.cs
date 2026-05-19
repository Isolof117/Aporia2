using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    // Weapon data from enemy
<<<<<<< HEAD
<<<<<<< HEAD
=======
<<<<<<< Updated upstream
=======
>>>>>>> parent of b2b968b (Fixed Loadout / Pickups)
    private WeaponData data;

    private void Awake()
    {
<<<<<<< HEAD
=======
>>>>>>> parent of 1e8ce25 (Merge branch 'main' into JJBranch)
    public WeaponData data;
    private Renderer PickupColour;

    private void Awake()
    {
<<<<<<< HEAD
        PickupColour = this.gameObject.GetComponent<Material>();

        data = GetComponent<WeaponData>();
=======
        PickupColour = this.gameObject.GetComponent<Renderer>();
>>>>>>> parent of 1e8ce25 (Merge branch 'main' into JJBranch)

>>>>>>> Stashed changes
=======
>>>>>>> parent of b2b968b (Fixed Loadout / Pickups)
        data = GetComponent<WeaponData>();

        if (data == null)
        {
            Debug.LogError("No WeaponData found on pickup prefab!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the pickup
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Player collided with pickup!");

        // Get the current weapon base from the pickup

<<<<<<< HEAD
<<<<<<< HEAD
            if (objectHealth == null)
            {
                Debug.LogError("Could not find player's health!");
                return;
            }

            Debug.Log("Player health obtained");

            objectHealth.TakeDamage(-30);
        }

        if (this.gameObject.CompareTag("WeaponPickup"))
        {
=======
<<<<<<< Updated upstream
=======
>>>>>>> parent of b2b968b (Fixed Loadout / Pickups)
        WeaponBase currentWeapon = other.GetComponentInChildren<WeaponBase>();

        if (currentWeapon == null)
        {
            Debug.LogError("Could not find player's WeaponBase!");
            return;
        }
<<<<<<< HEAD
=======
>>>>>>> parent of 1e8ce25 (Merge branch 'main' into JJBranch)
            WeaponBase currentWeapon = other.GetComponentInChildren<WeaponBase>();

            if (currentWeapon == null)
            {
                Debug.LogError("Could not find player's WeaponBase!");
                return;
            }

            Loadout playerLoadout = other.GetComponentInChildren<Loadout>();

            if (playerLoadout == null)
            {
                Debug.LogError("Could not find player's Loadout!");
                return;
            }

            Debug.Log("Player weapon obtained!");

            // If secondary empty -> fill it
            if (playerLoadout.SecondaryWeaponData == null)
            {
                playerLoadout.SecondaryWeaponData = data;
                Debug.Log("Stored weapon in secondary slot");
            }
            else
            {
                // overwrite primary
                playerLoadout.PrimaryWeaponData = data;

                // immediately equip
                data.SetData(currentWeapon);
>>>>>>> Stashed changes
=======
>>>>>>> parent of b2b968b (Fixed Loadout / Pickups)

        Debug.Log("Player weapon obtained!");

        // Get the weapon data from the enemy and apply it to the player's current weapon
        if (data != null)
        {
            currentWeapon.CancelQTE();
            data.SetData(currentWeapon);
            Debug.Log("Weapon data applied to player's current weapon");
            //currentWeapon.Reload();
        }

        currentWeapon.CancelQTE();
        //Destroy
        Destroy(gameObject);
    }
}
