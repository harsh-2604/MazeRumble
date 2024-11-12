using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensitivitycontroller : MonoBehaviour
{

    public void increaseSensitivity()
    {
        setSensitivity.sensitivity += 10;
    }

    public void decreaseSensitivity()
    {
        setSensitivity.sensitivity -= 10;
    }
}
