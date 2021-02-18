using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0f;
    public Renderer bgRend;
    public Renderer GameRoadRend;
    //private float offset;
    //private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        //mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //speed for cave background 0.02f, need to move it to a variable
        bgRend.material.mainTextureOffset += new Vector2(0.05f * Time.deltaTime, 0);
        GameRoadRend.material.mainTextureOffset += new Vector2(ObstacleSpawnController.Instance.speedAmountToAdd * Time.deltaTime, 0) / 20;


        //offset += (Time.deltaTime * scrollSpeed) / 10;
        //offset += (ObstacleSpawnController.Instance.speedAmountToAdd * Time.deltaTime) / 18;
        //mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }

}
