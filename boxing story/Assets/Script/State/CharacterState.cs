using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState {

    static public CharacterStateIdle stateIdle = new CharacterStateIdle();
    static public CharacterStateJob stateJob = new CharacterStateJob();
    static public CharacterStateMove stateMove = new CharacterStateMove();

    protected bool isRangeOut(int x, int y)
    {
        return (x < 0) || (y < 0) || (x >= Constants.Width) || (y >= Constants.Height);
    }
    public abstract bool Move(Character _character,Tile dest);
    public abstract Tile FindNext(Character _character, TileController tileController);
    public abstract CharacterState CheckTile(Character _character);
}
