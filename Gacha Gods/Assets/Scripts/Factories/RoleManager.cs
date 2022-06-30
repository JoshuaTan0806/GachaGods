using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/RoleManager")]
public class RoleManager : Factories.FactoryBase
{
    public static List<Role> AllRoles => allRoles;
    static List<Role> allRoles = new List<Role>();

    [SerializeField]
    List<Role> roles;

    public override void Initialize()
    {
        foreach (var item in roles)
        {
            allRoles.Add(item);
        }
    }
}
