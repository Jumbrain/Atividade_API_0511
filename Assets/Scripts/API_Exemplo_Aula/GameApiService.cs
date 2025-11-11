using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameApiService
{
    private readonly HttpClient httpClient;
    private const string BASE_URL = "http://localhost:5198/api";
    
    public GameApiService()
    {
        httpClient = new HttpClient();
    }
    
    #region Jogador Operations
    
    /// <summary>
    /// Busca todos os jogadores
    /// </summary>
    public async Task<Jogador[]> GetTodosJogadores()
    {
        try
        {
            string url = $"{BASE_URL}/player";
            Debug.Log($"GET: {url}");
            
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Resposta recebida: {json.Substring(0, Math.Min(200, json.Length))}...");
            
            // Como JsonUtility não suporta arrays diretamente, vamos usar um wrapper
            string wrappedJson = $"{{\"jogadores\":{json}}}";
            JogadorArray jogadorArray = JsonUtility.FromJson<JogadorArray>(wrappedJson);
            
            return jogadorArray.jogadores;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar jogadores: {ex.Message}");
            return new Jogador[0];
        }
    }
    
    /// <summary>
    /// Busca um jogador específico
    /// </summary>
    public async Task<Jogador> GetJogador(int id)
    {
        try
        {
            string url = $"{BASE_URL}/player/{id}";
            Debug.Log($"GET: {url}");
            
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador recebido: {json}");
            
            Jogador jogador = JsonUtility.FromJson<Jogador>(json);
            return jogador;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar jogador {id}: {ex.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Atualiza dados do jogador
    /// </summary>
    public async Task<Jogador> AtualizarJogador(int id, Jogador jogador)
    {
        try
        {
            string url = $"{BASE_URL}/player/{id}";
            Debug.Log($"PUT: {url}");
            
            string json = JsonUtility.ToJson(jogador);
            Debug.Log($"JSON sendo enviado: {json}");
            
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            
            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador atualizado: {responseJson}");
            
            return JsonUtility.FromJson<Jogador>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar jogador {id}: {ex.Message}");
            return null;
        }
    }
    
    /// <summary>
    /// Cria novo jogador
    /// </summary>
    public async Task<Jogador> CriarJogador(Jogador jogador)
    {
        try
        {
            string url = $"{BASE_URL}/player";
            Debug.Log($"POST: {url}");
            
            string json = JsonUtility.ToJson(jogador);
            Debug.Log($"JSON sendo enviado: {json}");
            
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            
            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador criado: {responseJson}");
            
            return JsonUtility.FromJson<Jogador>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao criar jogador: {ex.Message}");
            return null;
        }
    }
    
    #endregion
    
    public void Dispose()
    {
        httpClient?.Dispose();
    }
}

// Classes auxiliares para deserialização de arrays
[System.Serializable]
public class JogadorArray
{
    public Jogador[] jogadores;
}

[System.Serializable]
public class ItemArray
{
    public ItemJogador[] itens;
}