﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    // Перечисление неигровых сцен
    public enum Scenes { Menu = 1, Levels, Leaderboard, Training }

    /// <summary>
    /// Переход на указанную сцену
    /// </summary>
    /// <param name="scene">номер сцены</param>
    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoToScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public void GoToScene(string scene)
    {
        _ = SceneManager.LoadSceneAsync(scene);
    }

    /// <summary>
    /// Перезагрузка текущей сцены
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}