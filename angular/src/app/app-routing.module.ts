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
import { BorrowbookComponent } from './borrowbook/borrowbook.component';
import { BorrowbookReaderComponent } from './borrowbook-reader/borrowbook-reader.component';
import { StatisticReportComponent } from './statistic-report/statistic-report.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: LibraryComponent,  canActivate: [AppRouteGuard] },
                    { path: 'dashboard', component: HomeComponent, data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'categories', component: CategoryComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard]},
                    { path: 'authors', component: AuthorComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'publishers', component: PublisherComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'borrows', component: BorrowbookComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard]},
                    { path: 'books', component: BookComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'book-detail/:bookId', component: BookDetailComponent},
                    { path: 'statistic-report-category', component: StatisticReportComponent,data: { permission: 'Pages.Librarians' }, canActivate: [AppRouteGuard] },
                    { path: 'borrows-reader', component: BorrowbookReaderComponent,data: { permission: 'Pages.Readers' }, canActivate: [AppRouteGuard] },
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
