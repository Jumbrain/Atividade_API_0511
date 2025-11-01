using TMPro;
using UnityEngine;

public class ShowStats : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI posText;
    public TextMeshProUGUI itemText;
    void Update()
    {
        hpText.text = "vida: " + player.vida;
        posText.text = "localização: " + player.posicoes;
        itemText.text = "quantidade de itens: " + player.quantidadeItens;
    }
}
