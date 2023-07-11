export interface Usuario {
    idUsuario: number,
    email: string,
    clave: string,
    status: number,
    fkIdAsociado: number
}
