export class Store {
  id: number;
  name: string;
  countryCode: string;
  email: string;
  storeManagerName: string;
  storeManagerLastName: string;
  storeManagerEmail: string;
  category: number;

  constructor() {
    this.id = 0;
    this.name = null;
    this.countryCode = null;
    this.email = null;
    this.storeManagerName = null;
    this.storeManagerLastName = null;
    this.storeManagerEmail = null;
    this.category = 0;
  }
}

export class StoreCharacters extends Store {
  stockBackStore: number;
  stockFrontStore: number;
  stockShoppingWindow: number;
  stockAccuracy: number;
  onFloorAvailability: number;
  stockMeanAgeDays: number;
  totalStock: number;

  constructor() {
    super();

    this.stockBackStore = 0;
    this.stockFrontStore = 0;
    this.stockShoppingWindow = 0;
    this.stockAccuracy = 0;
    this.onFloorAvailability = 0;
    this.stockMeanAgeDays = 0;
    this.totalStock = 0;
  }
}
