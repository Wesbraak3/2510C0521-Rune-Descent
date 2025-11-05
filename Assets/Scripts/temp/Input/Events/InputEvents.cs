using UnityEngine;

# region Movement
public class MeveInputEvent {
    public Vector2 MoveVector;
    public MeveInputEvent(Vector2 moveVector) => MoveVector = moveVector;
}

public class EnableMovementEvent {
    public bool InputEnabled;
    public EnableMovementEvent(bool inputEnabled) => InputEnabled = inputEnabled;
}

#endregion
