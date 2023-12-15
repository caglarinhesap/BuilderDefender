using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    private void Awake()
    {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repairCost = missingHealth / 2;

            ResourceAmount[] resourceCost = new ResourceAmount[] {
                new ResourceAmount { resourceType = goldResourceType, amount = repairCost } };

            if (ResourceManager.Instance.CanAfford(resourceCost))
            {
                //Can afford the repair
                ResourceManager.Instance.SpendResources(resourceCost);
                healthSystem.HealFull();
            }
            else
            {
                //Can't afford the repairs!
                TooltipUI.Instance.Show("Cannot afford repair cost!", new TooltipUI.TooltipTimer { timer = 2f });
            }
        });
    }
}
