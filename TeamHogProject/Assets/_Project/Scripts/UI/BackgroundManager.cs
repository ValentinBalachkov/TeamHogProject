using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject _backObject;
    [SerializeField] private GameObject _endGameBackObject;
    [SerializeField] private BackgroundController _prepareBack;
    [SerializeField] private SpriteRenderer _flashImage;
    
    private List<Transform> _backgrounds = new();

    private void Start()
    {
        _prepareBack.StopMoveProp = true;
    }

    public void StartMoveBack()
    {
        _prepareBack.Init(true);
        _prepareBack.StopMoveProp = false;
        StartCoroutine(MoveBackCoroutine());
    }

    public void SetDefaultScreen()
    {
        foreach (var back in _backgrounds)
        {
            Destroy(back.gameObject);
        }
        _backgrounds.Clear();
        StopAllCoroutines();
        _prepareBack.StopMoveProp = true;
        _prepareBack.transform.position = Vector3.zero;
    }

    public void EndGameBack(GameStateMachine gameStateMachine)
    {
        StopAllCoroutines();
        StartCoroutine(EndGameMoveBackCoroutine(gameStateMachine));
    }
    

    private IEnumerator EndGameMoveBackCoroutine(GameStateMachine gameStateMachine)
    {
        foreach (var back in _backgrounds)
        {
            Destroy(back.gameObject);
        }
        _backgrounds.Clear();
        
        var backgrounds = Instantiate(_endGameBackObject, transform).transform;
        backgrounds.localPosition = new Vector3(0, 0, 0);

        while ( backgrounds.position.y > -46.57)
        {
            yield return null;
        }

        float f = 0;
        while (_flashImage.color.a < 1)
        {
            f += 0.01f;
            _flashImage.color = new Color(255, 255, 255, f);
        }

        backgrounds.GetComponent<BackgroundController>().StopMoveProp = true;
        yield return new WaitForSeconds(1);
        Destroy(backgrounds.gameObject);


        gameStateMachine.ChangeState<StateMachine.PreparationState>();
        f = 1;
        while (_flashImage.color.a > 0)
        {
            f -= 0.01f;
            _flashImage.color = new Color(255, 255, 255, f);
        }
    }
    
    private IEnumerator MoveBackCoroutine()
    {
        var backgrounds = Instantiate(_backObject, transform).transform;
        backgrounds.localPosition = new Vector3(0, 11, 0);
        _backgrounds.Add(backgrounds);
        backgrounds.GetComponent<BackgroundController>().OnDestroy += OnDestroyBack;

        while ( backgrounds.position.y > -80.4)
        {
            yield return null;
        }

        
        StartCoroutine(MoveBackCoroutine());
    }

    private void OnDestroyBack(Transform back)
    {
        _backgrounds.Remove(back);
    }
}
