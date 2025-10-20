using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreTextMesh;

    // static variable does not get reset when scene is reloaded
    public static int levelNumber = 1;

    // List of game levels to keep track of levels
    [SerializeField] private List<GameLevel> gameLevelList;

    public static int deathCount = 0;

    private bool gameHasEnded = false;
    private const float RESTART_DELAY = 1f;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        // Subscribe to the PlayerMovement.OnPlayerDied event
        PlayerMovement.Instance.OnPlayerDied += PlayerMovement_OnPlayerDied;
        LoadCurrentLevel();
    }


    // Event handler for the PlayerMovement.OnPlayerDied event
    private void PlayerMovement_OnPlayerDied(object sender, System.EventArgs e) {
        deathCount += 1; // Increment the death count
    }

    // Update is called once per frame
    private void Update() {
        float playerDistance = PlayerMovement.Instance.transform.position.z / 10;
        scoreTextMesh.text = playerDistance.ToString("0");
    }

    private void LoadCurrentLevel() {
        foreach (GameLevel gameLevel in gameLevelList) {
            if (gameLevel.GetLevelNumber() == levelNumber) {
                // Instantiate the level at the origin (0, 0, 0) with no rotation
                // Instantiate = spawning an object
                Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
            }
        }
    }

    public void EndGame() {
        if (!gameHasEnded) {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            Invoke("RetryLevel", RESTART_DELAY);
            return;
        }
    }

    private void RetryLevel() {
        SceneManager.LoadScene(1);
    }

    public int GetDeathCount() {
        return deathCount;
    }

}
