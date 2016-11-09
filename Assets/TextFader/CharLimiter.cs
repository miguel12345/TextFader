using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharLimiter : BaseMeshEffect, IMeshModifier {

	[SerializeField]
	int m_NumberOfLetters = -1;

	public int NumberOfLetters {
		get {
			return m_NumberOfLetters;
		}
		set {
			
			if( value == m_NumberOfLetters )
				return;
			
			m_NumberOfLetters = value;
			graphic.SetVerticesDirty();
		}
	}

	static List<UIVertex> tmpVertices = new List<UIVertex>();
	static UIVertex[] tmpVerticesQuad = new UIVertex[4];

	public override void ModifyMesh(VertexHelper toFill)
	{
		if (!IsActive())
			return;

		if( m_NumberOfLetters == -1 )
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

		int numberOfVertices = m_NumberOfLetters * 4;
		toFill.Clear();

		for( int i = 0; i < numberOfVertices && i < tmpVertices.Count; i++ )
		{
			int tempVertsIndex = i & 3;
			tmpVerticesQuad[tempVertsIndex] = tmpVertices[i];
			if( tempVertsIndex == 3 )
			{
				toFill.AddUIVertexQuad( tmpVerticesQuad );
			}
		}
	}
}
