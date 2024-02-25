using UnityEngine;

public class FollowPath : ArriveModified
{
    public Path path;
    public float threshold;
    public bool loop = false;
    public bool reverse = false;

    private int index = 0;

    public void ResetIndex() => index = 0;

    protected override Vector3 getTargetPosition()
    {
        if (path.Length < 1) {
            return character.transform.position; }
        Vector3 directionToTarget = path.PathObjects[index].position - character.transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        float speedMod = path.GetSpeedModifier(index);
        if (speedMod != 0)
        {
            speedMod = 10 / speedMod;
        }
        maxSpeed = 4 - speedMod;
        if (distanceToTarget < threshold)
        {
            index++;
            if (index >= path.PathObjects.Length)
            {
                if (loop || reverse)
                    index = 0;
                else
                    index -= 1;
                if (reverse) path.Reverse();
            }
        }

        return path.PathObjects[index].position;
    }
}