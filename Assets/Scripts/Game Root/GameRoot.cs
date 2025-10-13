using UnityEngine;

public class GameRoot : MonoBehaviour {
    [SerializeField] private GridManager grid;
    //[SerializeField] private TurnController turnController;
    //[SerializeField] private BattleSystem battleSystem;

    public GridManager Grid => grid;
    //public TurnController Turn => turnController;
    //public BattleSystem Battle => battleSystem;
}