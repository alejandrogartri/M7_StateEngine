using UnityEngine;
using System.Collections;

public class AlertState : IenemyState
{
    EnemyAI myEnemy;
    float currentRotationTime = 0;

    public AlertState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    //Coloca la luz del enemigo a amarillo. Le hace girar 360 para buscar al jugador. Si no lo encuentra,
    //vuelve al modo patrulla. Si lo encuentra en su radio de ataque, pasa al estado de ataque.
    public void UpdateState()
    {
        myEnemy.enemyColor.SetColor("_Color", Color.yellow);
        myEnemy.transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 360 / myEnemy.rotationTime, 0f);
        if(currentRotationTime > myEnemy.rotationTime)
        {
            currentRotationTime = 0;
            GoToPatrolState();
        }
        else
        {
            RaycastHit hit;
            if(Physics.Raycast(
                new Ray(
                    new Vector3(myEnemy.transform.position.x,
                                0.5f,
                                myEnemy.transform.position.z),
                    myEnemy.transform.forward*100f),
                out hit))
            {
                if(hit.collider.gameObject.tag == "Player")
                {
                    GoToAttackState();
                }
            }
        }
        currentRotationTime += Time.deltaTime;
    }

    //No hay nada porque ya está en el modo alerta.
    public void GoToAlertState() { }

    //Inicializa el método ataque si le atacan o detecta al enemigo.
    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.attackState;
    }

    //Inicializa el método patrulla si no encuentra al jugador.
    public void GoToPatrolState()
    {
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.patrolState;
    }

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerExit(Collider col){ }

    public void OnTriggerStay(Collider col){ }

}
