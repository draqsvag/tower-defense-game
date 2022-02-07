using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(PathManager.Instance.FollowPath(transform, 5f));
    }
}
