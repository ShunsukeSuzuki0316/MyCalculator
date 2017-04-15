using System;
namespace MyCalculator
{
	public interface IClipBoard
	{
		String GetTextFromClipBoard();
		bool SetTextToClipBoard(string text);
	}
}
