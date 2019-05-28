import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Book} from '../models/book';
import {Category} from '../models/category';
import {EndpointsService} from '../services/shared/endpoints.service';
import {OurCookieService} from '../services/shared/our-cookie.service';
import {ErrorHandlerService} from '../services/error-handler.service';
import {CustomSnackbarService} from '../services/custom-snackbar.service';
import {Router} from '@angular/router';
import {BooksService} from '../services/books.service';
import {ViewEncapsulation} from '@angular/cli/lib/config/schema';
import {TsConstants} from '../constants/tsConstants';
import {CategoriesService} from '../services/categories.service';

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrls: ['./create-book.component.css'],
  encapsulation: ViewEncapsulation.None,
})

export class CreateBookComponent implements OnInit {
  bookId: number;
  createBookForm: FormGroup;
  validity = false;
  book: Book;
  imgURL: string;
  categories: Category[];
  update = false;
  title = 'Add book';
  buttonLabel = 'Save';
  fileToUpload: File = null;
  checkedCategories: number[] = [];
  private message: string;

  constructor(private endPoints: EndpointsService,
              private ourCookieService: OurCookieService,
              private formBuilder: FormBuilder,
              private errorHandler: ErrorHandlerService,
              private snackBar: CustomSnackbarService,
              private router: Router,
              private categoryService: CategoriesService,
              private bookService: BooksService) {
    if (this.router.url === TsConstants.ROUTES.BOOKS_UPDATE) {
      this.update = true;
      this.title = 'Update book';
    }
    this.createBookForm = this.formBuilder.group({
      name: [''],
      author: [''],
      description: [''],
      edition: [''],
      rating: [''],
      category: [''],
    });
  }

  async ngOnInit() {

    this.bookService.currentBookId.subscribe(bookId => this.bookId = bookId);
    if (this.update) {
      this.buttonLabel = 'Update';
      this.book = await this.bookService.getBookById(this.bookId, TsConstants.APP_ENDPOINTS.BOOKS);
      this.updateFormValues();
      this.imgURL = this.book.image;
    }
    this.validity = !this.createBookForm.valid;
    this.categories = this.categoryService.getAll();
  }


  compareID(nr1: number, nr2: number) {
    return nr1 === nr2;
  }

  categoriesToSave(categoriesChecked: Category[]) {
    const categoriesToSaveList: Category[] = [];
    let categoryObj: Category;
    for (const obj of categoriesChecked) {
      categoryObj = new Category(+obj);
      categoriesToSaveList.push(categoryObj);
    }
    return categoriesToSaveList;
  }

  onSave() {
    this.book = this.getUserInput();
    if (this.update) {
      this.updateBook();
    } else {
      this.insertBook();
    }
    this.validity = true;
  }

  preview(file: FileList) {
    if (file.length === 0) {
      return;
    }
    const mimeType = file[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = 'Only images are supported.';
      return;
    }
    this.fileToUpload = file.item(0);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imgURL = event.target.result;
    };
    reader.readAsDataURL(this.fileToUpload);
  }

  private populateForm() {
    this.createBookForm.get('name').setValue(this.book.name);
    this.createBookForm.get('author').setValue(this.book.author);
    this.createBookForm.get('description').setValue(this.book.description);
    this.createBookForm.get('edition').setValue(this.book.edition);
    this.createBookForm.get('rating').setValue(this.book.rating);
    this.createBookForm.get('category').setValue(this.book.categories);
  }

  private updateFormValues() {
    this.populateForm();
  }

  private getUserInput() {
    const newBook = this.createBook();
    if (this.update) {
      newBook.id = this.book.id;
    }
    return newBook;
  }

  private createBook(): Book {
    const createBookValue = this.createBookForm.value;
    const newBook: Book = {
      name: createBookValue.name + TsConstants.BOOK_SUFFIX,
      author: createBookValue.author,
      description: createBookValue.description,
      edition: createBookValue.edition,
      rating: createBookValue.rating,
      image: this.imgURL,
      categories: this.categoriesToSave(createBookValue.category)
    };
    return newBook;
  }

  private updateBook() {
    this.endPoints.update<Book>(this.book, TsConstants.APP_ENDPOINTS.BOOKS).subscribe(
      success => {
        this.snackBar.successSnackBar('Your book has been updated!');
        setTimeout(() => {
          this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
        }, 2000);
      },
      error => {
        this.errorHandler.handleError(error);
      }
    );
  }

  private insertBook() {
    this.endPoints.post<Book>(this.book, TsConstants.APP_ENDPOINTS.BOOKS).subscribe(
     succes => {
        this.snackBar.successSnackBar('Your book has been created!');
        setTimeout(() => {
          this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
        }, 2000);
      },
      error => {
        this.errorHandler.handleError(error);
      }
    );
  }
}

