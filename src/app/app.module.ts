import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { FilesComponent } from './components/files/files.component';
import { FileInfoComponent } from './components/file-info/file-info.component';
import { EditFileComponent } from './components/edit-file/edit-file.component';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { GuestDownloadComponent } from './components/guest-download/guest-download.component';
import { UsersComponent } from './components/admin/users/users.component';
import { AssignUserToRolesComponent } from './components/admin/assign-user-to-roles/assign-user-to-roles.component';
import { UserInfoComponent } from './components/admin/user-info/user-info.component';
import { UserBlockComponent } from './components/admin/user-block/user-block.component';
import { MainPageComponent } from './components/main-page/main-page.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SignInComponent,
    SignUpComponent,
    FilesComponent,
    FileInfoComponent,
    EditFileComponent,
    UploadFileComponent,
    GuestDownloadComponent,
    UsersComponent,
    AssignUserToRolesComponent,
    UserInfoComponent,
    UserBlockComponent,
    MainPageComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'main', pathMatch: 'full'},
      { path:'files', component: FilesComponent},
      { path: 'files/:id', component: FileInfoComponent },
      { path: 'uploadFile', component: UploadFileComponent },
      { path: 'signin', component: SignInComponent },
      { path: 'signup', component: SignUpComponent },
      { path: 'editFile/:id', component: EditFileComponent },
      { path: 'file/:id', component: GuestDownloadComponent },
      { path: 'users', component: UsersComponent },
      { path: 'getRoles', component: AssignUserToRolesComponent },
      { path: 'users/:id', component: UserInfoComponent },
      { path: 'block', component: UserBlockComponent },
      { path: 'main', component: MainPageComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
