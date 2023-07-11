import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { Departamento } from 'src/app/interfaces/departamento';
import { DepartamentoService } from 'src/app/services/departamento.service';
import { UtilityService } from 'src/app/reusable/shared/utility.service';
import Swal from 'sweetalert2';
import { ModalDepartamentoComponent } from '../../modals/modal-departamento/modal-departamento.component';

@Component({
  selector: 'app-departamento',
  templateUrl: './departamento.component.html',
  styleUrls: ['./departamento.component.css']
})
export class DepartamentoComponent implements OnInit, AfterViewInit{

  columnsTable: string[] = ['nombre', 'ultimoAumento', 'status', 'acciones'];
  dataInicio: Departamento[] = [];
  dataListaDeptos = new MatTableDataSource(this.dataInicio);
  @ViewChild(MatPaginator) pagTable!: MatPaginator;

  constructor(
    private dialog: MatDialog,
    private _departamentoService: DepartamentoService,
    private _utilityService: UtilityService,
    private router: Router
  ) {}

  obtenerDepartamentos() {
    this._departamentoService.lista().subscribe({
      next: (response) => {
        if(response.status) this.dataListaDeptos.data = response.value;
        else this._utilityService.mostrarAlerta('No se encontraron departamentos.', 'Cerrar', 'notif-warning');
      },
      error: (e) => {}
    });
  }

  ngOnInit(): void {
    const usuario = this._utilityService.obtenerSesionUsuario();
    if(usuario != null)
      this.obtenerDepartamentos();
    else {
      this._utilityService.eliminarSesion();
      this.router.navigate(['login']);
    }
  }

  ngAfterViewInit(): void {
    this.dataListaDeptos.paginator = this.pagTable;
  }

  aplicarFiltros(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataListaDeptos.filter = filterValue.trim().toLowerCase();
  }

  nuevoDepartamento() {
    this.dialog.open(ModalDepartamentoComponent, {
      disableClose: true
    }).afterClosed().subscribe((result) => {
      if(result === 'true') this.obtenerDepartamentos();
    });
  }


  editarDepartamento(depto: Departamento) { 
    this.dialog.open(ModalDepartamentoComponent, {
      disableClose: true,
      data: depto
    }).afterClosed().subscribe((result) => {
      if(result === 'true') this.obtenerDepartamentos();
    });
  }
}
