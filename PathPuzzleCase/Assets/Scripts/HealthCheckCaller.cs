using UnityEngine;

public class HealthCheckCaller : MonoBehaviour{

    private LevelHealthChecker levelHealthChecker;

    void Start(){
        // LevelHealthChecker and HealthCheckerCaller are on the same GameObject
        levelHealthChecker = GetComponent<LevelHealthChecker>();

        if (levelHealthChecker != null){

            levelHealthChecker.CheckHealth(); // checker caller to test case level manually.

        }else{
            Debug.LogError("LevelHealthChecker component not found!");
        }
    }
}
