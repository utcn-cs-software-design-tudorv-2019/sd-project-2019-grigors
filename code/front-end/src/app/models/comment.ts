export class Comment {
  id?: number;
  title: string;
  body: string;
  date: Date;
  constructor(item: any) {
    if (typeof(item) === 'number') {
      this.id = item;
    } else {
      this.id = item.Id;
      this.title = item.Name;
      this.body = item.Description;
      this.date = item.Date;
    }
  }
}
