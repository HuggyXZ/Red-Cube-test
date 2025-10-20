using UnityEngine;

public class EndTrigger : MonoBehaviour {
    private GameObject completeLevelUI;
    private GameObject parentObject;

    private void Start() {
        // Find the parent object that contains the LevelCompleteUI object
        parentObject = GameObject.Find("Canvas");

        // Find the LevelCompleteUI object within the parent object
        completeLevelUI = parentObject.transform.Find("LevelCompleteUI").gameObject;

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out PlayerMovement player)) {
            Debug.Log("Level Complete!");
            completeLevelUI.SetActive(true);
        }
    }

}
