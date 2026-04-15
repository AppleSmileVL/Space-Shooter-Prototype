using UnityEngine;

public class SpawnObjectByPropertisList : MonoBehaviour // Этот класс отвечает за создание объектов на основе списка свойств, заданных в массиве properties.
                                                        // Он использует префаб для создания объектов и устанавливает их родителем указанный Transform.
                                                        // Метод SpawnInEditMode вызывается через контекстное меню и выполняет следующие действия:
                                                        // удаляет все дочерние объекты у parent, а затем создает новые объекты на основе префаба и
                                                        // применяет к ним свойства из массива properties с помощью интерфейса IScriptableObjectProperty.
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ScriptableObject[] properties;

    [ContextMenu(nameof(SpawnInEditMode))]
    public void SpawnInEditMode()
    {
        if (Application.isPlaying == true) return;

        GameObject[] allObject = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            allObject[i] = parent.GetChild(i).gameObject;
        }

        for (int i = 0; i < allObject.Length; i++)
        {
            DestroyImmediate(allObject[i]);
        }

        for (int i = 0; i < properties.Length; i++)
        {
            GameObject go = Instantiate(prefab, parent); // Используем Instantiate для создания объекта на основе префаба и устанавливаем его родителем parent
            IScriptableObjectProperty scriptableObjectProperty = go.GetComponent<IScriptableObjectProperty>(); // Получаем компонент IScriptableObjectProperty с созданного объекта
            scriptableObjectProperty.ApplyProperty(properties[i]); // Вызываем метод ApplyProperty, передавая ему соответствующий ScriptableObject из массива properties
        }
    }
}
