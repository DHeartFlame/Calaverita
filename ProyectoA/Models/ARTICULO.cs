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
        public int ID_ARTICULO { get; set; }

        [StringLength(50)]
        public string TITULO { get; set; }

        [StringLength(50)]
        public string ID_USUARIO { get; set; }

        [Column(TypeName = "image")]
        public byte[] IMAGEN1 { get; set; }

        [Column(TypeName = "image")]
        public byte[] IMAGEN2 { get; set; }

        [Column(TypeName = "image")]
        public byte[] IMAGEN3 { get; set; }

        [StringLength(500)]
        public string DESCRIPCION { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_PUBLICACION { get; set; }

        public int? PRECIO { get; set; }

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
