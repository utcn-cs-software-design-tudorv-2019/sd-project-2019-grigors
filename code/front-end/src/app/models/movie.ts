import {Category} from './category';
import {Comment} from './comment';
import {User} from './user';

export class Movie {
  id?: number;
  userId?: number;
  name: string;
  description: string;
  edition: string;
  author: string;
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
      this.description = item.Description;
      this.edition = item.Edition;
      this.author = item.Author;
      this.image = item.Image;
      this.rating = item.Rating;
      this.categories = item.Categories;
      this.user = item.User;
      this.comments = item.Comments;
    }
  }
}
