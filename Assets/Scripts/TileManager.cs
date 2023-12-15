using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> tilesAtivos = new List<GameObject>();
    
    public Transform transformacaoJogador;
    public GameObject[] prefabsTiles;
    public GameObject[] prefabsTilesBeiraDaEstrada;

    public float zSpawn = 0;
    public float comprimentoTile = 30;
    
    public int numeroDeTiles;
    public int numeroDeTilesBeiraDaEstrada;
    int tilesBeiraDaEstradaCriados = 0;
    void Start()
    {
        IniciarTiles();
    }
    void Update()
    {
        VerificarSpawnTiles();
    }

    public void SpawnTile(int indiceTile)
    {
        GameObject objetoTile = Instantiate(prefabsTiles[indiceTile], transform.forward * zSpawn, transform.rotation);
        tilesAtivos.Add(objetoTile);
        zSpawn += comprimentoTile;
    }

    public void SpawnTilesBeiraDaEstrada(int indiceTileEsquerdo, int indiceTileDireito)
    {
        GameObject esquerdo = Instantiate(prefabsTilesBeiraDaEstrada[indiceTileEsquerdo], new Vector3(0.5F + (comprimentoTile / 4), 0, comprimentoTile * tilesBeiraDaEstradaCriados), transform.rotation);
        GameObject direito = Instantiate(prefabsTilesBeiraDaEstrada[indiceTileDireito], new Vector3(-(0.5F + (comprimentoTile / 4)), 0, comprimentoTile * tilesBeiraDaEstradaCriados), transform.rotation);
        
        tilesAtivos.Add(esquerdo);
        tilesAtivos.Add(direito);

        tilesBeiraDaEstradaCriados++;
    }

    private void ExcluirTiles()
    {
        for (int i = 0; i < 2; i++)
        {
            Destroy(tilesAtivos[i]);
            tilesAtivos.RemoveAt(i);
        }
    }

    private void IniciarTiles()
    {
        for(int i = 0; i < numeroDeTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
                SpawnTilesBeiraDaEstrada(1, 1);
            }
            else
            {
                SpawnTile(UnityEngine.Random.Range(0, prefabsTiles.Length));
                SpawnTilesBeiraDaEstrada(UnityEngine.Random.Range(0, prefabsTilesBeiraDaEstrada.Length),
                    UnityEngine.Random.Range(0, prefabsTilesBeiraDaEstrada.Length));
            }
        }
    }

    private void VerificarSpawnTiles()
    {
        if (transformacaoJogador.position.z - 35 > zSpawn - (numeroDeTiles * comprimentoTile))
        {
            SpawnTile(UnityEngine.Random.Range(0, prefabsTiles.Length));
            SpawnTilesBeiraDaEstrada(UnityEngine.Random.Range(0, prefabsTilesBeiraDaEstrada.Length),
                UnityEngine.Random.Range(0, prefabsTilesBeiraDaEstrada.Length));
            ExcluirTiles();
        }
    }
}
