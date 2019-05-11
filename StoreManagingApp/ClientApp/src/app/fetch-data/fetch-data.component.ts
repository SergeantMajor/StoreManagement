import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs/index';
import { StoreCharacters } from '../store.model';
import { StoreService } from '../store.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent implements OnInit, OnDestroy {
  private clickAddMessage: string;
  private mandatoryValidationMessage: string;
  private numericFields: string[];
  private stockFields: string[];
  private sub: Subscription;

  public storeCharacters: StoreCharacters[];
  public minTotalStock: number;
  public maxTotalStock: number;
  public minStockAccuracy: number;
  public maxStockAccuracy: number;
  public minOnFloorAvailability: number;
  public maxOnFloorAvailability: number;
  public minStockMeanAgeDays: number;
  public maxStockMeanAgeDays: number;

  constructor(private toastr: ToastrService,
              private storeService: StoreService) {
    this.clickAddMessage = 'Please, save store before adding new';
    this.mandatoryValidationMessage = 'Please, fill all mandatory fields before saving store';
    this.numericFields = ['category', 'stockBackStore', 'stockFrontStore', 'stockShoppingWindow', 'stockAccuracy',
      'onFloorAvailability', 'stockMeanAgeDays'];
    this.stockFields = ['stockBackStore', 'stockFrontStore', 'stockShoppingWindow'];
  }

  ngOnInit() {
    this.sub = this.storeService.getStores().subscribe((result) => {
      this.storeCharacters = result;

      this.minTotalStock = Math.min(...this.storeCharacters.map(x => x.totalStock));
      this.maxTotalStock = Math.max(...this.storeCharacters.map(x => x.totalStock));
      this.minStockAccuracy = Math.min(...this.storeCharacters.map(x => x.stockAccuracy));
      this.maxStockAccuracy = Math.max(...this.storeCharacters.map(x => x.stockAccuracy));
      this.minOnFloorAvailability = Math.min(...this.storeCharacters.map(x => x.onFloorAvailability));
      this.maxOnFloorAvailability = Math.max(...this.storeCharacters.map(x => x.onFloorAvailability));
      this.minStockMeanAgeDays = Math.min(...this.storeCharacters.map(x => x.stockMeanAgeDays));
      this.maxStockMeanAgeDays = Math.max(...this.storeCharacters.map(x => x.stockMeanAgeDays));
    }, err => this.toastr.error(err));
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  public add() {
    const notSavedStore = this.storeCharacters.filter(x => x.id === 0);
    if (notSavedStore.length > 0) {
      this.toastr.error(this.clickAddMessage);
    } else {
      const store = new StoreCharacters();
      this.storeCharacters.unshift(store);
    }
  }

  public save(id) {
    const store = this.storeCharacters.filter(x => x.id === id);
    if (store.length > 0) {
      const item = store[0];

      if (!this.clientValidation(item)) {
        return false;
      }

      this.storeService.addStore(item).subscribe(id => {
        store[0].id = Number(id);
      }, err => this.toastr.error(err));
    }
  }

  public updateCell(id: number, property: string, event: any) {
    const store = this.storeCharacters.filter(x => x.id === id);
    if (store.length > 0) {
      const item = store[0];

      item[property] = event.target.textContent;
      event.target.textContent = '';
      event.target.textContent = item[property];

      if (id !== 0) {
        this.storeService.updateStore(item).subscribe(res => {
        }, err => this.toastr.error(err));
      }
    }
  }

  public changeValue(id: number, property: string, event: any) {
    if (this.isPropNumeric(property)) {
      const store = this.storeCharacters.filter(x => x.id === id);
      if (store.length > 0) {
        store[0][property] = event.target.textContent;

        switch (property) {
          case 'stockAccuracy': {
            this.minStockAccuracy = Math.min(...this.storeCharacters.map(x => x.stockAccuracy));
            this.maxStockAccuracy = Math.max(...this.storeCharacters.map(x => x.stockAccuracy));
            break;
          }
          case 'onFloorAvailability': {
            this.minOnFloorAvailability = Math.min(...this.storeCharacters.map(x => x.onFloorAvailability));
            this.maxOnFloorAvailability = Math.max(...this.storeCharacters.map(x => x.onFloorAvailability));
            break;
          }
          case 'stockMeanAgeDays': {
            this.minStockMeanAgeDays = Math.min(...this.storeCharacters.map(x => x.stockMeanAgeDays));
            this.maxStockMeanAgeDays = Math.max(...this.storeCharacters.map(x => x.stockMeanAgeDays));
            break;
          }
          default: {
            break;
          }
        }

        if (this.isStockField(property)) {
          store[0].totalStock = Number(store[0].stockBackStore) + Number(store[0].stockFrontStore) +
            Number(store[0].stockShoppingWindow);

          this.minTotalStock = Math.min(...this.storeCharacters.map(x => x.totalStock));
          this.maxTotalStock = Math.max(...this.storeCharacters.map(x => x.totalStock));
        }
      }
    } else {
      return false;
    }
  }

  public remove(id: number) {
    const store = this.storeCharacters.filter(x => x.id === id);
    if (store.length > 0) {
      this.storeService.removeStore(id).subscribe(res => {
        const index: number = this.storeCharacters.indexOf(store[0]);
        if (index !== -1) {
          this.storeCharacters.splice(index, 1);
        }
      }, err => this.toastr.error(err));
    }
  }

  private isPropNumeric(property): boolean {
    return this.numericFields.filter(x => x === property).length > 0;
  }

  private isStockField(property): boolean {
    return this.stockFields.filter(x => x === property).length > 0;
  }

  private clientValidation(item: StoreCharacters): boolean {
    if (!item.name || !item.countryCode || !item.email || !item.storeManagerName
      || !item.storeManagerLastName || !item.storeManagerEmail || !item.category) {
      this.toastr.error(this.mandatoryValidationMessage);
      return false;
    }
    return true;
  }
}
