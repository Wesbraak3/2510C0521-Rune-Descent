using PurrNet.Utils;
using UnityEngine;

public class BossHealth : BaseHealth {

    [SerializeField] private Transform _body;


    protected override void Die() {
        base.Die();



        Debug.Log("KILLED A BOSS");
        Destroy(this.gameObject);
    }
}