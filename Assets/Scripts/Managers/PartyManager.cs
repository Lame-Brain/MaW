using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private bool moving;

    private Party_Class _party;
    private void Awake()
    {
        _party = FindObjectOfType<Party_Class>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moving = false;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[0] = new Item(401, true, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[1] = new Item(401, true, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[2] = new Item(401, false, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[3] = new Item(39, true, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[4] = new Item(217, true, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[5] = new Item(199, true, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[6] = new Item(204, true, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Party_Class>().Bag[7] = new Item(206, false, false);

        Blobber.Roster.ROSTER.Add(new Character_C("Dair du'Corbett", Blobber.CharacterClassEnum.Warrior, 18, 16, 14, 9, 7, 11, 1));

        Blobber.Roster.ROSTER[0].Weapon_Slot = new Item(55, true, true);
        //for (int i = 0; i < Blobber.Roster.ROSTER[0].SpellBook.Length; i++) Blobber.Roster.ROSTER[0].SpellBook[i] = true;

        Blobber.Roster.ROSTER.Add(new Character_C("Sir Prize", Blobber.CharacterClassEnum.Knight, 16, 14, 18, 9, 7, 11, 1));
        Blobber.Roster.ROSTER.Add(new Character_C("Father Gilbert", Blobber.CharacterClassEnum.Cleric, 9, 14, 16, 7, 18, 11, 1));
        Blobber.Roster.ROSTER.Add(new Character_C("Jinx", Blobber.CharacterClassEnum.Rogue, 16, 18, 11, 9, 7, 14, 1));
        Blobber.Roster.ROSTER.Add(new Character_C("Tattersail", Blobber.CharacterClassEnum.Mage, 7, 16, 14, 18, 7, 11, 1));
        Blobber.Roster.ROSTER.Add(new Character_C("Bek", Blobber.CharacterClassEnum.Mage, 7, 16, 14, 18, 7, 11, 1));

        Blobber.Roster.ROSTER[0].Party_Slot = 0;
        Blobber.Roster.ROSTER[1].Party_Slot = 1;
        Blobber.Roster.ROSTER[2].Party_Slot = 2;
        Blobber.Roster.ROSTER[3].Party_Slot = 3;
        Blobber.Roster.ROSTER[4].Party_Slot = 4;
        Blobber.Roster.ROSTER[5].Party_Slot = 5;

        Debug.Log(Blobber.Roster.ROSTER[0].SaveCharacter());
        Debug.Log(Blobber.Roster.ROSTER[1].SaveCharacter());
        Debug.Log(Blobber.Roster.ROSTER[2].SaveCharacter());
        Debug.Log(Blobber.Roster.ROSTER[3].SaveCharacter());
        Debug.Log(Blobber.Roster.ROSTER[4].SaveCharacter());
        Debug.Log(Blobber.Roster.ROSTER[5].SaveCharacter());

        

        FindObjectOfType<Character_Sheet>().Update_Sheet(Blobber.Roster.ROSTER[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)) MovePartyForward();
        if (Input.GetKeyUp(KeyCode.DownArrow)) MovePartyBackward();
        if (Input.GetKeyUp(KeyCode.RightArrow)) TurnPartyClockwise();
        if (Input.GetKeyUp(KeyCode.LeftArrow)) TurnPartyCounterClockwise();

    }

    void MovePartyForward()
    {
        if (!moving) StartCoroutine(MoveParty_CR(1, .1f));
    }
    void MovePartyBackward()
    {
        if (!moving) StartCoroutine(MoveParty_CR(-1, .1f));
    }
    void TurnPartyClockwise()
    {
        if(!moving) StartCoroutine(TurnParty_CR(1, .1f));
    }
    void TurnPartyCounterClockwise()
    {
        if (!moving) StartCoroutine(TurnParty_CR(-1, .1f));
    }

    IEnumerator MoveParty_CR(int _dir, float _timeToMove)
    {
        Vector3 _currentPos = this.transform.position;
        Vector3 _locationToMoveTo = _currentPos + (this.transform.forward * _dir);
        float t = 0f;
        moving = true;

        while (t < 1)
        {
            t += Time.deltaTime / _timeToMove;
            this.transform.position = Vector3.Lerp(_currentPos, _locationToMoveTo, t);
            if (t > 0.8f)
                this.transform.position = _locationToMoveTo;
            yield return null;
        }

        moving = false;
    }
    IEnumerator TurnParty_CR(int _dir, float _timeToTurn)
    {
        Quaternion _currentPos = this.transform.rotation;
        Quaternion _locationToTurnTo = _currentPos * Quaternion.Euler(0, (90 * _dir), 0);
        float t = 0f;
        moving = true;

        while (t < 1)
        {
            t += Time.deltaTime / _timeToTurn;
            this.transform.rotation = Quaternion.Lerp(_currentPos, _locationToTurnTo, t);
            if (t > 0.8f)
                this.transform.rotation = _locationToTurnTo;
            yield return null;
        }

        moving = false;
    }

}
