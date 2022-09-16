using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
public class NavMeshController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;


    #region Singlenton
    public static NavMeshController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion 

    void Start()
    {
       
    }

    public void SetNavMesh()
    {
        UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        NavMeshAgent agente = GetComponent<NavMeshAgent>();
        agente.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
