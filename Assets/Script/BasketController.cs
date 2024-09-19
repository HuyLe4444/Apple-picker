using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketController : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI scoreText;
    int scoreCount = 0;
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.TryGetComponent<ICollectables>(out ICollectables collectable))
        {
            if(collectable.IsCollectable()){
                print("Apple Collected");
                Destroy(collision.gameObject);
                scoreCount++;
                scoreText.text = $"Score: {scoreCount}";
            } else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                // Time.timeScale = 0;
            }
        }
    }

    void Update() {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
}
