using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    public int points = 5;

    private void OnEnable() {
        EventBus.Subscribe<PlayerScoredEvent>(PrintScore);
    }

    private void OnDisable() {
        EventBus.Unsubscribe<PlayerScoredEvent>(PrintScore);
    }

    void PrintScore(PlayerScoredEvent e) {
        Debug.Log(e);
    }

    [ContextMenu("PlayerScored")]
    public void PlayerScored() {
        EventBus.Publish(new PlayerScoredEvent(points));
    }
    [ContextMenu("EndGame")]
    public void EndGame() {
        EventBus.Publish(new GameOverEvent("Player ran out of lives"));
    }
}