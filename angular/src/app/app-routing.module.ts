import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { CategoryComponent } from './category/category.component';
import { AuthorComponent } from './author/author.component';
import { PublisherComponent } from './publisher/publisher.component';
import { BookComponent } from './book/book.component';
import { LibraryComponent } from './library/library.component';
import { BookDetailComponent } from './library/book-detail/book-detail.component';
import { ListCartComponent } from './library/list-cart/list-cart.component';
import { ReaderBorrowComponent } from './reader-borrow/reader-borrow.component';
import { ReaderBorrowDetailComponent } from './reader-borrow/reader-borrow-detail/reader-borrow-detail.component';
import { BorrowMComponent } from './borrow-m/borrow-m.component';
import { BorrowDetailComponent } from './borrow-m/borrow-detail/borrow-detail.component';
import { StatisticReportComponent } from './statistic-report/statistic-report.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'categories', component: CategoryComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard]},
                    { path: 'authors', component: AuthorComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'publishers', component: PublisherComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'books', component: BookComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'statistic-report', component: StatisticReportComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'borrows', component: BorrowMComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'borrow-book-detail/:id', component: BorrowDetailComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard]},
                    { path: 'library', component: LibraryComponent },
                    { path: 'book-detail/:bookId', component: BookDetailComponent },
                    { path: 'cart', component: ListCartComponent },
                    { path: 'reader-borrow', component: ReaderBorrowComponent },
                    { path: 'reader-borrow-detail/:id', component: ReaderBorrowDetailComponent },
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
