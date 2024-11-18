using Cogobyte.ProceduralIndicators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    public static IndicatorManager instance;
    public GameObject straightIndicatorPrefab;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //CreatStraightIndicator(Vector3.zero,new Vector3(0,0,8));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatStraightIndicator(Vector3 startPos,Vector3 endPos)
    {
        Debug.Log("lllllll");
        var sIndicaight = Instantiate(straightIndicatorPrefab);
        var arrowObj = sIndicaight.GetComponent<ArrowObject>();
        arrowObj.arrowPath.editedPath = new List<Vector3>
        {
            startPos,
            endPos
        };
        arrowObj.hideIndicator= false;
        arrowObj.updateArrowMesh();
        Destroy(sIndicaight,3);

    }

}
