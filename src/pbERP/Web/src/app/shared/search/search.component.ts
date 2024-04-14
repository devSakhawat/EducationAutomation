import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { EduABuildingParams } from '../models/eduABuildingParams';
import { AccountService } from 'src/app/accounts/account.service';
import { AccountComponent } from 'src/app/accounts/account.component';
import { EduABuildingInfoToReturn } from '../models/eduABuildingInfoToReturn';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  // @ViewChild('search') searchTerm?: ElementRef;
  //  @Input() eduABuilding?: EduABuildingInfoToReturn;
  //  eduParams = new EduABuildingParams();
  //  injector: any;

  //  constructor(private accountService: AccountService) {}



//    OnSearch(){
//      this.eduParams.search = this.searchTerm?.nativeElement.value;
//      this.eduParams.pageNumber = 1;

//      const accountComponent = this.injector.get(AccountComponent);
//      accountComponent.getEduABuilding(this.eduParams);
//    }

//    onReset()
//    {
//      if(this.searchTerm) this.searchTerm.nativeElement.value = '';
//      this.eduParams = new EduABuildingParams();
//      // this.accountComponent?.getEduABuilding();
//      const accountComponent = this.injector.get(AccountComponent);
//      accountComponent.getEduABuilding(this.eduParams);
//    }

}
