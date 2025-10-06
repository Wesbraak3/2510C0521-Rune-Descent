using System;
using UnityEngine;

public class PlayerScoredEvent {
    public int Points;
    public PlayerScoredEvent(int points) => Points = points;
}

public class GameOverEvent {
    public string Reason;
    public GameOverEvent(string reason) => Reason = reason;
}