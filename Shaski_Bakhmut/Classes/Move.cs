using Shaski_Bakhmut;
using System.Collections.Generic;

public class Move
{
    public Checker Checker { get; set; }
    public string StartPosition { get; set; }
    public List<int> IntermediatePosition { get; set; }
    public string EndPosition { get; set; }

    public Move(Checker piece, string startPosition, List<int> intermediatePosition, string endPosition)
    {
        Checker = piece;
        StartPosition = startPosition;
        IntermediatePosition = intermediatePosition;
        EndPosition = endPosition;
    }
}
