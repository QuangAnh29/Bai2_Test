using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public GameObject cubePrefab;
    public int gridSize = 100;
    public float gapSize = 1f; 
    private float maxMoveDistance;
    public float speed = 0.5f;

    private Transform[,] cubes;
    private Vector3[,] originalPositions;
    private float[,] moveDirections;

    void Start()
    {
        maxMoveDistance = cubePrefab.gameObject.transform.localScale.x * 2;
        
        cubes = new Transform[gridSize, gridSize];
        originalPositions = new Vector3[gridSize, gridSize];
        moveDirections = new float[gridSize, gridSize];

        // Generate cubes
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 cubePosition = new Vector3(x * (cubePrefab.transform.localScale.x + gapSize), 0, y * (cubePrefab.transform.localScale.z + gapSize));
                GameObject cubeObject = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
                cubes[x, y] = cubeObject.transform;
                originalPositions[x, y] = cubePosition;
                moveDirections[x, y] = Random.Range(-speed, speed);
            }
        }

        
    }

    void Update()
    {
        // Move cubes
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 currentPosition = cubes[x, y].position;
                Vector3 newPosition = currentPosition + Vector3.up * moveDirections[x, y];
                if (newPosition.y - originalPositions[x, y].y > maxMoveDistance || newPosition.y - originalPositions[x, y].y < -maxMoveDistance)
                {
                    moveDirections[x, y] *= -1f;
                    newPosition = currentPosition + Vector3.up * moveDirections[x, y];
                }
                cubes[x, y].position = newPosition;
            }
        }
    }
}
