using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZombieMovement : MonoBehaviour
{
    private string looseScreen = "GameOver";
    private string winScreen = "GameWon";
    public Animator animator;
    public GameObject lifeStrip;

    //Zmienne określające kolejno: aktualny kąt obrotu postaci, płynność obrotu, nominalny kąt obrotu oraz aktualny zycie postaci.
    private float movementAngle=0;
    private float smooth = 5.0f;
    private float tiltAngle = 60.0f;
    private int health=100;

    //W metodzie wywoływanej po inizjalizacji obiektu dekalrujemy cykliczne wywoływanie funkcji  dekrementującej zycie postaci (okres wywołania wynosi 2 s)
    void Start()
    {
        InvokeRepeating("DecreaseHealth", 2.0f, 1.0f);
    }

    //W kazdej klatce sprawdzamy czy gracz wciska przyciski odpowiedzialne za sterowanie postacia i dokonujemy odpowiedniej obslugi.
    void Update()
    {
        bool moveForeward = Input.GetKey(KeyCode.W);
        animator.SetBool("Moves", moveForeward);    //Jezeli gracz wcisnal klawisz "w" to uruchamiamy animacje chodzenia (nie jest to animacja chodzenia w miejscu - obsługuje ona chodzenie w przód zwalniając z koniecznosci dokonania transformacji obiektu gracza).
        
        movementAngle = moveForeward ? Input.GetAxis("Horizontal") * tiltAngle : 0; //Odczytujemy czy gracz idzie do przodu. Jezeli tak to zmiennej moveLeftRigth przypisujemy wartosc osi horyzontalnej (zmienianej klawiszami 'a' i 'd'). W innym przypadku przypisujemy zero. Unikamy obracania się w miesjcu.
        Quaternion target = Quaternion.Euler(0, movementAngle, 0);  
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);    //Obracamy obiekt gracza o wskazany kąt.
        
    }

    //Obsługa wyzwalaczy w colliderach (jezeli znajdziemy się wewnątrz collidera)
    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "RadioactiveCell") {        //Jezeli gracz wejdzie w collider radioaktywnego ogniwa paliwowego to usuwamy obiekt i zwiększamy zycie.
            Destroy(collider.gameObject);
            IncreaseHealth();
        } else  if (collider.tag == "Finish") {
            SceneManager.LoadScene(winScreen);      //Jezeli gracz wejdzie w collider tunelu to konczymy gre poprzez zmiane sceny.
        }
    }

    private void DecreaseHealth() {
        health = health > 0 ? health-7 : 0;     //Sprawdzamy czy gracz "zyje" i jezeli tak to zmniejszamy jego zycie. Wykorzystałem operator Elvisa poniewaz w przypadku smeirci mamy animacje trwajacą 2 sekundy w trakcie której pasek zycia nadal my się zmniejszał.
        float lifeStripLength = ((float)health)/100;        //wyznaczamy nową wartość paska zdrowia.
        lifeStrip.transform.localScale = new Vector3(lifeStripLength,1,1);      //Ustawiamy szerokosc paska zdrowia adekwatnie to liczby punktow zycia.

        if (health <= 0 ) {
            animator.SetBool("Dies", true);         //Jezeli po odjeciu punktow zycia ich liczba spadnie ponizej 1 to uruchamiamy animacje śmierci.
            StartCoroutine(DeatchCoroutine());     //Wywolujemy metode konczącą gre.
        }
    }

    //Zwiększamy zycie bohatera. Uniemozliwiamy przykroczenie wartości 100 HP.
    private void IncreaseHealth() {
        health += 10;
        health = health > 100 ? 100 : health;
    }

    //Metoda kończy grę po dwoch sekundach od jej wywołania dzięki czemu mozemy podziwiać animację upadania.
    IEnumerator DeatchCoroutine() {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(looseScreen);
    }
    
}
