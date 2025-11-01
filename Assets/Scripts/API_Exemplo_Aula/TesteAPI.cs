using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TesteAPI : MonoBehaviour
{
    private GameApiService apiService;
    public Player player;
    public Pause pause;
    
    async void Start()
    {
        apiService = new GameApiService();
        
        Debug.Log("=== TESTE DA API ===");

        //Adicionar Jogadores
        Jogador novoJogador1 = new Jogador();
        novoJogador1.Vida = player.vida;
        novoJogador1.QuantidadeItens = player.quantidadeItens;
        novoJogador1.PosicaoX = player.posicoes.x;
        novoJogador1.PosicaoY = player.posicoes.y;
        novoJogador1.PosicaoZ = player.posicoes.z;

        //adicionar jogador na API
        Jogador jogador1Start = await apiService.CriarJogador(novoJogador1);
        Debug.Log($"Jogadores criados: (ID: {jogador1Start.id})");
        //adicionar itens para o jogador 1
        //ItemJogador novoItem1 = new ItemJogador();
        //novoItem1.Nome = "Espada";
        //novoItem1.Descricao = "Espada de Aço";
        //novoItem1.Dano = 10;
        //novoItem1.JogadorId = criadoJogador1.id;
        //ItemJogador criadoItem1 = await apiService.AdicionarItem(criadoJogador1.id, novoItem1);
        //ItemJogador novoItem2 = new ItemJogador();
        //novoItem2.Nome = "Escudo";
        //novoItem2.Descricao = "Escudo de Madeira";
        //novoItem2.Dano = 5;
        //novoItem2.JogadorId = criadoJogador1.id;
        //ItemJogador criadoItem2 = await apiService.AdicionarItem(criadoJogador1.id, novoItem2);
        //alterar vida do jogador 1
        //criadoJogador1.Vida = 80;
        //Jogador atualizadoJogador1 = await apiService.AtualizarJogador(criadoJogador1.id, criadoJogador1);

        //mostrar todos os jogadores
        await MostrarTodosJogadores();


        Debug.Log("=== FIM DOS TESTES ===");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SavePause();
        }
    }

    async void SavePause()
    {
        Jogador novoJogador1 = new Jogador();
        novoJogador1.Vida = player.vida;
        novoJogador1.QuantidadeItens = player.quantidadeItens;
        novoJogador1.PosicaoX = player.posicoes.x;
        novoJogador1.PosicaoY = player.posicoes.y;
        novoJogador1.PosicaoZ = player.posicoes.z;

        //adicionar jogador na API
        Jogador jogador1End = await apiService.CriarJogador(novoJogador1);
        Debug.Log($"Jogadores criados: (ID: {jogador1End.id})");
    }

    async Task MostrarTodosJogadores()
    {
        Jogador[] jogadores = await apiService.GetTodosJogadores();
        Debug.Log($"Total de jogadores: {jogadores.Length}");
        foreach (var jogador in jogadores)
        {
            Debug.Log($"Jogador: (ID: {jogador.id}, Vida: {jogador.Vida})");
            //ItemJogador[] itens = await apiService.GetItensJogador(jogador.id);
            //Debug.Log($"  Itens ({itens.Length}):");
            //foreach (var item in itens)
            //{
            //    Debug.Log($"    - {item.Nome} (Dano: {item.Dano})");
            //}
        }
    }


    void OnDestroy()
    {
        apiService?.Dispose();
    }
}