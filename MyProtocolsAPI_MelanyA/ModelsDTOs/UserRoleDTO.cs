namespace MyProtocolsAPI_MelanyA.ModelsDTOs
{
    public class UserRoleDTO
    {
        // Un DTO (data transfer object) sirve basicamente para dos funciones:

        // 1. Simplificar la estructura de los JSON que se envian y llegan a los end points de 
        // los controllers, quitando composicion innecesarias que solo los JSON sean muy pesados
        // o que muestren informacion que no se desea ver (puede se por seguridad)

        // 2. Ocultar la estructura real de los modelos y por tanto de las tablas de bases de datos
        // a los programadores de las apps, paginas web o aplicaciones de escritorio.
        // --------------------- //

        // Tomando encuenta el segundo criterio y sobre manera de ejemplo, este DTO tendra los nombres
        // de propiedades en espaniol 
        public int IDRolUsuario { get; set; }
        public string DescripcionRol { get; set; } = null!;
    }
}
