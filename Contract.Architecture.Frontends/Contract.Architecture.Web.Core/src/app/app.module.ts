import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UiComponentsModule } from './components/ui/ui-components.module';
import { SessionServicesModule } from './model/sessions/sessions-services.module';
import { BackendCoreService } from './services/rest/backend-core.service';
import { RestService } from './services/rest/rest.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    UiComponentsModule,
    SessionServicesModule,
    AppRoutingModule,
  ],
  providers: [
    RestService,
    BackendCoreService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
