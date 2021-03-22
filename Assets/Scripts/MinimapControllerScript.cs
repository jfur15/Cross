using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapControllerScript : MonoBehaviour
{
    public GameObject MapController;
    public GameObject pixel;
    public GameObject player;
    GameObject playerPixel;
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Texture2D mapTexture = MapController.GetComponent<MapControllerScript>().map;
        Sprite mapSprite = Sprite.Create(mapTexture, new Rect(0f, 0f, mapTexture.width, mapTexture.height), new Vector2(0.5f,0.5f));
        gameObject.GetComponent<Image>().sprite = mapSprite;
        //create a object 
        playerPixel = Instantiate(pixel);
        //px.transform.parent = gameObject.transform;
        playerPixel.transform.SetParent(gameObject.transform);
        RectTransform rt = playerPixel.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(0, 0, 0);
        rt.localScale = new Vector3(1,1,1);
    }

    public void SetPlayer(Vector2 pos)
    {

        playerPixel.GetComponent<RectTransform>().localPosition = new Vector3(pos.x, pos.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 coo = player.GetComponent<PlayerController>().coordinates;
        playerPixel.GetComponent<RectTransform>().localPosition = new Vector3(coo.x-32, coo.y, 0);
    }
}
