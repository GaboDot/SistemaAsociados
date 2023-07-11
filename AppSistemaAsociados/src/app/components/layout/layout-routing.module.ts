import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';
import { DepartamentoComponent } from './pages/departamento/departamento.component';
import { AsociadoComponent } from './pages/asociado/asociado.component';

const routes: Routes = [{ 
  path: '',
  component: LayoutComponent,
  children: [
    { path: 'departamentos', component: DepartamentoComponent },
    { path: 'asociados', component:  AsociadoComponent }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
