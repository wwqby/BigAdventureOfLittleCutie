using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollecable : MonoBehaviour, ICollecable
{

    protected bool isCollected;

    void OnEnable()
    {
        isCollected = false;
    }

    public void Collect(Player player)
    {
        if (isCollected)
        {
            return;
        }
        isCollected = true;
        StartCoroutine(FollowPlayer(player));
    }

    IEnumerator FollowPlayer(Player player)
    {
        float timer = 0;
        Vector2 from = transform.position;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            transform.position = Vector2.Lerp(from, player.GetCenterPoint(), timer);
            yield return null;
        }
        Collected();
    }

    protected abstract void Collected();

}
