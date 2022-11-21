using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            Debug.Log("Item was interacted with " + other.name);
            PickUp();
        }
    }

    void PickUp() {
        Debug.Log("Picking up item");
        // Add to inventory
        Destroy(gameObject);
    }
}
