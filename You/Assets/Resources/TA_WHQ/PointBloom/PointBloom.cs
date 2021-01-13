using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointBloom : MonoBehaviour
{
    private Mesh mesh;

    private GameObject point;

    private List<Vector3> pointsWorldPosList = new List<Vector3>();

    private List<GameObject> pointsList = new List<GameObject>();

    private SkinnedMeshRenderer skin;

    [SerializeField]
    private float refreshPosTime = 0.1f;

    private float refreshPosTimer = 0;

    private float refreshBlinTime = 0.25f;

    private float refreshBlinTimer = 0;

    private bool isFirstRefresh = true;

    private bool suspend = false;

    private Material[] originMaterials;

    private Material pointBloomMaterial;

    [SerializeField, Range(0, 1), Tooltip("闪光密度")]
    private float blinDensity = 0.25f;

    private float endEffectTimer = 0;

    [SerializeField, Tooltip("逐渐消失时间")]
    public float endEffectTime = 3;

    void Awake()
    {
        skin = GetComponent<SkinnedMeshRenderer>();
        mesh = new Mesh();
        originMaterials = skin.materials;
        point = Resources.Load<GameObject>("TA_WHQ/PointBloom/Point");
        pointBloomMaterial = Resources.Load<Material>("TA_WHQ/PointBloom/PointBloomMaterial");
    }

    void FixedUpdate()
    {
        RefreshPointBloom();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            EffectEnd();
        }
    }

    private void OnEnable()
    {
        skin.materials = new Material[] {
            pointBloomMaterial
        };
        foreach (var p in pointsList)
        {
            p.SetActive(false);
        }
        suspend = false;
    }

    private void OnDisable()
    {
        suspend = true;
        foreach (var p in pointsList)
        {
            p.SetActive(false);
        }
        skin.materials = originMaterials;
    }

    private void RefreshPointBloom()
    {
        RefreshVerticesPos();
        StartCoroutine(RefreshVerticesBlin());
    }

    private void RefreshVerticesPos()
    {
        refreshPosTimer += Time.fixedDeltaTime;
        if (refreshPosTimer > refreshPosTime)
        {
            refreshPosTimer = 0;
            RefreshVerticesPosList();
            for (int i = 0; i < pointsList.Count; ++i)
            {
                pointsList[i].transform.position = pointsWorldPosList[i];
            }
        }


    }

    private void RefreshVerticesPosList()
    {
        pointsWorldPosList.Clear();
        skin.BakeMesh(mesh);
        mesh.GetVertices(pointsWorldPosList);
        pointsWorldPosList = pointsWorldPosList.Distinct().ToList();
        for (int i = 0; i < pointsWorldPosList.Count; ++i)
        {
            pointsWorldPosList[i] = transform.TransformPoint(pointsWorldPosList[i]);
        }
        if (isFirstRefresh)
        {
            isFirstRefresh = false;
            foreach (var pos in pointsWorldPosList)
            {
                GameObject go = Instantiate(point, pos, Quaternion.identity);
                go.transform.SetParent(transform);
                go.SetActive(false);
                pointsList.Add(go);
            }
        }
    }

    private IEnumerator RefreshVerticesBlin()
    {

        refreshBlinTimer += Time.fixedDeltaTime;
        if (refreshBlinTimer > refreshBlinTime)
        {
            refreshBlinTimer = 0;
            for (int i = 0; i < pointsList.Count; ++i)
            {
                int baseRandom = 1000;
                if (!suspend)
                {
                    pointsList[i].SetActive(Random.Range(0, baseRandom) < baseRandom * blinDensity ? true : false);
                } else
                {
                    break;
                }
                yield return new WaitForSeconds(refreshBlinTime / pointsList.Count);
            }
        }
    }

    private IEnumerator PointBloomEnd()
    {
        suspend = true;
        endEffectTimer = 0;
        float deltaTime = endEffectTime / pointsList.Count;

        for (int i = 0; i < pointsList.Count; ++i)
        {
            pointsList[i].SetActive(false);
            yield return new WaitForSeconds(deltaTime);
        }
    }
    public void EffectEnd()
    {
        StartCoroutine(PointBloomEnd());
    }
}
