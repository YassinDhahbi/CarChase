using UnityEngine;

public class RaycastDeactivator : MonoBehaviour
{
    public float rayLength = 10f;            // Length of the ray
    public LayerMask hitMask;                // Mask to filter the raycast
    public float reactivationDelay = 2f;     // Delay before reactivating the object (in seconds)

    private GameObject lastHitObject = null;
    private float timer = 0f;                // Timer to track time since last hit

    void Update()
    {
        // Create a ray from the position of this object in the direction it's facing
        Ray ray = new Ray(transform.position, transform.forward);

        // Raycast hit info
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, rayLength, hitMask))
        {
            // Check if the hit object is not null
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;

                // Deactivate the hit object if it was not already deactivated
                if (hitObject.activeSelf)
                {
                    hitObject.SetActive(false);
                }

                // Update the last hit object and reset the timer
                lastHitObject = hitObject;
                timer = 0f; // Reset the timer since the raycast is hitting something
            }
        }
        else
        {
            // Increment the timer based on time since last raycast
            timer += Time.deltaTime;

            // If enough time has passed since the last hit
            if (lastHitObject != null && timer >= reactivationDelay)
            {
                // Reactivate the previously hit object if it was not already active
                if (!lastHitObject.activeSelf)
                {
                    lastHitObject.SetActive(true);
                }

                // Clear the last hit object reference
                lastHitObject = null;
                timer = 0f; // Reset the timer
            }
        }
    }
}
