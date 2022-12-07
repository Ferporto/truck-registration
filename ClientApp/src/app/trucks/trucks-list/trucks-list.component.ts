import {AfterViewInit, Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {TrucksService} from "../trucks.service";

@Component({
  selector: 'app-trucks-list',
  templateUrl: './trucks-list.component.html',
  styleUrls: ['./trucks-list.component.scss']
})
export class TrucksListComponent implements AfterViewInit {
  @ViewChild('TrucksListHeader') private headerTemplate!: TemplateRef<any>;

  public colunas: string[] = ['produto', 'quantidade', 'valorUnitario', 'valorTotal'];

  constructor(private trucksService: TrucksService) {
  }

  ngAfterViewInit(): void {
    this.emitHeaderTemplate();
  }

  public addTruck(): void {
    console.log('AAAA');
  }

  private emitHeaderTemplate(): void {
    this.trucksService.headerTemplate.next(this.headerTemplate);
  }
}
