using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Transform cam;

    private NavMeshAgent agent;
    private Collider other;
    private List<Renderer> previousRenderers = new List<Renderer>();

    private void Start()
    {
        // Defini agent navmesh
        agent = GetComponent<NavMeshAgent>();        
    }

    private void Update()
    {
        // Interage se existe outro objeto e pressionou 'E'
        if (other != null && Input.GetKeyUp(KeyCode.E))
        {
            other.gameObject.GetComponent<IInteractable>().Interact();
            other = null;
        }

        HandleObjectInFront();
    }

    private void FixedUpdate()
    {
        // Seta destino do agent com base na direcao do input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 position = this.transform.position + direction;   
        
        agent.SetDestination(position);
    }

    private void OnTriggerEnter(Collider _other)
    {
        // Salva o outro objeto interagivel
        if (_other.CompareTag("Interactable"))
        {
            Debug.Log("Jogador entrou");
            other = _other;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // Descarta o objeto interagivel
        if (_other.CompareTag("Interactable"))
        {
            Debug.Log("Jogador saiu");
            other = null;
        }
    }

    private void HandleObjectInFront()
    {
        // Reseta transparencia
        foreach (var renderer in previousRenderers) { SetTransparency(renderer, 1f); }
        previousRenderers.Clear();

        // Configura ponto final da linha
        Vector3 yOffset = new Vector3(0, 1.5f, 0);
        Vector3 direction = this.transform.position - cam.position + yOffset;

        // Debug e hit do da linha
        Debug.DrawRay(cam.position, direction, Color.red);
        RaycastHit[] hits = Physics.RaycastAll(cam.position, direction);

        // Coloca transparente para cada hit da linha caso objeto seja "escondivel"
        foreach (var hit in hits)
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null && hit.collider.CompareTag("Hideable"))
            {
                SetTransparency(renderer, 0f);
                previousRenderers.Add(renderer);
            }
        }
    }

    void SetTransparency(Renderer renderer, float alpha)
    {
        foreach (var material in renderer.materials)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;

            if (alpha < 1.0f)
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.SetInt("_Surface", 1);

                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                material.SetShaderPassEnabled("DepthOnly", false);
                material.SetShaderPassEnabled("SHADOWCASTER", true);

                material.SetOverrideTag("RenderType", "Transparent");

                //material.DisableKeyword("_ALPHATEST_ON");
                //material.EnableKeyword("_ALPHABLEND_ON");   // Totalmente transparente
                material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");

            }
            else
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.SetInt("_Surface", 0);

                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

                material.SetShaderPassEnabled("DepthOnly", true);
                material.SetShaderPassEnabled("SHADOWCASTER", true);

                material.SetOverrideTag("RenderType", "Opaque");

                //material.DisableKeyword("_ALPHATEST_ON");
                //material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            }
        }
    }
}
