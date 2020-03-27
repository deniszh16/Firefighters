﻿using UnityEngine;

public class Drop : MonoBehaviour, IPoolable
{
    [Header("Эффект огненных брызг")]
    [SerializeField] private ParticleSystem spray;

    // Скорость падения
    private float speed;

    // Ссылка на компонент
    private Rigidbody2D rigbody;

    private void Awake()
    {
        rigbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Активация объекта из пула
    /// </summary>
    public void ActivateObject()
    {
        gameObject.SetActive(true);

        // Определяем случайную скорость падения
        speed = Random.Range(5.1f, 6.5f);
    }

    private void FixedUpdate()
    {
        // Перемещаем каплю вниз с указанной скоростью и коэффициентом замедления
        rigbody.MovePosition(rigbody.position + Vector2.down * (speed * Slowdown.coefficient) * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Отображение эффекта огненных брызг от капли
    /// </summary>
    public void ShowSplashEffect()
    {
        // Перемещаем эффект брызг к огненной капле
        spray.transform.position = transform.position;
        spray.Play();
    }

    /// <summary>
    /// Деактивация объекта при возвращении в пул
    /// </summary>
    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}