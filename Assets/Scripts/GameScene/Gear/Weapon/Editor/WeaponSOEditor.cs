using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;


[CustomEditor(typeof(WeaponSO))]
public class WeaponSOEditor : Editor
{
    private static List<Type> DataCompTypes = new List<Type>();

    private WeaponSO dataSO;

    private void OnEnable()
    {
        dataSO = target as WeaponSO;
    }
    public override void OnInspectorGUI()
    {
        foreach (var type in DataCompTypes)
        {
            if (GUILayout.Button(type.Name))
            {
                var comp = Activator.CreateInstance(type) as ComponentData;

                if (comp == null)
                {
                    return;
                }
                dataSO.AddData(comp);
            }
        }

        base.OnInspectorGUI();
    }


    [DidReloadScripts]
    private static void OnRecompile()
    {
        Debug.Log("Recomplied");
        var assemblis = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblis.SelectMany(t => t.GetTypes());
        var filltredTypes = types.Where(t => t.IsSubclassOf(typeof(ComponentData)) && !t.ContainsGenericParameters && t.IsClass);

        DataCompTypes = filltredTypes.ToList();

    }
}
