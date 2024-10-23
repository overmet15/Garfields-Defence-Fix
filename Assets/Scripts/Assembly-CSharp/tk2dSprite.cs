using UnityEngine;

[AddComponentMenu("2D Toolkit/tk2dSprite")]
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class tk2dSprite : tk2dBaseSprite
{
	private Mesh mesh;

	private Vector3[] meshVertices;

	private Color[] meshColors;

	private void Awake()
	{
		if ((bool)collection)
		{
			if (_spriteId < 0 || _spriteId >= collection.Count)
			{
				_spriteId = 0;
			}
			Build();
		}
	}

	protected void OnDestroy()
	{
		if ((bool)mesh)
		{
			Object.Destroy(mesh);
		}
	}

	public override void Build()
	{
		tk2dSpriteDefinition tk2dSpriteDefinition2 = collection.spriteDefinitions[base.spriteId];
		meshVertices = new Vector3[tk2dSpriteDefinition2.positions.Length];
		meshColors = new Color[tk2dSpriteDefinition2.positions.Length];
		SetPositions(meshVertices);
		SetColors(meshColors);
		Mesh mesh = new Mesh();
		mesh.vertices = meshVertices;
		mesh.colors = meshColors;
		mesh.uv = tk2dSpriteDefinition2.uvs;
		mesh.triangles = tk2dSpriteDefinition2.indices;
		GetComponent<MeshFilter>().mesh = mesh;
		this.mesh = GetComponent<MeshFilter>().sharedMesh;
		UpdateMaterial();
		CreateCollider();
	}

	protected override void UpdateGeometry()
	{
		UpdateGeometryImpl();
	}

	protected override void UpdateColors()
	{
		UpdateColorsImpl();
	}

	protected override void UpdateVertices()
	{
		UpdateVerticesImpl();
	}

	protected void UpdateColorsImpl()
	{
		SetColors(meshColors);
		mesh.colors = meshColors;
	}

	protected void UpdateVerticesImpl()
	{
		tk2dSpriteDefinition tk2dSpriteDefinition2 = collection.spriteDefinitions[base.spriteId];
		SetPositions(meshVertices);
		mesh.vertices = meshVertices;
		mesh.uv = tk2dSpriteDefinition2.uvs;
		mesh.bounds = GetBounds();
	}

	protected void UpdateGeometryImpl()
	{
		if (!(mesh == null))
		{
			tk2dSpriteDefinition tk2dSpriteDefinition2 = collection.spriteDefinitions[base.spriteId];
			if (mesh.vertexCount > tk2dSpriteDefinition2.positions.Length)
			{
				mesh.triangles = tk2dSpriteDefinition2.indices;
				meshVertices = new Vector3[tk2dSpriteDefinition2.positions.Length];
				meshColors = new Color[tk2dSpriteDefinition2.positions.Length];
				SetPositions(meshVertices);
				SetColors(meshColors);
				mesh.vertices = meshVertices;
				mesh.colors = meshColors;
				mesh.uv = tk2dSpriteDefinition2.uvs;
				mesh.bounds = GetBounds();
			}
			else
			{
				meshVertices = new Vector3[tk2dSpriteDefinition2.positions.Length];
				meshColors = new Color[tk2dSpriteDefinition2.positions.Length];
				SetPositions(meshVertices);
				SetColors(meshColors);
				mesh.vertices = meshVertices;
				mesh.colors = meshColors;
				mesh.uv = tk2dSpriteDefinition2.uvs;
				mesh.triangles = tk2dSpriteDefinition2.indices;
				mesh.bounds = GetBounds();
			}
		}
	}

	protected override void UpdateMaterial()
	{
		if (GetComponent<Renderer>().sharedMaterial != collection.spriteDefinitions[base.spriteId].material)
		{
			GetComponent<Renderer>().material = collection.spriteDefinitions[base.spriteId].material;
		}
	}

	protected override int GetCurrentVertexCount()
	{
		if (meshVertices == null)
		{
			return 0;
		}
		return meshVertices.Length;
	}
}
