using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Vector2 _detectorSize = Vector2.one;

    private Enemy _enemy;
    private Player _player;

    private float _healthTransferRate = 1;
    private float _abilityDuration = 6;

    private Coroutine _coroutineVampirism;

    private bool isAbilityActive = false;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, _detectorSize, 0, _layerMask);

        if (Input.GetKey(KeyCode.Q)  && isAbilityActive == false)
        {
            if (collider.TryGetComponent<Enemy>(out _enemy) && _coroutineVampirism == null)
            {
                _coroutineVampirism = StartCoroutine(TransferHealth());
            }
        }
    }

    private void ApplyVampirism(float healthToTransfer)
    {
        if (_enemy != null)
        {
            _player.Healing(healthToTransfer);
            _enemy.ApplyDamage(healthToTransfer);
        }
    }

    private void StopVampirism()
    {
        if (_coroutineVampirism != null)
        {
            StopCoroutine(_coroutineVampirism);
            _coroutineVampirism = null;
        }
    }

    private IEnumerator TransferHealth()
    {
        float elapsedTime = 0;
        isAbilityActive = true;

        while (elapsedTime < _abilityDuration)
        {
            elapsedTime += Time.deltaTime;

            float healthToTransfer = _healthTransferRate * Time.deltaTime;

            ApplyVampirism(healthToTransfer);

            yield return null;
        }

        isAbilityActive = false;
        StopVampirism();
    }
}
