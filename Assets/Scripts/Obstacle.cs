using UnityEngine;

public class Obstacle : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player)) {
            // change obstacle color
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

}
