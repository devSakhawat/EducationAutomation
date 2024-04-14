import { AfterViewInit, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EduAStudent } from 'src/app/shared/models/Education/eduAStudent';
import { GenConfigDPoliceStation } from 'src/app/shared/models/GeneralConfiguration/GenConfigDPoliceStation';
import { GenConfigGReligion } from 'src/app/shared/models/GeneralConfiguration/GenConfigGReligion';
import { GenConfigACountry } from 'src/app/shared/models/GeneralConfiguration/genConfigACountry';
import { genConfigBDivisionOrState } from 'src/app/shared/models/GeneralConfiguration/genConfigBDivisionOrState';
import { GenConfigCDistrictOrCity } from 'src/app/shared/models/GeneralConfiguration/genConfigCDistrictOrCity';
import { GenConfigEGender } from 'src/app/shared/models/GeneralConfiguration/genConfigEGender';
import { GenConfigFBloodGroup } from 'src/app/shared/models/GeneralConfiguration/genConfigFBloodGroup';
import { EducationService } from '../education.service';
import { GeneralConfigurationService } from 'src/app/general-configuration/general-configuration.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-a-student',
  templateUrl: './a-student.component.html',
  styleUrls: ['./a-student.component.css']
})

export class AStudentComponent implements AfterViewInit, OnInit {
  students: EduAStudent[] = [];
  student: EduAStudent = {} as EduAStudent;
  studentForm!: FormGroup;
  selectedFile: File | null = null;
  imagePreviewUrl: string | ArrayBuffer | null = null;

  isLoadingResults = true;
  isRateLimitReached = false;
  dataSource!: MatTableDataSource<EduAStudent>;

  //#region Dropdown
  // genders!: GenConfigEGender[];
  genders!: GenConfigEGender[];
  // filterGenders!: GenConfigEGender[];

  bloodGroups!: GenConfigFBloodGroup[];
  // filterBloodGroups!: GenConfigFBloodGroup[];

  religions!: GenConfigGReligion[];
  // filterReligions!: GenConfigGReligion[];
//#endregion Dropdown

//#region AutoComplete
  presentPSs!: GenConfigDPoliceStation[];
  filterPresentPSs!: GenConfigDPoliceStation[];

  presentDistricts!: GenConfigCDistrictOrCity[];
  filterPresentDistricts!: GenConfigCDistrictOrCity[];

  presentDivisions!: genConfigBDivisionOrState[];
  filterPresentDivisions!: genConfigBDivisionOrState[];

  presentCountries!: GenConfigACountry[];
  filterPresentCountries!: GenConfigACountry[];

  permanentPSs!: GenConfigDPoliceStation[];
  filterPermanentPSs!: GenConfigDPoliceStation[];

  permanentDistricts!: GenConfigCDistrictOrCity[];
  filterPermanentDistricts!: GenConfigCDistrictOrCity[];

  permanentDivisions!: genConfigBDivisionOrState[];
  filterPermanentDivisions!: genConfigBDivisionOrState[];

  permanentCountries!: GenConfigACountry[];
  filterPermanentCountries!: GenConfigACountry[];
//#endregion AutoComplete

  displayedColumns: string[] = [
    'studentCode',
    'studentName',
    'studentFathersName',
    'studentMothersName',
    'actions'
  ];

  private studentSubscription: Subscription | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('DetailDialog', { static: true }) detailDialog!: TemplateRef<any>;
  @ViewChild('DeleteDialog', { static: true }) deleteDialog!: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private services: EducationService,
    private generalServices: GeneralConfigurationService,
    private snackbar: MatSnackBar,
    private dialog: MatDialog
  ) {
    this.initForm();
  }

  initForm(): void {
    this.studentForm = this.fb.group({
      studentId: [null],
      studentCode: ['', Validators.required],
      studentName: ['', Validators.required],
      studentFathersName: [''],
      studentMothersName: [''],
      genderId: [0],
      bloodGroupId: [0],
      religionId: [0],
      bloodGroupName: [''],
      genderName: [''],
      religionName: [''],
      studentPhoto: [''],
      studentImage: null,

      // Present Address
      presentAddressId: [0],
      presentAddress: [''],
      presentPostOffice: [''],
      presentPoliceStationId: [0],
      presentPoliceStationName: [''],
      presentDistrictId: [0],
      presentDistrictName: [''],
      presentDivisionId: [0],
      presentDivisionName: [''],
      presentCountryId: [0],
      presentCountryName: [''],

      // Permanent Address
      permanentAddressId: [0],
      permanentAddress: [''],
      permanentPostOffice: [''],
      permanentPoliceStationId: [0],
      permanentPoliceStationName: [''],
      permanentDistrictId: [0],
      permanentDistrictName: [''],
      permanentDivisionId: [0],
      permanentDivisionName: [''],
      permanentCountryId: [0],
      permanentCountryName: ['']
    });
    this.imagePreviewUrl = null;
  }

  resetForm() {
    this.selectedFile = null;
    this.imagePreviewUrl = null;
    this.studentForm.reset();
    this.studentForm.patchValue({
      studentId: [null],
      studentCode: [''],
      studentName: [''],
      studentFathersName: [''],
      studentMothersName: [''],
      genderId: [0],
      bloodGroupId: [0],
      religionId: [0],
      bloodGroupName: [''],
      genderName: [''],
      religionName: [''],
      studentPhoto: [''],
      studentImage: null,

      // Present Address
      presentAddressId: [0],
      presentAddress: [''],
      presentPostOffice: [''],
      presentPoliceStationId: [0],
      presentPoliceStationName: [''],
      presentDistrictId: [0],
      presentDistrictName: [''],
      presentDivisionId: [0],
      presentDivisionName: [''],
      presentCountryId: [0],
      presentCountryName: [''],

      // Permanent Address
      permanentAddressId: [0],
      permanentAddress: [''],
      permanentPostOffice: [''],
      permanentPoliceStationId: [0],
      permanentPoliceStationName: [''],
      permanentDistrictId: [0],
      permanentDistrictName: [''],
      permanentDivisionId: [0],
      permanentDivisionName: [''],
      permanentCountryId: [0],
      permanentCountryName: ['']
    });

  }

  ngOnInit(): void {
    this.onLoad();
    this.dataSource = new MatTableDataSource<EduAStudent>();

    // Get Genders from Database
    this.generalServices.getGenders().subscribe((response) => {
      this.genders = [];
      for (const data of response) {
        if (data.genderName !== null && !this.genders.some((x) => x.genderId === data.genderId)) {
          this.genders.push(data);
        }
      }
    });

    // Get BloodGroups from Database
    this.generalServices.getBloodGroups().subscribe((response) => {
      this.bloodGroups = [];
      for (const data of response) {
        if (data.bloodGroupName !== null && !this.bloodGroups.some((x) => x.bloodGroupId === data.bloodGroupId)) {
          this.bloodGroups.push(data);
        }
      }
    });

    // Get Religons from Database
    this.generalServices.getReligions().subscribe((response) => {
      this.religions = [];
      for (const data of response) {
        if (data.religionName !== null && !this.religions.some((x) => x.religionId === data.religionId)) {
          this.religions.push(data);
        }
      }
    });

    // Get PoliceStations from Database
    this.generalServices.getPoliceStations().subscribe((response) => {
      this.presentPSs = [];
      this.permanentPSs = [];
      for (const data of response) {
        if (data.policeStationName !== null || data.policeStationName != '' && !this.presentPSs.some((x) => x.policeStationId === data.policeStationId) && !this.permanentPSs.some((x) => x.policeStationId === data.policeStationId)) {
          this.presentPSs.push(data);
          this.permanentPSs.push(data);
        }
      }
    });

    // Get Districts from Database
    this.generalServices.getDistrictsOrCities().subscribe((response) => {
      this.presentDistricts = [];
      this.permanentDistricts = [];
      for (const data of response) {
        if (data.districtName !== null && !this.presentDistricts.some((x) => x.districtId === data.districtId) && !this.permanentDistricts.some((x) => x.districtId === data.districtId)) {
          this.presentDistricts.push(data);
          this.permanentDistricts.push(data);
        }
      }
      console.log(this.presentDistricts);
      console.log(this.permanentDistricts);
    });

    // Get Divisions from Database
    this.generalServices.getDivisionsOrStates().subscribe((response) => {
      this.presentDivisions = [];
      this.permanentDivisions = [];
      for (const data of response) {
        if (data.divisionName !== null && !this.presentDivisions.some((x) => x.divisionId === data.divisionId) && !this.permanentDivisions.some((x) => x.divisionId === data.divisionId)) {
          this.presentDivisions.push(data);
          this.permanentDivisions.push(data);
        }
      }
    });

    // Get Countrys from Database
    this.generalServices.getCountries().subscribe((response) => {
      this.presentCountries = [];
      this.permanentCountries = [];
      for (const data of response) {
        if (data.countryName !== null && !this.presentCountries.some((x) => x.countryId === data.countryId) && !this.permanentCountries.some((x) => x.countryId === data.countryId)) {
          this.presentCountries.push(data);
          this.permanentCountries.push(data);
        }
      }
    });

  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.studentSubscription) this.studentSubscription.unsubscribe();
  }

  applyFilterDataTable(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) this.dataSource.paginator.firstPage();

  }

  getStudentPhotoUrl(photo: Uint8Array | null): string {
    if (photo && photo.length > 0) {
      const base64Image = this.arrayBufferToBase64(photo);
      return 'data:image/jpeg;base64,' + photo;
    }
    // Return a placeholder image URL if no photo is available
    return 'assets/logo/Pbd-Logo.png';
  }

  private arrayBufferToBase64(buffer: Uint8Array): string {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
  }

  onLoad() {
    this.studentSubscription = this.services.getStudents().subscribe({
      next: (response: EduAStudent[]) => {
        this.students = response;
        this.dataSource = new MatTableDataSource<EduAStudent>(this.students);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.isLoadingResults = false;
        this.initForm();
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      }
    });
  }

  //#region  autoComplete

  // Present Address
  applyFilterPresentPS(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'presentPSId') {
      this.filterPresentPSs = [];
      this.filterPresentPSs = this.presentPSs.filter((x: GenConfigDPoliceStation) => x.policeStationName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  applyFilterPresentDistrict(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'presentDistrictId') {
      this.filterPresentDistricts = [];
      this.filterPresentDistricts = this.presentDistricts.filter((x: GenConfigCDistrictOrCity) => x.districtName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  applyFilterPresentDivision(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'presentDivisionId') {
      this.filterPresentDivisions = [];
      this.filterPresentDivisions = this.presentDivisions.filter((x: genConfigBDivisionOrState) => x.divisionName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  applyFilterPresentCountry(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'presentCountryId') {
      this.filterPresentCountries = [];
      this.filterPresentCountries = this.presentCountries.filter((x: GenConfigACountry) => x.countryName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  // Permanent Address
  applyFilterPermanentPS(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'permanentPSId') {
      this.filterPermanentPSs = [];
      this.filterPermanentPSs = this.permanentPSs.filter((x: GenConfigDPoliceStation) => x.policeStationName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
    console.log(this.filterPermanentPSs);
  }

  applyFilterPermanentDistrict(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'permanentDistrictId') {
      this.filterPermanentDistricts = [];
      this.filterPermanentDistricts = this.permanentDistricts.filter((x: GenConfigCDistrictOrCity) => x.districtName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  applyFilterPermanentDivision(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'permanentDivisionId') {
      this.filterPermanentDivisions = [];
      this.filterPermanentDivisions = this.permanentDivisions.filter((x: genConfigBDivisionOrState) => x.divisionName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  applyFilterPermanentCountry(event: Event, field: string) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (field === 'permanentCountryId') {
      this.filterPermanentCountries = [];
      this.filterPermanentCountries = this.permanentCountries.filter((x: GenConfigACountry) => x.countryName.toLowerCase().trim().includes(filterValue.toLowerCase().trim()))
    }
  }

  onGenderSelected(event: MatAutocompleteSelectedEvent) {
    const gender: GenConfigEGender = event.option.value;
    this.studentForm.patchValue({
      genderId: gender.genderId,
      genderName: gender.genderName
    });
  }

  onBloodGroupSelected(event: MatAutocompleteSelectedEvent) {
    const bloodGroup: GenConfigFBloodGroup = event.option.value;
    this.studentForm.patchValue({
      bloodGroupId: bloodGroup.bloodGroupId,
      bloodGroupName: bloodGroup.bloodGroupName
    });
  }

  onReligionSelected(event: MatAutocompleteSelectedEvent) {
    const religion: GenConfigGReligion = event.option.value;
    this.studentForm.patchValue({
      religionId: religion.religionId,
      religionName: religion.religionName
    });
  }

  // Present Address
  onPresentPSSelected(event: MatAutocompleteSelectedEvent) {
    const presentPS: GenConfigDPoliceStation = event.option.value;
    console.log(presentPS);
    this.studentForm.patchValue({
      presentPoliceStationId: presentPS.policeStationId,
      presentPoliceStationName: presentPS.policeStationName
    });
    console.log(this.studentForm.value);
  }

  onPresentDistrictSelected(event: MatAutocompleteSelectedEvent) {
    const presentDistrict: GenConfigCDistrictOrCity = event.option.value;
    console.log(presentDistrict);
    this.studentForm.patchValue({
      presentDistrictId: presentDistrict.districtId,
      presentDistrictName: presentDistrict.districtName
    });
    console.log(this.studentForm.value);
  }

  onPresentDivisionSelected(event: MatAutocompleteSelectedEvent) {
    const presentDivision: genConfigBDivisionOrState = event.option.value;
    this.studentForm.patchValue({
      presentDivisionId: presentDivision.divisionId,
      presentDivisionName: presentDivision.divisionName
    });
  }

  onPresentCountrySelected(event: MatAutocompleteSelectedEvent) {
    const presentCountry: GenConfigACountry = event.option.value;
    this.studentForm.patchValue({
      presentCountryId: presentCountry.countryId,
      presentCountryName: presentCountry.countryName
    });
  }

  // Permanent Address
  onPermanentPSSelected(event: MatAutocompleteSelectedEvent) {
    const permanentPS: GenConfigDPoliceStation = event.option.value;
    this.studentForm.patchValue({
      permanentPoliceStationId: permanentPS.policeStationId,
      permanentPoliceStationName: permanentPS.policeStationName
    });
  }

  onPermanentDistrictSelected(event: MatAutocompleteSelectedEvent) {
    const permanentDistrict: GenConfigCDistrictOrCity = event.option.value;
    this.studentForm.patchValue({
      permanentDistrictId: permanentDistrict.districtId,
      permanentDistrictName: permanentDistrict.districtName
    });
  }

  onPermanentDivisionSelected(event: MatAutocompleteSelectedEvent) {
    const permanentDivision: genConfigBDivisionOrState = event.option.value;
    this.studentForm.patchValue({
      permanentDivisionId: permanentDivision.divisionId,
      permanentDivisionName: permanentDivision.divisionName
    });
  }

  onPermanentCountrySelected(event: MatAutocompleteSelectedEvent) {
    const permanentCountry: GenConfigACountry = event.option.value;
    this.studentForm.patchValue({
      permanentCountryId: permanentCountry.countryId,
      permanentCountryName: permanentCountry.countryName
    });
  }

  //#endregion

  onFileSelected(event: any): void {
    const files = event.target.files;
    if (files.length > 0) {
      this.selectedFile = files[0];
      if (this.selectedFile instanceof File) {
        this.previewImage(this.selectedFile);
      }
    }
    console.log(this.selectedFile);
  }

  private previewImage(file: File): void {
    const reader = new FileReader();
    reader.onload = () => {
      this.imagePreviewUrl = reader.result; // Ensure reader.result is of type string or ArrayBuffer
    };
    reader.readAsDataURL(file);
  }

  onSubmit() {
    if (this.studentForm.valid) {
      const formData: EduAStudent = this.studentForm.value;
      if (formData.studentId) {
        // Update existing student
        this.updateRecord();
      } else {
        // Insert new student
        this.insertRecord();
      }
    }
  }

  insertRecord() {
    debugger;
    if (this.studentForm.valid) {
      const formData = new FormData();
      const formValue = this.studentForm.value;
      console.log(this.studentForm.value);
      for (const key in formValue) {
        if (formValue.hasOwnProperty(key)) {
          let value = formValue[key];

          // Set studentId to 0 and other IDs to 1 if value is 0
          if (key === 'studentId' && value !== 0) {
            value = 0;
          }
          formData.append(key, value);
        }
      }
      formData.forEach((value, key) => {
        console.log(key, value);
      });


      // Append the file if selected
      if (this.selectedFile) {
        formData.append('studentImage', this.selectedFile, this.selectedFile.name);
      }

      this.services.insertStudent(formData).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record inserted successfully!', 'Dismiss', {
          duration: 1500,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }

  onEdit(studentId: number) {
    if (studentId) {
      // Assuming you have a service method to fetch a single student by studentId
      this.services.getByStudentId(studentId).subscribe((response) => {
        console.log(response);
        if (response) {
          this.student = response;
          this.studentForm.patchValue(response);
          this.imagePreviewUrl = 'data:image/jpeg;base64,' + response.studentPhoto;
        }
      });
    }
  }

  updateRecord() {
    debugger;
    if (this.studentForm.valid) {
      const formValue = this.studentForm.value;

      formValue.presentCountryId = (formValue.presentCountryName !== this.student.presentCountryName) ? 0 : formValue.presentCountryId;
      formValue.presentDivisionId = (formValue.presentDivisionName !== this.student.presentDivisionName) ? 0 : formValue.presentDivisionId;
      formValue.presentDistrictId = (formValue.presentDistrictName !== this.student.presentDistrictName) ? 0 : formValue.presentDistrictId;
      formValue.presentPoliceStationId = (formValue.presentPoliceStationName !== this.student.presentPoliceStationName) ? 0 : formValue.presentPoliceStationId;
      formValue.permanentCountryId = (formValue.permanentCountryName !== this.student.permanentCountryName) ? 0 : formValue.permanentCountryId;
      formValue.permanentDivisionId = (formValue.permanentDivisionName !== this.student.permanentDivisionName) ? 0 : formValue.permanentDivisionId;
      formValue.permanentDistrictId = (formValue.permanentDistrictName !== this.student.permanentDistrictName) ? 0 : formValue.permanentDistrictId;
      formValue.permanentPoliceStationId = (formValue.permanentPoliceStationName !== this.student.permanentPoliceStationName) ? 0 : formValue.permanentPoliceStationId;

      // console.log(this.studentForm.hasOwnProperty('presentCountryName'))
      console.log(this.student);
      console.log(this.studentForm.value);
      console.log(formValue);
      debugger;

      // Loop through the form values and append them to formData
      const formData = new FormData();
      for (const key in formValue) {
        if (this.studentForm.value.hasOwnProperty(key)) {
          const value = this.studentForm.value[key];
          formData.append(key, value);
        }
      }

      // formData.forEach((value, key) =>
      //   console.log(key, value)
      // );

      debugger;
      // Append the file if selected
      if (this.selectedFile) {
        formData.append('studentImage', this.selectedFile, this.selectedFile.name);
      }
      debugger;
      this.services.updateStudent(this.studentForm.value.studentId, formData).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open(this.studentForm.value.studentName + ' updated successfully!', 'Dismiss', {
          duration: 1500,
        });
      });
    } else {
      this.snackbar.open('Please fill in the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }


    // if (this.studentForm.valid) {
    //   const updateRecord: EduAStudent = this.studentForm.value;
    //   this.services.updateStudent(updateRecord).subscribe(() => {
    //     this.resetForm();
    //     this.onLoad();
    //     this.snackbar.open('Record updated successfully!', 'Dismiss', {
    //       duration: 1500
    //     });
    //   });
    // } else {
    //   this.snackbar.open('Please fill the required fields!', 'Dismiss', {
    //     duration: 2000
    //   })
    // }
  }

  onDetail(studentId: number) {
    this.services.getByStudentId(studentId)
      .subscribe({
        next: (record) => {
          console.log('API Response:', record);

          if (record !== null) {
            this.dialog.open(this.detailDialog, {
              width: '100%',
              data: record,
            });
          } else {
            // Show an error message to the user.
            console.log("No student record found.");
          }
        },
        error: (error) => {
          console.error('API Error:', error);
          // Handle API errors here, e.g., show an error message.
        },
      });
  }

  onDelete(row: EduAStudent) {
    console.log(row);
    const dialogRef = this.dialog.open(this.deleteDialog, {
      width: '500px',
      data: row
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteRecord(row);
      }
    });
  }

  private deleteRecord(row: EduAStudent) {
    this.services.deleteStudent(row.studentId).subscribe(() => {
      this.onLoad();
      this.snackbar.open('Record deleted successfully!', 'Dismiss', {
        duration: 3000
      });
    });
  }
}
