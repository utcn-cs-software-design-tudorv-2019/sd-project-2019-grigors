export class User {
    email: string;
    password: string;
    name: string;
    image: string;
    id?: number;
    constructor(item: any) {
      if (typeof(item) === 'number') {
        this.id = item;
      } else {
        this.id = item.Id;
        this.name = item.Name;
        this.email = item.Email;
        this.password = item.Password;
        this.image = item.Image;
      }
    }
}
