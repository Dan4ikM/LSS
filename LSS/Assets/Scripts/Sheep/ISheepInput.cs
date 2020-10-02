using UnityEngine;

public interface ISheepInput
{
    void ReadInput();
    Vector3 MoveDirection { get; }
}
