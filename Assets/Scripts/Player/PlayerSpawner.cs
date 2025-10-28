using PurrNet;
using UnityEngine;


public class PlayerSpawning : NetworkIdentity {
    [Header("Spawn Settings")]
    [SerializeField] private NetworkIdentity _networkIdentity;

    protected override void OnSpawned() {
        base.OnSpawned();

        if (!isServer) {
            return;
        }

        Instantiate(_networkIdentity, Vector3.zero, Quaternion.identity);
    }
}
