using TMPro;
using UnityEngine;


public class StatsUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI deathStats;

    // Update is called once per frame
    void Update() {
        UpdateStats();
    }

    private void UpdateStats() {
        deathStats.text = GameManager.Instance.GetDeathCount().ToString();
    }
}
