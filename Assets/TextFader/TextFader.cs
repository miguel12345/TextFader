using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFader : MonoBehaviour {

	public bool IgnoreWhiteSpaces = true;
	public bool StartOnEnable = true;
	public float CharFadeDuration = 0.1f;

	CharLimiter CharLimiter;
	CharFader CharFader;
	Text text;

	int currentLetterIndex;
	float currentCharFadeTime;

	void OnEnable() {

		if( CharLimiter == null )
			CharLimiter = gameObject.AddComponent<CharLimiter>();

		if( CharFader == null )
			CharFader = gameObject.AddComponent<CharFader>();

		CharLimiter.enabled = true;
		CharFader.enabled = true;

		text = GetComponent<Text>();

		if( StartOnEnable )
		{
			PerformAnimation();
		}
	}

	void OnDisable() {
		CharLimiter.enabled = false;
		CharFader.enabled = false;
	}

	public void PerformAnimation() {
		currentLetterIndex = 0;
		currentCharFadeTime = 0.0f;
	}

	void Update() {

		if( IgnoreWhiteSpaces )
		{
			var str = text.text;

			if( currentLetterIndex >= str.Length )
				return;

			var currentChar = str[currentLetterIndex];
			if( currentChar == ' ' )
			{
				currentLetterIndex++;
				Update();
				return;
			}
		}

		CharLimiter.NumberOfLetters = currentLetterIndex + 1;

		currentCharFadeTime += Time.deltaTime;
		float progress = currentCharFadeTime / CharFadeDuration;
	
		if( progress >= 1.0f )
		{
			CharFader.SetCharAlpha( currentLetterIndex, 255 );

			currentLetterIndex++;
			currentCharFadeTime = 0.0f;

			if( currentLetterIndex >= text.text.Length )
			{
				enabled = false;
			}
		}
		else
		{
			byte alpha = (byte)(progress * 255);
			CharFader.SetCharAlpha( currentLetterIndex, alpha );

		}
	}
}
