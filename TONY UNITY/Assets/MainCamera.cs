using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public float smoothTime = 10f;
    

    Transform target;
    float tLX, tLY, bRX, bRY;

    Vector2 velocity; // necesario para el suavizado de cámara

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        // Forzar la resolución si no estamos en versión Web
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            Screen.SetResolution(800, 800, true);
    }

    void Update()
    {
        /* v1 classic
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
        */
        /* v2 con límites
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x,tLX,bRX),
            Mathf.Clamp(target.position.y,bRY,tLY),
            transform.position.z
        );
        */
        /* v3 con límites y suavizado
            NOTA: REQUIERE LLAMAR FAST MOVE AL CAMBIAR DE MAPA */
        float posX = Mathf.Round(
            Mathf.SmoothDamp(transform.position.x,
                target.position.x, ref velocity.x, smoothTime) * 100) / 100;
        float posY = Mathf.Round(
            Mathf.SmoothDamp(transform.position.y,
                target.position.y, ref velocity.y, smoothTime) * 100) / 100;
        transform.position = new Vector3(
            Mathf.Clamp(posX, tLX, bRX),
            Mathf.Clamp(posY, bRY, tLY),
            transform.position.z
        );

    }

    public void SetBound(GameObject map)
    {
        Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
        float cameraSize = Camera.main.orthographicSize;

        tLX = map.transform.position.x + cameraSize;
        tLY = map.transform.position.y - cameraSize;
        bRX = map.transform.position.x + config.NumTilesWide - cameraSize;
        bRY = map.transform.position.y - config.NumTilesHigh + cameraSize;

        FastMove();
    }

    public void FastMove()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
    }
    private void OnGUI()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(
          new Rect(
              pos.x - 200,                   // posición x de la barra
              Screen.height - pos.y + 250,   // posición y de la barra
              80,                           // anchura de la barra    
              30                            // altura de la barra  
          ),"puntos"            // texto de la barra
      );
    }
  
}