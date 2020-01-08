using System;
using UnityEngine;

namespace UnityGoodies
{
    public static class Physics2DExtended
    {
        private static RaycastHit2D[] _raycastResults = new RaycastHit2D[20];

        public static RaycastHit2D[] RaycastAllAtMousePosition()
        {
            var camera = Camera.main;
            var resultCount = Mathf.Min(
                Physics2D.RaycastNonAlloc(camera.ScreenToWorldPoint(Input.mousePosition), camera.transform.forward, _raycastResults),
                _raycastResults.Length);

            if (resultCount == 0)
            {
                return null;
            }

            var result = new RaycastHit2D[resultCount];
            Array.Copy(_raycastResults, result, resultCount);

            return result;
        }
    }
}
