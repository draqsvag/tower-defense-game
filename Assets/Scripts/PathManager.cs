using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    public static PathManager Instance { get; private set; }

    public Transform[] path;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator FollowPath(Transform follower, float speed)
    {
        for (int i = 0; i < path.Length; i++)
        {
            while (follower.position != path[i].position)
            {
                Vector2 direction = (follower.position - path[i].position).normalized;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                follower.position = Vector3.MoveTowards(follower.position, path[i].position, Time.deltaTime * speed);
                follower.GetChild(0).rotation = Quaternion.Slerp(follower.GetChild(0).rotation, targetRotation, Time.deltaTime * 20); ;
                yield return null;
            }

            follower.position = path[i].position;
        }

        yield return null;
    }
}
