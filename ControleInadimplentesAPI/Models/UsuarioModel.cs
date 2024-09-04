using System.ComponentModel.DataAnnotations;

public class UsuarioModel
{
    [Key]
    public int IdUsuario { get; set; }

    [Required] //informações obrigatórias
    public string Nome { get; set; }

    [Required] //informações obrigatórias
    public string Email { get; set; }

    [Required] //informações obrigatórias
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    public DateTime Criado_Em { get; set; } = DateTime.Now;
}