using K_PathFinder.Samples;
using UnityEngine;

public class Detection : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            
            GameObject agentObj = GameObject.Find("Agent");
            
            if (agentObj != null)
            {
                
                PointOfView pov = agentObj.GetComponent<PointOfView>();
                if (pov != null) pov.playerFinded = true;
                
                
                EnemyAI ai = agentObj.GetComponent<EnemyAI>();
                if (ai != null) ai.playerFinded = true;
                
                Debug.Log("Agent spotted player! Pursuit begins!");
            }
        }
    }
}