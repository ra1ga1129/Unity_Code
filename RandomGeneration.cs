using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    [SerializeField] private ListGameObject[] gameobjects;
    [SerializeField]private int upperLimit = 10;
    [SerializeField] private float _time = 1f;
    [SerializeField] private float x = 1;
    [SerializeField] private float y = 1;
    [SerializeField] private float z = 1;
    [SerializeField] private Color gizmoColor = Color.red;

    private ObjectPoolManager objectPool;

    [System.Serializable]
    public class ListGameObject
    {
        public float probability;
        public GameObject prefab;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPool = GetComponent<ObjectPoolManager>();
        StartCoroutine(ChooseObjectCoroutine());
    }

    public IEnumerator ChooseObjectCoroutine()
    {
        float total = 0f;
        // 確率の合計値を計算
        foreach (var list in gameobjects)
        {
            total += list.probability;
        }
        float randomValue = Random.value * total;

        foreach (var list in gameobjects)
        {
            if (randomValue < list.probability)
            {
                GenerationGameObject(list.prefab);
                break;
            }
            else
            {
                randomValue -= list.probability;
            }
        }
        int counter = 0;
        foreach(Transform _transform in this.transform)
        {
            counter += _transform.childCount;
        }
        if(counter == upperLimit)
        {
            StartCoroutine(CheckUpperLimitCoroutine());
            yield break;
        }
        yield return new WaitForSeconds(_time);
        StartCoroutine(ChooseObjectCoroutine());
    }

    public IEnumerator CheckUpperLimitCoroutine()
    {
        int counter = 0;
        foreach (Transform _transform in this.transform)
        {
            counter += _transform.childCount;
        }
        if (counter < upperLimit)
        {
            StartCoroutine(ChooseObjectCoroutine());
            yield break;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(CheckUpperLimitCoroutine());
    }

    // オブジェクトを生成する
    private void GenerationGameObject(GameObject _gameObject)
    {
        var randomX = Random.Range(-x, x) / 2 + transform.position.x;
        var randomY = Random.Range(-y, y) / 2 + transform.position.y;
        var randomZ = Random.Range(-z, z) / 2 + transform.position.z;

        Quaternion quaternion = Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f);

        objectPool.CreateObjectPool(_gameObject, new Vector3(randomX, randomY, randomZ), quaternion);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, new Vector3(x, y, z));
    }
}
