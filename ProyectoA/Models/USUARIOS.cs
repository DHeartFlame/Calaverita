namespace ProyectoA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class USUARIOS
    {
        [Key]
        [StringLength(20)]
        public string ID_USUARIO { get; set; }

        [Required]
        [StringLength(20)]
        public string PASS { get; set; }

        [StringLength(50)]
        public string NOMBRE { get; set; }

        [StringLength(50)]
        public string APELLIDO { get; set; }

        [StringLength(150)]
        public string CORREO { get; set; }

        [StringLength(2)]
        public string ADMINISTRADOR { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FECHA_NAC { get; set; }

        [StringLength(50)]
        public string CIUDAD { get; set; }

        [Column(TypeName = "image")]
        public byte[] FOTO { get; set; }

        public List<USUARIOS> Listar()
        {
            List<USUARIOS> lista = new List<USUARIOS>();
            using (var context = new ProyectoaDbContext())
            {
                lista = context.USUARIOS.ToList();
            }
            return lista;
        }
    }
}
