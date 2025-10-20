using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {
    private void GoToNextLevel() {
        if (GameManager.levelNumber >= 3) {
            SceneManager.LoadScene(2);
            return;
        }
        GameManager.levelNumber++;
        SceneManager.LoadScene(1);
    }
}
