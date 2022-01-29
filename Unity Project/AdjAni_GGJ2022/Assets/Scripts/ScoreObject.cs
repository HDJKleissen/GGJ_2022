using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScoreObject
{
    public float DogTime;
    public float OwnerTime;
    public int TotalBreakables;
    public int BreakablesBroken;
    public int BreakablesFixed;

    //calc score in some magical way
    public int FinalScore {
        get {
            float score = GameController.Instance.OwnerTimeAmount;
            score -= BreakablesBroken;
            score -= OwnerTime;
            score -= DogTime;
            return (int)score;
        }
    }
}

