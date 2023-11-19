using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderGenerator : MonoBehaviour
{
    public GameObject pillarPrefab; // Բ����Ԥ����
    public GameObject hintPrefab;  //��ʾģ��Ԥ����

    public float generationInterval = 8f; // Բ�������ɼ��
    public float destroyDelay = 2f; // Բ���������ӳ�

    public float scale_X = 23f, scale_Z = 28f;

    public int cylindars_count = 0; // Բ������������
    public float cylindars_size = 0f; // Բ�����С

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
                // ������ʾԤ����
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
        // ����Բ����
        Vector3 spawnPosition = hint.transform.position; 
        GameObject pillow = Instantiate(pillarPrefab, spawnPosition, Quaternion.identity);
        pillow.transform.localScale = new Vector3(cylindars_size, 50, cylindars_size);
        Destroy(hint);  // remove hint

        //��ӵ�Բ�����б�
        pillars.Add(pillow);

        yield return new WaitForSeconds(destroyDelay);

        if (pillow != null)
        {
            Destroy(pillow);
        }
        pillars.Remove(pillow);
    }
}