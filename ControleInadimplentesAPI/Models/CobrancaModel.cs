using System.ComponentModel.DataAnnotations;

public class CobrancaModel
{
    [Key]
    public int IdCobranca { get; set; } 

    //Chave estrangeira para Cliente
    public int IdCliente { get; set; }  

    [Required] //Informações obrigatórias
    public string Descricao { get; set; }

    [Required] //Informações obrigatórias
    public decimal Valor_Total { get; set; }

    [Required] //Informações obrigatórias
    public string Parcelas_Total { get; set; }

    [Required] //Informações obrigatórias
    public decimal Valor_Parcela { get; set; }

    [Required] //Informações obrigatórias
    public string Parcela_Atual { get; set; }

    [Required] //Informações obrigatórias
    public DateTime Data_Vencimento { get; set; }

    public bool Pago { get; set; }

    public DateTime Criado_Em { get; set; } = DateTime.Now;

    //Relacionamento com Cliente
    public ClienteModel Cliente { get; set; }     
}