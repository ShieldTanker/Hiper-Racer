using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float gas = 100;
    [SerializeField] private float horizontalMoveSpeed = 1f;

    public float Gas { get; }
    [SerializeField] float gasEfficiency = 10f;

    private void Start()
    {
        StartCoroutine(GasCoroutine());
    }

    IEnumerator GasCoroutine()
    {
        while (true)
        {
            gas -= gasEfficiency;
            if (gas <= 0)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        // TODO: 게임 종료
        GameManager.Instance.EndGame();
    }

    public void Move(float direction)
    {
        transform.Translate(Vector3.right * (direction * horizontalMoveSpeed * Time.deltaTime));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2f, 2f), 0, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gas"))
        {
            gas += 30;
            other.gameObject.SetActive(false);
        }
    }
}
