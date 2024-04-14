import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { SearchComponent } from './search/search.component';



@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    SearchComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    PagingHeaderComponent,
    PagerComponent,
    SearchComponent
  ]
})
export class SharedModule { }
