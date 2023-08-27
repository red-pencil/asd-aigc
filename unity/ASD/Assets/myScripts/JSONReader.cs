using UnityEngine;
 
public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
 
    void Start()
    {
        Employees employeesInJson = JsonUtility.FromJson<Employees>(jsonFile.text);

        Debug.Log(employeesInJson.employees[0].firstName);

        foreach (Employee employee in employeesInJson.employees)
        {
            Debug.Log("Found employee: " + employee.firstName + " " + employee.lastName);
        }
    }
}