using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path
{
    Transform[] pathObjects;
    float[] speedModifiers;
    public Transform[] PathObjects { get => pathObjects; private set {} }
    public int Length { get => pathObjects.Length; private set {} }
    public float GetSpeedModifier (int index) {
        return speedModifiers[index];
    }
    public Path(Transform[] pathObjects, float[] speedModifiers) {
        this.pathObjects = pathObjects;
        this.speedModifiers = speedModifiers;
    }
    public void Reverse()
    {
        pathObjects = pathObjects.Reverse().ToArray();
    }
}
