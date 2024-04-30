using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static Vector2 GetRelativePosition(GameObject target, Vector3 worldPos, Vector3 origin)
        {
            if(target == null) return Vector2.zero;

            Vector3 targetPos = target.transform.position - origin;
            Vector3 normY = Vector3.Cross(targetPos, Vector3.up);
            Vector3 normX = Vector3.Cross(normY, targetPos);
            float x = Vector3.SignedAngle(targetPos, worldPos, normX);
            float y = Vector3.SignedAngle(targetPos, worldPos, normY);
            return new Vector2(x, y);
        }
    }
}
