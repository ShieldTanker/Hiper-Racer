﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{

    [SerializeField] private GameObject[] gasObjects;

    private void Start()
    {
        foreach (var obj in gasObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        foreach (var obj in gasObjects)
        {
            obj.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 플레이어 차량이 도로에 진입하면 다음 도로를 생성
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SpawnRoad(transform.position + new Vector3(0, 0, 10));
        }
    }

    /// <summary>
    /// 플레이어 차량이 도로를 벗어나면 해당 도로를 제거
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DestroyRoad(gameObject);
        }
    }

    public void SpawnGas()
    {
        int index = Random.Range(0, gasObjects.Length);
        gasObjects[index].SetActive(true);
    }
}
