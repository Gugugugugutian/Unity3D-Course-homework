using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    /* Flower signal:
     * 0: Not correct color
     * 1: Correct color
     */
    public int signal;
    private int nowFlowerColor, nowPlatformColor;
    public string displayText;
    /* COLORS:
             * 0:   Red
             * 1:   Yellow
             * 2:   Purple
             * 3:   White
             */
    public int plat_color, flow_color;  // requried color of platform and flower

    public GameObject player;
    public List<GameObject> yellowFlowers, redFlowers, purpleFlowers, whiteFlowers;
    public List<GameObject> yellowPlatform, redPlatform, purplePlatform;

    void Start()
    {
        signal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var flower in redFlowers)
        {
            float distance = Vector3.Distance(player.transform.position, flower.transform.position);
            if (distance < 1.0f)
            {
                nowFlowerColor = 0;
                displayText = "You have RED Flower now.";
                break;
            }
        }
        foreach (var flower in yellowFlowers)
        {
            float distance = Vector3.Distance(player.transform.position, flower.transform.position);
            if (distance < 1.0f)
            {
                nowFlowerColor = 1;
                displayText = "You have YELLOW Flower now.";
                break;
            }
        }
        foreach (var flower in purpleFlowers)
        {
            float distance = Vector3.Distance(player.transform.position, flower.transform.position);
            if (distance < 1.0f)
            {
                nowFlowerColor = 2;
                displayText = "You have PURPLE Flower now.";
                break;
            }
        }
        foreach (var flower in whiteFlowers)
        {
            float distance = Vector3.Distance(player.transform.position, flower.transform.position);
            if (distance < 1.0f)
            {
                nowFlowerColor = 3;
                displayText = "You have WHITE Flower now.";
                break;
            }
        }

        if (player != null)
        {
            Collider playerCollider = player.GetComponent<Collider>();
            nowPlatformColor = 999;
            foreach (var plat in redPlatform)
            {
                Collider platCollider = plat.GetComponent<Collider>();
                // ¼ì²âÅö×²
                if (playerCollider != null && platCollider != null && playerCollider.bounds.Intersects(platCollider.bounds))
                {
                    nowPlatformColor = 0;
                    break;
                }
            }
            foreach (var plat in yellowPlatform)
            {
                Collider platCollider = plat.GetComponent<Collider>();
                // ¼ì²âÅö×²
                if (playerCollider != null && platCollider != null && playerCollider.bounds.Intersects(platCollider.bounds))
                {
                    nowPlatformColor = 1;
                    break;
                }
            }
            foreach (var plat in purplePlatform)
            {
                Collider platCollider = plat.GetComponent<Collider>();
                // ¼ì²âÅö×²
                if (playerCollider != null && platCollider != null && playerCollider.bounds.Intersects(platCollider.bounds))
                {
                    nowPlatformColor = 2;
                    break;
                }
            }
        }

        if (nowPlatformColor == plat_color && nowFlowerColor == flow_color)
        {
            // match the demand
            signal = 1;
        }
    }
}
