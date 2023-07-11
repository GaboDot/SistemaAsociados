import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AsociadoDetalle } from 'src/app/interfaces/asociado-detalle';
import { AsociadoService } from 'src/app/services/asociado.service';
import { DepartamentoService } from 'src/app/services/departamento.service';
import { UtilityService } from 'src/app/reusable/shared/utility.service';
import { Departamento } from 'src/app/interfaces/departamento';

@Component({
  selector: 'app-modal-asociado',
  templateUrl: './modal-asociado.component.html',
  styleUrls: ['./modal-asociado.component.css']
})
export class ModalAsociadoComponent implements OnInit{

  formularioAsociado: FormGroup;
  ocultarPassword: boolean = true;
  tituloModal: string = 'Agregar';
  botonModal: string = 'Guardar';
  listaDepartamentos: Departamento[] = [];

  constructor(
    private modalActual: MatDialogRef<ModalAsociadoComponent>,
    @Inject(MAT_DIALOG_DATA) public detalles: AsociadoDetalle,
    private fb: FormBuilder,
    private _deptoServico: DepartamentoService,
    private _asociadoService: AsociadoService,
    private _utilityService: UtilityService
  ) {
    this.formularioAsociado = this.fb.group({
      nombre: ['', Validators.required],
      apellidoPaterno: ['', Validators.required],
      apellidoMaterno: ['', Validators.required],
      salario: ['', Validators.required],
      email: ['', Validators.required],
      clave: ['', Validators.required],
      idDepartamento: ['', Validators.required],
      status: ['1', Validators.required],
    });

    if(this.detalles!= null) {
      this.tituloModal = 'Editar';
      this.botonModal = 'Actualizar';
    }

    this._deptoServico.lista().subscribe({
      next: (response) => {
        if(response.status) this.listaDepartamentos = response.value;
      },
      error: (e) => {}
    });
  }

  ngOnInit(): void {
    if(this.detalles != null) {
      this.formularioAsociado.patchValue({
        nombre: this.detalles.nombre,
        apellidoPaterno: this.detalles.apellidoPaterno,
        apellidoMaterno: this.detalles.apellidoMaterno,
        salario: this.detalles.salario,
        email: this.detalles.email,
        clave: this.detalles.clave,
        idDepartamento: this.detalles.idDepartamento,
        status: this.detalles.status.toString(),
      });
    }
  }

  guardarEditarAsociado() {
    const _asociado: AsociadoDetalle = {
      idAsociado: this.detalles == null ? 0 : this.detalles.idAsociado,
      idUsuario: this.detalles == null ? 0 : this.detalles.idUsuario,
      fechaIngreso: this.detalles == null ? '' : this.detalles.fechaIngreso,
      nombre: this.formularioAsociado.value.nombre,
      apellidoPaterno: this.formularioAsociado.value.apellidoPaterno,
      apellidoMaterno: this.formularioAsociado.value.apellidoMaterno,
      salario: this.formularioAsociado.value.salario,
      email: this.formularioAsociado.value.email,
      clave: this.formularioAsociado.value.clave == this.detalles.clave ? window.btoa(this.detalles.clave) : window.btoa(this.formularioAsociado.value.clave),
      idDepartamento: this.formularioAsociado.value.idDepartamento,
      status: parseInt(this.formularioAsociado.value.status)
    };

    if(this.detalles == null) {
      this._asociadoService.guardar(_asociado).subscribe({
        next: (response) => {
          if(response.status) {
            this._utilityService.mostrarAlerta('El asociado se registró correctamente', 'Cerrar', 'notif-success');
            this.modalActual.close('true');
          }
          else {
            this._utilityService.mostrarAlerta('No se pudo registrar el asociado', 'Cerrar', 'notif-error');
          }
        },
        error: (e) => {}
      });
    }
    else {
      this._asociadoService.editar(_asociado).subscribe({
        next: (response) => {
          if(response.status) {
            this._utilityService.mostrarAlerta('El asociado se actualizó correctamente', 'Cerrar', 'notif-success');
            this.modalActual.close('true');
          }
          else {
            this._utilityService.mostrarAlerta('No se pudo actualizar el asociado', 'Cerrar', 'notif-error');
          }
        },
        error: (e) => {}
      });
    }
  }
}
