using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required]
        public EnumStatusTarefa Status { get; set; } = EnumStatusTarefa.Pendente;
    }
}