using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    //Jezeli obiekt wejdzie w collider gracza (który to ma specjalny, osobny box collider z wyzwalaczem) to uruchamiamy grawitacje dla kolumny i zaczyna ona spadać na mapę.
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            Debug.Log("Iam in!");
            GetComponent<Collider>().attachedRigidbody.useGravity = true;
        } 
    }
}
