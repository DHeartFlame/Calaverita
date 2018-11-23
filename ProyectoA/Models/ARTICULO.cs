namespace ProyectoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("ARTICULO")]
    public partial class ARTICULO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_ARTICULO { get; set; }

        [StringLength(50)]
        public string TITULO { get; set; }

        [StringLength(50)]
        public string ID_USUARIO { get; set; }

        [StringLength(200)]
        public string IMAGEN1 { get; set; }

        [StringLength(200)]
        public string IMAGEN2 { get; set; }

        [StringLength(200)]
        public string IMAGEN3 { get; set; }

        [StringLength(500)]
        public string DESCRIPCION { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_PUBLICACION { get; set; }

        public List<ARTICULO> Listar()
        {
            List<ARTICULO> lista = new List<ARTICULO>();
            using (var context = new ProyectoaDbContext())
            {
                lista = context.ARTICULO.ToList();
            }
            return lista;
        }
    }
}
