using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    // Weapon data from enemy
<<<<<<< Updated upstream
    private WeaponData data;

    private void Awake()
    {
=======
    public WeaponData data;
    private Renderer PickupColour;

    private void Awake()
    {
        PickupColour = this.gameObject.GetComponent<Renderer>();

>>>>>>> Stashed changes
        data = GetComponent<WeaponData>();

        if (data == null)
        {
            Debug.LogError("No WeaponData found on pickup prefab!");
        }
    }

<<<<<<< Updated upstream
=======
    void SelectPickupType()
    {
        int choice = Random.Range(0, 1);
        switch (choice)
        {
            case 0:
                {
                    this.gameObject.tag = "HealthPickup";
                    PickupColour.material.SetColor("_BaseColor", Color.green);
                    // Change model
                    break;
                }

            case 1:
                {
                    this.gameObject.tag = "WeaponPickup";
                    PickupColour.material.SetColor("_BaseColor", Color.gray);
                    // Change model
                    break;
                }
            default:
                {
                    this.gameObject.tag = "HealthPickup";
                    PickupColour.material.SetColor("_BaseColor", Color.green);
                    break;
                }
        }
    }
>>>>>>> Stashed changes

    private void OnTriggerEnter(Collider other)
    {
        // Check if only the player collides with the pickup
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Player collided with pickup!");

        // Get the current weapon base from the pickup

<<<<<<< Updated upstream
        WeaponBase currentWeapon = other.GetComponentInChildren<WeaponBase>();
=======
            Debug.Log("Player health obtained");
            objectHealth.TakeDamage(-30);
            Debug.Log("Player Health: " + objectHealth.currentHealth);
        }
>>>>>>> Stashed changes

        if (currentWeapon == null)
        {
<<<<<<< Updated upstream
            Debug.LogError("Could not find player's WeaponBase!");
            return;
        }
=======
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

        Debug.Log("Player weapon obtained!");

<<<<<<< Updated upstream
        // Get the weapon data from the enemy and apply it to the player's current weapon
        if (data != null)
        {
            currentWeapon.CancelQTE();
            data.SetData(currentWeapon);
            Debug.Log("Weapon data applied to player's current weapon");
            //currentWeapon.Reload();
        }

        currentWeapon.CancelQTE();
=======
            if (playerLoadout.PrimaryWeaponData != null || playerLoadout.SecondaryWeaponData != null)
            {
                return;
            }
        }

>>>>>>> Stashed changes
        //Destroy
        Destroy(gameObject);
    }
}
