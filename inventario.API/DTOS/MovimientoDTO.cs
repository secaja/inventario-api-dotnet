namespace inventario.API.DTOS
{
    public class MovimientoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string Tipo { get; set; } = string.Empty; //Entrada / Salida
    }
}
