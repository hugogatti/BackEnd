using System.ComponentModel.DataAnnotations;

public class ClienteModel
{
    [Key]
    public int IdCliente { get; set; }

    [Required]
    public string Nome { get; set; }
    
    [Required] //informações obrigatórias
    [StringLength(11)] //garantir que não haja problemas de comprimento.
    public string Cpf { get; set; }
    
    [Required]
    public string Telefone { get; set; }
    
    [Required]
    public string Endereco { get; set; }

    public DateTime Criado_Em { get; set; } = DateTime.Now;

    // Relacionamento com Cobranca
    public ICollection<CobrancaModel> Cobranca { get; set; }
}