using System.Collections;
using UnityEngine;

public interface IDamageable {
    public void OnHit<T>(int amount, T attacker);
}
