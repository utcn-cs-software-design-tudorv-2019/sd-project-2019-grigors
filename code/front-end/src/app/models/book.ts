import {Category} from './category';
import {Comment} from './comment';
import {User} from './user';
export class Book {
  id?: number;
  userId?: number;
  name: string;
  author: string;
  description: string;
  edition: string;
  image?: string;
  rating: number;
  user?: User;
  categories?: Array<Category>;
  comments?: Array<Comment>;

  constructor(item: any) {
    if (typeof(item) === 'number') {
      this.id = item;
    } else {
      this.id = item.Id;
      this.userId = item.UserId;
      this.name = item.Name;
      this.author = item.Author;
      this.description = item.Description;
      this.edition = item.Edition;
      this.image = item.Image;
      this.rating = item.Rating;
      this.user = item.User;
      this.categories = item.Categories;
      this.comments = item.Comments;
    }
  }
}
