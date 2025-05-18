using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    public void Collect(Player player)
    {
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

    private void Collected()
    {
        Destroy(gameObject);
    }
}
