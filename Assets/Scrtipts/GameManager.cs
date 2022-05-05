using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform uiRoot;

    private ServicesHub servicesHub;
    private DataHub dataHub;


    public ServicesHub ServicesHub => servicesHub;
    public DataHub DataHub => dataHub;
    public Transform UiRoot => uiRoot;


    private void Awake()
    {
        Instance = this;

        dataHub = new DataHub();
        servicesHub = new ServicesHub();
    }
}
