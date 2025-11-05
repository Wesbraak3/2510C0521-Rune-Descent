using System.Collections;
using UnityEngine;

public class Crate : MonoBehaviour, IDamageable {

    [SerializeField] private int _hitsToBreak = 3;
    [SerializeField] private int _currentHits = 0;

    public void OnHit<T>(int amount, T attacker) {
        if (_currentHits >= _hitsToBreak) {
            return;
        }
        _currentHits++;

        TextPopup.CreateTextPopup(transform.position, "Hit!", Color.blue, new(0, 1, 0));

        if (_currentHits >= _hitsToBreak) {
            Break();
        }
    }

    private void Break() {
        Destroy(gameObject);
    }
}