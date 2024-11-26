/**
 * =============================================================
 * File         :GameInfo.cs
 * Summary      :ゲーム情報クラス
 * Author       :kinketsu patron (https://kinketsu-patron.com)
 * Ver          :1.0
 * Date         :2024/06/30
 * =============================================================
 */

// =======================================================
// using
// =======================================================
using System.Text.Json.Serialization;

namespace Pachislot_DataCounter.Models.Entity
{
    /// <summary>
    /// ゲーム情報クラス
    /// </summary>
    public class GameInfo
    {
        [JsonPropertyName( "game" )]
        public int Game { get; set; }

        [JsonPropertyName( "totalgame" )]
        public int TotalGame { get; set; }

        [JsonPropertyName( "in" )]
        public int In { get; set; }

        [JsonPropertyName( "out" )]
        public int Out { get; set; }

        [JsonPropertyName( "diff" )]
        public int Diff { get; set; }

        [JsonPropertyName( "rb" )]
        public int RB { get; set; }

        [JsonPropertyName( "bb" )]
        public int BB { get; set; }

        [JsonPropertyName( "duringrb" )]
        public bool DuringRB { get; set; }

        [JsonPropertyName( "duringbb" )]
        public bool DuringBB { get; set; }
    }
}
