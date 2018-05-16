using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{

    public int width = 256;
    public int height = 256;

    public float scale = 20f;
    public float time;
    public float offset = 0f;
    public float offsetX = 100f;
    public float offsetY = 100f;

    public GameObject cube;

    /// <summary>
    /// Testing
    /// </summary>
    public List<GameObject> cubes;
    public int widthOfCubes = 0;
    public int lengthOfCubes = 0;
    public float perlinNoise = 0f;
    public float frequency = 0f;
    public float magnitude = 0f;
    public float cubeScale;

    private float maxX = 0f;
    private float maxZ = 0f;

    public bool AnimatePerlin = false;
    private void Start()
    {
        
        //offsetX = Random.Range(0f, 99999f);
        //offsetY = Random.Range(0f, 99999f);
        maxX = widthOfCubes * cubeScale;
        maxZ = lengthOfCubes * cubeScale;

        for (int i = 0; i < widthOfCubes; i++)
        {
            for (int j = 0; j < lengthOfCubes; j++)
            {
                

                //perlinNoise = Mathf.PerlinNoise(floai * refinement, j * refinement);
                GameObject go = Instantiate(cube);
                go.transform.localScale = new Vector3(cubeScale,cubeScale,cubeScale);
                go.transform.position = new Vector3(i, 0.0f, j);
                

                cubes.Add(go);
            }
        }


    }


    void Update()
    {
        //float time = 0.0f;
        if (AnimatePerlin)
        {
            time += (Time.deltaTime * offset);
            foreach (GameObject cube in cubes)
            {
                
                float scaledX = ScaleACoord(0.0f, cubeScale * widthOfCubes, 0.0f, 1.0f, cube.transform.position.x);
                float scaledZ = ScaleACoord(0.0f, cubeScale * widthOfCubes, 0.0f, 1.0f, cube.transform.position.z);
                perlinNoise = Mathf.PerlinNoise((scaledX * frequency + time), (scaledZ * frequency + time));
                //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(cube.transform.position.x, perlinNoise * magnitude, cube.transform.position.z);
                //cubes.Add(go);

            }
        }
        else
        {
            foreach (GameObject cube in cubes)
            {
                float scaledX = ScaleACoord(0.0f, cubeScale * widthOfCubes, 0.0f, 1.0f, cube.transform.position.x);
                float scaledZ = ScaleACoord(0.0f, cubeScale * widthOfCubes, 0.0f, 1.0f, cube.transform.position.z);
                perlinNoise = Mathf.PerlinNoise((scaledX * frequency + time), (scaledZ * frequency + time));

                cube.transform.position = new Vector3(cube.transform.position.x, perlinNoise * magnitude, cube.transform.position.z);

            }
        }


    }
    public float ScaleACoord(float originalMin, float originalMax, float newMin, float newMax , float originalValue)
    {

        float OldRange, NewRange, NewValue;
        OldRange = originalMax - originalMin;
        NewRange = newMax - newMin;

        NewValue = (((originalValue - originalMin) * NewRange) / OldRange ) + newMin;
    // first, normalize the coord.
    //normalizedCoord = (coord - 0.0f) / (1.0f - 0.0f);
    //scaledCoord = Mathf.Clamp(coord, 0.0f, 1.0f);


    

    return NewValue;
    }



}



    


