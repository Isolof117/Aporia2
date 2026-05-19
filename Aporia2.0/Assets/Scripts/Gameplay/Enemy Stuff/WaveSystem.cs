using System.Collections;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class WaveSystem : MonoBehaviour
{
    [Header("Doors (assign 7)")]
    [SerializeField] private Transform[] doors;

    [SerializeField] private float doorMoveAmount = 10f;
    [SerializeField] private float doorMoveSpeed = 2f;

    [Header("Enemy Setup")]
    [SerializeField] private GameObject enemyTemplate; // your single "master enemy"
    [SerializeField] private float spawnOffsetBehindDoor = 2f;

    [Header("NavMesh")]
    [SerializeField] private NavMeshSurface navMeshSurface;

    [Header("Wave Text")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private float textFadeDuration = 1f;

    private void Start()
    {
        if (waveText != null)
        {
            waveText.alpha = 0f;
            waveText.text = "WAVE ONE";
        }

        StartCoroutine(StartWaveIntro());
    }

    private IEnumerator StartWaveIntro()
    {
        // Wait 5 seconds after scene load
        yield return new WaitForSeconds(5f);

        // Pick 2 random doors
        int[] selectedDoors = GetTwoRandomDoors();

        //  Spawn enemies FIRST (before doors open)
        SpawnEnemies(selectedDoors);

        // Move doors
        yield return StartCoroutine(MoveDoorsUp(selectedDoors));

        // Rebuild navmesh AFTER doors move
        if (navMeshSurface != null)
            navMeshSurface.BuildNavMesh();

        // UI
        StartCoroutine(ShowWaveText());
    }

    private void SpawnEnemies(int[] activeDoors)
    {
        if (enemyTemplate == null || doors == null) return;

        foreach (int i in activeDoors)
        {
            if (i < 0 || i >= doors.Length) continue;
            if (doors[i] == null) continue;

            Transform door = doors[i];

            // Spawn position: behind door
            Vector3 spawnPos = door.position - door.forward * spawnOffsetBehindDoor;

            // Copy enemy
            Instantiate(enemyTemplate, spawnPos, door.rotation);
        }
    }

    private int[] GetTwoRandomDoors()
    {
        if (doors == null || doors.Length < 2)
            return new int[] { 0, 1 };

        int first = Random.Range(0, doors.Length);

        int second;
        do
        {
            second = Random.Range(0, doors.Length);
        }
        while (second == first);

        return new int[] { first, second };
    }

    private IEnumerator MoveDoorsUp(int[] activeDoors)
    {
        Vector3[] startPositions = new Vector3[doors.Length];
        Vector3[] targetPositions = new Vector3[doors.Length];
        bool[] shouldMove = new bool[doors.Length];

        foreach (int i in activeDoors)
        {
            if (i >= 0 && i < doors.Length)
                shouldMove[i] = true;
        }

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i] == null) continue;

            startPositions[i] = doors[i].position;

            targetPositions[i] = shouldMove[i]
                ? doors[i].position + Vector3.up * doorMoveAmount
                : doors[i].position;
        }

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * doorMoveSpeed;

            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i] == null) continue;

                doors[i].position = Vector3.Lerp(
                    startPositions[i],
                    targetPositions[i],
                    t
                );
            }

            yield return null;
        }
    }

    private IEnumerator ShowWaveText()
    {
        if (waveText == null) yield break;

        float t = 0f;

        while (t < textFadeDuration)
        {
            t += Time.deltaTime;
            waveText.alpha = Mathf.Lerp(0f, 1f, t / textFadeDuration);
            yield return null;
        }

        waveText.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        t = 0f;

        while (t < textFadeDuration)
        {
            t += Time.deltaTime;
            waveText.alpha = Mathf.Lerp(1f, 0f, t / textFadeDuration);
            yield return null;
        }

        waveText.alpha = 0f;
    }
}