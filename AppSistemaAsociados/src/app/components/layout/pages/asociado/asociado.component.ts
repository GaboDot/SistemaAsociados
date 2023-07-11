import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { AsociadoDetalle } from 'src/app/interfaces/asociado-detalle';
import { AsociadoService } from 'src/app/services/asociado.service';
import { UtilityService } from 'src/app/reusable/shared/utility.service';
import Swal from 'sweetalert2';
import { ModalAsociadoComponent } from '../../modals/modal-asociado/modal-asociado.component';

@Component({
  selector: 'app-asociado',
  templateUrl: './asociado.component.html',
  styleUrls: ['./asociado.component.css']
})
export class AsociadoComponent implements OnInit, AfterViewInit{

  columnsTable: string[] = ['nombre', 'apellidoPaterno', 'apellidoMaterno', 'salario', 'status', 'acciones'];
  dataInicio: AsociadoDetalle[] = [];
  dataListaAsociados = new MatTableDataSource(this.dataInicio);
  @ViewChild(MatPaginator) pagTable!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _asociadoService: AsociadoService,
    private _utilityService: UtilityService,
    private router: Router
  ) {}

  obtenerAsociados() {
    this._asociadoService.detalles().subscribe({
      next: (response) => {
        if(response.status) this.dataListaAsociados.data = response.value;
        else this._utilityService.mostrarAlerta('No se encontraron usuarios.', 'Cerrar', 'notif-warning');
      },
      error: (e) => {}
    });
  }

  ngOnInit(): void {
    const usuario = this._utilityService.obtenerSesionUsuario();
    if(usuario != null)
      this.obtenerAsociados();
    else {
      this._utilityService.eliminarSesion();
      this.router.navigate(['login']);
    }
  }

  ngAfterViewInit(): void {
    this.dataListaAsociados.paginator = this.pagTable;
  }

  aplicarFiltros(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListaAsociados.filter = filterValue.trim().toLowerCase();
  }

  nuevoAsociado() { 
    this.dialog.open(ModalAsociadoComponent, {
      disableClose: true
    }).afterClosed().subscribe((result) => {
      if(result === 'true') this.obtenerAsociados();
    });
  }

  editarAsociado(usuario: AsociadoDetalle) { 
    this.dialog.open(ModalAsociadoComponent, {
      disableClose: true,
      data: usuario
    }).afterClosed().subscribe((result) => {
      if(result === 'true') this.obtenerAsociados();
    });
  }

  eliminarUsuario(usuario: AsociadoDetalle) {
    // Swal.fire({
    //   title: '¿Deseas eliminar el usuario?',
    //   text: `${usuario.nombre} ${usuario.apellidoPaterno}`,
    //   icon: 'warning',
    //   confirmButtonColor: '#3085d6',
    //   confirmButtonText: 'Eliminar',
    //   showCancelButton: true,
    //   cancelButtonColor: '#d33',
    //   cancelButtonText: 'Cancelar'
    // }).then((result) => {
    //   if(result.isConfirmed) {
    //     this._usuarioService.eliminar(usuario.idUsuario).subscribe({
    //       next: (response) => {
    //         if(response.status) {
    //           this._utilityService.mostrarAlerta('Se eliminó el usuario', 'Cerrar', 'notif-success');
    //           this.obtenerUsuarios();
    //         }
    //         else {
    //           this._utilityService.mostrarAlerta('No se pudo eliminar el usuario', 'Cerrar', 'notif-error');
    //         }
    //       },
    //       error: (e) => {}
    //     });
    //   }
    // });
  }

}
