# PlateUpTestCubes
Create cube prefabs for testing

```cs
using TestCubes;
GameObject prefab = TestCubeManager.GetPrefab<T>(
    float scaleX,
    float scaleY,
    float scaleZ,
    Material material,
    bool collider);
```

```cs
using TestCubes;
GameObject prefab = TestCubeManager.GetPrefab(
    string guid,
    float scaleX,
    float scaleY,
    float scaleZ,
    Material material,
    bool collider);
```
