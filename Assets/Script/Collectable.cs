using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectables
{
    public bool IsCollectable() {
        return true;
    }
}
