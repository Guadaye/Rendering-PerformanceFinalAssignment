using UnityEngine;

public class Env_Objects : MonoBehaviour
{

    public GameObject sample1;
    public GameObject sample2;

    GameObject innerBound;
    GameObject outerBound;


    // Start is called before the first frame update
    void Start()
    {
       // sample1 = GameObject.Find("sample_1");
       // sample2 = GameObject.Find("sample_2");

        innerBound = GameObject.Find("innerBound");
        outerBound = GameObject.Find("outerBound");


        for (int i = 0; i < 32; i++)
        {
            float innerDistance = Vector3.Magnitude(innerBound.transform.position);
            float outerDistance = Vector3.Magnitude(outerBound.transform.position);

            if (sample1)
            {
                GameObject newGo;
                newGo = Instantiate(sample1);
               // newGo.transform.position = new Vector3(Random.Range(innerDistance, outerDistance),
               //     sample1.transform.position.y,
               //     Random.Range(innerDistance, outerDistance));

                float angle = Random.Range(0, 360);
                //Debug.Log(angle);
                float x = Random.Range(innerDistance, outerDistance) * Mathf.Sin(angle);
                float z = Random.Range(innerDistance, outerDistance) * Mathf.Cos(angle);
                newGo.transform.position = new Vector3(x,
                    sample1.transform.position.y,
                    z);
                newGo.transform.parent = transform;
            }
            else if(sample2)
            {
                
                GameObject newGo;
                newGo = Instantiate(sample2);
              //  newGo.transform.position = new Vector3(Random.Range(innerDistance, outerDistance),
               //     sample2.transform.position.y,
               //     Random.Range(innerDistance, outerDistance));

                float angle = Random.Range(0, 360);
                float x = Random.Range(innerDistance, outerDistance) * Mathf.Sin(angle);
                float z = Random.Range(innerDistance, outerDistance) * Mathf.Cos(angle);
                newGo.transform.position = new Vector3(x,
                    sample2.transform.position.y,
                    z);
                newGo.transform.parent = transform;
            }

        }
        DynamicBatching();
    }

    void DynamicBatching()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combineArray = new CombineInstance[meshFilters.Length-1];
        int i = 1;
        while(i<meshFilters.Length)
        {
            combineArray[i - 1].mesh = meshFilters[i].sharedMesh;
            combineArray[i - 1].transform = meshFilters[i].transform.localToWorldMatrix;

            meshFilters[i - 1].gameObject.SetActive(false);
            i++;
        }

        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combineArray);
        transform.gameObject.SetActive(true);
        MeshRenderer mr = (MeshRenderer)transform.GetComponent("MeshRenderer");
        if(sample1)
        mr.material= sample1.transform.GetComponent<MeshRenderer>().material;
        else if (sample2)
            mr.material = sample2.transform.GetComponent<MeshRenderer>().material;

    }
}
