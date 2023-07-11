import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutRoutingModule } from './layout-routing.module';
import { DepartamentoComponent } from './pages/departamento/departamento.component';
import { SharedModule } from 'src/app/reusable/shared/shared.module';
import { AsociadoComponent } from './pages/asociado/asociado.component';
import { ModalDepartamentoComponent } from './modals/modal-departamento/modal-departamento.component';
import { ModalAsociadoComponent } from './modals/modal-asociado/modal-asociado.component';


@NgModule({
  declarations: [
    DepartamentoComponent,
    AsociadoComponent,
    ModalDepartamentoComponent,
    ModalAsociadoComponent,
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    SharedModule
  ]
})
export class LayoutModule { }
