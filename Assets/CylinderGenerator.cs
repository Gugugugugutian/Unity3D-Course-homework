using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderGenerator : MonoBehaviour
{
    public GameObject pillarPrefab; // 圆柱体预制体
    public GameObject hintPrefab;  //提示模块预制体

    public float generationInterval = 8f; // 圆柱体生成间隔
    public float destroyDelay = 2f; // 圆柱体销毁延迟

    public float scale_X = 23f, scale_Z = 28f;

    public int cylindars_count = 0; // 圆柱体生成数量
    public float cylindars_size = 0f; // 圆柱体大小

    public List<GameObject> pillars = new List<GameObject>();

    private float nextGenerationTime;

    void Start()
    {
        nextGenerationTime = Time.time + generationInterval;
    }

    void Update()
    {
        if (Time.time >= nextGenerationTime)
        {
            for (int i = 0; i < cylindars_count; i++)
            {
                // 生成提示预制体
                Vector3 spawnPosition = new Vector3(Random.Range(-scale_X, scale_X), -2f, Random.Range(-4f, scale_Z));
                GameObject hint = Instantiate(hintPrefab, spawnPosition, Quaternion.identity);
                hint.transform.localScale = new Vector3(cylindars_size, 50, cylindars_size);

                StartCoroutine(HintTransfertoPillow(hint, 1f));
            }
            nextGenerationTime = Time.time + generationInterval;
        }
    }

    IEnumerator HintTransfertoPillow(GameObject hint, float delay)
    {
        yield return new WaitForSeconds(delay);
        // 生成圆柱体
        Vector3 spawnPosition = hint.transform.position; 
        GameObject pillow = Instantiate(pillarPrefab, spawnPosition, Quaternion.identity);
        pillow.transform.localScale = new Vector3(cylindars_size, 50, cylindars_size);
        Destroy(hint);  // remove hint

        //添加到圆柱体列表
        pillars.Add(pillow);

        yield return new WaitForSeconds(destroyDelay);

        if (pillow != null)
        {
            Destroy(pillow);
        }
        pillars.Remove(pillow);
    }
}