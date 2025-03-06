using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities.Ejemplo
{
    [Table("Tbl_Ejemplo")]
    public class EjemploTabla : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Numero { get; set; }
    }
}