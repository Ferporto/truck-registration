import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {TrucksService} from "../trucks.service";
import {TrucksModelsEditorModalComponent} from "./trucks-models-editor-modal/trucks-models-editor-modal.component";

@Component({
  selector: 'app-trucks-models-list',
  templateUrl: './trucks-models-list.component.html',
  styleUrls: ['./trucks-models-list.component.scss']
})
export class TrucksModelsListComponent implements AfterViewInit {
  @ViewChild('TrucksModelsListHeader') private headerTemplate!: TemplateRef<any>;

  public columns: string[] = ['name', 'type', 'year'];

  constructor(private trucksService: TrucksService,  private matDialog: MatDialog) {
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public addTruckModel(): void {
    this.matDialog.open(TrucksModelsEditorModalComponent, {
      hasBackdrop: true,
      height: 'calc(100% - 64px)',
      width: '40%',
      position: {
        right: '0',
        bottom: '0',
      }
    });
  }

  private emitHeaderTemplate(): void {
    this.trucksService.headerTemplate.next(this.headerTemplate);
  }
}
