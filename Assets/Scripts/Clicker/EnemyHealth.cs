using PurrNet.Utils;
using UnityEngine;

public class EnemyHealth : BaseHealth {

    [SerializeField] private Transform _body;


    protected override void Die() {
        base.Die();

        Debug.Log("Enemy died");
        Destroy(this.gameObject);
    }
}