using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateJob : CharacterState
{
    public override bool Move(Character _character, Tile dest)
    {
        return false;
    }
    public override Tile FindNext(Character _character, TileController tileController)
    {

        return null;
    }
    public override CharacterState CheckTile(Character _character)
    {

        return CharacterState.stateJob;
    }
}
