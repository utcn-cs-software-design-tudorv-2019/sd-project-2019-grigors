import {Injectable} from '@angular/core';
import {Category} from '../models/category';
import {UtilityService} from './shared/utility.service';
import {TsConstants} from '../constants/tsConstants';
import {EndpointsService} from './shared/endpoints.service';
import {ErrorHandlerService} from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  constructor(private errorHandler: ErrorHandlerService,
              private utilityService: UtilityService,
              private endPoints: EndpointsService) { }
  getAll(): Category[] {
    const categories: Category[] = [];
    this.utilityService.getAll<Category[]>(TsConstants.APP_ENDPOINTS.CATEGORIES)
      .subscribe(items => {
          items.forEach(obj => {
            categories.push(new Category(obj));
          });
        },
        error => {
          this.errorHandler.handleError(error);
        });
    return categories;
  }
  async getAllAsync(): Promise<Category[]> {
    const businessUnits: Category[] = [];
    let businessUnit: Category;
    const businessUnitsList = await this.endPoints.getAll<any[]>(TsConstants.APP_ENDPOINTS.CATEGORIES).toPromise();
    for (const obj of businessUnitsList) {
      businessUnit = new Category(obj);
      businessUnits.push(businessUnit);
    }
    return businessUnits;
  }
  async getBook(id: number): Promise<Category> {
    const item = await this.endPoints.getById<Category>(id, TsConstants.APP_ENDPOINTS.CATEGORIES).toPromise();
    return new Category(item);
  }
}
