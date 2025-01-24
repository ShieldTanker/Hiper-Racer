using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    public static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject roadPrefab;

    [SerializeField] private MoveButton leftMoveButton;
    [SerializeField] private MoveButton rightMoveButton;
    [SerializeField] private TMP_Text gasText;

    // 도로 오브젝트풀
    private Queue<GameObject> _roadPool = new Queue<GameObject>();
    private int roadPoolSize = 4;

    // 활성화 상태의 도로들
    private List<GameObject> _activeRoads = new List<GameObject>();

    // 자동차
    private CarController _carController;

    public enum State { Start, Play, End }
    public State gameState { get; private set; } = State.Start;

    int _roadIndex;

    private void Start()
    {
        // 로드 오브젝트풀 초기화
        InitializeRoadPool();

        gameState = State.Start;

        StartGame();
    }

    private void Update()
    {
        switch (gameState)
        {
            case State.Start:
                break;

            case State.Play:
                foreach (GameObject activeRoad in _activeRoads)
                {
                    activeRoad.transform.Translate(Vector3.back * Time.deltaTime);
                }

                if (_carController != null)
                    gasText.text = _carController.Gas.ToString();
                break;

            case State.End:
                break;
        }
    }

    private void StartGame()
    {
        // 도로 생성
        SpawnRoad(Vector3.zero);

        // 자동차 생성
        _carController = Instantiate(carPrefab, new Vector3(0, 0, -3f), Quaternion.identity).GetComponent<CarController>();

        leftMoveButton.OnMoveButtondown += () => { _carController.Move(-1f); };
        rightMoveButton.OnMoveButtondown += () => { _carController.Move(1f); };

        gameState = State.Play;
    }

    public void EndGame()
    {
        gameState = State.End;

        // 자동차 제거

        // 도로 제거
        foreach (var road in _activeRoads)
        {
            road.SetActive(false);
        }

        // 게임오버 패널 표시
    }

    #region 도로 생성 및 관리

    /// <summary>
    /// 도로 오브젝트 풀 초기화
    /// </summary>
    private void InitializeRoadPool()
    {
        for (int i = 0; i < roadPoolSize; i++)
        {
            GameObject road = Instantiate(roadPrefab);
            road.SetActive(false);
            _roadPool.Enqueue(road);
        }
    }

    /// <summary>
    /// 도로 오브젝트 풀에서 불러와 배치하는 함수
    /// </summary>
    public void SpawnRoad(Vector3 position)
    {
        GameObject road;
        if (_roadPool.Count > 0)
        {
            road = _roadPool.Dequeue();
            road.transform.position = position;
            road.SetActive(true);
        }
        else
        {
            road = Instantiate(roadPrefab, position, Quaternion.identity);
        }

        if (_roadIndex > 0 && _roadIndex % 2 == 0)
        {
            road.GetComponent<RoadController>().SpawnGas();
        }

        _roadIndex++;
        _activeRoads.Add(road);
    }

    public void DestroyRoad(GameObject road)
    {
        road.SetActive(false);
        _activeRoads.Remove(road);
        _roadPool.Enqueue(road);
    }
    #endregion
}
