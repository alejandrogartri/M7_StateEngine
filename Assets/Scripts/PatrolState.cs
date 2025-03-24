using UnityEngine;

public class PatrolState : IenemyState
{
    //Inicializa el estado patrulla y el primer punto del array de coordenadas de desplazamiento.
    EnemyAI myEnemy;
    private int nextWayPoint = 0;

    public PatrolState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    //Coloca la luz del enemigo a verde y le mueve hacia una posición del array de coordenadas.
    //A través del IF, va gestionando si ha llegado al punto de destino para hacerle cambiar a otro punto.
    public void UpdateState()
    {
        myEnemy.enemyColor.SetColor("_Color", Color.green);
        // Debug.Log(myEnemy.enemyColor);
        myEnemy.navMeshAgent.destination = myEnemy.waypoints[nextWayPoint].position;

        if (!myEnemy.navMeshAgent.pathPending &&
            myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance &&
            myEnemy.navMeshAgent.velocity.sqrMagnitude == 0)
        { 
            nextWayPoint = (nextWayPoint + 1) % myEnemy.waypoints.Length;
        }
    }

    //Detiene el avance del enemigo y se coloca en estado alerta.
    public void GoToAlertState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.alertState;
    }

    //Detiene el avance del enemigo y se coloca en estado ataque.
    public void GoToAttackState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.attackState;
    }

    //No hace nada porque ya está en modo patrulla.
    public void GoToPatrolState()
    {
    }

    //En caso de que algo entre en el collider y se mantenga, y el enemigo esté en modo patrulla, pasará a modo alerta.
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GoToAlertState();
        }
    }

    public void OnTriggerExit(Collider col){}

}
