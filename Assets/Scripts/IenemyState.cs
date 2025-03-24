using UnityEngine;

//Interface con los métodos obligatorios que tienen que usarse cada vez que se cree un enemigo.
public interface IenemyState 
{
    void UpdateState();
    void GoToAttackState();
    void GoToAlertState();
    void GoToPatrolState();
   // void GoToIdleState();

    void OnTriggerEnter(Collider col);
    void OnTriggerStay(Collider col);
    void OnTriggerExit(Collider col);
    //void Impact();
}