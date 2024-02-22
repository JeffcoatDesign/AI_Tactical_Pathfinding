using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path
{
    Transform[] pathObjects;
    public Transform[] PathObjects { get => pathObjects; private set {} }
    public int Length { get => pathObjects.Length; private set {} }
    public Path(Transform[] pathObjects) {
        this.pathObjects = pathObjects;
    }
    public void Reverse()
    {
        pathObjects = pathObjects.Reverse().ToArray();
    }
}
