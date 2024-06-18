using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreToAdd = 100; // Skor yang akan ditambahkan saat musuh dihancurkan
    public Text scoreText; // Referensi ke objek UI Text untuk menampilkan skor
    public int score = 0; // Variabel untuk menyimpan skor saat ini

    private void Start()
    {
        // Mengatur skor awal
        score = 0;
        UpdateScoreText();
    }

    public void AddScore()
    {
        // Menambahkan skor saat musuh dihancurkan
        score += scoreToAdd;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Memperbarui tampilan skor di UI
        scoreText.text = " " + score.ToString();
    }
}
