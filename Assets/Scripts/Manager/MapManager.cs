using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    [System.Serializable]
    public class Obstacle
    {
        public GameObject[] obstacles;
    }
    [SerializeField]
    private SkyBoxData skyBoxData;

    [SerializeField]
    public Obstacle[] maps;

    private void Start()
    {
        for(int i = 0; i < StageMapController.Instance.isTrigger.Length; i++)
        {
            if(StageMapController.Instance.isTrigger[i])
            {
                for(int j =0; j < maps[i].obstacles.Length; j++)
                {
                    Destroy(maps[i].obstacles[j]);
                }
            }
        }
        ChangeSkyBox(0);
    }

    public void ChangeSkyBox(int num)
    {
        RenderSettings.skybox = skyBoxData.skyBoxMaterial[num];
    }

}
