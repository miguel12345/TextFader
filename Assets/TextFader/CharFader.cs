using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharFader : BaseMeshEffect, IMeshModifier {

	static List<UIVertex> tmpVertices = new List<UIVertex>();
	static UIVertex[] tmpVerticesQuad = new UIVertex[4];

	int charIndex = -1;
	byte alpha;

	public void SetCharAlpha(int charIndex, byte alpha) {
		this.charIndex = charIndex;
		this.alpha = alpha;
		graphic.SetVerticesDirty();
	}

	public override void ModifyMesh(VertexHelper toFill)
	{
		if (!IsActive())
			return;

		if( charIndex == -1 )
			return;

		int vertCount = toFill.currentVertCount;

		for( int i = 0; i < vertCount; i++ )
		{
			if( tmpVertices.Count < (i+1) )
			{
				tmpVertices.Add( new UIVertex() );
			}
			UIVertex vert = tmpVertices[i];
			toFill.PopulateUIVertex( ref vert, i );
			tmpVertices[i] = vert;
		}

		toFill.Clear();

		for( int i = 0; i < vertCount; i++ )
		{
			int tempVertsIndex = i & 3;
			tmpVerticesQuad[tempVertsIndex] = tmpVertices[i];


			int letterIndex = i / 4;

			if( charIndex == letterIndex )
			{
				var color = tmpVerticesQuad[tempVertsIndex].color;
				color.a = alpha;
				tmpVerticesQuad[tempVertsIndex].color = color;
			}


			if( tempVertsIndex == 3 )
			{
				toFill.AddUIVertexQuad( tmpVerticesQuad );
			}
		}
	}
}
