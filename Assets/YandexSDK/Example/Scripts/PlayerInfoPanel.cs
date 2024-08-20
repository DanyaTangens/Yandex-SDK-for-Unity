﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YandexSDK.Example.Scripts
{
    public class PlayerInfoPanel: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerId;
        [SerializeField] private TextMeshProUGUI payingStatus;
        [SerializeField] private TextMeshProUGUI playerPhotoText;
        [SerializeField] private RawImage playerPhoto;

        public void Authorization()
        {
            playerName.text = YandexGame.Instance.playerName;
            playerId.text = YandexGame.Instance.playerId;
            payingStatus.text = YandexGame.Instance.payingStatus;
            playerPhotoText.text = YandexGame.Instance.playerPhoto;
        }

        private void OnEnable()
        {
            YandexGame.onGetDataEvent += Authorization;
        }

        private void OnDisable()
        {
            YandexGame.onGetDataEvent -= Authorization;
        }
    }
}
