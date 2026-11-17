using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
         // When an item has not been picked up yet, as the user moves over it, it will disappear and go to the top of the player's inventory.
        if (other.gameObject.CompareTag("PowerUp"))
        {
            PowerUp pu = other.GetComponent<PowerUp>();

            if (pu.power == 0)
            {
               gameObject.GetComponent<CarController>().motorForce += 10;
            }
            other.gameObject.SetActive(false);
        }
    }
}
