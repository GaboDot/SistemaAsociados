import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponseApi } from '../interfaces/response-api';import { Asociado } from '../interfaces/asociado';
import { AsociadoDetalle } from '../interfaces/asociado-detalle';

@Injectable({
  providedIn: 'root'
})
export class AsociadoService {

  private urlApi: string = environment.endpoint + 'Asociado/';

  constructor(private http: HttpClient) { }

  lista():Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}Lista`);
  }

  detalles():Observable<ResponseApi> {
    return this.http.get<ResponseApi>(`${this.urlApi}Detalles`);
  }

  buscar(id: number):Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.urlApi}Buscar/${id}`);
  }

  guardar(request: AsociadoDetalle):Observable<ResponseApi> {
    return this.http.post<ResponseApi>(`${this.urlApi}Guardar`, request);
  }

  editar(request: AsociadoDetalle):Observable<ResponseApi> {
    return this.http.put<ResponseApi>(`${this.urlApi}Editar`, request);
  }

  eliminar(id: number):Observable<ResponseApi> {
    return this.http.delete<ResponseApi>(`${this.urlApi}Eliminar/${id}`);
  }
}
