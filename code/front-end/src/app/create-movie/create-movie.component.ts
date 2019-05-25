import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Movie} from '../models/movie';
import {Comment} from '../models/comment';
import {Category} from '../models/category';
import {EndpointsService} from '../services/shared/endpoints.service';
import {OurCookieService} from '../services/shared/our-cookie.service';
import {ErrorHandlerService} from '../services/error-handler.service';
import {CustomSnackbarService} from '../services/custom-snackbar.service';
import {Router} from '@angular/router';
import {MoviesService} from '../services/movies.service';
import {ViewEncapsulation} from '@angular/cli/lib/config/schema';
import {TsConstants} from '../constants/tsConstants';
import {CategoriesService} from '../services/categories.service';

@Component({
  selector: 'app-create-movie',
  templateUrl: './create-movie.component.html',
  styleUrls: ['./create-movie.component.css'],
  encapsulation: ViewEncapsulation.None,
})

export class CreateMovieComponent implements OnInit {
  movieId: number;
  createMovieForm: FormGroup;
  validity = false;
  movie: Movie;
  imgURL: string;
  categories: Category[];
  update = false;
  title = 'Add movie';
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
              private movieService: MoviesService) {
    if (this.router.url === TsConstants.ROUTES.MOVIES_UPDATE) {
      this.update = true;
      this.title = 'Update movie';
    }
    this.createMovieForm = this.formBuilder.group({
      name: [''],
      author: [''],
      description: [''],
      edition: [''],
      rating: [''],
      category: [''],
    });
  }

  async ngOnInit() {

    this.movieService.currentMovieId.subscribe(movieId => this.movieId = movieId);
    if (this.update) {
      this.buttonLabel = 'Update';
      this.movie = await this.movieService.getMovieById(this.movieId, TsConstants.APP_ENDPOINTS.MOVIES);
      this.updateFormValues();
      this.imgURL = this.movie.image;
    }
    this.validity = !this.createMovieForm.valid;
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
    this.movie = this.getUserInput();
    if (this.update) {
      this.updateMovie();
    } else {
      this.insertMovie();
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
    this.createMovieForm.get('name').setValue(this.movie.name);
    this.createMovieForm.get('author').setValue(this.movie.author);
    this.createMovieForm.get('description').setValue(this.movie.description);
    this.createMovieForm.get('edition').setValue(this.movie.edition);
    this.createMovieForm.get('rating').setValue(this.movie.rating);
    this.createMovieForm.get('category').setValue(this.movie.categories);
  }

  private updateFormValues() {
    this.populateForm();
  }

  private getUserInput() {
    const newMovie = this.createMovie();
    if (this.update) {
      newMovie.id = this.movie.id;
    }
    return newMovie;
  }

  private createMovie(): Movie {
    const createMovieValue = this.createMovieForm.value;
    const newMovie: Movie = {
      name: createMovieValue.name + TsConstants.MOVIE_SUFFIX,
      author: createMovieValue.author,
      description: createMovieValue.description,
      edition: createMovieValue.edition,
      rating: createMovieValue.rating,
      image: this.imgURL,
      categories: this.categoriesToSave(createMovieValue.category)
    };
    return newMovie;
  }

  private updateMovie() {
    this.endPoints.update<Movie>(this.movie, TsConstants.APP_ENDPOINTS.MOVIES).subscribe(
      success => {
        this.snackBar.successSnackBar('Your movie has been updated!');
        setTimeout(() => {
          this.router.navigate(['dashboard']);
        }, 2000);
      },
      error => {
        this.errorHandler.handleError(error);
      }
    );
  }

  private insertMovie() {
    this.endPoints.post<Movie>(this.movie, TsConstants.APP_ENDPOINTS.MOVIES).subscribe(
      succes => {
        this.snackBar.successSnackBar('Your movie has been created!');
        setTimeout(() => {
          this.router.navigate(['dashboard']);
        }, 2000);
      },
      error => {
        this.errorHandler.handleError(error);
      }
    );
  }
}

