using UnityEngine;
using System.Collections;

public class AiSheepInput : MonoBehaviour
{
    public void ReadInput()
    {
        MoveDirection = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f));
    }

    public Vector3 MoveDirection { get; private set; }
}
