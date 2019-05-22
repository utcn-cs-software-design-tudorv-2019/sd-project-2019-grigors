export class Category {
  id: number;
  name: string;
  description: string;
  constructor(item: any) {
    if (typeof(item) === 'number') {
      this.id = item;
    } else {
      this.id = item.Id;
      this.name = item.Name;
      this.description = item.Description;
    }
  }

}
