using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenRoom : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    BoxCollider2D cldr;
    BoundsInt area;

    void Start() {
        cldr = GetComponent<BoxCollider2D>();


        Vector3Int position = Vector3Int.FloorToInt(cldr.bounds.min);
        Vector3Int size = Vector3Int.FloorToInt(cldr.bounds.size + new Vector3Int(0, 0, 1));
        area = new BoundsInt(position, size);
    }

    public void ToggleTilemapTiles(bool value = false) {
        foreach (Vector3Int point in area.allPositionsWithin) {
            tilemap.SetTileFlags(point, TileFlags.None);
            tilemap.SetColor(point, new Color(255, 255, 255, value ? 1 : 0.2f));
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.CompareTo("Player") == 0) {
            ToggleTilemapTiles(false);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag.CompareTo("Player") == 0) {
            ToggleTilemapTiles(true);
        }
    }
}
