﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    [Header("Максимум открытых окон")] // при старте уровня
    [SerializeField] private int maximum;

    [Header("Промежуток появления жильцов")]
    [SerializeField] private float minSeconds;
    [SerializeField] private float maxSeconds;

    [Header("Количество жителей")]
    [SerializeField] private int victims;

    public int Victims { get { return victims; } }

    // Список открытых окон доступных для персонажей
    public static List<Window> windows = new List<Window>();

    private void Start()
    {
        // Определяем количество окон с пожаром при старте
        var amount = Random.Range(2, maximum + 1);

        for (int i = 0; i < amount; i++)
        {
            // Определяем номер случайного окна (из всех окон в пуле)
            var number = Random.Range(0, PoolsManager.QuantityObjects(ListingPools.Pools.Windows.ToString()));

            // Получаем объект из пула и получаем его компонент
            var window = PoolsManager.GetObjectFromPool(ListingPools.Pools.Windows.ToString(), number).GetComponent<Window>();

            // Открываем данное окно
            window.OpenWindow = true;

            // Активируем объект
            window.ActivateObject();
        }

        // Запускаем открытие других окон
        StartCoroutine(OpenWindows());

        // Запускаем прыжки персонажей
        StartCoroutine(CharacterJumping());
    }

    /// <summary>
    /// Переодическое открытие закрытых окон (появление пожара)
    /// </summary>
    private IEnumerator OpenWindows()
    {
        // Пока в пуле есть доступные окна и активен игровой режим
        while (PoolsManager.QuantityObjects(ListingPools.Pools.Windows.ToString()) > 0 && LevelManager.Mode == "play")
        {
            var seconds = Random.Range(5, 12);
            yield return new WaitForSeconds(seconds);

            // Определяем номер случайного окна (из всех окон в пуле)
            var number = Random.Range(0, PoolsManager.QuantityObjects(gameObject.name));

            // Получаем объект из пула и получаем его компонент
            var window = PoolsManager.GetObjectFromPool(ListingPools.Pools.Windows.ToString(), number).GetComponent<Window>();
            
            // Активируем объект
            window.ActivateObject();
        }
    }

    /// <summary>
    /// Переодическое появление жильцов в открытых окнах
    /// </summary>
    private IEnumerator CharacterJumping()
    {
        // Пока есть жители и активен игровой режим
        while (victims > 0 && LevelManager.Mode == "play")
        {
            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));

            if (windows.Count > 0)
            {
                // Определяем случайное окно из доступных
                var window = Random.Range(0, windows.Count);

                // Показываем персонажа в окне
                windows[window].ShowVictims();

                // Удаляем окно из доступных
                windows.RemoveAt(window);

                victims--;
                // Выводим количество оставшихся персонажей
                VictimsCounting.QuantityChange?.Invoke();
            }
        }
    }
}