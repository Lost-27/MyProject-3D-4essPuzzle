using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialSetter2D : MonoBehaviour
{
    private Renderer _renderer;

    private Renderer Renderer
    {
        get
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();
            return _renderer;
        }
    }

    public void SetSingleMaterial(Material material)
    {
        Renderer.material = material;
    }
}