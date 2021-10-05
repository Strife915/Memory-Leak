using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PathCreator : MonoBehaviour
{
    public static PathCreator instance;
    public int objectCount;
    public int redTileCount, blueTileCount, yellowTileCount;
    public int listIndex = 0;
    public float spaceBetweenTiles;
    public GameObject tilePrefab;
    public GameObject pathStartingPoint;
    public GameObject trapSpawnerParent;

    public List<GameObject> myPath;
    public List<Color> myPatchColor;
    public List<Color> sortedPathColor;
    public Image firstTile, secondTile, thirdTile;
    public Text firstTileCount, secondTileCount, thirdTileCount;

    void Start()
    {
        instance = this;
        myPatchColor = new List<Color>();
        myPath = new List<GameObject>();
        sortedPathColor = new List<Color>();
        PathListCreator();
        TileCreator();
        SetTrapSpawnerParentPosition();
        Debug.Log("In total red tile count : " + redTileCount + " Blue tile count " + blueTileCount + " Yellow tile count " + yellowTileCount);
        SetPathMap(firstTile, secondTile, thirdTile);
        SetFirstTileCounts(firstTile.color);
        SetSecondTileCounts(secondTile.color);
        SetThirdCounts(thirdTile.color);
    }
    //This Function create n colors randomly than sort them asc or desc to make a path for player
    public void PathListCreator() 
    {
        for (int k=0;k<objectCount;k++)
        {
            int i = Random.Range(0, 3);
            if (i == 0)
            {
                myPatchColor.Add(Color.red);
                redTileCount++;
            }
            else if (i == 1)
            {
                myPatchColor.Add(Color.cyan);
                blueTileCount++;
            }
            else
            {
                myPatchColor.Add(Color.yellow);
                yellowTileCount++;
            }
        }
        int j = Random.Range(0,2);
        if(j == 1)
        {
            sortedPathColor = myPatchColor.OrderBy(x => x.r).ThenBy(x => x.g).ThenBy(x => x.b).ToList();
        }
        else
        {
            sortedPathColor = myPatchColor.OrderByDescending(x => x.r).ThenBy(x => x.g).ThenBy(x => x.b).ToList();
        }
    }
    //This Function instantiate tile main object with 3 tile than it call other function to paint them
    private void TileCreator() 
    {
        for (int i = 1; i <= objectCount; i++)
        {
            GameObject tilePeace = Instantiate(tilePrefab);
            tilePeace.transform.position = pathStartingPoint.transform.position + new Vector3(0, 0, spaceBetweenTiles * i);
            myPath.Add(tilePeace);
            GameObject leftTile = tilePeace.GetComponentInChildren<TilePieceHolder>().leftTile;
            GameObject midTile = tilePeace.GetComponentInChildren<TilePieceHolder>().midTile;
            GameObject rightTile = tilePeace.GetComponentInChildren<TilePieceHolder>().rightTile;
            TileColorChoice(leftTile, midTile, rightTile,i-1);
        }
    }
    //This function takes 3 tiles from other function and paint one of three accordingly player path than rest two gets color random
    public GameObject TileColorChoice(GameObject _leftTile,GameObject _midTile,GameObject _rightTile,int listIndex) 
    {
        GameObject mainPathTile, regularPathTile_,regularPathTile;
        MeshRenderer leftMeshRenderer = _leftTile.GetComponent<MeshRenderer>();
        MeshRenderer middleMeshRenderer = _midTile.GetComponent<MeshRenderer>();
        MeshRenderer  rightMeshRenderer = _rightTile.GetComponent<MeshRenderer>();

        int i = Random.Range(0, 3);
        {
            switch(i)
            {
                case 0:
                    mainPathTile = _leftTile;
                    regularPathTile = _midTile;
                    regularPathTile_ = _rightTile;
                    leftMeshRenderer.material.color = sortedPathColor[listIndex];
                    leftMeshRenderer.material.SetColor("_EmissionColor", leftMeshRenderer.material.color);
                    middleMeshRenderer.material.color = myPatchColor[Random.Range(0, objectCount)];
                    middleMeshRenderer.material.SetColor("_EmissionColor", middleMeshRenderer.material.color);
                    rightMeshRenderer.material.color = myPatchColor[Random.Range(0, objectCount)];
                    rightMeshRenderer.material.SetColor("_EmissionColor", rightMeshRenderer.material.color);
                    return _leftTile;
                case 1:
                    mainPathTile = _midTile;
                    regularPathTile = _leftTile;
                    regularPathTile = _rightTile;
                    leftMeshRenderer.material.color = myPatchColor[Random.Range(0, objectCount)];
                    leftMeshRenderer.material.SetColor("_EmissionColor", leftMeshRenderer.material.color);
                    middleMeshRenderer.material.color = sortedPathColor[listIndex];
                    middleMeshRenderer.material.SetColor("_EmissionColor", middleMeshRenderer.material.color);
                    rightMeshRenderer.material.color = myPatchColor[Random.Range(0, objectCount)];
                    rightMeshRenderer.material.SetColor("_EmissionColor", rightMeshRenderer.material.color);
                    return _midTile;
                case 2:
                    mainPathTile = _rightTile;
                    regularPathTile = _leftTile;
                    regularPathTile_ = _midTile;
                    middleMeshRenderer.material.color = myPatchColor[Random.Range(0, objectCount)];
                    middleMeshRenderer.material.SetColor("_EmissionColor", middleMeshRenderer.material.color);
                    leftMeshRenderer.material.color = myPatchColor[Random.Range(0, objectCount)];
                    leftMeshRenderer.material.SetColor("_EmissionColor",leftMeshRenderer.material.color);
                    rightMeshRenderer.material.color = sortedPathColor[listIndex];
                    rightMeshRenderer.material.SetColor("_EmissionColor", rightMeshRenderer.material.color);
                    return _rightTile;
            }
        }
        return default;
    }
    //This function checks for sort of the colors of images first than paint them accordingly order of sort
    public void SetPathMap(Image firstTile,Image secondTile,Image thirdTile)
    {
        bool secondFound = false;
        bool thirdFound = false;
        firstTile.color = sortedPathColor[0];
        if (!secondFound && !thirdFound)
        {
            for (int i = 0; i < objectCount-1; i++)
            {
                if (sortedPathColor[i] != sortedPathColor[i + 1] && !secondFound && !thirdFound)
                {
                    secondFound = true;
                    secondTile.color = sortedPathColor[i + 1];
                }
                else if(secondFound && secondTile.color != sortedPathColor[i])
                {
                    thirdFound = true;
                    thirdTile.color = sortedPathColor[i];
                }
            }
        }
    } 
    //This 3 function sets number texts of map images
    public void SetFirstTileCounts(Color firstColor)
    {
        if(firstTile.color == Color.red)
        {
            firstTileCount.text = redTileCount.ToString();
        }
        else if(firstTile.color == Color.cyan)
        {
            firstTileCount.text = blueTileCount.ToString();
        }
        else
        {
            firstTileCount.text = yellowTileCount.ToString();
        }
    }
    public void SetSecondTileCounts(Color secondColor)
    {
        if (secondTile.color == Color.red)
        {
            secondTileCount.text = redTileCount.ToString();
        }
        else if (secondTile.color == Color.cyan)
        {
            secondTileCount.text = blueTileCount.ToString();
        }
        else
        {
            secondTileCount.text = yellowTileCount.ToString();
        }
    }
    public void SetThirdCounts(Color thirdColor)
    {
        if (thirdTile.color == Color.red)
        {
            thirdTileCount.text = redTileCount.ToString();
        }
        else if (thirdTile.color == Color.cyan)
        {
            thirdTileCount.text = blueTileCount.ToString();
        }
        else
        {
            thirdTileCount.text = yellowTileCount.ToString();
        }
    }
    public void SetTrapSpawnerParentPosition()
    {
        trapSpawnerParent.transform.position = Vector3.forward * ((objectCount * spaceBetweenTiles)+30f);
    }
    



}
