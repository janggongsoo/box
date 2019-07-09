using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct IntVector2
{
    public int x, y;

    public IntVector2(int _x,int _y)
    {
        x = _x;
        y = _y;
    }
}

public abstract class Character : MonoBehaviour {

    CharacterState state ;
    int money = 150;

    int maxHp = 1000;
    int hp;
    int str = 1000;
    int spd = 1000;
    
    Facility useFacility;

    protected TileController tileController;
    protected FacilityController facilityController;

    Tile currentTile;
    Tile destTile;
    Tile nextTile;

    protected Facility beforeFacility;
    public Animator anim;
    
    public string textPoolName = "TextPool";

    void Start()
    {
        tileController = GameObject.Find("TileController").GetComponent<TileController>();
        facilityController = GameObject.Find("FacilityController").GetComponent<FacilityController>();
        hp = maxHp;
        anim = GetComponent<Animator>();
        state = CharacterState.stateIdle;
        currentTile = tileController.getTile(0, 0);
        nextTile = state.FindNext(this, tileController);
    }
    void Update()
    {
        if (state.Move(this, nextTile))
        {
              Debug.Log(state);
            currentTile = nextTile;
            state = state.CheckTile(this);

            nextTile = state.FindNext(this, tileController);
        }
    }
    public void setDestTile(Tile _dest)
    {
        destTile = _dest;
        Debug.Log(destTile.GetPosX() + " " + destTile.GetPosY());
    }
    public Tile getDestTile()
    {
        return destTile;
    }
    public Tile getCurrentTile()
    {
        return currentTile;
    }

    public void addHp(int _hp)
    {
        hp = Mathf.Min(maxHp, hp + _hp);

        ShowMoveText("Hp " + _hp);
    }
    public bool isFullHp()
    {
        return hp >= maxHp;
    }
    public int getHp()
    {
        return hp;
    }
    public void addMoney(int _moeny)
    {
        money += _moeny;

        ShowMoveText("Price " + _moeny);
    }
    public bool isEmptyMoney(int price)
    {
        return money-price >= 0;
    }
    public int getMoney()
    {
        return money;
    }
    public void addStr(int _str)
    {
        str += _str;
        ShowMoveText("Strong " + _str);
    }
    public int getStr()
    {
        return str;
    }
    public void addSpd(int _spd)
    {
        spd += _spd;
        ShowMoveText("Speed " + _spd);
    }
    public int getSpd()
    {
        return spd;
    }

    // 건물에 들어가 애니메이션 동작. (건물별로 애니메이션 번호)
    public void EnterFacility(Facility _facility)
    {
        beforeFacility = _facility;
        useFacility = _facility;
        useFacility.EnterFacility(this);

        state = CharacterState.stateJob;

        StartCoroutine(UseFacilityInTime(useFacility.getUseTime()));
    }
    IEnumerator UseFacilityInTime(float time)
    {
        yield return new WaitForSeconds(time);
        while (useFacility.InteractWithCharacter(this))
        {
            yield return new WaitForSeconds(time);
        }
        ExitFacility();
    }
    public void ExitFacility()
    {
        useFacility.ExitFacility();
        useFacility = new NullFacility();

        
        state = CharacterState.stateIdle;
        nextTile = state.FindNext(this, tileController);
    }

    private void ShowMoveText(string _text)
    {
        MoveText textObj = PoolManager.Instance.getObject(textPoolName,transform).GetComponent<MoveText>();
        textObj.setTextMessage(_text);
        textObj.transform.position = transform.position + new Vector3(0, 0, -1);
        textObj.gameObject.SetActive(true);
    }
    public abstract Tile FindDestination();
}
