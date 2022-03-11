using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public Transform CreateObjectPool(GameObject prefab, Vector3 createPos, Quaternion quaternion)
    {
        Transform objectPool = null;
        // ��������I�u�W�F�N�g���܂Ƃ߂锠�����邩���ׂ�
        foreach(Transform _transform in this.transform)
        {
            if(_transform.gameObject.name == prefab.gameObject.name)
            {
                objectPool = _transform;
                break;
            }
        }
        // �����Ȃ��Ƃ���������
        if(objectPool == null)
        {
            // ���𐶐�
            GameObject _gameObject = new GameObject(prefab.gameObject.name);
            // �����q�I�u�W�F�N�g�ɂ���
            _gameObject.transform.parent = transform;
            objectPool = _gameObject.transform;
        }

        // ��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ������ׂ�
        foreach(Transform _transform in objectPool)
        {
            if (!_transform.gameObject.activeSelf)
            {
                // �ʒu�Ɖ�]��ݒ肷��
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
