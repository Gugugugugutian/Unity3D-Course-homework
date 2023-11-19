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
        // ���ʷ��ķ�Ƿ�����ҽӴ�
        if (Vector3.Distance(player.transform.position, slime.transform.position) < 1.0f)
        {
            // ��ʷ��ķ����ҽӴ�ʱ�������ź�
            signal = 1;
            Debug.Log("Player caught the slime!");
        }
    }
}
