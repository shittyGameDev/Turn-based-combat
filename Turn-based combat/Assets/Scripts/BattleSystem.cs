using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    private BattleController battleController;
    private BattleView battleView;

    private Unit playerUnit;
    private Unit enemyUnit;

    void Start()
    {
        // Instantiate Units
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        // Initialize Controller and View
        battleController = new BattleController(playerUnit, enemyUnit, this);
        battleView = GetComponent<BattleView>();

        // Provide the controller to the view
        battleView.SetBattleController(battleController);

        // Start the battle
        battleController.StartBattle();
    }

    public BattleController GetBattleController()
    {
        return battleController;
    }
}