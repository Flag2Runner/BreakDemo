using UnityEngine;

public static class TransformExtentions
{
    public static bool InRangeof(this Transform transform, Transform otherTransform, float range)
    {
        return Vector3.Distance(transform.position, otherTransform.position) <= range;
    }

    public static Vector3 GetDir(this Transform transform, Transform otherTransform)
    {
        return (otherTransform.position - transform.position).normalized;
    }

    public static bool InAngleOf(this Transform transform, Transform otherTransform, float angle)
    {
        return Vector3.Angle(transform.forward, transform.GetDir(otherTransform)) <= angle;
    }

    // other transform needs to have collision to work
    public static bool IsBlockedTo(this Transform transform, Transform otherTransform, Vector3 startOffset, 
        float checkDistance = Mathf.Infinity)
    {
        if (Physics.Raycast(transform.position + startOffset, transform.GetDir(otherTransform), out RaycastHit hit,
                checkDistance))
        {
            if (hit.collider.gameObject == otherTransform.gameObject)
                return false;
        }

        return true;
    }
}
