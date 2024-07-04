using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[Serializable]
public class ComponentData
{

    public Type ComponentDependency { get; protected set; }
}


/*[Serializable]
public class ComponentData//<T> : ComponentData where T : AttackData
{
    [field: SerializeField] public T[] AttackData { get; private set; }


}
*/
