using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class bullet : MonoBehaviour
{

    public float power = 8f;
    public void Shoot(bool isLeft)
    {
        Vector3 direction = isLeft ? Vector3.left : Vector3.right;

        transform.DOMove(transform.position + direction * 20f, 1.25f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void DeleteObj()
    {
        transform.DOKill();
        Destroy(gameObject);
    }

}
