import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {TrucksComponent} from './trucks.component';
import {MatIconModule} from "@angular/material/icon";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatTabsModule} from "@angular/material/tabs";
import {MatTableModule} from "@angular/material/table";
import {MatButtonModule} from "@angular/material/button";
import {RouterModule} from "@angular/router";
import {TrucksListComponent} from './trucks-list/trucks-list.component';
import {TrucksModelsListComponent} from './trucks-models-list/trucks-models-list.component';
import {TrucksService} from "./trucks.service";
import {TrucksEditorModalComponent} from './trucks-list/trucks-editor-modal/trucks-editor-modal.component';
import {
  TrucksModelsEditorModalComponent
} from './trucks-models-list/trucks-models-editor-modal/trucks-models-editor-modal.component';

@NgModule({
  declarations: [
    TrucksComponent,
    TrucksListComponent,
    TrucksModelsListComponent,
    TrucksEditorModalComponent,
    TrucksModelsEditorModalComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatTabsModule,
    MatTableModule,
    MatButtonModule,
    RouterModule.forChild([
      {
        path: '',
        component: TrucksComponent
      },
      {
        path: '**',
        redirectTo: ''
      }
    ])
  ],
  providers: [
    TrucksService
  ]
})
export class TrucksModule { }
