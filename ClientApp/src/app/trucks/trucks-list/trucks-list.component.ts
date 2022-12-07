import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";

import {TrucksService} from "../trucks.service";
import {TrucksEditorModalComponent} from "./trucks-editor-modal/trucks-editor-modal.component";

@Component({
  selector: 'app-trucks-list',
  templateUrl: './trucks-list.component.html',
  styleUrls: ['./trucks-list.component.scss']
})
export class TrucksListComponent implements AfterViewInit {
  @ViewChild('TrucksListHeader') private headerTemplate!: TemplateRef<any>;

  public colunas: string[] = ['produto', 'quantidade', 'valorUnitario', 'valorTotal'];

  constructor(private trucksService: TrucksService, private matDialog: MatDialog) {
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public addTruck(): void {
    this.matDialog.open(TrucksEditorModalComponent, {
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
