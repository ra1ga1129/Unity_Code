using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public Transform CreateObjectPool(GameObject prefab, Vector3 createPos, Quaternion quaternion)
    {
        Transform objectPool = null;
        // 生成するオブジェクトをまとめる箱があるか調べる
        foreach(Transform _transform in this.transform)
        {
            if(_transform.gameObject.name == prefab.gameObject.name)
            {
                objectPool = _transform;
                break;
            }
        }
        // 箱がないとき生成する
        if(objectPool == null)
        {
            // 箱を生成
            GameObject _gameObject = new GameObject(prefab.gameObject.name);
            // 箱を子オブジェクトにする
            _gameObject.transform.parent = transform;
            objectPool = _gameObject.transform;
        }

        // 非アクティブなオブジェクトがないか調べる
        foreach(Transform _transform in objectPool)
        {
            if (!_transform.gameObject.activeSelf)
            {
                // 位置と回転を設定する
                _transform.SetPositionAndRotation(createPos, quaternion);
                _transform.gameObject.SetActive(true);
                return _transform;
            }
        }
        Transform clone = (Instantiate(prefab, createPos, quaternion, objectPool)).transform;
        return clone;
    }

    public void DestroyObjectPool(GameObject _gameObject)
    {
        Destroy(_gameObject);
    }
}
