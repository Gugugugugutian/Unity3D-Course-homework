using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    /* Slime signal:
     * 0: Slime not caught
     * 1: Slime caught.
     */
    public int signal;

    public GameObject player;
    public GameObject slime;

    void Start()
    {
        signal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 检测史莱姆是否与玩家接触
        if (Vector3.Distance(player.transform.position, slime.transform.position) < 1.0f)
        {
            // 当史莱姆与玩家接触时，更新信号
            signal = 1;
            Debug.Log("Player caught the slime!");
        }
    }
}
