import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Departamento } from 'src/app/interfaces/departamento';
import { DepartamentoService } from 'src/app/services/departamento.service';
import { UtilityService } from 'src/app/reusable/shared/utility.service';

@Component({
  selector: 'app-modal-departamento',
  templateUrl: './modal-departamento.component.html',
  styleUrls: ['./modal-departamento.component.css']
})
export class ModalDepartamentoComponent implements OnInit{

  formularioDepto: FormGroup;
  tituloModal: string = 'Agregar';
  botonModal: string = 'Guardar';

  constructor(
    private modalActual: MatDialogRef<ModalDepartamentoComponent>,
    @Inject(MAT_DIALOG_DATA) public datosDepto: Departamento,
    private fb: FormBuilder,
    private _deptoService: DepartamentoService,
    private _utilityService: UtilityService
  ) {
    this.formularioDepto = this.fb.group({
      nombre: ['', Validators.required],
      aumento: [''],
      status: ['1', Validators.required]
    });

    if(this.datosDepto != null) {
      this.tituloModal = 'Editar';
      this.botonModal = 'Actualizar';
    }
  }

  ngOnInit(): void {
    if(this.datosDepto != null) {
      this.formularioDepto.patchValue({
        nombre: this.datosDepto.nombre,
        aumento: (parseFloat(this.datosDepto.ultimoAumento) * 100),
        status: this.datosDepto.status.toString()
      });
    }
  }

  guardarEditarDepto() {
    const _depto: Departamento = {
      idDepartamento: this.datosDepto == null ? 0 : this.datosDepto.idDepartamento,
      nombre: this.formularioDepto.value.nombre,
      ultimoAumento: this.datosDepto == null ? '' : (this.formularioDepto.value.aumento.toFixed(2)/100).toString(),
      status: parseInt(this.formularioDepto.value.status)
    };

    if(this.datosDepto == null) {
      this._deptoService.guardar(_depto).subscribe({
        next: (response) => {
          if(response.status) {
            this._utilityService.mostrarAlerta('El producto se registró correctamente', 'Cerrar', 'notif-success');
            this.modalActual.close('true');
          }
          else {
            this._utilityService.mostrarAlerta('No se pudo registrar el producto', 'Cerrar', 'notif-error');
          }
        },
        error: (e) => {}
      });
    }
    else {
      this._deptoService.editar(_depto).subscribe({
        next: (response) => {
          if(response.status) {
            this._utilityService.mostrarAlerta('El departamento se actualizó correctamente', 'Cerrar', 'notif-success');
            this.modalActual.close('true');
          }
          else {
            this._utilityService.mostrarAlerta('No se pudo actualizar el departamento', 'Cerrar', 'notif-warning');
          }
        },
        error: (e) => {
          console.log(e);
          this._utilityService.mostrarAlerta(e.error.title, 'Cerrar', 'notif-error');
        }
      });
    }
  }
}
