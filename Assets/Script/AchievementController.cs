﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    // Instance ini mirip seperti GameManager, fungsinya adalah membuat sistem singleton 
    // untuk memudahkan pemanggilan script yang bersifat manager dari script lain 
    private static AchievementController _instance = null;
    public static AchievementController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<AchievementController>();
            }

            return _instance;
        }
    }

    [SerializeField] private Transform _popUpTransform;
    [SerializeField] private Text _popUpText;
    [SerializeField] private float _popUpShowDuration = 3f;
    [SerializeField] private List<AchievementData> _achievementList;

    private float _popUpShowDurationCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_popUpShowDurationCounter > 0)
        {
            // Kurangi durasi ketika pop up durasi lebih dari 0
            _popUpShowDurationCounter -= Time.unscaledDeltaTime;

            // Lerp adalah fungsi linier interpolation, digunakan untuk mengubah value secara perlahan
            _popUpTransform.localScale = Vector3.LerpUnclamped(_popUpTransform.localScale, Vector3.right, 0.5f);
        }
    }

    public void UnlockAchievement(AchievementType type, string value)
    {
        // Mencari data Achievement
        AchievementData achievement = _achievementList.Find(a=> a.Type == type && a.Value == value);
        
        if(achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            ShowAchievementPopUp(achievement);
        }
    }

    private void ShowAchievementPopUp(AchievementData achievement)

    {
        _popUpText.text = achievement.Title;
        _popUpShowDurationCounter = _popUpShowDuration;
        _popUpTransform.localScale = Vector2.right;
    }
}

// System.Serializeable digunakan agar object dan script bisa di-serialize
// data bisa di-inputkan dari Inspector, jika tidak terdapat ini, maka variabel tidak akan muncil di inspector
[System.Serializable]

public class AchievementData
{
    public string Title;
    public AchievementType Type;
    public string Value;
    public bool IsUnlocked;
}

public enum AchievementType
{
    UnlockResource
}