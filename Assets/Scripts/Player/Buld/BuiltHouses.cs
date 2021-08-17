using System.Collections.Generic;

public class BuiltHouses
{
    private Dictionary<BuildingConfiguration, int> _housesBuilt = new Dictionary<BuildingConfiguration, int>();

    public bool IsUnlockBuilding(BuildingConfiguration buildingConfiguration)
    {
        if (_housesBuilt.ContainsKey(buildingConfiguration) == false)
        {
            return true;
        }
        else
        {
            if (_housesBuilt[buildingConfiguration] >= buildingConfiguration.MaxAmount)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public void AddBuilding(BuildingConfiguration buildingConfiguration)
    {
        _housesBuilt[buildingConfiguration]++;
    }

    public void NewBuild(BuildingConfiguration buildingConfiguration)
    {
        if (_housesBuilt.ContainsKey(buildingConfiguration) == false)
        {
            _housesBuilt.Add(buildingConfiguration,0);
        }
    }
}