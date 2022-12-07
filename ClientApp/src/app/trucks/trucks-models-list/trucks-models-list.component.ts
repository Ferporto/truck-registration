import {AfterViewInit, Component, TemplateRef, ViewChild} from '@angular/core';
import {TrucksService} from "../trucks.service";

@Component({
  selector: 'app-trucks-models-list',
  templateUrl: './trucks-models-list.component.html',
  styleUrls: ['./trucks-models-list.component.scss']
})
export class TrucksModelsListComponent implements AfterViewInit {
  @ViewChild('TrucksModelsListHeader') private headerTemplate!: TemplateRef<any>;

  public colunas: string[] = ['produto', 'quantidade', 'valorUnitario'];

  constructor(private trucksService: TrucksService) {
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public addTruckModel(): void {
    console.log('BBBB');
  }

  private emitHeaderTemplate(): void {
    this.trucksService.headerTemplate.next(this.headerTemplate);
  }
}
